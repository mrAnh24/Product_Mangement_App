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

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        public Update()
        {
            InitializeComponent();
            LoadGrid();         
        }

        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        
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
            con.Open();

            if (tbProductId.Text != "")
            {
                SqlCommand cmd = new SqlCommand("Select Product, Description, Price from Products where ProductCode = @ProductCode", con);
                cmd.Parameters.AddWithValue("@ProductCode", tbProductId.Text);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    tbProduct.Text = da.GetValue(0).ToString();
                    tbDescription.Text = da.GetValue(1).ToString();
                    tbPrice.Text = da.GetValue(2).ToString();
                }
            }
            con.Close();
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
            LoadGrid();
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
