using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using FilePath = System.IO.Path;

namespace Demo
{
    /// <summary>
    /// Логика взаимодействия для CPWindow.xaml
    /// </summary>
    public partial class CPWindow : Window
    {
        private const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private Random random = new Random();
        private string captchaCode;

        public CPWindow()
        {
            InitializeComponent();

        }

        private string GenerateRandomCode(int length)
        {
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void GenerateCaptchaImage()
        {
            using (Bitmap image = new Bitmap(250, 100))
            {
                using (Graphics g = Graphics.FromImage(image))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Clear(System.Drawing.Color.White);

                    using (Font font1 = new Font("Arial", 25, System.Drawing.FontStyle.Bold))
                    using (Font font2 = new Font("Times New Roman", 30, System.Drawing.FontStyle.Bold))
                    using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black, 2))
                    {
                        g.DrawString(captchaCode[0].ToString(), font1, System.Drawing.Brushes.Aqua, 10, 20);
                        g.DrawString(captchaCode[1].ToString(), font2, System.Drawing.Brushes.Aqua, 40, 20);
                        g.DrawString(captchaCode[2].ToString(), font1, System.Drawing.Brushes.Aqua, 80, 20);
                        g.DrawString(captchaCode[3].ToString(), font2, System.Drawing.Brushes.Aqua, 110, 20);
                        g.DrawString(captchaCode[4].ToString(), font1, System.Drawing.Brushes.Aqua, 140, 20);
                        g.DrawString(captchaCode[5].ToString(), font2, System.Drawing.Brushes.Aqua, 170, 20);

                        g.DrawLine(pen, 10, 50, 190, 50);
                    }
                }

                string imagePath = FilePath.Combine(FilePath.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "img.png");
                image.Save(imagePath, ImageFormat.Png);
                using (Stream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    captcha.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                }
            }
        }

        private void RefreshCaptcha(object sender, RoutedEventArgs e)
        {
            captchaCode = GenerateRandomCode(6);
            GenerateCaptchaImage();
        }

        private void CheckCaptcha_Click(object sender, RoutedEventArgs e)
        {
            string userInput = captchaInput.Text;
            if (userInput.ToLower() == captchaCode.ToLower())
            {
                ProductListWindow cw = new ProductListWindow();
                cw.Show();
                this.Close();
                MessageBox.Show("Капча пройдена!");
                RefreshCaptcha(sender, e);
            }
            else
            {
                MessageBox.Show("Неправильный код с изображения. Попробуйте еще раз.");
                Thread.Sleep(1000);
                RefreshCaptcha(sender, e);
            }
        }
    }
}
