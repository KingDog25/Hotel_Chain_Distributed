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
    public partial class UpdateHotelForm : Form
    {
        public UpdateHotelForm()
        {
            InitializeComponent();
        }

        private void UpdateHotelForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Name FROM Hotel", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToInt32(reader["ID"]) != 1)
                    {
                        comboBox1.Items.Add(Tuple.Create(reader["Name"].ToString(), Convert.ToInt32(reader["ID"])));
                    }
                }
                reader.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            Tuple<string, int> selectedTuple = (Tuple<string, int>)comboBox1.SelectedItem;
            //Hotel.Name = selectedTuple.Item1;
            Person.Id = selectedTuple.Item2;
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main_Hotel"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Name, Address, Phone FROM Hotel WHERE ID = @id", sqlConnection);
                cmd.Parameters.AddWithValue("@id", Person.Id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader["Name"].ToString();
                    textBox2.Text = reader["Address"].ToString();
                    textBox3.Text = reader["Phone"].ToString();
                }
                reader.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hotel.Name = textBox1.Text;
            Hotel.Address = textBox2.Text;
            Hotel.Phone = textBox3.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
