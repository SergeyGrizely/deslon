using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Net.Sockets;


namespace deslon
{
    public partial class Friends : Form
    {
        public string friend;
        User Friend = new User();
        private MySqlConnection mySqlConnection;
        public Friends()
        {
            InitializeComponent();
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        }
        
        public DataTable Query(string script)
        {
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(script, mySqlConnection);
            DataTable table = new DataTable();
            dataAdapter.Fill(table);

            return table;
        }
        List<User> ListUsers = new List<User>();
        private void Friends_Load(object sender, EventArgs e)
        {
            mySqlConnection = new MySqlConnection("************");

            mySqlConnection.Open();

            //Загружаем всех пользователей
            DataTable data = Query("select `first_name`,`last_name`, `password`, `phone`, `id` from users;");
            
            
            foreach (DataRow row in data.Rows)
            {
                User user = new User();


                user.first_Name = row.ItemArray[0].ToString();

                user.last_Name = row.ItemArray[1].ToString();

                user.Password = row.ItemArray[2].ToString();

                user.Phone = row.ItemArray[3].ToString();

                user.Id = row.ItemArray[4].ToString();


                //Добавляем пользователей в коллецию
                ListUsers.Add(user);
                
                listBox1.Items.Clear();
                for (int i = 0; i < ListUsers.Count; i++)
                {
                    listBox1.Items.Add(ListUsers[i].first_Name + " " + ListUsers[i].last_Name);
                }

            }
        }
        void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = listBox1.SelectedItem.ToString();
            var array = selectedState.Split(' ');
            //Если первая фамилия то делаешь так:
            var surname = array[1];
            var name = array[0];
            MySqlConnection mySqlConnection = new MySqlConnection("*****************");
            try
            {
                mySqlConnection.Open();
                MySqlCommand sCommand = new MySqlCommand("select * from users where first_name = @name and last_name = @lastname", mySqlConnection);
                sCommand.Parameters.AddWithValue("@name", name);
                sCommand.Parameters.AddWithValue("@lastname", surname);
                MySqlDataReader mySqlDataReader = sCommand.ExecuteReader();
                if (mySqlDataReader.HasRows)
                {
                    User user = ListUsers.Find(x => x.last_Name == surname);
                    User userer = ListUsers.Find(x => x.first_Name == name);
                    if (user != null && userer != null)
                    {
                        friend = $"{ user.Id}";
                    }
                }
                else
                {
                    MessageBox.Show("Не верный логин или пароль");

                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                mySqlConnection.Close();
            }
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string peop = textBox1.Text;
            var array = peop.Split(' ');
            //Если первая фамилия то делаешь так:
            var surname = array[0];
            var name = array[1];
            //Если нет, то меняешь местами 0 и 1
            MySqlConnection mySqlConnection = new MySqlConnection("*****************");
            try
            {
                mySqlConnection.Open();
                MySqlCommand sCommand = new MySqlCommand("select * from users where first_name = @name and last_name = @lastname", mySqlConnection);
                sCommand.Parameters.AddWithValue("@name", name);
                sCommand.Parameters.AddWithValue("@lastname", surname);
                MySqlDataReader mySqlDataReader = sCommand.ExecuteReader();
                if (mySqlDataReader.HasRows)
                {
                    User user = ListUsers.Find(x => x.last_Name == surname);
                    User userer = ListUsers.Find(x => x.first_Name == name);
                    if (user != null && userer != null)
                    {
                        friend = $"{ user.Id}";
                        deslon f3 = new deslon();
                        f3.Nfriend= friend;
                    }
                }
                else
                {
                    MessageBox.Show("Не верный логин или пароль");

                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                mySqlConnection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Profil f4 = new Profil();
            f4.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            deslon f1 = new deslon();
            f1.Show();
            this.Hide();
        }
    }
}
