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

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for UpdateData.xaml
    /// </summary>
    public partial class UpdateData : System.Windows.Window
    {
        public static string type;
        string connectionString = "Data Source=OS-GPCP-GPDN171\\MSSQLSERVER01;Initial catalog=dbdemo;Persist Security info=True;Encrypt=false;;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true";
        public UpdateData()
        {
            InitializeComponent();
            btnUpload.IsEnabled = false;
            if (View.UserControls.MenuBar.role == "admin")
            {
                cbSpecial.Visibility = Visibility.Visible;
                cbAdvanceCustomers.Visibility = Visibility.Visible;
            }
            if (View.UserControls.MenuBar.role == "Lv3")
            {
                cbType.Items.Remove(cbAccount);
            }
            else if (View.UserControls.MenuBar.role == "Lv2")
            {
                cbType.Items.Remove(cbAccount);
                cbType.Items.Remove(cbProduct);
            }
        }
        DataTableCollection tableCollection;

        //reload window command
        private void Reset()
        {
            new UpdateData().Show();
            this.Close();
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            tableCollection = null;
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls" })
            {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
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
                            cbSheet.Items.Clear();
                            foreach (System.Data.DataTable table in tableCollection)
                                cbSheet.Items.Add(table.TableName);
                        }
                    }
                }
            }
        }

        //Choose a row
        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbType.SelectedIndex.ToString())
            {
                case "0":
                    cbType.Text = type = "Customers";
                    btnUpload.IsEnabled = true;
                    break;
                case "1":
                    cbType.Text = type = "Products";
                    btnUpload.IsEnabled = true;
                    break;
                case "2":
                    cbType.Text = "Accounts";
                    btnUpload.IsEnabled = true;
                    type = "Account";
                    break;
                case "4":
                    cbType.Text = type = "AdvanceCustomers";
                    btnUpload.IsEnabled = true;
                    break;
            }
            lblTitle.Content = $"{cbType.Text} management";
            tbFile.Clear();
        }

        private void cbSheet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Data.DataTable dt = tableCollection[cbSheet.SelectedItem.ToString()];
            switch (cbType.SelectedIndex.ToString())
            {
                case "0":
                    List<Customers> customers = new List<Customers>();
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Customers info = new Customers();
                            info.CustomerID = dt.Rows[i]["CustomerID"].ToString();
                            info.Name = dt.Rows[i]["Name"].ToString();
                            info.Title = dt.Rows[i]["Title"].ToString();
                            info.Company = dt.Rows[i]["Name"].ToString();
                            info.Address = dt.Rows[i]["Address"].ToString();
                            info.City = dt.Rows[i]["City"].ToString();
                            info.Country = dt.Rows[i]["Country"].ToString();
                            info.Phone = dt.Rows[i]["Phone"].ToString();
                            info.Fax = dt.Rows[i]["Fax"].ToString();
                            info.Region = dt.Rows[i]["Region"].ToString();
                            info.PostalCode = dt.Rows[i]["PostalCode"].ToString();
                            customers.Add(info);
                        }
                        dgData.ItemsSource = customers;
                    }
                    break;
                case "1":
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
                case "2":
                    List<Accounts> accounts = new List<Accounts>();
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Accounts info = new Accounts();
                            info.Email = dt.Rows[i]["Email"].ToString();
                            info.Username = dt.Rows[i]["Username"].ToString();
                            info.Password = dt.Rows[i]["Password"].ToString();
                            info.Role = dt.Rows[i]["Role"].ToString();
                            info.PhoneNumbers = Convert.ToInt32(dt.Rows[i]["PhoneNumbers"].ToString());
                            info.Gender = dt.Rows[i]["Gender"].ToString();
                            accounts.Add(info);
                        }
                        dgData.ItemsSource = accounts;
                    }
                    break;
                case "4":
                    List<AdvanceCustomers> aCustomers = new List<AdvanceCustomers>();
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AdvanceCustomers info = new AdvanceCustomers();
                            info.AName = dt.Rows[i]["Name"].ToString();
                            info.ATitle = dt.Rows[i]["Title"].ToString();
                            info.ACompany = dt.Rows[i]["Name"].ToString();
                            info.AAddress = dt.Rows[i]["Address"].ToString();
                            info.ACity = dt.Rows[i]["City"].ToString();
                            info.ACountry = dt.Rows[i]["Country"].ToString();
                            info.APhone = dt.Rows[i]["Phone"].ToString();
                            info.AFax = dt.Rows[i]["Fax"].ToString();
                            info.ARegion = dt.Rows[i]["Region"].ToString();
                            info.APostalCode = dt.Rows[i]["PostalCode"].ToString();
                            aCustomers.Add(info);
                        }
                        dgData.ItemsSource = aCustomers;
                    }
                    break;
            }
        }     

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (cbType.SelectedIndex.ToString())
                {
                    case "0":
                        DapperPlusManager.Entity<Customers>().Table("Customers");
                        List<Customers> customers = dgData.ItemsSource as List<Customers>;
                        if (customers != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                db.BulkMerge(customers);
                            }
                        }
                        break;
                    case "1":
                        DapperPlusManager.Entity<Products>().Table("Products");
                        List<Products> products = dgData.ItemsSource as List<Products>;
                        if (products != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                db.BulkMerge(products);
                            }
                        }
                        break;
                    case "2":
                        DapperPlusManager.Entity<Accounts>().Table("Account");
                        List<Accounts> accounts = dgData.ItemsSource as List<Accounts>;
                        if (accounts != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                db.BulkMerge(accounts);
                            }
                        }
                        break;
                }
                System.Windows.MessageBox.Show("Data updated to the server successfully!");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (cbType.SelectedIndex.ToString())
                {
                    case "0":
                        DapperPlusManager.Entity<Customers>().Table("Customers");
                        List<Customers> customers = dgData.ItemsSource as List<Customers>;
                        if (customers != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                var result = System.Windows.MessageBox.Show("All customers info will be delete, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                                if (result == MessageBoxResult.Yes)
                                {
                                    db.BulkDelete(customers);
                                    System.Windows.MessageBox.Show("Data wiped from the server successfully!");
                                }
                            }
                        }
                        break;
                    case "1":
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
                    case "2":
                        DapperPlusManager.Entity<Accounts>().Table("Account");
                        List<Accounts> accounts = dgData.ItemsSource as List<Accounts>;
                        if (accounts != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                var result = System.Windows.MessageBox.Show("All accounts info will be delete, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                                if (result == MessageBoxResult.Yes)
                                {
                                    db.BulkDelete(accounts);
                                    System.Windows.MessageBox.Show("Data wiped from the server successfully!");
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }

        //reload window
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (cbType.SelectedIndex.ToString())
                {
                    case "0":
                        DapperPlusManager.Entity<Customers>().Table("Customers");
                        List<Customers> customers = dgData.ItemsSource as List<Customers>;
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
                        break;
                    case "1":
                        DapperPlusManager.Entity<Products>().Table("Products");
                        List<Products> products = dgData.ItemsSource as List<Products>;
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
                        break;
                    case "2":
                        DapperPlusManager.Entity<Accounts>().Table("Account");
                        List<Accounts> accounts = dgData.ItemsSource as List<Accounts>;
                        if (accounts != null)
                        {
                            using (IDbConnection db = new SqlConnection(connectionString))
                            {
                                var result = System.Windows.MessageBox.Show("This action will overwrite any existing data, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                                if (result == MessageBoxResult.Yes)
                                {
                                    db.BulkDelete(accounts);
                                    db.BulkInsert(accounts);
                                    System.Windows.MessageBox.Show("Data imported to the server successfully!");
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            string currentdatetime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string LogFolder = @"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs";
            string queryString = $"SELECT * FROM {type}";
            string filePath = $@"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs\{cbType.Text}.XLSX";

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
                            Workbook excelWorkbook = excelApp.Workbooks.Add();
                            Worksheet excelWorksheet = excelWorkbook.Worksheets[1];

                            //Range range = excelWorksheet.Range["A1:C5"];
                            //Formatting rangeFormatting = range.BeginUpdateFormatting();
                            //Borders rangeBorders = rangeFormatting.Borders;
                            //rangeBorders.LineStyle = XlLineStyle.xlContinuous;
                            //rangeBorders[XlBordersIndex.xlEdgeTop].Weight = 10d;
                            //rangeBorders.ColorIndex = XlRgbColor.rgbCrimson;
                            //range.EndUpdateFormatting(rangeFormatting);

                            //Add the headers to first row
                            int col = 1;
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                excelWorksheet.Cells[1, col].Value2 = reader.GetName(i);
                                col++;
                            }

                            //Iterate through data start from second row and insert into worksheet
                            int row = 2;
                            while (reader.Read())
                            {
                                col = 1;
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    excelWorksheet.Cells[row, col].Value2 = reader[i];
                                    col++;
                                }
                                row++;
                            }

                            //UpdateFormat(filePath);
                            //Save workbook and close Excel application
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
                    sw.WriteLine(exception.ToString());
                }
            }
        }
    }
}
