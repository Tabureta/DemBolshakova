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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DemBolshakova
{
    public partial class Sing_up : Form
    {
        DataBase dataBase = new DataBase();
        public Sing_up()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Log_in log_In = new Log_in();
            log_In.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkUser())
            {
                return;
            }

            var login = textBox_log.Text;
            var password = textBox_pass.Text;

            string querystring = $"insert into register(login_user, password_user) values('{login}','{password}')";

            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            dataBase.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан!", "Успех!");
                Log_in log_In = new Log_in();
                this.Hide();
                log_In.ShowDialog();
            }
            else
            {
                MessageBox.Show("Аккаунт не создан!");
            }
            dataBase.closeConnection();


        }
        private Boolean checkUser()
        {
            var loginUser = textBox_log.Text;
            var passUser = textBox_pass.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select id_user, login_user, password_user from register where login_user = '{loginUser}' and password_user = '{passUser}';";

            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует!");
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
