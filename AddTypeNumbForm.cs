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
    public partial class AddTypeNumbForm : Form
    {
        public AddTypeNumbForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Room.type = textBox1.Text;
            Room.cost = numericUpDown1.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
