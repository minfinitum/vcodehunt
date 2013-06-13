namespace VCodeHunt
{
    partial class FormFont
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbFont = new System.Windows.Forms.ComboBox();
            this.tbTest = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(163, 98);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(244, 98);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cbFont
            // 
            this.cbFont.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbFont.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFont.FormattingEnabled = true;
            this.cbFont.Location = new System.Drawing.Point(13, 13);
            this.cbFont.Name = "cbFont";
            this.cbFont.Size = new System.Drawing.Size(306, 21);
            this.cbFont.TabIndex = 1;
            this.cbFont.SelectedIndexChanged += new System.EventHandler(this.cbFont_SelectedIndexChanged);
            // 
            // tbTest
            // 
            this.tbTest.Location = new System.Drawing.Point(13, 41);
            this.tbTest.Multiline = true;
            this.tbTest.Name = "tbTest";
            this.tbTest.ReadOnly = true;
            this.tbTest.Size = new System.Drawing.Size(306, 51);
            this.tbTest.TabIndex = 2;
            this.tbTest.Text = "The quick brown fox jumps over the lazy dog.";
            // 
            // FormFont
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 133);
            this.Controls.Add(this.tbTest);
            this.Controls.Add(this.cbFont);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFont";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Font";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormFont_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbFont;
        private System.Windows.Forms.TextBox tbTest;
    }
}