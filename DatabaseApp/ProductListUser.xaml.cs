using System;
using System.Collections.Generic;
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
    /// Interaction logic for ProductListUser.xaml
    /// </summary>
    /// 
    public partial class ProductListUser : Window
    {
        public ProductListUser()
        {
            InitializeComponent();
            double total = 0;
            List<double> amount = ProductList.finalAmount;
            foreach(double Item in amount)
            {
                total += Item;
            }
            txtTotal.Text = total + " $";
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            new ProductList().Show();
            this.Close();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show("All product will be remove from the list, are you sure?", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
            if (result == MessageBoxResult.Yes)
            {
                txtTotal.Text = 0 + " $";
            }
        }
    }
}
