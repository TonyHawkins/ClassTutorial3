using Gallery3WinForm1.ServiceReference1;
using System;
using System.Windows.Forms;

namespace Gallery3WinForm1
{
    public partial class frmWork : Form
    {
        protected clsWork _Work;

        public frmWork()
        {
            InitializeComponent();
        }

        public void SetDetails(clsWork prWork)
        {
            _Work = prWork;
            updateForm();
            ShowDialog();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                pushData();
                if (txtName.Enabled)
                Program.SvcClient.InsertWork(_Work);
            else
                Program.SvcClient.UpdateWork(_Work);
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected virtual bool isValid()
        {
            return true;
        }

        protected virtual void updateForm()
        {
            txtName.Text = _Work.Name;
            txtCreation.Text = _Work.Date.ToShortDateString();
            txtValue.Text = _Work.Value.ToString();
            txtName.Enabled = string.IsNullOrEmpty(_Work.Name);
        }

        protected virtual void pushData()
        {
            _Work.Name = txtName.Text;
            _Work.Date = DateTime.Parse(txtCreation.Text);
            _Work.Value = decimal.Parse(txtValue.Text);
        }

    }
}