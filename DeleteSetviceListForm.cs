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
    public partial class DeleteSetviceListForm : Form
    {
        public DeleteSetviceListForm()
        {
            InitializeComponent();
        }

        private void DeleteSetviceListForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT l.ID, s.Name FROM Service_List l JOIN Service s ON s.ID=l.Service_ID JOIN Booking b ON b.ID=l.Booking_ID JOIN Room r ON b.Room_ID=r.ID JOIN Hotel h ON h.ID=r.Hotel_ID JOIN Type_numb t ON t.ID=r.Type_ID JOIN Employee e ON e.ID=l.Employee_ID WHERE h.ID = {Hotel.id}", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(Tuple.Create(reader["Name"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tuple<string, int> selectedTuple = (Tuple<string, int>)comboBox1.SelectedItem;
            Person.Id = selectedTuple.Item2;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
