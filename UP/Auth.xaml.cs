using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace UP
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public Auth()
        {
            InitializeComponent();
            Login.FontSize = Login.Height - 10;
            Password.FontSize = Password.Height - 10;
        }

        private void Btn_auth_Click(object sender, RoutedEventArgs e)
        {
            bool b = false;
            var aut = BussEntities.GetContext().Users.ToList();
            for (int i = 0; i < aut.Count; i++)
            {
                if (aut[i].Login.ToString() == Login.Text && aut[i].Password.ToString() == Password.Password)
                {
                    AuthStatus.InAuth = true;
                    AuthStatus.Login = Login.Text;
                    AuthStatus.Status = aut[i].Type;
                    b = true;
                    FrameManager.MainFrame.Navigate(new Empty());
                    break;
                }
            }
            if (b == false)
            {
                MessageBox.Show("Ошибка авторизации!");
            }

        }
    }
}
