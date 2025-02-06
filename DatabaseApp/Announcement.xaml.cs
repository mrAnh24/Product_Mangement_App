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
using MessageBox = System.Windows.MessageBox;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for Announcement.xaml
    /// </summary>
    public partial class Announcement : Window
    {
        public Announcement()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void cbTarget_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtName.IsEnabled = false;
            switch (cbTarget.SelectedIndex.ToString())
            {
                case "0":
                    cbTarget.Text = "All";
                    break;
                case "1":
                    cbTarget.Text = "Lv4";
                    break;
                case "2":
                    cbTarget.Text = "Lv2 and Lv3";
                    break;
                case "3":
                    cbTarget.Text = "Lv1";
                    break;
                case "4":
                    txtName.IsEnabled = true;
                    cbTarget.Text = "Specific account";
                    break;
            }
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbRequestType.IsEnabled = false;
            switch (cbCategory.SelectedIndex.ToString())
            {
                case "0":
                    cbRequestType.IsEnabled = true;
                    cbTarget.Text = "Request";
                    break;
                case "1":
                    cbTarget.Text = "Account news";
                    break;
                case "2":
                    cbTarget.Text = "Product news";
                    break;
                case "3":
                    cbTarget.Text = "Other";
                    break;
            }
        }

        private void cbRequestType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbRequestType.SelectedIndex.ToString())
            {
                case "0":
                    cbTarget.Text = "Account upgrade request";
                    break;
                case "1":
                    cbTarget.Text = "New product request";
                    break;
                case "2":
                    cbTarget.Text = "Pre-Order product";
                    break;
            }
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Enter a name", "Error");
            }
            else
            {
                txtCheck.Visibility = Visibility.Visible;
                if (txtName.Text == "John Doe") //Temp name
                {
                    txtCheck.Foreground = Brushes.ForestGreen;
                }
                else
                {
                    txtCheck.Foreground = Brushes.Red;
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = txtDetails.Text = 
            cbTarget.Text = cbCategory.Text =
            txtName.Text = cbRequestType.Text = "";
            txtCheck.Visibility = Visibility.Collapsed;
            btnPost.IsEnabled = false;
        }

        private void btnPost_Click(object sender, RoutedEventArgs e)
        {
            if(txtDisplay.Text == "" || txtDetails.Text == "" ||
            cbTarget.Text == "" || cbCategory.Text == "" ||
            txtName.Text == "" || cbRequestType.Text == "")
            {
                MessageBox.Show("Fill all the field to continue", "Error");
            }
            else if(txtCheck.Foreground == Brushes.Red && txtCheck.Visibility == Visibility.Visible)
            {
                MessageBox.Show("Enter a valid name", "Error");
            }
            else
            {
                var result = System.Windows.MessageBox.Show("Cancel this pre-order request?", "Warning", MessageBoxButton.YesNo, (MessageBoxImage)MessageBoxIcon.Information);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Post successfully, Notice");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Announcement canceled", "Notice");
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show("You haven't post the announcement yet, go back anyway?", "Warning", MessageBoxButton.YesNo, (MessageBoxImage)MessageBoxIcon.Information);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
