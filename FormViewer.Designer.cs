namespace VCodeHunt
{
    partial class FormViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormViewer));
            this.tbAppPath = new System.Windows.Forms.TextBox();
            this.tbAppArgs = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbApplication = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lbAppArgs = new System.Windows.Forms.Label();
            this.lbArgumentFile = new System.Windows.Forms.Label();
            this.lbArgumentLine = new System.Windows.Forms.Label();
            this.tbPreview = new System.Windows.Forms.TextBox();
            this.lbPreview = new System.Windows.Forms.Label();
            this.lbExtensions = new System.Windows.Forms.Label();
            this.tbExtensions = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbAppPath
            // 
            this.tbAppPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbAppPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tbAppPath.Location = new System.Drawing.Point(12, 28);
            this.tbAppPath.MaxLength = 260;
            this.tbAppPath.Name = "tbAppPath";
            this.tbAppPath.Size = new System.Drawing.Size(329, 20);
            this.tbAppPath.TabIndex = 2;
            this.tbAppPath.Text = "c:\\windows\\system32\\notepad.exe";
            this.tbAppPath.TextChanged += new System.EventHandler(this.tbApplicationPath_TextChanged);
            // 
            // tbAppArgs
            // 
            this.tbAppArgs.Location = new System.Drawing.Point(12, 67);
            this.tbAppArgs.MaxLength = 260;
            this.tbAppArgs.Name = "tbAppArgs";
            this.tbAppArgs.Size = new System.Drawing.Size(410, 20);
            this.tbAppArgs.TabIndex = 7;
            this.tbAppArgs.Text = "\"%1\"";
            this.tbAppArgs.TextChanged += new System.EventHandler(this.tbArguments_TextChanged);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(266, 213);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(347, 213);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lbApplication
            // 
            this.lbApplication.AutoSize = true;
            this.lbApplication.Location = new System.Drawing.Point(9, 9);
            this.lbApplication.Name = "lbApplication";
            this.lbApplication.Size = new System.Drawing.Size(59, 13);
            this.lbApplication.TabIndex = 1;
            this.lbApplication.Text = "Application";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(347, 26);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lbAppArgs
            // 
            this.lbAppArgs.AutoSize = true;
            this.lbAppArgs.Location = new System.Drawing.Point(9, 51);
            this.lbAppArgs.Name = "lbAppArgs";
            this.lbAppArgs.Size = new System.Drawing.Size(57, 13);
            this.lbAppArgs.TabIndex = 6;
            this.lbAppArgs.Text = "Arguments";
            // 
            // lbArgumentFile
            // 
            this.lbArgumentFile.AutoSize = true;
            this.lbArgumentFile.Location = new System.Drawing.Point(12, 90);
            this.lbArgumentFile.Name = "lbArgumentFile";
            this.lbArgumentFile.Size = new System.Drawing.Size(75, 13);
            this.lbArgumentFile.TabIndex = 8;
            this.lbArgumentFile.Text = "%1 - File name";
            // 
            // lbArgumentLine
            // 
            this.lbArgumentLine.AutoSize = true;
            this.lbArgumentLine.Location = new System.Drawing.Point(12, 104);
            this.lbArgumentLine.Name = "lbArgumentLine";
            this.lbArgumentLine.Size = new System.Drawing.Size(88, 13);
            this.lbArgumentLine.TabIndex = 9;
            this.lbArgumentLine.Text = "%2 - Line number";
            // 
            // tbPreview
            // 
            this.tbPreview.Location = new System.Drawing.Point(15, 187);
            this.tbPreview.Name = "tbPreview";
            this.tbPreview.ReadOnly = true;
            this.tbPreview.Size = new System.Drawing.Size(407, 20);
            this.tbPreview.TabIndex = 11;
            // 
            // lbPreview
            // 
            this.lbPreview.AutoSize = true;
            this.lbPreview.Location = new System.Drawing.Point(12, 171);
            this.lbPreview.Name = "lbPreview";
            this.lbPreview.Size = new System.Drawing.Size(45, 13);
            this.lbPreview.TabIndex = 10;
            this.lbPreview.Text = "Preview";
            // 
            // lbExtensions
            // 
            this.lbExtensions.AutoSize = true;
            this.lbExtensions.Location = new System.Drawing.Point(12, 126);
            this.lbExtensions.Name = "lbExtensions";
            this.lbExtensions.Size = new System.Drawing.Size(110, 13);
            this.lbExtensions.TabIndex = 12;
            this.lbExtensions.Text = "Supported Extensions";
            // 
            // tbExtensions
            // 
            this.tbExtensions.Location = new System.Drawing.Point(15, 142);
            this.tbExtensions.MaxLength = 260;
            this.tbExtensions.Name = "tbExtensions";
            this.tbExtensions.Size = new System.Drawing.Size(410, 20);
            this.tbExtensions.TabIndex = 13;
            this.tbExtensions.Text = "*";
            // 
            // FormViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 248);
            this.Controls.Add(this.lbExtensions);
            this.Controls.Add(this.tbExtensions);
            this.Controls.Add(this.lbPreview);
            this.Controls.Add(this.tbPreview);
            this.Controls.Add(this.lbArgumentLine);
            this.Controls.Add(this.lbArgumentFile);
            this.Controls.Add(this.lbAppArgs);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lbApplication);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbAppArgs);
            this.Controls.Add(this.tbAppPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Viewer";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormViewer_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbAppPath;
        private System.Windows.Forms.TextBox tbAppArgs;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbApplication;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lbAppArgs;
        private System.Windows.Forms.Label lbArgumentFile;
        private System.Windows.Forms.Label lbArgumentLine;
        private System.Windows.Forms.TextBox tbPreview;
        private System.Windows.Forms.Label lbPreview;
        private System.Windows.Forms.Label lbExtensions;
        private System.Windows.Forms.TextBox tbExtensions;
    }
}