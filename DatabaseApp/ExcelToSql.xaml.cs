using ClosedXML.Excel;
using ExcelDataReader;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Z.Dapper.Plus;
using DocumentFormat.OpenXml.Presentation;
using System.Linq;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for ExcelToSql.xaml
    /// </summary>
    public partial class ExcelToSql : Window
    {
        string connectionString = "Data Source=OS-GPCP-GPDN171\\MSSQLSERVER01;Initial catalog=dbdemo;Persist Security info=True;Encrypt=false;;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true";
        public ExcelToSql()
        {  
            InitializeComponent();
        }
        DataTableCollection tableCollection;

        //-- Customers --//

        //Choose a file
        private void btnCustomersUpload_Click(object sender, RoutedEventArgs e)
        {
            tableCollection = null;
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls"})
            {
                if(openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tbCustomersFile.Text = openFileDialog.FileName;
                    using(var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            tableCollection = result.Tables;
                            cbCustomersSheet.Items.Clear();
                            foreach(DataTable table in tableCollection)
                                cbCustomersSheet.Items.Add(table.TableName);
                        }
                    }
                }
            }
        }

        //Import data to server
        private void btnCustomersImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DapperPlusManager.Entity<Customer>().Table("Customers");
                List<Customer> customers = dgCustomers.ItemsSource as List<Customer>;
                if (customers != null)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        var result = System.Windows.MessageBox.Show("This action will overwrite any existing data, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                        if (result == MessageBoxResult.Yes)
                        {
                            db.BulkDelete(customers);
                            db.BulkInsert(customers);
                            System.Windows.MessageBox.Show("Data imported to the server successfully!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }

        //reload window command
        private void Reset()
        {          
            new ExcelToSql().Show();
            this.Close();
        }

        //reload current window
        private void btnCustomersClear_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
        
        //Choose a row
        private void cbCustomersSheet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable dt = tableCollection[cbCustomersSheet.SelectedItem.ToString()];
            List<Customer> customers = new List<Customer>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Customer info = new Customer();
                    info.CustomerID = dt.Rows[i]["CustomerID"].ToString();
                    info.ContactName = dt.Rows[i]["ContactName"].ToString();
                    info.ContactTitle = dt.Rows[i]["ContactTitle"].ToString();
                    info.CompanyName = dt.Rows[i]["CompanyName"].ToString();
                    info.Address = dt.Rows[i]["Address"].ToString();
                    info.City = dt.Rows[i]["City"].ToString();
                    info.Country = dt.Rows[i]["Country"].ToString();
                    info.Phone = dt.Rows[i]["Phone"].ToString();
                    info.Fax = dt.Rows[i]["Fax"].ToString();
                    info.Region = dt.Rows[i]["Region"].ToString();
                    info.PostalCode = dt.Rows[i]["PostalCode"].ToString();
                    customers.Add(info);
                }
                dgCustomers.ItemsSource = customers;
            }
        }

        //Update a customer info
        private void btnCustomersUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DapperPlusManager.Entity<Customer>().Table("Customers");
                List<Customer> customers = dgCustomers.ItemsSource as List<Customer>;
                if (customers != null)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        //db.BulkDelete(customers);
                        db.BulkMerge(customers);
                    }
                }
                System.Windows.MessageBox.Show("Data updated to the server successfully!");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }

        //Delete a customer
        private void btnCustomersDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DapperPlusManager.Entity<Customer>().Table("Customers");
                List<Customer> customers = dgCustomers.ItemsSource as List<Customer>;
                if (customers != null)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        var result = System.Windows.MessageBox.Show("All customers info will be delete, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                        if(result == MessageBoxResult.Yes)
                        {
                            db.BulkDelete(customers);
                            System.Windows.MessageBox.Show("Data wiped from the server successfully!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }

        private void btnCustomersExport_Click(object sender, RoutedEventArgs e)
        {
            //XLWorkbook workbook = new XLWorkbook();
            //DataTable table = gey
            //workbook.Worksheets.Add(table);
        }

        //-- Products --//

        //Choose a file
        private void btnProductsUpload_Click(object sender, RoutedEventArgs e)
        {
            tableCollection = null;
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls" })
            {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tbProductsFile.Text = openFileDialog.FileName;
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            tableCollection = result.Tables;
                            cbProductsSheet.Items.Clear();
                            foreach (DataTable table in tableCollection)
                                cbProductsSheet.Items.Add(table.TableName);
                        }
                    }
                }
            }
        }

        //Import data to server
        private void btnProductsImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DapperPlusManager.Entity<Products>().Table("Products");
                List<Products> products = dgProducts.ItemsSource as List<Products>;
                if (products != null)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        var result = System.Windows.MessageBox.Show("This action will overwrite any existing data, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                        if (result == MessageBoxResult.Yes)
                        {
                            db.BulkDelete(products);
                            db.BulkInsert(products);
                            System.Windows.MessageBox.Show("Data imported to the server successfully!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }

        //Reload current window
        private void btnProductsClear_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        //Choose a row
        private void cbProductsSheet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable dt = tableCollection[cbProductsSheet.SelectedItem.ToString()];
            List<Products> products = new List<Products>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Products product = new Products();
                    product.Product = dt.Rows[i]["Product"].ToString();
                    product.ProductCode = dt.Rows[i]["ProductCode"].ToString();
                    product.Description = dt.Rows[i]["Description"].ToString();
                    product.Price = Convert.ToDouble(dt.Rows[i]["Price"].ToString());
                    products.Add(product);
                }
                dgProducts.ItemsSource = products;
            }
        }

        //update product info
        private void btnProductsUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DapperPlusManager.Entity<Products>().Table("Products");
                List<Products> products = dgProducts.ItemsSource as List<Products>;
                if (products != null)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        //db.BulkDelete(products);
                        db.BulkMerge(products);
                    }
                }
                System.Windows.MessageBox.Show("Data updated to the server successfully!");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }

        //delete a product
        private void btnProductsDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DapperPlusManager.Entity<Products>().Table("Products");
                List<Products> products = dgProducts.ItemsSource as List<Products>;
                if (products != null)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        var result = System.Windows.MessageBox.Show("All products info will be delete, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                        if (result == MessageBoxResult.Yes)
                        {
                            db.BulkDelete(products);
                            System.Windows.MessageBox.Show("Data wiped from the server successfully!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }

        private void btnProductsExport_Click(object sender, RoutedEventArgs e)
        {
            //XLWorkbook workbook = new XLWorkbook();
            //DataTable table = gey
            //workbook.Worksheets.Add(table);
        }
    }
}
