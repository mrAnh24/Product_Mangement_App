using DatabaseApp.Data;
using DatabaseApp.Data.DataModels;
using DatabaseApp.View.UserControls;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Z.Dapper.Plus;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for ProductListUser.xaml
    /// </summary>
    /// 

    public partial class ProductListUser : Window
    {
        string connectionString = "Data Source=OS-GPCP-GPDN171\\MSSQLSERVER01;Initial catalog=dbdemo;Persist Security info=True;Encrypt=false;;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true";
        public static double total;
        public static double number;
        public static double number2;
        public static int itemsCount = 0;
        public ProductListUser()
        {
            InitializeComponent();
            GetProducts();
            txtList.Text = $"{Login.passText} list of products";
            //DataContext = this;
            List<double> amount = ProductList.finalAmount;
            
            //total amounts
            foreach(double Item in amount)
            {
                total += Item;
            }
            txtTotal.Text = total + " $";

            //total numbers
            List<double> totals = ProductList.finalNumber;
            foreach (double Item in totals)
            {
                number += Item;
            }
            txtAmount.Text = number.ToString();
            number2 = number;
            txtItemCount.Text = itemsCount.ToString();

            ProductList.finalAmount.Clear();
            ProductList.finalNumber.Clear();

            //foreach (var item in ProductList.list)
            //{
            //    Name = ProductList.list[0].Product,
            //    Id = ProductList.list[0].ProductCode,
            //    Price = ProductList.list[0].Price
            //}

            //this.DataContext = _product;
            //this.DataContext = this;

        }

        void GetProducts()
        {
            dgList.ItemsSource = ProductList.list;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            new ProductList().Show();
            this.Close();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show("All product will be remove from the list, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if(ProductList.list.Count != 0)
                {
                    total = 0;
                    number = 0;
                    itemsCount = 0;
                    txtAmount.Text = total.ToString();
                    txtTotal.Text = total + " $";
                    txtItemCount.Text = "0";

                    ProductList.finalNumber.Clear();
                    ProductList.finalAmount.Clear();
                    ProductList.list.Clear();
                    dgList.Items.Refresh();
                    //dgList.Items.Clear();
                }
                else
                {
                    System.Windows.MessageBox.Show("Your list is empty");
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            new ProductListUser().Show();
            this.Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //DapperPlusManager.Entity<CustomerList>().Table("CustomerList");
            //List<CustomerList> list = dgList.ItemsSource as List<CustomerList>;
            //if (list != null)
            //{
            //    using (IDbConnection db = new SqlConnection(connectionString))
            //    {
            //        db.BulkInsert(list);
            //        System.Windows.MessageBox.Show("List saved","info");
            //    }
            //}
            if (ProductList.list.Count != 0)
            {
                var result = System.Windows.MessageBox.Show("Your list can't not be change after this, continue?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    new Checkout().Show();
                    this.Close();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Empty list","Error");
            }
        }
    }
}
