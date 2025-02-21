using DatabaseApp.Data.DataModels;
using DatabaseApp.Data;
using DatabaseApp.View.UserControls;
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
using System.Diagnostics;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for AccountNotification.xaml
    /// </summary>
    public partial class AccountNotification : Window
    {
        public double notify;
        public static string index;
        public string details;
        List<AccountNotify> accountNotify = new List<AccountNotify>();
        public string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        public string filter1 = "ON";
        public string filter2 = "ON";
        public string filter3 = "ON";
        public string filter4 = "ON";

        public AccountNotification()
        {
            InitializeComponent();
            GetNotify();

            if (Login.GetRole == "admin" || Login.GetRole == "Lv4")
            {
                txt1.Visibility = Mfilter4.Visibility = Lfilter4.Visibility =
                Rfilter4.Visibility = btnFilter4.Visibility = Visibility.Visible;
            }
        }
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        void GetNotifyCount()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from AccountLinked where Username = @username", con);
            cmd.Parameters.AddWithValue("@username", Login.passText);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                notify = Convert.ToDouble(da.GetValue(6));
            }
            con.Close();
        }

        void Refresh()
        {
            new AccountNotification().Show();
            this.Close();
        }

        void GetNotify()
        {
            var db = new AccountNotifyDb();
            accountNotify = db.accountNotify.ToList();
            dgNotify.ItemsSource = accountNotify;
            Filter();
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            var row = sender as DataGridRow;
            var select = row.DataContext as AccountNotify;
            index = select.NotifyID;
            details = select.Details;
        }

        void Filter()
        {
            if (filter1 == "OFF")
            {
                accountNotify.RemoveAll(x => x.Category == "Data modified");
            }

            if (filter2 == "OFF")
            {
                accountNotify.RemoveAll(x => x.Category == "Product news");
            }

            if (filter3 == "OFF")
            {
                accountNotify.RemoveAll(x => x.Category == "Request");
            }

            if (filter4 == "OFF")
            {
                accountNotify.RemoveAll(x => x.RequestType == "admin");
            }

        }

        //Category filter
        private void btnFilter1_Click(object sender, RoutedEventArgs e)
        {
            if (filter1 == "ON")
            {
                filter1 = "OFF";
                Mfilter1.Fill = Brushes.LightGray;
                Lfilter1.Fill = Brushes.WhiteSmoke;
                Rfilter1.Fill = Brushes.LightGray;
            }
            else
            {
                filter1 = "ON";
                Mfilter1.Fill = Brushes.CornflowerBlue;
                Lfilter1.Fill = Brushes.CornflowerBlue;
                Rfilter1.Fill = Brushes.WhiteSmoke;
            }
            GetNotify();
        }

        private void btnFilter2_Click(object sender, RoutedEventArgs e)
        {
            if (filter2 == "ON")
            {
                filter2 = "OFF";
                Mfilter2.Fill = Brushes.LightGray;
                Lfilter2.Fill = Brushes.WhiteSmoke;
                Rfilter2.Fill = Brushes.LightGray;
            }
            else
            {
                filter2 = "ON";
                Mfilter2.Fill = Brushes.CornflowerBlue;
                Lfilter2.Fill = Brushes.CornflowerBlue;
                Rfilter2.Fill = Brushes.WhiteSmoke;
            }
            GetNotify();
        }

        private void btnFilter3_Click(object sender, RoutedEventArgs e)
        {
            if (filter3 == "ON")
            {
                filter3 = "OFF";
                Mfilter3.Fill = Brushes.LightGray;
                Lfilter3.Fill = Brushes.WhiteSmoke;
                Rfilter3.Fill = Brushes.LightGray;
            }
            else
            {
                filter3 = "ON";
                Mfilter3.Fill = Brushes.CornflowerBlue;
                Lfilter3.Fill = Brushes.CornflowerBlue;
                Rfilter3.Fill = Brushes.WhiteSmoke;
            }
            GetNotify();
        }

        private void btnFilter4_Click(object sender, RoutedEventArgs e)
        {
            if (filter4 == "ON")
            {
                filter4 = "OFF";
                Mfilter4.Fill = Brushes.LightGray;
                Lfilter4.Fill = Brushes.WhiteSmoke;
                Rfilter4.Fill = Brushes.LightGray;
            }
            else
            {
                filter4 = "ON";
                Mfilter4.Fill = Brushes.CornflowerBlue;
                Lfilter4.Fill = Brushes.CornflowerBlue;
                Rfilter4.Fill = Brushes.WhiteSmoke;
            }
            GetNotify();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            GetNotifyCount();
            if(notify != 0)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update AccountLinked Set NotifyCount = @NotifyCount Where Username = @Username", con);
                cmd.Parameters.AddWithValue("@NotifyCount", 0);
                cmd.Parameters.AddWithValue("@Username", Login.passText);
                cmd.ExecuteNonQuery();
                con.Close();
                Refresh();
            }
            else
            {
                MessageBox.Show("All notification read", "Notification");
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void dgNotify_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgNotify.Columns[0].Visibility = Visibility.Hidden;     // No
            dgNotify.Columns[1].Visibility = Visibility.Hidden;     // Notify ID
            dgNotify.Columns[2].Visibility = Visibility.Hidden;     // Account ID
            dgNotify.Columns[3].Visibility = Visibility.Hidden;     // Username
            dgNotify.Columns[4].Visibility = Visibility.Hidden;     // Role
            dgNotify.Columns[6].Visibility = Visibility.Hidden;     // Details
            dgNotify.Columns[7].Visibility = Visibility.Hidden;     // Category
            dgNotify.Columns[8].Visibility = Visibility.Hidden;     // Request type
            dgNotify.Columns[9].Visibility = Visibility.Hidden;     // Status
            //dgNotify.Columns[10].Visibility = Visibility.Hidden;    // Time

            dgNotify.Columns[5].Header = "Notification";            // Detail
            if (Login.passText != "admin")
            {
                accountNotify.RemoveAll(x => x.AccountID != Login.GetID && x.Category == "Data modified");
                accountNotify.RemoveAll(x => x.AccountID != Login.GetID && x.Category == "Request");
                accountNotify.RemoveAll(x => x.RequestType == "admin");
            }
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            if (index != null)
            {
                tbIndex.Text = details;
            }
            else
            {
                MessageBox.Show("Select a notify first","Error");
            }
        }
    }
}
