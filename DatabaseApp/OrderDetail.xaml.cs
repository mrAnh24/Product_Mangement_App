using Microsoft.Identity.Client;
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
    /// Interaction logic for OrderDetail.xaml
    /// </summary>
    public partial class OrderDetail : System.Windows.Window
    {
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        public string connectionString = "Data Source=OS-GPCP-GPDN171\\MSSQLSERVER01;Initial catalog=dbdemo;Persist Security info=True;Encrypt=false;;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true";
        public string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        public double notify;
        public string status;
        public string action;
        public string stage;
        public int x;

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

        public OrderDetail(System.Windows.Window parentWindow)
        {
            Owner = parentWindow; 
            InitializeComponent();
            GetInvoice();
            GetOrderStatus();
            txtHeader.Text = $"Order {AccountOrder.index} detail";
            x = 1;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            AccountOrder.index = null;
        }

        void NotifyCount()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from AccountLinked where Username = @username", con);
            cmd.Parameters.AddWithValue("@username", Login.passText);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                notify = Convert.ToDouble(da.GetValue(6)) + 1;
            }
            con.Close();

            con.Open();
            cmd = new SqlCommand("Update AccountLinked Set NotifyCount = @NotifyCount Where Username = @Username", con);
            cmd.Parameters.AddWithValue("@NotifyCount", notify);
            cmd.Parameters.AddWithValue("@Username", Login.passText);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void GetInvoice()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Customer where CustomerID = @CustomerID", con);
            cmd.Parameters.AddWithValue("@CustomerID", AccountOrder.index);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                txtCustomerID.Text = da.GetValue(1).ToString();
                txtInputName.Text = c5 = da.GetValue(5).ToString();
                cbGender.Text = c6 = da.GetValue(6).ToString();
                txtTitle.Text = c7 = da.GetValue(7).ToString();
                txtCompany.Text = c8 = da.GetValue(8).ToString();

                txtAddress.Text = c9 = da.GetValue(9).ToString();
                txtCity.Text = c10 = da.GetValue(10).ToString();
                txtRegion.Text = c11 = da.GetValue(11).ToString();
                txtPostalCode.Text = c12 = da.GetValue(12).ToString();
                txtCountry.Text = c13 = da.GetValue(13).ToString();

                txtPhoneNumber.Text = c14 = da.GetValue(14).ToString();
                txtFaxNumber.Text = c15 = da.GetValue(15).ToString();               
                cbPaymentMethod.Text = c16 = da.GetValue(16).ToString();
                txtCouponCode.Text = da.GetValue(18).ToString();
                status = da.GetValue(19).ToString();

                txtBill.Text = da.GetValue(17).ToString();
                txtCreatedDate.Text = da.GetValue(20).ToString();
            }
            con.Close();

            btnEdit.IsEnabled = false;
            btnEdit.Foreground = Brushes.Black;
            if (status == "Order cancel")
            {
                txtOrderStatus.Text = "  🚫  ";
            }
            else if (status == "Payment complete")
            {
                txtOrderStatus.Text = "  💸✓  ";
            }
            else
            {
                txtOrderStatus.Text = "  💸❌  ";
                btnEdit.IsEnabled = true;
                btnEdit.Foreground = Brushes.WhiteSmoke;
            }
        }

        private void cbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbGender.SelectedIndex.ToString())
            {
                case "0":
                    cbGender.Text = "Male";
                    break;
                case "1":
                    cbGender.Text = "Female";
                    break;
                case "2":
                    cbGender.Text = "Unknown";
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

        private void cbPaymentMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbPaymentMethod.SelectedIndex.ToString())
            {
                case "0":
                    cbPaymentMethod.Text = "Bank account";
                    break;
                case "1":
                    cbPaymentMethod.Text = "E-wallet";
                    break;
                case "2":
                    cbPaymentMethod.Text = "COD";
                    break;
            }
        }

        void ReadOnly()
        {
            txtInputName.IsEnabled = false;
            cbGender.IsEnabled = false;
            txtTitle.IsEnabled = false;
            txtCompany.IsEnabled = false;

            txtCountry.IsEnabled = false;
            txtRegion.IsEnabled = false;
            txtCity.IsEnabled = false;
            txtAddress.IsEnabled = false;
            txtPostalCode.IsEnabled = false;

            txtPhoneNumber.IsEnabled = false;
            txtFaxNumber.IsEnabled = false;
            cbPaymentMethod.IsEnabled = false;
        }

        void UpdateOrder()
        {
            action = $"Order {AccountOrder.index} information modified";

            // table Customer
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Customer SET InputName = @InputName, Title = @Title, Gender = @Gender, Company = @Company, Address = @Address, City = @City, Region = @Region, Country = @Country, Phone = @Phone, Fax = @Fax, PostalCode = @PostalCode, PaymentMethod = @PaymentMethod WHERE CustomerID = @CustomerID", con);
            cmd.Parameters.AddWithValue("@CustomerID", AccountOrder.index);
            cmd.Parameters.AddWithValue("@InputName", txtInputName.Text);
            cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
            cmd.Parameters.AddWithValue("@Gender", cbGender.Text);
            cmd.Parameters.AddWithValue("@Company", txtCompany.Text);
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@City", txtCity.Text);
            cmd.Parameters.AddWithValue("@Region", txtRegion.Text);
            cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
            cmd.Parameters.AddWithValue("@Phone", txtPhoneNumber.Text);
            cmd.Parameters.AddWithValue("@Fax", txtFaxNumber.Text);
            cmd.Parameters.AddWithValue("@PostalCode", txtPostalCode.Text);
            cmd.Parameters.AddWithValue("@PaymentMethod", cbPaymentMethod.Text);
            cmd.ExecuteNonQuery();
            con.Close();

            // table CustomerOrder
            con.Open();
            cmd = new SqlCommand("UPDATE CustomerOrder SET Name = @Name, PaymentStatus = @PaymentStatus WHERE CustomerID = @CustomerID", con);
            cmd.Parameters.AddWithValue("@CustomerID", AccountOrder.index);
            cmd.Parameters.AddWithValue("@Name", txtInputName.Text);
            cmd.Parameters.AddWithValue("@PaymentStatus", status);
            cmd.ExecuteNonQuery();
            con.Close();

            // table CustomerListFinal
            con.Open();
            cmd = new SqlCommand("UPDATE CustomerListFinal SET InputName = @InputName WHERE CreatedDate = @CreatedDate", con);
            cmd.Parameters.AddWithValue("@CreatedDate", AccountOrder.currentDate);
            cmd.Parameters.AddWithValue("@InputName", txtInputName.Text);
            cmd.ExecuteNonQuery();
            con.Close();

            ActivityLog();
            System.Windows.MessageBox.Show("Order Updated", "Notification");
            this.Close();
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

        void GetOrderStatus()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from CustomerOrder where CustomerID = @CustomerID", con);
            cmd.Parameters.AddWithValue("@CustomerID", AccountOrder.index);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                stage = da.GetValue(4).ToString();              
            }
            con.Close();

            if (stage == "Stage 0")
            {
                txtPaymentStatus.Text = "Checkout complete";
            }
            else if (stage == "Stage 1")
            {
                txtPaymentStatus.Text = "Admin approved";
            }
            else if (stage == "Stage 2")
            {
                txtPaymentStatus.Text = "Ready for deliver";
            }
            else if (stage == "Stage 3")
            {
                txtPaymentStatus.Text = "Delivering";
            }
            else if (stage == "Stage 4")
            {
                txtPaymentStatus.Text = "Complete";
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (x == 1)
            {              
                btnEdit.Content = "Confirm";

                txtInputName.IsEnabled = true;
                cbGender.IsEnabled = true;
                txtTitle.IsEnabled = true;
                txtCompany.IsEnabled = true;

                txtCountry.IsEnabled = true;
                txtRegion.IsEnabled = true;
                txtCity.IsEnabled = true;
                txtAddress.IsEnabled = true;
                txtPostalCode.IsEnabled = true;

                txtPhoneNumber.IsEnabled = true;
                txtFaxNumber.IsEnabled = true;

                if (status == "Payment incomplete")
                {
                    cbPaymentMethod.IsEnabled = true;
                }
                x = 2;
            }
            else
            {
                if (txtInputName.Text == c5 && cbGender.Text == c6 && txtTitle.Text == c7 && txtCompany.Text == c8 
                    && txtAddress.Text == c9 && txtCity.Text == c10 && txtRegion.Text == c11 && txtPostalCode.Text == c12 
                    && txtCountry.Text == c13 && txtPhoneNumber.Text == c14 && txtFaxNumber.Text == c15 
                    && cbPaymentMethod.Text == c16)
                {
                    System.Windows.MessageBox.Show("No change was made", "Notify");
                    ReadOnly();
                    btnEdit.Content = "Edit";
                    x = 1;
                }
                else
                {
                    var result = System.Windows.MessageBox.Show($"Change the order {AccountOrder.index} information?", "Notification", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        UpdateOrder();
                        x = 1;
                        System.Windows.MessageBox.Show($"Order {AccountOrder.index} information change", "Notification");
                        NotifyCount();

                        con.Open();
                        SqlCommand cmd = new SqlCommand($"INSERT INTO AccountNotify VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + "Change an order information" + "','" + $"{Login.passText} ({Login.GetRole}) change order {AccountOrder.index} information" + "','" + "Data modified" + "','" + "none" + "', '" + "Complete" + "', '" + currentdatetime + "')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        this.Close();
                    }
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Order receipt print (--Currently order's product list--)
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            string currentdatetime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string LogFolder = @"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs";
            string queryString = "SELECT * FROM Customer WHERE CustomerID = @CustomerID";
            string filePath = $@"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs\Receipts\ Order {AccountOrder.index}_{txtInputName.Text} information.XLSX";
            action = $"Order {AccountOrder.index} customer informations printed";

            var result = System.Windows.MessageBox.Show("Print the receipt?", "Notification", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
            if(result == MessageBoxResult.Yes)
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

        private void txtOrderStatus_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.MessageBox.Show($"{status}", "Information");
        }
    }
}
