using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Save_DB_Gorbachev
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBackup_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Параметры подключения к базе данных
                string connectionString = "Data Source=(local);Initial Catalog=GORBACHEV;Integrated Security=True";
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                string databaseName = builder.InitialCatalog;
                string serverName = builder.DataSource;

                // Создание пути для сохранения резервной копии
                string backupDirectory = @"C:\Users\georg\OneDrive\Рабочий стол\Fract2K\DB"; // Укажите путь к папке, где будет сохраняться резервная копия
                string backupFileName = $"{serverName}_{databaseName}_{DateTime.Now:yyyyMMddHHmmss}.bak";
                string backupFilePath = System.IO.Path.Combine(backupDirectory, backupFileName);

                // Создание соединения с базой данных
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Создание команды для создания резервной копии
                    using (SqlCommand command = new SqlCommand($"BACKUP DATABASE [{databaseName}] TO DISK='{backupFilePath}' WITH FORMAT", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Резервная копия базы данных успешно создана.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании резервной копии базы данных: {ex.Message}");
            }
        }
    }


            
        
    
}

    
