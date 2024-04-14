using MyContacts.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyContacts
{
    public partial class FrmAddOrEdit : Form
    {
        IContactsRepository repository;
        public int contactID = 0;
        public FrmAddOrEdit()
        {
            InitializeComponent();
            repository = new ContactsRepository();
        }

        private void FrmAddContact_Load(object sender, EventArgs e)
        {
            if (contactID == 0)
            {
                this.Text = "افزودن شحص جدید";
            }
            else
            {
                this.Text = "ویرایش شخص";
                DataTable dt = repository.SelectRow(contactID);
                txtFirstName.Text = dt.Rows[0][1].ToString();
                txtLastName.Text = dt.Rows[0][2].ToString();
                txtPhoneNumber.Text = dt.Rows[0][3].ToString();
                txtEmail.Text = dt.Rows[0][4].ToString();
                txtAge.Text = dt.Rows[0][5].ToString();
                txtAddress.Text = dt.Rows[0][6].ToString();
                btnSubmit.Text = "ویرایش";

            }
        }

        bool IsValidInputs()
        {
            if (txtFirstName.Text == "")
            {
                MessageBox.Show("! لطفا نام را وارد کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtPhoneNumber.Text == "")
            {
                MessageBox.Show("! لطفا شماره تلفن را وارد کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValidInputs())
            {
                bool isSuccessfull;
                if (contactID == 0)
                {
                    isSuccessfull = repository.Insert(txtFirstName.Text, txtLastName.Text, txtPhoneNumber.Text, txtEmail.Text, (int)txtAge.Value, txtAddress.Text);
                }
                else
                {
                    isSuccessfull = repository.Update(contactID, txtFirstName.Text, txtLastName.Text, txtPhoneNumber.Text, txtEmail.Text, (int)txtAge.Value, txtAddress.Text);
                }
                
                if (isSuccessfull) 
                {
                    MessageBox.Show("! عمیلات با موفقیت انجام شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else {
                    MessageBox.Show("! عمیلات با خطا مواجه شد", "خطا", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }
    }
}
