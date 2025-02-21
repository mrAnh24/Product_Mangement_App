using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office.Word;
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
using static ClosedXML.Excel.XLPredefinedFormat;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public static string GetID;
        public static string GetName;
        public static string GetRole;
        public static string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public Register()
        {
            InitializeComponent();
            btnRegister.IsEnabled = false;
            btnRegister.Foreground = Brushes.Black;
        }
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

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

        public void ActivityLog()
        {
            con.Open();
            String query = "INSERT INTO ActivityLog VALUES ('" + GetID + "','" + GetName + "','" + GetRole + "','" + "Account created" + "', '" + "Notification" + "', '" + currentdatetime + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void AccountNotify()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"INSERT INTO AccountNotify VALUES ('" + GetID + "','" + GetName + "','" + GetRole + "','" + $"Welcome user {GetName}" + "','" + $"Account {GetID} created" + "','" + "Data modified" + "','" + "None" + "', '" + "Complete" + "', '" + currentdatetime + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void GetInfo()
        {
            con.Open();
            String query = "SELECT * FROM AccountTest WHERE Username=@Username";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", tbUsername.Text);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                GetID = da.GetValue(1).ToString();
                GetName = da.GetValue(2).ToString();
                GetRole = da.GetValue(5).ToString();
            }
            con.Close();
        }

        public void AccountLinked()
        {
            con.Open();
            String query = "INSERT INTO AccountLinked VALUES ('" + GetID + "','" + tbUsername.Text + "', '" + null + "', '" + null + "', '" + null + "', '" + null + "', '" + 1 + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //Register a new account
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();

            tbPassword.Text = pbPassword.Password;
            tbCPassword.Text = pbCPassword.Password;
            
            if (tbEmail.Text == "" || tbUsername.Text == "" || pbPassword.Password == "" || pbCPassword.Password == "")
            {
                System.Windows.MessageBox.Show("all fields need to be filled", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            else if (tbUsername.Text == "admin")
            {
                System.Windows.MessageBox.Show("admin can not be use as Username", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            else if(pbPassword.Password == pbCPassword.Password)
            {
                String query;
                con.Open();
                //if (tbUsername.Text == "admin")
                //{
                //    query = "INSERT INTO AccountTest VALUES ('" + tbUsername.Text + "','" + tbEmail.Text + "', '" + pbPassword.Password + "', '" + "admin" + "', '" + "094345816" + "', '" + "male" + "', '" + currentdatetime + "')";
                //}
                //else
                //{
                //    query = "INSERT INTO AccountTest VALUES ('" + tbUsername.Text + "','" + tbEmail.Text + "', '" + pbPassword.Password + "', '" + "Lv1" + "', '" + null + "', '" + "unknown" + "', '" + currentdatetime + "')";
                //}
                query = "INSERT INTO AccountTest VALUES ('" + tbUsername.Text + "','" + tbEmail.Text + "', '" + pbPassword.Password + "', '" + "Lv1" + "', '" + null + "', '" + "unknown" + "', '" + currentdatetime + "')";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();

                /*tbEmail.Text = "";
                tbUsername.Text = "";
                pbPassword.Password = "";
                pbCPassword.Password = "";*/
                
                GetInfo();
                AccountLinked();
                ActivityLog();
                AccountNotify();

                System.Windows.MessageBox.Show("Account created successfully", "Registration Success", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
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
                if (pbPassword.Password == "" || pbCPassword.Password == "")
                {
                    System.Windows.MessageBox.Show("Password empty");
                    passwordCb.IsChecked = false;
                }
                else
                {
                    tbPassword.Text = pbPassword.Password;
                    pbPassword.Visibility = Visibility.Hidden;

                    tbCPassword.Text = pbCPassword.Password;
                    pbCPassword.Visibility = Visibility.Hidden;
                }
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
