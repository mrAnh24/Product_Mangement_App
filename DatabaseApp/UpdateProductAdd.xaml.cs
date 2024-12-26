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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for UpdateProductAdd.xaml
    /// </summary>
    public partial class UpdateProductAdd : Window
    {
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        public static string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string p0;

        public UpdateProductAdd(Window parentWindow)
        {
            Owner = parentWindow;
            InitializeComponent();
            txtStatus.Text = "Unavailable";
        }

        void ReadID()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from ProductLists", con);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                p0 = da.GetValue(0).ToString();
            }
            con.Close();
        }

        public void ActivityLog()
        {
            con.Open();
            string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "INSERT INTO ActivityLog VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + $"Add a new product" + "', '" + "Products modified" + "', '" + currentdatetime + "')"; ;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            ReadID();
            if (txtProductID.Text == "" || txtProduct.Text == "" || txtPrice.Text == "" || txtAmount.Text == "" || cbType.Text == "" )
            {
                System.Windows.MessageBox.Show("Fill all (*) field", "Error");
            }
            else
            {
                if (txtProductID.Text != p0)
                {
                    if (!txtProductID.Text.Contains("-"))
                    {
                        System.Windows.MessageBox.Show("Wrong ProductID format", "Error");
                    }
                    else
                    {
                        try
                        {
                            var result = System.Windows.MessageBox.Show($"Add a new product?", "Confirmation", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                            if (result == MessageBoxResult.Yes)
                            {
                                //double.Parse((string){text})
                                con.Open();
                                //SqlCommand cmd = new SqlCommand("INSERT INTO ProductList VALUES ('" + txtProductID.Text + "','" + txtProduct.Text + "','" + txtDescription.Text + "','" + txtPrice.Text + "','" + cbType.Text + "','" + txtAmount.Text + "','" + txtStatus.Text + "','" + Login.passText + "','" + currentdatetime + "','" + Login.passText + "','" + currentdatetime + "')", con);
                                SqlCommand cmd = new SqlCommand("Insert into ProductLists values (@ProductCode, @Product, @Description, @Type , @Price, @Amount, @Status, @CreatedBy, @TimeCreated, @ModifiedBy, @TimeModified)", con);
                                cmd.Parameters.AddWithValue("@ProductCode", txtProductID.Text);
                                cmd.Parameters.AddWithValue("@Product", txtProduct.Text);
                                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                                cmd.Parameters.AddWithValue("@Type", cbType.Text);
                                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                                cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                                cmd.Parameters.AddWithValue("@Status", txtStatus.Text);
                                cmd.Parameters.AddWithValue("@CreatedBy", Login.passText);
                                cmd.Parameters.AddWithValue("@TimeCreated", currentdatetime);
                                cmd.Parameters.AddWithValue("@ModifiedBy", Login.passText);
                                cmd.Parameters.AddWithValue("@TimeModified", currentdatetime);                               
                                cmd.ExecuteNonQuery();
                                con.Close();

                                ActivityLog();
                                System.Windows.MessageBox.Show("Successfully Added new Product");
                                this.Close();
                            }
                    }
                        catch (Exception ex)
                        {
                        System.Windows.MessageBox.Show(ex.Message, "Message", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                        con.Close();
                    }
                }
                }                
                else
                {
                    System.Windows.MessageBox.Show("ProductID already existed", "Error");
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtProductID.Clear();
            txtProduct.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
            txtAmount.Clear();
            cbType.Text = "";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            txtProductID.Text = "1234-AB";
            txtProduct.Text = "Item";
            txtDescription.Text = "a brand new item";
            txtPrice.Text = "69.99";
            txtAmount.Text = "80";
            cbType.Text = "Other";
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
    }
}
