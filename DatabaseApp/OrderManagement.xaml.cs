using DatabaseApp.Data;
using DatabaseApp.Data.DataModels;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office.Word;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
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
using MessageBox = System.Windows.MessageBox;
using Window = System.Windows.Window;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for OrderManagement.xaml
    /// </summary>
    public partial class OrderManagement : Window
    {
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        public string connectionString = "Data Source=OS-GPCP-GPDN171\\MSSQLSERVER01;Initial catalog=dbdemo;Persist Security info=True;Encrypt=false;;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true";
        public string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        List<CustomerOrder> customerOrders = new List<CustomerOrder>();

        public static string index;
        public static string inputName;
        public static DateTime currentDate;
        public static string status;
        public static string stage;
        public string query;
        public string action;

        public static string userID;
        public static string userName;
        public static string userRole;

        public OrderManagement()
        {
            InitializeComponent();
            GetOrder();
            cbStage.SelectedIndex = 0;

            if(txtStatus.Text == "Order cancel")
            {
                btnCancel.IsEnabled = false;
                btnCancel.Foreground = Brushes.Black;
            }
            else
            {
                btnCancel.IsEnabled = true;
                btnCancel.Foreground = Brushes.WhiteSmoke;
            }
        }

        void GetOrder()
        {
            var db = new CustomerOrderDb();
            customerOrders = db.CustomerOrders.ToList();
            dgOrder.ItemsSource = customerOrders;
        }

        void ReadOrder()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Customer WHERE @CustomerID = CustomerID", con);
            cmd.Parameters.AddWithValue("@CustomerID", index);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                userID = da.GetValue(2).ToString();
                userName = da.GetValue(3).ToString();
                userRole = da.GetValue(4).ToString();
                txtPaymentMethod.Text = da.GetValue(16).ToString();
                currentDate = Convert.ToDateTime(da.GetValue(20));
            }
            con.Close();

            con.Open();
            cmd = new SqlCommand("SELECT * FROM CustomerOrder WHERE @CustomerID = CustomerID", con);
            cmd.Parameters.AddWithValue("@CustomerID", index);
            da = cmd.ExecuteReader();
            while (da.Read())
            {
                txtName.Text = da.GetValue(2).ToString();
                txtStatus.Text = status = da.GetValue(3).ToString();
                txtStage.Text = da.GetValue(4).ToString();
            }
            con.Close();
        }

        void CancelOrder()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@CustomerID", index);
            cmd.Parameters.AddWithValue("@PaymentStatus", "Order cancel");
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            var row = sender as DataGridRow;
            var acc = row.DataContext as CustomerOrder;

            index = acc.CustomerID;
            inputName = acc.Name;
            stage = acc.OrderStatus;

            ReadOrder();

            if (stage == "Stage 0")
            {
                btnUpdate.Visibility = Visibility.Visible;
                btnDetail.IsEnabled = false;
                btnDetail.Foreground = Brushes.Black;
            }
            else if (txtStatus.Text == "Order cancel")
            {
                btnCancel.Foreground = Brushes.Black;
                btnCancel.IsEnabled = false;
            }
            else
            {
                btnCancel.Foreground = Brushes.WhiteSmoke;
                btnCancel.IsEnabled = true;

                btnUpdate.Visibility = Visibility.Collapsed;
                btnDetail.IsEnabled = true;
                btnDetail.Foreground = Brushes.WhiteSmoke;
            }
        }

        private void dgOrder_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgOrder.Columns[2].Visibility = Visibility.Hidden;
            dgOrder.Columns[3].Visibility = Visibility.Hidden;
            dgOrder.Columns[4].Visibility = Visibility.Hidden;
            dgOrder.Columns[5].Visibility = Visibility.Hidden;
            dgOrder.Columns[6].Visibility = Visibility.Hidden;
        }

        private void cbStage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetOrder();
            switch (cbStage.SelectedIndex.ToString())
            {
                case "0":
                    cbStage.Text = "Full";
                    break;
                case "1":
                    cbStage.Text = "Stage 0";
                    customerOrders.RemoveAll(x => x.OrderStatus != "Stage 0");
                    break;
                case "2":
                    cbStage.Text = "Stage 1";
                    customerOrders.RemoveAll(x => x.OrderStatus != "Stage 1");
                    break;
                case "3":
                    cbStage.Text = "Stage 2";
                    customerOrders.RemoveAll(x => x.OrderStatus != "Stage 2");
                    break;
                case "4":
                    cbStage.Text = "Stage 3";
                    customerOrders.RemoveAll(x => x.OrderStatus != "Stage 3");
                    break;
                case "5":
                    cbStage.Text = "Stage 4";
                    customerOrders.RemoveAll(x => x.OrderStatus != "Stage 4");
                    break;
            }
        }

        void Refresh()
        {
            index = null;
            new OrderManagement().Show();
            this.Close();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            if (index != null)
            {
                OrderManagementDetail orderManagementDetail = new OrderManagementDetail(this);
                Opacity = 0.2;
                orderManagementDetail.ShowDialog();
                Opacity = 1;
                Refresh();
            }
            else
            {
                MessageBox.Show("Choose an order","Error");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Approve this order?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
            if (result == MessageBoxResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE CustomerOrder SET OrderStatus = @OrderStatus WHERE CustomerID = @CustomerID", con);
                cmd.Parameters.AddWithValue("@CustomerID", index);
                cmd.Parameters.AddWithValue("@OrderStatus", "Stage 1");
                cmd.ExecuteNonQuery();
                con.Close();

                //ActivityLog();

                con.Open();
                cmd = new SqlCommand($"INSERT INTO AccountNotify VALUES ('" + userID + "','" + userName + "','" + userRole + "','" + $"Order {index} approved" + "','" + $"{Login.passText} approve order {index}" + "','" + "Product news" + "','" + "none" + "', '" + "Complete" + "', '" + currentdatetime + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show($"Order {index} approved", "Notification");
                index = null;
                Refresh();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (txtStatus.Text != "Order cancel")
            {
                var result = MessageBox.Show("This action is permanent, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    query = "UPDATE Customer SET PaymentStatus = @PaymentStatus WHERE CustomerID = @CustomerID";
                    CancelOrder();
                    query = "UPDATE CustomerOrder SET PaymentStatus = @PaymentStatus WHERE CustomerID = @CustomerID";
                    CancelOrder();

                    //ActivityLog();

                    con.Open();
                    SqlCommand cmd = new SqlCommand($"INSERT INTO AccountNotify VALUES ('" + userID + "','" + userName + "','" + userRole + "','" + $"Order {index} canceled" + "','" + $"{Login.passText} cancel order {index}" + "','" + "Product news" + "','" + "none" + "', '" + "Complete" + "', '" + currentdatetime + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Order cancel", "Notification");
                    index = null;
                    Refresh();
                }
            }
            else
            {
                MessageBox.Show("Order already canceled", "Error");
                index = null;
                GetOrder();
            }
        }

        //private void btnPrint_Click(object sender, RoutedEventArgs e)
        //{
        //    string LogFolder = @"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs";
        //    string queryString = "SELECT * FROM CustomerOrder";
        //    string filePath = $@"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs\Receipts\OrderInformation\ All_Of_CustomerOrder_information.XLSX";
        //    action = $"Order {AccountOrder.index} customer informations printed";

        //    var result = System.Windows.MessageBox.Show("Print the receipt?", "Notification", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
        //    if (result == MessageBoxResult.Yes)
        //    {
        //        try
        //        {
        //            if (File.Exists(filePath))
        //                File.Delete(filePath);

        //            using (SqlConnection connection = new SqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                using (SqlCommand command = new SqlCommand(queryString, connection))
        //                {
        //                    //command.Parameters.AddWithValue("CustomerID", OrderManagement.index);
        //                    using (SqlDataReader reader = command.ExecuteReader())
        //                    {
        //                        //Create new Excel application and workbook
        //                        Application excelApp = new Application();
        //                        Workbook excelWorkbook = excelApp.Workbooks.Add();
        //                        Worksheet excelWorksheet = excelWorkbook.Worksheets[1];

        //                        //Add the headers to first row
        //                        int col = 1;
        //                        for (int i = 1; i < reader.FieldCount; i++)
        //                        {
        //                            excelWorksheet.Cells[1, col].Value2 = reader.GetName(i);
        //                            excelWorksheet.Cells[1, col].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
        //                            excelWorksheet.Cells[1, col].Borders.LineStyle = 1;
        //                            excelWorksheet.Cells[1, col].HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //                            col++;
        //                        }

        //                        //Iterate through data start from second row and insert into worksheet
        //                        int row = 2;
        //                        while (reader.Read())
        //                        {
        //                            col = 1;
        //                            for (int i = 1; i < reader.FieldCount; i++)
        //                            {
        //                                excelWorksheet.Cells[row, col].EntireColumn.NumberFormat = "@";
        //                                excelWorksheet.Cells[row, col].Value2 = reader[i];
        //                                excelWorksheet.Cells[row, col].EntireColumn.AutoFit();
        //                                excelWorksheet.Cells[row, col].HorizontalAlignment = XlHAlign.xlHAlignLeft;
        //                                excelWorksheet.Cells[row, col].Borders.LineStyle = 1;
        //                                col++;
        //                            }
        //                            row++;
        //                        }
        //                        //UpdateFormat(filePath);
        //                        //Save workbook and close Excel application
        //                        excelWorkbook.SaveAs(filePath);
        //                        excelWorkbook.Close();
        //                        excelApp.Quit();

        //                        System.Windows.MessageBox.Show("Receipt printed");
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception exception)
        //        {
        //            using (StreamWriter sw = File.CreateText(LogFolder + "\\" + "ErrorLog" + currentdatetime + ".log"))
        //            {
        //                sw.WriteLine(exception.ToString());
        //            }
        //        }
        //    }
        //}
    }
}
