using DatabaseApp.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for HomeAdmin.xaml
    /// </summary>
    public partial class HomeAdmin : Window
    {
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        public List<string> idnumber = new List<string>();
        public List<string> actionCount = new List<string>();

        public string IDnumber;
        public string name;
        public string role;
        public double action;
        public int count;
        public static double customerNumbers;
        public static double productNumbers;
        public static string userName;

        public HomeAdmin()
        {
            InitializeComponent();
            productNumbers = 0;
            customerNumbers = 0;
            GetInfo();
            txtCustomer.Text = customerNumbers.ToString();
            txtProduct.Text = productNumbers.ToString();
        }

        void GetInfo()
        {
            GetName();
            IDnumber = idnumber[0];
            GetTop3();
            txtUsername1.Text = name;
            txtRole1.Text = role;
            txtAction1.Text = actionCount[0] + " Action";

            IDnumber = idnumber[1];
            GetTop3();
            txtUsername2.Text = name;
            txtRole2.Text = role;
            txtAction2.Text = actionCount[1] + " Action";

            IDnumber = idnumber[2];
            GetTop3();
            txtUsername3.Text = name;
            txtRole3.Text = role;
            txtAction3.Text = actionCount[2] + " Action";

            GetCustomer();
            GetProduct();
        }

        void GetTop3()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM AccountTest WHERE @AccountID = AccountID", con);
            cmd.Parameters.AddWithValue("@AccountID", IDnumber);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                name = da.GetValue(2).ToString();
                role = da.GetValue(5).ToString();
            }
            con.Close();
        }

        void GetName()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT TOP 3 AccountID, count(AccountID) FROM ActivityLog WHERE Category != 'Notification' AND AccountID != '' GROUP BY AccountID", con);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                string number = da.GetValue(0).ToString();
                string account = da.GetValue(1).ToString();
                idnumber.Add(number);
                actionCount.Add(account);
            }
            con.Close();
        }

        void GetCustomer()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT TOP 1000 CustomerID, count(CustomerID) FROM Customer GROUP BY CustomerID", con);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                string customer = da.GetValue(0).ToString();
                customerNumbers++;
            }
            con.Close();
        }

        void GetProduct()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM CustomerListFinal", con);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                double customer = Convert.ToDouble(da.GetValue(7));
                productNumbers += customer;
            }
            con.Close();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            new HomeAdmin().Show();
            this.Close();
        }

        private void btnAccountManagement_Click(object sender, RoutedEventArgs e)
        {
            if (Login.passText == "admin")
            {
                new AccountManagement().Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Permission denied", "Error");
            }
        }

        private void btnAccountRequest_Click(object sender, RoutedEventArgs e)
        {
            new AccountRequests().Show();
            this.Close();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new UpdateProductAdmin().Show();
            this.Close();
        }

        private void btnInvoiceManagement_Click(object sender, RoutedEventArgs e)
        {
            new InvoiceManagement().Show();
            this.Close();
        }

        public void ButtonPress()
        {
            new AccountView().Show();
            this.Close();
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            userName = txtUsername1.Text;
            ButtonPress();
        }

        private void Image_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            userName = txtUsername2.Text;
            ButtonPress();
        }

        private void Image_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            userName = txtUsername3.Text;
            ButtonPress();
        }

        private void btnAnoucement_Click(object sender, RoutedEventArgs e)
        {
            new Announcement().Show();
            this.Close();
        }
    }
}
