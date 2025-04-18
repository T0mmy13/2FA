using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Cryptography;

namespace CodePass
{
    public partial class Form1 : Form
    {
        private Timer codeTimer;
        private Timer progressBarTimer;
        private Panel progressBarPanel;
        private Label codeLabel;
        private Random random;
        private int progressValue;
        private const int ProgressBarWidth = 250;

        public Form1()
        {
            InitializeComponent();
            random = new Random();
            InitializeUIComponents();
            InitializeTimers();
            GenerateAndDisplayCode();

            this.FormClosing += new FormClosingEventHandler(OnFormClosing);
        }

        private void InitializeUIComponents()
        {

            progressBarPanel = new Panel();
            progressBarPanel.Size = new Size(0, 30);
            progressBarPanel.Location = new Point((this.ClientSize.Width - ProgressBarWidth) / 2, 200);
            progressBarPanel.BackColor = Color.Green;
            this.Controls.Add(progressBarPanel);

            codeLabel = new Label();
            codeLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            codeLabel.AutoSize = true;
            codeLabel.Location = new Point((this.ClientSize.Width - codeLabel.Width) / 2, 150);
            this.Controls.Add(codeLabel);
        }

        private void InitializeTimers()
        {
            codeTimer = new Timer();
            codeTimer.Interval = 15000;
            codeTimer.Tick += new EventHandler(OnTimerTick);
            codeTimer.Start();

            progressBarTimer = new Timer();
            progressBarTimer.Interval = 100; 
            progressBarTimer.Tick += new EventHandler(OnProgressBarTick);
            progressBarTimer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            GenerateAndDisplayCode();
            progressValue = 0;
            progressBarPanel.Size = new Size(0, 30);
            progressBarPanel.BackColor = Color.Green;
        }

        private void OnProgressBarTick(object sender, EventArgs e)
        {
            if (progressValue < 125)
            {
                progressValue++;
                progressBarPanel.Size = new Size((ProgressBarWidth / 125) * progressValue, 30);
                int green = Convert.ToInt32(255 * (1 - (double)progressValue / 125));
                int red = Convert.ToInt32(255 * ((double)progressValue / 125));
                progressBarPanel.BackColor = Color.FromArgb(red, green, 0);
            }
        }

        private void GenerateAndDisplayCode()
        {
            string newCode = GenerateRandomCode(6);
            string salt;
            string hash = HashCode(newCode, out salt);

            // Сохраняем соль и хеш вместо чистого кода
            File.WriteAllText("Code.txt", $"{salt}|{hash}");

            codeLabel.Text = $"Код: {newCode}";
            codeLabel.Location = new Point((this.ClientSize.Width - codeLabel.Width) / 2, 150);
        }
        private string HashCode(string code, out string salt, int iterations = 10000)
        {
            byte[] saltBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            salt = Convert.ToBase64String(saltBytes);

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                code,
                saltBytes,
                iterations,
                HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(hashBytes);
            }
        }

        private string GenerateRandomCode(int length)
        {
            const string chars = "ACDEFGHJKLMNPQRTUVWXYZ234679";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText("Code.txt", string.Empty);
        }
    }
}