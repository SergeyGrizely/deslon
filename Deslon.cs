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
    
    public partial class deslon : Form
        
    {
        User Im = new User();
        User ID = new User();
        string id;
        User Friend = new User();
        public string Nfriend;
        class Response
        {
            public string sms { get; set; }
        }

        private MySqlConnection mySqlConnection;
        public deslon()
        {
            InitializeComponent();
            panel3.Visible = false;
            textBox1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }
        class Msg
        {
            public string Message { get; set; }
        }
        public DataTable Query(string script)
        {
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(script, mySqlConnection);
            DataTable table = new DataTable();
            dataAdapter.Fill(table);

            return table;
        }

        List<User> ListUsers = new List<User>();
        string log = File.ReadAllText("Login.txt");
        private void Osnova_Load(object sender, EventArgs e)
        {
            mySqlConnection = new MySqlConnection("server=tsovyan.myjino.ru;port=3306;username=047094112_sergey;password=K@2j2344!s4P0%$p;database=tsovyan_deslon");

            mySqlConnection.Open();

            //Загружаем всех пользователей
            DataTable data = Query("select `first_name`,`last_name`, `password`, `phone`, `id` from users;");
           
            foreach(DataRow row in data.Rows)
            {
                User user = new User();


                user.first_Name = row.ItemArray[0].ToString();

                user.last_Name = row.ItemArray[1].ToString();

                user.Password = row.ItemArray[2].ToString();

                user.Phone = row.ItemArray[3].ToString();

                user.Id = row.ItemArray[4].ToString();
                //Добавляем пользователей в коллецию
                ListUsers.Add(user);
            }

            try
            {
                User user = ListUsers.Find(x => x.Phone == log);
                if (user != null)
                {
                    string im = $"{user.last_Name} {user.first_Name}";
                    label1.Text = im;
                    Im.im = im;
                    id = $"{ user.Id}";
                    ID.myId = id;

                }
                else //иначе, т.е. если в ридере нет строк
                {
                    MessageBox.Show("Не верный логин или пароль");  //сообщаем, что что-то не то
                                                                  
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
            Friends f2 = new Friends();
            Nfriend = f2.friend;
            string im1 = Im.im;
            var array = im1.Split(' ');
            //Если первая фамилия то делаешь так:
            var name2 = array[0];
            var name1 = array[1];
            

            User user1 = ListUsers.Find(x => x.Phone == "79180044442");
            User user1_1 = ListUsers.Find(x => x.last_Name != name1);
            User user1_2 = ListUsers.Find(x => x.first_Name != name2);
            if (user1 != null && user1_1 != null && user1_2 != null)
            {
               comboBox1.Items.Add($"{user1.last_Name} {user1.first_Name}");
            }

            User user2 = ListUsers.Find(x => x.Phone == "79180044443");
            User user2_1 = ListUsers.Find(x => x.last_Name != name1);
            User user2_2 = ListUsers.Find(x => x.first_Name != name2);
            if (user2 != null && user2_1 != null && user2_2 != null)
            {
                comboBox1.Items.Add($"{user2.last_Name} {user2.first_Name}");
            }

            User user3 = ListUsers.Find(x => x.Phone == "79180044441");
            User user3_1 = ListUsers.Find(x => x.last_Name != name1);
            User user3_2 = ListUsers.Find(x => x.first_Name != name2);
            if (user3 != null && user3_1 != null && user3_2 != null)
            {
                comboBox1.Items.Add($"{user3.last_Name} {user3.first_Name}");
            }

            User user0 = ListUsers.Find(x => x.Id == Nfriend);
            if (user0 != null)
            {
                comboBox1.Items.Add($"{user0.last_Name} {user0.first_Name}");
            }

        }
        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            string selectedState = comboBox1.SelectedItem.ToString();
            var array = selectedState.Split(' ');
            //Если первая фамилия то делаешь так:
            var surname = array[0];
            var name = array[1];
            MySqlConnection mySqlConnection = new MySqlConnection("server=tsovyan.myjino.ru;port=3306;username=047094112_sergey;password=K@2j2344!s4P0%$p;database=tsovyan_deslon");
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
                        Nfriend = $"{ user.Id}";
                        Friend.FR = Nfriend;

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

        private void button1_Click(object sender, EventArgs e)
        {
            string fr = Friend.FR;
            string myid = ID.myId;
            string time = DateTime.Now.ToString("HH:mm");
            string Dattime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Dattime = textBox1.Text;
            if (richTextBox1.Text != "")
            {
                
                listView1.Items.Add(new ListViewItem() { BackColor = Color.Aquamarine, Text = time + "  " + richTextBox1.Text });
                
                string jsmes = JsonConvert.SerializeObject(new Response() { sms = richTextBox1.Text });
                MySqlConnection mySqlConnection = new MySqlConnection("server=tsovyan.myjino.ru;port=3306;" +
                    "username=047094112_sergey;password=K@2j2344!s4P0%$p;database=tsovyan_deslon;charset=utf8");
                mySqlConnection.Open();
                MySqlCommand command = new MySqlCommand("insert into messages" +
                "(id_user, id_friend, message, date, message_status) Values (@myId, @fr, @mes, @time, 0)", mySqlConnection);
                command.Parameters.AddWithValue("@myId", myid);
                command.Parameters.AddWithValue("@mes", jsmes);
                command.Parameters.AddWithValue("@time", Dattime);
                command.Parameters.AddWithValue("@fr", fr);

                command.ExecuteNonQuery();
                mySqlConnection.Close();
                richTextBox1.Text = "";
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            Friends f3 = new Friends();
            f3.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Profil f4 = new Profil();
            f4.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            
        }

        

    }
}
