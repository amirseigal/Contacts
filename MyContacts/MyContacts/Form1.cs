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
    public partial class Form1 : Form
    {
        IContactsRepository repository;
        public Form1()
        {
            InitializeComponent();
            repository = new ContactsRepository();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            BindGrid();

        }

        private void BindGrid()
        {
            dgContacts.AutoGenerateColumns = false;
            dgContacts.DataSource = repository.SelectAll();
        }

        private void btnRefresh_click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnNewContact_Click(object sender, EventArgs e)
        {
            FrmAddOrEdit frm = new FrmAddOrEdit();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                BindGrid();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgContacts.CurrentRow != null)
            {
                string firstName = dgContacts.CurrentRow.Cells[1].Value.ToString();
                string lastName = dgContacts.CurrentRow.Cells[2].Value.ToString();
                string fullName = firstName + " " + lastName;
                if (MessageBox.Show($"آیا از حذف {fullName} مطمئن هستید ؟","توجه",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
                {
                    int ContactID = (int)dgContacts.CurrentRow.Cells[0].Value;
                    repository.Delete(ContactID);
                    BindGrid();
                }
            }
            else
            {
                MessageBox.Show("! لطفا یک شخص را از لیست انتخاب کنید");
            }
        }

        private void brnUpdate_Click(object sender, EventArgs e)
        {
            string firstName = dgContacts.CurrentRow.Cells[1].Value.ToString();
            string lastName = dgContacts.CurrentRow.Cells[2].Value.ToString();
            string fullName = firstName + " " + lastName;
            if (dgContacts.CurrentRow != null)
            {
                int ContactID = (int)dgContacts.CurrentRow.Cells[0].Value;
                FrmAddOrEdit frm = new FrmAddOrEdit();
                frm.contactID = ContactID;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    BindGrid();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgContacts.DataSource = repository.Search(txtSearch.Text);
        }
    }
}
