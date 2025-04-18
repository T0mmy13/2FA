using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MainApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void clickReg(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
        }

        private void clickSign(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
        }

        private void ButtonSign_Click(object sender, EventArgs e)
        {
            string login = LoginTextBox.Text.ToString();
            string password = PasswordTextBox.Text.ToString();
            StreamReader sr = new StreamReader("db.txt");

            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split(' ');
                if (login == line[0] && password == line[1])
                {
                    sr.Close();

                    // Открываем форму для ввода кода
                    using (var codeInputForm = new CodeInputForm())
                    {
                        if (codeInputForm.ShowDialog() == DialogResult.OK)
                        {
                            string userInputCode = codeInputForm.InputCode;

                            string filePath = @"Code.txt";
                            if (File.Exists(filePath))
                            {
                                string storedCode = File.ReadAllText(filePath).Trim();

                                if (userInputCode == storedCode)
                                {
                                    MessageBox.Show("Вход произведён");
                                }
                                else
                                {
                                    MessageBox.Show("Код неверен");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Файл с кодом не найден");
                            }
                        }
                    }

                    return;
                }
            }

            sr.Close();
            MessageBox.Show("Неверный ввод");
        }

        public static bool IsLoginUnique(string login, string filePath = "db.txt")
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл не найден, логин считается уникальным.");
                return true;
            }

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string existingLogin = line.Split(' ')[0];
                if (login == existingLogin)
                {
                    MessageBox.Show("Логин уже существует.");
                    return false;
                }
            }
            return true;
        }

        public static bool IsPasswordStrong(string password)
        {
            if (password.Length < 8)
            {
                MessageBox.Show("Пароль должен быть не менее 8 символов длиной.");
                return false;
            }
            if (!Regex.IsMatch(password, @"\d"))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну цифру.");
                return false;
            }
            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну заглавную букву.");
                return false;
            }
            if (!password.Any(ch => "!@#$%^&*(),.?\":{}|<>".Contains(ch)))
            {
                MessageBox.Show("Пароль должен содержать хотя бы один специальный символ.");
                return false;
            }
            return true;
        }

        private void buttonRegistr_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = textBoxPassword.Text;
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(textBoxNumber.Text))
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                if (IsLoginUnique(login) && IsPasswordStrong(password))
                {
                    File.AppendAllText("db.txt", $"\n{login} {password} {textBoxNumber.Text}");
                    panel1.Visible = true;
                    panel2.Visible = false;
                }
            }
        }
    }
}