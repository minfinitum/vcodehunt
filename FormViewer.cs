namespace VCodeHunt
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    public partial class FormViewer : Form
    {
        ToolTip _tt = new ToolTip();
        public FormViewer()
        {
            InitializeComponent();

            Viewer = new Config.Viewer();
            InitForm();
        }

        public FormViewer(Config.Viewer viewer)
        {
            InitializeComponent();

            Viewer = viewer;
            InitForm();
        }

        private void InitForm()
        {
            if (!string.IsNullOrEmpty(Viewer.AppPath))
            {
                tbAppPath.Text = Viewer.AppPath.Trim();
            }

            if (!string.IsNullOrEmpty(Viewer.AppArgs))
            {
                tbAppArgs.Text = Viewer.AppArgs.Trim();
            }

            if (!string.IsNullOrEmpty(Viewer.Extensions))
            {
                tbExtensions.Text = Viewer.Extensions.Trim().TrimStart(new[] { '.' });
            }

            SetToolTips();

            UpdatePreview();
        }

        public void SetToolTips()
        {
            // tool tips
            string argtips = "Examples:\n notepad++: \"%1\" -n%2\ndevenv: /Edit \"%1\" /Command \"Edit.GoTo %2\"";
            _tt.SetToolTip(this.lbArguments, argtips);
            _tt.Active = true;
        }

        public Config.Viewer Viewer { get; set; }

        private void UpdatePreview()
        {
            string testfile = "c:\\windows\\win.ini";
            lbArgumentFile.Text = string.Format("%1 - File:[{0}]", testfile);

            int testlinenumber = 10;
            lbArgumentLine.Text = string.Format("%2 - Line Number:[{0}]", testlinenumber);

            string arguments = tbAppArgs.Text.Trim();
            if (!string.IsNullOrEmpty(arguments))
            {
                arguments = tbAppArgs.Text.Trim();
                arguments = arguments.Replace("%1", testfile);
                arguments = arguments.Replace("%2", "10");

                tbPreview.Text = string.Format("{0} {1}", tbAppPath.Text, arguments);
            }
            else
            {
                tbPreview.Text = string.Format("{0}", tbAppPath.Text);
            }
        }

        private void SetViewer()
        {
            Viewer.AppPath = tbAppPath.Text.Trim();
            Viewer.AppArgs = tbAppArgs.Text;

            Viewer.Extensions = tbExtensions.Text.Trim();
            if (string.IsNullOrEmpty(Viewer.Extensions))
            {
                Viewer.Extensions = "*";
            }
        }

        private void tbApplicationPath_TextChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void tbExtension_TextChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void tbArguments_TextChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SetViewer();

            if (!File.Exists(Viewer.AppPath))
            {
                MessageBox.Show(this, string.Format("Application does not exist:\n[{0}]\n", Viewer.AppPath), "ERROR - Application Path", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void BrowseSearchPath()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.DefaultExt = "exe";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbAppPath.Text = ofd.FileName;

                // autodetect applications
                if (tbAppPath.Text.EndsWith("notepad++.exe"))
                {
                    tbAppArgs.Text = "\"%1\" -n%2";
                }
                else if (tbAppPath.Text.EndsWith("ultraedit.exe"))
                {
                    tbAppArgs.Text = "\"%1\"/%2";
                }
                else if (tbAppPath.Text.EndsWith("sublime_text.exe"))
                {
                    tbAppArgs.Text = "\"%1\":%2";
                }

            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            BrowseSearchPath();
        }
    }
}
