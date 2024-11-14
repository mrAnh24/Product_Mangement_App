using DatabaseApp.Data;
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

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        List<Products> products = new List<Products>();
        public static List<Products> list = new List<Products>();
        public static List<double> finalAmount = new List<double>();
        public static string index;
        public static int count;
        public static int selectedIndex;
        public static string detail;
        public static double totals;

        public ProductList()
        {
            InitializeComponent();
            GetProducts();
            //LoadGrid();
            count = dgProduct.Items.Count;
            selectedIndex = dgProduct.SelectedIndex;
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
            //MessageBox.Show("Successfully added to list", "Information");
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
                    else if (tbNumber.Text == "0" || tbNumber.Text == "00")
                    //else if (mon == 0 || mon == 00)
                    {
                        System.Windows.MessageBox.Show("Enter amount > 0", "error");
                    }
                    else
                    {
                        //Read chosen Product price
                        double mon = double.Parse(tbNumber.Text);
                        System.Windows.MessageBox.Show($"{mon} {txtProduct.Content} added to list", "Confirmations");
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
                        finalAmount.Add(totals);
                        tbNumber.Text = "0";

                        //products.Add(txtProduct.Content, txtProductCode.Content, txtDescription.Content, txtPrice.ToString());

                        //DataTable dt = tableCollection[dgProduct.SelectedItem.ToString()];
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    Products current = new Products();
                        //    current.Product = dt.Rows[i]["Product"].ToString();
                        //    current.ProductCode = dt.Rows[i]["ProductCode"].ToString();
                        //    current.Description = dt.Rows[i]["Description"].ToString();
                        //    current.Price = Convert.ToDouble(dt.Rows[i]["Price"].ToString());
                        //    products.Add(current);
                        //}
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Choose an product first", "error");
                }             
            }
        }

        //Search for a product
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            dgProduct.ItemsSource = dt.DefaultView;
            LoadGrid();
            try
            {
                DataView dv = dgProduct.ItemsSource as DataView;
                if (dv != null)
                {
                    dv.RowFilter = "Product LIKE '%" + tbSearch.Text + "%'"; 
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
            SqlCommand cmd = new SqlCommand("Select * from Products where Product = @Product", con);
            cmd.Parameters.AddWithValue("@Product", tbSearch.Text);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                txtProduct.Content = da.GetValue(0).ToString();
                txtProductCode.Content = da.GetValue(1).ToString();
                txtDescription.Content = da.GetValue(2).ToString();
                txtPrice.Content = da.GetValue(3).ToString();
            }
            con.Close();

            System.Windows.MessageBox.Show("Reload to avoid error!", "Warning", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
        }

        //Refresh this application
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            new ProductList().Show();
            this.Close();
        }

        //Condition for only numbers in textbox
        private void tbNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(tbNumber.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }
    }

}
