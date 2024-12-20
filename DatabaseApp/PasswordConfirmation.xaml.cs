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
    /// Interaction logic for PasswordConfirmation.xaml
    /// </summary>
    public partial class PasswordConfirmation : Window
    {
        public static string match;
        public PasswordConfirmation(Window parentWindow)
        {
            InitializeComponent();
            Owner = parentWindow;
            string accountName = Login.passText;
            txtName.Text = $"re-enter password for {accountName}";
        }
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        
        //Submit button
        private void btnPassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                string userName = Login.passText;
                String query = "SELECT COUNT(1) FROM AccountTest WHERE Username=@Username AND Password=@Password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Username", userName);
                cmd.Parameters.AddWithValue("@Password", tbPassword.Password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (tbPassword.Password == "")
                {
                    System.Windows.MessageBox.Show("Please enter your password");
                }
                else if (count == 1)
                {
                    match = tbPassword.Password;
                    Opacity = 0.2;
                    //new PasswordChange().Show();
                    PasswordChange passwordChange = new PasswordChange(this);
                    passwordChange.ShowDialog();
                    Opacity = 1;
                    this.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show("Password is incorrect", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                    tbPassword.Password = "";
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                con.Close();
            }
            finally
            {
                con.Close();
            }
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
