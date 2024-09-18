using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hotel_Chain_Distributed
{
    public partial class AddClientForm : Form
    {
        public AddClientForm()
        {
            InitializeComponent();
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            if (textBoxFIO.Text.Length != 0 && textBoxEmail.Text.Length != 0 && textBoxPhone.Text.Length != 0
                && textBoxLogin.Text.Length != 0 && textBoxPass.Text.Length != 0)
            {
                // Считывание значений из полей editbox
                Person.Name = textBoxFIO.Text;
                Person.Pt = textBoxEmail.Text;
                Person.Tl = textBoxPhone.Text;
                Person.Sur = textBoxLogin.Text;
                Person.Sr = textBoxPass.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Одно из полей пустое. Заполните все поля");
        }
    }
}
