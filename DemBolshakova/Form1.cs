using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace DemBolshakova
{
    public partial class Form1 : Form
    {
        DataBase database = new DataBase();

        public Form1()
        {
            InitializeComponent();
            
        }

        private void LoadData(object sender, EventArgs e)
        {
            string connectString = "Data Source= LAPTOP-BLTQNR9D;Initial Catalog=DataGridView;" + "Integrated Security=true;";

            SqlConnection myConnection = new SqlConnection(connectString);

            myConnection.Open();

            string query = "SELECT * FROM Group_IS ORDER BY fspo_id";

            SqlCommand command = new SqlCommand(query, myConnection);

            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[3]);

                data[data.Count - 1][0] = reader["fspo_id"].ToString();
                data[data.Count - 1][1] = reader["fspo_fio"].ToString();
                data[data.Count - 1][2] = reader["fspo_name"].ToString();
            }

            reader.Close();

            myConnection.Close();

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fspoFio = textBox1.Text;
            string fspoName = textBox2.Text;

            string connectString = "Data Source= LAPTOP-BLTQNR9D;Initial Catalog=DataGridView;Integrated Security=true;";

            using (SqlConnection myConnection = new SqlConnection(connectString))
            {
                myConnection.Open();

                string query = "INSERT INTO Group_IS (fspo_fio,fspo_name) VALUES (@fio, @name); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    command.Parameters.AddWithValue("@fio", fspoFio);
                    command.Parameters.AddWithValue("@name", fspoName);

                    int newId = Convert.ToInt32(command.ExecuteScalar());

                    dataGridView1.Rows.Add(new object[] {newId, fspoFio, fspoName });
                    MessageBox.Show("Вы успешно добавили", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            textBox1.Clear();
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Id = textBox3.Text;

            string connectString = "Data Source= LAPTOP-BLTQNR9D;Initial Catalog=DataGridView;Integrated Security=true;";

            string Delete = "DELETE FROM Group_IS " + "WHERE fspo_Id = " + Id;

            SqlConnection myConnection = new SqlConnection(connectString);

            using (SqlCommand command = new SqlCommand(Delete, myConnection))
            {
                myConnection.Open();

                command.ExecuteNonQuery();

                myConnection.Close();

                MessageBox.Show("Вы успешно удалили", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            textBox3.Clear();
        }

    }
}
