using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using DatabaseApp.Data;
using DatabaseApp.Data.DataModels;
using DatabaseApp.View.UserControls;
using DocumentFormat.OpenXml.EMMA;
using System.Drawing;
using Brushes = System.Windows.Media.Brushes;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for UpdateData.xaml
    /// </summary>
    public partial class UpdateData : System.Windows.Window
    {
        public static string function;
        public static string type;
        public static string item;
        string connectionString = "Data Source=OS-GPCP-GPDN171\\MSSQLSERVER01;Initial catalog=dbdemo;Persist Security info=True;Encrypt=false;;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true";
        public UpdateData()
        {
            InitializeComponent();            
            DataNotLoad();

            switch (Login.GetRole)
            {
                case "admin":
                    cbAccountTest.Visibility= Visibility.Visible;
                    cbAccountLinked.Visibility= Visibility.Visible;
                    cbProductList.Visibility= Visibility.Visible;
                    btnDelete.IsEnabled = true;
                    btnDelete.Foreground = Brushes.WhiteSmoke;
                    break;
                case "Lv4":
                    cbAccountLinked.Visibility = Visibility.Visible;
                    break;
                case "Lv3":
                    cbSpecial.Visibility= Visibility.Collapsed;
                    break;
            }
        }
        DataTableCollection tableCollection;
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        void DataNotLoad()
        {
            btnUpload.IsEnabled = false;
            btnUpload.Foreground = Brushes.Black;
            cbSheet.IsEnabled = false;
            cbSheet.Foreground = Brushes.Black;
            btnImport.IsEnabled = false;
            btnImport.Foreground = Brushes.Black;
            btnUpdate.IsEnabled = false;
            btnUpdate.Foreground = Brushes.Black;
            btnExport.IsEnabled = false;
            btnExport.Foreground = Brushes.Black;
            btnDelete.IsEnabled = false;
            btnDelete.Foreground = Brushes.Black;
        }

        void DataLoad()
        {
            btnImport.IsEnabled = true;
            btnImport.Foreground = Brushes.WhiteSmoke;
            btnUpdate.IsEnabled = true;
            btnUpdate.Foreground = Brushes.WhiteSmoke;
            //btnDelete.IsEnabled = true;
            //btnDelete.Foreground = Brushes.WhiteSmoke;           
        }

        void ExportData()
        {
            btnExport.IsEnabled = true;
            btnExport.Foreground = Brushes.WhiteSmoke;
            btnUpload.IsEnabled = true;
            btnUpload.Foreground = Brushes.WhiteSmoke;
        }

        public void LoadGrid()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"Select * from {type}", con);
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgData.ItemsSource = dt.DefaultView;
        }

        public void ActivityLog()
        {
            con.Open();
            string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "INSERT INTO ActivityLog VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + $"{function} {type} {item}" + "', '" + "Data modified" + "', '" + currentdatetime + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //reload window
        private void Reset()
        {
            new UpdateData().Show();
            this.Close();
        }

        //Choose a table
        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataNotLoad();
            tbInfo.Visibility = Visibility.Collapsed;
            switch (cbType.SelectedIndex.ToString())
            {
                case "0":
                    cbType.Text = type = "Products";
                    LoadGrid();
                    ExportData();
                    break;
                case "1":
                    cbType.Text = type = "Customer";
                    LoadGrid();
                    ExportData();
                    break;
                case "2":
                    cbType.Text = type = "CustomerListFinal";
                    LoadGrid();
                    ExportData();
                    break;
                case "4":
                    cbType.Text = type = "AccountTest";
                    LoadGrid();
                    ExportData();
                    break;
                case "5":
                    cbType.Text = type = "AccountLinked";
                    LoadGrid();
                    ExportData();
                    break;
                case "6":
                    cbType.Text = type = "ProductList";
                    LoadGrid();
                    ExportData();
                    break;
            }
            lblTitle.Content = $"{cbType.Text} management";
            tbFile.Clear();
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls" })
            {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    dgData.ItemsSource = null;
                    tableCollection = null;
                    tbFile.Text = openFileDialog.FileName;
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            tableCollection = result.Tables;
                            function = "Upload";
                            item = "file";
                            ActivityLog();

                            //cbSheet active
                            cbSheet.IsEnabled = true;
                            cbSheet.Items.Clear();
                            cbSheet.IsEditable = false;
                            foreach (System.Data.DataTable table in tableCollection)
                            {
                                cbSheet.Items.Add(table.TableName);
                            }
                        }
                    }
                }
            }
        }

        //choose a sheet
        private void cbSheet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Data.DataTable dt = tableCollection[cbSheet.SelectedItem.ToString()];
            switch (cbType.SelectedIndex.ToString())
            {               
                case "0":
                    List<Products> products = new List<Products>();
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Products info = new Products();
                            info.Product = dt.Rows[i]["Product"].ToString();
                            info.ProductCode = dt.Rows[i]["ProductCode"].ToString();
                            info.Description = dt.Rows[i]["Description"].ToString();
                            info.Price = Convert.ToDouble(dt.Rows[i]["Price"].ToString());
                            products.Add(info);
                        }
                        dgData.ItemsSource = products;
                    }
                    break;
                case "1":
                    List<Customer> customer = new List<Customer>();
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Customer info = new Customer();
                            info.CustomerID = dt.Rows[i]["CustomerID"].ToString();
                            info.AccountID = dt.Rows[i]["AccountID"].ToString();
                            info.Username = dt.Rows[i]["Username"].ToString();
                            info.Role = dt.Rows[i]["Role"].ToString();
                            info.InputName = dt.Rows[i]["InputName"].ToString();
                            info.Gender = dt.Rows[i]["Gender"].ToString();
                            info.Title = dt.Rows[i]["Title"].ToString();
                            info.Company = dt.Rows[i]["Name"].ToString();
                            info.Address = dt.Rows[i]["Address"].ToString();
                            info.City = dt.Rows[i]["City"].ToString();
                            info.Region = dt.Rows[i]["Region"].ToString();
                            info.PostalCode = dt.Rows[i]["PostalCode"].ToString();
                            info.Country = dt.Rows[i]["Country"].ToString();
                            info.Phone = dt.Rows[i]["Phone"].ToString();
                            info.Fax = dt.Rows[i]["Fax"].ToString();
                            info.PaymentMethod = dt.Rows[i]["PaymentMethod"].ToString();
                            info.Bill = Convert.ToDouble(dt.Rows[i]["Bill"]);
                            info.CouponCode = dt.Rows[i]["CouponCode"].ToString();
                            info.PaymentStatus = dt.Rows[i]["PaymentStatus"].ToString();
                            info.CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"]);

                            customer.Add(info);
                        }
                        dgData.ItemsSource = customer;
                    }
                    break;
                case "2":
                    List<CustomerListFinal> customerListFinal = new List<CustomerListFinal>();
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            CustomerListFinal info = new CustomerListFinal();
                            info.OrderID = dt.Rows[i]["OrderID"].ToString();
                            info.AccountID = dt.Rows[i]["AccountID"].ToString();
                            info.Username = dt.Rows[i]["Username"].ToString();
                            info.InputName = dt.Rows[i]["InputName"].ToString();
                            info.Product = dt.Rows[i]["Product"].ToString();
                            info.ProductCode = dt.Rows[i]["ProductCode"].ToString();
                            info.Price = Convert.ToDouble(dt.Rows[i]["Price"].ToString());
                            info.Amount = Convert.ToDouble(dt.Rows[i]["Amount"].ToString());
                            info.CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"]);

                            customerListFinal.Add(info);
                        }
                        dgData.ItemsSource = customerListFinal;
                    }
                    break;
                case "4":
                    List<AccountTest> accounts = new List<AccountTest>();
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AccountTest info = new AccountTest();
                            info.AccountID = dt.Rows[i]["AccountID"].ToString();
                            info.Username = dt.Rows[i]["Username"].ToString();
                            info.Email = dt.Rows[i]["Email"].ToString();
                            info.Password = dt.Rows[i]["Password"].ToString();
                            info.Role = dt.Rows[i]["Role"].ToString();
                            info.PhoneNumbers = dt.Rows[i]["PhoneNumbers"].ToString();
                            info.Gender = dt.Rows[i]["Gender"].ToString();
                            info.CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"]);
                            accounts.Add(info);
                        }
                        dgData.ItemsSource = accounts;
                    }
                    break;
                case "5":
                    List<AccountLinked> accountLinked = new List<AccountLinked>();
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AccountLinked info = new AccountLinked();
                            info.AccountID = dt.Rows[i]["AccountID"].ToString();
                            info.Username = dt.Rows[i]["Name"].ToString();
                            info.Apple = dt.Rows[i]["Apple"].ToString();
                            info.Facebook = dt.Rows[i]["Facebook"].ToString();
                            info.Twitter = dt.Rows[i]["Twitter"].ToString();
                            info.Github = dt.Rows[i]["Github"].ToString();
                            accountLinked.Add(info);
                        }
                        dgData.ItemsSource = accountLinked;
                        btnImport.IsEnabled = false;
                    }
                    break;
                case "6":
                    List<ProductLists> productLists = new List<ProductLists>();
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ProductLists info = new ProductLists();
                            info.ProductCode = dt.Rows[i]["ProductCode"].ToString();
                            info.Product = dt.Rows[i]["Product"].ToString();                            
                            info.Description = dt.Rows[i]["Description"].ToString();
                            info.Type = dt.Rows[i]["Type"].ToString();
                            info.Price = Convert.ToDouble(dt.Rows[i]["Price"].ToString());
                            info.Amount = Convert.ToDouble(dt.Rows[i]["Amount"].ToString());
                            info.Status = dt.Rows[i]["Status"].ToString();
                            info.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                            info.TimeCreated = Convert.ToDateTime(dt.Rows[i]["TimeCreated"]);
                            info.ModifiedBy = dt.Rows[i]["ModifiedBy"].ToString();
                            info.TimeModified = Convert.ToDateTime(dt.Rows[i]["TimeModified"]);
                            info.SalePercent = Convert.ToDouble(dt.Rows[i]["SalePercent"]);
                            productLists.Add(info);
                        }
                        dgData.ItemsSource = productLists;
                        btnImport.IsEnabled = false;
                    }
                    break;
            }
            DataLoad();
        }     

        //Update button
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (cbType.SelectedIndex.ToString())
                {
                    case "0":
                        DapperPlusManager.Entity<Products>().Table("Products");
                        List<Products> products = dgData.ItemsSource as List<Products>;
                        if (products != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                db.BulkMerge(products);
                                System.Windows.MessageBox.Show("Data updated to the server successfully!");
                            }
                        }
                        break;
                    case "1":
                        DapperPlusManager.Entity<Customer>().Table("Customer");
                        List<Customer> customer = dgData.ItemsSource as List<Customer>;
                        if (customer != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                db.BulkMerge(customer);
                                System.Windows.MessageBox.Show("Data updated to the server successfully!");
                            }
                        }
                        break;
                    case "2":
                        DapperPlusManager.Entity<CustomerListFinal>().Table("CustomerListFinal");
                        List<CustomerListFinal> customerListFinal = dgData.ItemsSource as List<CustomerListFinal>;
                        if (customerListFinal != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                db.BulkMerge(customerListFinal);
                                System.Windows.MessageBox.Show("Data updated to the server successfully!");
                            }
                        }
                        break;
                    case "4":
                        DapperPlusManager.Entity<AccountTest>().Table("AccountTest");
                        List<AccountTest> accountTest = dgData.ItemsSource as List<AccountTest>;
                        if (accountTest != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                db.BulkMerge(accountTest);
                                System.Windows.MessageBox.Show("Data updated to the server successfully!");
                            }
                        }
                        break;
                    case "5":
                        DapperPlusManager.Entity<AccountLinked>().Table("AccountLinked");
                        List<AccountLinked> accountLinked = dgData.ItemsSource as List<AccountLinked>;
                        if (accountLinked != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                db.BulkMerge(accountLinked);
                                System.Windows.MessageBox.Show("Data updated to the server successfully!");
                            }
                        }
                        break;
                    case "6":
                        DapperPlusManager.Entity<ProductLists>().Table("ProductList");
                        List<ProductLists> productLists = dgData.ItemsSource as List<ProductLists>;
                        if (productLists != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                db.BulkMerge(productLists);
                                System.Windows.MessageBox.Show("Data updated to the server successfully!");
                            }
                        }
                        break;
                }
                function = "Update on";
                item = "database";
                ActivityLog();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }

        //Delete button (admin only)
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (cbType.SelectedIndex.ToString())
                {
                    case "0":
                        DapperPlusManager.Entity<Products>().Table("Products");
                        List<Products> products = dgData.ItemsSource as List<Products>;
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
                        break;
                    case "1":
                        DapperPlusManager.Entity<Customer>().Table("Customer");
                        List<Customer> customer = dgData.ItemsSource as List<Customer>;
                        if (customer != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                var result = System.Windows.MessageBox.Show("All customers information will be delete, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                                if (result == MessageBoxResult.Yes)
                                {
                                    db.BulkDelete(customer);
                                    System.Windows.MessageBox.Show("Data wiped from the server successfully!");
                                }
                            }
                        }
                        break;
                    case "2":
                        DapperPlusManager.Entity<CustomerListFinal>().Table("CustomerListFinal");
                        List<CustomerListFinal> customerListFinal = dgData.ItemsSource as List<CustomerListFinal>;
                        if (customerListFinal != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                var result = System.Windows.MessageBox.Show("All customers information will be delete, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                                if (result == MessageBoxResult.Yes)
                                {
                                    db.BulkDelete(customerListFinal);
                                    System.Windows.MessageBox.Show("Data wiped from the server successfully!");
                                }
                            }
                        }
                        break;
                    case "4":
                        DapperPlusManager.Entity<AccountTest>().Table("AccountTest");
                        List<AccountTest> accountTest = dgData.ItemsSource as List<AccountTest>;
                        if (accountTest != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                var result = System.Windows.MessageBox.Show("All accountTests information will be delete, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                                if (result == MessageBoxResult.Yes)
                                {
                                    db.BulkDelete(accountTest);
                                    System.Windows.MessageBox.Show("Data wiped from the server successfully!");
                                }
                            }
                        }
                        break;
                    case "5":
                        DapperPlusManager.Entity<AccountLinked>().Table("AccountLinked");
                        List<AccountLinked> accountLinked = dgData.ItemsSource as List<AccountLinked>;
                        if (accountLinked != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                var result = System.Windows.MessageBox.Show("All account linked information will be delete, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                                if (result == MessageBoxResult.Yes)
                                {
                                    db.BulkDelete(accountLinked);
                                    System.Windows.MessageBox.Show("Data wiped from the server successfully!");
                                }
                            }
                        }
                        break;
                    case "6":
                        DapperPlusManager.Entity<ProductLists>().Table("ProductList");
                        List<ProductLists> productLists = dgData.ItemsSource as List<ProductLists>;
                        if (productLists != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                var result = System.Windows.MessageBox.Show("All products list information will be delete, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                                if (result == MessageBoxResult.Yes)
                                {
                                    db.BulkDelete(productLists);
                                    System.Windows.MessageBox.Show("Data wiped from the server successfully!");
                                }
                            }
                        }
                        break;
                }
                function = "Delete";
                item = "database";
                ActivityLog();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }

        //Reload window
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        //Import button
        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (cbType.SelectedIndex.ToString())
                {
                    case "0":
                        DapperPlusManager.Entity<Products>().Table("Products");
                        List<Products> products = dgData.ItemsSource as List<Products>;
                        if (products != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                var result = System.Windows.MessageBox.Show("This action will overwrite any existing data, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                                if (result == MessageBoxResult.Yes)
                                {
                                    db.BulkInsert(products);
                                    System.Windows.MessageBox.Show("Data imported to the server successfully!");
                                }
                            }
                        }
                        break;
                    case "1":
                        DapperPlusManager.Entity<Customer>().Table("Customer");
                        List<Customer> customer = dgData.ItemsSource as List<Customer>;
                        if (customer != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                var result = System.Windows.MessageBox.Show("This action will overwrite any existing data, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                                if (result == MessageBoxResult.Yes)
                                {
                                    db.BulkInsert(customer);
                                    System.Windows.MessageBox.Show("Data imported to the server successfully!");
                                }
                            }
                        }
                        break;
                    case "2":
                        DapperPlusManager.Entity<CustomerListFinal>().Table("CustomerListFinal");
                        List<CustomerListFinal> customerListFinal = dgData.ItemsSource as List<CustomerListFinal>;
                        if (customerListFinal != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                var result = System.Windows.MessageBox.Show("This action will overwrite any existing data, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                                if (result == MessageBoxResult.Yes)
                                {
                                    db.BulkInsert(customerListFinal);
                                    System.Windows.MessageBox.Show("Data imported to the server successfully!");
                                }
                            }
                        }
                        break;
                    case "4":
                        DapperPlusManager.Entity<AccountTest>().Table("AccountTest");
                        List<AccountTest> accountTest = dgData.ItemsSource as List<AccountTest>;
                        if (accountTest != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                var result = System.Windows.MessageBox.Show("This action will overwrite any existing data, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                                if (result == MessageBoxResult.Yes)
                                {
                                    db.BulkInsert(accountTest);
                                    System.Windows.MessageBox.Show("Data imported to the server successfully!");
                                }
                            }
                        }
                        break;
                    case "5":
                        DapperPlusManager.Entity<AccountLinked>().Table("AccountLinked");
                        List<AccountLinked> accountLinked = dgData.ItemsSource as List<AccountLinked>;
                        if (accountLinked != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                var result = System.Windows.MessageBox.Show("This action will overwrite any existing data, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                                if (result == MessageBoxResult.Yes)
                                {
                                    db.BulkInsert(accountLinked);
                                    System.Windows.MessageBox.Show("Data imported to the server successfully!");
                                }
                            }
                        }
                        break;
                    case "6":
                        DapperPlusManager.Entity<ProductLists>().Table("ProductList");
                        List<ProductLists> productLists = dgData.ItemsSource as List<ProductLists>;
                        if (productLists != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                var result = System.Windows.MessageBox.Show("This action will overwrite any existing data, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                                if (result == MessageBoxResult.Yes)
                                {
                                    db.BulkInsert(productLists);
                                    System.Windows.MessageBox.Show("Data imported to the server successfully!");
                                }
                            }
                        }
                        break;
                }
                function = "Import to";
                item = "database";
                ActivityLog();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }

        //Export button
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            string currentdatetime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string LogFolder = @"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs";
            string queryString = $"SELECT * FROM {type}";
            string filePath = $@"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs\File\{cbType.Text}.XLSX";

            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //Create new Excel application and workbook
                            Application excelApp = new Application();
                            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Add();
                            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = excelWorkbook.Worksheets[1];

                            //Add the headers to first row
                            int col = 1;
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                excelWorksheet.Cells[1, col].Value2 = reader.GetName(i);
                                excelWorksheet.Cells[1, col].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                                excelWorksheet.Cells[1, col].Borders.LineStyle = 1;
                                excelWorksheet.Cells[1, col].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                col++;
                            }

                            //Iterate through data start from second row and insert into worksheet
                            int row = 2;
                            while (reader.Read())
                            {
                                col = 1;
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    excelWorksheet.Cells[row, col].EntireColumn.NumberFormat = "@";
                                    excelWorksheet.Cells[row, col].Value2 = reader[i];
                                    excelWorksheet.Cells[row, col].EntireColumn.AutoFit();
                                    excelWorksheet.Cells[row, col].HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                    excelWorksheet.Cells[row, col].Borders.LineStyle = 1;
                                    if (type == "Customer")
                                    {
                                        excelWorksheet.Columns["U"].NumberFormat = "yyyy-MM-dd HH:mm:ss";
                                    }
                                    else if (type == "AccountTest" || type == "CustomerListFinal")
                                    {
                                        excelWorksheet.Columns["I"].NumberFormat = "yyyy-MM-dd HH:mm:ss";
                                    }
                                    col++;
                                }
                                row++;
                            }

                            //UpdateFormat(filePath);
                            //Save workbook and close Excel application
                            function = "Export";
                            item = "file";
                            ActivityLog();
                            excelWorkbook.SaveAs(filePath);
                            System.Windows.MessageBox.Show("File created successfully");
                            excelWorkbook.Close();
                            excelApp.Quit();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                using (StreamWriter sw = File.CreateText(LogFolder + "\\" + "ErrorLog" + currentdatetime + ".log"))
                {
                    function = "Attempt to export";
                    item = "file";
                    ActivityLog();
                    sw.WriteLine(exception.ToString());
                }
            }
        }
    }
}
