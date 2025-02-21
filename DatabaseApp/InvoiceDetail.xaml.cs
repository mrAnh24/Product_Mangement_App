using DatabaseApp.Data.DataModels;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
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
using Application = Microsoft.Office.Interop.Excel.Application;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for InvoiceDetail.xaml
    /// </summary>
    public partial class InvoiceDetail : System.Windows.Window
    {
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        public string connectionString = "Data Source=OS-GPCP-GPDN171\\MSSQLSERVER01;Initial catalog=dbdemo;Persist Security info=True;Encrypt=false;;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true";
        public string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string status;
        public string title;
        public string action;
        public int x;
        public double notify;

        //--temp--//
        public string c5;
        public string c6;
        public string c7;
        public string c8;
        public string c9;
        public string c10;
        public string c11;
        public string c12;
        public string c13;
        public string c14;
        public string c15;
        public string c16;
        //--temp--//

        public InvoiceDetail(System.Windows.Window parentWindow)
        {
            Owner = parentWindow;
            InitializeComponent();
            GetInvoice();
            txtHeader.Text = title + "'s invoice details";
            x = 1;
        }

        void GetInvoice()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Customer where CustomerID = @CustomerID", con);
            cmd.Parameters.AddWithValue("@CustomerID", InvoiceManagement.index);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                txtCustomerID.Text = da.GetValue(1).ToString();
                txtAccountID.Text = da.GetValue(2).ToString();
                txtUsername.Text = da.GetValue(3).ToString();
                txtRole.Text = da.GetValue(4).ToString();
                txtInputName.Text = title = c5 = da.GetValue(5).ToString();
                txtTitle.Text = c7 = da.GetValue(7).ToString();

                txtGender.Text = c6 = da.GetValue(6).ToString();
                txtCompany.Text = c8 = da.GetValue(8).ToString();
                txtAddress.Text = c9 = da.GetValue(9).ToString();
                txtCity.Text = c10 = da.GetValue(10).ToString();
                txtRegion.Text = c11 = da.GetValue(11).ToString();
                txtCountry.Text = c13 = da.GetValue(13).ToString();

                txtPhoneNumber.Text = c14 = da.GetValue(14).ToString();
                txtFaxNumber.Text = c15 = da.GetValue(15).ToString();
                txtPostalCode.Text = c12 = da.GetValue(12).ToString();
                txtPaymentMethod.Text = c16 = da.GetValue(16).ToString();
                txtCouponCode.Text = da.GetValue(18).ToString();
                txtBill.Text = da.GetValue(17).ToString();

                txtDayCreated.Text = da.GetValue(20).ToString();
                status = da.GetValue(19).ToString();
            }
            con.Close();

            if (status == "Order cancel")
            {
                ReadOnly();
                btnConfirm.IsEnabled = false;
                btnConfirm.Foreground = Brushes.Black;
                btnCancel.IsEnabled = false;
                btnCancel.Foreground = Brushes.Black;
                txtPaymentStatus.Text = "  🚫  ";
            }
            else if (status == "Payment complete")
            {
                txtPaymentStatus.Text = "  💸✓  ";
            }
            else
            {
                txtPaymentStatus.Text = "  💸❌  ";
            }
        }

        void ReadOnly()
        {
            txtInputName.IsEnabled = false;
            txtTitle.IsEnabled = false;

            txtGender.IsEnabled = false;
            txtCompany.IsEnabled = false;
            txtCountry.IsEnabled = false;
            txtRegion.IsEnabled = false;
            txtCity.IsEnabled = false;
            txtAddress.IsEnabled = false;

            txtPostalCode.IsEnabled = false;
            txtPhoneNumber.IsEnabled = false;
            txtFaxNumber.IsEnabled = false;
            txtPaymentMethod.IsEnabled = false;
        }

        void Edit()
        {
            txtInputName.IsEnabled = true;
            txtTitle.IsEnabled = true;

            txtGender.IsEnabled = true;
            txtCompany.IsEnabled = true;
            txtCountry.IsEnabled = true;
            txtRegion.IsEnabled = true;
            txtCity.IsEnabled = true;
            txtAddress.IsEnabled = true;

            txtPostalCode.IsEnabled = true;
            txtPhoneNumber.IsEnabled = true;
            txtFaxNumber.IsEnabled = true;
            
            if(txtPaymentStatus.Text == "Payment incomplete")
            {
                txtPaymentMethod.IsEnabled = true;
            }
        }

        void NotifyCount()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from AccountLinked where Username = @username", con);
            cmd.Parameters.AddWithValue("@username", txtUsername.Text);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                notify = Convert.ToDouble(da.GetValue(6)) + 1;
            }
            con.Close();

            con.Open();
            cmd = new SqlCommand("Update AccountLinked Set NotifyCount = @NotifyCount Where Username = @Username", con);
            cmd.Parameters.AddWithValue("@NotifyCount", notify);
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void ActivityLog()
        {
            con.Open();
            string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "INSERT INTO ActivityLog VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + action + "', '" + "Checkout status" + "', '" + currentdatetime + "')"; ;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void txtGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (txtGender.SelectedIndex.ToString())
            {
                case "0":
                    txtGender.Text = "Male";
                    break;
                case "1":
                    txtGender.Text = "Female";
                    break;
                case "2":
                    txtGender.Text = "Unknown";
                    break;
            }
        }

        private void txtPaymentMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (txtPaymentMethod.SelectedIndex.ToString())
            {
                case "0":
                    txtPaymentMethod.Text = "Bank account";
                    break;
                case "1":
                    txtPaymentMethod.Text = "E-wallet";
                    break;
                case "2":
                    txtPaymentMethod.Text = "COD";
                    break;
            }
        }

        private void txtRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (txtRegion.SelectedIndex.ToString())
            {
                case "0":
                    txtRegion.Text = "North";
                    break;
                case "1":
                    txtRegion.Text = "East";
                    break;
                case "2":
                    txtRegion.Text = "Middle";
                    break;
                case "3":
                    txtRegion.Text = "West";
                    break;
                case "4":
                    txtRegion.Text = "South";
                    break;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            InvoiceManagement.index = null;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (x == 1)
            {
                btnConfirm.Content = "Confirm";
                Edit();
                x =2;
            }
            else
            {
                if (txtInputName.Text == c5 && txtGender.Text == c6 && txtTitle.Text == c7 && txtCompany.Text == c8
                    && txtAddress.Text == c9 && txtCity.Text == c10 && txtRegion.Text == c11 && txtPostalCode.Text == c12
                    && txtCountry.Text == c13 && txtPhoneNumber.Text == c14 && txtFaxNumber.Text == c15
                    && txtPaymentMethod.Text == c16)
                {
                    System.Windows.MessageBox.Show("No change was made", "Notify");
                    ReadOnly();
                    btnConfirm.Content = "Edit";
                    x = 1;
                }
                else
                {
                    action = $"Order {InvoiceManagement.index} information modified";
                    var result = System.Windows.MessageBox.Show($"Change order {AccountOrder.index} information?", "Notification", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        // table Customer
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE Customer SET InputName = @InputName, Title = @Title, Gender = @Gender, Company = @Company, Address = @Address, City = @City, Region = @Region, Country = @Country, Phone = @Phone, Fax = @Fax, PostalCode = @PostalCode, PaymentMethod = @PaymentMethod WHERE CustomerID = @CustomerID", con);
                        cmd.Parameters.AddWithValue("@CustomerID", InvoiceManagement.index);
                        cmd.Parameters.AddWithValue("@InputName", txtInputName.Text);
                        cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                        cmd.Parameters.AddWithValue("@Gender", txtGender.Text);
                        cmd.Parameters.AddWithValue("@Company", txtCompany.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@City", txtCity.Text);
                        cmd.Parameters.AddWithValue("@Region", txtRegion.Text);
                        cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                        cmd.Parameters.AddWithValue("@Phone", txtPhoneNumber.Text);
                        cmd.Parameters.AddWithValue("@Fax", txtFaxNumber.Text);
                        cmd.Parameters.AddWithValue("@PostalCode", txtPostalCode.Text);
                        cmd.Parameters.AddWithValue("@PaymentMethod", txtPaymentMethod.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        // table CustomerOrder
                        con.Open();
                        cmd = new SqlCommand("UPDATE CustomerOrder SET Name = @Name, PaymentStatus = @PaymentStatus WHERE CustomerID = @CustomerID", con);
                        cmd.Parameters.AddWithValue("@CustomerID", InvoiceManagement.index);
                        cmd.Parameters.AddWithValue("@Name", txtInputName.Text);
                        cmd.Parameters.AddWithValue("@PaymentStatus", status);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        // table CustomerListFinal
                        con.Open();
                        cmd = new SqlCommand("UPDATE CustomerListFinal SET InputName = @InputName WHERE CreatedDate = @CreatedDate", con);
                        cmd.Parameters.AddWithValue("@CreatedDate", InvoiceManagement.currentDateTime);
                        cmd.Parameters.AddWithValue("@InputName", txtInputName.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        ActivityLog();
                        NotifyCount();

                        con.Open();
                        cmd = new SqlCommand($"INSERT INTO AccountNotify VALUES ('" + txtAccountID.Text + "','" + txtUsername.Text + "','" + txtRole.Text + "','" + $"Order {InvoiceManagement.index} information change" + "','" + $"{Login.passText} ({Login.GetRole}) change order {AccountOrder.index} information" + "','" + "Data modified" + "','" + "admin" + "', '" + "Complete" + "', '" + currentdatetime + "')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        System.Windows.MessageBox.Show("Order Updated", "Notification");
                        this.Close();
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            status = "Order cancel";
            action = $" Order {InvoiceManagement.index} canceled";

            var result = System.Windows.MessageBox.Show("This action is permanent, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
            if (result == MessageBoxResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Customer SET PaymentStatus = @PaymentStatus WHERE CustomerID = @CustomerID", con);
                cmd.Parameters.AddWithValue("@CustomerID", InvoiceManagement.index);
                cmd.Parameters.AddWithValue("@PaymentStatus", status);
                cmd.ExecuteNonQuery();
                con.Close();

                ActivityLog();
                NotifyCount();

                con.Open();
                cmd = new SqlCommand($"INSERT INTO AccountNotify VALUES ('" + txtAccountID.Text + "','" + txtUsername.Text + "','" + txtRole.Text + "','" + $"Order {InvoiceManagement.index} had been canceled" + "','" + $"{Login.passText} ({Login.GetRole}) change order {InvoiceManagement.index} information" + "','" + "Data modified" + "','" + "admin" + "', '" + "Complete" + "', '" + currentdatetime + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();

                System.Windows.MessageBox.Show("Order cancel", "Notification");
                this.Close();
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            string currentdatetime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string LogFolder = @"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs";
            string queryString = "SELECT * FROM Customer WHERE CustomerID = @CustomerID";
            string filePath = $@"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs\Receipts\ Order {AccountOrder.index}_{txtInputName.Text} information.XLSX";
            action = $"Order {AccountOrder.index} customer informations printed";

            var result = System.Windows.MessageBox.Show("Print the receipt?", "Notification", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(queryString, connection))
                        {
                            command.Parameters.AddWithValue("CustomerID", AccountOrder.index);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                //Create new Excel application and workbook
                                Application excelApp = new Application();
                                Workbook excelWorkbook = excelApp.Workbooks.Add();
                                Worksheet excelWorksheet = excelWorkbook.Worksheets[1];

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
                                        excelWorksheet.Columns["Q"].NumberFormat = "yyyy-MM-dd HH:mm:ss";
                                        col++;
                                    }
                                    row++;
                                }
                                //UpdateFormat(filePath);
                                //Save workbook and close Excel application
                                excelWorkbook.SaveAs(filePath);
                                excelWorkbook.Close();
                                excelApp.Quit();

                                ActivityLog();
                                System.Windows.MessageBox.Show("Receipt printed");
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
