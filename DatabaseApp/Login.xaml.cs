using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            //GetRole();
        }

        public static string passText;
        public static string PassRole;

        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        
        //Login button
        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            tbPassword.Text = pbPassword.Password;
            try
            {
                if(con.State == System.Data.ConnectionState.Closed)
                con.Open();
                String query = "SELECT COUNT(1) FROM Account WHERE Username=@Username AND Password=@Password";
                //string query2 = "Select Role from Account where Username = @Username";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Username", tbUsername.Text);
                cmd.Parameters.AddWithValue("@Password", pbPassword.Password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (tbUsername.Text == "" || pbPassword.Password == "")
                {
                    System.Windows.MessageBox.Show("Username or Password fields are empty", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                }
                else if (count == 1)
                {
                    passText = tbUsername.Text;
                    //GetRole();
                    if (tbUsername.Text == "admin")
                    {
                        new AccountManagement().Show();
                        this.Close();
                    }
                    else if (PassRole != "Lv1")
                    {
                        new Home().Show();
                        this.Close();
                    }
                    else
                    {
                        new Error().Show();
                        this.Close();
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Username or Password is incorrect", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                }
            }
            catch
            {

            }
            finally 
            {
                con.Close(); 
            }
        }

        //Move the form
        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        //Close the form
        private void txtClose_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        //Switch to Register form
        private void txtRegister_Click(object sender, RoutedEventArgs e)
        {
            new Register().Show();
            this.Close();
        }

        //Show/Hide password 
        private void passwordCb_Click(object sender, RoutedEventArgs e)
        {
          if(passwordCb.IsChecked == true)
            {
                if (pbPassword.Password == "")
                {
                    System.Windows.MessageBox.Show("Only check this box if you already write down your password!!!");
                    passwordCb.IsChecked = false;
                }
                else
                {
                    tbPassword.Text = pbPassword.Password;
                    pbPassword.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                pbPassword.Password = tbPassword.Text;
                pbPassword.Visibility = Visibility.Visible;
            }
        }

        //Login as guest
        private void btnGuess_Click(object sender, RoutedEventArgs e)
        {
            passText = "Guest account";
            new Home().Show();
            this.Close();
        }
    }
}
