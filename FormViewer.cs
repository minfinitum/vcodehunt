namespace VCodeHunt
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    public partial class FormViewer : Form
    {
        class App
        {
            public App(string name, string args)
            {
                Name = name;
                Args = args;
            }
            public string Name { get; set; }
            public string Args { get; set; }
        };

        App[] apps = new App[]
            {
                new App("notepad++.exe", "\"%1\" -n%2"),
                new App("ultraedit.exe", "\"%1\"/%2"),
                new App("sublime_text.exe", "\"%1\":%2"),
                new App("devenv.exe", "/Edit \"%1\" /Command \"Edit.GoTo %2\"")
            };

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
            string argtips = "Examples:" + Environment.NewLine;
            foreach (App app in apps)
            {
                argtips += string.Format("{0}: {1}", app.Name, app.Args) + Environment.NewLine;
            }
            _tt.SetToolTip(this.lbAppArgs, argtips);
            _tt.SetToolTip(this.tbAppArgs, argtips);

            string extentiontips = "Multiple extensions use ';' or ',' (* for all files).";
            _tt.SetToolTip(this.lbExtensions, extentiontips);
            _tt.SetToolTip(this.tbExtensions, extentiontips);

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
                foreach (App app in apps)
                {
                    if (tbAppPath.Text.EndsWith(app.Name))
                    { 
                        tbAppArgs.Text = app.Args;
                    }
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            BrowseSearchPath();
        }

        private void FormViewer_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch ((Keys)e.KeyChar)
            {
                case Keys.Escape:
                    DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    e.Handled = true;
                    this.Close();
                    break;
            }
        }
    }
}
