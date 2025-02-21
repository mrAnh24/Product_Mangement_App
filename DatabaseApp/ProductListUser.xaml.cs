using DatabaseApp.Data;
using DatabaseApp.Data.DataModels;
using DatabaseApp.View.UserControls;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using Z.Dapper.Plus;
using static ClosedXML.Excel.XLPredefinedFormat;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for ProductListUser.xaml
    /// </summary>
    /// 

    public partial class ProductListUser : Window
    {
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        
        private static readonly object syncLock = new object();

        public static List<CustomerList> list = new List<CustomerList>();

        public string tableName;
        public static double total;
        public static double number;
        public static double number2;
        public static int itemsCount = 0;
        public static string userName;
        public static string index; //placeholder for productId
        public static string name; //placeholder for name
        public static double amount; //placeholder for amount
        public static double itemSum;
        public static double itemPrice;

        public static double InitialAmount;
        public static double AddedAmount;
        public static string CurrentStatus;

        public ProductListUser()
        {
            InitializeComponent();
            GetProducts();
            LoadData();
            txtList.Text = $"{Login.passText} list of products";
            //DataContext = this;

            //total amounts
            List<double> amount = ProductList.finalAmount;
            foreach (double Item in amount)
            {
                total += Item;
            }
            txtTotal.Text = total + " $";

            double num = 0;
            //total numbers
            List<double> totals = ProductList.finalNumber;
            foreach (double Item in totals)
            {
                number += Item;
            }
            number2 = number;
            txtAmount.Text = number.ToString();
            txtItemCount.Text = itemsCount.ToString();

            ProductList.finalAmount.Clear();
            ProductList.finalNumber.Clear();
        }

        void GetProducts()
        {
            var db = new CustomerListDb();
            list = db.customerList.ToList();
            dgList.ItemsSource = list;           
        }

        void GetResult()
        {
            GetProducts();
            dgList.Columns[0].Visibility = Visibility.Hidden;
            dgList.Columns[1].Visibility = Visibility.Hidden;
            dgList.Columns[2].Visibility = Visibility.Hidden;
            dgList.Columns[4].Visibility = Visibility.Hidden;
            dgList.Columns[7].Visibility = Visibility.Hidden;
            list.RemoveAll(x => x.Username != Login.passText);
        }

        public void Refresh()
        {
            new ProductListUser().Show();
            this.Close();
        }

        void ItemDelete()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"DELETE FROM {tableName} where OrderID = @OrderID", con);
            cmd.Parameters.AddWithValue("@OrderID", index);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void ReadProduct()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * From ProductLists where Product = @Product", con);
            cmd.Parameters.AddWithValue("@Product", name);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                InitialAmount = Convert.ToDouble(da.GetValue(5));
                CurrentStatus = da.GetValue(6).ToString();
            }
            con.Close();    
        }

        void DeleteSingleItem()
        {
            con.Open();
            string query = $"Update ProductLists Set Amount = @Amount Where Product = @Product";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Amount", AddedAmount);
            cmd.Parameters.AddWithValue("@Product", name);
            cmd.ExecuteNonQuery();
            con.Close();

            if (CurrentStatus == "Sold Out")
            {
                con.Open();
                query = $"UPDATE ProductLists SET Status = @Status Where Product = @Product";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Status", "Available");
                cmd.Parameters.AddWithValue("@Product", name);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        void DeleteAllItems()
        {
            foreach (var item in list)
            {
                name = item.Product;
                ReadProduct();
                AddedAmount = InitialAmount + item.Amount;
                DeleteSingleItem();
            }
        }

        void KeepResult()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"DELETE FROM CustomerListFinal WHERE InputName = '' ", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void TableClear()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"DELETE FROM CustomerList where AccountID = @AccountID", con);
            cmd.Parameters.AddWithValue("@AccountID", Login.GetID);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void LoadData()
        {
            lock (syncLock)
            {
                if (!Home.isRun)
                {
                    if (dgList.Items.Count != 0)
                    {
                        ProductList.finalAmount.Clear();
                        ProductList.finalNumber.Clear();
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Select * From CustomerList where AccountID = @AccountID", con);
                        cmd.Parameters.AddWithValue("@AccountID", Login.GetID);
                        SqlDataReader da = cmd.ExecuteReader();
                        while (da.Read())
                        {
                            double values = Convert.ToDouble(da.GetValue(6));
                            double amounts = Convert.ToDouble(da.GetValue(7));
                            double total = values * amounts;

                            ProductList.finalNumber.Add(amounts);
                            ProductList.finalAmount.Add(total);
                        }
                        con.Close();
                        Home.isRun = true;
                    }
                }
            }
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            var row = sender as DataGridRow;
            var select = row.DataContext as CustomerList;
            index = select.OrderID;
            name = select.Product;
            amount = select.Amount;
            itemPrice = select.Price;
            itemSum = select.Amount * select.Price;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            new ProductList().Show();
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            GetResult();
            if(dgList.Items.Count != 0)
            {
                if (index == null)
                {
                    System.Windows.MessageBox.Show("Choose a product first", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                }
                else
                {
                    ProductListUserAmount productListUserAmount = new ProductListUserAmount(this);
                    Opacity = 0.2;
                    productListUserAmount.ShowDialog();
                    Opacity = 1;
                    Refresh();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Your list is empty");
                index = null;
                Refresh();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            GetResult();
            if(dgList.Items.Count != 0)
            {
                if (index == null)
                {
                    System.Windows.MessageBox.Show("Choose a product first", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                }
                else
                {
                    var result = System.Windows.MessageBox.Show("The product will be remove from the list, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        
                        tableName = "CustomerList";
                        ItemDelete();

                        tableName = "CustomerListFinal";
                        ItemDelete();

                        ReadProduct();
                        AddedAmount = InitialAmount + amount;
                        DeleteSingleItem();

                        total -= itemSum;
                        number -= amount;
                        itemsCount++;
                        Refresh();
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Your list is empty, modified count reset");
                index = null;
                total = 0;
                Refresh();
            }
            
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            index = null;
            if (dgList.Items.Count != 0)
            {
                var result = System.Windows.MessageBox.Show("All product will be remove from the list, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    DeleteAllItems();

                    total = 0;
                    number = 0;
                    itemsCount = 0;
                    txtAmount.Text = total.ToString();
                    txtTotal.Text = total + " $";
                    txtItemCount.Text = "0";

                    list.Clear();
                    dgList.Items.Refresh();
                    //dgList.Items.Clear();

                    //CustomerList
                    TableClear();

                    //CustomerListFinal
                    KeepResult();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Your list is empty");
                total = 0;
                itemsCount = 0;
                Refresh();
            }
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

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (dgList.Items.Count != 0)
            {
                var result = System.Windows.MessageBox.Show("Your list can't not be change after this, continue?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    new Checkout().Show();
                    this.Close();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Empty list","Error");
            }
        }

        private void dgList_Loaded(object sender, RoutedEventArgs e)
        {
            GetResult();
        }
    }
}
