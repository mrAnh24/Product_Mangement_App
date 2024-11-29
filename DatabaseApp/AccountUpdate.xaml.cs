using Microsoft.VisualBasic.ApplicationServices;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for AccountUpdate.xaml
    /// </summary>
    public partial class AccountUpdate : Window
    {
        public AccountUpdate(Window parentWindow)
        {
            Owner = parentWindow;
            InitializeComponent();
            con.Open();          
                tbUsername.Text = Login.passText;
                SqlCommand cmd = new SqlCommand("Select * from Account where Username = @username", con);
                cmd.Parameters.AddWithValue("@username", tbUsername.Text);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    tbEmail.Text = da.GetValue(0).ToString();
                    tbUsername.Text = da.GetValue(1).ToString();
                    tbRole.Text = da.GetValue(3).ToString();
                    tbPhoneNumber.Text = da.GetInt32(4).ToString();
                    tbGender.Text = da.GetValue(5).ToString();
                }
            if (tbUsername.Text == "admin")
            {
                tbEmail.IsEnabled = false;
                tbUsername.IsEnabled = false;
                btnCancel.Content = "Close";
            }
                con.Close();
        }

        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        
        //Save account's updated information
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Update Account Set Email = @Email, Role = @Role, PhoneNumbers = @PhoneNumbers, Gender = @Gender  Where Username = @Username", con);
            if (tbEmail.Text == "" || tbUsername.Text == "")
            {
                System.Windows.MessageBox.Show("Email and Username field can not be blank","Error");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Email", tbEmail.Text);
                cmd.Parameters.AddWithValue("@Username", tbUsername.Text);
                cmd.Parameters.AddWithValue("@Role", tbRole.Text);
                cmd.Parameters.AddWithValue("@PhoneNumbers", tbPhoneNumber.Text);
                cmd.Parameters.AddWithValue("@Gender", tbGender.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                System.Windows.MessageBox.Show("Account information updated", "Success");
                this.Close();
            }
            con.Close();
        }

        //Clear textbox content
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbPhoneNumber.Clear();
            tbGender.Clear();
            if (tbUsername.Text != "admin")
            {
                tbEmail.Clear();
                tbUsername.Clear();
            }
        }

        //Exit the window
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
