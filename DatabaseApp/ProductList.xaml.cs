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
using DocumentFormat.OpenXml.Office.Word;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        //ProductList lists
        List<ProductLists> products = new List<ProductLists>();       
        public static List<double> finalAmount = new List<double>(); //totals price
        public static List<double> finalNumber = new List<double>(); //totals number of products

        //ProductList values
        public static string index; //placeholder for productCode
        public static int count; // total number of products in grid
        public static int selectedIndex;
        public static double totals; //price
        public static double amounts; //number of products
        public static string placeholder; // temp for a for product

        public static string orderId;
        public static string query;
        public static string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        //Product List col
        public static string productID;
        public static string productName;
        public static string productDescription;
        public static string productType;
        public static string productPrice;
        public static string productAmount;
        public static string productStatus;
        public static string ProductCreator;

        public ProductList()
        {
            InitializeComponent();
            GetProducts();
            //LoadGrid();
            txtProduct.Content = productName;
            txtPrice.Content = productPrice;
            txtAmount.Content = productAmount;
            txtStatus.Content = productStatus;

            count = dgProduct.Items.Count;
            selectedIndex = dgProduct.SelectedIndex;
        }
        DataTableCollection tableCollection;
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        //Load products
        void GetProducts()
        {
            var db = new ProductListDb();
            products = db.ProductLists.ToList();
            dgProduct.ItemsSource = products;
            txtTotal.Text = $"Number of products: {dgProduct.Items.Count}";
        }

        //Load products (for search only)
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("Select * from ProductLists", con);
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

        void ReadProductList()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from ProductLists where ProductCode = @ProductCode", con);
            cmd.Parameters.AddWithValue("@ProductCode", index);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                productID = da.GetValue(0).ToString();
                txtProduct.Content = productName = da.GetValue(1).ToString();
                productDescription = da.GetValue(2).ToString();
                productType = da.GetValue(3).ToString();
                txtPrice.Content = productPrice = da.GetValue(4).ToString();
                txtAmount.Content = productAmount = da.GetValue(5).ToString();
                txtStatus.Content = productStatus = da.GetValue(6).ToString();
                ProductCreator = da.GetValue(7).ToString();
            }
            con.Close();
        }

        void ComboBoxDisable()
        {
            cbS1.Visibility = cbS2.Visibility = cbS3.Visibility = cbT1.Visibility
            = cbT2.Visibility = cbT3.Visibility = cbT4.Visibility = cbT5.Visibility 
            = cbT6.Visibility = cbT7.Visibility = cbT8.Visibility = Visibility.Collapsed;
            cbStatus.Text = "";
        }

        void EmptyGrid()
        {
            if(dgProduct.Items.Count == 0)
            {
                txtNotify.Visibility = Visibility.Visible;
            }
            else
            {
                txtNotify.Visibility = Visibility.Collapsed;
            }
        }

        //Choose a row
        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            var row = sender as DataGridRow;
            var select = row.DataContext as ProductLists;
            index = select.ProductCode;

            ReadProductList();
        }

        //Show product detail
        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            if (index == null)
            {
                System.Windows.MessageBox.Show("Choose a product first", "Error");
            }
            else
            {
                Opacity = 0.2;
                ProductDetail productDetail = new ProductDetail(this);
                productDetail.ShowDialog();
                Opacity = 1;
            }
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
                if (index != null)
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
                        ReadProductList();

                        //Get the total amount
                        double tue = double.Parse((string)txtPrice.Content);
                        totals = mon * tue;
                        amounts = mon;
                        finalAmount.Add(totals);
                        finalNumber.Add(amounts);
                        tbNumber.Text = "0";

                        con.Open();
                        query = "INSERT INTO CustomerList VALUES ('" + Login.GetID + "','" + Login.passText + "','" + txtProduct.Content + "','" + productID + "', '" + txtPrice.Content + "', '" + amounts + "', '" + currentdatetime + "')";
                        ProductAdd();

                        ReadProduct();
                        con.Open();
                        query = $"INSERT INTO CustomerListFinal VALUES ('" + orderId + "','" + Login.GetID + "','" + Login.passText + "','" + null + "','" + txtProduct.Content + "','" + productID + "', '" + txtPrice.Content + "', '" + amounts + "', '" + currentdatetime + "')";
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
            GetProducts();
            ComboBoxDisable();
            cbStatus.IsEnabled = false;
            btnSearch.IsEnabled = true;
            btnSearch.Foreground = Brushes.White;
            tbSearch.IsEnabled = true;
            switch (cbFilter.SelectedIndex.ToString())
            {
                case "0":
                    cbFilter.Text = "Name";
                    break;
                case "1":
                    cbFilter.Text = "Type";
                    cbStatus.IsEnabled = true;
                    btnSearch.IsEnabled = false;
                    btnSearch.Foreground = Brushes.Black;
                    tbSearch.IsEnabled = false;
                    cbT1.Visibility = cbT2.Visibility = cbT3.Visibility = cbT4.Visibility 
                    = cbT5.Visibility = cbT6.Visibility = cbT7.Visibility = cbT8.Visibility = Visibility.Visible;
                    break;
                case "2":
                    cbFilter.Text = "Price";
                    break;
                case "3":
                    cbFilter.Text = "Status";
                    cbStatus.IsEnabled = true;
                    btnSearch.IsEnabled = false;
                    btnSearch.Foreground = Brushes.Black;
                    tbSearch.IsEnabled = false;
                    cbS1.Visibility = cbS2.Visibility = cbS3.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void cbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetProducts();
            switch (cbStatus.SelectedIndex.ToString())
            {
                // Status
                case "0":
                    cbStatus.Text = "Available";
                    products.RemoveAll(x => x.Status != "Available");
                    cbStatus.IsEnabled = true;
                    break;
                case "1":
                    cbStatus.Text = "Unavailable";
                    products.RemoveAll(x => x.Status == "Available");
                    products.RemoveAll(x => x.Status == "On sale");
                    cbStatus.IsEnabled = true;
                    break;
                case "2":
                    cbStatus.Text = "On sale";
                    products.RemoveAll(x => x.Status != "On sale");
                    cbStatus.IsEnabled = true;
                    break;

                //Type
                case "3":
                    cbStatus.Text = "Meat";
                    products.RemoveAll(x => x.Type != "Meat");
                    cbStatus.IsEnabled = true;
                    break;
                case "4":
                    cbStatus.Text = "Dairy";
                    products.RemoveAll(x => x.Type != "Dairy");
                    cbStatus.IsEnabled = true;
                    break;
                case "5":
                    cbStatus.Text = "Vegetable";
                    products.RemoveAll(x => x.Type != "Vegetable");
                    cbStatus.IsEnabled = true;
                    break;
                case "6":
                    cbStatus.Text = "Drink";
                    products.RemoveAll(x => x.Type != "Drink");
                    cbStatus.IsEnabled = true;
                    break;
                case "7":
                    cbStatus.Text = "Fruit";
                    products.RemoveAll(x => x.Type != "Fruit");
                    cbStatus.IsEnabled = true;
                    break;
                case "8":
                    cbStatus.Text = "Dessert";
                    products.RemoveAll(x => x.Type != "Dessert");
                    cbStatus.IsEnabled = true;
                    break;
                case "9":
                    cbStatus.Text = "Snack";
                    products.RemoveAll(x => x.Type != "Snack");
                    cbStatus.IsEnabled = true;
                    break;
                case "10":
                    cbStatus.Text = "Other";
                    products.RemoveAll(x => x.Type != "Other");
                    cbStatus.IsEnabled = true;
                    break;
            }
            EmptyGrid();
            txtTotal.Text = $"Number of products: {dgProduct.Items.Count}";
        }

        //Search for a product
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbSearch.Text == "")
                {
                    System.Windows.MessageBox.Show("Search box blank", "Error");
                    GetProducts();
                }
                else
                {
                    if (cbFilter.SelectedIndex == 0)
                    {
                        GetProducts();
                        products.RemoveAll(x => x.Product != tbSearch.Text);
                        txtTotal.Text = $"Number of products: {dgProduct.Items.Count}";
                    }
                    else if (cbFilter.SelectedIndex == 2)
                    {
                        GetProducts();
                        products.RemoveAll(x => x.Price.ToString() != tbSearch.Text);
                        txtTotal.Text = $"Number of products: {dgProduct.Items.Count}";
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Choose a filter", "Error");
                        GetProducts();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            EmptyGrid();
            txtTotal.Text = $"Total accounts: {dgProduct.Items.Count}";
        }

        void Clear()
        {
            txtProduct.Content = "";
            txtPrice.Content = "";
            txtAmount.Content = "";
            txtStatus.Content = "";
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

        void ShowResult()
        {
            dgProduct.Columns[0].Visibility = Visibility.Hidden;
            dgProduct.Columns[1].Visibility = Visibility.Hidden;
            dgProduct.Columns[3].Visibility = Visibility.Hidden;
            dgProduct.Columns[4].Visibility = Visibility.Hidden;
            dgProduct.Columns[5].Visibility = Visibility.Hidden;
            dgProduct.Columns[6].Visibility = Visibility.Hidden;
            dgProduct.Columns[7].Visibility = Visibility.Hidden;
            dgProduct.Columns[8].Visibility = Visibility.Hidden;
            dgProduct.Columns[9].Visibility = Visibility.Hidden;
            dgProduct.Columns[10].Visibility = Visibility.Hidden;

            dgProduct.Columns[2].Header = "Product";

            //products.RemoveAll(x => x. != Login.GetID);
        }

        private void dgProduct_AutoGeneratedColumns(object sender, EventArgs e)
        {
            ShowResult();
        }
    }

}
