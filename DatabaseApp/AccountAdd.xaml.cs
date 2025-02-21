using DatabaseApp.Data.DataModels;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    /// Interaction logic for AccountAdd.xaml
    /// </summary>
    public partial class AccountAdd : Window
    {
        public static string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public static string NewID;
        public AccountAdd(Window parentWindow)
        {
            Owner = parentWindow;
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        public void ActivityLog()
        {
            con.Open();
            string query = "INSERT INTO ActivityLog VALUES ('" + NewID + "','" + tbUsername.Text + "','" + cbRole.Text + "','" + "Account created" + "', '" + "Admin action" + "', '" + currentdatetime + "')"; ;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void AccountLinked()
        {
            con.Open();
            String query = "INSERT INTO AccountLinked VALUES ('" + NewID + "','" + tbUsername.Text + "', '" + null + "', '" + null + "', '" + null + "', '" + null + "', '" + 1 + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void AccountNotify()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"INSERT INTO AccountNotify VALUES ('" + NewID + "','" + tbUsername.Text + "','" + cbRole.Text + "','" + $"Welcome user {tbUsername.Text}" + "','" + $"Account {NewID} created" + "','" + "Data modified" + "','" + "admin" + "', '" + "Complete" + "', '" + currentdatetime + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //Add new Account
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            tbPassword.Text = pbPassword.Password;

            try
            {
                if (tbEmail.Text == "" || tbUsername.Text == "" || pbPassword.Password == "" || cbRole.Text == "" || tbPhoneNumber.Text == "" || cbGender.Text == "")
                {
                    System.Windows.MessageBox.Show("All field have to be filled", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                }
                else if (tbUsername.Text == "admin")
                {
                    System.Windows.MessageBox.Show("'admin' can't be set as an username ", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                }
                else
                {
                    con.Open();
                    String query = "INSERT INTO AccountTest VALUES ('" + tbUsername.Text + "','" + tbEmail.Text + "', '" + pbPassword.Password + "', '" + cbRole.Text + "', '" + tbPhoneNumber.Text + "', '" + cbGender.Text + "', '" + currentdatetime + "')";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    cmd = new SqlCommand("Select * from AccountTest where Username = @Username", con);
                    cmd.Parameters.AddWithValue("@Username", tbUsername.Text);
                    SqlDataReader da = cmd.ExecuteReader();
                    while (da.Read())
                    {
                        NewID = da.GetValue(1).ToString();
                    }
                    con.Close();

                    AccountLinked();
                    ActivityLog();
                    AccountNotify();
                    System.Windows.MessageBox.Show("new account added", "Registration Success", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                con.Close();
            }           
        }

        //Clear all textbox
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbEmail.Clear();
            tbUsername.Clear();
            pbPassword.Clear();
            tbPassword.Clear();
            cbRole.Text = "";
            tbPhoneNumber.Clear();
            cbGender.Text = "";
        }

        //Exit this form
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Action after exit form
        private void Window_Closed(object sender, EventArgs e)
        {
            AccountManagement.index = null;
        }

        //Show/Hide password
        private void cbPassword_Click(object sender, RoutedEventArgs e)
        {
            if (cbPassword.IsChecked == true)
            {
                if (pbPassword.Password == "")
                {
                    System.Windows.MessageBox.Show("password field is blank", "Warning", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                    cbPassword.IsChecked = false;
                }
                else
                {
                    cbPassword.Content = "Password (show)";
                    tbPassword.Text = pbPassword.Password;
                    pbPassword.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                cbPassword.Content = "Password";
                pbPassword.Password = tbPassword.Text;
                pbPassword.Visibility = Visibility.Visible;
            }
        }

        private void tbPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(tbPhoneNumber.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }

        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            tbEmail.Text = "email@gmail.com";
            tbUsername.Text = "test69";
            pbPassword.Password = "1111";
            cbRole.Text = "Lv1";
            tbPhoneNumber.Text = "0943452753";
            cbGender.Text = "unknown";
        }
    }
}
