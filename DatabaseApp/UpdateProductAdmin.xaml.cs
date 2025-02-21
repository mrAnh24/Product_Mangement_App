using DatabaseApp.Data;
using DatabaseApp.Data.DataModels;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for UpdateProductAdmin.xaml
    /// </summary>
    public partial class UpdateProductAdmin : Window
    {
        List<ProductLists> products = new List<ProductLists>();
        List<AccountTest> accountName = new List<AccountTest>();
        public static string index;
        public string pressed;
        public string action;
        public string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string name;
        public double notify;
        public string detailText;
        public string displayText;
        public string discount;

        //Product info//
        public static string p0;
        public static string p1;
        public static string p2;
        public static string p3;
        public static string p4;
        public static string p5;
        public static string p6;
        public static string p7;
        public static string p8;
        public static string p9;
        public static string p10;
        public static string p11;
        //Product info//

        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        public UpdateProductAdmin()
        {
            InitializeComponent();
            GetProducts();
            pressed = "No";
            discount = "No";
            cbFilter.SelectedIndex = 0;
            index = null;
        }

        void GetProducts()
        {
            var db = new ProductListDb();
            products = db.ProductLists.ToList();
            dgProduct.ItemsSource = products;
            txtCount.Text = $"Number of product: {dgProduct.Items.Count}";
        }

        void GetNotifyCount()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * From AccountLinked where Username = @Username", con);
            cmd.Parameters.AddWithValue("@Username", name);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                notify = Convert.ToDouble(da.GetValue(6));
            }
            con.Close();
        }

        void NotifyCount()
        {
            var dx = new AccountDb();
            accountName = dx.Accounts.ToList();

            foreach (var item in accountName)
            {
                name = item.Username;
                GetNotifyCount();

                con.Open();
                string query = $"Update AccountLinked Set NotifyCount = @NotifyCount Where Username = @Username";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@NotifyCount", notify + 1);
                cmd.Parameters.AddWithValue("@Username", name);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Default();
            GetProducts();
            switch (cbFilter.SelectedIndex.ToString())
            {
                case "0":
                    cbFilter.Text = "All";
                    break;
                case "1":
                    cbFilter.Text = "Available";
                    products.RemoveAll(x => x.Status != "Available");
                    break;
                case "2":
                    cbFilter.Text = "On sale";
                    products.RemoveAll(x => x.Status != "On sale");
                    OnSale();
                    break;
                case "3":
                    cbFilter.Text = "Unavailable";
                    products.RemoveAll(x => x.Status != "Unavailable");
                    break;
                case "4":
                    cbFilter.Text = "Sold Out";
                    products.RemoveAll(x => x.Status != "Sold Out");
                    break;
                case "5":
                    cbFilter.Text = "Discontinue";
                    products.RemoveAll(x => x.Status != "Discontinue");
                    break;
            }
        }

        private void cbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pressed == "Yes")
            {
                NotOnSale();
                switch (cbStatus.SelectedIndex.ToString())
                {
                    case "0":
                        cbStatus.Text = "Available";
                        break;
                    case "1":
                        cbStatus.Text = "On sale";
                        OnSale();
                        if (txtDA.Text == "0")
                        {
                            txtDA.Text = "5";
                        }
                        break;
                    case "2":
                        cbStatus.Text = "Unavailable";
                        break;
                    case "3":
                        cbStatus.Text = "Sold Out";
                        break;
                    case "4":
                        cbStatus.Text = "Discontinue";
                        break;
                }
                TextBoxEnable();
            }
            else
            {
                TextBoxDisable();
            }
        }

        private void dgProduct_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgProduct.Columns[0].Visibility = Visibility.Hidden;    // (p0)  ProductCode
            dgProduct.Columns[1].Visibility = Visibility.Hidden;    // (p1)  Product
            //dgProduct.Columns[2].Visibility = Visibility.Hidden;  // (p2)  Description
            dgProduct.Columns[3].Visibility = Visibility.Hidden;    // (p3)  Type
            dgProduct.Columns[4].Visibility = Visibility.Hidden;    // (p4)  Price
            dgProduct.Columns[5].Visibility = Visibility.Hidden;    // (p5)  Amount
            dgProduct.Columns[6].Visibility = Visibility.Hidden;    // (p6)  Status
            dgProduct.Columns[7].Visibility = Visibility.Hidden;    // (p7)  CreatedBy
            dgProduct.Columns[8].Visibility = Visibility.Hidden;    // (p8)  TimeCreated
            dgProduct.Columns[9].Visibility = Visibility.Hidden;    // (p9)  ModifiedBy
            dgProduct.Columns[10].Visibility = Visibility.Hidden;   // (p10)  TimeModified
            dgProduct.Columns[11].Visibility = Visibility.Hidden;   // (p11)  SalePercent

            dgProduct.Columns[2].Header = "Product";
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            index = null;
            var row = sender as DataGridRow;
            var select = row.DataContext as ProductLists;
            index = txtPCode.Text = p0 = select.ProductCode;

            p1 = select.Product;
            p2 = select.Description;
            cbType.Text = p3 = select.Type.ToString();
            txtPrice.Text = p4 = select.Price.ToString();
            txtAmount.Text = p5 = select.Amount.ToString();
            cbStatus.Text = p6 = select.Status.ToString();
            //p7 = select.CreatedBy;
            //p8 = select.TimeCreated.ToString();
            //p9 = select.ModifiedBy;
            //p10 = select.TimeModified.ToString();
            txtDA.Text = p11 = select.SalePercent.ToString();

            if (p6 == "On sale")
            {
                OnSale();
            }
            else
            {
                NotOnSale();
            }
        }

        void ProductChange()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"INSERT INTO AccountNotify VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + displayText + "','" + detailText + "', '" + "Product news" + "', '" + "none" + "','" + "Complete" + "', '" + currentdatetime + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void ActivityLog()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO ActivityLog VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + $"{action} product" + "', '" + "Products modified" + "', '" + currentdatetime + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void adminNotify()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"INSERT INTO AccountNotify VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + $"{p1} information modified" + "','" + detailText + "', '" + "Data modified" + "', '" + "admin" + "','" + "Complete" + "', '" + currentdatetime + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void TextBoxEnable()
        {
            txtPrice.IsEnabled = cbType.IsEnabled
            = cbStatus.IsEnabled = true;

            btnPClear.Visibility = pPUp.Visibility
            = btnPAdd.Visibility = pPDown.Visibility
            = Visibility.Visible;

            if (cbStatus.Text != "Discontinue")
            {
                txtAmount.IsEnabled = true;
                btnAClear.Visibility = pAUp.Visibility
                = btnAAdd.Visibility = pADown.Visibility
                = Visibility.Visible;
            }
            else
            {
                txtAmount.IsEnabled = true;
                btnAClear.Visibility = pAUp.Visibility
                = btnAAdd.Visibility = pADown.Visibility
                = Visibility.Hidden;
            }

            if (cbStatus.Text == "On sale")
            {
                btnDClear.Visibility = pDUp.Visibility
                = pDDown.Visibility = btnDAdd.Visibility
                = Visibility.Visible;
            }
            else
            {
                btnDClear.Visibility = pDUp.Visibility
                = pDDown.Visibility = btnDAdd.Visibility
                = Visibility.Hidden;
            }
        }

        void TextBoxDisable()
        {
            txtPrice.IsEnabled = txtAmount.IsEnabled
            = cbType.IsEnabled = cbStatus.IsEnabled = false;

            btnPClear.Visibility = pPUp.Visibility = btnPAdd.Visibility
            = pPDown.Visibility = btnAClear.Visibility = pAUp.Visibility
            = btnAAdd.Visibility = pADown.Visibility = Visibility.Collapsed;

            if (cbStatus.Text == "On sale")
            {
                btnDClear.Visibility = pDUp.Visibility
                = pDDown.Visibility = btnDAdd.Visibility
                = Visibility.Collapsed;
            }
        }

        void OnSale()
        {
            btnDiscount.Visibility = Visibility.Visible;
            txtPCode.Visibility = Visibility.Hidden;
            ttDA.Visibility = Visibility.Visible;
            txtDA.Visibility = Visibility.Visible;
        }

        void NotOnSale()
        {
            btnDiscount.Visibility = Visibility.Collapsed;
            txtPCode.Visibility = Visibility.Visible;
            ttDA.Visibility = Visibility.Hidden;
            txtDA.Visibility = Visibility.Hidden;
        }

        void Default()
        {
            index = null;
            txtPCode.Text = "0000-AA";
            txtPrice.Text = "0";
            txtAmount.Text = "0";
            cbType.Text = "";
            cbStatus.Text = "";
            btnDiscount.Visibility = Visibility.Collapsed;

            NotOnSale();
        }

        void Refresh()
        {
            index = null;
            new UpdateProductAdmin().Show();
            this.Close();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void txtPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(txtPrice.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }

        private void txtAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(txtAmount.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }

        private void dgProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Opacity = 0.2;
            UpdateProductAdminDetail productDetail = new UpdateProductAdminDetail(this);
            productDetail.ShowDialog();
            Opacity = 1;
        }


        //Price adjustment
        private void btnPClear_Click(object sender, RoutedEventArgs e)
        {
            txtPrice.Text = "0";
        }

        private void pPUp_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(txtPrice.Text) >= 999)
            {
                txtPrice.Text = "999.99";
            }
            else if(Convert.ToDouble(txtPrice.Text) == 999.99)
            {
                System.Windows.MessageBox.Show("Maximum value reach", "Notification");
            }
            else
            {
                txtPrice.Text = (Convert.ToDouble(txtPrice.Text) + 1).ToString();
            }
        }

        private void pPDown_Click(object sender, RoutedEventArgs e)
        {
            if(Convert.ToDouble(txtPrice.Text) < 1 && Convert.ToDouble(txtPrice.Text) -1 < 1)
            {
                txtPrice.Text = "0";
            }
            else if (Convert.ToDouble(txtPrice.Text) == 0)
            {
                System.Windows.MessageBox.Show("Enter price > 0", "Notification");
            }
            else
            {
                txtPrice.Text = (Convert.ToDouble(txtPrice.Text) - 1).ToString();
            }
        }

        private void btnPAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(txtPrice.Text) > 999 || Convert.ToDouble(txtPrice.Text) + 50 > 999)
            {
                txtPrice.Text = "999.99";
            }
            else if (Convert.ToDouble(txtPrice.Text) == 999.99)
            {
                System.Windows.MessageBox.Show("Maximum value reach", "Notification");
            }
            else
            {
                txtPrice.Text = (Convert.ToDouble(txtPrice.Text) + 50).ToString();
            }
        }


        //Amount adjustment
        private void btnAClear_Click(object sender, RoutedEventArgs e)
        {
            txtAmount.Text = "0";
        }

        private void pAUp_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(txtAmount.Text) >= 99)
            {
                txtAmount.Text = "99";
            }
            else if (Convert.ToDouble(txtAmount.Text) == 99)
            {
                System.Windows.MessageBox.Show("Maximum value reach", "Notification");
            }
            else
            {
                txtAmount.Text = (Convert.ToDouble(txtAmount.Text) + 1).ToString();
            }
        }

        private void pADown_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(txtAmount.Text) < 1 || Convert.ToDouble(txtAmount.Text) - 1 < 1)
            {
               txtAmount.Text = "0";
            }
            else if (Convert.ToDouble(txtAmount.Text) == 0)
            {
                System.Windows.MessageBox.Show("Enter amount > 0", "Notification");
            }
            else
            {
                txtAmount.Text = (Convert.ToDouble(txtAmount.Text) - 1).ToString();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(Convert.ToDouble(txtAmount.Text) > 98 || Convert.ToDouble(txtAmount.Text) + 10 > 98)
            {
                txtAmount.Text = "99";
            }
            else if (Convert.ToDouble(txtAmount.Text) == 99)
            {
                System.Windows.MessageBox.Show("Maximum value reach", "Notification");
            }
            else
            {
                txtAmount.Text = (Convert.ToDouble(txtAmount.Text) + 10).ToString();
            }
        }

        //Discount adjustment
        private void btnDClear_Click(object sender, RoutedEventArgs e)
        {
            txtDA.Text = "5";
            txtDiscount.Text = (Convert.ToDouble(txtPrice.Text) - (Convert.ToDouble(txtPrice.Text) * 0.05)).ToString();
        }

        private void pDUp_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(txtDA.Text) > 90)
            {
                txtDA.Text = "95";
                txtDiscount.Text = (Convert.ToDouble(txtPrice.Text) - (Convert.ToDouble(txtPrice.Text) * 0.95)).ToString();
            }
            else if (Convert.ToDouble(txtDA.Text) == 95)
            {
                System.Windows.MessageBox.Show("Maximum value reach", "Notification");
                txtDiscount.Text = (Convert.ToDouble(txtPrice.Text) - (Convert.ToDouble(txtPrice.Text) * 0.95)).ToString();
            }
            else
            {
                txtDA.Text = (Convert.ToDouble(txtDA.Text) + 5).ToString();
                txtDiscount.Text = (Convert.ToDouble(txtPrice.Text) - (Convert.ToDouble(txtPrice.Text) * (Convert.ToDouble(txtDA.Text) / 100) )).ToString();
            }
        }

        private void pDDown_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(txtDA.Text) < 10)
            {
                txtDA.Text = "5";
                txtDiscount.Text = (Convert.ToDouble(txtPrice.Text) - (Convert.ToDouble(txtPrice.Text) * 0.05)).ToString();
            }
            else if (Convert.ToDouble(txtDA.Text) == 5)
            {
                System.Windows.MessageBox.Show("Minimum value reach", "Notification");
                txtDiscount.Text = (Convert.ToDouble(txtPrice.Text) - (Convert.ToDouble(txtPrice.Text) * 0.05)).ToString();
            }
            else
            {
                txtDA.Text = (Convert.ToDouble(txtDA.Text) - 5).ToString();
                txtDiscount.Text = (Convert.ToDouble(txtPrice.Text) - (Convert.ToDouble(txtPrice.Text) * (Convert.ToDouble(txtDA.Text) / 100))).ToString();
            }
        }

        private void btnDAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(txtDA.Text) > 90 || Convert.ToDouble(txtDA.Text) + 25 > 90)
            {
                txtDA.Text = "95";
                txtDiscount.Text = (Convert.ToDouble(txtPrice.Text) - (Convert.ToDouble(txtPrice.Text) * 0.95)).ToString();
            }
            else if (Convert.ToDouble(txtDA.Text) == 95)
            {
                System.Windows.MessageBox.Show("Maximum value reach", "Notification");
                txtDiscount.Text = (Convert.ToDouble(txtPrice.Text) - (Convert.ToDouble(txtPrice.Text) * 0.95)).ToString();
            }
            else
            {
                txtDA.Text = (Convert.ToDouble(txtDA.Text) + 25).ToString();
                txtDiscount.Text = (Convert.ToDouble(txtPrice.Text) - (Convert.ToDouble(txtPrice.Text) * (Convert.ToDouble(txtDA.Text) / 100))).ToString();
            }
        }

        //Big button
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(index != null)
            {
                if (txtPrice.Text == "00" || txtPrice.Text == "000" || txtPrice.Text == "0000" ||  txtPrice.Text == "00000" ||
                    txtPrice.Text == "000000" || Convert.ToDouble(txtPrice.Text) > 999.99)
                {
                    System.Windows.MessageBox.Show("Enter a valid Price number (Range from 0 - 999.99)");
                }
                else if (txtAmount.Text == "00" || txtAmount.Text.Contains("."))
                {
                    System.Windows.MessageBox.Show("Enter a valid Amount number (Range from 0 - 99)");
                }
                else
                {
                    if (pressed == "No")
                    {
                        TextBoxEnable();
                        btnUpdate.Content = "Confirm";
                        btnDelete.Content = "Cancel";
                        pressed = "Yes";

                        btnDiscount.Foreground = Brushes.White;
                        btnDiscount.IsEnabled = true;
                        dgProduct.IsEnabled = false;
                        cbFilter.IsEnabled = false;
                    }
                    else
                    {
                        if (cbType.Text == p3 && txtPrice.Text == p4 && txtAmount.Text == p5
                            && cbStatus.Text == p6 && txtDA.Text == p11)
                        {
                            System.Windows.MessageBox.Show("No change was made", "Notification");
                            TextBoxDisable();
                            btnUpdate.Content = "Update";
                            btnDelete.Content = "Delete";
                            pressed = "No";

                            btnDiscount.Foreground = Brushes.Black;
                            btnDiscount.IsEnabled = false;
                            dgProduct.IsEnabled = true;
                            cbFilter.IsEnabled = true;
                        }
                        else
                        {
                            if (cbStatus.Text == "Sold Out" && txtAmount.Text != "0")
                            {
                                var result0 = System.Windows.MessageBox.Show("Amount of product > 0, change status to available?", "Notice", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                                if (result0 == MessageBoxResult.Yes)
                                {
                                    cbStatus.Text = "Available";
                                }
                                else
                                {
                                    txtAmount.Text = "0";
                                }
                            }

                            var result = System.Windows.MessageBox.Show($"Change product information?", "Notification", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                            if (result == MessageBoxResult.Yes)
                            {
                                if (txtAmount.Text == "0")
                                {
                                    if (cbStatus.Text != "Available" || cbStatus.Text != "On sale")
                                    {
                                        cbStatus.Text = "Sold Out";
                                    }
                                }                               

                                if(cbStatus.Text != "On sale")
                                {
                                    txtDA.Text = "0";
                                }

                                con.Open();
                                SqlCommand cmd = new SqlCommand("Update ProductLists Set Price = @Price, Type = @Type, Amount = @Amount, Status = @Status, ModifiedBy = @ModifiedBy, TimeModified = @TimeModified, SalePercent = @SalePercent Where ProductCode = @ProductCode", con);
                                cmd.Parameters.AddWithValue("@ProductCode", index);
                                cmd.Parameters.AddWithValue("@Type", cbType.Text);
                                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                                cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                                cmd.Parameters.AddWithValue("@Status", cbStatus.Text);
                                cmd.Parameters.AddWithValue("@ModifiedBy", Login.passText);
                                cmd.Parameters.AddWithValue("@TimeModified", currentdatetime);
                                cmd.Parameters.AddWithValue("@SalePercent", txtDA.Text);
                                cmd.ExecuteNonQuery();
                                con.Close();

                                action = "Update a";
                                ActivityLog();
                                NotifyCount();

                                //Status change
                                if (cbStatus.Text != p6)
                                {
                                    if (cbStatus.Text == "On sale")
                                    {
                                        displayText = $"{p1} now on sale";
                                        detailText = $"{p1} status change to On sale";
                                        ProductChange();
                                    }
                                }

                                if (p6 == "On sale")
                                {
                                    if (cbStatus.Text == "Available")
                                    {
                                        displayText = $"{p1} no longer on sale";
                                        detailText = $"{p1} status change to Available";
                                        ProductChange();
                                    }
                                }

                                if (p6 != "Available")
                                {
                                    if (cbStatus.Text == "Available" && p6 != "On sale")
                                    {
                                        displayText = $"{p1} now available";
                                        detailText = $"{p1} status change to Available";
                                        ProductChange();
                                    }
                                }


                                //admin notify
                                if (Login.passText != "admin")
                                {
                                    name = "admin";

                                    if (cbType.Text != p3)
                                    {
                                        detailText = $"{p1} type change";
                                        GetNotifyCount();
                                        adminNotify();
                                    }

                                    if (txtPrice.Text != p4)
                                    {
                                        detailText = $"{p1} price change";
                                        GetNotifyCount();
                                        adminNotify();
                                    }

                                    if (txtAmount.Text != p5)
                                    {
                                        detailText = $"{p1} amount change";
                                        GetNotifyCount();
                                        adminNotify();
                                    }

                                    if (cbStatus.Text != p6)
                                    {
                                        detailText = $"{p1} status change";
                                        GetNotifyCount();
                                        adminNotify();
                                    }
                                }

                                System.Windows.MessageBox.Show($"Successfully update product {p1}");
                                TextBoxDisable();
                                btnUpdate.Content = "Update";
                                btnDelete.Content = "Delete";
                                pressed = "No";

                                btnDiscount.Foreground = Brushes.Black;
                                btnDiscount.IsEnabled = false;
                                dgProduct.IsEnabled = true;
                                cbFilter.IsEnabled = true;

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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (pressed == "Yes")
            {
                txtPCode.Text = index;
                cbType.Text = p3;
                txtPrice.Text = p4.ToString();
                txtAmount.Text = p5.ToString();
                cbStatus.Text = p6.ToString();
                txtDA.Text = p11.ToString();
                txtDiscount.Text = (Convert.ToDouble(txtPrice.Text) - (Convert.ToDouble(txtPrice.Text) * (Convert.ToDouble(txtDA.Text) / 100))).ToString();

                TextBoxDisable();
                btnDelete.Content = "Delete";
                btnUpdate.Content = "Update";
                pressed = "No";

                btnDiscount.Foreground = Brushes.Black;
                btnDiscount.IsEnabled = false;
                dgProduct.IsEnabled = true;
                cbFilter.IsEnabled = true;
            }
            else
            {
                if (index != null)
                {
                    var result = System.Windows.MessageBox.Show("The product and it's data will be delete, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Delete ProductLists Where ProductCode = @ProductCode", con);
                        cmd.Parameters.AddWithValue("@ProductCode", index);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        action = "Delete a";
                        ActivityLog();
                        System.Windows.MessageBox.Show($"Successfully deleted product {p1}");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Choose a product first");
                }
            }
        }

        private void btnDiscount_Click(object sender, RoutedEventArgs e)
        {
            if (discount == "No")
            {               
                txtPrice.Visibility = Visibility.Hidden;
                txtDiscount.Visibility = Visibility.Visible;
                btnDiscount.Content = "Hide discount";
                txtPrice.IsEnabled = false;
                discount = "Yes";

                btnPClear.IsEnabled = false;
                pPUp.IsEnabled = false;
                pPDown.IsEnabled = false;
                btnPAdd.IsEnabled = false;
                txtDiscount.Text = (Convert.ToDouble(txtPrice.Text) - (Convert.ToDouble(txtPrice.Text) * (Convert.ToDouble(txtDA.Text) / 100))).ToString();
            }
            else
            {
                txtPrice.Visibility = Visibility.Visible;
                txtDiscount.Visibility = Visibility.Collapsed;
                btnDiscount.Content = "Show discount";
                txtPrice.IsEnabled = true;
                discount = "No";
                if (pressed == "No")
                {
                    txtPrice.IsEnabled = false;
                }

                btnPClear.IsEnabled = true;
                pPUp.IsEnabled = true;
                pPDown.IsEnabled = true;
                btnPAdd.IsEnabled = true;
            }
        }
    }
}
