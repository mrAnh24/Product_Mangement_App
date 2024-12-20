﻿using DatabaseApp.Data;
using DatabaseApp.Data.DataModels;
using DatabaseApp.Logic;
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
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Office.Word;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        //ProductList lists
        List<Products> products = new List<Products>();       
        public static List<double> finalAmount = new List<double>(); //totals price
        public static List<double> finalNumber = new List<double>(); //totals number of products

        //ProductList values
        public static string index; //placeholder for productCode
        public static int count; // total number of products in grid
        public static int selectedIndex;
        public static string detail;
        public static double totals; //price
        public static double amounts; //number of products
        public static string placeholder; // temp for a for product

        public static string orderId;
        public static string query;
        public static string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        //Home values
        public static string homeName = "";
        public static string homeCode = "";
        public static string homeDescription = "";
        public static string homePrice = "";

        public ProductList()
        {
            InitializeComponent();
            GetProducts();
            //LoadGrid();
            count = dgProduct.Items.Count;
            selectedIndex = dgProduct.SelectedIndex;

            txtProduct.Content = homeName;
            txtProductCode.Content = homeCode;
            txtDescription.Content = homeDescription;
            txtPrice.Content = homePrice;
        }
        DataTableCollection tableCollection;
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        //Load products
        void GetProducts()
        {
            var db = new ProductDb();
            products = db.Products.ToList();
            dgProduct.ItemsSource = products;
            txtTotal.Text = $"Number of products: {dgProduct.Items.Count}";
        }

        //Load products (for search only)
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("Select * from Products", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgProduct.ItemsSource = dt.DefaultView;
            txtTotal.Text = $"Number of products: {dgProduct.Items.Count}";
        }

        public void ProductAdd()
        {          
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void ReadProduct()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from CustomerList where AccountID = @AccountID", con);
            cmd.Parameters.AddWithValue("@AccountID", Login.GetID);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                orderId = da.GetValue(1).ToString();
            }
            con.Close();
        }

        //Choose a row
        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            var row = sender as DataGridRow;
            var select = row.DataContext as Products;
            index = select.ProductCode;

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Products where ProductCode = @ProductCode", con);
            cmd.Parameters.AddWithValue("@ProductCode", index);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                txtProduct.Content = da.GetValue(0).ToString();
                txtProductCode.Content = da.GetValue(1).ToString();
                txtDescription.Content = da.GetValue(2).ToString();
                txtPrice.Content = da.GetValue(3).ToString();
                detail = txtProduct.Content.ToString();
            }
            con.Close();
        }

        //Show product detail
        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            Opacity = 0.2;
            ProductDetail productDetail = new ProductDetail(this);
            productDetail.ShowDialog();
            Opacity = 1;
        }

        //Add product to personal list
        private void btnAdd_Click(object sender, RoutedEventArgs e)
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
                if (txtProduct.Content != "")
                {
                    if (tbNumber.Text == "")
                    {
                        System.Windows.MessageBox.Show("Amount can not be blank", "error");
                    }
                    else if (tbNumber.Text == "0" || tbNumber.Text == "00" || tbNumber.Text.Contains("."))
                    {
                        System.Windows.MessageBox.Show("Enter amount > 0", "error");
                    }
                    else
                    {
                        //Read chosen Product price
                        double mon = double.Parse(tbNumber.Text);
                        System.Windows.MessageBox.Show($"{mon} {txtProduct.Content} added to list", "Notify");
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Select * from Products where ProductCode = @ProductCode", con);
                        cmd.Parameters.AddWithValue("@ProductCode", txtProduct.Content);
                        SqlDataReader da = cmd.ExecuteReader();
                        while (da.Read())
                        {
                            txtProduct.Content = da.GetValue(0).ToString();
                            txtProductCode.Content = da.GetValue(1).ToString();
                            txtDescription.Content = da.GetValue(2).ToString();
                            txtPrice.Content = da.GetValue(3).ToString();
                        }
                        con.Close();

                        //Get the total amount
                        double tue = double.Parse((string)txtPrice.Content);
                        totals = mon * tue;
                        amounts = mon;
                        finalAmount.Add(totals);
                        finalNumber.Add(amounts);
                        tbNumber.Text = "0";

                        con.Open();
                        query = "INSERT INTO CustomerList VALUES ('" + Login.GetID + "','" + Login.passText + "','" + txtProduct.Content + "','" + txtProductCode.Content + "', '" + txtPrice.Content + "', '" + amounts + "', '" + currentdatetime + "')";
                        ProductAdd();

                        ReadProduct();
                        con.Open();
                        query = $"INSERT INTO CustomerListFinal VALUES ('" + orderId + "','" + Login.GetID + "','" + Login.passText + "','" + null + "','" + txtProduct.Content + "','" + txtProductCode.Content + "', '" + txtPrice.Content + "', '" + amounts + "', '" + currentdatetime + "')";
                        ProductAdd();

                        ProductListUser.itemsCount += 1;
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Choose an product first", "error");
                }             
            }
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbFilter.SelectedIndex.ToString())
            {
                case "0":
                    cbFilter.Text = "Name";
                    break;
                case "1":
                    cbFilter.Text = "Code";
                    break;
                case "2":
                    cbFilter.Text = "Price";
                    break;
            }
        }

        //Search for a product
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            string type = "";
            dgProduct.ItemsSource = dt.DefaultView;
            LoadGrid();
            try
            {
                DataView dv = dgProduct.ItemsSource as DataView;
                if (dv != null)
                {
                    //dv.RowFilter = "Product LIKE '%" + tbSearch.Text + "%'";
                    txtNotify1.Visibility = Visibility.Collapsed;
                    txtNotify2.Visibility = Visibility.Collapsed;
                    if (cbFilter.SelectedIndex == 0)
                    {
                        type = "Product";
                        dv.RowFilter = $"{type} LIKE '%{tbSearch.Text}%'";
                        if (dgProduct.Items.Count == 0)
                        {
                            System.Windows.MessageBox.Show("No items found", "Error");
                            txtNotify1.Visibility = Visibility.Collapsed;
                            txtNotify2.Visibility = Visibility.Visible;
                        }
                        else if (dgProduct.Items.Count > 1 && dgProduct.Items.Count != count)
                        {
                            System.Windows.MessageBox.Show("Multiple items found", "Information");
                            txtNotify2.Visibility = Visibility.Collapsed;
                            txtNotify1.Visibility = Visibility.Visible;
                        }
                        else if (tbSearch.Text == "" && dgProduct.Items.Count == count)
                        {
                            Clear();
                            GetProducts();
                        }
                    }
                    else if (cbFilter.SelectedIndex == 1)
                    {
                        type = "ProductCode";
                        dv.RowFilter = $"{type} LIKE '%{tbSearch.Text}%'";
                        if (dgProduct.Items.Count == 0)
                        {
                            System.Windows.MessageBox.Show("No items found", "Error");
                            txtNotify1.Visibility = Visibility.Collapsed;
                            txtNotify2.Visibility = Visibility.Visible;
                        }
                        else if (dgProduct.Items.Count > 1 && dgProduct.Items.Count != count)
                        {
                            System.Windows.MessageBox.Show("Multiple items found", "Information");
                            txtNotify2.Visibility = Visibility.Collapsed;
                            txtNotify1.Visibility = Visibility.Visible;
                        }
                        else if (tbSearch.Text == "" && dgProduct.Items.Count == count)
                        {
                            Clear();
                            GetProducts();
                        }
                    }
                    else if (cbFilter.SelectedIndex == 2)
                    {
                        type = "Price";
                        dv.RowFilter = $"CONVERT({type}, System.String) LIKE '%{tbSearch.Text}%'";
                        if (dgProduct.Items.Count == 0)
                        {
                            System.Windows.MessageBox.Show("No items found", "Error");
                            txtNotify1.Visibility = Visibility.Collapsed;
                            txtNotify2.Visibility = Visibility.Visible;
                        }
                        else if (dgProduct.Items.Count > 1 && dgProduct.Items.Count != count)
                        {
                            System.Windows.MessageBox.Show("Multiple items found", "Information");
                            txtNotify2.Visibility = Visibility.Collapsed;
                            txtNotify1.Visibility = Visibility.Visible;
                        }
                        else if (tbSearch.Text == "" && dgProduct.Items.Count == count)
                        {
                            Clear();
                            GetProducts();
                        }
                    }
                    else
                    {
                        Clear();
                        System.Windows.MessageBox.Show("Select a filter", "Error");
                    }
                }
                else
                {
                    dt = new DataTable();
                    dgProduct.ItemsSource = dt.DefaultView;
                    GetProducts();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            txtTotal.Text = $"Number of products: {dgProduct.Items.Count}";

            con.Open();
            SqlCommand cmd = new SqlCommand();
            switch (type)
            {
                case "Product":
                    cmd = new SqlCommand("Select * from Products where Product = @Product", con);
                    cmd.Parameters.AddWithValue("@Product", tbSearch.Text);
                    break;
                case "ProductCode":
                    cmd = new SqlCommand("Select * from Products where ProductCode = @ProductCode", con);
                    cmd.Parameters.AddWithValue("@ProductCode", tbSearch.Text);
                    break;
                case "Price":
                    cmd = new SqlCommand("Select * from Products where Price = @Price", con);
                    cmd.Parameters.AddWithValue("@Price", tbSearch.Text);
                    break;
                default:
                    cmd = new SqlCommand("Select * from Products where Product = @Product", con);
                    cmd.Parameters.AddWithValue("@Product", "");
                    GetProducts();
                    break;
            }
            //SqlCommand cmd = new SqlCommand("Select * from Products where Product = @Product", con);
            //cmd.Parameters.AddWithValue("@Product", tbSearch.Text);

            if (cbFilter.Items != null)
            {
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    txtProduct.Content = da.GetValue(0).ToString();
                    txtProductCode.Content = da.GetValue(1).ToString();
                    txtDescription.Content = da.GetValue(2).ToString();
                    txtPrice.Content = da.GetValue(3).ToString();
                }
                con.Close();
            }
        }

        void Clear()
        {
            txtProduct.Content = "";
            txtProductCode.Content = "";
            txtDescription.Content = "";
            txtPrice.Content = "";
        }

        
        void Refresh()
        {
            new ProductList().Show();
            this.Close();
        }

        //Refresh this application
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        //Condition for only numbers in textbox
        private void tbNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(tbNumber.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }

        private void btnSearchClear_Click(object sender, RoutedEventArgs e)
        {
            tbSearch.Clear();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
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
    }

}
