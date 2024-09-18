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
    public partial class AutorizationForm : Form
    {
        public AutorizationForm()
        {
            InitializeComponent();
        }

        private void AutorizationForm_Load(object sender, EventArgs e)
        {
            labelAutLogin.Visible = false;
            comboBox1.Visible = false;
        }

        private void comboBoxAut_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAut.SelectedIndex == 1)
            {
                labelAutLogin.Visible = true;
                comboBox1.Visible = true;
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
            else
            {
                labelAutLogin.Visible = false;
                comboBox1.Visible = false;
                comboBox1.Items.Clear();
            }
        }

        private void buttonAutOK_Click(object sender, EventArgs e)
        {
            string login = textBoxAutLogin.Text;
            string password = textBoxAutPass.Text;

            if (login == "test" && password == "test" && comboBoxAut.SelectedIndex != -1)
            {
                SingletonClass autorizObject = SingletonClass.getInstance();

                if (comboBoxAut.SelectedIndex == 0)
                {
                    autorizObject.setField1(1);     //1й-тип авторизации (Центральный отель)
                    this.DialogResult = DialogResult.OK;     // Установка результата авторизации
                    this.Close(); // Закрываем форму только если выбран первый тип авторизации
                }
                else if (comboBoxAut.SelectedIndex == 1)
                {
                    autorizObject.setField1(2);     //2й-тип авторизации (Филиал)
                    if (comboBox1.SelectedIndex == -1)
                    {
                        MessageBox.Show("Выберите тип отеля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Возвращаемся из метода без закрытия формы
                    }
                    else
                    {
                        this.Close(); // Закрываем форму если выбран второй тип и сделан выбор в comboBox1
                        this.DialogResult = DialogResult.OK;     // Установка результата авторизации
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                var tuple = (Tuple<string, int>)comboBox1.SelectedItem; // заменить индекс_элемента на нужный индекс
                Hotel.id = tuple.Item2;
                Hotel.Name = tuple.Item1;
            }
        }
    }
}
