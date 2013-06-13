using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VCodeHunt
{
    public partial class FormFont : Form
    {
        const float fontsize = 8.5f;

        public FormFont(Font selected)
        {
            Selected = selected;

            InitializeComponent();

            int index = 0;
            int selectedIndex = 0;
            foreach (FontFamily ff in FontFamily.Families)
            {
                if (ff.IsStyleAvailable(FontStyle.Regular))
                {
                    Font font = new Font(ff, fontsize);
                    cbFont.Items.Add(font.Name);

                    if (font.Name == Selected.Name)
                        selectedIndex = index;
                }

                index++;
            }

            cbFont.SelectedIndex = selectedIndex;
            tbTest.Font = Selected;
        }

        public Font Selected { get; set; }

        private void cbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFont.SelectedIndex == -1) 
            {
                return;
            }

            string fontfamily = cbFont.Items[cbFont.SelectedIndex].ToString();
            Selected = new Font(fontfamily, fontsize);
            tbTest.Font = Font;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void FormFont_KeyPress(object sender, KeyPressEventArgs e)
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
