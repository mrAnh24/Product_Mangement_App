using DatabaseApp.Data;
using DatabaseApp.Data.DataModels;
using DocumentFormat.OpenXml.Office2010.Excel;
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
        public static double total = 0;
        public static double number = 0;
        public static int count = 0;
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
                count++;
            }
            txtAmount.Text = number + " $";
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
                    txtTotal.Text = total + " $";
                    ProductList.finalAmount.Clear();
                    ProductList.list.Clear();
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
        }
    }
}
