using DatabaseApp.Data;
using DatabaseApp.Data.DataModels;
using DocumentFormat.OpenXml.Office2010.Excel;
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
using MessageBox = System.Windows.MessageBox;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for Announcement.xaml
    /// </summary>
    public partial class Announcement : Window
    {
        List<AccountTest> accountName = new List<AccountTest>();

        public string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string name;
        public double notify;

        public string id;
        public string username;
        public string role;
        public string display;
        public string details;
        public string category;
        public string requestType;

        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        public Announcement()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void cbTarget_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtName.IsEnabled = false;
            btnCheck.IsEnabled = false;
            btnCheck.Foreground = Brushes.Black;
            C1.IsEnabled = false;
            switch (cbTarget.SelectedIndex.ToString())
            {
                case "0":
                    cbTarget.Text = "All";
                    break;
                case "1":
                    cbTarget.Text = "Lv4";
                    break;
                case "2":
                    cbTarget.Text = "Lv2, Lv3";
                    break;
                case "3":
                    cbTarget.Text = "Lv1";
                    break;
                case "4":
                    txtName.IsEnabled = true;
                    btnCheck.IsEnabled = true;
                    C1.IsEnabled = true;
                    btnCheck.Foreground = Brushes.WhiteSmoke;
                    cbTarget.Text = "Specific account";
                    break;
            }
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbRequestType.IsEnabled = false;
            switch (cbCategory.SelectedIndex.ToString())
            {
                case "0":
                    cbRequestType.IsEnabled = true;
                    cbCategory.Text = "Request";
                    break;
                case "1":
                    cbCategory.Text = "Account news";
                    break;
                case "2":
                    cbCategory.Text = "Product news";
                    break;
                case "3":
                    cbCategory.Text = "Other";
                    break;
            }
        }

        private void cbRequestType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbRequestType.SelectedIndex.ToString())
            {
                case "0":
                    cbRequestType.Text = "Account upgrade request";
                    break;
                case "1":
                    cbRequestType.Text = "New product request";
                    break;
                case "2":
                    cbRequestType.Text = "Pre-Order product";
                    break;
            }
        }

        void GetInfo()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * From AccountTest where Username = @Username", con);
            cmd.Parameters.AddWithValue("@Username", username);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                notify = Convert.ToDouble(da.GetValue(6));
                id = da.GetValue(1).ToString();
                role = da.GetValue(5).ToString();
            }
            con.Close();
        }

        void GetNotifyCount()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * From AccountLinked where Username = @Username", con);
            cmd.Parameters.AddWithValue("@Username", name);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                notify = Convert.ToDouble(da.GetValue(6));
            }
            con.Close();
        }

        void NotifyCount()
        {
            foreach (var item in accountName)
            {
                name = item.Username;
                GetNotifyCount();

                con.Open();
                string query = $"Update AccountLinked Set NotifyCount = @NotifyCount Where Username = @Username";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@NotifyCount", notify + 1);
                cmd.Parameters.AddWithValue("@Username", name);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        void Notify()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"INSERT INTO AccountNotify VALUES ('" + id + "','" + username + "','" + role + "','" + display + "','" + details + "','" + category + "','" + requestType + "', '" + "Complete" + "', '" + currentdatetime + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            NotifyCount();
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "")
            {
                txtCheck.Visibility = Visibility.Hidden;
            }
            else
            {
                txtCheck.Visibility = Visibility.Visible;
                var dx = new AccountDb();
                accountName = dx.Accounts.ToList();
                if (accountName.Any(x => x.Username.Contains(txtName.Text))) //Temp name
                {
                    txtCheck.Foreground = Brushes.ForestGreen;
                    txtCheck.Text = "valid name";
                }
                else
                {
                    txtCheck.Foreground = Brushes.Red;
                    txtCheck.Text = "invalid name";
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = txtDetails.Text = 
            cbTarget.Text = cbCategory.Text =
            txtName.Text = cbRequestType.Text = "";
            txtCheck.Visibility = Visibility.Collapsed;
            btnCheck.Foreground = Brushes.Black;
            btnCheck.IsEnabled = false;
        }

        private void btnPost_Click(object sender, RoutedEventArgs e)
        {
            if(txtDisplay.Text == "" || txtDetails.Text == "" 
              || cbTarget.Text == "" || cbCategory.Text == "" )
            {
                MessageBox.Show("Fill all the field to continue", "Error");
            }
            else if(txtCheck.Foreground == Brushes.Red && txtCheck.Visibility == Visibility.Visible)
            {
                MessageBox.Show("Enter a valid name", "Error");
            }
            else if (cbTarget.Text == "Specific account" && txtName.Text == "")
            {
                MessageBox.Show("Enter target name", "Error");
            }
            else if (cbCategory.Text == "Request" && cbRequestType.Text == "")
            {
                MessageBox.Show("Choose a request type", "Error");
            }
            else
            {
                var result = System.Windows.MessageBox.Show("Post this announcement", "Warning", MessageBoxButton.YesNo, (MessageBoxImage)MessageBoxIcon.Information);
                if (result == MessageBoxResult.Yes)
                {
                    id = Login.GetID;
                    username = Login.passText;
                    role = Login.GetRole;
                    display = txtDisplay.Text;
                    details = txtDetails.Text;

                    //Type of announcement
                    requestType = "none";
                    if (cbCategory.Text == "Request")
                    {
                        category = "Data modified";
                        requestType = "admin";
                    }
                    else if (cbCategory.Text == "Account news")
                    {
                        category = "Account news";
                    }
                    else if (cbCategory.Text == "Product news")
                    {
                        category = "Product news";
                    }
                    else if (cbCategory.Text == "Other")
                    {
                        category = "Other";
                    }

                    //Receiver
                    var dx = new AccountDb();
                    accountName = dx.Accounts.ToList();
                    if (cbTarget.Text == "Lv4")
                    {
                        accountName.RemoveAll(x => x.Role != "Lv4");
                    }
                    else if (cbTarget.Text == "Lv2, Lv3")
                    {
                        accountName.RemoveAll(x => x.Role != "Lv2" && x.Role != "Lv3");
                    }
                    else if (cbTarget.Text == "Lv1")
                    {
                        accountName.RemoveAll(x => x.Role != "Lv1");
                    }
                    else if (cbTarget.Text == "Specific account")
                    {
                        accountName.RemoveAll(x => x.Username != txtName.Text);
                        username = txtName.Text;
                        GetInfo();
                    }
                    Notify();

                    MessageBox.Show("Post successfully, Notice");
                    //this.Close();
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show("Announcement haven't been post yet, go back anyway?", "Warning", MessageBoxButton.YesNo, (MessageBoxImage)MessageBoxIcon.Information);
            if (result == MessageBoxResult.Yes)
            {
                new HomeAdmin().Show();
                this.Close();
            }
        }
    }
}
