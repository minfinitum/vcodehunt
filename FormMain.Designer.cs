namespace VCodeHunt
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.viewFiles = new System.Windows.Forms.DataGridView();
            this.viewMatches = new System.Windows.Forms.RichTextBox();
            this.cmFileMatches = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPathAndFileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblSearchHistory = new System.Windows.Forms.Label();
            this.cbSearchHistory = new System.Windows.Forms.ComboBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.cbPath = new System.Windows.Forms.ComboBox();
            this.btnAction = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblFilter = new System.Windows.Forms.Label();
            this.cbFilters = new System.Windows.Forms.ComboBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.cbKeywords = new System.Windows.Forms.ComboBox();
            this.nudMaxFileSize = new System.Windows.Forms.NumericUpDown();
            this.nudMinFileSize = new System.Windows.Forms.NumericUpDown();
            this.cbMaxFileSize = new System.Windows.Forms.CheckBox();
            this.cbShowContext = new System.Windows.Forms.CheckBox();
            this.cbSearchCaseSensitive = new System.Windows.Forms.CheckBox();
            this.cbSearchWholeWord = new System.Windows.Forms.CheckBox();
            this.cbSearchUseRegex = new System.Windows.Forms.CheckBox();
            this.cbSearchNegate = new System.Windows.Forms.CheckBox();
            this.cbSearchSubFolders = new System.Windows.Forms.CheckBox();
            this.cbSearchShowLineNumbers = new System.Windows.Forms.CheckBox();
            this.cbMinFileSize = new System.Windows.Forms.CheckBox();
            this.nudContextLines = new System.Windows.Forms.NumericUpDown();
            this.lblFileContentType = new System.Windows.Forms.Label();
            this.cbFileContentType = new System.Windows.Forms.ComboBox();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewFiles)).BeginInit();
            this.cmFileMatches.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxFileSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinFileSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudContextLines)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 539);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip.TabIndex = 25;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(993, 17);
            this.toolStripStatusLabel.Spring = true;
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitter
            // 
            this.splitter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitter.Location = new System.Drawing.Point(290, 51);
            this.splitter.Name = "splitter";
            this.splitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitter.Panel1
            // 
            this.splitter.Panel1.Controls.Add(this.viewFiles);
            // 
            // splitter.Panel2
            // 
            this.splitter.Panel2.Controls.Add(this.viewMatches);
            this.splitter.Size = new System.Drawing.Size(712, 458);
            this.splitter.SplitterDistance = 148;
            this.splitter.TabIndex = 6;
            // 
            // viewFiles
            // 
            this.viewFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.viewFiles.Location = new System.Drawing.Point(3, 3);
            this.viewFiles.Name = "viewFiles";
            this.viewFiles.ReadOnly = true;
            this.viewFiles.RowHeadersVisible = false;
            this.viewFiles.Size = new System.Drawing.Size(706, 145);
            this.viewFiles.TabIndex = 0;
            this.viewFiles.SelectionChanged += new System.EventHandler(this.viewFiles_SelectionChanged);
            this.viewFiles.MouseClick += new System.Windows.Forms.MouseEventHandler(this.viewFiles_MouseClick);
            this.viewFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.viewFiles_MouseDoubleClick);
            // 
            // viewMatches
            // 
            this.viewMatches.BackColor = System.Drawing.Color.White;
            this.viewMatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewMatches.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.viewMatches.Location = new System.Drawing.Point(0, 0);
            this.viewMatches.Name = "viewMatches";
            this.viewMatches.ReadOnly = true;
            this.viewMatches.Size = new System.Drawing.Size(712, 306);
            this.viewMatches.TabIndex = 0;
            this.viewMatches.Text = "";
            this.viewMatches.WordWrap = false;
            this.viewMatches.DoubleClick += new System.EventHandler(this.viewKeywordMatches_DoubleClick);
            // 
            // cmFileMatches
            // 
            this.cmFileMatches.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyPathToolStripMenuItem,
            this.copyFileToolStripMenuItem,
            this.copyPathAndFileNameToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.openPathToolStripMenuItem});
            this.cmFileMatches.Name = "cmFileMatches";
            this.cmFileMatches.Size = new System.Drawing.Size(204, 114);
            // 
            // copyPathToolStripMenuItem
            // 
            this.copyPathToolStripMenuItem.Name = "copyPathToolStripMenuItem";
            this.copyPathToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.copyPathToolStripMenuItem.Text = "Copy Path";
            this.copyPathToolStripMenuItem.Click += new System.EventHandler(this.copyPathToolStripMenuItem_Click);
            // 
            // copyFileToolStripMenuItem
            // 
            this.copyFileToolStripMenuItem.Name = "copyFileToolStripMenuItem";
            this.copyFileToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.copyFileToolStripMenuItem.Text = "Copy Filename";
            this.copyFileToolStripMenuItem.Click += new System.EventHandler(this.copyFileToolStripMenuItem_Click);
            // 
            // copyPathAndFileNameToolStripMenuItem
            // 
            this.copyPathAndFileNameToolStripMenuItem.Name = "copyPathAndFileNameToolStripMenuItem";
            this.copyPathAndFileNameToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.copyPathAndFileNameToolStripMenuItem.Text = "Copy Path and Filename";
            this.copyPathAndFileNameToolStripMenuItem.Click += new System.EventHandler(this.copyPathAndFileNameToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // openPathToolStripMenuItem
            // 
            this.openPathToolStripMenuItem.Name = "openPathToolStripMenuItem";
            this.openPathToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.openPathToolStripMenuItem.Text = "Open Path";
            this.openPathToolStripMenuItem.Click += new System.EventHandler(this.openPathToolStripMenuItem_Click);
            // 
            // menuStripMain
            // 
            this.menuStripMain.BackColor = System.Drawing.SystemColors.Control;
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1008, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearHistoryToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // clearHistoryToolStripMenuItem
            // 
            this.clearHistoryToolStripMenuItem.Name = "clearHistoryToolStripMenuItem";
            this.clearHistoryToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.clearHistoryToolStripMenuItem.Text = "Clear History";
            this.clearHistoryToolStripMenuItem.Click += new System.EventHandler(this.clearHistoryToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editorsToolStripMenuItem,
            this.fontToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // editorsToolStripMenuItem
            // 
            this.editorsToolStripMenuItem.Name = "editorsToolStripMenuItem";
            this.editorsToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.editorsToolStripMenuItem.Text = "Editors";
            this.editorsToolStripMenuItem.Click += new System.EventHandler(this.editorsToolStripMenuItem_Click);
            // 
            // fontToolStripMenuItem
            // 
            this.fontToolStripMenuItem.Name = "fontToolStripMenuItem";
            this.fontToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.fontToolStripMenuItem.Text = "Font";
            this.fontToolStripMenuItem.Click += new System.EventHandler(this.fontToolStripMenuItem_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.BackColor = System.Drawing.SystemColors.Control;
            this.lblVersion.Location = new System.Drawing.Point(806, 9);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(196, 15);
            this.lblVersion.TabIndex = 26;
            this.lblVersion.Text = "Version: 0000.0000.0000.0000";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSearchHistory
            // 
            this.lblSearchHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSearchHistory.AutoSize = true;
            this.lblSearchHistory.Location = new System.Drawing.Point(9, 517);
            this.lblSearchHistory.Name = "lblSearchHistory";
            this.lblSearchHistory.Size = new System.Drawing.Size(60, 13);
            this.lblSearchHistory.TabIndex = 23;
            this.lblSearchHistory.Text = "&History (F7)";
            // 
            // cbSearchHistory
            // 
            this.cbSearchHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSearchHistory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbSearchHistory.FormattingEnabled = true;
            this.cbSearchHistory.Location = new System.Drawing.Point(75, 515);
            this.cbSearchHistory.MaxDropDownItems = 32;
            this.cbSearchHistory.Name = "cbSearchHistory";
            this.cbSearchHistory.Size = new System.Drawing.Size(927, 21);
            this.cbSearchHistory.TabIndex = 24;
            this.cbSearchHistory.SelectedIndexChanged += new System.EventHandler(this.cbSearchHistory_SelectedIndexChanged);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(12, 27);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(50, 13);
            this.lblPath.TabIndex = 1;
            this.lblPath.Text = "&Path (F2)";
            // 
            // cbPath
            // 
            this.cbPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.cbPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbPath.FormattingEnabled = true;
            this.cbPath.Location = new System.Drawing.Point(81, 24);
            this.cbPath.Name = "cbPath";
            this.cbPath.Size = new System.Drawing.Size(921, 21);
            this.cbPath.TabIndex = 2;
            // 
            // btnAction
            // 
            this.btnAction.Location = new System.Drawing.Point(205, 134);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(75, 23);
            this.btnAction.TabIndex = 8;
            this.btnAction.Text = "&Search (F5)";
            this.btnAction.UseVisualStyleBackColor = true;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(12, 134);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 7;
            this.btnBrowse.Text = "&Browse (F6)";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(12, 51);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(69, 13);
            this.lblFilter.TabIndex = 3;
            this.lblFilter.Text = "&File Filter (F3)";
            // 
            // cbFilters
            // 
            this.cbFilters.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbFilters.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbFilters.FormattingEnabled = true;
            this.cbFilters.Location = new System.Drawing.Point(12, 67);
            this.cbFilters.Name = "cbFilters";
            this.cbFilters.Size = new System.Drawing.Size(268, 21);
            this.cbFilters.TabIndex = 4;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 91);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(74, 13);
            this.lblSearch.TabIndex = 5;
            this.lblSearch.Text = "&Keywords (F4)";
            // 
            // cbKeywords
            // 
            this.cbKeywords.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbKeywords.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbKeywords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbKeywords.FormattingEnabled = true;
            this.cbKeywords.Location = new System.Drawing.Point(12, 107);
            this.cbKeywords.Name = "cbKeywords";
            this.cbKeywords.Size = new System.Drawing.Size(268, 21);
            this.cbKeywords.TabIndex = 6;
            // 
            // nudMaxFileSize
            // 
            this.nudMaxFileSize.Enabled = false;
            this.nudMaxFileSize.Location = new System.Drawing.Point(144, 391);
            this.nudMaxFileSize.Maximum = new decimal(new int[] {
            0,
            256,
            0,
            0});
            this.nudMaxFileSize.Name = "nudMaxFileSize";
            this.nudMaxFileSize.Size = new System.Drawing.Size(136, 20);
            this.nudMaxFileSize.TabIndex = 22;
            // 
            // nudMinFileSize
            // 
            this.nudMinFileSize.Enabled = false;
            this.nudMinFileSize.Location = new System.Drawing.Point(144, 368);
            this.nudMinFileSize.Maximum = new decimal(new int[] {
            0,
            256,
            0,
            0});
            this.nudMinFileSize.Name = "nudMinFileSize";
            this.nudMinFileSize.Size = new System.Drawing.Size(136, 20);
            this.nudMinFileSize.TabIndex = 20;
            // 
            // cbMaxFileSize
            // 
            this.cbMaxFileSize.AutoSize = true;
            this.cbMaxFileSize.Location = new System.Drawing.Point(12, 392);
            this.cbMaxFileSize.Name = "cbMaxFileSize";
            this.cbMaxFileSize.Size = new System.Drawing.Size(122, 17);
            this.cbMaxFileSize.TabIndex = 21;
            this.cbMaxFileSize.Text = "Max File Size (bytes)";
            this.cbMaxFileSize.UseVisualStyleBackColor = true;
            this.cbMaxFileSize.CheckedChanged += new System.EventHandler(this.cbMaxFileSize_CheckedChanged);
            // 
            // cbShowContext
            // 
            this.cbShowContext.AutoSize = true;
            this.cbShowContext.Location = new System.Drawing.Point(12, 305);
            this.cbShowContext.Name = "cbShowContext";
            this.cbShowContext.Size = new System.Drawing.Size(92, 17);
            this.cbShowContext.TabIndex = 16;
            this.cbShowContext.Text = "Show Context";
            this.cbShowContext.UseVisualStyleBackColor = true;
            this.cbShowContext.CheckedChanged += new System.EventHandler(this.cbShowContext_CheckedChanged);
            // 
            // cbSearchCaseSensitive
            // 
            this.cbSearchCaseSensitive.AutoSize = true;
            this.cbSearchCaseSensitive.Location = new System.Drawing.Point(12, 203);
            this.cbSearchCaseSensitive.Name = "cbSearchCaseSensitive";
            this.cbSearchCaseSensitive.Size = new System.Drawing.Size(129, 17);
            this.cbSearchCaseSensitive.TabIndex = 11;
            this.cbSearchCaseSensitive.Text = "Case Sensitive Match";
            this.cbSearchCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // cbSearchWholeWord
            // 
            this.cbSearchWholeWord.AutoSize = true;
            this.cbSearchWholeWord.Location = new System.Drawing.Point(143, 203);
            this.cbSearchWholeWord.Name = "cbSearchWholeWord";
            this.cbSearchWholeWord.Size = new System.Drawing.Size(119, 17);
            this.cbSearchWholeWord.TabIndex = 12;
            this.cbSearchWholeWord.Text = "Whole Word Match";
            this.cbSearchWholeWord.UseVisualStyleBackColor = true;
            // 
            // cbSearchUseRegex
            // 
            this.cbSearchUseRegex.AutoSize = true;
            this.cbSearchUseRegex.Location = new System.Drawing.Point(12, 226);
            this.cbSearchUseRegex.Name = "cbSearchUseRegex";
            this.cbSearchUseRegex.Size = new System.Drawing.Size(97, 17);
            this.cbSearchUseRegex.TabIndex = 13;
            this.cbSearchUseRegex.Text = "RegExp Match";
            this.cbSearchUseRegex.UseVisualStyleBackColor = true;
            // 
            // cbSearchNegate
            // 
            this.cbSearchNegate.AutoSize = true;
            this.cbSearchNegate.Location = new System.Drawing.Point(12, 249);
            this.cbSearchNegate.Name = "cbSearchNegate";
            this.cbSearchNegate.Size = new System.Drawing.Size(98, 17);
            this.cbSearchNegate.TabIndex = 14;
            this.cbSearchNegate.Text = "Negate Search";
            this.cbSearchNegate.UseVisualStyleBackColor = true;
            // 
            // cbSearchSubFolders
            // 
            this.cbSearchSubFolders.AutoSize = true;
            this.cbSearchSubFolders.Location = new System.Drawing.Point(12, 346);
            this.cbSearchSubFolders.Name = "cbSearchSubFolders";
            this.cbSearchSubFolders.Size = new System.Drawing.Size(113, 17);
            this.cbSearchSubFolders.TabIndex = 18;
            this.cbSearchSubFolders.Text = "Search Subfolders";
            this.cbSearchSubFolders.UseVisualStyleBackColor = true;
            // 
            // cbSearchShowLineNumbers
            // 
            this.cbSearchShowLineNumbers.AutoSize = true;
            this.cbSearchShowLineNumbers.Location = new System.Drawing.Point(12, 282);
            this.cbSearchShowLineNumbers.Name = "cbSearchShowLineNumbers";
            this.cbSearchShowLineNumbers.Size = new System.Drawing.Size(121, 17);
            this.cbSearchShowLineNumbers.TabIndex = 15;
            this.cbSearchShowLineNumbers.Text = "Show Line Numbers";
            this.cbSearchShowLineNumbers.UseVisualStyleBackColor = true;
            // 
            // cbMinFileSize
            // 
            this.cbMinFileSize.AutoSize = true;
            this.cbMinFileSize.Location = new System.Drawing.Point(12, 369);
            this.cbMinFileSize.Name = "cbMinFileSize";
            this.cbMinFileSize.Size = new System.Drawing.Size(100, 17);
            this.cbMinFileSize.TabIndex = 19;
            this.cbMinFileSize.Text = "Min Size (bytes)";
            this.cbMinFileSize.UseVisualStyleBackColor = true;
            this.cbMinFileSize.CheckedChanged += new System.EventHandler(this.cbMinFileSize_CheckedChanged);
            // 
            // nudContextLines
            // 
            this.nudContextLines.Enabled = false;
            this.nudContextLines.Location = new System.Drawing.Point(143, 304);
            this.nudContextLines.Name = "nudContextLines";
            this.nudContextLines.Size = new System.Drawing.Size(137, 20);
            this.nudContextLines.TabIndex = 17;
            // 
            // lblFileContentType
            // 
            this.lblFileContentType.AutoSize = true;
            this.lblFileContentType.Location = new System.Drawing.Point(12, 179);
            this.lblFileContentType.Name = "lblFileContentType";
            this.lblFileContentType.Size = new System.Drawing.Size(63, 13);
            this.lblFileContentType.TabIndex = 9;
            this.lblFileContentType.Text = "File Content";
            // 
            // cbFileContentType
            // 
            this.cbFileContentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFileContentType.FormattingEnabled = true;
            this.cbFileContentType.Location = new System.Drawing.Point(144, 176);
            this.cbFileContentType.Name = "cbFileContentType";
            this.cbFileContentType.Size = new System.Drawing.Size(136, 21);
            this.cbFileContentType.TabIndex = 10;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 561);
            this.Controls.Add(this.nudMaxFileSize);
            this.Controls.Add(this.nudMinFileSize);
            this.Controls.Add(this.cbMaxFileSize);
            this.Controls.Add(this.cbShowContext);
            this.Controls.Add(this.cbSearchCaseSensitive);
            this.Controls.Add(this.cbSearchWholeWord);
            this.Controls.Add(this.cbSearchUseRegex);
            this.Controls.Add(this.cbSearchNegate);
            this.Controls.Add(this.cbSearchSubFolders);
            this.Controls.Add(this.cbSearchShowLineNumbers);
            this.Controls.Add(this.cbMinFileSize);
            this.Controls.Add(this.nudContextLines);
            this.Controls.Add(this.lblFileContentType);
            this.Controls.Add(this.cbFileContentType);
            this.Controls.Add(this.btnAction);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.cbFilters);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.cbKeywords);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.cbPath);
            this.Controls.Add(this.lblSearchHistory);
            this.Controls.Add(this.cbSearchHistory);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.splitter);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(640, 520);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "VCodeHunt";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            this.Move += new System.EventHandler(this.FormMain_Move);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewFiles)).EndInit();
            this.cmFileMatches.ResumeLayout(false);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxFileSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinFileSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudContextLines)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitter;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.RichTextBox viewMatches;
        private System.Windows.Forms.ContextMenuStrip cmFileMatches;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editorsToolStripMenuItem;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.ToolStripMenuItem clearHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyPathAndFileNameToolStripMenuItem;
        private System.Windows.Forms.DataGridView viewFiles;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPathToolStripMenuItem;
        private System.Windows.Forms.Label lblSearchHistory;
        private System.Windows.Forms.ComboBox cbSearchHistory;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.ComboBox cbPath;
        private System.Windows.Forms.Button btnAction;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ComboBox cbFilters;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ComboBox cbKeywords;
        private System.Windows.Forms.NumericUpDown nudMaxFileSize;
        private System.Windows.Forms.NumericUpDown nudMinFileSize;
        private System.Windows.Forms.CheckBox cbMaxFileSize;
        private System.Windows.Forms.CheckBox cbShowContext;
        private System.Windows.Forms.CheckBox cbSearchCaseSensitive;
        private System.Windows.Forms.CheckBox cbSearchWholeWord;
        private System.Windows.Forms.CheckBox cbSearchUseRegex;
        private System.Windows.Forms.CheckBox cbSearchNegate;
        private System.Windows.Forms.CheckBox cbSearchSubFolders;
        private System.Windows.Forms.CheckBox cbSearchShowLineNumbers;
        private System.Windows.Forms.CheckBox cbMinFileSize;
        private System.Windows.Forms.NumericUpDown nudContextLines;
        private System.Windows.Forms.Label lblFileContentType;
        private System.Windows.Forms.ComboBox cbFileContentType;
        private System.Windows.Forms.ToolStripMenuItem fontToolStripMenuItem;
    }
}

