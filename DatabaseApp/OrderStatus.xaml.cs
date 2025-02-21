using DatabaseApp.Data.DataModels;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for OrderStatus.xaml
    /// </summary>
    public partial class OrderStatus : Window
    {
        public static string cancel = "Order cancel";
        public static string img = "🚫";
        public string status;
        public string stage;

        public OrderStatus(Window parentWindow)
        {
            Owner = parentWindow;
            InitializeComponent();
            txtTitle.Text = "Order " + AccountOrder.index;
            CurrentStage();
        }
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        // Approve zone
        void Stage1()
        {
            txtStep1T.Visibility = Visibility.Visible;
            txtStep1B.Visibility = Visibility.Visible;

            CircleProgress0.Fill = Brushes.ForestGreen;
            LineProgress0.Fill = Brushes.ForestGreen;
            LineProgress1L.Fill = Brushes.ForestGreen;
            CircleProgress1.Fill = Brushes.ForestGreen;
        }
        void Stage2()
        {
            Stage1();
            txtStep2T.Visibility = Visibility.Visible;
            txtStep2B.Visibility = Visibility.Visible;

            LineProgress1R.Fill = Brushes.ForestGreen;
            LineProgress2L.Fill = Brushes.ForestGreen;
            CircleProgress2.Fill = Brushes.ForestGreen;

        }
        void Stage3()
        {
            Stage2();
            txtStep3T.Visibility = Visibility.Visible;
            txtStep3B.Visibility = Visibility.Visible;

            LineProgress2R.Fill = Brushes.ForestGreen;
            LineProgress3L.Fill = Brushes.ForestGreen;
            CircleProgress3.Fill = Brushes.ForestGreen;
        }
        void Stage4()
        {
            Stage3();
            txtStep4T.Visibility = Visibility.Visible;
            txtStep4B.Visibility = Visibility.Visible;

            LineProgress3R.Fill = Brushes.ForestGreen;
            LineProgress4.Fill = Brushes.ForestGreen;
            CircleProgress4.Fill = Brushes.ForestGreen;
        }


        // Cancel zone

        void Stage0Cancel()
        {
            CircleProgress0.Fill = Brushes.Red;
            txtStep0T.Text = img;
            txtStep0B.Text = cancel;
            txtStep0B.Foreground = Brushes.Red;
        }

        void Stage1Cancel()
        {
            Stage0Cancel();
            txtStep0T.Text = "📋";
            txtStep0B.Text = "Checkout complete";

            txtStep1T.Visibility = Visibility.Visible;
            txtStep1B.Visibility = Visibility.Visible;
            txtStep1T.Text = img;
            txtStep1B.Text = cancel;
            txtStep1B.Foreground = Brushes.Red;

            LineProgress0.Fill = Brushes.Red;
            LineProgress1L.Fill = Brushes.Red;
            CircleProgress1.Fill = Brushes.Red;
        }
        void Stage2Cancel()
        {
            Stage1Cancel();
            txtStep1T.Text = "📝";
            txtStep1B.Text = "Order approved";

            txtStep2T.Visibility = Visibility.Visible;
            txtStep2B.Visibility = Visibility.Visible;
            txtStep2T.Text = img;
            txtStep2B.Text = cancel;
            txtStep2B.Foreground = Brushes.Red;

            LineProgress1R.Fill = Brushes.Red;
            LineProgress2L.Fill = Brushes.Red;
            CircleProgress2.Fill = Brushes.Red;

        }
        void Stage3Cancel()
        {
            Stage2Cancel();
            txtStep2T.Text = "📦";
            txtStep2B.Text = "Transferred to shipping unit";

            txtStep3T.Visibility = Visibility.Visible;
            txtStep3B.Visibility = Visibility.Visible;
            txtStep3T.Text = img;
            txtStep3B.Text = cancel;
            txtStep3B.Foreground = Brushes.Red;

            LineProgress2R.Fill = Brushes.Red;
            LineProgress3L.Fill = Brushes.Red;
            CircleProgress3.Fill = Brushes.Red;
        }
        void Stage4Cancel()
        {
            Stage3Cancel();
            txtStep3T.Text = "🚀";
            txtStep3B.Text = "Delivering";

            txtStep4T.Visibility = Visibility.Visible;
            txtStep4B.Visibility = Visibility.Visible;
            txtStep4T.Text = img;
            txtStep4B.Text = cancel;
            txtStep4B.Foreground = Brushes.Red;

            LineProgress3R.Fill = Brushes.Red;
            LineProgress4.Fill = Brushes.Red;
            CircleProgress4.Fill = Brushes.Red;
        }
        
        void LoadOrder()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from CustomerOrder where CustomerID = @CustomerID", con);
            cmd.Parameters.AddWithValue("@CustomerID", AccountOrder.index);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                status = da.GetValue(3).ToString();
                stage = da.GetValue(4).ToString();
            }
            con.Close();
        }

        void CurrentStage()
        {
            LoadOrder();
            if (stage == "Stage 0")
            {
                if (status == "Order cancel")
                {
                    Stage0Cancel();
                }
            }
            else if (stage == "Stage 1")
            {
                if (status == "Order cancel")
                {
                    Stage1Cancel();
                }
                else
                {
                    Stage1();
                }
            }
            else if (stage == "Stage 2")
            {
                if (status == "Order cancel")
                {
                    Stage2Cancel();
                }
                else
                {
                    Stage2();
                }
            }
            else if (stage == "Stage 3")
            {
                if (status == "Order cancel")
                {
                    Stage3Cancel();
                }
                else
                {
                    Stage3();
                }
            }
            else if (stage == "Stage 4")
            {
                if (status == "Order cancel")
                {
                    Stage4Cancel();
                }
                else
                {
                    Stage4();
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            AccountOrder.index = null;
        }

        private void btnEscape_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
