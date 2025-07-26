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

namespace deslon
{
    public partial class Open : Form
    {
       
        public Open()
        {
            InitializeComponent();
        }

        private readonly string FileName = @"PhoneNumbers.txt";

        private void button1_Click(object sender, EventArgs e)
        {
            WriteInFileIfOk();
        }

        public void WriteInFileIfOk()
        {
            int Row = File.ReadAllLines(FileName, Encoding.Default).Where(_ => _.SequenceEqual(textBox1.Text)).Select(_ => _).Count();

            using (StreamWriter Writer = new StreamWriter(FileName, true, Encoding.Default))
            {
                if (Row == 0)
                {
                    if (textBox1.Text.Length == 11)
                    {
                        if (textBox1.Text.All(char.IsDigit))
                        {
                            Writer.WriteLine(textBox1.Text);

                            MessageBox.Show("Номер добавлен в файл.");
                        }
                        else
                        {
                            MessageBox.Show("Недопустимый формат номера. Номер должен содержать только цифры.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Длина введенного номера меньше допустимой. Длина номера должна быть 11 цифр.");
                    }

                }
                else
                {
                    MessageBox.Show("Введенный номер уже существует в файле.");
                }
            }
        }

        //Создание обьекта для MD5 хеша
        private string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }

        //Убираю задний фон у пароля и логина
        private void Open_Load(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;
        }


        private void buttonLogin_Click(object sender, EventArgs e)
        {
            MySqlConnection mySqlConnection = new MySqlConnection("*******");
            try
            {
                mySqlConnection.Open();
                MySqlCommand sCommand = new MySqlCommand("select * from users where phone = @iLogin and password = @iPassword", mySqlConnection);
                sCommand.Parameters.AddWithValue("@iLogin", textBox1.Text);
                sCommand.Parameters.AddWithValue("@iPassword", GetMd5Hash(textBox2.Text));

                File.WriteAllText("Login.txt", textBox1.Text);
                
                MySqlDataReader mySqlDataReader = sCommand.ExecuteReader();
                if (mySqlDataReader.HasRows)
                {
                    deslon f2 = new deslon();
                    f2.Show();
                    this.Hide();
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
        private string GetMd5Hash(string iValue)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(iValue));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains("+"))
            {
                textBox1.Text = textBox1.Text.Replace("+", "");

            }
            textBox1.SelectionStart = textBox1.Text.Length;
            char[] wrd = new char[textBox1.Text.Length];
            wrd[0] = textBox1.Text[0];
            textBox1.Text = textBox1.Text.Replace(wrd[0], '7');
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://deslon.ru/reg");
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_MouseEnter(object sender, EventArgs e)
        { 
            button1.ForeColor = Color.White;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Black;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
