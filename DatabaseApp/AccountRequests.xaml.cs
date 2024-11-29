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

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for AccountRequests.xaml
    /// </summary>
    public partial class AccountRequests : Window
    {
        public AccountRequests()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbSearch.Clear();
        }

        private void btnFilter1_Click(object sender, RoutedEventArgs e)
        {
            if (GridCol1.Background == Brushes.ForestGreen)
            {
                GridCol1.Background = Brushes.Red;
            }
            else
            {
                GridCol1.Background = Brushes.ForestGreen;
            }
        }

        private void btnFilter2_Click(object sender, RoutedEventArgs e)
        {
            if (GridCol2.Background == Brushes.ForestGreen)
            {
                GridCol2.Background = Brushes.Red;
            }
            else
            {
                GridCol2.Background = Brushes.ForestGreen;
            }
        }

        private void btnFilter3_Click(object sender, RoutedEventArgs e)
        {
            if (GridCol3.Background == Brushes.ForestGreen)
            {
                GridCol3.Background = Brushes.Red;
            }
            else
            {
                GridCol3.Background = Brushes.ForestGreen;
            }
        }
    }
}
