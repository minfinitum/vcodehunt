namespace VCodeHunt
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;
    using VCodeHunt.Config;

    public partial class FormViewers : Form
    {
        BindingList<Viewer> m_viewerBinding = null;
        AppConfig m_config = null;

        public FormViewers(AppConfig config)
        {
            InitializeComponent();

            viewViewers.AllowUserToAddRows = false;
            viewViewers.AllowUserToDeleteRows = false;
            viewViewers.AllowUserToOrderColumns = false;
            viewViewers.AllowUserToResizeRows = false;
            viewViewers.EditMode = DataGridViewEditMode.EditProgrammatically;
            viewViewers.AllowUserToResizeColumns = true;
            viewViewers.MultiSelect = false;

            m_config = config;
            if (m_config.Viewers.Count <= 0)
            {
                m_viewerBinding = new BindingList<Viewer>();
            }
            else
            {
                m_viewerBinding = new BindingList<Viewer>(m_config.Viewers.ToList());
            }

            viewViewers.AutoGenerateColumns = false;
            Type viewertype = typeof(Viewer);
            foreach (PropertyInfo pinfo in viewertype.GetProperties())
            {
                if (pinfo.PropertyType.IsPublic)
                {
                    DataGridViewTextBoxColumn colTextBox = new DataGridViewTextBoxColumn();
                    colTextBox.CellTemplate = new DataGridViewTextBoxCell();
                    colTextBox.Name = pinfo.Name;
                    colTextBox.HeaderText = pinfo.Name;
                    colTextBox.DataPropertyName = pinfo.Name;

                    if (colTextBox.Name == "AppPath")
                    {
                        colTextBox.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    else
                    {
                        colTextBox.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    }

                    viewViewers.Columns.Add(colTextBox);
                }
            }

            viewViewers.DataSource = m_viewerBinding;

            SetPositionControls();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormViewer fv = new FormViewer();
            if (fv.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                m_viewerBinding.Add(fv.Viewer);
            }

            SetPositionControls();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int count = viewViewers.SelectedRows.Count;
            if (1 != count)
                return;

            DataGridViewRow row = viewViewers.SelectedRows[0];
            FormViewer fv = new FormViewer((Viewer) row.DataBoundItem);
            if (fv.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                int idx = m_viewerBinding.IndexOf((Viewer)row.DataBoundItem);
                if(idx != -1)
                {
                    m_viewerBinding.RemoveAt(idx);
                    m_viewerBinding.Insert(idx, fv.Viewer);
                }
            }

            SetPositionControls();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int count = viewViewers.SelectedRows.Count;
            if (1 != count)
                return;

            DataGridViewRow row = viewViewers.SelectedRows[0];
            int idx = m_viewerBinding.IndexOf((Viewer)row.DataBoundItem);
            if (idx != -1)
            {
                m_viewerBinding.RemoveAt(idx);
            }

            SetPositionControls();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_config.Viewers.Clear();
            m_config.Viewers.AddRange(m_viewerBinding.ToList());
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void FormViewers_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnUp_Click(object sender, EventArgs e)
        {
            int count = m_viewerBinding.Count;
            if (1 >= count)
                return;

            DataGridViewRow row = viewViewers.SelectedRows[0];
            int idx = m_viewerBinding.IndexOf((Viewer)row.DataBoundItem);
            if (idx == 0)
                return;

            Viewer viewer = (Viewer)row.DataBoundItem;
            m_viewerBinding.RemoveAt(idx);
            m_viewerBinding.Insert(idx - 1, viewer);
            viewViewers.Rows[idx - 1].Selected = true;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int count = m_viewerBinding.Count;
            if (1 >= count)
                return;

            DataGridViewRow row = viewViewers.SelectedRows[0];
            int idx = m_viewerBinding.IndexOf((Viewer)row.DataBoundItem);
            if (idx == m_viewerBinding.Count() - 1)
                return;

            Viewer viewer = (Viewer)row.DataBoundItem;
            m_viewerBinding.RemoveAt(idx);
            m_viewerBinding.Insert(idx + 1, viewer);
            viewViewers.Rows[idx + 1].Selected = true;
        }

        void SetPositionControls()
        {
            btnUp.Enabled = (m_viewerBinding.Count > 1);
            btnDown.Enabled = (m_viewerBinding.Count > 1);
        }
    }
}
