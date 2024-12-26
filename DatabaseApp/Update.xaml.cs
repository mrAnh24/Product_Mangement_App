using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using ClosedXML.Excel;
using DatabaseApp.Data;
using DatabaseApp.Data.DataModels;
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        List<ProductLists> products = new List<ProductLists>();
        public static string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public static string index;
        public static string detail;
        public static string update;
        public double x;

        //--ProductList--/
        public string p0;
        public string p1;
        public string p2;
        public string p3;
        public string p4;
        public string p5;
        public string p6;
        public string p9;
        public string p10;
        //--ProductList--/
        public Update()
        {
            InitializeComponent();
            GetProducts();
            x = 1;
            //LoadGrid();
        }

        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        //Load data
        void GetProducts()
        {
            var db = new ProductListDb();
            products = db.ProductLists.ToList();
            dgProduct.ItemsSource = products;
        }

        void Refresh()
        {
            index = null;
            new Update().Show();
            this.Close();
        }

        void Clear()
        {
            tbProductId.Clear();
            tbProduct.Clear();
            tbDescription.Clear();
            tbPrice.Clear();
            tbAmount.Clear();
            cbType.Text = "";
            cbStatus.Text = "";
            txtUploader.Text = "...";
            Disable();
            x = 1;
            tbProduct.IsEnabled = true;
            index = null;
        }

        public void ActivityLog()
        {
            con.Open();
            string query = "INSERT INTO ActivityLog VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + $"{update} product" + "', '" + "Products modified" + "', '" + currentdatetime + "')"; ;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            //x = 1;
            var row = sender as DataGridRow;
            var select = row.DataContext as ProductLists;
            index = select.ProductCode;

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from ProductLists where ProductCode = @ProductCode", con);
            cmd.Parameters.AddWithValue("@ProductCode", index);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                tbProductId.Text = p0 = da.GetValue(0).ToString();
                tbProduct.Text = p1 = da.GetValue(1).ToString();
                tbDescription.Text = p2 = da.GetValue(2).ToString();
                cbType.Text = p3 = da.GetValue(3).ToString();
                tbPrice.Text = p4 = da.GetValue(4).ToString();
                tbAmount.Text = p5 = da.GetValue(5).ToString();
                cbStatus.Text = p6 = da.GetValue(6).ToString();
                txtUploader.Text = da.GetValue(7).ToString();
                detail = tbProduct.Text.ToString();
            }
            con.Close();
            Disable();
        }

        void Result()
        {
            dgProduct.Columns[2].Visibility = Visibility.Hidden;
            dgProduct.Columns[3].Visibility = Visibility.Hidden;
            dgProduct.Columns[7].Visibility = Visibility.Hidden;
            dgProduct.Columns[8].Visibility = Visibility.Hidden;
            dgProduct.Columns[9].Visibility = Visibility.Hidden;
            dgProduct.Columns[10].Visibility = Visibility.Hidden;
        }

        void Enable()
        {
            tbProductId.IsEnabled = true;
            tbProduct.IsEnabled = true;
            tbDescription.IsEnabled = true;
            tbPrice.IsEnabled = true;
            tbAmount.IsEnabled = true;
            cbType.IsEnabled = true;
            cbStatus.IsEnabled = true;
        }

        void Disable()
        {
            tbProductId.IsEnabled = false;
            tbProduct.IsEnabled = false;
            tbDescription.IsEnabled = false;
            tbPrice.IsEnabled = false;
            tbAmount.IsEnabled = false;
            cbType.IsEnabled = false;
            cbStatus.IsEnabled = false;
        }

        void ButtonEnable()
        {
            dgProduct.IsEnabled = true;

            btnAdd.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnClear.IsEnabled = true;

            btnAdd.Foreground = Brushes.WhiteSmoke;
            btnDelete.Foreground = Brushes.WhiteSmoke;
            btnClear.Foreground = Brushes.WhiteSmoke;
        }

        void buttonDisable()
        {
            dgProduct.IsEnabled = false;

            btnAdd.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnClear.IsEnabled = false;

            btnAdd.Foreground = Brushes.Black;
            btnDelete.Foreground = Brushes.Black;
            btnClear.Foreground = Brushes.Black;
        }

        //Load data
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("Select * from ProductLists", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgProduct.ItemsSource = dt.DefaultView;
        }

        //Auto fill data
        private void tbProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbProduct.Text != "" && tbProductId.Text == "")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from ProductLists where Product = @Product", con);
                cmd.Parameters.AddWithValue("@Product", tbProduct.Text);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    tbProductId.Text = p0 = da.GetValue(0).ToString();
                    tbDescription.Text = p2 = da.GetValue(2).ToString();
                    cbType.Text = p3 = da.GetValue(3).ToString();
                    tbPrice.Text = p4 = da.GetValue(4).ToString();
                    tbAmount.Text = p5 = da.GetValue(5).ToString();
                    cbStatus.Text = p6 = da.GetValue(6).ToString();
                    txtUploader.Text = da.GetValue(7).ToString();
                }
                con.Close();
            }
        }

        //Add new Product
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            UpdateProductAdd updateProductAdd = new UpdateProductAdd(this);
            Opacity = 0.2;
            updateProductAdd.ShowDialog();
            Opacity = 1;          
            Refresh();
        }

        //Update existing Product
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (index != null)
            {
                if (x == 1)
                {
                    btnUpdate.Content = "Confirm";
                    buttonDisable();
                    Enable();
                    x = 2;
                }
                else
                {
                    if (tbProductId.Text != p0)
                    {
                        System.Windows.MessageBox.Show("Product ID can not be change", "Error");
                    }
                    else
                    {
                        if (tbProduct.Text == p1 && tbDescription.Text == p2 &&
                            cbType.Text == p3 && tbPrice.Text == p4 && tbAmount.Text == p5 && cbStatus.Text == p6)
                        {
                            System.Windows.MessageBox.Show("No change was made", "Notification");
                            Disable();
                            btnUpdate.Content = "Update";
                            ButtonEnable();
                            Refresh();
                            x = 1;
                        }
                        else
                        {
                            var result = System.Windows.MessageBox.Show($"Change product information?", "Notification", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                            if (result == MessageBoxResult.Yes)
                            {
                                con.Open();
                                SqlCommand cmd = new SqlCommand("Update ProductLists Set Product = @Product, Description = @Description, Price = @Price, Type = @Type, Amount = @Amount, Status = @Status, ModifiedBy = @ModifiedBy, TimeModified = @TimeModified Where ProductCode = @ProductCode", con);
                                cmd.Parameters.AddWithValue("@ProductCode", tbProductId.Text);
                                cmd.Parameters.AddWithValue("@Product", tbProduct.Text);
                                cmd.Parameters.AddWithValue("@Description", tbDescription.Text);
                                cmd.Parameters.AddWithValue("@Type", cbType.Text);
                                cmd.Parameters.AddWithValue("@Price", tbPrice.Text);
                                cmd.Parameters.AddWithValue("@Amount", tbAmount.Text);
                                cmd.Parameters.AddWithValue("@Status", cbStatus.Text);
                                cmd.Parameters.AddWithValue("@ModifiedBy", Login.passText);
                                cmd.Parameters.AddWithValue("@TimeModified", currentdatetime);
                                cmd.ExecuteNonQuery();
                                con.Close();

                                update = "Update a";
                                ActivityLog();
                                ButtonEnable();
                                System.Windows.MessageBox.Show("Successfully Updated Product");
                                x = 1;
                                index = null;
                                Refresh();
                            }
                        }
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Choose a product first");
            }
        }

        //Delete a Product
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (index != null)
            {
                var result = System.Windows.MessageBox.Show("The product and it's data will be delete, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete ProductLists Where ProductCode = @ProductCode", con);
                    cmd.Parameters.AddWithValue("@ProductCode", tbProductId.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    update = "Delete a";
                    ActivityLog();
                    Refresh();
                    System.Windows.MessageBox.Show("Successfully Deleted Product");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Choose a product first");
            }
        }

        //Refresh Datagrid
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        //Clear all textbox
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            //DataTable dt = new DataTable();
            //dgProduct.ItemsSource = dt.DefaultView;
        }

        private void tbPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(tbPrice.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }

        private void tbAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(tbAmount.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }

        private void dgProduct_AutoGeneratedColumns(object sender, EventArgs e)
        {
            Result();
        }
    }
}
