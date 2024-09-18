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
    public partial class UpdateEmployeeForm : Form
    {
        public UpdateEmployeeForm()
        {
            InitializeComponent();
        }

        private void UpdateEmployeeForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT e.ID, e.FIO, e.Specialization FROM Employee e JOIN Service_List s ON s.Employee_ID=e.ID JOIN Booking b ON b.ID=s.Booking_ID JOIN Room r ON b.Room_ID=r.ID JOIN Hotel h ON h.ID=r.Hotel_ID WHERE h.ID = {Hotel.id}", sqlConnection);
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
                SqlCommand cmd = new SqlCommand("SELECT FIO, Specialization FROM Employee WHERE ID = @id", sqlConnection);
                cmd.Parameters.AddWithValue("@id", Person.Id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader["FIO"].ToString();
                    textBox2.Text = reader["Specialization"].ToString();
                }
                reader.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Person.Name = textBox1.Text;
            Person.Sur = textBox2.Text;        
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
