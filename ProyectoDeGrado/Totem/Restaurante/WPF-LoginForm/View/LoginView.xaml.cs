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
using System.Windows.Shapes;

namespace WPF_LoginForm.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {


            if (txtUser.Text == "totem" && txtPassword.Text == "totem")
            {
                var mainView = new ClienteMainView();
                this.Close();
                mainView.Show();


            }else if(txtUser.Text == "cajero" && txtPassword.Text == "cajero") 
            {
                var mainView = new CajeroMainView();
                this.Close();
                mainView.Show();


            }else if (txtUser.Text == "cheff" && txtPassword.Text == "cheff") {
                var mainView = new CheffMainView();
                this.Close();
                mainView.Show();
            }
            else
            {
                MessageBox.Show("Contraseña o Usuario no existe");
                txtUser.Text = "";
                txtPassword.Text = "";
            }
           
           
        }
    }
}
