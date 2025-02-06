using DatabaseApp.Data;
using DatabaseApp.View.UserControls;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;
using System.Data;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public static bool isRun;
        public static bool isDone;
        private static readonly object syncLock = new object();

        public static double price1; // = 75.84;
        public static double price2; // = 26.87;
        public static double price3; // = 70.78;
        public Home()
        {
            InitializeComponent();
            ListStatus();
            string accountName = Login.passText;
            
            txtTitle1.Text = "Tea";
            txtTitle2.Text = "Kiwi";
            txtTitle3.Text = "Wine";

            LoadProduct(txtTitle1.Text);
            txtPrice1.Text = ProductList.productPrice + " $";
            LoadProduct(txtTitle2.Text);
            txtPrice2.Text = ProductList.productPrice + " $";
            LoadProduct(txtTitle3.Text);
            txtPrice3.Text = ProductList.productPrice + " $";

            if(Login.passText != "Guest account")
            {
                Rfooter.Visibility = Visibility.Visible;
                btnList.Visibility = Visibility.Visible;
                btnAccount.Visibility = Visibility.Visible;
            }
        }
        DataTableCollection tableCollection;
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        public void ListStatus()
        {
            lock (syncLock)
            {
                if (!isDone)
                {
                    var db = new CustomerListDb();
                    ProductListUser.list = db.customerList.ToList();

                    if (ProductListUser.list.Count != 0)
                    {
                        isRun = false;
                    }
                    else
                    {
                        isRun = true;
                    }
                    isDone = true;
                }
            }
        }

        public void LoadProduct(string Title)
        {
            SqlCommand cmd = new SqlCommand("Select * from ProductLists where Product = @Product", con);
            cmd.Parameters.AddWithValue("@Product", Title);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                ProductList.productID = da.GetValue(0).ToString();
                ProductList.productName = da.GetValue(1).ToString();
                ProductList.productPrice = da.GetValue(4).ToString();
                ProductList.productAmount = da.GetValue(5).ToString();
                ProductList.productStatus = da.GetValue(6).ToString();
            }
            con.Close();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new ProductList().Show();
            this.Close();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            new Home().Show();
            this.Close();
        }

        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            if (Login.passText == "Guest account")
            {
                System.Windows.MessageBox.Show("Needed an account", "error");
                var result = System.Windows.MessageBox.Show("Create an account?", "suggestion", MessageBoxButton.YesNo, (MessageBoxImage)MessageBoxIcon.Information);
                if (result == MessageBoxResult.Yes)
                {
                    new Register().Show();
                    this.Close();
                }
            }
            else
            {
                new ProductListUser().Show();
                this.Close();
            }
        }

        public void ButtonPress()
        {
            new ProductList().Show();
            this.Close();
        }

        private void btnAccount_Click(object sender, RoutedEventArgs e)
        {
            new Account().Show();
            this.Close();
        }

        private void btnAdd1_Click(object sender, RoutedEventArgs e)
        {
            LoadProduct(txtTitle1.Text);
            ButtonPress();
        }

        private void btnAdd2_Click(object sender, RoutedEventArgs e)
        {
            LoadProduct(txtTitle2.Text);
            ButtonPress();
        }

        private void btnAdd3_Click(object sender, RoutedEventArgs e)
        {
            LoadProduct(txtTitle3.Text);
            ButtonPress();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            ProductList.index = null;
            ProductList.productName = ProductList.productPrice = ProductList.productAmount = ProductList.productStatus = "";
            new ProductList().Show();
            this.Close();
        }
    }
}
