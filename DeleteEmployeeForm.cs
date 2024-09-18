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
    public partial class DeleteEmployeeForm : Form
    {
        public DeleteEmployeeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tuple<string, int> selectedTuple = (Tuple<string, int>)comboBox1.SelectedItem;
            Person.Id = selectedTuple.Item2;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DeleteEmployeeForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT e.ID, e.FIO FROM Employee e JOIN Service_List s ON s.Employee_ID=e.ID JOIN Booking b ON b.ID=s.Booking_ID JOIN Room r ON b.Room_ID=r.ID JOIN Hotel h ON h.ID=r.Hotel_ID WHERE h.ID = {Hotel.id} OR h.ID IS NULL", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(Tuple.Create(reader["FIO"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }
        }
    }
}
