using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Chain_Distributed
{
    public partial class AddServiceForm : Form
    {
        public AddServiceForm()
        {
            InitializeComponent();
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0 && numericUpDown1.Value != 0)
            {
                Person.Name = textBox1.Text;
                Person.cost = numericUpDown1.Value;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
