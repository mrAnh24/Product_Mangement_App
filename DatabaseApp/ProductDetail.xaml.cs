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
    /// Interaction logic for ProductDetail.xaml
    /// </summary>
    public partial class ProductDetail : Window
    {
        public ProductDetail(Window ParentWindow)
        {
            InitializeComponent();
            Owner = ParentWindow;
            txtProduct.Text = ProductList.detail;
            txtDescription.Text = "Products description: Adaptability is the ability to adjust and thrive in changing circumstances," +
                        " and it is essential to navigating the complexities of life. Whether it's adapting to new technologies, " +
                        "social norms, or personal challenges, adaptability allows us to stay resilient and flexible in the face of change." +
                        "Optimism is the belief that good things can happen, even in the face of challenges and adversity, " +
                        "and it is essential to maintaining a positive outlook and sense of hope. Whether it's focusing on the positive aspects of a situation," +
                        "reframing challenges as opportunities, or seeking out support and encouragement, optimism can help us stay resilient and hopeful in difficult times." +
                        "One of the most important uses of technology in nature is the development of conservation tools. " +
                        "From wildlife tracking devices to habitat restoration techniques, we are constantly finding new ways to protect endangered species and their ecosystems." +
                        " With the help of technology, we can make a real difference in the fight against extinction.";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Successfully added to list","Information");
            MessageBox.Show("Not yet implemented", "Nice try");
        }

        private void btnEscape_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
