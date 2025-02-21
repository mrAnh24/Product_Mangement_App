using DatabaseApp.Data;
using DatabaseApp.Data.DataModels;
using DocumentFormat.OpenXml.Office.Word;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
using static ClosedXML.Excel.XLPredefinedFormat;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for InvoiceManagement.xaml
    /// </summary>
    public partial class InvoiceManagement : Window
    {
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        List<Customer> customers = new List<Customer>();
        public List<double> highestBill = new List<double>();
        public List<string> paymentMethod = new List<string>();

        public static string index;
        public static System.DateTime currentDateTime;
        public double bill;
        public string country;
        public string product;
        public string payment;
        public string table;

        public InvoiceManagement()
        {
            InitializeComponent();
            //CustomerInvoice();
            AllInOne();
            txtCustomer.Text = HomeAdmin.customerNumbers.ToString();
            txtProduct.Text = HomeAdmin.productNumbers.ToString();
            txtIncome.Text = bill + " $";
            txtHighest.Text = highestBill.Max() + " $";
            txtProfit.Text = country;
            txtPayment.Text = paymentMethod[0];
            txt1Product.Text = product;
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            var row = sender as DataGridRow;
            var acc = row.DataContext as Customer;
            
            index = acc.CustomerID;
            currentDateTime = acc.CreatedDate;
        }

        private void cbFilter1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadInvoice();
            Invisible();
            ShowResult();
            cbFilter2.IsEnabled = true;
            cbFilter2.Text = "";
            btnSearch.IsEnabled = true;
            tbSearchBox.IsEnabled = true;
            btnSearch.Foreground = Brushes.White;
            switch (cbFilter1.SelectedIndex.ToString())
            {
                case "0":
                    ShowResult();
                    cbFilter1.Text = "Full Detail";
                    ShowFull();
                    cbFilter2.IsEnabled = false;
                    cbFilter2.Text = "Unavailable";
                    btnSearch.IsEnabled = false;
                    tbSearchBox.IsEnabled = false;
                    btnSearch.Foreground = Brushes.Black;
                    tbSearchBox.Text = "";
                    break;
                case "1":
                    cbFilter1.Text = "Customer";
                    ShowCustomer();                                    
                    break;
                case "2":
                    cbFilter1.Text = "Location";
                    ShowLocation();
                    break;
                case "3":
                    cbFilter1.Text = "Other";
                    ShowOther();
                    break;
            }
        }

        private void cbFilter2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbFilter2.SelectedIndex.ToString() == "8")
            {
                tbSearchBox.Text = "****                ";
            }
            else
            {
                tbSearchBox.Text = "";
            }
        }

        void PaymentStatusChange()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"UPDATE {table} SET PaymentStatus = @PaymentStatus WHERE CustomerID = @CustomerID", con);
            cmd.Parameters.AddWithValue("@CustomerID", index);
            cmd.Parameters.AddWithValue("@PaymentStatus", payment);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void ActivityLog()
        {
            con.Open();
            string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "INSERT INTO ActivityLog VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + $" Order {index} payment status change" + "', '" + "Checkout status" + "', '" + currentdatetime + "')"; ;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void AllInOne()
        {
            TopProduct();
            CountryProfit();
            Payment();
            CustomerBill();
            LoadInvoice();
        }

        void Invisible()
        {

            cbCustomerID.Visibility = Visibility.Collapsed;
            cbUsername.Visibility = Visibility.Collapsed;
            cbInputName.Visibility = Visibility.Collapsed;
            cbGender.Visibility = Visibility.Collapsed;

            cbCountry.Visibility = Visibility.Collapsed;
            cbCity.Visibility = Visibility.Collapsed;
            cbRegion.Visibility = Visibility.Collapsed;
            cbAddress.Visibility = Visibility.Collapsed;

            cbPostalCode.Visibility = Visibility.Collapsed;
            cbPaymentMethod.Visibility = Visibility.Collapsed;
            cbCouponCode.Visibility = Visibility.Collapsed;
            cbPaymentStatus.Visibility = Visibility.Collapsed;
        }

        void LoadInvoice()
        {
            var db = new CustomerDb();
            customers = db.Customers.ToList();
            dgInvoice.ItemsSource = customers;
        }

        void Refresh()
        {
            index = null;
            new InvoiceManagement().Show();
            this.Close();
        }

        void TopProduct()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 Product FROM CustomerListFinal ORDER BY Amount DESC", con);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                product = da.GetValue(0).ToString();
            }
            con.Close();
        }

        void CustomerBill()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Customer", con);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                double number = Convert.ToDouble(da.GetValue(17));
                highestBill.Add(number);
                bill += number;
            }
            con.Close();
        }

        void CountryProfit()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 Country, count(Country) FROM Customer GROUP BY Country ORDER BY count(Country) DESC", con);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                country = da.GetValue(0).ToString();
            }
            con.Close();
        }

        void Payment()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT TOP 3 PaymentMethod, count(PaymentMethod) FROM Customer GROUP BY PaymentMethod  ORDER BY count(PaymentMethod) DESC", con);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                string name = da.GetValue(0).ToString();
                paymentMethod.Add(name);
            }
            con.Close();
        }

        void KeepFilter()
        {
            LoadInvoice();
            if (cbFilter1.SelectedIndex.ToString() == "0")
            {
                ShowFull();
            }
            else if (cbFilter1.SelectedIndex.ToString() == "1")
            {
                ShowCustomer();
            }
            else if (cbFilter1.SelectedIndex.ToString() == "2")
            {
                ShowLocation();
            }
            else if (cbFilter1.SelectedIndex.ToString() == "3")
            {
                ShowOther();
            }
        }

        void ShowResult()
        {
            dgInvoice.Columns[0].Visibility = Visibility.Hidden;  // No
            dgInvoice.Columns[1].Visibility = Visibility.Hidden;  // CustomerID
            dgInvoice.Columns[2].Visibility = Visibility.Hidden;  // AccountID
            dgInvoice.Columns[3].Visibility = Visibility.Hidden;  // Username
            dgInvoice.Columns[4].Visibility = Visibility.Hidden;  // Role
            //dgInvoice.Columns[5].Visibility = Visibility.Hidden;  // InputName
            dgInvoice.Columns[6].Visibility = Visibility.Hidden;  // Gender
            dgInvoice.Columns[7].Visibility = Visibility.Hidden;  // Title
            dgInvoice.Columns[8].Visibility = Visibility.Hidden;  // Company
            dgInvoice.Columns[9].Visibility = Visibility.Hidden;  // Address
            dgInvoice.Columns[10].Visibility = Visibility.Hidden; // City
            dgInvoice.Columns[11].Visibility = Visibility.Hidden; //Region
            dgInvoice.Columns[12].Visibility = Visibility.Hidden; // PostalCode
            dgInvoice.Columns[13].Visibility = Visibility.Hidden; // Country
            dgInvoice.Columns[14].Visibility = Visibility.Hidden; // Phone
            dgInvoice.Columns[15].Visibility = Visibility.Hidden; // Fax
            //dgInvoice.Columns[16].Visibility = Visibility.Hidden; // PaymentMethod
            //dgInvoice.Columns[17].Visibility = Visibility.Hidden; // Bill
            //dgInvoice.Columns[18].Visibility = Visibility.Hidden; // CouponCode
            //dgInvoice.Columns[19].Visibility = Visibility.Hidden; // PaymentStatus
            //dgInvoice.Columns[20].Visibility = Visibility.Hidden; // CreatedDate
        }

        void ShowFull()
        {
            dgInvoice.Columns[0].Visibility = Visibility.Visible;  
            dgInvoice.Columns[1].Visibility = Visibility.Visible;
            dgInvoice.Columns[2].Visibility = Visibility.Visible;  
            dgInvoice.Columns[3].Visibility = Visibility.Visible; 
            dgInvoice.Columns[4].Visibility = Visibility.Visible; 
            dgInvoice.Columns[6].Visibility = Visibility.Visible; 
            dgInvoice.Columns[7].Visibility = Visibility.Visible; 
            dgInvoice.Columns[8].Visibility = Visibility.Visible;  
            dgInvoice.Columns[9].Visibility = Visibility.Visible;  
            dgInvoice.Columns[10].Visibility = Visibility.Visible; 
            dgInvoice.Columns[11].Visibility = Visibility.Visible; 
            dgInvoice.Columns[12].Visibility = Visibility.Visible;
            dgInvoice.Columns[13].Visibility = Visibility.Visible; 
            dgInvoice.Columns[14].Visibility = Visibility.Visible; 
            dgInvoice.Columns[15].Visibility = Visibility.Visible; 
        }

        void ShowCustomer()
        {
            cbCustomerID.Visibility = Visibility.Visible;
            cbUsername.Visibility = Visibility.Visible;
            cbInputName.Visibility = Visibility.Visible;
            cbGender.Visibility = Visibility.Visible;

            dgInvoice.Columns[1].Visibility = Visibility.Visible;
            dgInvoice.Columns[2].Visibility = Visibility.Visible;
            dgInvoice.Columns[3].Visibility = Visibility.Visible;
            dgInvoice.Columns[6].Visibility = Visibility.Visible;
        }

        void ShowLocation()
        {
            cbCountry.Visibility = Visibility.Visible;
            cbCity.Visibility = Visibility.Visible;
            cbRegion.Visibility = Visibility.Visible;
            cbAddress.Visibility = Visibility.Visible;

            dgInvoice.Columns[9].Visibility = Visibility.Visible;
            dgInvoice.Columns[10].Visibility = Visibility.Visible;
            dgInvoice.Columns[11].Visibility = Visibility.Visible;
            dgInvoice.Columns[13].Visibility = Visibility.Visible;
        }
        void ShowOther()
        {
            cbPostalCode.Visibility = Visibility.Visible;
            cbPaymentMethod.Visibility = Visibility.Visible;
            cbCouponCode.Visibility = Visibility.Visible;
            cbPaymentStatus.Visibility = Visibility.Visible;

            dgInvoice.Columns[12].Visibility = Visibility.Visible;
        }

        private void btnPayment_Click(object sender, RoutedEventArgs e)
        {
            if (index != null)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customer WHERE CustomerID = @CustomerID", con);
                cmd.Parameters.AddWithValue("@CustomerID", index);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    payment = da.GetValue(19).ToString();
                }
                con.Close();

                if (payment == "Order cancel")
                {
                    System.Windows.MessageBox.Show("This invoice can no longer be modified", "Error");
                    KeepFilter();
                }
                else
                {
                    var result = System.Windows.MessageBox.Show("Change payment status of this invoice?", "Confirmation", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (payment == "Payment complete")
                        {
                            payment = "Payment incomplete";
                        }
                        else
                        {
                            payment = "Payment complete";
                        }

                        table = "Customer";
                        PaymentStatusChange();
                        table = "CustomerOrder";
                        PaymentStatusChange();

                        ActivityLog();
                        System.Windows.MessageBox.Show("Payment status change", "Notification");

                        index = null;
                        Refresh();
                    }
                    else
                    {
                        index = null;
                        KeepFilter();
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Select an invoice first", "Error");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (index != null)
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail(this);
                Opacity = 0.2;
                invoiceDetail.ShowDialog();
                Opacity = 1;
                Refresh();
            }
            else
            {
                System.Windows.MessageBox.Show("Select an invoice first","Error");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void dgInvoice_AutoGeneratedColumns(object sender, EventArgs e)
        {
            ShowResult();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (tbSearchBox.Text != "")
            {
                if (cbFilter1.Text != "")
                {
                    if (cbFilter2.Text != "")
                    {
                        if (cbFilter1.SelectedIndex.ToString() == "1")
                        {
                            if (cbFilter2.SelectedIndex.ToString() == "0")
                            {
                                LoadInvoice();
                                ShowCustomer();
                                customers.RemoveAll(x => x.CustomerID != tbSearchBox.Text);
                            }
                            else if (cbFilter2.SelectedIndex.ToString() == "1")
                            {
                                LoadInvoice();
                                ShowCustomer();
                                customers.RemoveAll(x => x.Username != tbSearchBox.Text);
                            }
                            else if (cbFilter2.SelectedIndex.ToString() == "2")
                            {
                                LoadInvoice();
                                ShowCustomer();
                                customers.RemoveAll(x => x.InputName != tbSearchBox.Text);
                            }
                            else if (cbFilter2.SelectedIndex.ToString() == "3")
                            {
                                LoadInvoice();
                                ShowCustomer();
                                customers.RemoveAll(x => x.Gender != tbSearchBox.Text);
                            }

                            if (dgInvoice.Items.Count == 0)
                            {
                                System.Windows.MessageBox.Show("No result found", "Error");
                                LoadInvoice();
                                ShowCustomer();
                            }
                        }
                        else if (cbFilter1.SelectedIndex.ToString() == "2")
                        {
                            if (cbFilter2.SelectedIndex.ToString() == "4")
                            {
                                LoadInvoice();
                                ShowLocation();
                                customers.RemoveAll(x => x.Country != tbSearchBox.Text);
                            }
                            else if (cbFilter2.SelectedIndex.ToString() == "5")
                            {
                                LoadInvoice();
                                ShowLocation();
                                customers.RemoveAll(x => x.City != tbSearchBox.Text);
                            }
                            else if (cbFilter2.SelectedIndex.ToString() == "6")
                            {
                                LoadInvoice();
                                ShowLocation();
                                customers.RemoveAll(x => x.Region != tbSearchBox.Text);
                            }
                            else if (cbFilter2.SelectedIndex.ToString() == "7")
                            {
                                LoadInvoice();
                                ShowLocation();
                                customers.RemoveAll(x => x.Address != tbSearchBox.Text);
                            }

                            if(dgInvoice.Items.Count == 0)
                            {
                                System.Windows.MessageBox.Show("No result found", "Error");
                                LoadInvoice();
                                ShowLocation();
                            }
                        }
                        else if (cbFilter1.SelectedIndex.ToString() == "3")
                        {
                            if (cbFilter2.SelectedIndex.ToString() == "8")
                            {
                                LoadInvoice();
                                ShowOther();
                                customers.RemoveAll(x => x.PostalCode != tbSearchBox.Text);
                            }
                            else if (cbFilter2.SelectedIndex.ToString() == "9")
                            {
                                LoadInvoice();
                                ShowOther();
                                customers.RemoveAll(x => x.PaymentMethod != tbSearchBox.Text);
                            }
                            else if (cbFilter2.SelectedIndex.ToString() == "10")
                            {
                                LoadInvoice();
                                ShowOther();
                                customers.RemoveAll(x => x.CouponCode != tbSearchBox.Text);
                            }
                            else if (cbFilter2.SelectedIndex.ToString() == "11")
                            {
                                LoadInvoice();
                                ShowOther();
                                customers.RemoveAll(x => x.PaymentStatus != tbSearchBox.Text);
                            }

                            if (dgInvoice.Items.Count == 0)
                            {
                                System.Windows.MessageBox.Show("No result found", "Error");
                                LoadInvoice();
                                ShowOther();
                            }
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Choose a filter", "Error");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Choose a category", "Error");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Search box blank", "Error");
                LoadInvoice();
            }
        }

        private void btnSearchClear_Click(object sender, RoutedEventArgs e)
        {
            tbSearchBox.Text = "";
        }
    }
}
