﻿using DatabaseApp.Data.DataModels;
using DatabaseApp.View.UserControls;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Office.Interop.Excel;
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
using static Azure.Core.HttpHeader;
using static ClosedXML.Excel.XLPredefinedFormat;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for Checkout.xaml
    /// </summary>
    public partial class Checkout : System.Windows.Window
    {
        public static double discountAmount;
        public static string paymentMethod;
        public static double bill;
        public static string status;
        public static string query;
        public static string code;
        public static string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        public Checkout()
        {
            InitializeComponent();
            txtUser.Text = Login.passText;
            txtAmount.Text = ProductListUser.number.ToString();
            bill = ProductListUser.total;
            txtTotals.Text = bill + " $";
            //txtTotals2.Text = discount;

            txtStatus.Visibility = Visibility.Collapsed;
            LbAmount2.Visibility = Visibility.Collapsed;
            txtTotals2.Visibility = Visibility.Collapsed;
            LbDiscountAmount.Visibility = Visibility.Collapsed;
            txtDiscountAmount.Visibility = Visibility.Collapsed;

            if(ProductListUser.number2 >= 10)
            {
                txtcode1.Text = "SmallGift (5% OFF)";
                txtcode1.Foreground = Brushes.ForestGreen;
                if (ProductListUser.number2 >= 20)
                {
                    txtcode2.Text = "BiggerOne (10% OFF)";
                    txtcode2.Foreground = Brushes.ForestGreen;
                    if (ProductListUser.number2 >= 50)
                    {
                        txtcode3.Text = "RealDeal (15% OFF)";
                        txtcode3.Foreground = Brushes.ForestGreen;
                        if (ProductListUser.number2 >= 100)
                        {
                            txtcode4.Text = "GiftCode (20% OFF)";
                            txtcode4.Foreground = Brushes.ForestGreen;
                            if (ProductListUser.number2 >= 200)
                            {
                                txtcode5.Text = "GoodDeal (25% OFF)";
                                txtcode5.Foreground = Brushes.ForestGreen;
                                if (ProductListUser.number2 >= 500)
                                {
                                    txtcode6.Text = "MegaDeal (30% OFF)";
                                    txtcode6.Foreground = Brushes.ForestGreen;
                                }
                            }
                        }
                    }
                }
            }
        }
        string connectionString = "Data Source=OS-GPCP-GPDN171\\MSSQLSERVER01;Initial catalog=dbdemo;Persist Security info=True;Encrypt=false;;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true";
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        public void Clear()
        {
            ProductListUser.itemsCount = 0;
            ProductListUser.total = 0;
            ProductListUser.number = 0;
            ProductList.finalAmount.Clear();
            ProductList.finalNumber.Clear();
        }

        public void ActivityLog()
        {
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void HlBack_Click(object sender, RoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show("Your current products list will be deleted, continue?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
            if (result == MessageBoxResult.Yes)
            {
                con.Open();
                query = "INSERT INTO ActivityLog VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + "Checkout cancel" + "', '" + "Checkout status" + "', '" + currentdatetime + "')";
                ActivityLog();
                Clear();
                DeleteList();
                DeleteListFinal();
                new ProductList().Show();
                this.Close();
            }
        }

        public void CodeApllied()
        {
            //System.Windows.MessageBox.Show("Code redeemed", "Code applied");
            txtDiscountAmount.Text = (ProductListUser.total * discountAmount) + " $";
            bill = (ProductListUser.total - (ProductListUser.total * discountAmount));
            txtTotals2.Text = bill + " $";

            txtStatus.Visibility = Visibility.Visible;
            LbAmount1.Visibility = Visibility.Visible;
            txtTotals.Visibility = Visibility.Visible;
            LbAmount2.Visibility = Visibility.Visible;
            txtTotals2.Visibility = Visibility.Visible;
            LbDiscountAmount.Visibility = Visibility.Visible;
            txtDiscountAmount.Visibility = Visibility.Visible;

            txtTotals.TextDecorations = TextDecorations.Strikethrough;
            LbAmount1.TextDecorations = TextDecorations.Strikethrough;
        }

        private void btnCode_Click(object sender, RoutedEventArgs e)
        {
            if (tbCode.Text == "MegaDeal")
            {
                if (ProductListUser.number2 < 500)
                {
                    System.Windows.MessageBox.Show("Number of product required to use this code not met", "Error");
                }
                else
                {
                    if (txtStatus.Visibility != Visibility.Visible)
                    {
                        discountAmount = 0.3;
                        CodeApllied();
                        txtStatus.Text = "Code (30% Off) applied";
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("A code is already applied", "Error");
                    }
                }               
            }
            else if (tbCode.Text == "GoodDeal")
            {
                if (ProductListUser.number2 < 200)
                {
                    System.Windows.MessageBox.Show("Number of product required to use this code not met", "Error");
                }
                else
                {
                    if (txtStatus.Visibility != Visibility.Visible)
                    {
                        discountAmount = 0.25;
                        CodeApllied();
                        txtStatus.Text = "Code (25% Off) applied";
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("A code is already applied", "Error");
                    }
                }
            }
            else if (tbCode.Text == "GiftCode")
            {
                if (ProductListUser.number2 < 100)
                {
                    System.Windows.MessageBox.Show("Number of product required to use this code not met", "Error");
                }
                else
                {
                    if (txtStatus.Visibility != Visibility.Visible)
                    {
                        discountAmount = 0.2;
                        CodeApllied();
                        txtStatus.Text = "Code (20% Off) applied";
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("A code is already applied", "Error");
                    }
                }               
            }
            else if (tbCode.Text == "RealDeal")
            {
                if (ProductListUser.number2 < 50)
                {
                    System.Windows.MessageBox.Show("Number of product required to use this code not met", "Error");
                }
                else
                {
                    if (txtStatus.Visibility != Visibility.Visible)
                    {
                        discountAmount = 0.15;
                        CodeApllied();
                        txtStatus.Text = "Code (15% Off) applied";
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("A code is already applied", "Error");
                    }
                }
            }
            else if (tbCode.Text == "BiggerOne")
            {
                if (ProductListUser.number2 < 20)
                {
                    System.Windows.MessageBox.Show("Number of product required to use this code not met", "Error");
                }
                else
                {
                    if (txtStatus.Visibility != Visibility.Visible)
                    {
                        discountAmount = 0.1;
                        CodeApllied();
                        txtStatus.Text = "Code (10% Off) applied";
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("A code is already applied", "Error");
                    }
                }
            }
            else if (tbCode.Text == "SmallGift")
            {
                if (ProductListUser.number2 < 10)
                {
                    System.Windows.MessageBox.Show("Number of product required to use this code not met", "Error");
                }
                else
                {
                    if (txtStatus.Visibility != Visibility.Visible)
                    {
                        discountAmount = 0.05;
                        CodeApllied();
                        txtStatus.Text = "Code (5% Off) applied";
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("A code is already applied", "Error");
                    }
                }                
            }
            else
            {
                if (tbCode.Text == "")
                {
                    System.Windows.MessageBox.Show("Enter a Code", "Error");
                }
                else
                {
                    System.Windows.MessageBox.Show("Invalid Code", "Error");
                }
                bill = ProductListUser.total;
                txtTotals2.Text = bill + " $";

                txtStatus.Visibility = Visibility.Collapsed;
                LbAmount1.Visibility = Visibility.Collapsed;
                txtTotals.Visibility = Visibility.Collapsed;
                LbAmount2.Visibility = Visibility.Visible;
                txtTotals2.Visibility = Visibility.Visible;
                LbDiscountAmount.Visibility = Visibility.Collapsed;
                txtDiscountAmount.Visibility = Visibility.Collapsed;

                //txtTotals.TextDecorations.Clear();
                //LbAmount1.TextDecorations.Clear();
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                if (tbName.Text == "" || cbGender.Text == "" || tbAddress.Text == "" || tbCity.Text == "" || tbRegion.Text == "" || tbPostalCode.Text == "" || tbCountry.Text == "" || tbPhone.Text == "")
                {
                    System.Windows.MessageBox.Show("filled all the field with (*)", "Error");
                }
                else
                {
                    if (txtPayment.Text == "Choose an payment method")
                    {
                        System.Windows.MessageBox.Show("Choose an payment method", "Error");
                    }
                    else
                    {
                        if (txtStatus.Visibility == Visibility.Collapsed)
                        {
                            code = "No Coupon";
                        }
                        else
                        {
                            code = tbCode.Text;
                        }

                        var result = System.Windows.MessageBox.Show("Your information will be saved to the systems, continue?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                        if (result == MessageBoxResult.Yes)
                        {
                            // insert info into Customer
                            con.Open();
                            query = "INSERT INTO Customer VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + tbName.Text + "','" + cbGender.Text + "','" + tbTitle.Text + "', '" + tbCompany.Text + "', '" + tbAddress.Text + "', '" + tbCity.Text + "', '" + tbRegion.Text + "', '" + tbPostalCode.Text + "', '" + tbCountry.Text + "', '" + tbPhone.Text + "', '" + tbFax.Text + "', '" + paymentMethod + "', '" + bill + "', '" + code + "', '" + status + "', '" + currentdatetime + "')";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                            con.Close();

                            // insert info into CustomerOrder
                            con.Open();
                            query = "INSERT INTO CustomerOrder VALUES ('" + tbName.Text + "','" + status + "','" + "Stage 0" + "')";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                            con.Close();

                            con.Open();
                            query = "INSERT INTO ActivityLog VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + "Checkout complete" + "', '" + "Checkout status" + "', '" + currentdatetime + "')";
                            ActivityLog();
                            System.Windows.MessageBox.Show("Check out successfully", "Info");
                            PrintReceipt();

                            //Finalize table CustomerListFinal
                            con.Open();
                            query = $"Update CustomerListFinal Set InputName = @InputName, CreatedDate = @CreatedDate Where AccountID = @AccountID AND InputName = '' ";
                            cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@InputName", tbName.Text);
                            cmd.Parameters.AddWithValue("@CreatedDate", currentdatetime);
                            cmd.Parameters.AddWithValue("@AccountID", Login.GetID);
                            cmd.ExecuteNonQuery();
                            con.Close();

                            result = System.Windows.MessageBox.Show("Would you like to print the receipt?", "Notify", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                            if (result == MessageBoxResult.Yes)
                            {
                                con.Open();
                                query = "INSERT INTO ActivityLog VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + "Receipt print" + "', '" + "Checkout status" + "', '" + currentdatetime + "')";
                                ActivityLog();
                                System.Windows.MessageBox.Show("Receipt print successfully");
                            }

                            Clear();
                            DeleteList();

                            if (Login.GetRole == "admin" || Login.GetRole == "Lv4")
                            {
                                new HomeAdmin().Show();
                            }
                            else
                            {
                                new Home().Show();
                            }
                            this.Close();                        
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                con.Close();
            }
            
        }

        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            tbName.Text = Login.passText;
            tbAddress.Text = Login.passText + " house";
            cbGender.Text = "Unknown";
            tbTitle.Text = Login.GetRole;
            tbCompany.Text = Login.passText + " company";
            tbCity.Text = "New York";
            tbRegion.Text = "Middle";
            tbPostalCode.Text = "0350";
            tbCountry.Text = "USA"; 
            tbPhone.Text = "03503846958";
            tbFax.Text = "0904366666";
        }

        private void Epayment_Click(object sender, RoutedEventArgs e)
        {
            txtPayment.Text = "The bill had been payed";
            paymentMethod = "E-wallet";
            status = "Payment complete";

            payment1.Background = Brushes.ForestGreen;
            payment2.Background = Brushes.Red;
            payment3.Background = Brushes.Red;
        }

        private void Bpayment_Click(object sender, RoutedEventArgs e)
        {
            txtPayment.Text = "The bill had been payed";
            paymentMethod = "Bank account";
            status = "Payment complete";

            payment1.Background = Brushes.Red;
            payment2.Background = Brushes.ForestGreen;
            payment3.Background = Brushes.Red;
        }

        private void Cpayment_Click(object sender, RoutedEventArgs e)
        {
            txtPayment.Text = "The bill is on hold";
            paymentMethod = "COD";
            status = "Payment incomplete";

            payment1.Background = Brushes.Red;
            payment2.Background = Brushes.Red;
            payment3.Background = Brushes.ForestGreen;
        }

        //Delete list information
        private void DeleteList()
        {
            DapperPlusManager.Entity<CustomerList>().Table("CustomerList");
            List<CustomerList> customerList = ProductListUser.list;
            if (customerList != null)
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    db.BulkDelete(customerList);
                }
            }
        }

        private void DeleteListFinal()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"DELETE FROM CustomerListFinal WHERE InputName = '' ", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //Print receipt
        private void PrintReceipt()
        {
            string currentdatetime = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            string LogFolder = @"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs";
            string queryString = "SELECT * FROM CustomerList WHERE AccountID = @AccountID";
            string filePath = $@"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs\Receipts\{tbName.Text} Receipt.XLSX";

            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("AccountID", Login.GetID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //Create new Excel application and workbook
                            Application excelApp = new Application();
                            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Add();
                            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = excelWorkbook.Worksheets[1];

                            //Add the headers to first row
                            int col = 1;
                            for (int i = 4; i < reader.FieldCount; i++)
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
                                for (int i = 4; i < reader.FieldCount; i++)
                                {
                                    excelWorksheet.Cells[row, col].EntireColumn.NumberFormat = "@";
                                    excelWorksheet.Cells[row, col].Value2 = reader[i];
                                    excelWorksheet.Cells[row, col].EntireColumn.AutoFit();
                                    excelWorksheet.Cells[row, col].HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                    excelWorksheet.Cells[row, col].Borders.LineStyle = 1;
                                    excelWorksheet.Columns["E"].NumberFormat = "yyyy-MM-dd HH:mm:ss";
                                    col++;
                                }
                                row++;
                            }

                            //Add price column
                            excelWorksheet.Cells[1, 9].Value2 = "Totals";
                            excelWorksheet.Cells[2, 9].Value2 = txtTotals.Text;                           
                            excelWorksheet.Cells[1, 10].Value2 = "Discount";
                            if (txtStatus.Visibility == Visibility.Visible)
                            {
                                excelWorksheet.Cells[2, 10].Value2 = txtStatus.Text;
                                excelWorksheet.Cells[1, 11].Value2 = "Totals after discount";
                                excelWorksheet.Cells[2, 11].Value2 = txtTotals2.Text;

                                excelWorksheet.Cells[1, 11].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                                excelWorksheet.Cells[1, 11].Borders.LineStyle = 1;
                                excelWorksheet.Cells[1, 11].EntireColumn.AutoFit();
                                excelWorksheet.Cells[1, 11].HorizontalAlignment = XlHAlign.xlHAlignCenter;

                                excelWorksheet.Cells[2, 11].EntireColumn.NumberFormat = "@";
                                excelWorksheet.Cells[2, 11].EntireColumn.AutoFit();
                                excelWorksheet.Cells[2, 11].HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                excelWorksheet.Cells[2, 11].Borders.LineStyle = 1;
                            }
                            else
                            {
                                excelWorksheet.Cells[2, 8].Value2 = "No code applied";
                            }

                            excelWorksheet.Cells[1, 9].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                            excelWorksheet.Cells[1, 10].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                            excelWorksheet.Cells[1, 9].Borders.LineStyle = 1;
                            excelWorksheet.Cells[1, 10].Borders.LineStyle = 1;
                            excelWorksheet.Cells[1, 9].EntireColumn.AutoFit();
                            excelWorksheet.Cells[1, 10].EntireColumn.AutoFit();
                            excelWorksheet.Cells[1, 9].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                            excelWorksheet.Cells[1, 10].HorizontalAlignment = XlHAlign.xlHAlignCenter;

                            excelWorksheet.Cells[2, 9].EntireColumn.NumberFormat = "@";
                            excelWorksheet.Cells[2, 10].EntireColumn.NumberFormat = "@";
                            excelWorksheet.Cells[2, 9].EntireColumn.AutoFit();
                            excelWorksheet.Cells[2, 10].EntireColumn.AutoFit();
                            excelWorksheet.Cells[2, 9].HorizontalAlignment = XlHAlign.xlHAlignLeft;
                            excelWorksheet.Cells[2, 10].HorizontalAlignment = XlHAlign.xlHAlignLeft;
                            excelWorksheet.Cells[2, 9].Borders.LineStyle = 1;
                            excelWorksheet.Cells[2, 10].Borders.LineStyle = 1;

                            //UpdateFormat(filePath);
                            //Save workbook and close Excel application
                            excelWorkbook.SaveAs(filePath);
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

        // Button zone
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            new Checkout().Show();
            this.Close();
        }

        private void tbPostalCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(tbPostalCode.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }

        private void tbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(tbPhone.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }

        private void tbFax_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(tbFax.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }
    }
}