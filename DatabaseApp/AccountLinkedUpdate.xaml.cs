using DatabaseApp.Data.DataModels;
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
    /// Interaction logic for AccountLinkedUpdate.xaml
    /// </summary>
    public partial class AccountLinkedUpdate : Window
    {
        public AccountLinkedUpdate(Window parentWindow)
        {
            Owner = parentWindow;
            InitializeComponent();
            lbLinked.Content = $"Update {Login.passText} Linked accounts";

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from AccountLinked where Username = @username", con);
            cmd.Parameters.AddWithValue("@username", Login.passText);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                tbApple.Text = da.GetValue(2).ToString();
                tbFacebook.Text = da.GetValue(3).ToString();
                tbTwitter.Text = da.GetValue(4).ToString();
                tbGithub.Text = da.GetValue(5).ToString();
            }
            con.Close();

        }
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        public void ActivityLog()
        {
            con.Open();
            string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "INSERT INTO ActivityLog VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + "Account linked modified" + "', '" + "Account modified" + "', '" + currentdatetime + "')"; ;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Update AccountLinked Set Apple = @Apple, Facebook = @Facebook, Twitter = @Twitter, Github = @Github  Where Username = @Username", con);
            cmd.Parameters.AddWithValue("@Apple", tbApple.Text);
            cmd.Parameters.AddWithValue("@Facebook", tbFacebook.Text);
            cmd.Parameters.AddWithValue("@Twitter", tbTwitter.Text);
            cmd.Parameters.AddWithValue("@Github", tbGithub.Text);
            cmd.Parameters.AddWithValue("@Username", Login.passText);
            cmd.ExecuteNonQuery();
            con.Close();

            ActivityLog();
            System.Windows.MessageBox.Show("Account linked information updated", "Success");
            this.Close();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbApple.Clear();
            tbFacebook.Clear();
            tbTwitter.Clear();
            tbGithub.Clear();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
