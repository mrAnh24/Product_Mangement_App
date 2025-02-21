using DatabaseApp.Data;
using DatabaseApp.Data.DataModels;
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
using Window = System.Windows.Window;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for OrderManagementDetail.xaml
    /// </summary>
    public partial class OrderManagementDetail : Window
    {
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        public string connectionString = "Data Source=OS-GPCP-GPDN171\\MSSQLSERVER01;Initial catalog=dbdemo;Persist Security info=True;Encrypt=false;;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true";
        public string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        List<CustomerListFinal> products = new List<CustomerListFinal>();

        public string chosen;
        public string status;
        public string action;
        public string stage;
        public DateTime currentDate;
        public int x;

        public string OrderStatus;
        public string OrderStage;
        public string Deliver1;
        public string Deliver2;
        public string Deliver3;
        public string Deliver4;

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

        public OrderManagementDetail(Window parentWindow)
        {
            Owner = parentWindow; 
            InitializeComponent();
            GetInvoice();
            GetOrderStatus();
            GetProducts();
            LoadOrder();
            CurrentStage();
            OnFoot();
            Underliver();
            txtHeader.Text = $"Customer {OrderManagement.index} Order detail";
            x = 1;

            if (OrderStatus == "Order cancel")
            {
                cbStage.IsEnabled = false;
                cbPartner.IsEnabled = false;

                cbPartner.Text = "Order cancel";
                cbMethod.Text = "Order cancel";
                cbVehicled.Text = "Order cancel";
                cbHidden.Visibility = Visibility.Visible;
                cbHidden.Text = "Order cancel";

                btnRevert.IsEnabled = false;
                btnConfirm.IsEnabled = false;

                btnRevert.Foreground = Brushes.Black;
                btnConfirm.Foreground = Brushes.Black;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //Tab 1

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

        public void ActivityLog()
        {
            con.Open();
            string query = "INSERT INTO ActivityLog VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + action + "', '" + "Checkout status" + "', '" + currentdatetime + "')"; ;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void GetOrderStatus()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from CustomerOrder where CustomerID = @CustomerID", con);
            cmd.Parameters.AddWithValue("@CustomerID", OrderManagement.index);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                stage = da.GetValue(4).ToString();
                cbPartner.Text = da.GetValue(5).ToString();
                cbMethod.Text = da.GetValue(6).ToString();
                cbVehicled.Text = da.GetValue(7).ToString();
            }
            con.Close();

            if (OrderManagement.status == "Order cancel")
            {
                txtPaymentStatus.Text = "Order cancel";
            }
            else
            {
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
        }

        void GetInvoice()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Customer where CustomerID = @CustomerID", con);
            cmd.Parameters.AddWithValue("@CustomerID", OrderManagement.index);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                txtCustomerID.Text = da.GetValue(1).ToString();
                txtName.Text = c5 = da.GetValue(5).ToString();
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
                txtDate.Text = da.GetValue(20).ToString();
                currentDate = Convert.ToDateTime(da.GetValue(20));
            }
            con.Close();

            if (status == "Order cancel")
            {
                txtOrderStatus.Text = "  🚫  ";
                btnEdit.IsEnabled = false;
                btnEdit.Foreground = Brushes.Black;
            }
            else if (status == "Payment complete")
            {
                txtOrderStatus.Text = "  💸✓  ";
            }
            else
            {
                txtOrderStatus.Text = "  💸❌  ";
            }
        }

        void ReadOnly()
        {
            txtName.IsEnabled = false;
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
            cmd.Parameters.AddWithValue("@CustomerID", OrderManagement.index);
            cmd.Parameters.AddWithValue("@InputName", txtName.Text);
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
            cmd.Parameters.AddWithValue("@CustomerID", OrderManagement.index);
            cmd.Parameters.AddWithValue("@Name", txtName.Text);
            cmd.Parameters.AddWithValue("@PaymentStatus", status);
            cmd.ExecuteNonQuery();
            con.Close();

            // table CustomerListFinal
            con.Open();
            cmd = new SqlCommand("UPDATE CustomerListFinal SET InputName = @InputName WHERE CreatedDate = @CreatedDate", con);
            cmd.Parameters.AddWithValue("@CreatedDate", OrderManagement.currentDate);
            cmd.Parameters.AddWithValue("@InputName", txtName.Text);
            cmd.ExecuteNonQuery();
            con.Close();

            ActivityLog();
            System.Windows.MessageBox.Show("Order Updated", "Notification");
            this.Close();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (x == 1)
            {
                btnEdit.Content = "         Confirm        ";

                txtName.IsEnabled = true;
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
                if (txtName.Text == c5 && cbGender.Text == c6 && txtTitle.Text == c7 && txtCompany.Text == c8
                    && txtAddress.Text == c9 && txtCity.Text == c10 && txtRegion.Text == c11 && txtPostalCode.Text == c12
                    && txtCountry.Text == c13 && txtPhoneNumber.Text == c14 && txtFaxNumber.Text == c15
                    && cbPaymentMethod.Text == c16)
                {
                    System.Windows.MessageBox.Show("No change was made", "Notify");
                    ReadOnly();
                    btnEdit.Content = "           Edit            ";
                    x = 1;
                }
                else
                {
                    var result = System.Windows.MessageBox.Show($"Change the order {AccountOrder.index} information?", "Notification", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        UpdateOrder();
                        x = 1;
                        System.Windows.MessageBox.Show("Order information change", "Notification");
                        this.Close();
                    }
                }
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            string LogFolder = @"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs";
            string queryString = "SELECT * FROM Customer WHERE CustomerID = @CustomerID";
            string filePath = $@"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs\Receipts\OrderInformation\ Order {OrderManagement.index}_{txtName.Text} information.XLSX";
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
                            command.Parameters.AddWithValue("CustomerID", OrderManagement.index);
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


        //Tab 2

        void GetProducts()
        {
            var db = new CustomerListFinalDb();
            products = db.customerListFinal.ToList();
            dgProduct.ItemsSource = products;
        }

        void GetProductInfo()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from ProductLists where ProductCode = @ProductCode", con);
            cmd.Parameters.AddWithValue("@ProductCode", chosen);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                txtProduct.Text = da.GetValue(1).ToString();
                txtProductCode.Text = da.GetValue(0).ToString();
                txtPrice.Text = da.GetValue(4).ToString();
                txtType.Text = da.GetValue(3).ToString();
            }
            con.Close();
        }

        void ShowResult()
        {
            dgProduct.Columns[0].Visibility = Visibility.Hidden;
            dgProduct.Columns[1].Visibility = Visibility.Hidden;
            dgProduct.Columns[2].Visibility = Visibility.Hidden;
            dgProduct.Columns[3].Visibility = Visibility.Hidden;
            dgProduct.Columns[5].Header = "PCode";
            dgProduct.Columns[8].Visibility = Visibility.Hidden;

            products.RemoveAll(x => x.CreatedDate != currentDate);
            txtCountInput.Text = dgProduct.Items.Count.ToString();
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            var row = sender as DataGridRow;
            var select = row.DataContext as CustomerListFinal;
            chosen = select.ProductCode;
            GetProductInfo();
        }

        private void dgProduct_AutoGeneratedColumns(object sender, EventArgs e)
        {
            ShowResult();
        }

        private void btnPrint2_Click(object sender, RoutedEventArgs e)
        {
            string LogFolder = @"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs";
            string queryString = "SELECT * FROM CustomerListFinal WHERE InputName = @InputName AND CreatedDate = @CreatedDate";
            string filePath = $@"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs\Receipts\OrderProducts\ Order {OrderManagement.index}_{OrderManagement.inputName} products.XLSX";

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
                            command.Parameters.AddWithValue("InputName", OrderManagement.inputName);
                            command.Parameters.AddWithValue("CreatedDate", OrderManagement.currentDate);
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
                                        excelWorksheet.Columns["E"].NumberFormat = "yyyy-MM-dd HH:mm:ss";
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

        //Tab 3

        //Order current stage comboBox
        private void cbStage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbPartner.IsEnabled = false;
            Revert();
            switch (cbStage.SelectedIndex.ToString())
            {
                case "0":
                    OrderStage = "Stage 0";
                    CurrentStage();
                    cbStage.Text = "Checkout complete";
                    cbVehicled.IsEnabled = false;
                    cbVehicled.Text = "";
                    cbPartner.Text = "";
                    break;
                case "1":
                    OrderStage = "Stage 1";
                    CurrentStage();
                    cbStage.Text = "Admin approved";
                    cbVehicled.IsEnabled = false;
                    cbVehicled.Text = "";
                    cbPartner.Text = "";
                    break;
                case "2":
                    OrderStage = "Stage 2";
                    CurrentStage();
                    cbStage.Text = "Ready for deliver";
                    cbPartner.IsEnabled = true;
                    break;
                case "3":
                    OrderStage = "Stage 3";
                    CurrentStage();
                    cbStage.Text = "Delivering";
                    cbPartner.IsEnabled = true;
                    break;
                case "4":
                    OrderStage = "Stage 4";
                    CurrentStage();
                    cbStage.Text = "Order complete";
                    cbPartner.IsEnabled = true;
                    break;
            }
        }

        //Delivery partner comboBox
        private void cbPartner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbMethod.IsEnabled = false;
            cbMethod.Text = "";
            Underliver();
            switch (cbPartner.SelectedIndex.ToString())
            {
                case "0":
                    cbPartner.Text = "Default";
                    cbMethod.IsEnabled = true;
                    break;
                case "1":
                    cbPartner.Text = "Faster";
                    cbMethod.IsEnabled = true;
                    Sea.Visibility = Visibility.Visible;
                    break;
                case "2":
                    cbPartner.Text = "Express";
                    cbMethod.IsEnabled = true;
                    Sea.Visibility = Visibility.Visible;
                    Air.Visibility = Visibility.Visible;
                    break;
                case "3":
                    cbPartner.Text = "RightAtYourDoorStep";
                    cbMethod.IsEnabled = true;
                    Sea.Visibility = Visibility.Visible;
                    Air.Visibility = Visibility.Visible;
                    Space.Visibility = Visibility.Visible;
                    break;
                case "4":
                    cbPartner.Text = "LookBehindYou";
                    cbMethod.IsEnabled = true;
                    Land.Visibility = Visibility.Collapsed;
                    Sea.Visibility = Visibility.Visible;
                    Air.Visibility = Visibility.Visible;
                    Space.Visibility = Visibility.Visible;
                    break;
            }
        }

        //Deliver method comboBox
        private void cbMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbVehicled.IsEnabled = false;
            cbVehicled.Text = "";
            OnFoot();
            switch (cbMethod.SelectedIndex.ToString())
            {
                case "0":
                    cbMethod.Text = "Inland";
                    L1.Visibility = L2.Visibility = L3.Visibility
                    = L4.Visibility = L5.Visibility = Visibility.Visible;
                    cbVehicled.IsEnabled = true;
                    break;
                case "1":
                    cbMethod.Text = "Oversea";
                    O1.Visibility = O2.Visibility = Visibility.Visible;
                    cbVehicled.IsEnabled = true;
                    break;
                case "2":
                    cbMethod.Text = "Airborne";
                    A1.Visibility = A2.Visibility = Visibility.Visible;
                    cbVehicled.IsEnabled = true;
                    break;
                case "3":
                    cbMethod.Text = "OuterSpace";
                    S1.Visibility = S2.Visibility = Visibility.Visible;
                    cbVehicled.IsEnabled = true;
                    break;
            }
        }

        //Vehicle comboBox
        private void cbVehicled_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbVehicled.SelectedIndex.ToString())
            {
                case "0":
                    cbVehicled.Text = "Bicycle";
                    break;
                case "1":
                    cbVehicled.Text = "Motorbike";
                    break;
                case "2":
                    cbVehicled.Text = "Car";
                    break;
                case "3":
                    cbVehicled.Text = "Truck";
                    break;
                case "4":
                    cbVehicled.Text = "Container";
                    break;
                case "5":
                    cbVehicled.Text = "Ferry";
                    break;
                case "6":
                    cbVehicled.Text = "Boat";
                    break;
                case "7":
                    cbVehicled.Text = "Helicopter";
                    break;
                case "8":
                    cbVehicled.Text = "Plane";
                    break;
                case "9":
                    cbVehicled.Text = "Space shutter";
                    break;
                case "10":
                    cbVehicled.Text = "Rocket";
                    break;
            }
        }

        void Underliver()
        {
            Sea.Visibility = Air.Visibility = Space.Visibility = Visibility.Collapsed;
        }

        void OnFoot()
        {
              L1.Visibility = L2.Visibility = L3.Visibility = L4.Visibility 
            = L5.Visibility = O1.Visibility = O2.Visibility = A1.Visibility
            = A2.Visibility = S1.Visibility = S2.Visibility = Visibility.Collapsed;
        }

        // Approve zone
        void Stage1()
        {
            CircleProgress0.Fill = Brushes.ForestGreen;
            LineProgress0.Fill = Brushes.ForestGreen;
            LineProgress1L.Fill = Brushes.ForestGreen;
            CircleProgress1.Fill = Brushes.ForestGreen;
        }
        void Stage2()
        {
            Stage1();
            LineProgress1R.Fill = Brushes.ForestGreen;
            LineProgress2L.Fill = Brushes.ForestGreen;
            CircleProgress2.Fill = Brushes.ForestGreen;

        }
        void Stage3()
        {
            Stage2();
            LineProgress2R.Fill = Brushes.ForestGreen;
            LineProgress3L.Fill = Brushes.ForestGreen;
            CircleProgress3.Fill = Brushes.ForestGreen;
        }
        void Stage4()
        {
            Stage3();
            LineProgress3R.Fill = Brushes.ForestGreen;
            LineProgress4.Fill = Brushes.ForestGreen;
            CircleProgress4.Fill = Brushes.ForestGreen;
        }

        void Revert()
        {
            LineProgress0.Fill = Brushes.WhiteSmoke;
            LineProgress1L.Fill = Brushes.WhiteSmoke;
            CircleProgress1.Fill = Brushes.WhiteSmoke;

            LineProgress1R.Fill = Brushes.WhiteSmoke;
            LineProgress2L.Fill = Brushes.WhiteSmoke;
            CircleProgress2.Fill = Brushes.WhiteSmoke;

            LineProgress2R.Fill = Brushes.WhiteSmoke;
            LineProgress3L.Fill = Brushes.WhiteSmoke;
            CircleProgress3.Fill = Brushes.WhiteSmoke;

            LineProgress3R.Fill = Brushes.WhiteSmoke;
            LineProgress4.Fill = Brushes.WhiteSmoke;
            CircleProgress4.Fill = Brushes.WhiteSmoke;
        }

        // Cancel zone
        void Stage0Cancel()
        {
            CircleProgress0.Fill = Brushes.Red;
        }

        void Stage1Cancel()
        {
            Stage0Cancel();
            LineProgress0.Fill = Brushes.Red;
            LineProgress1L.Fill = Brushes.Red;
            CircleProgress1.Fill = Brushes.Red;
        }
        void Stage2Cancel()
        {
            Stage1Cancel();
            LineProgress1R.Fill = Brushes.Red;
            LineProgress2L.Fill = Brushes.Red;
            CircleProgress2.Fill = Brushes.Red;

        }
        void Stage3Cancel()
        {
            Stage2Cancel();
            LineProgress2R.Fill = Brushes.Red;
            LineProgress3L.Fill = Brushes.Red;
            CircleProgress3.Fill = Brushes.Red;
        }
        void Stage4Cancel()
        {
            Stage3Cancel();
            LineProgress3R.Fill = Brushes.Red;
            LineProgress4.Fill = Brushes.Red;
            CircleProgress4.Fill = Brushes.Red;
        }

        void LoadOrder()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from CustomerOrder where CustomerID = @CustomerID", con);
            cmd.Parameters.AddWithValue("@CustomerID", OrderManagement.index);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                OrderStatus = da.GetValue(3).ToString();
                OrderStage = da.GetValue(4).ToString();
                Deliver1 = da.GetValue(5).ToString();
                Deliver2 = da.GetValue(6).ToString();
                Deliver3 = da.GetValue(7).ToString();
            }
            con.Close();
        }

        void CurrentStage()
        {
            if (OrderStage == "Stage 0")
            {
                cbStage.Text = "Checkout complete";
                Deliver4 = "Stage 0";
                if (OrderStatus == "Order cancel")
                {
                    Stage0Cancel();
                }
            }
            else if (OrderStage == "Stage 1")
            {
                cbStage.Text = "Admin approved";
                Deliver4 = "Stage 1";
                if (OrderStatus == "Order cancel")
                {
                    Stage1Cancel();
                }
                else
                {
                    Stage1();
                }
            }
            else if (OrderStage == "Stage 2")
            {
                cbStage.Text = "Ready for deliver";
                Deliver4 = "Stage 2";
                if (OrderStatus == "Order cancel")
                {
                    Stage2Cancel();
                }
                else
                {
                    Stage2();
                }
            }
            else if (OrderStage == "Stage 3")
            {
                cbStage.Text = "Delivering";
                Deliver4 = "Stage 3";
                if (OrderStatus == "Order cancel")
                {
                    Stage3Cancel();
                }
                else
                {
                    Stage3();
                }
            }
            else if (OrderStage == "Stage 4")
            {
                cbStage.Text = "Order complete";
                Deliver4 = "Stage 4";
                if (OrderStatus == "Order cancel")
                {
                    Stage4Cancel();
                }
                else
                {
                    Stage4();
                }
            }
        }

        private void btnRevert_Click(object sender, RoutedEventArgs e)
        {
            LoadOrder();
            GetOrderStatus();
            CurrentStage();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (OrderStatus == cbStage.Text && Deliver1 == cbPartner.Text && Deliver2 == cbMethod.Text && Deliver3 == cbVehicled.Text)
            {
                System.Windows.MessageBox.Show("No change was made","Notification");
            }
            else
            {
                var result = System.Windows.MessageBox.Show($"Change the order {AccountOrder.index} status?", "Notification", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE CustomerOrder SET OrderStatus = @OrderStatus, DeliveryPartner = @DeliveryPartner, DeliveryMethod = @DeliveryMethod, Vehicle = @Vehicle WHERE CustomerID = @CustomerID", con);
                    cmd.Parameters.AddWithValue("@CustomerID", OrderManagement.index);
                    cmd.Parameters.AddWithValue("@OrderStatus", Deliver4);
                    cmd.Parameters.AddWithValue("@DeliveryPartner", cbPartner.Text);
                    cmd.Parameters.AddWithValue("@DeliveryMethod", cbMethod.Text);
                    cmd.Parameters.AddWithValue("@Vehicle", cbVehicled.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    cmd = new SqlCommand($"INSERT INTO AccountNotify VALUES ('" + OrderManagement.userID + "','" + OrderManagement.userName + "','" + OrderManagement.userRole + "','" + $"Order {OrderManagement.index} status change" + "','" + $"{Login.passText} change order {OrderManagement.index} status" + "','" + "Data modified" + "','" + "none" + "', '" + "Complete" + "', '" + currentdatetime + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    System.Windows.MessageBox.Show("Order updated", "Notification");
                    this.Close();
                }
            }
        }

        private void btnPrint3_Click(object sender, RoutedEventArgs e)
        {
            string LogFolder = @"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs";
            string queryString = "SELECT * FROM CustomerOrder WHERE CustomerID = @CustomerID";
            string filePath = $@"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs\Receipts\OrderStatus\ Order {OrderManagement.index}_{OrderManagement.inputName} status.XLSX";

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
                            command.Parameters.AddWithValue("CustomerID", OrderManagement.index);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                //Create new Excel application and workbook
                                Application excelApp = new Application();
                                Workbook excelWorkbook = excelApp.Workbooks.Add();
                                Worksheet excelWorksheet = excelWorkbook.Worksheets[1];

                                //Add the headers to first row
                                int col = 1;
                                for (int i = 1; i < reader.FieldCount; i++)
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
                                    for (int i = 1; i < reader.FieldCount; i++)
                                    {
                                        excelWorksheet.Cells[row, col].EntireColumn.NumberFormat = "@";
                                        excelWorksheet.Cells[row, col].Value2 = reader[i];
                                        excelWorksheet.Cells[row, col].EntireColumn.AutoFit();
                                        excelWorksheet.Cells[row, col].HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                        excelWorksheet.Cells[row, col].Borders.LineStyle = 1;
                                        col++;
                                    }
                                    row++;
                                }

                                //if (OrderStage == "Stage 1") //Can't see order detail at stage 1
                                //{
                                //    excelWorksheet.Cells[2, 4].Value2 = "Admin approved";
                                //    excelWorksheet.Cells[2, 5].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                                //    excelWorksheet.Cells[2, 6].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                                //    excelWorksheet.Cells[2, 7].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                                //}
                                if (OrderStage == "Stage 2")
                                {
                                    excelWorksheet.Cells[2, 4].Value2 = "Ready for deliver";

                                    if(excelWorksheet.Cells[2, 6].Value2 == "")
                                    {
                                        excelWorksheet.Cells[2, 6].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                                        excelWorksheet.Cells[2, 7].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                                    }

                                    if (excelWorksheet.Cells[2, 7].Value2 == "")
                                    {
                                        excelWorksheet.Cells[2, 7].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                                    }

                                }
                                else if (OrderStage == "Stage 3")
                                {
                                    excelWorksheet.Cells[2, 4].Value2 = "Delivering";

                                    if (excelWorksheet.Cells[2, 6].Value2 == "")
                                    {
                                        excelWorksheet.Cells[2, 6].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                                        excelWorksheet.Cells[2, 7].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                                    }

                                    if (excelWorksheet.Cells[2, 7].Value2 == "")
                                    {
                                        excelWorksheet.Cells[2, 7].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                                    }
                                }
                                else if (OrderStage == "Stage 4")
                                {
                                    excelWorksheet.Cells[2, 4].Value2 = "Order complete";
                                }
                                excelWorksheet.Cells[2, 4].EntireColumn.AutoFit();

                                if (OrderStatus == "Order cancel")
                                {
                                    excelWorksheet.Cells[2, 4].Value2
                                  = excelWorksheet.Cells[2, 5].Value2
                                  = excelWorksheet.Cells[2, 6].Value2
                                  = excelWorksheet.Cells[2, 7].Value2
                                  = "Order cancel";

                                    excelWorksheet.Cells[2, 4].EntireColumn.AutoFit();
                                    excelWorksheet.Cells[2, 5].EntireColumn.AutoFit();
                                    excelWorksheet.Cells[2, 6].EntireColumn.AutoFit();
                                    excelWorksheet.Cells[2, 7].EntireColumn.AutoFit();
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
    }
}
