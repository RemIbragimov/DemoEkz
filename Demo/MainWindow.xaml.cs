using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Demo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TradeEntities db;
        public MainWindow()
        {
            InitializeComponent();
            db = new TradeEntities();
        }

        private void LoginAsGuest_Click(object sender, RoutedEventArgs e)
        {
            CPWindow cw = new CPWindow();
            cw.Show();
            this.Close();
            MessageBox.Show("Вход в качестве гостя выполнен.");
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = db.Users.Where(w => w.UserLogin == txtLogin.Text).First();
                if (user != null && user.UserPassword == txtPassword.Password)
                {
                    CPWindow cw = new CPWindow();
                    cw.Show();
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка в логине или пароле");
            }
        }
    }
}