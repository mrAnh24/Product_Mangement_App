using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using DatabaseApp.Data;
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        List<Products> products = new List<Products>();
        public static string index;
        public static string detail;
        public Update()
        {
            InitializeComponent();
            GetProducts();
            //LoadGrid();
        }

        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        //Load data
        void GetProducts()
        {
            var db = new ProductDb();
            products = db.Products.ToList();
            dgProduct.ItemsSource = products;
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            tbProductId.Text = ".";
            var row = sender as DataGridRow;
            var select = row.DataContext as Products;
            index = select.ProductCode;

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Products where ProductCode = @ProductCode", con);
            cmd.Parameters.AddWithValue("@ProductCode", index);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                tbProduct.Text = da.GetValue(0).ToString();
                tbProductId.Text = da.GetValue(1).ToString();
                tbDescription.Text = da.GetValue(2).ToString();
                tbPrice.Text = da.GetValue(3).ToString();
                //detail = tbProduct.Text.ToString();
            }
            con.Close();
        }

        //Load data
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("Select * from Products", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgProduct.ItemsSource = dt.DefaultView;
        }

        //Auto fill data
        private void tbProductId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbProductId.Text != "" && tbProduct.Text == "")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select Product, Description, Price from Products where ProductCode = @ProductCode", con);
                cmd.Parameters.AddWithValue("@ProductCode", tbProductId.Text);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    tbProduct.Text = da.GetValue(0).ToString();
                    tbDescription.Text = da.GetValue(1).ToString();
                    tbPrice.Text = da.GetValue(2).ToString();
                }
                con.Close();
            }
        }

        private void tbProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbProduct.Text != "" && tbProductId.Text == "")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select ProductCode, Description, Price from Products where Product = @Product", con);
                cmd.Parameters.AddWithValue("@Product", tbProduct.Text);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    tbProductId.Text = da.GetValue(0).ToString();
                    tbDescription.Text = da.GetValue(1).ToString();
                    tbPrice.Text = da.GetValue(2).ToString();
                }
                con.Close();
            }
        }

        //Add new Product
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into Products values (@Product, @ProductCode, @Description, @Price)", con);
            cmd.Parameters.AddWithValue("@ProductCode", tbProductId.Text);
            cmd.Parameters.AddWithValue("@Product", tbProduct.Text);
            cmd.Parameters.AddWithValue("@Description", tbDescription.Text);
            cmd.Parameters.AddWithValue("@Price", tbPrice.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            System.Windows.MessageBox.Show("Successfully Added new Product");
        }

        //Update existing Product
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Update Products Set Product = @Product, Description = @Description, Price = @Price Where ProductCode = @ProductCode", con);
            cmd.Parameters.AddWithValue("@ProductCode", tbProductId.Text);
            cmd.Parameters.AddWithValue("@Product", tbProduct.Text);
            cmd.Parameters.AddWithValue("@Description", tbDescription.Text);
            cmd.Parameters.AddWithValue("@Price", tbPrice.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            System.Windows.MessageBox.Show("Successfully Updated Product");
        }

        //Delete a Product
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show("The product and it's data will be delete, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
            if (result == MessageBoxResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete Products Where ProductCode = @ProductCode", con);
                cmd.Parameters.AddWithValue("@ProductCode", tbProductId.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                System.Windows.MessageBox.Show("Successfully Deleted Product");
            }
        }

        //Refresh Datagrid
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            new Update().Show();
            this.Close();
        }

        //Clear all textbox
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbProductId.Clear();
            tbProduct.Clear();
            tbDescription.Clear();
            tbPrice.Clear();
            //DataTable dt = new DataTable();
            //dgProduct.ItemsSource = dt.DefaultView;
        }
    }
}
