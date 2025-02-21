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
using System.Windows.Forms;
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
        public double notify;
        public string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        //--Account--/
        public string a2;
        public string a3;
        public string a6;
        public string a7;
        //--Account--/

        public AccountUpdate(Window parentWindow)
        {
            Owner = parentWindow;
            InitializeComponent();
            lbUpdate.Content = $"{Login.passText} information";

            con.Open();
            tbUsername.Text = Login.passText;
            SqlCommand cmd = new SqlCommand("Select * from AccountTest where AccountID = @AccountID", con);
            cmd.Parameters.AddWithValue("@AccountID", Login.GetID);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                tbUsername.Text = a2 = da.GetValue(2).ToString();
                tbEmail.Text = a3 = da.GetValue(3).ToString();
                tbRole.Text = da.GetValue(5).ToString();
                tbPhoneNumber.Text = a6 = da.GetValue(6).ToString();
                cbGender.Text = a7 = da.GetValue(7).ToString();
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

        void NotifyCount()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from AccountLinked where Username = @username", con);
            cmd.Parameters.AddWithValue("@username", Login.passText);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                notify = Convert.ToDouble(da.GetValue(6)) + 1;
            }
            con.Close();

            con.Open();
            cmd = new SqlCommand("Update AccountLinked Set NotifyCount = @NotifyCount Where Username = @Username", con);
            cmd.Parameters.AddWithValue("@NotifyCount", notify);
            cmd.Parameters.AddWithValue("@Username", Login.passText);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //Save account's updated information
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text == "" || tbUsername.Text == "")
            {
                System.Windows.MessageBox.Show("Email and Username field can not be blank","Error");
            }
            else
            {
                if(tbUsername.Text == a2 && tbEmail.Text == a3 && tbPhoneNumber.Text == a6 && cbGender.Text == a7 )
                {
                    System.Windows.MessageBox.Show("No change was made", "Notification");
                    this.Close();
                }
                else
                {
                    var result = System.Windows.MessageBox.Show($"Change the account information?", "Notification", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                    if (result == MessageBoxResult.Yes)
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

                        if (tbUsername.Text != Login.passText)
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

                            tableName = "CustomerListFinal";
                            NameChange();

                            tableName = "CustomerPreOrder";
                            NameChange();

                            Login.passText = tbUsername.Text;
                        }

                        con.Open();
                        cmd = new SqlCommand($"INSERT INTO AccountNotify VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + "Account information changed" + "','" + $"Account {Login.GetID} information changed" + "','" + "Data modified" + "','" + "none" + "', '" + "Complete" + "', '" + currentdatetime + "')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        NotifyCount();

                        System.Windows.MessageBox.Show("Account information updated", "Success");
                        this.Close();
                    }                  
                }
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
