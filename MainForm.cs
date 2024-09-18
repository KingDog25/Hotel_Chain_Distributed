using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Media;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hotel_Chain_Distributed
{
    public partial class MainForm : Form
    {
        // Создаем экземпляр класса SoundPlayer и указываем путь к аудиофайлу
        SoundPlayer sound1 = new SoundPlayer(@"..\..\sound\Запрос.wav");
        SoundPlayer soundExit = new SoundPlayer(@"..\..\sound\Exit.wav");
        SoundPlayer soundAdd = new SoundPlayer(@"..\..\sound\lineAdd.wav");
        SoundPlayer soundDrum = new SoundPlayer(@"..\..\sound\DRUMROLL.WAV");
        SoundPlayer soundPush = new SoundPlayer(@"..\..\sound\PUSH.WAV");
        SoundPlayer soundWHOOSH = new SoundPlayer(@"..\..\sound\WHOOSH.WAV");

        public MainForm()
        {
            InitializeComponent();
            setSettings();
        }

        private void setSettings()
        {
            // Настройки для запрета изменений
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToResizeColumns = true;
            //dataGridView1.EditMode = DataGridViewEditMode.None;
        }

        void ElementsForMain()
        {
            добавитьToolStripMenuItem7.Visible = true;
            изменитьToolStripMenuItem2.Visible = true;
            удалитьToolStripMenuItem7.Visible = true;
            добавитьToolStripMenuItem8.Visible = false;
            изменитьToolStripMenuItem4.Visible = false;
            удалитьToolStripMenuItem8.Visible = false;   
            добавитьToolStripMenuItem9.Visible = false;
            изменитьToolStripMenuItem5.Visible = false;
            удалитьToolStripMenuItem9.Visible = false;
            типToolStripMenuItem.Visible = false;
            услугиToolStripMenuItem.Visible = false;
            оказанныеУслугиToolStripMenuItem1.Visible = false;
            сотрудникToolStripMenuItem1.Visible = false;
            добавитьToolStripMenuItem14.Visible = false;
            удалитьToolStripMenuItem11.Visible = false;
            всеАктивныеБронированияToolStripMenuItem.Visible = false;
            наиболееВостребованныеУслугиToolStripMenuItem.Visible = true;
            средняяСтоимостьБронированияПоТипамНомеровToolStripMenuItem.Visible = true;
        }

        void ElementsForBranch()
        {
            добавитьToolStripMenuItem7.Visible = false;
            изменитьToolStripMenuItem2.Visible = false;
            удалитьToolStripMenuItem7.Visible = false;
            //добавитьToolStripMenuItem8.Visible = true;
            //изменитьToolStripMenuItem4.Visible = true;
            //удалитьToolStripMenuItem8.Visible = true;
            добавитьToolStripMenuItem9.Visible = true;
            изменитьToolStripMenuItem5.Visible = true;
            удалитьToolStripMenuItem9.Visible = true;
            типToolStripMenuItem.Visible = true;
            услугиToolStripMenuItem.Visible = true;
            оказанныеУслугиToolStripMenuItem1.Visible = true;
            сотрудникToolStripMenuItem1.Visible = true;
            добавитьToolStripMenuItem14.Visible = true;
            удалитьToolStripMenuItem11.Visible = true;
            всеАктивныеБронированияToolStripMenuItem.Visible = true;
            наиболееВостребованныеУслугиToolStripMenuItem.Visible = false;
            средняяСтоимостьБронированияПоТипамНомеровToolStripMenuItem.Visible = false;
        }


        void LoadAutorizForm()
        {
            // Создание экземпляра формы AuthorizationForm
            AutorizationForm authorizationForm = new AutorizationForm();
            authorizationForm.ShowDialog();
            // Проверка результата авторизации
            if (authorizationForm.DialogResult == DialogResult.OK)
            {
                this.Show();    // Отображение главной формы
                SingletonClass autorizObject = SingletonClass.getInstance();
                if (autorizObject.getField1() == 1) //если авторизован центральный
                {
                    ElementsForMain();
                    this.Text = "Отель Центральный";
                }
                if (autorizObject.getField1() == 2) //если авторизован филиал
                {
                    ElementsForBranch();
                    this.Text = "Филиал " + Hotel.Name;
                }
            }
            else
            {
                Application.Exit();     // Закрытие приложения, если авторизация не прошла успешно
            }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadAutorizForm();
        }

        void selectQuery(string query, string tableName, int autorizObject)
        {
            string connectionString;
            if (autorizObject == 1)
                connectionString = ConfigurationManager.ConnectionStrings["Main_Hotel"].ConnectionString;
            else
                connectionString = ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, sqlConnection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
                Table.tableName = tableName;
                // Автоматически подгоняем ширину столбцов под содержимое ячеек
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView1.AutoResizeColumns();
                sound1.Play(); // Воспроизводим звук
            }
        }

        private void ExecuteQuery(string query, SqlParameter[] parameters, SqlConnection sqlConnection)
        {
            try
            {
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddRange(parameters);

                if (command.ExecuteNonQuery() < 1)
                    MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
                else
                {
                    MessageBox.Show("Данные обработаны!", "Внимание!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void добавитьToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            AddServiceForm addServiceForm = new AddServiceForm();
            addServiceForm.ShowDialog();
            if (addServiceForm.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        //new SqlParameter("@ID", Hotel.id),
                        new SqlParameter("@Capacity", Room.Capacity),
                        new SqlParameter("@isAvailable", Room.isAvailable),
                        new SqlParameter("@Floor", Room.Floor),
                        new SqlParameter("@Room_number", Room.Numb),
                        new SqlParameter("@Hotel_ID", Hotel.id),
                        new SqlParameter("@Type_ID", Room.Type_ID),
                    };

                    string query = "INSERT INTO Room (Capacity, isAvailable, Floor, Room_number, Hotel_ID, Type_ID) VALUES (@Capacity, @isAvailable, @Floor, @Room_number, @Hotel_ID, @Type_ID)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                номерToolStripMenuItem.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void отельToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SingletonClass autorizObject = SingletonClass.getInstance();
            if (autorizObject.getField1() == 1) //если авторизован центральный
            {
                string query = "SELECT ID AS 'Номер отеля', Name AS 'Название', Address AS 'Адрес',  Phone AS 'Телефон' From Hotel";
                selectQuery(query, "Hotel", autorizObject.getField1());
            }

            else if (autorizObject.getField1() == 2) //если авторизован офис
            {
                string query = $"SELECT ID AS 'Номер отеля', Name AS 'Название', Address AS 'Адрес',  Phone AS 'Телефон' From Hotel WHERE ID = {Hotel.id}";
                selectQuery(query, "Hotel", autorizObject.getField1());
            }
        }

        private void типToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SingletonClass autorizObject = SingletonClass.getInstance();
            //string query = $"SELECT t.ID AS 'ID Типа', t.Type AS 'Тип номера', t.Cost_per_day AS 'Стоимость за сутки', h.Name AS 'Отель' From Room r JOIN Hotel h ON h.ID=r.Hotel_ID JOIN Type_numb t ON t.ID=r.Type_ID WHERE h.ID = {Hotel.id}";
            string query = "SELECT ID AS 'ID Типа', Type AS 'Тип номера', Cost_per_day AS 'Стоимость за сутки' FROM Type_numb";
            selectQuery(query, "Type_numb", autorizObject.getField1());
        }

        private void номерToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SingletonClass autorizObject = SingletonClass.getInstance();
            if (autorizObject.getField1() == 1)
            {
                string query = "SELECT r.ID AS 'ID Номера', Capacity AS 'Вместимость', Floor AS 'Этаж', isAvailable AS 'Свободен', Room_number AS 'Номер комнаты', h.Name AS 'Отель' From Room r JOIN Hotel h ON h.ID=r.Hotel_ID";
                selectQuery(query, "Room", autorizObject.getField1());
            }

            else if (autorizObject.getField1() == 2)
            {
                string query = $"SELECT r.ID AS 'ID Номера', Capacity AS 'Вместимость', Floor AS 'Этаж', isAvailable AS 'Свободен', Room_number AS 'Номер комнаты', t.Type AS 'Тип номера', h.Name AS 'Отель' From Room r JOIN Hotel h ON h.ID=r.Hotel_ID JOIN Type_numb t ON t.ID=r.Type_ID WHERE h.ID = {Hotel.id}";
                selectQuery(query, "Room", autorizObject.getField1());
            }
        }

        private void клиентToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SingletonClass autorizObject = SingletonClass.getInstance();
            string query = "SELECT ID AS 'ID клиента', FIO AS 'ФИО клиента', Email AS 'Почта',  Phone AS 'Телефон', Login AS 'Логин', Pass AS 'Пароль' From Client";
            selectQuery(query, "Client", autorizObject.getField1());
        }

        private void услугиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SingletonClass autorizObject = SingletonClass.getInstance();
            string query = "SELECT ID AS 'ID услуги', Name AS 'Название', Cost AS 'Стоимость' From Service";
            selectQuery(query, "Service", autorizObject.getField1());
        }

        private void сотрудникToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SingletonClass autorizObject = SingletonClass.getInstance();
            string query = $"SELECT e.ID AS 'ID сотрудника', e.FIO AS 'ФИО сотрудника', e.Specialization AS 'Специализация', h.Name AS 'Отель' From Employee e JOIN Service_List s ON s.Employee_ID=e.ID JOIN Booking b ON b.ID=s.Booking_ID JOIN Room r ON b.Room_ID=r.ID JOIN Hotel h ON h.ID=r.Hotel_ID WHERE h.ID = {Hotel.id} OR h.ID IS NULL";
            selectQuery(query, "Employee", autorizObject.getField1());
        }

        private void добавитьToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            AddClientForm addClientForm = new AddClientForm();
            addClientForm.ShowDialog();
            if (addClientForm.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        //new SqlParameter("@ID", Hotel.id),
                        new SqlParameter("@FIO", Person.Name),
                        new SqlParameter("@Email", Person.Pt),
                        new SqlParameter("@Phone", Person.Tl),
                        new SqlParameter("@Login", Person.Sur),
                        new SqlParameter("@Pass", Person.Sr),
                    };

                    string query = "INSERT INTO Client (FIO, Email, Phone, Login, Pass) VALUES (@FIO, @Email, @Phone, @Login, @Pass)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                клиентToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void добавитьToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            AddNumbHotelForm addNumbHotelForm = new AddNumbHotelForm();
            addNumbHotelForm.ShowDialog();
            if (addNumbHotelForm.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        //new SqlParameter("@ID", Hotel.id),
                        new SqlParameter("@Capacity", Room.Capacity),
                        new SqlParameter("@isAvailable", true),
                        new SqlParameter("@Floor", Room.Floor),
                        new SqlParameter("@Room_number", Room.Numb),
                        new SqlParameter("@Hotel_ID", Hotel.id),
                        new SqlParameter("@Type_ID", Room.Type_ID),
                    };

                    string query = "INSERT INTO Room (Capacity, isAvailable, Floor, Room_number, Hotel_ID, Type_ID) VALUES (@Capacity, @isAvailable, @Floor, @Room_number, @Hotel_ID, @Type_ID)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                номерToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void добавитьToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            AddTypeNumbForm addTypeNumbForm = new AddTypeNumbForm();
            addTypeNumbForm.ShowDialog();
            if (addTypeNumbForm.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        //new SqlParameter("@ID", Hotel.id),
                        new SqlParameter("@Type", Room.type),
                        new SqlParameter("@Cost_per_day", Room.cost),
                    };

                    string query = "INSERT INTO Type_numb (Type, Cost_per_day) VALUES (@Type, @Cost_per_day)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                типToolStripMenuItem.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void удалитьToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            DeleteTypeNumbForm deleteTypeNumbForm = new DeleteTypeNumbForm();
            deleteTypeNumbForm.ShowDialog();
            if (deleteTypeNumbForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id.ToString()),
                };
                string query = "DELETE FROM Type_numb WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                типToolStripMenuItem.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }

        private void изменитьToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            UpdateTypeNumbForm updateTypeNumbForm = new UpdateTypeNumbForm();
            updateTypeNumbForm.ShowDialog();
            if (updateTypeNumbForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Room.Type_ID),
                    new SqlParameter("@Type", Room.type),
                    new SqlParameter("@Cost_per_day", Room.cost),
            };

                string query = "UPDATE Type_numb SET Type = @Type, Cost_per_day = @Cost_per_day WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                типToolStripMenuItem.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }

        private void добавитьToolStripMenuItem11_Click(object sender, EventArgs e)
        {
            AddServiceForm addServiceForm = new AddServiceForm();
            addServiceForm.ShowDialog();
            if (addServiceForm.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        //new SqlParameter("@ID", Hotel.id),
                        new SqlParameter("@Name", Person.Name),
                        new SqlParameter("@Cost", Person.cost),
                    };

                    string query = "INSERT INTO Service (Name, Cost) VALUES (@Name, @Cost)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                услугиToolStripMenuItem.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void оказанныеУслугиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SingletonClass autorizObject = SingletonClass.getInstance();
            string query = $"SELECT t.ID AS 'ID услуги', r.Room_number AS 'Номер комнаты', t.Type AS 'Тип номера', e.FIO AS 'Сотрудник', e.Specialization AS 'Специализация', s.Name AS 'Услуга',  s.Cost AS 'Стоимость', l.Count AS 'Количество', h.Name AS 'Отель' From Service_List l JOIN Service s ON l.Service_ID=s.ID JOIN Booking b ON b.ID=l.Booking_ID JOIN Room r ON b.Room_ID=r.ID JOIN Hotel h ON h.ID=r.Hotel_ID JOIN Type_numb t ON t.ID=r.Type_ID JOIN Employee e ON e.ID=l.Employee_ID WHERE h.ID = {Hotel.id}";
            selectQuery(query, "Service", autorizObject.getField1());
        }

        private void добавитьToolStripMenuItem12_Click(object sender, EventArgs e)
        {
            AddService_ListForm addService_ListForm = new AddService_ListForm();
            addService_ListForm.ShowDialog();
            if (addService_ListForm.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        //new SqlParameter("@ID", Hotel.id),
                        new SqlParameter("@Service_ID", Booking.services_id),
                        new SqlParameter("@Count", Person.count),
                        new SqlParameter("@Employee_ID", Person.Id),
                        new SqlParameter("@Booking_ID", Booking.id),
                    };

                    string query = "INSERT INTO Service_List (Service_ID, Count, Employee_ID, Booking_ID) VALUES (@Service_ID, @Count, @Employee_ID, @Booking_ID)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                оказанныеУслугиToolStripMenuItem.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void удалитьToolStripMenuItem12_Click(object sender, EventArgs e)
        {
            DeleteSetviceListForm deleteSetviceListForm = new DeleteSetviceListForm();
            deleteSetviceListForm.ShowDialog();
            if (deleteSetviceListForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id.ToString()),
                };
                string query = "DELETE FROM Service_List WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                оказанныеУслугиToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }

        private void удалитьToolStripMenuItem11_Click(object sender, EventArgs e)
        {

        }

        private void изменитьToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            UpdateRoomForm updateRoomForm = new UpdateRoomForm();
            updateRoomForm.ShowDialog();
            if (updateRoomForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Room.id),
                    new SqlParameter("@Capacity", Room.Capacity),
                    new SqlParameter("@isAvailable", Room.isAvailable),
                    new SqlParameter("@Floor", Room.Floor),
                    new SqlParameter("@Room_number", Room.Numb),
                    new SqlParameter("@Hotel_ID", Hotel.id),
                    new SqlParameter("@Type_ID", Room.Type_ID),
            };

                string query = "UPDATE Room SET Capacity = @Capacity, isAvailable = @isAvailable, Floor = @Floor, Room_number = @Room_number, Hotel_ID = @Hotel_ID, Type_ID = @Type_ID WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                номерToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }

        private void добавитьToolStripMenuItem13_Click(object sender, EventArgs e)
        {
            AddEmployeeForm addEmployeeForm = new AddEmployeeForm();
            addEmployeeForm.ShowDialog();

            if (addEmployeeForm.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        //new SqlParameter("@ID", Hotel.id),
                        new SqlParameter("@FIO", Person.Name),
                        new SqlParameter("@Specialization", Person.Sr),
                    };

                    string query = "INSERT INTO Employee (FIO, Specialization) VALUES (@FIO, @Specialization)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                сотрудникToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void изменитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            UpdateHotelForm updateHotelForm = new UpdateHotelForm();
            updateHotelForm.ShowDialog();
            if (updateHotelForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id),
                    new SqlParameter("@Name", Hotel.Name),
                    new SqlParameter("@Address", Hotel.Address),
                    new SqlParameter("@Phone", Hotel.Phone),
                };

                string query = "UPDATE Hotel SET Name = @Name, Address = @Address, Phone = @Phone WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main_Hotel"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                отельToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }

        private void удалитьToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            DeleteHotelForm deleteHotelForm = new DeleteHotelForm();
            deleteHotelForm.ShowDialog();
            if (deleteHotelForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Hotel.id.ToString()),
                };
                string query = "DELETE FROM Hotel WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main_Hotel"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                отельToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }

        private void изменитьToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            UpdateClientForm updateClientForm = new UpdateClientForm();
            updateClientForm.ShowDialog();
            if (updateClientForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id),
                    new SqlParameter("@FIO", Person.Name),
                    new SqlParameter("@Email", Person.Pt),
                    new SqlParameter("@Phone", Person.Tl),
                    new SqlParameter("@Login", Person.Sr),
                    new SqlParameter("@Pass", Person.Po_U),
            };

                string query = "UPDATE Client SET FIO = @FIO, Email = @Email, Phone = @Phone, Login = @Login, Pass = @Pass  WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                клиентToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }

        //удалить сервис
        private void изменитьToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            DeleteServicesForm deleteServicesForm = new DeleteServicesForm();
            deleteServicesForm.ShowDialog();
            if (deleteServicesForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id.ToString()),
                };
                string query = "DELETE FROM Service WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                услугиToolStripMenuItem.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }

        private void добавитьToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            AddHotelForm addHotelForm = new AddHotelForm();
            addHotelForm.ShowDialog();

            if (addHotelForm.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main_Hotel"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        //new SqlParameter("@ID", Hotel.id),
                        new SqlParameter("@Name", Hotel.Name),
                        new SqlParameter("@Address", Hotel.Address),
                        new SqlParameter("@Phone", Hotel.Phone)
                    };

                    string query = "INSERT INTO Hotel (Name, Address, Phone) VALUES (@Name, @Address, @Phone)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                отельToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void удалитьToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            DeleteClientForm deleteClientForm = new DeleteClientForm();
            deleteClientForm.ShowDialog();
            if (deleteClientForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id.ToString()),
                };
                string query = "DELETE FROM Client WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                клиентToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }

        private void удалитьToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            DeleteRoomForm deleteRoomForm = new DeleteRoomForm();
            deleteRoomForm.ShowDialog();
            if (deleteRoomForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id.ToString()),
                };
                string query = "DELETE FROM Room WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                номерToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }

        private void оПрограммеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ИС Распределенная сеть гостиниц\n" +
                "v 1.0.1");
        }

        private void деавторизацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            LoadAutorizForm();
        }

        private void выходToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void добавитьToolStripMenuItem14_Click(object sender, EventArgs e)
        {
            AddBookingForm addBookingForm = new AddBookingForm();
            addBookingForm.ShowDialog();
            if (addBookingForm.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        //new SqlParameter("@ID", Hotel.id),
                        new SqlParameter("@Client_ID", Person.Id),
                        new SqlParameter("@Room_ID", Room.id),
                        new SqlParameter("@Checkin_date", Person.dateTime1),
                        new SqlParameter("@Checkout_date", Person.dateTime2),
                    };

                    string query = "INSERT INTO Booking (Client_ID, Room_ID, Checkin_date, Checkout_date) VALUES (@Client_ID, @Room_ID, @Checkin_date, @Checkout_date)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                бронированиеToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void удалитьToolStripMenuItem11_Click_1(object sender, EventArgs e)
        {
            DeleteBookingForm deleteBookingForm = new DeleteBookingForm();
            deleteBookingForm.ShowDialog();
            if (deleteBookingForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id.ToString()),
                };
                string query = "DELETE FROM Booking WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                бронированиеToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }

        private void справкаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ИС Распределенная сеть гостиниц (Прототип)\n" 
                + "Управяет 2 БД - центральной и филиалами\n" 
                + "Таблицы рекомндуется заполнять в таком порядке:\n"
                + "Отель\n"
                + "Тип номера\n"
                + "Номер\n"
                + "Сотрудник\n"
                + "Услуги\n"
                + "Клиент\n"
                + "Бронирование\n"
                + "Оказанные услуги\n");
        }

        private void бронированиеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SingletonClass autorizObject = SingletonClass.getInstance();
            if (autorizObject.getField1() == 1) //если авторизован центральный
            {
                string query = "SELECT b.ID AS 'ID бронирования', c.FIO AS 'ФИО клиента', r.Floor AS 'Этаж', r.Capacity AS 'Эмкость', Room_number AS 'Номер комнаты', b.Checkin_date AS 'Дата заезда', b.Checkout_date AS 'Дата выезда', b.Cost_booking AS 'Стоимость бронирования', Cost_services AS 'Стоимость услуг', h.Name AS 'Отель' From Booking b JOIN Room r ON b.Room_ID=r.ID JOIN Hotel h ON h.ID=r.Hotel_ID JOIN Client c ON c.ID=b.Client_ID";
                selectQuery(query, "Booking", autorizObject.getField1());
            }

            else if (autorizObject.getField1() == 2) //если авторизован офис
            {
                string query = $"SELECT b.ID AS 'ID бронирования', c.FIO AS 'ФИО клиента', r.Floor AS 'Этаж', r.Capacity AS 'Эмкость', Room_number AS 'Номер комнаты', b.Checkin_date AS 'Дата заезда', b.Checkout_date AS 'Дата выезда', b.Cost_booking AS 'Стоимость бронирования', Cost_services AS 'Стоимость услуг', h.Name AS 'Отель' From Booking b JOIN Room r ON b.Room_ID=r.ID JOIN Hotel h ON h.ID=r.Hotel_ID JOIN Client c ON c.ID=b.Client_ID WHERE h.ID = {Hotel.id}";
                selectQuery(query, "Booking", autorizObject.getField1());
            }
        }

        private void изменитьToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            UpdateEmployeeForm updateEmployeeForm = new UpdateEmployeeForm();
            updateEmployeeForm.ShowDialog();
            if (updateEmployeeForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id),
                    new SqlParameter("@FIO", Person.Name),
                    new SqlParameter("@Specialization", Person.Sur),
                };

                string query = "UPDATE Employee SET FIO = @FIO, Specialization = @Specialization WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                сотрудникToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }

        private void удалитьToolStripMenuItem13_Click(object sender, EventArgs e)
        {
            DeleteEmployeeForm deleteEmployeeForm = new DeleteEmployeeForm();
            deleteEmployeeForm.ShowDialog();
            if (deleteEmployeeForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id.ToString()),
                };
                string query = "DELETE FROM Employee WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Branch_Hotel"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                сотрудникToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }

        private void всеАктивныеБронированияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SingletonClass autorizObject = SingletonClass.getInstance();
            if (autorizObject.getField1() == 1)
            {
                string query = "SELECT b.ID AS Booking_ID, c.FIO AS Client_Name, r.Room_number AS Room_Number, h.Name AS Hotel_Name, b.Checkin_date AS Checkin_Date, b.Checkout_date AS Checkout_Date, b.Cost_booking AS Booking_Cost, b.Cost_services AS Services_Cost FROM [dbo].[Booking] b JOIN [dbo].[Client] c ON b.Client_ID = c.ID JOIN [dbo].[Room] r ON b.Room_ID = r.ID JOIN [dbo].[Hotel] h ON r.Hotel_ID = h.ID WHERE b.Checkout_date >= GETDATE();";
                selectQuery(query, "Booking", autorizObject.getField1());
            }

            else if (autorizObject.getField1() == 2)
            {
                string query = $"SELECT b.ID AS Booking_ID, c.FIO AS Client_Name, r.Room_number AS Room_Number, h.Name AS Hotel_Name, b.Checkin_date AS Checkin_Date, b.Checkout_date AS Checkout_Date, b.Cost_booking AS Booking_Cost, b.Cost_services AS Services_Cost FROM [dbo].[Booking] b JOIN [dbo].[Client] c ON b.Client_ID = c.ID JOIN [dbo].[Room] r ON b.Room_ID = r.ID JOIN [dbo].[Hotel] h ON r.Hotel_ID = h.ID WHERE b.Checkout_date >= GETDATE() AND h.ID = {Hotel.id};";
                selectQuery(query, "Booking", autorizObject.getField1());
            }
        }

        private void наиболееВостребованныеУслугиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = $"SELECT b.ID AS Booking_ID, c.FIO AS Client_Name, r.Room_number AS Room_Number, h.Name AS Hotel_Name, b.Checkin_date AS Checkin_Date, b.Checkout_date AS Checkout_Date, b.Cost_booking AS Booking_Cost, b.Cost_services AS Services_Cost FROM [dbo].[Booking] b JOIN [dbo].[Client] c ON b.Client_ID = c.ID JOIN [dbo].[Room] r ON b.Room_ID = r.ID JOIN [dbo].[Hotel] h ON r.Hotel_ID = h.ID WHERE b.Checkout_date >= GETDATE() AND h.ID = {Hotel.id};";
            selectQuery(query, "Booking", autorizObject.getField1());
        }
    }
}
