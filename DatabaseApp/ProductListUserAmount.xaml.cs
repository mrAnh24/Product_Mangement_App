﻿using System;
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

        public ProductListUserAmount(Window parentWindow)
        {
            InitializeComponent();
            Owner = parentWindow;
            string accountName = Login.passText;
            txtName.Text = $"Enter amount of {ProductListUser.name}";
            tbSubmit.Text = ProductListUser.amount.ToString();
        }
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

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
            if(tbSubmit.Text == "0" || tbSubmit.Text == "00" || tbSubmit.Text.Contains("."))
            {
                System.Windows.MessageBox.Show("Enter amount > 0", "error");
            }
            else
            {
                tableName = "CustomerList";
                AmountChange();

                tableName = "CustomerListFinal";
                AmountChange();

                ProductListUser.total -= ProductListUser.itemSum;
                ProductListUser.total += (Convert.ToDouble(tbSubmit.Text)* ProductListUser.itemPrice);
                ProductListUser.number -= ProductListUser.amount;
                ProductListUser.number += Convert.ToDouble(tbSubmit.Text);
                ProductListUser.itemsCount++;

                System.Windows.MessageBox.Show($"Amount of {ProductListUser.name} changed", "Notify");
                this.Close();
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