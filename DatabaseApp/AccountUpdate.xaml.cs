using DocumentFormat.OpenXml.Office.Word;
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
        public static string query;
        public static string tableName;

        public AccountUpdate(Window parentWindow)
        {
            Owner = parentWindow;
            InitializeComponent();
            lbUpdate.Content = $"Update {Login.passText} information";

            con.Open();
            tbUsername.Text = Login.passText;
            SqlCommand cmd = new SqlCommand("Select * from AccountTest where AccountID = @AccountID", con);
            cmd.Parameters.AddWithValue("@AccountID", Login.GetID);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                tbUsername.Text = da.GetValue(2).ToString();
                tbEmail.Text = da.GetValue(3).ToString();
                tbRole.Text = da.GetValue(5).ToString();
                tbPhoneNumber.Text = da.GetInt32(6).ToString();
                cbGender.Text = da.GetValue(7).ToString();
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

        public void ActivityLog()
        {
            con.Open();
            string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            query = "INSERT INTO ActivityLog VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + "Account information modified" + "', '" + "Account modified" + "', '" + currentdatetime + "')"; ;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void NameChange()
        {
            con.Open();
            query = $"Update {tableName} Set Username = @Username Where AccountID = @AccountID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", tbUsername.Text);
            cmd.Parameters.AddWithValue("@AccountID", Login.GetID);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //Save account's updated information
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text == "" || tbUsername.Text == "")
            {
                MessageBox.Show("Email and Username field can not be blank","Error");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update AccountTest Set Username = @Username, Email = @Email, Role = @Role, PhoneNumbers = @PhoneNumbers, Gender = @Gender Where AccountID = @AccountID", con);
                cmd.Parameters.AddWithValue("@AccountID", Login.GetID);
                cmd.Parameters.AddWithValue("@Email", tbEmail.Text);
                cmd.Parameters.AddWithValue("@Username", tbUsername.Text);
                cmd.Parameters.AddWithValue("@Role", tbRole.Text);
                cmd.Parameters.AddWithValue("@PhoneNumbers", tbPhoneNumber.Text);
                cmd.Parameters.AddWithValue("@Gender", cbGender.Text);
                cmd.ExecuteNonQuery();
                con.Close();

                ActivityLog();

                if(tbUsername.Text != Login.passText)
                {
                    con.Open();
                    string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string query = "INSERT INTO ActivityLog VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + $"Account name changed to {tbUsername.Text}" + "', '" + "Account modified" + "', '" + currentdatetime + "')"; ;
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    tableName = "AccountLinked";
                    NameChange();

                    tableName = "CustomerList";
                    NameChange();

                    Login.passText = tbUsername.Text;
                }
                else { }
                MessageBox.Show("Account information updated", "Success");
                this.Close();
            }
            con.Close();
        }

        //Clear textbox content
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbPhoneNumber.Clear();
            cbGender.Text = "";
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

        private void tbPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(tbPhoneNumber.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }
    }
}
