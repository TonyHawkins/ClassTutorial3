using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Gallery3WinForm1.ServiceReference1;
using System.Linq;

namespace Gallery3WinForm1
{
    public partial class frmArtist : Form
    {
        private clsArtist _Artist;
     //   private clsWorksList _WorksList;

        private static Dictionary<string, frmArtist> _ArtistFormList =
            new Dictionary<string, frmArtist>();

        private frmArtist()
        {
            InitializeComponent();
        }

        public static void Run(string prArtistName)
        {
            frmArtist lcArtistForm;
            if (!_ArtistFormList.TryGetValue(prArtistName, out lcArtistForm))
            {
                lcArtistForm = new frmArtist();
                if (string.IsNullOrEmpty(prArtistName))
                    lcArtistForm.SetDetails(new clsArtist());
                else
                {
                    _ArtistFormList.Add(prArtistName, lcArtistForm);
                    lcArtistForm.refreshFormFromDB(prArtistName);
                }
            }
            else
            {
                lcArtistForm.Show();
                lcArtistForm.Activate();
            }
        }


        private void refreshFormFromDB(string prArtistName)
        {
            SetDetails(Program.SvcClient.GetArtist(prArtistName));
        }

        private void updateTitle(string prGalleryName)
        {
            if (!string.IsNullOrEmpty(prGalleryName))
                Text = "Artist Details - " + prGalleryName;
        }

        private void UpdateDisplay()
        {
            lstWorks.DataSource = null;
            if (_Artist.Works != null)
                lstWorks.DataSource = _Artist.Works.ToList();
     /*       if (_WorksList.SortOrder == 0)
            {
                _WorksList.SortByName();
                rbByName.Checked = true;
            }
            else
            {
                _WorksList.SortByDate();
                rbByDate.Checked = true;
            }

            lstWorks.DataSource = null;
            lstWorks.DataSource = _WorksList;
            lblTotal.Text = Convert.ToString(_WorksList.GetTotalValue()); */
        }

        public void UpdateForm()
        {
            txtName.Text = _Artist.Name;
            txtSpeciality.Text = _Artist.Speciality;
            txtPhone.Text = _Artist.Phone;
         //   _WorksList = _Artist.WorksList;

            //frmMain.Instance.GalleryNameChanged += new frmMain.Notify(updateTitle);
            //updateTitle(_Artist.ArtistList.GalleryName);
        }

        public void SetDetails(clsArtist prArtist)
        {
            _Artist = prArtist;
            txtName.Enabled = string.IsNullOrEmpty(_Artist.Name);
            UpdateForm();
            UpdateDisplay();
            frmMain.Instance.GalleryNameChanged += new frmMain.Notify(updateTitle);
       //     updateTitle(_Artist.ArtistList.GalleryName);
            Show();
        }

        private void pushData()
        {
            _Artist.Name = txtName.Text;
            _Artist.Speciality = txtSpeciality.Text;
            _Artist.Phone = txtPhone.Text;
            //_WorksList.SortOrder = _SortOrder; // no longer required, updated with each rbByDate_CheckedChanged
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string lcReply = new InputBox(clsWork.FACTORY_PROMPT).Answer;
            if (!string.IsNullOrEmpty(lcReply)) // not cancelled?
            {
                clsWork lcWork = clsWork.NewWork(lcReply[0]);
                if (lcWork != null)             //valid artwork created?
                {
                    if (txtName.Enabled)        //new artist not saved?
                    {
                        pushData();
                        Program.SvcClient.InsertArtist(_Artist);
                        txtName.Enabled = false;
                    }
                    lcWork.ArtistName = _Artist.Name;
                    lcWork.EditDetails();
                    if (!string.IsNullOrEmpty(lcWork.Name))     // not cancelled?
                    {
                        refreshFormFromDB(_Artist.Name);
                        frmMain.Instance.UpdateDisplay();
                    }
                }
            }
        }

        private void lstWorks_DoubleClick(object sender, EventArgs e)
        {

            try
            {
                (lstWorks.SelectedValue as clsWork).EditDetails();
                UpdateDisplay();
                frmMain.Instance.UpdateDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int lcIndex = lstWorks.SelectedIndex;

            if (lcIndex >= 0 && MessageBox.Show("Are you sure?", "Deleting work", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
             //   _WorksList.RemoveAt(lcIndex);
                UpdateDisplay();
                frmMain.Instance.UpdateDisplay();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            pushData();
            if (txtName.Enabled)
            {
                Program.SvcClient.InsertArtist(_Artist);
                frmMain.Instance.UpdateDisplay();
                txtName.Enabled = false;
            }
            else
                Program.SvcClient.UpdateArtist(_Artist);
            Hide();
        }

        private Boolean isValid()
        {
       /*     if (txtName.Enabled && txtName.Text != "")
                if (_Artist.IsDuplicate(txtName.Text))
                {
                    MessageBox.Show("Artist with that name already exists!", "Error adding artist");
                    return false;
                }
                else
                    return true;
            else */
                return true;
        }

        private void rbByDate_CheckedChanged(object sender, EventArgs e)
        {
       //     _WorksList.SortOrder = Convert.ToByte(rbByDate.Checked);
            UpdateDisplay();
        }

    }
}