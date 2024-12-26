using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
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
using static ClosedXML.Excel.XLPredefinedFormat;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for ProductDetail.xaml
    /// </summary>
    public partial class ProductDetail : Window
    {
        public ProductDetail(Window ParentWindow)
        {
            InitializeComponent();
            Owner = ParentWindow;

            txtProductDescription.Text = ProductList.productDescription;
            txtProductUploader.Text = ProductList.ProductCreator;

            txtProductCode.Text = ProductList.productID;
            txtProductName.Text = ProductList.productName;
            txtProductType.Text = ProductList.productType;

            txtProductPrice.Text = ProductList.productPrice;
            txtProductAmount.Text = ProductList.productAmount;

            txtStatus.Text = ProductList.productStatus;
            if (txtStatus.Text == "Available")
            {
                txtStatus.Foreground = Brushes.ForestGreen;
            }
            else
            {
                txtStatus.Foreground = Brushes.Red;
            }              
        }

        private void btnEscape_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ProductList.index = null;
        }

        private void tbRequest_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(tbRequest.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }
    }
}
