using DatabaseApp.Data;
using System;
using DatabaseApp.Data.DataModels;
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
using System.Data;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for AccountRequests.xaml
    /// </summary>
    public partial class AccountRequests : Window
    {
        List<Activity> activities = new List<Activity>();
        public AccountRequests()
        {
            InitializeComponent();
            //ActivityLog();
            LoadData();
        }
        DataTableCollection tableCollection;
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbSearch.Clear();
        }

        public void ActivityLog()
        {
            var db = new ActivityDb();
            activities = db.activities.ToList();
            dgActivity.ItemsSource = activities;
            tbSearch.Text = dgActivity.Items.Count.ToString();
        }

        void LoadData()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM ActivityLog", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgActivity.ItemsSource = dt.DefaultView;
            tbSearch.Text = $"Number of activities: {dgActivity.Items.Count}";
        }

        private void btnFilter1_Click(object sender, RoutedEventArgs e)
        {
            if (GridCol1.Background == Brushes.ForestGreen)
            {
                GridCol1.Background = Brushes.Red;
            }
            else
            {
                GridCol1.Background = Brushes.ForestGreen;
            }
        }

        private void btnFilter2_Click(object sender, RoutedEventArgs e)
        {
            if (GridCol2.Background == Brushes.ForestGreen)
            {
                GridCol2.Background = Brushes.Red;
            }
            else
            {
                GridCol2.Background = Brushes.ForestGreen;
            }
        }

        private void btnFilter3_Click(object sender, RoutedEventArgs e)
        {
            if (GridCol3.Background == Brushes.ForestGreen)
            {
                GridCol3.Background = Brushes.Red;
            }
            else
            {
                GridCol3.Background = Brushes.ForestGreen;
            }
        }
    }
}
