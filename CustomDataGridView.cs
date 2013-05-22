namespace VCodeHunt
{
    using System.Windows.Forms;

    public class CustomDataGridView : DataGridView
    {
        public CustomDataGridView()
        {
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = false;
            this.AllowUserToResizeRows = false;
            this.EditMode = DataGridViewEditMode.EditProgrammatically;

            this.AllowUserToResizeColumns = true;
        }
    }
}
