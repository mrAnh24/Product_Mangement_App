using DatabaseApp.Data.DataModels;
using DatabaseApp.View.UserControls;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for Permission.xaml
    /// </summary>
    public partial class Permission : Window
    {
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

        //Role permission (Incomplete)
        public void RolePermission()
        {
            //ForestGreen
            //if (txtRole.Text != "admin")
            //{
            //    boxAccountManager.Background = Brushes.Red;
            //    if (txtRole.Text != "Lv4")
            //    {
            //        boxCustomerData.Background = Brushes.Red;
            //        if (txtRole.Text != "Lv3")
            //        {
            //            boxUpdateProducts.Background = Brushes.Red;
            //            if (txtRole.Text != "Lv2")
            //            {
            //                boxPermission.Background = Brushes.Red;
            //                boxOpenExcelFile.Background = Brushes.Red;
            //                if (txtRole.Text != "Lv1")
            //                {
            //                    boxEditAccount.Background = Brushes.Red;
            //                    boxUserList.Background = Brushes.Red;
            //                    boxProductList.Background = Brushes.Red;
            //                }
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    boxAccountManager.Background = Brushes.Red;
            //    boxCustomerData.Background = Brushes.Red;
            //    boxUpdateProducts.Background = Brushes.Red;
            //    boxOpenExcelFile.Background = Brushes.Red;
            //    boxPermission.Background = Brushes.Red;
            //    boxEditAccount.Background = Brushes.Red;
            //    boxUserList.Background = Brushes.Red;
            //    boxProductList.Background = Brushes.Red;
            //}
        }

        //Count the numbers of current Role
        public void RoleCount()
        {
            listOfRole.Clear();
            SqlCommand cmd = new SqlCommand("Select * from Account", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                List<string> addRole = new List<string>();
                string role = dr.GetValue(3).ToString();
                if (role == txtRole.Text)
                {
                    listOfRole.Add(role);
                }
            }
            con.Close();
        }

        //Role details (Incomplete)
        public void RoleDetails()
        {
            
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
                    txtCurrentRoleNumber.Visibility = Visibility.Visible;
                    lbCurrentRoleNumber.Visibility = Visibility.Visible;
                    txtCurrentRoleNumber.Text = listOfRole.Count().ToString();
                    boxAccountManager.Background = Brushes.Red;
                    boxAccountRequest.Background = Brushes.Red;
                    boxCustomerData.Background = Brushes.Red;
                    boxUpdateProducts.Background = Brushes.Red;
                    boxOpenExcelFile.Background = Brushes.Red;
                    boxPermission.Background = Brushes.Red;
                    boxEditAccount.Background = Brushes.ForestGreen;
                    boxUserList.Background = Brushes.ForestGreen;
                    boxProductList.Background = Brushes.ForestGreen;
                    txtDetail.Text = "- Have an account with personal info. \n- Can add products to personal list.";
                    break;
                case "2":
                    txtRole.Text = "Lv2";
                    RoleCount();
                    txtCurrentRoleNumber.Text = listOfRole.Count().ToString();
                    boxAccountManager.Background = Brushes.Red;
                    boxAccountRequest.Background = Brushes.Red;
                    boxCustomerData.Background = Brushes.Yellow;
                    boxUpdateProducts.Background = Brushes.Red;
                    boxOpenExcelFile.Background = Brushes.ForestGreen;
                    boxPermission.Background = Brushes.ForestGreen;
                    boxEditAccount.Background = Brushes.ForestGreen;
                    boxUserList.Background = Brushes.ForestGreen;
                    boxProductList.Background = Brushes.ForestGreen;
                    txtDetail.Text = "- Gain access to all of Lv1 permission. \n- Access to Permission page." +
                                     "\n- Can upload and open an excel file. \n- Can send request to update customers list.";
                    break;
                case "3":
                    txtRole.Text = "Lv3";
                    RoleCount();
                    txtCurrentRoleNumber.Text = listOfRole.Count().ToString();
                    boxAccountManager.Background = Brushes.Red;
                    boxAccountRequest.Background = Brushes.Red;
                    boxCustomerData.Background = Brushes.Yellow;
                    boxUpdateProducts.Background = Brushes.ForestGreen;
                    boxOpenExcelFile.Background = Brushes.ForestGreen;
                    boxPermission.Background = Brushes.ForestGreen;
                    boxEditAccount.Background = Brushes.ForestGreen;
                    boxUserList.Background = Brushes.ForestGreen;
                    boxProductList.Background = Brushes.ForestGreen;
                    txtDetail.Text = "- Gain access to all of Lv2 permission. \n- Full access to products data.";
                    break;
                case "4":
                    txtRole.Text = "Lv4";
                    RoleCount();
                    txtCurrentRoleNumber.Text = listOfRole.Count().ToString();
                    boxAccountManager.Background = Brushes.Red;
                    boxAccountRequest.Background = Brushes.Yellow;
                    boxCustomerData.Background = Brushes.ForestGreen;
                    boxUpdateProducts.Background = Brushes.ForestGreen;
                    boxOpenExcelFile.Background = Brushes.ForestGreen;
                    boxPermission.Background = Brushes.ForestGreen;
                    boxEditAccount.Background = Brushes.ForestGreen;
                    boxUserList.Background = Brushes.ForestGreen;
                    boxProductList.Background = Brushes.ForestGreen;
                    txtDetail.Text = "- Gain access to all of Lv3 permission. \n- Can send request to update account list." +
                                     "\n- Can check the request sent to admin.";
                    break;
                case "5":
                    txtRole.Text = "admin";
                    RoleCount();
                    txtCurrentRoleNumber.Text = listOfRole.Count().ToString();
                    boxAccountManager.Background = Brushes.ForestGreen;
                    boxAccountRequest.Background = Brushes.ForestGreen;
                    boxCustomerData.Background = Brushes.ForestGreen;
                    boxUpdateProducts.Background = Brushes.ForestGreen;
                    boxOpenExcelFile.Background = Brushes.ForestGreen;
                    boxPermission.Background = Brushes.ForestGreen;
                    boxEditAccount.Background = Brushes.Yellow;
                    boxUserList.Background = Brushes.ForestGreen;
                    boxProductList.Background = Brushes.ForestGreen;
                    txtDetail.Text = "- Get Full access to all pages and functions. \n- Can only change specific admin info." +
                                     "\n- Manage all accounts. \n- Check and approve requests.";
                    break;
                default:
                    txtRole.Text = "Guest";
                    txtLabel.Text = "Account for";
                    lbCurrentRoleNumber.Visibility = Visibility.Collapsed;
                    txtCurrentRoleNumber.Visibility = Visibility.Collapsed;
                    boxAccountManager.Background = Brushes.Red;
                    boxAccountRequest.Background = Brushes.Red;
                    boxCustomerData.Background = Brushes.Red;
                    boxUpdateProducts.Background = Brushes.Red;
                    boxOpenExcelFile.Background = Brushes.Red;
                    boxPermission.Background = Brushes.Red;
                    boxEditAccount.Background = Brushes.Red;
                    boxUserList.Background = Brushes.Red;
                    boxProductList.Background = Brushes.Yellow;
                    txtDetail.Text = "- Access to home page. \n- See the products. (but can't put on list) " +
                                     "\n- Access to a special footer.";
                    break;
            }
        }

        //Request upgrade link
        private void hlGuest_Click(object sender, RoutedEventArgs e)
        {
            new Error().Show();

        }
    }
}
