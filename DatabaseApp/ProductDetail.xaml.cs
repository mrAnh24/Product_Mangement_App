using DatabaseApp.Data;
using DatabaseApp.Data.DataModels;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office.Word;
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
using static ClosedXML.Excel.XLPredefinedFormat;
using MessageBox = System.Windows.Forms.MessageBox;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for ProductDetail.xaml
    /// </summary>
    public partial class ProductDetail : Window
    {
        public double x;
        public static string uploader;
        public string action;
        public static string currentdatetime;
        public string dateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        public ProductDetail(Window ParentWindow)
        {
            InitializeComponent();
            Owner = ParentWindow;
            x = 1;

            txtProductDescription.Text = " " + ProductList.productDescription;
            txtProductUploader.Text = uploader = ProductList.ProductCreator;

            txtProductCode.Text = ProductList.index = ProductList.productID;
            txtProductName.Text = ProductList.productName;
            txtProductType.Text = ProductList.productType;

            txtProductPrice.Text = ProductList.productPrice;
            txtProductAmount.Text = ProductList.productAmount;

            ReadProductList();
            txtStatus.Text = ProductList.productStatus;

            txtStatus.Foreground = Brushes.Red;
            if (txtStatus.Text != "Sold Out")
            {
                rNotify.Visibility = Visibility.Visible;
                txtNotify.Visibility = Visibility.Visible;
                txtNotify.Text = "Pre-Order unavailable";
                txtNotify.Foreground = Brushes.Red;
                btnConfirm.Visibility = Visibility.Collapsed;

                if (txtStatus.Text == "Available" || txtStatus.Text == "On sale")
                {
                    txtStatus.Foreground = Brushes.ForestGreen;
                    if (txtStatus.Text == "On sale")
                    {
                        txtOldPrice.Visibility = Visibility.Visible;
                        txtOldPrice.Text = $"(was {ProductList.productPrice})";
                        txtProductPrice.Text = ProductList.finalPrice;
                    }
                }
            }
        }

        public void ActivityLog()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO ActivityLog VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + action + "', '" + "Checkout status" + "', '" + currentdatetime + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void ReadProductList()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from ProductLists where ProductCode = @ProductCode", con);
            cmd.Parameters.AddWithValue("@ProductCode", ProductList.index);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                txtProductDescription.Text = da.GetValue(2).ToString();
                txtProductType.Text = da.GetValue(3).ToString();
                txtProductUploader.Text = da.GetValue(7).ToString();
            }
            con.Close();
        }

        void GetOrder()
        {
            con.Open();
            //tbUsername.Text = AccountManagement.dgAccount.GetValue(DataGridRow[item]);
            SqlCommand cmd = new SqlCommand("SELECT * FROM CustomerPreOrder", con);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                currentdatetime = da.GetValue(8).ToString();
            }
            con.Close();
        }

        void PreOrder()
        {
            con.Open();
            string query = "INSERT INTO CustomerPreOrder VALUES ('" + Login.GetID + "','" + Login.passText + "','" + txtProductName.Text + "','" + txtProductCode.Text + "', '" + txtProductPrice.Text + "', '" + tbRequest.Text + "', '" + dateTime + "', '" + "Incomplete" + "')"; ;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void CancelOrder()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM CustomerPreOrder WHERE ProductCode = @ProductCode AND CreatedDate = @CreatedDate", con);
            cmd.Parameters.AddWithValue("@ProductCode", txtProductCode.Text);
            cmd.Parameters.AddWithValue("@CreatedDate", currentdatetime);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            string action = $"{Login.passText} pre-order {tbRequest.Text} {txtProductName.Text}";
            if (x == 1)
            {
                if(tbRequest.Text == "")
                {
                    MessageBox.Show("Request box empty", "Error");
                }
                else if (tbRequest.Text == "0" || tbRequest.Text == "00" || tbRequest.Text.Contains("."))
                {
                    MessageBox.Show("Please enter valid number", "Error");
                }
                else
                {
                    x = 2;
                    btnConfirm.Content = "Cancel";
                    txtNotify.Visibility = Visibility.Visible;
                    rNotify.Visibility = Visibility.Visible;

                    PreOrder();
                    action = $"{txtProductName.Text} PreOrder";
                    ActivityLog();
                }
            }
            else
            {
                x = 1;
                btnConfirm.Content = "Send";
                txtNotify.Visibility = Visibility.Collapsed;
                rNotify.Visibility = Visibility.Collapsed;
                
                GetOrder();
                CancelOrder();
                action = $"{txtProductName.Text} PreOrder canceled";
                ActivityLog();
            }
        }

        private void btnEscape_Click(object sender, RoutedEventArgs e)
        {
            if (x == 2)
            {
                var result = System.Windows.MessageBox.Show("Pre-Order status can't not be change after this, continue?", "Reminder", MessageBoxButton.YesNo, (MessageBoxImage)MessageBoxIcon.Information);
                if (result == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            else if (tbRequest.Text != "")
            {
                if(tbRequest.Text == "0" || tbRequest.Text == "00" || tbRequest.Text.Contains("."))
                {
                    this.Close();
                }
                else
                {
                    var result = System.Windows.MessageBox.Show("Pre-Order haven't been send yet, go back anyway?", "Warning", MessageBoxButton.YesNo, (MessageBoxImage)MessageBoxIcon.Information);
                    if (result == MessageBoxResult.Yes)
                    {
                        this.Close();
                    }
                }
            }
            else
            {
                this.Close();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //ProductList.index = null;
        }

        private void tbRequest_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(tbRequest.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }

        private void txtProductUploader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtProductUploader.Text == Login.passText)
            {
                new Account().Show();
                this.Close();
                Owner.Close();
            }
            else
            {
                new AccountView().Show();
                this.Close();
                Owner.Close();
            }
        }
    }
}
