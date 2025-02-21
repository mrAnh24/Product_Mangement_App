using DatabaseApp.Data;
using DatabaseApp.Data.DataModels;
using DatabaseApp.View.UserControls;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using MessageBox = System.Windows.MessageBox;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for Permission.xaml
    /// </summary>
    public partial class Permission : Window
    {
        public string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public List<string> listOfRole = new List<string>();
        //public static string role;
        public Permission()
        {
            InitializeComponent();
            Role();
            //RolePermission();
            txtRoleNumber.Text = (slideRole.Ticks.Count()-1).ToString();
            if (MenuBar.role == "admin")
            {
                txtRequest.Text = "Highest authority";
                txtRequest.IsEnabled = false;
                txtRequest.Foreground = Brushes.WhiteSmoke;
            }
            else if((MenuBar.role == "Lv4"))
            {
                txtRequest.Text = "Max Lv";
                txtRequest.IsEnabled = false;
                txtRequest.Foreground = Brushes.WhiteSmoke;
            }
        }
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        public void RolePermissionGreen()
        {
            boxAccountManager.Background = Brushes.ForestGreen;
            boxInvoiceManager.Background = Brushes.ForestGreen;
            boxAccountRequest.Background = Brushes.ForestGreen;

            boxUpdateData.Background = Brushes.ForestGreen;
            boxUpdateProducts.Background = Brushes.ForestGreen;
            boxOpenExcelFile.Background = Brushes.ForestGreen;

            boxPermission.Background = Brushes.ForestGreen;
            boxEditAccount.Background = Brushes.ForestGreen;
            boxAccountOrder.Background = Brushes.ForestGreen;
            boxUserList.Background = Brushes.ForestGreen;

            boxProductList.Background = Brushes.ForestGreen;
            boxHome.Background = Brushes.ForestGreen;
        }

        void RolePermissionRed()
        {
            boxAccountManager.Background = Brushes.Red;
            boxInvoiceManager.Background = Brushes.Red;
            boxAccountRequest.Background = Brushes.Red;

            boxUpdateData.Background = Brushes.Red;
            boxUpdateProducts.Background = Brushes.Red;
            boxOpenExcelFile.Background = Brushes.Red;

            boxPermission.Background = Brushes.Red;
            boxAccountOrder.Background = Brushes.Red;
            boxEditAccount.Background = Brushes.Red;
            boxUserList.Background = Brushes.Red;

            boxProductList.Background = Brushes.Red;
        }

        //Count the numbers of current Role
        public void RoleCount()
        {
            listOfRole.Clear();
            SqlCommand cmd = new SqlCommand("Select * from AccountTest", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                List<string> addRole = new List<string>();
                string role = dr.GetValue(5).ToString();
                if (role == txtRole.Text)
                {
                    listOfRole.Add(role);
                }
            }
            con.Close();
        }

        //starting role on slider
        public void Role()
        {
            switch (MenuBar.role.ToString())
            {
                case "Lv1":
                    slideRole.Value = 1;
                    break;
                case "Lv2":
                    slideRole.Value = 2;
                    break;
                case "Lv3":
                    slideRole.Value = 3;
                    break;
                case "Lv4":
                    slideRole.Value = 4;
                    break;
                case "admin":
                    slideRole.Value = 5;
                    break;
                default:
                    slideRole.Value = 0;
                    break;
            }
        }

        //Change values base on slider
        private void slideRole_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //RolePermission();
            switch (slideRole.Value.ToString())
            {              
                case "1":
                    txtRole.Text = "Lv1";
                    txtLabel.Text = "Role:";
                    RoleCount();
                    RolePermissionRed();
                    txtCurrentRoleNumber.Visibility = Visibility.Visible;
                    lbCurrentRoleNumber.Visibility = Visibility.Visible;
                    txtCurrentRoleNumber.Text = listOfRole.Count().ToString();
                    boxEditAccount.Background = Brushes.ForestGreen;
                    boxUserList.Background = Brushes.ForestGreen;
                    boxProductList.Background = Brushes.ForestGreen;
                    boxAccountOrder.Background = Brushes.ForestGreen;
                    txtDetail.Text = "- Have an account with personal info. " +
                                     "\n- Can add products to personal list." +
                                     "\n- Can check list history.";
                    break;
                case "2":
                    txtRole.Text = "Lv2";
                    RoleCount();
                    RolePermissionGreen();
                    txtCurrentRoleNumber.Text = listOfRole.Count().ToString();
                    boxAccountManager.Background = Brushes.Red;
                    boxInvoiceManager.Background = Brushes.Red;
                    boxAccountRequest.Background = Brushes.Red;
                    boxUpdateData.Background = Brushes.Red;
                    boxUpdateProducts.Background = Brushes.Red;
                    txtDetail.Text = "- Gain access to all of Lv1 permission. " +
                                     "\n- Access to Permission page." +
                                     "\n- Can upload and working with a CSV file. ";
                    break;
                case "3":
                    txtRole.Text = "Lv3";
                    RoleCount();
                    RolePermissionGreen();
                    txtCurrentRoleNumber.Text = listOfRole.Count().ToString();
                    boxAccountManager.Background = Brushes.Red;
                    boxInvoiceManager.Background = Brushes.Red;
                    boxAccountRequest.Background = Brushes.Red;
                    boxUpdateData.Background = Brushes.Yellow;
                    txtDetail.Text = "- Gain access to all of Lv2 permission. " +
                                     "\n- Can modified products data. (Update product)" +
                                     "\n- Can work with test data. (Update data)";
                    break;
                case "4":
                    txtRole.Text = "Lv4";
                    RoleCount();
                    RolePermissionGreen();
                    txtCurrentRoleNumber.Text = listOfRole.Count().ToString();
                    boxAccountManager.Background = Brushes.Red;
                    boxAccountRequest.Background = Brushes.Yellow;
                    boxUpdateData.Background = Brushes.Yellow;
                    txtDetail.Text = "- Gain access to all of Lv3 permission. " +
                                     "\n- Special admin home page." +
                                     "\n- Can check and approve account's requests." +
                                     "\n- Can work with customer's invoices.";
                    break;
                case "5":
                    txtRole.Text = "admin";
                    RoleCount();
                    RolePermissionGreen();
                    txtCurrentRoleNumber.Text = listOfRole.Count().ToString();
                    boxEditAccount.Background = Brushes.Yellow;
                    txtDetail.Text = "- Get Full access to all pages and functions. " +
                                     "\n- Can only change specific admin info." +
                                     "\n- Manage all accounts. ";
                    break;
                default:
                    txtRole.Text = "Guest";
                    txtLabel.Text = "Account for";
                    RolePermissionRed();
                    lbCurrentRoleNumber.Visibility = Visibility.Collapsed;
                    txtCurrentRoleNumber.Visibility = Visibility.Collapsed;
                    boxProductList.Background = Brushes.Yellow;
                    txtDetail.Text = "- Access to home page. " +
                                     "\n- See the products. (can't put on list) " +
                                     "\n- Access to a special footer. (login + register recommend)";
                    break;
            }
        }

        //Request upgrade hyperlink
        private void hlGuest_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Request for an account upgrade?", "Confirmation", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
            if (result == MessageBoxResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"INSERT INTO AccountNotify VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + $"Upgrade request sent" + "','" + $"{Login.passText} sent request for an account upgrade" + "','" + "Request" + "','" + "Account upgrade" + "', '" + "Incomplete" + "', '" + currentdatetime + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Upgrade request sent","Information");
            }
        }
    }
}
