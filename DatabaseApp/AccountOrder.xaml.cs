using DatabaseApp.Data;
using DatabaseApp.Data.DataModels;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
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
using Brushes = System.Windows.Media.Brushes;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for AccountOrder.xaml
    /// </summary>
    public partial class AccountOrder : System.Windows.Window
    {
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        List<Customer> customers = new List<Customer>();
        List<CustomerOrder> customerOrders = new List<CustomerOrder>();
        List<CustomerPreOrder> customersPreOrder = new List<CustomerPreOrder>();
        public double notify;
        public string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        //dgInvoice
        public static string index;
        public static string currentStatus;
        public static string currentInputName;
        public static string currentBill;
        public static DateTime currentDate;

        //dgPreOrder
        public static string ProductName;
        public static string indexP;
        public static DateTime DateP;
        public static double AmountP;
        public static string ConditionP;

        public string orderId;
        public double productAmount;
        public string status;
        public string action;
        public string query;

        public AccountOrder()
        {
            InitializeComponent();
            txtTitle.Text = $"List of {Login.passText} orders";
            LoadOrder();
            //cbFilter.SelectedIndex = 0;
            //cbFilter.Text = "All";
        }

        public void ProductAdd()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void ReadProductList()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from ProductLists where ProductCode = @ProductCode", con);
            cmd.Parameters.AddWithValue("@ProductCode", txtProductCode.Text);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                productAmount = Convert.ToDouble(da.GetValue(5));
            }
            con.Close();
        }

        public void ReadProduct()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from CustomerList where AccountID = @AccountID", con);
            cmd.Parameters.AddWithValue("@AccountID", Login.GetID);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                orderId = da.GetValue(1).ToString();
            }
            con.Close();
        }

        void AmountOfProduct()
        {
            double num = (productAmount - AmountP);
            string currentAmount = num.ToString();
            con.Open();
            string query = $"Update ProductLists Set Amount = @Amount Where ProductCode = @ProductCode";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Amount", currentAmount);
            cmd.Parameters.AddWithValue("@ProductCode", txtProductCode.Text);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void CancelRequest()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE CustomerPreOrder SET Condition = @Condition WHERE PreOrderID = @PreOrderID", con);
            cmd.Parameters.AddWithValue("@PreOrderID", indexP);
            cmd.Parameters.AddWithValue("@Condition", "Cancel");
            cmd.ExecuteNonQuery();
            con.Close();

            IndexPNull();
            LoadPreOrder();
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

        void LoadOrder()
        {
            var db = new CustomerDb();
            customers = db.Customers.ToList();
            dgInvoice.ItemsSource = customers;
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            var row = sender as DataGridRow;
            var acc = row.DataContext as Customer;

            index = acc.CustomerID;
            currentInputName = acc.InputName;
            currentBill = acc.Bill.ToString();
            currentDate = acc.CreatedDate;
            currentStatus = acc.PaymentStatus;

            if (currentStatus == "Order cancel")
            {
                btnCancelOder.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnCancelOder.Visibility = Visibility.Visible;
            }
        }

        private void dgInvoice_AutoGeneratedColumns(object sender, EventArgs e)
        {
            ShowResult();
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

        void CancelOrder()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@CustomerID", index);
            cmd.Parameters.AddWithValue("@PaymentStatus", status);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void Refresh()
        {
            new AccountOrder().Show();
            this.Close();
        }

        void IndexPNull()
        {
            indexP = null;
            txtProductCode.Text = "...";
            txtPrice.Text = "...";
            txtAmount.Text = "...";
        }

        void PreOrderDetailsShow()
        {
            dgPreOrder.Visibility = Visibility.Visible;
            RBackground.Visibility = Visibility.Visible;
            RHeaderBackground.Visibility = Visibility.Visible;
            txtPreHeader.Visibility = Visibility.Visible;
            PreOrderDetail.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;

            btnSearch.IsEnabled = false;
            btnSearch.Foreground = Brushes.Black;
            txtSearch.IsEnabled = false;
        }

        void PreOrderDetailsHide()
        {
            dgPreOrder.Visibility = Visibility.Collapsed;
            RBackground.Visibility = Visibility.Collapsed;
            RHeaderBackground.Visibility = Visibility.Collapsed;
            txtPreHeader.Visibility = Visibility.Collapsed;
            PreOrderDetail.Visibility = Visibility.Collapsed;
            btnCancel.Visibility = Visibility.Collapsed;

            btnSearch.IsEnabled = true;
            btnSearch.Foreground = Brushes.WhiteSmoke;
            txtSearch.IsEnabled = true;

            IndexPNull();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadOrder();
            PreOrderDetailsHide();
            switch (cbFilter.SelectedIndex.ToString())
            {
                case "0":
                    ShowResult();
                    break;
                case "1":
                    Ongoing();
                    break;
                case "2":
                    Completed();
                    break;
                case "3":
                    Canceled();
                    break;
                case "4":
                    PreOrderDetailsShow();
                    LoadPreOrder();
                    break;
            }
        }

        void ShowResult()
        {
            dgInvoice.Columns[5].Header = "Name";
            dgInvoice.Columns[20].Header = "Purchase date";

            dgInvoice.Columns[0].Visibility = Visibility.Hidden;
            dgInvoice.Columns[1].Visibility = Visibility.Hidden;
            dgInvoice.Columns[2].Visibility = Visibility.Hidden;
            dgInvoice.Columns[3].Visibility = Visibility.Hidden;
            dgInvoice.Columns[4].Visibility = Visibility.Hidden;
            dgInvoice.Columns[6].Visibility = Visibility.Hidden;
            dgInvoice.Columns[7].Visibility = Visibility.Hidden;
            dgInvoice.Columns[8].Visibility = Visibility.Hidden;
            dgInvoice.Columns[9].Visibility = Visibility.Hidden;
            dgInvoice.Columns[10].Visibility = Visibility.Hidden;
            dgInvoice.Columns[11].Visibility = Visibility.Hidden;
            dgInvoice.Columns[12].Visibility = Visibility.Hidden;
            dgInvoice.Columns[13].Visibility = Visibility.Hidden;
            dgInvoice.Columns[14].Visibility = Visibility.Hidden;
            dgInvoice.Columns[15].Visibility = Visibility.Hidden;
            dgInvoice.Columns[16].Visibility = Visibility.Hidden;
            dgInvoice.Columns[18].Visibility = Visibility.Hidden;

            customers.RemoveAll(x => x.AccountID != Login.GetID);
        }

        void Ongoing()
        {
            ShowResult();
            customerOrders.RemoveAll(x => x.OrderStatus == "Stage 4");
            customers.RemoveAll(x => x.PaymentStatus != "Payment incomplete");
        }

        void Completed()
        {
            ShowResult();
            customerOrders.RemoveAll(x => x.OrderStatus != "Stage 4");
            customers.RemoveAll(x => x.PaymentStatus != "Payment complete");
        }

        void Canceled()
        {
            ShowResult();
            customers.RemoveAll(x => x.PaymentStatus != "Order cancel");
        }

        private void btnAccount_Click(object sender, RoutedEventArgs e)
        {
            new Account().Show();
            this.Close();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btnOderDetail_Click(object sender, RoutedEventArgs e)
        {
            if (index != null)
            {
                OrderDetail orderDetail = new OrderDetail(this);
                Opacity = 0.2;
                orderDetail.ShowDialog();
                Opacity = 1;
                Refresh();
            }
            else
            {
                System.Windows.MessageBox.Show("Select an order first", "Error");
            }
        }

        private void btnProductsDetail_Click(object sender, RoutedEventArgs e)
        {
            if (index != null)
            {
                OrderProductDetail orderProductDetail = new OrderProductDetail(this);
                Opacity = 0.2;
                orderProductDetail.ShowDialog();
                Opacity = 1;
                Refresh();
            }
            else
            {
                System.Windows.MessageBox.Show("Select an order first", "Error");
            }
        }

        private void btnOderStatus_Click(object sender, RoutedEventArgs e)
        {
            if (index != null)
            {
                OrderStatus orderStatus = new OrderStatus(this);
                Opacity = 0.2;
                orderStatus.ShowDialog();
                Opacity = 1;
                Refresh();
            }
            else
            {
                System.Windows.MessageBox.Show("Select an order first", "Error");
            }
        }

        private void btnCancelOder_Click(object sender, RoutedEventArgs e)
        {
            status = "Order cancel";
            action = $" Order {index} canceled";

            if (index != null)
            {
                if (currentStatus != "Order cancel")
                {
                    var result = System.Windows.MessageBox.Show("This action is permanent, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        query = "UPDATE Customer SET PaymentStatus = @PaymentStatus WHERE CustomerID = @CustomerID";
                        CancelOrder();
                        query = "UPDATE CustomerOrder SET PaymentStatus = @PaymentStatus WHERE CustomerID = @CustomerID";
                        CancelOrder();

                        ActivityLog();
                        System.Windows.MessageBox.Show("Order cancel", "Notification");
                        index = null;
                        Refresh();
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Order already canceled", "Error");
                    index = null;
                    LoadOrder();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Select an order first", "Error");
            }
        }

        void LoadPreOrder()
        {
            var db = new CustomerPreOrderDb();
            customersPreOrder = db.CustomerPreOrders.ToList();
            dgPreOrder.ItemsSource = customersPreOrder;
        }

        void ShowPreOrder()
        {
            dgPreOrder.Columns[4].Header = "Product name";
            dgPreOrder.Columns[8].Header = "Time ordered";

            dgPreOrder.Columns[0].Visibility = Visibility.Hidden;
            dgPreOrder.Columns[1].Visibility = Visibility.Hidden;
            dgPreOrder.Columns[2].Visibility = Visibility.Hidden;
            dgPreOrder.Columns[3].Visibility = Visibility.Hidden;
            dgPreOrder.Columns[5].Visibility = Visibility.Hidden;
            dgPreOrder.Columns[6].Visibility = Visibility.Hidden;
            dgPreOrder.Columns[7].Visibility = Visibility.Hidden;
            dgPreOrder.Columns[9].Visibility = Visibility.Hidden;

            customersPreOrder.RemoveAll(x => x.AccountID != Login.GetID);
        }

        private void dg_AutoGeneratedColumns(object sender, EventArgs e)
        {
            ShowPreOrder();
        }

        private void DataGridRow_Selected_1(object sender, RoutedEventArgs e)
        {
            var row = sender as DataGridRow;
            var acc = row.DataContext as CustomerPreOrder;

            ProductName = acc.Product;
            indexP = acc.PreOrderID;
            DateP = acc.CreatedDate;
            txtProductCode.Text = acc.ProductCode;
            txtPrice.Text = acc.Price.ToString();
            AmountP = acc.Amount;
            txtAmount.Text = acc.Amount.ToString();
            txtCondition.Text = ConditionP = acc.Condition;
            btnCancel.Visibility = Visibility.Visible;

            if (ConditionP == "Complete")
            {
                btnCancel.Content = "Confirmed order";
            }
            if (ConditionP == "Cancel")
            {
                btnCancel.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnCancel.Content = "Cancel order";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (indexP != null )
            {
                if(ConditionP == "Complete")
                {
                    var result = System.Windows.MessageBox.Show("Add this order to your list?", "Confirmation", MessageBoxButton.YesNo, (MessageBoxImage)MessageBoxIcon.Information);
                    if (result == MessageBoxResult.Yes)
                    {
                        string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        ReadProductList();
                        AmountOfProduct();

                        query = "INSERT INTO CustomerList VALUES ('" + Login.GetID + "','" + Login.passText + "','" + ProductName + "','" + txtProductCode.Text + "', '" + txtPrice.Text + "', '" + txtAmount.Text + "', '" + currentdatetime + "')";
                        ProductAdd();

                        ReadProduct();
                        query = $"INSERT INTO CustomerListFinal VALUES ('" + orderId + "','" + Login.GetID + "','" + Login.passText + "','" + null + "','" + ProductName + "','" + txtProductCode.Text + "', '" + txtPrice.Text + "', '" + txtAmount.Text + "', '" + currentdatetime + "')";
                        ProductAdd();

                        CancelRequest();
                    }
                }
                else
                {
                    var result = System.Windows.MessageBox.Show("Cancel this pre-order request?", "Warning", MessageBoxButton.YesNo, (MessageBoxImage)MessageBoxIcon.Information);
                    if (result == MessageBoxResult.Yes)
                    {
                        action = $"Order {indexP} canceled";
                        ActivityLog();
                        NotifyCount();

                        con.Open();
                        SqlCommand cmd = new SqlCommand($"INSERT INTO AccountNotify VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + "Canceled an order" + "','" + $"{Login.passText} ({Login.GetRole}) cancel order {index}" + "','" + "Data modified" + "','" + "none" + "', '" + "Complete" + "', '" + currentdatetime + "')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        CancelRequest();
                        System.Windows.MessageBox.Show("Order cancel","Notification");
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Choose an order first", "Error");
                index = null;
            }
        }

        private void btnSearchClear_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == currentInputName)
            {
                customersPreOrder.RemoveAll(x => x.AccountID != txtSearch.Text); //Can't search (can't read database?)//
            }
            else
            {
                System.Windows.MessageBox.Show("No match found", "Error");
            }
        }
    }
}
