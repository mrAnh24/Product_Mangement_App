using DatabaseApp.Data.DataModels;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for InvoiceDetail.xaml
    /// </summary>
    public partial class InvoiceDetail : Window
    {
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        public string status;
        public string title;
        public string action;

        public InvoiceDetail(Window parentWindow)
        {
            Owner = parentWindow;
            InitializeComponent();
            GetInvoice();
            txtHeader.Text = title + "'s invoice details";
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
                txtInputName.Text = title = da.GetValue(5).ToString();
                txtRole.Text = da.GetValue(4).ToString();
                txtTitle.Text = da.GetValue(7).ToString();

                txtGender.Text = da.GetValue(6).ToString();
                txtCompany.Text = da.GetValue(8).ToString();
                txtAddress.Text = da.GetValue(9).ToString();
                txtCity.Text = da.GetValue(10).ToString();
                txtRegion.Text = da.GetValue(11).ToString();
                txtCountry.Text = da.GetValue(13).ToString();

                txtPhoneNumber.Text = da.GetValue(14).ToString();
                txtFaxNumber.Text = da.GetValue(15).ToString();
                txtPostalCode.Text = da.GetValue(12).ToString();
                txtPaymentMethod.Text = da.GetValue(16).ToString();
                txtCouponCode.Text = da.GetValue(18).ToString();
                txtBill.Text = da.GetValue(17).ToString();

                txtDayCreated.Text = da.GetValue(20).ToString();
                status = da.GetValue(19).ToString();
            }
            con.Close();

            if (status == "Order cancel")
            {
                Disable();
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

        public void ActivityLog()
        {
            con.Open();
            string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "INSERT INTO ActivityLog VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + action + "', '" + "Checkout status" + "', '" + currentdatetime + "')"; ;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void Disable()
        {
            txtInputName.IsEnabled = false;
            txtTitle.IsEnabled = false;
            txtGender.IsEnabled = false;
            txtCompany.IsEnabled = false;
            txtAddress.IsEnabled = false;
            txtCity.IsEnabled = false;
            txtRegion.IsEnabled = false;
            txtCountry.IsEnabled = false;
            txtPhoneNumber.IsEnabled = false;
            txtFaxNumber.IsEnabled = false;
            txtPostalCode.IsEnabled = false;
            txtPaymentMethod.IsEnabled = false;
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

        private void Window_Closed(object sender, EventArgs e)
        {
            InvoiceManagement.index = null;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            action = $"Order {InvoiceManagement.index} information modified";

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
            System.Windows.MessageBox.Show("Order Updated", "Notification");
            this.Close();
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
                System.Windows.MessageBox.Show("Order cancel", "Notification");
                this.Close();
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            AccountOrder.index = null;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
