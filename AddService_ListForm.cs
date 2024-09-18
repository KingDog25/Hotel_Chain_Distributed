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
    public partial class AddService_ListForm : Form
    {
        public AddService_ListForm()
        {
            InitializeComponent();
        }

        private void AddService_ListForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT b.ID, Checkin_date FROM Booking b JOIN Room r ON r.ID=b.Room_ID JOIN Hotel h ON h.ID=r.Hotel_ID WHERE h.ID = {Hotel.id}", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //user.SetId(Convert.ToInt32(reader["ID"]));
                    comboBox1.Items.Add(Tuple.Create(reader["Checkin_date"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Name FROM Service", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //user.SetId(Convert.ToInt32(reader["ID"]));
                    comboBox2.Items.Add(Tuple.Create(reader["Name"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT e.ID, e.FIO, e.Specialization FROM Employee e JOIN Service_List s ON s.Employee_ID=e.ID JOIN Booking b ON b.ID=s.Booking_ID JOIN Room r ON b.Room_ID=r.ID JOIN Hotel h ON h.ID=r.Hotel_ID WHERE h.ID = {Hotel.id}", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //user.SetId(Convert.ToInt32(reader["ID"]));
                    comboBox3.Items.Add(Tuple.Create(reader["FIO"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }

        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length != 0 && comboBox2.Text.Length != 0
                && comboBox3.Text.Length != 0 && numericUpDown1.Value !=0)
            {
                Person.count = Convert.ToInt32(numericUpDown1.Value);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                var tuple = (Tuple<string, int>)comboBox1.SelectedItem; // заменить индекс_элемента на нужный индекс
                Booking.id = tuple.Item2;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tuple = (Tuple<string, int>)comboBox2.SelectedItem;
            Booking.services_id = tuple.Item2;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tuple = (Tuple<string, int>)comboBox3.SelectedItem;
            Person.Id = tuple.Item2;
        }
    }
}
