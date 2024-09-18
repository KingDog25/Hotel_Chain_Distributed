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

namespace Hotel_Chain_Distributed
{
    public partial class UpdateClientForm : Form
    {
        public UpdateClientForm()
        {
            InitializeComponent();
        }

        private void UploadClientForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, FIO, Email, Phone, Login, Pass FROM Client", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(Tuple.Create(reader["FIO"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tuple<string, int> selectedTuple = (Tuple<string, int>)comboBox1.SelectedItem;
            //Hotel.Name = selectedTuple.Item1;
            Person.Id = selectedTuple.Item2;
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT FIO, Email, Phone, Login, Pass FROM Client WHERE ID = @id", sqlConnection);
                cmd.Parameters.AddWithValue("@id", Person.Id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader["FIO"].ToString();
                    textBox2.Text = reader["Email"].ToString();
                    textBox3.Text = reader["Phone"].ToString();
                    textBox4.Text = reader["Login"].ToString();
                    textBox5.Text = reader["Pass"].ToString();
                }
                reader.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Person.Name = textBox1.Text;
            Person.Pt = textBox2.Text;
            Person.Tl = textBox3.Text;
            Person.Sr = textBox4.Text;
            Person.Po_U = textBox5.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
