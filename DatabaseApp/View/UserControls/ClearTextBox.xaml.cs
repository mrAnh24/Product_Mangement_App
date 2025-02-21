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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatabaseApp.View.UserControls
{
    public partial class ClearTextBox : UserControl
    {
        public ClearTextBox()
        {
            InitializeComponent();
        }

        private string placeHolder;

        public string PlaceHolder
        {
            get { return placeHolder; }
            set 
            { 
                placeHolder = value; 
                tbPlaceHolder.Text = placeHolder;
            }
        }

        public string Text { get; internal set; }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Clear();
            txtInput.Focus();
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtInput.Text))
                tbPlaceHolder.Visibility = Visibility.Visible;
            else
                tbPlaceHolder.Visibility = Visibility.Hidden;
        }
    }
}
