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
    public partial class AddBookingForm : Form
    {
        public AddBookingForm()
        {
            InitializeComponent();
        }

        private void AddBookingForm_Load(object sender, EventArgs e)
        {

            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT ID, FIO FROM Client", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //user.SetId(Convert.ToInt32(reader["ID"]));
                    comboBox1.Items.Add(Tuple.Create(reader["FIO"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }

            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT r.ID, r.Room_number FROM Room r JOIN Hotel h ON h.ID=r.Hotel_ID WHERE h.ID = {Hotel.id} AND r.isAvailable = 1", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //user.SetId(Convert.ToInt32(reader["ID"]));
                    comboBox2.Items.Add(Tuple.Create(reader["Room_number"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }

        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1)
            {
                // Получение выбранных значений
                //Person.Id = Convert.ToInt32(comboBox1.SelectedValue);
                //Room.id = Convert.ToInt32(comboBox2.SelectedValue);
                Person.dateTime1 = dateTimePicker1.Value.Date;
                Person.dateTime2 = dateTimePicker2.Value.Date;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tuple<string, int> selectedTuple = (Tuple<string, int>)comboBox1.SelectedItem;
            Person.Id = selectedTuple.Item2;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tuple<string, int> selectedTuple = (Tuple<string, int>)comboBox2.SelectedItem;
            Room.id = selectedTuple.Item2;
        }
    }
}
