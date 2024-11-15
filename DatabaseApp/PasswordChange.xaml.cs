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
    /// Interaction logic for PasswordChange.xaml
    /// </summary>
    public partial class PasswordChange : Window
    {
        public PasswordChange(Window ParrentWindow)
        {
            InitializeComponent();
            Owner = ParrentWindow;
            string user = Login.passText;
            txtName.Text = $"Change password for {user}";
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //DragMove();
        }

        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            string oldPass = PasswordConfirmation.match;
            string user = Login.passText;
            tbOldPassword.Text = pbOldPassword.Password;
            tbNewPassword.Text = pbNewPassword.Password;
            tbConfirmedPassword.Text = pbConfirmedPassword.Password;

            if (pbOldPassword.Password == "" || pbNewPassword.Password == "" || pbConfirmedPassword.Password == "")
            {
                System.Windows.MessageBox.Show("Please fill in all the fields!!!", "Update Failed", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            else if (pbOldPassword.Password == oldPass)
            {
                if(pbOldPassword.Password != pbNewPassword.Password || pbOldPassword.Password != pbConfirmedPassword.Password)
                {
                    if (pbNewPassword.Password == pbConfirmedPassword.Password)
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE Account SET Password = @Password WHERE Username = @Username", con);
                        cmd.Parameters.AddWithValue("@Username", user);
                        cmd.Parameters.AddWithValue("@Password", pbNewPassword.Password);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        System.Windows.MessageBox.Show("Your Password has been successfully updated!", "Update Success", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("new Passwords not match, check if Caps lock is on!", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                        pbNewPassword.Password = "";
                        pbConfirmedPassword.Password = "";
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Old password can not be the same as new password!!!", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                    pbNewPassword.Password = "";
                    pbConfirmedPassword.Password = "";
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Incorrect old password!!!", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void passwordCb_Click(object sender, RoutedEventArgs e)
        {
            if (passwordCb.IsChecked == true)
            {
                if (pbOldPassword.Password == "" || pbNewPassword.Password == "" || pbConfirmedPassword.Password == "")
                {
                    System.Windows.MessageBox.Show("Write down your password before you check to avoid error", "Warning", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                    passwordCb.IsChecked = false;
                }
                else
                {
                    tbOldPassword.Text = pbOldPassword.Password;
                    pbOldPassword.Visibility = Visibility.Hidden;

                    tbNewPassword.Text = pbNewPassword.Password;
                    pbNewPassword.Visibility = Visibility.Hidden;

                    tbConfirmedPassword.Text = pbConfirmedPassword.Password;
                    pbConfirmedPassword.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                pbOldPassword.Password = tbOldPassword.Text;
                pbOldPassword.Visibility = Visibility.Visible;

                pbNewPassword.Password = tbNewPassword.Text;
                pbNewPassword.Visibility = Visibility.Visible;

                pbConfirmedPassword.Password = tbConfirmedPassword.Text;
                pbConfirmedPassword.Visibility = Visibility.Visible;

            }
        }
    }
}
