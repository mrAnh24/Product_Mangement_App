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
    /// Interaction logic for UpdateProductAdminDetail.xaml
    /// </summary>
    public partial class UpdateProductAdminDetail : Window
    {
        public UpdateProductAdminDetail(Window ParentWindow)
        {
            Owner = ParentWindow;
            InitializeComponent();
            txtHeader.Text = $"Product {UpdateProductAdmin.p1} Detail";
            //txtCreator.Text = $"Created at {UpdateProductAdmin.p8} by {UpdateProductAdmin.p7}";
            //txtEditor.Text = $"Last modifier at {UpdateProductAdmin.p10} by {UpdateProductAdmin.p9}";
            tbDescription.Text = UpdateProductAdmin.p2;
            tbCreator.Text = UpdateProductAdmin.p7;
            tbEditor.Text = UpdateProductAdmin.p9;
            tbCreatedTime.Text = UpdateProductAdmin.p8;
            tbEditorTime.Text = UpdateProductAdmin.p10;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
