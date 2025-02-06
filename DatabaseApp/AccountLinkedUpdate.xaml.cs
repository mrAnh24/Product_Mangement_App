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
using System.Windows.Forms;
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
        //--AccountLinked--/
        public string link1;
        public string link2;
        public string link3;
        public string link4;
        public double notify;
        //--AccountLinked--/
        public string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        public AccountLinkedUpdate(Window parentWindow)
        {
            Owner = parentWindow;
            InitializeComponent();
            lbLinked.Content = $"{Login.passText} Linked accounts";

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from AccountLinked where Username = @username", con);
            cmd.Parameters.AddWithValue("@username", Login.passText);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                tbApple.Text = link1 = da.GetValue(2).ToString();
                tbFacebook.Text = link2 = da.GetValue(3).ToString();
                tbTwitter.Text = link3 = da.GetValue(4).ToString();
                tbGithub.Text = link4 = da.GetValue(5).ToString();
                notify = Convert.ToDouble(da.GetValue(6)) + 1;
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

        public void AccountNotify()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"INSERT INTO AccountNotify VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + "Linked information change" + "','" + $"Account {Login.GetID} linked account modified" + "','" + "Data modified" + "','" + "none" + "', '" + "Complete" + "', '" + currentdatetime + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(tbApple.Text == link1 && tbFacebook.Text == link2 && tbTwitter.Text == link3 && tbGithub.Text == link4)
            {
                System.Windows.MessageBox.Show("No change was made", "Notification");
                this.Close();
            }
            else
            {
                var result = System.Windows.MessageBox.Show("Update linked account?", "Notification", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update AccountLinked Set Apple = @Apple, Facebook = @Facebook, Twitter = @Twitter, Github = @Github, NotifyCount = @NotifyCount  Where Username = @Username", con);
                    cmd.Parameters.AddWithValue("@Apple", tbApple.Text);
                    cmd.Parameters.AddWithValue("@Facebook", tbFacebook.Text);
                    cmd.Parameters.AddWithValue("@Twitter", tbTwitter.Text);
                    cmd.Parameters.AddWithValue("@Github", tbGithub.Text);
                    cmd.Parameters.AddWithValue("@NotifyCount", notify);
                    cmd.Parameters.AddWithValue("@Username", Login.passText);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    ActivityLog();
                    AccountNotify();
                    System.Windows.MessageBox.Show("Account linked information updated", "Success");
                    this.Close();
                }
            }
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
