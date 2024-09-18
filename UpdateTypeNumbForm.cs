using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hotel_Chain_Distributed
{
    public partial class UpdateTypeNumbForm : Form
    {
        public UpdateTypeNumbForm()
        {
            InitializeComponent();
        }

        private void UpdateTypeNumbForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Type FROM Type_numb", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(Tuple.Create(reader["Type"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tuple<string, int> selectedTuple = (Tuple<string, int>)comboBox1.SelectedItem;
            //Hotel.Name = selectedTuple.Item1;
            Room.Type_ID = selectedTuple.Item2;
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Type, Cost_per_day FROM Type_numb WHERE ID = @id", sqlConnection);
                cmd.Parameters.AddWithValue("@id", Room.Type_ID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader["Type"].ToString();
                    numericUpDown1.Value = Convert.ToDecimal(reader["Cost_per_day"]);
                }
                reader.Close();
            }
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
