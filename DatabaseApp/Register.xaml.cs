using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
            btnRegister.IsEnabled = false;
            btnRegister.Foreground = Brushes.Black;
        }

        //Move the form
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        //Close the form
        private void txtClose_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        //Register a new account
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();

            tbPassword.Text = pbPassword.Password;
            tbCPassword.Text = pbCPassword.Password;
            
            if (tbEmail.Text == "" || tbUsername.Text == "" || pbPassword.Password == "" || pbCPassword.Password == "")
            {
                System.Windows.MessageBox.Show("all fields need to be filled", "Registration Failed", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            else if (tbUsername.Text == "admin")
            {
                System.Windows.MessageBox.Show("admin can not be use as Username", "Registration Failed", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            else if(pbPassword.Password == pbCPassword.Password)
            {
                con.Open();
                String query = "INSERT INTO Account VALUES ('" + tbEmail.Text + "','" + tbUsername.Text + "', '" + pbPassword.Password + "', '"+ "Lv1" + "', '"+ null + "', '"+ "unknown" + "')";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();

                /*tbEmail.Text = "";
                tbUsername.Text = "";
                pbPassword.Password = "";
                pbCPassword.Password = "";*/

                System.Windows.MessageBox.Show("Your account has been successfully created", "Registration Success", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
                new Login().Show();
                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("Passwords not match, try again", "Registration Failed", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                pbPassword.Password = "";
                pbCPassword.Password = "";
            }
        }

        //Go to Login form
        private void txtRegister_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            this.Close();
        }

        //Show/Hide password
        private void passwordCb_Click(object sender, RoutedEventArgs e)
        {
            if (passwordCb.IsChecked == true)
            {
                if (pbPassword.Password == "")
                {
                    System.Windows.MessageBox.Show("Only check this box if you already write down your password!!!");
                    passwordCb.IsChecked = false;
                }
                tbPassword.Text = pbPassword.Password;
                pbPassword.Visibility = Visibility.Hidden;

                tbCPassword.Text = pbCPassword.Password;
                pbCPassword.Visibility = Visibility.Hidden;
            }
            else
            {
                pbPassword.Password = tbPassword.Text;
                pbPassword.Visibility = Visibility.Visible;

                pbCPassword.Password = tbCPassword.Text;
                pbCPassword.Visibility = Visibility.Visible;
            }
        }

        //Tos Check
        private void tosCb_Click(object sender, RoutedEventArgs e)
        {
            if(tosCb.IsChecked == true)
            {
                btnRegister.IsEnabled = true;
                btnRegister.Foreground = Brushes.White;
            }
            else
            {
                btnRegister.IsEnabled = false;
                btnRegister.Foreground = Brushes.Black;
            }
        }

        //Open Tos form
        private void txtTos_Click(object sender, RoutedEventArgs e)
        {
            new Error().Show();
        }

    }
}
