namespace VCodeHunt
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;
    using VCodeHunt.Config;
    using System.Text;

    public partial class FormMain : Form
    {
        AppConfig m_config;
        Thread m_searchThread = null;

        enum UpdateStatusType
        {
            STATUS_BAR,
            STATUS_BUTTON,
            STATUS_TITLE
        };

        static string s_AppName = "VCodeHunt";
        delegate void UpdateStatusCallback(UpdateStatusType type, string msg);
        UpdateStatusCallback m_updateStatusCallback;

        delegate void AddFileMatchCallback(FileMatch match);
        AddFileMatchCallback m_addFileMatchCallback;

        BindingList<FileMatch> m_fileMatchBinding = new BindingList<FileMatch>();
        Dictionary<string, List<SearchFile.KeywordMatch>> m_keywordMatches = new Dictionary<string, List<SearchFile.KeywordMatch>>();
        Dictionary<string, List<Int64>> m_matchToLineTable = new Dictionary<string, List<long>>();

        string[] m_buttonActionLabels = { "&Search (F5)", "&Cancel (F5)" };

        SearchFile m_filesearch = new SearchFile();
        EventWaitHandle m_terminate = new EventWaitHandle(false, EventResetMode.ManualReset);

        enum Actions
        {
            Search = 0,
            Cancel = 1
        }

        public FormMain()
        {
            InitializeComponent();
            this.Text = s_AppName;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            m_updateStatusCallback = new UpdateStatusCallback(UpdateStatus);
            m_addFileMatchCallback = new AddFileMatchCallback(AddFileMatch);

            Type searchType = typeof(FileContentType);
            foreach (FileContentType type in Enum.GetValues(searchType))
            {
                if (type != FileContentType.None)
                    cbFileContentType.Items.Add(type);
            }
            
            SetDefaultConfig();
            ReadConfig();

            BindViews();
            LoadWindowState();
            
            try
            {
                lblVersion.Text = string.Format("Version: {0}", System.Diagnostics.Process.GetCurrentProcess().MainModule.FileVersionInfo.FileVersion.ToString());
            }
            catch (SystemException ex)
            {
                Debug.WriteLine("[{0}][{1}]", ex.Source, ex.Message);
            }

            UpdateStatus(UpdateStatusType.STATUS_BUTTON, m_buttonActionLabels[System.Convert.ToInt32(Actions.Search)]);
        }

        void BindViews()
        {
            // file match view
            viewFiles.AutoGenerateColumns = false;

            viewFiles.AllowUserToAddRows = false;
            viewFiles.AllowUserToDeleteRows = false;
            viewFiles.AllowUserToOrderColumns = false;
            viewFiles.AllowUserToResizeRows = false;
            viewFiles.ReadOnly = true;
            viewFiles.EditMode = DataGridViewEditMode.EditProgrammatically;
            viewFiles.AllowUserToResizeColumns = true;
            viewFiles.RowHeadersVisible = false;
            viewFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            Type filetype = typeof(FileMatch);

            PropertyInfo []pinfo = filetype.GetProperties();
            for(int idx = 0; idx < pinfo.Count(); idx++)
            {
                if (pinfo[idx].PropertyType.IsPublic)
                {
                    DataGridViewTextBoxColumn colTextBox = new DataGridViewTextBoxColumn();
                    colTextBox.CellTemplate = new DataGridViewTextBoxCell();
                    colTextBox.Name = pinfo[idx].Name;
                    colTextBox.HeaderText = pinfo[idx].Name;
                    colTextBox.DataPropertyName = pinfo[idx].Name;

                    if (pinfo[idx].Name == "Path")
                    {
                        colTextBox.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    else if (pinfo[idx].Name == "File")
                    {
                        colTextBox.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    else
                    {
                        colTextBox.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    }

                    viewFiles.Columns.Add(colTextBox);
                }
            }

            viewFiles.DataSource = m_fileMatchBinding;
        }

        bool IsWindowStateValid(Point location, Size size)
        {
            Rectangle rect = new Rectangle(location, size);
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.IntersectsWith(rect))
                {
                    return true;
                }
            }
            return false;
        }

        void LoadWindowState()
        {
            bool state = false;
            if (m_config.WindowState.UseSize)
            {
                state = true;
                this.Size = m_config.WindowState.Size;
            }

            if (m_config.WindowState.UseLocation)
            {
                state = true;
                this.Location = m_config.WindowState.Location;
            }

            if (m_config.WindowState.UseState)
            {
                state = true;
                this.WindowState = m_config.WindowState.State;
            }

            this.StartPosition = (state && IsWindowStateValid(this.Location, this.Size)) ? FormStartPosition.Manual : FormStartPosition.WindowsDefaultLocation;

            if (m_config.WindowFont.UseFont)
            {
                SetWindowFont(m_config.WindowFont.Font);
            }
            else
            {
                Font font = new Font("Courier New", (float)8.25);
                SetWindowFont(font);
            }

        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    cbPath.Focus();
                    e.Handled = true;
                    break;
                case Keys.F3:
                    cbFilters.Focus();
                    e.Handled = true;
                    break;
                case Keys.F4:
                    cbKeywords.Focus();
                    e.Handled = true;
                    break;
                case Keys.F5:
                    Search();
                    e.Handled = true;
                    break;
                case Keys.F6:
                    BrowseSearchPath();
                    e.Handled = true;
                    break;
                case Keys.F7:
                    cbSearchHistory.Focus();
                    e.Handled = true;
                    break;
                case Keys.Enter:
                    // prevent double 'click'
                    if (!this.btnAction.Focused)
                    {
                        Search();
                    }
                    e.Handled = true;
                    break;
            }
        }

        SearchParams UserSelectedConfig()
        {
            SearchParams config = new SearchParams();
            config.Path = cbPath.Text;
            config.Filters = cbFilters.Text;
            config.Keywords = cbKeywords.Text;
            config.FileType = (FileContentType)cbFileContentType.Items[cbFileContentType.SelectedIndex];

            config.UseCaseSensitiveMatch = cbSearchCaseSensitive.Checked;
            config.UseRegexMatch = cbSearchUseRegex.Checked;
            config.UseWholeWordMatch = cbSearchWholeWord.Checked;
            config.UseNegateSearch = cbSearchNegate.Checked;

            config.ShowLineNumbers = cbSearchShowLineNumbers.Checked;
            config.UseSubFolders = cbSearchSubFolders.Checked;

            config.ShowContextLines = cbShowContext.Checked;
            config.ContextLinesCount = (int)nudContextLines.Value;

            config.UseMinFileSize = cbMinFileSize.Checked;
            config.MinFileSize = (int)nudMinFileSize.Value;

            config.UseMaxFileSize = cbMaxFileSize.Checked;
            config.MaxFileSize = (int)nudMaxFileSize.Value;

            return config;
        }

        void Search()
        {
            if (m_searchThread != null)
            {
                SearchAbort();
                return;
            }

            SearchParams config = UserSelectedConfig();
            m_terminate.Reset();
            m_fileMatchBinding.Clear();
            m_keywordMatches.Clear();
            m_matchToLineTable.Clear();
            viewMatches.Clear();

            if (string.IsNullOrEmpty(config.Path) || string.IsNullOrEmpty(config.Filters) || string.IsNullOrEmpty(config.Keywords))
            {
                return;
            }

            m_config.AddSearchParams(config);

            WriteConfig();
            ReadConfig();

            m_searchThread = new Thread(SearchThread);
            m_searchThread.IsBackground = true;
            m_searchThread.Start(config);
        }

        void SearchAbort()
        {
            m_terminate.Set();
            m_filesearch.Teminate();
        }

        class FileMatch
        {
            private string m_fileName;
            private long m_count;

            public FileMatch(string fileName, long count)
            {
                m_fileName = fileName;
                m_count = count;
            }

            public string GetFullFileName() { return m_fileName; }

            public string File { get { return System.IO.Path.GetFileName(m_fileName); } }
            public string Path { get { return System.IO.Path.GetDirectoryName(m_fileName); } }

            public long Count { get { return m_count; } }
            public string LastModified { get { FileInfo fi = new FileInfo(m_fileName); return fi.LastWriteTime.ToString("g");   } }
        };

        void AddFileMatch(FileMatch match)
        {
            if (viewFiles.InvokeRequired)
            {
                viewFiles.Invoke(m_addFileMatchCallback, new object[] { match });
                return;
            }

            m_fileMatchBinding.Add(match);
        }

        void SearchThread(object data)
        {
            SearchParams context = (SearchParams)data;

            UpdateStatus(UpdateStatusType.STATUS_TITLE, string.Format("{0} - {1}", s_AppName, context.Path));
            UpdateStatus(UpdateStatusType.STATUS_BAR, "Searching...");
            UpdateStatus(UpdateStatusType.STATUS_BUTTON, m_buttonActionLabels[System.Convert.ToInt32(Actions.Cancel)]);

            if (!File.Exists(context.Path) && !Directory.Exists(context.Path))
            {
                UpdateStatus(UpdateStatusType.STATUS_BAR, string.Format("Path not found: [{0}]", context.Path));
                UpdateStatus(UpdateStatusType.STATUS_BUTTON, m_buttonActionLabels[System.Convert.ToInt32(Actions.Search)]);
                m_searchThread = null;
                return;
            }

            Int64 filecount = 0;
            Int64 matchcount = 0;

            try
            {
                string[] filters = context.Filters.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                SearchOption directorySearchOption = context.UseSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                foreach (string file in Directory.EnumerateFiles(context.Path, "*", directorySearchOption))
                {
                    if (m_terminate.WaitOne(0))
                    {
                        break;
                    }

                    try
                    {
                        FileInfo finfo = new FileInfo(file);
                        if ((context.UseMinFileSize && finfo.Length < context.MinFileSize))
                        {
                            UpdateStatus(UpdateStatusType.STATUS_BAR, "Skip File (min size): " + file);
                            continue;
                        }
                        else if ((context.UseMaxFileSize && finfo.Length > context.MaxFileSize))
                        {
                            UpdateStatus(UpdateStatusType.STATUS_BAR, "Skip File (max size): " + file);
                            continue;
                        }
                    }
                    catch (Exception)
                    { 
                    }

                    foreach (string filter in filters)
                    {
                        if (m_terminate.WaitOne(0))
                        {
                            break;
                        }

                        List<SearchFile.KeywordMatch> matches = null;
                        try
                        {
                            if (Like(file, filter))
                            {
                                UpdateStatus(UpdateStatusType.STATUS_BAR, "Scanning file: " + file);
                                matches = m_filesearch.Search(context, file);
                            }

                            if (matches != null)
                            {
                                matchcount += matches.Count;

                                m_keywordMatches[file] = matches;
                                m_matchToLineTable[file] = new List<long>();

                                AddFileMatch(new FileMatch(file, matches.Count));
                            }
                        }
                        catch (SystemException ex)
                        {
                            Debug.WriteLine("[{0}][{1}]", ex.Source, ex.Message);
                        }
                    }

                    filecount++;
                }
            }
            catch (SystemException ex)
            {
                Debug.WriteLine("[{0}][{1}]", ex.Source, ex.Message);
            }

            UpdateStatus(UpdateStatusType.STATUS_BAR, string.Format("Search complete: Files: {0} Matches: {1}", filecount, matchcount));
            UpdateStatus(UpdateStatusType.STATUS_BUTTON, m_buttonActionLabels[System.Convert.ToInt32(Actions.Search)]);

            m_searchThread = null;
        }

        public static bool Like(string value, string pattern)
        {
            // check if wildcards exist, simple string comparison
            char [] wildcards = new char [] { '*', '?' };
            if (pattern.IndexOfAny(wildcards) == -1)
            {
                return value.EndsWith(pattern, StringComparison.CurrentCultureIgnoreCase);
            }

            string regexPattern = "^" + Regex.Escape(pattern).Replace(@"\*", ".*").Replace(@"\?", ".") + "$";
            return new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(value);
        }

        private void SetDefaultConfig()
        {
            SearchParams config = new SearchParams();
            SetConfig(config);
        }

        private void WriteConfig()
        {
            try
            {
                AppConfigIO.Write<AppConfig>(m_config);
            }
            catch (SystemException ex)
            {
                Debug.WriteLine("[{0}][{1}]", ex.Source, ex.Message);
            }
        }

        private void ReadConfig()
        {
            AppConfig config = new AppConfig();

            try
            {
                AppConfigIO.Read<AppConfig>(ref config);
            }
            catch (SystemException ex)
            {
                Debug.WriteLine("[{0}][{1}]", ex.Source, ex.Message);
            }

            cbPath.Items.Clear();
            cbFilters.Items.Clear();
            cbKeywords.Items.Clear();
            cbSearchHistory.Items.Clear();

            foreach (SearchParams searchParams in config.SearchParamsHistory)
            {
                if (!cbPath.Items.Contains(searchParams.Path))
                    cbPath.Items.Add(searchParams.Path);
                if (!cbFilters.Items.Contains(searchParams.Filters))
                    cbFilters.Items.Add(searchParams.Filters);
                if (!cbKeywords.Items.Contains(searchParams.Keywords))
                    cbKeywords.Items.Add(searchParams.Keywords);

                cbSearchHistory.Items.Add(searchParams.DisplayID);
            }

            m_config = config;
            SetHistoryConfig(0);
        }

        private void SetHistoryConfig(int selectedIndex)
        {
            if (selectedIndex >= cbSearchHistory.Items.Count)
            {
                return;
            }

            string displayid = cbSearchHistory.Items[selectedIndex].ToString();
            foreach (SearchParams config in m_config.SearchParamsHistory)
            {
                if (displayid == config.DisplayID)
                {
                    SetConfig(config);
                    break;
                }
            }
        }

        private void SetConfig(SearchParams config)
        {
            if (null == config)
            {
                return;
            }

            for (int idx = 0; idx < cbPath.Items.Count; idx++)
            {
                if (cbPath.Items[idx].ToString() == config.Path)
                {
                    cbPath.SelectedIndex = idx;
                    break;
                }
            }

            for (int idx = 0; idx < cbFilters.Items.Count; idx++)
            {
                if (cbFilters.Items[idx].ToString() == config.Filters)
                {
                    cbFilters.SelectedIndex = idx;
                    break;
                }
            }

            for (int idx = 0; idx < cbKeywords.Items.Count; idx++)
            {
                if (cbKeywords.Items[idx].ToString() == config.Keywords)
                {
                    cbKeywords.SelectedIndex = idx;
                    break;
                }
            }

            for (int idx = 0; idx < cbFileContentType.Items.Count; idx++)
            {
                if ((FileContentType)cbFileContentType.Items[idx] == config.FileType)
                {
                    cbFileContentType.SelectedIndex = idx;
                    break;
                }
            }

            cbSearchCaseSensitive.Checked = config.UseCaseSensitiveMatch;
            cbSearchWholeWord.Checked = config.UseWholeWordMatch;
            cbSearchUseRegex.Checked = config.UseRegexMatch;
            cbSearchNegate.Checked = config.UseNegateSearch;
            cbSearchSubFolders.Checked = config.UseSubFolders;
            cbSearchShowLineNumbers.Checked = config.ShowLineNumbers;

            cbShowContext.Checked = config.ShowContextLines;
            nudContextLines.Value = config.ContextLinesCount;

            cbMinFileSize.Checked = config.UseMinFileSize;
            nudMinFileSize.Value = config.MinFileSize;

            cbMaxFileSize.Checked = config.UseMaxFileSize;
            nudMaxFileSize.Value = config.MaxFileSize;

            cbSearchHistory.Text = config.DisplayID;
        }

        private void UpdateStatus(UpdateStatusType type, string msg)
        {
            if (statusStrip.InvokeRequired)
            {
                statusStrip.Invoke(m_updateStatusCallback, new object[] {type, msg});
                return;
            }

            switch(type)
            {
                case UpdateStatusType.STATUS_BAR:
                    toolStripStatusLabel.Text = msg;
                    break;

                case UpdateStatusType.STATUS_BUTTON:
                    btnAction.Text = msg;
                    break;

                case UpdateStatusType.STATUS_TITLE:
                    this.Text = msg;
                    break;
            }
        }

        private void ShowKeywordResults(SearchParams config, int selectIndex)
        {
            viewMatches.Clear();
            viewMatches.SuspendLayout();

            try
            {

                string fileName = m_fileMatchBinding.ElementAt(selectIndex).GetFullFileName();
                List<SearchFile.KeywordMatch> matches = m_keywordMatches[fileName];
                foreach (SearchFile.KeywordMatch match in matches)
                {
                    HighlightKeyWords(config, match, fileName);
                }

            }
            catch (SystemException ex)
            {
                Debug.WriteLine("[{0}][{1}]", ex.Source, ex.Message);
            }

            viewMatches.ResumeLayout();
        }

        private void HighlightKeyWords(SearchParams config, SearchFile.KeywordMatch match, string fileName)
        {
            string text = match.ToString(config);
            if (config.UseNegateSearch)
            {
                viewMatches.SelectedText = text;
            }
            else
            {
                int lines = (text.Count(p => p.CompareTo('\n') == 0));
                m_matchToLineTable[fileName].AddRange(Enumerable.Repeat(match.Index, lines));

                int textIndex = 0;
                int matchIndex = text.IndexOf(match.Match, 0);
                do
                {
                    matchIndex = text.IndexOf(match.Match, textIndex);
                    if (matchIndex == -1)
                    {
                        // no more matches
                        break;
                    }

                    viewMatches.SelectionColor = Color.Black;
                    viewMatches.SelectedText = text.Substring(textIndex, matchIndex - textIndex);

                    // highlight text
                    viewMatches.SelectionColor = Color.Red;
                    viewMatches.SelectedText = text.Substring(matchIndex, match.Match.Length);

                    textIndex = matchIndex + match.Match.Length;

                } while (true);

                viewMatches.SelectionColor = Color.Black;
                viewMatches.SelectedText = text.Substring(textIndex);
            }
        }

        private void HighlightKeyWords(SearchParams config, List<SearchFile.KeywordMatch> matches, bool onlyVisible = false)
        {
            int firstIndex = 0;
            int lastIndex = viewMatches.Text.Length;

            // highlight only visible area - performance
            if (onlyVisible)
            {
                Point pos = new Point(0, 0);
                firstIndex = viewMatches.GetCharIndexFromPosition(pos);

                pos.X = viewMatches.ClientRectangle.Width;
                pos.Y = viewMatches.ClientRectangle.Height;

                lastIndex = viewMatches.GetCharIndexFromPosition(pos);
            }

            int matchindex = 0;
            System.StringComparison compare = config.UseCaseSensitiveMatch ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;
            string viewContent = viewMatches.Text;
                    
            int keywordIndex = viewContent.IndexOf(matches[matchindex].Match, firstIndex, compare);
            while (-1 != keywordIndex && keywordIndex < lastIndex)
            {
                viewMatches.Select(keywordIndex, matches[matchindex].Match.Length);
                viewMatches.SelectionColor = Color.Red;

                matchindex++;
                if (matchindex >= matches.Count)
                {
                    break;
                }

                keywordIndex = viewContent.IndexOf(matches[matchindex].Match, keywordIndex + matches[matchindex].Match.Length, compare);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            BrowseSearchPath();
        }

        private void BrowseSearchPath()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cbPath.Text = fbd.SelectedPath;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormViewers fe = new FormViewers(m_config);
            if (fe.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                AppConfigIO.Write<AppConfig>(m_config);
            }
        }

        private void LaunchFolder(string path)
        {
            Process.Start(path);
        }

        private bool FitsMask(string fileName, string fileMask)
        {
            string pattern =
                 '^' +
                 Regex.Escape(fileMask.Replace(".", "__DOT__")
                                 .Replace("*", "__STAR__")
                                 .Replace("?", "__QM__"))
                     .Replace("__DOT__", "[.]")
                     .Replace("__STAR__", ".*")
                     .Replace("__QM__", ".")
                 + '$';
            return new Regex(pattern, RegexOptions.IgnoreCase).IsMatch(fileName);
        }

        private bool FitsOneOfMultipleMasks(string fileName, string fileMasks)
        {
            return fileMasks
                .Split(new string[] { "\r\n", "\n", ",", "|", " ", ";"},
                    StringSplitOptions.RemoveEmptyEntries)
                .Any(fileMask => FitsMask(fileName, fileMask));
        }

        private void LaunchViewer(string viewFile, long lineNumber)
        {
            if (!File.Exists(viewFile) || null == m_config.Viewers)
            {
                return;
            }

            Viewer viewer = null;
            for (int idx = 0; idx < m_config.Viewers.Count; idx++)
            {
                if (FitsOneOfMultipleMasks(viewFile, m_config.Viewers[idx].Extensions))
                {
                    viewer = m_config.Viewers[idx];
                    break;
                }
            }

            if (null != viewer)
            {
                string fileName = string.Format("\"{0}\"", viewer.AppPath);
                string args = viewer.AppArgs;
                args = args.Replace("%1", viewFile);
                args = args.Replace("%2", lineNumber.ToString());

                Process.Start(fileName, args);
            }
        }

        private void viewKeywordMatches_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs m = e as MouseEventArgs;
            Point pos = new Point(m.X, m.Y);
            int charindex = viewMatches.GetCharIndexFromPosition(pos);
            if (charindex == -1)
            {
                return;
            }

            int lineindex = viewMatches.GetLineFromCharIndex(charindex);
            if (lineindex == -1)
            {
                return;
            }

            DataGridViewSelectedRowCollection rows = viewFiles.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                FileMatch fm = row.DataBoundItem as FileMatch;

                string fileName = fm.GetFullFileName();
                LaunchViewer(fileName, m_matchToLineTable[fileName][lineindex]);
            }
        }

        private void cbSearchHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = cbSearchHistory.SelectedIndex;
            if (selectedIndex == -1)
            {
                selectedIndex = 0;
            }

            SetHistoryConfig(selectedIndex);
        }

        private void clearHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_config.SearchParamsHistory.RemoveRange(1, m_config.SearchParamsHistory.Count - 1);

            WriteConfig();
            ReadConfig();
        }

        private void CopyToClipboard(string msg)
        {
            Clipboard.SetText(msg);
        }

        private void copyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            DataGridViewSelectedRowCollection rows = viewFiles.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                // only prefix newline when needed
                if (sb.Length != 0)
                {
                    sb.Append(Environment.NewLine);
                }

                FileMatch fm = row.DataBoundItem as FileMatch;
                sb.Append(Path.GetDirectoryName(fm.GetFullFileName()));
            }

            CopyToClipboard(sb.ToString());
        }

        private void copyFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            DataGridViewSelectedRowCollection rows = viewFiles.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                // only prefix newline when needed
                if(sb.Length != 0) 
                {
                    sb.Append(Environment.NewLine);
                }

                FileMatch fm = row.DataBoundItem as FileMatch;
                sb.Append(Path.GetFileName(fm.GetFullFileName()));
            }

            CopyToClipboard(sb.ToString());
        }

        private void copyPathAndFileNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            DataGridViewSelectedRowCollection rows = viewFiles.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                // only prefix newline when needed
                if (sb.Length != 0)
                {
                    sb.Append(Environment.NewLine);
                }

                FileMatch fm = row.DataBoundItem as FileMatch;
                sb.Append(fm.GetFullFileName());
            }

            CopyToClipboard(sb.ToString());
        }

        private void viewFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
            {
                return;
            }

            DataGridViewSelectedRowCollection rows = viewFiles.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                FileMatch fm = row.DataBoundItem as FileMatch;
                LaunchViewer(fm.GetFullFileName(), 0);
            }
        }

        private void viewFiles_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = viewFiles.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                ShowKeywordResults(m_config.SearchParamsHistory.First(), row.Index);
            }
        }

        private void viewFiles_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                int currentMouseOverRow = viewFiles.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow >= 0 && currentMouseOverRow < viewFiles.Rows.Count)
                {
                    viewFiles.Rows[currentMouseOverRow].Selected = true;
                    cmFileMatches.Show(viewFiles, new Point(e.X, e.Y));
                }
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = viewFiles.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                FileMatch fm = row.DataBoundItem as FileMatch;
                LaunchViewer(fm.GetFullFileName(), 0);
            }
        }

        private void openPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = viewFiles.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                FileMatch fm = row.DataBoundItem as FileMatch;
                LaunchFolder(Path.GetDirectoryName(fm.GetFullFileName()));
            }
        }
        
        private void cbShowContext_CheckedChanged(object sender, EventArgs e)
        {
            nudContextLines.Enabled = cbShowContext.Checked;
        }

        private void cbMinFileSize_CheckedChanged(object sender, EventArgs e)
        {
            nudMinFileSize.Enabled = cbMinFileSize.Checked;
        }

        private void cbMaxFileSize_CheckedChanged(object sender, EventArgs e)
        {
            nudMaxFileSize.Enabled = cbMaxFileSize.Checked;
        }

        private void nudMinFileSize_ValueChanged(object sender, EventArgs e)
        {
            if (cbMaxFileSize.Checked && (Int64)nudMinFileSize.Value > (Int64)nudMaxFileSize.Value)
            {
                nudMinFileSize.Value = nudMaxFileSize.Value;
            }
        }

        private void nudMaxFileSize_ValueChanged(object sender, EventArgs e)
        {
            if (cbMinFileSize.Checked && (Int64)nudMaxFileSize.Value < (Int64)nudMinFileSize.Value)
            {
                nudMaxFileSize.Value = nudMinFileSize.Value;
            }
        }

        private void FormMain_Move(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                m_config.WindowState.SetLocation(this.Location);
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Normal)
                m_config.WindowState.SetSize(this.Size);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_config.WindowState.SetState(this.WindowState);
            WriteConfig();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectFont();
        }

        private void SelectFont()
        {
            FormFont ff = new FormFont(m_config.WindowFont.Font);
            DialogResult result = ff.ShowDialog(this);
            if (System.Windows.Forms.DialogResult.OK == result)
            {
                m_config.WindowFont.SetFont(ff.Selected);
                SetWindowFont(m_config.WindowFont.Font);
            }
        }

        private void SetWindowFont(Font font)
        {
            cbPath.Font = font;
            cbFilters.Font = font;
            cbKeywords.Font = font;
            cbSearchHistory.Font = font;
            viewFiles.Font = font;
            viewMatches.Font = font;
        }
    }
}
