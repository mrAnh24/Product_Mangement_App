﻿using DatabaseApp.Data;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for OrderProductDetail.xaml
    /// </summary>
    public partial class OrderProductDetail : Window
    {
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        List<CustomerListFinal> products = new List<CustomerListFinal>();
        public string chosen;

        public OrderProductDetail(Window parentWindow)
        {
            Owner = parentWindow;
            InitializeComponent();
            GetProducts();
        }

        void GetProducts()
        {
            var db = new CustomerListFinalDb();
            products = db.customerListFinal.ToList();
            dgProduct.ItemsSource = products;
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            var row = sender as DataGridRow;
            var select = row.DataContext as CustomerListFinal;
            chosen = select.ProductCode;
            GetProductInfo();
        }

        void GetProductInfo()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Products where ProductCode = @ProductCode", con);
            cmd.Parameters.AddWithValue("@ProductCode", chosen);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                txtProduct.Text = da.GetValue(0).ToString();
                txtProductCode.Text = da.GetValue(1).ToString();
                txtDescription.Text = da.GetValue(2).ToString();
                txtPrice.Text = da.GetValue(3).ToString();
            }
            con.Close();
        }

        private void dgProduct_AutoGeneratedColumns(object sender, EventArgs e)
        {
            ShowResult();
        }

        void ShowResult()
        {
            dgProduct.Columns[0].Visibility = Visibility.Hidden;
            dgProduct.Columns[1].Visibility = Visibility.Hidden;
            dgProduct.Columns[2].Visibility = Visibility.Hidden;
            dgProduct.Columns[3].Visibility = Visibility.Hidden;
            dgProduct.Columns[5].Header = "PCode";
            dgProduct.Columns[8].Visibility = Visibility.Hidden;

            products.RemoveAll(x => x.CreatedDate != AccountOrder.currentDate);
            txtCount.Text = $"Number of products: {dgProduct.Items.Count}";
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            AccountOrder.index = null;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearchClear_Click(object sender, RoutedEventArgs e)
        {
            tbSearchBox.Clear();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}