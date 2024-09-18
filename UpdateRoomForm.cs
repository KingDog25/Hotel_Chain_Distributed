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
    public partial class UpdateRoomForm : Form
    {
        public UpdateRoomForm()
        {
            InitializeComponent();
        }

        private void UpdateRoomForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "branch_HotelDataSet.Type_numb". При необходимости она может быть перемещена или удалена.
            this.type_numbTableAdapter.Fill(this.branch_HotelDataSet.Type_numb);
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT r.ID, r.Room_number FROM Room r JOIN Hotel h ON h.ID=r.Hotel_ID WHERE h.ID = {Hotel.id}", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(Tuple.Create(reader["Room_number"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tuple<string, int> selectedTuple = (Tuple<string, int>)comboBox1.SelectedItem;
            //Hotel.Name = selectedTuple.Item1;
            Room.id = selectedTuple.Item2;
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Capacity, isAvailable, Floor, Room_number, Hotel_ID, Type_ID FROM Room WHERE ID = @id", sqlConnection);
                cmd.Parameters.AddWithValue("@id", Room.id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    numericUpDown1.Value = Convert.ToDecimal(reader["Room_number"].ToString());
                    numericUpDown2.Value = Convert.ToDecimal(reader["Capacity"].ToString());
                    numericUpDown3.Value = Convert.ToDecimal(reader["Floor"].ToString());
                    comboBox2.Text = reader["Type_ID"].ToString();
                    checkBox1.Checked = Convert.ToBoolean(reader["isAvailable"]);
                }
                reader.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox1.Text = "Да";
            }
            else
            {
                checkBox1.Text = "Нет";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Room.Capacity = Convert.ToInt32(numericUpDown2.Value);
            Room.isAvailable = checkBox1.Checked;
            Room.Numb = Convert.ToInt32(numericUpDown1.Value);
            Room.Floor = Convert.ToInt32(numericUpDown3.Value);
            Room.Type_ID = Convert.ToInt32(comboBox2.SelectedValue);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
