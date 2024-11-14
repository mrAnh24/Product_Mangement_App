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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DatabaseApp.View.UserControls
{
    /// <summary>
    /// Interaction logic for MenuBar.xaml
    /// </summary>
    //Application.Current.Shutdown();
    public partial class MenuBar : UserControl
    {
        public static int count;
        public static string role;

        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        public MenuBar()
        {
            InitializeComponent();
            string accountName = Login.passText;
            txtAccname.Text = accountName;

            //Read role
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Account where Username = @Username", con);
            cmd.Parameters.AddWithValue("@Username", accountName);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                role = da.GetValue(3).ToString();
            }

            //Assign role
            if(role !="admin")
            {
                mAdmin.Visibility = Visibility.Collapsed;
                if(role != "Lv4")
                {
                    miUpdateData.Visibility= Visibility.Collapsed;
                    if (role != "Lv3")
                    {
                        miUpdateProducts.Visibility= Visibility.Collapsed;
                        if (role != "Lv2")
                        {
                            miCSV.Visibility= Visibility.Collapsed;
                            miPermisson.Visibility= Visibility.Collapsed;
                            if (role != "Lv1")
                            {
                                mEdit.Visibility= Visibility.Collapsed;
                                mAccount.Visibility = Visibility.Collapsed;
                                LogOut.Header = "Exit";
                            }
                        }
                    }
                }
            }
            con.Close();

        }
        
        public class formList
        {

        }

        //--File Tab--//

        // Home page 
        private void miHome_Click(object sender, RoutedEventArgs e)
        {
            new Home().Show();
            Application.Current.Windows[count].Close();
            count++;
        }

        //Product
        private void miProduct_Click(object sender, RoutedEventArgs e)
        {
            new ProductList().Show();
            Application.Current.Windows[count].Close();
            count++;
        }

        // Log out 
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.MainWindow.Hide();
            new Login().Show();
            Application.Current.Windows[count].Close();
            //MessageBox.Show("Number of times" + count);
            count++;
        }

        //--Account Tab--//

        //User Account 
        private void miAccount_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.MainWindow.Hide();
            new Account().Show();
            Application.Current.Windows[count].Close();
            //MessageBox.Show("Number of times" + count);
            count++;
        }

        // Product List 
        private void miProductList_Click(object sender, RoutedEventArgs e)
        {
            new ProductListUser().Show();
            Application.Current.Windows[count].Close();
            count++;
        }

        //Permission
        private void miPermisson_Click(object sender, RoutedEventArgs e)
        {
            new Permission().Show();
            Application.Current.Windows[count].Close();
            count++;
        }

        //--Edit Tab--//

        //Open CSV file 
        private void miCSV_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.MainWindow.Hide();
            new MainWindow().Show();
            Application.Current.Windows[count].Close();
            //MessageBox.Show("Number of times" + count);
            count++;
        }

        //Update products
        private void miUpdateProducts_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.MainWindow.Hide();
            new Update().Show();
            Application.Current.Windows[count].Close();
            //MessageBox.Show("Number of times" + count);
            count++;
        }

        //Update customers data 
        private void miUpdateData_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.MainWindow.Hide();
            new ExcelToSql().Show();
            Application.Current.Windows[count].Close();
            //MessageBox.Show("Number of times" + count);
            count++;
        }

        //Export data
        private void ExportData_Click(object sender, RoutedEventArgs e)
        {

        }

        //--Admin Tab--//

        // Account manage 
        private void miDatabase_Click(object sender, RoutedEventArgs e)
        {
            new AccountManagement().Show();
            Application.Current.Windows[count].Close();
            count++;
        }
    }
}
