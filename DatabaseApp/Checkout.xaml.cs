using DatabaseApp.View.UserControls;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Vml;
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
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for Checkout.xaml
    /// </summary>
    public partial class Checkout : Window
    {
        public static double discountAmount;
        public static string paymentMethod;
        public static string bill;
        public static string status;
        public Checkout()
        {
            InitializeComponent();
            txtUser.Text = Login.passText;
            txtAmount.Text = ProductListUser.number.ToString();
            txtTotals.Text = ProductListUser.total + " $";
            //txtTotals2.Text = discount;
            bill = txtTotals.Text;

            txtStatus.Visibility = Visibility.Collapsed;
            LbAmount2.Visibility = Visibility.Collapsed;
            txtTotals2.Visibility = Visibility.Collapsed;
            LbDiscountAmount.Visibility = Visibility.Collapsed;
            txtDiscountAmount.Visibility = Visibility.Collapsed;

            if(ProductListUser.number2 >= 10)
            {
                txtcode1.Text = "SmallGift (5% OFF)";
                txtcode1.Foreground = Brushes.ForestGreen;
                if (ProductListUser.number2 >= 20)
                {
                    txtcode2.Text = "BiggerOne (10% OFF)";
                    txtcode2.Foreground = Brushes.ForestGreen;
                    if (ProductListUser.number2 >= 50)
                    {
                        txtcode3.Text = "RealDeal (15% OFF)";
                        txtcode3.Foreground = Brushes.ForestGreen;
                        if (ProductListUser.number2 >= 100)
                        {
                            txtcode4.Text = "GiftCode (20% OFF)";
                            txtcode4.Foreground = Brushes.ForestGreen;
                            if (ProductListUser.number2 >= 200)
                            {
                                txtcode5.Text = "GoodDeal (25% OFF)";
                                txtcode5.Foreground = Brushes.ForestGreen;
                                if (ProductListUser.number2 >= 500)
                                {
                                    txtcode6.Text = "MegaDeal (30% OFF)";
                                    txtcode6.Foreground = Brushes.ForestGreen;
                                }
                            }
                        }
                    }
                }
            }
        }
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        public void Clear()
        {
            ProductListUser.itemsCount = 0;
            ProductListUser.total = 0;
            ProductListUser.number = 0;
            ProductList.list.Clear();
            ProductList.finalAmount.Clear();
            ProductList.finalNumber.Clear();
        }

        private void HlBack_Click(object sender, RoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show("Your current products list will be deleted, continue?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Clear();
                new ProductList().Show();
                this.Close();
            }
        }

        public void CodeApllied()
        {
            //System.Windows.MessageBox.Show("Code redeemed", "Code applied");
            txtDiscountAmount.Text = (ProductListUser.total * discountAmount) + " $";
            txtTotals2.Text = (ProductListUser.total - (ProductListUser.total * discountAmount)) + " $";
            bill = txtTotals2.Text;

            txtStatus.Visibility = Visibility.Visible;
            LbAmount1.Visibility = Visibility.Visible;
            txtTotals.Visibility = Visibility.Visible;
            LbAmount2.Visibility = Visibility.Visible;
            txtTotals2.Visibility = Visibility.Visible;
            LbDiscountAmount.Visibility = Visibility.Visible;
            txtDiscountAmount.Visibility = Visibility.Visible;

            txtTotals.TextDecorations = TextDecorations.Strikethrough;
            LbAmount1.TextDecorations = TextDecorations.Strikethrough;
        }

        private void btnCode_Click(object sender, RoutedEventArgs e)
        {
            if (tbCode.Text == "MegaDeal")
            {
                if (txtStatus.Visibility != Visibility.Visible)
                {
                    discountAmount = 0.3;
                    CodeApllied();
                    txtStatus.Text = "Code (30% Off) applied";
                }
                else
                {
                    System.Windows.MessageBox.Show("A code is already applied", "Error");
                }
            }
            else if (tbCode.Text == "GoodDeal")
            {
                if (txtStatus.Visibility != Visibility.Visible)
                {
                    discountAmount = 0.25;
                    CodeApllied();
                    txtStatus.Text = "Code (25% Off) applied";
                }
                else
                {
                    System.Windows.MessageBox.Show("A code is already applied", "Error");
                }
            }
            else if (tbCode.Text == "GiftCode")
            {
                if (txtStatus.Visibility != Visibility.Visible)
                {
                    discountAmount = 0.2;
                    CodeApllied();
                    txtStatus.Text = "Code (20% Off) applied";
                }
                else
                {
                    System.Windows.MessageBox.Show("A code is already applied", "Error");
                }
            }
            else if (tbCode.Text == "RealDeal")
            {
                if (txtStatus.Visibility != Visibility.Visible)
                {
                    discountAmount = 0.15;
                    CodeApllied();
                    txtStatus.Text = "Code (15% Off) applied";
                }
                else
                {
                    System.Windows.MessageBox.Show("A code is already applied", "Error");
                }
            }
            else if (tbCode.Text == "BiggerOne")
            {
                if (txtStatus.Visibility != Visibility.Visible)
                {
                    discountAmount = 0.1;
                    CodeApllied();
                    txtStatus.Text = "Code (10% Off) applied";
                }
                else
                {
                    System.Windows.MessageBox.Show("A code is already applied", "Error");
                }
            }
            else if (tbCode.Text == "SmallGift")
            {
                if (txtStatus.Visibility != Visibility.Visible)
                {
                    discountAmount = 0.05;
                    CodeApllied();
                    txtStatus.Text = "Code (5% Off) applied";
                }
                else
                {
                    System.Windows.MessageBox.Show("A code is already applied", "Error");
                }
            }
            else
            {
                if (tbCode.Text == "")
                {
                    System.Windows.MessageBox.Show("Enter a Code", "Error");
                }
                else
                {
                    System.Windows.MessageBox.Show("Invalid Code", "Error");
                }               
                txtTotals2.Text = ProductListUser.total + " $";
                bill = txtTotals.Text;

                txtStatus.Visibility = Visibility.Collapsed;
                LbAmount1.Visibility = Visibility.Collapsed;
                txtTotals.Visibility = Visibility.Collapsed;
                LbAmount2.Visibility = Visibility.Visible;
                txtTotals2.Visibility = Visibility.Visible;
                LbDiscountAmount.Visibility = Visibility.Collapsed;
                txtDiscountAmount.Visibility = Visibility.Collapsed;

                //txtTotals.TextDecorations.Clear();
                //LbAmount1.TextDecorations.Clear();
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                if (tbName.Text == "" || cbGender.Text == "" || tbAddress.Text == "" || tbCity.Text == "" || tbRegion.Text == "" || tbPostalCode.Text == "" || tbCountry.Text == "" || tbPhone.Text == "")
                {
                    System.Windows.MessageBox.Show("filled all the field with (*)", "Error");
                }
                else
                {
                    if (txtPayment.Text == "Choose an payment method")
                    {
                        System.Windows.MessageBox.Show("Choose an payment method", "Error");
                    }
                    else
                    {
                        var result = System.Windows.MessageBox.Show("Your information will be saved to the systems, continue?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                        if (result == MessageBoxResult.Yes)
                        {
                            con.Open();
                            //String query = "INSERT INTO AdvanceCustomers VALUES ('" + tbName.Text + "','" + tbTitle.Text + "', '" + tbCompany.Text + "', '" + tbAddress.Text + "', '" + tbCity.Text + "', '" + tbRegion.Text + "', '" + tbPostalCode.Text + "', '" + tbCountry.Text + "', '" + tbPhone.Text + "', '" + tbFax.Text + "', '" + currentdatetime + "')";
                            String query = "INSERT INTO CustomerInvoice VALUES ('" + tbName.Text + "','" + cbGender.Text + "','" + tbTitle.Text + "', '" + tbCompany.Text + "', '" + tbAddress.Text + "', '" + tbCity.Text + "', '" + tbRegion.Text + "', '" + tbPostalCode.Text + "', '" + tbCountry.Text + "', '" + tbPhone.Text + "', '" + tbFax.Text + "', '" + paymentMethod + "', '" + bill + "', '" + currentdatetime + "', '" + status + "')";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            System.Windows.MessageBox.Show("Check out successfully", "Info");
                            
                            Clear();
                            new ProductList().Show();
                            this.Close();
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Checkout canceled");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                con.Close();
            }
            
        }

        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            tbAddress.Text = Login.passText + "House";
            cbGender.Text = "Unknown";
            tbTitle.Text = MenuBar.role;
            tbCompany.Text = "This company";
            tbCity.Text = "New York";
            tbRegion.Text = "Middle";
            tbPostalCode.Text = "0350";
            tbCountry.Text = "USA"; 
            tbPhone.Text = "03503846958";
            tbFax.Text = "0904366666";
        }

        private void Epayment_Click(object sender, RoutedEventArgs e)
        {
            txtPayment.Text = "The bill had been payed";
            paymentMethod = "E-wallet";
            status = "Payment complete";

            payment1.Background = Brushes.ForestGreen;
            payment2.Background = Brushes.Red;
            payment3.Background = Brushes.Red;
        }

        private void Bpayment_Click(object sender, RoutedEventArgs e)
        {
            txtPayment.Text = "The bill had been payed";
            paymentMethod = "Bank account";
            status = "Payment complete";

            payment1.Background = Brushes.Red;
            payment2.Background = Brushes.ForestGreen;
            payment3.Background = Brushes.Red;
        }

        private void Cpayment_Click(object sender, RoutedEventArgs e)
        {
            txtPayment.Text = "The bill is on hold";
            paymentMethod = "COD";
            status = "Payment incomplete";

            payment1.Background = Brushes.Red;
            payment2.Background = Brushes.Red;
            payment3.Background = Brushes.ForestGreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Checkout().Show();
            this.Close();
        }

        private void tbPostalCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(tbPostalCode.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }

        private void tbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(tbPhone.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }

        private void tbFax_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(tbFax.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }
    }
}
