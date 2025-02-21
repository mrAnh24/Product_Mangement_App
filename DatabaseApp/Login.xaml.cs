using DatabaseApp.Data;
using DatabaseApp.Data.DataModels;
using DatabaseApp.View.UserControls;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Office.Word;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using static DatabaseApp.dbdemoDataSet;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public static string passText;
        public static string GetUsername;
        public static string GetRole;
        public static string GetID;

        public Login()
        {
            InitializeComponent();
            //GetRole();
            //HomeAdmin.productNumbers = 0;
            //HomeAdmin.customerNumbers = 0;
            //HomeAdmin.isRun = false;
        }

        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        public void Role()
        {
            con.Open();
            String query = "SELECT * FROM AccountTest WHERE Username=@Username";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", tbUsername.Text);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                GetID = da.GetValue(1).ToString();
                GetRole = da.GetValue(5).ToString();
            }
            con.Close();
        }

        public void ActivityLog()
        {
            con.Open();
            string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query;
            if(passText == "Guest account")
            {
                query = "INSERT INTO ActivityLog VALUES ('" + "Guest" + "','" + "Guest" + "','" + "Guest" + "','" + "Guest visit" + "', '" + "Notification" + "', '" + currentdatetime + "')";
            }
            else
            {
                query = "INSERT INTO ActivityLog VALUES ('" + GetID + "','" + passText + "','" + GetRole + "','" + "Account login" + "', '" + "Notification" + "', '" + currentdatetime + "')";
            }
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //Login button
        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            tbPassword.Text = pbPassword.Password;
            try
            {
                if(con.State == System.Data.ConnectionState.Closed)
                con.Open();
                String query = "SELECT COUNT(1) FROM AccountTest WHERE Username=@Username AND Password=@Password";
                //string query2 = "Select Role from Account where Username = @Username";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Username", tbUsername.Text);
                cmd.Parameters.AddWithValue("@Password", pbPassword.Password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                if (tbUsername.Text == "" || pbPassword.Password == "")
                {
                    System.Windows.MessageBox.Show("Username or Password fields are empty", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                }
                else if (count == 1)
                {
                    passText = tbUsername.Text;
                    Role();
                    if (GetRole == "admin" || GetRole == "Lv4")
                    {
                        ActivityLog();
                        new HomeAdmin().Show();
                        this.Close();
                    }
                    else if (GetRole != "Guest account")
                    {
                        ActivityLog();
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
                    System.Windows.MessageBox.Show("Password field empty");
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
            ActivityLog();
            new Home().Show();
            this.Close();
        }
    }
}
