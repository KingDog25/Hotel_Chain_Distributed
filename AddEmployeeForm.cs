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
    public partial class AddEmployeeForm : Form
    {
        public AddEmployeeForm()
        {
            InitializeComponent();
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0 && textBox2.Text.Length != 0)
            {
                // Считывание значений из полей editbox
                Person.Name = textBox1.Text;
                Person.Sr = textBox2.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Одно из полей пустое. Заполните все поля");
        }
    }
}
