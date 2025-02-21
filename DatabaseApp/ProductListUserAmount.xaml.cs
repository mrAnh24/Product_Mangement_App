using DatabaseApp.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ProductListUserAmount.xaml
    /// </summary>
    public partial class ProductListUserAmount : Window
    {
        public string tableName;
        public double o7;
        public double totalAmount;

        public ProductListUserAmount(Window parentWindow)
        {
            InitializeComponent();
            Owner = parentWindow;
            ProductNumber();
            txtName.Text = $"Enter amount of {ProductListUser.name}";
            tbSubmit.Text = ProductListUser.amount.ToString();
        }
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        void GetTotal()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from ProductLists where Product = @Product", con);
            cmd.Parameters.AddWithValue("@Product", ProductListUser.name);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                totalAmount = Convert.ToDouble(da.GetValue(5));
            }
            con.Close();
        }

        void ProductNumber()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from CustomerList where OrderID = @OrderID", con);
            cmd.Parameters.AddWithValue("@OrderID", ProductListUser.index);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                o7 = Convert.ToDouble(da.GetValue(7));
            }
            con.Close();
        }

        void AmountChange()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"UPDATE {tableName} SET Amount = @Amount WHERE OrderID = @OrderID", con);
            cmd.Parameters.AddWithValue("@OrderID", ProductListUser.index);
            cmd.Parameters.AddWithValue("@Amount", tbSubmit.Text);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            GetTotal();
            if (tbSubmit.Text == "0" || tbSubmit.Text == "00" || tbSubmit.Text.Contains("."))
            {
                System.Windows.MessageBox.Show("Enter amount > 0", "Error");
            }            
            else
            {
                if (tbSubmit.Text == o7.ToString())
                {
                    System.Windows.MessageBox.Show("No change was made", "Notification");
                    this.Close();
                }
                else
                {
                    if (Convert.ToDouble(tbSubmit.Text) > totalAmount)
                    {
                        System.Windows.MessageBox.Show("Amount purchase higher than amount available", "Error");
                    }
                    else
                    {
                        tableName = "CustomerList";
                        AmountChange();

                        tableName = "CustomerListFinal";
                        AmountChange();

                        ProductListUser.total -= ProductListUser.itemSum;
                        ProductListUser.total += (Convert.ToDouble(tbSubmit.Text) * ProductListUser.itemPrice);
                        ProductListUser.number -= ProductListUser.amount;
                        ProductListUser.number += Convert.ToDouble(tbSubmit.Text);
                        ProductListUser.itemsCount++;

                        System.Windows.MessageBox.Show($"Amount of {ProductListUser.name} changed", "Notify");
                        this.Close();
                }
            }
            }
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void tbSubmit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(tbSubmit.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ProductListUser.index = null;
        }
    }
}
