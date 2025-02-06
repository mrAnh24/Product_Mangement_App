using DocumentFormat.OpenXml.Wordprocessing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static DatabaseApp.dbdemoDataSet;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DatabaseApp.View.UserControls
{
    /// <summary>
    /// Interaction logic for MenuBar.xaml
    /// </summary>
    //Application.Current.Shutdown();
    public partial class MenuBar : UserControl
    {
        public static int count;
        public static string role;
        public static double notifyNumber;
        public static double unRead;

        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        public MenuBar()
        {
            InitializeComponent();

            //--admin--//
            //Login.passText = "admin"; //temporary
            //Login.GetRole = "admin";  //temporary
            //Login.GetID = "Acc00001"; //temporary

            //--Lv4--//
            Login.passText = "John Doe";  //temporary
            Login.GetRole = "Lv4";        //temporary
            Login.GetID = "Acc00002";     //temporary

            ////--Lv3--//
            //Login.passText = "Emma Stock";    //temporary
            //Login.GetRole = "Lv3";            //temporary
            //Login.GetID = "Acc00003";         //temporary

            //--Lv2--//
            //Login.passText = "Mike";  //temporary
            //Login.GetRole = "Lv2";    //temporary
            //Login.GetID = "Acc00004"; //temporary

            //--Lv1--//
            //Login.passText = "test";  //temporary
            //Login.GetRole = "Lv1";    //temporary
            //Login.GetID = "Acc00005"; //temporary

            //--Guest--//
            //Login.passText = "Guest account";     //temporary
            //Login.GetRole = "Guest account";      //temporary

            string accountName = Login.passText;
            txtAccname.Text = accountName;

            //Read role
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from AccountTest where Username = @Username", con);
            cmd.Parameters.AddWithValue("@Username", accountName);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                role = da.GetValue(5).ToString();
            }
            con.Close();
            //Assign role
            if (role !="admin")
            {
                miDatabase.Visibility = Visibility.Collapsed;
                if(role != "Lv4")
                {
                    mAdmin.Visibility = Visibility.Collapsed;
                    miRequest.Visibility = Visibility.Collapsed;
                    if (role != "Lv3")
                    {
                        miUpdateProducts.Visibility= Visibility.Collapsed;
                        if (role != "Lv2")
                        {
                            miUpdateData.Visibility= Visibility.Collapsed;
                            miCSV.Visibility= Visibility.Collapsed;
                            miPermisson.Visibility= Visibility.Collapsed;
                            if (role != "Lv1")
                            {
                                mEdit.Visibility= Visibility.Collapsed;
                                mAccount.Visibility = Visibility.Collapsed;
                                LogOut.Header = "Exit";

                                btnBell.Visibility= Visibility.Collapsed;
                                NotifyBubble.Visibility= Visibility.Collapsed;
                                NotifyText.Visibility= Visibility.Collapsed;
                                btnChat.Visibility= Visibility.Collapsed;
                                ChatBubble.Visibility= Visibility.Collapsed;
                            }
                        }
                    }
                }
            }

            //Read role
            con.Open();
            cmd = new SqlCommand("Select * from AccountLinked where Username = @Username", con);
            cmd.Parameters.AddWithValue("@Username", accountName);
            da = cmd.ExecuteReader();
            while (da.Read())
            {
                notifyNumber = Convert.ToDouble(da.GetValue(6));
            }
            con.Close();

            unRead = 0;

            Notification();
            Chat();

            NotifyText.Visibility = Visibility.Collapsed;
        }

        public void ActivityLog()
        {
            con.Open();
            string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            String query;
            if(LogOut.Header == "Exit")
            {
                query = "INSERT INTO ActivityLog VALUES ('" + "Guest" + "','" + "Guest" + "','" + "Guest" + "','" + "Guest exit" + "', '" + "Notification" + "', '" + currentdatetime + "')";
            }
            else
            {
                query = "INSERT INTO ActivityLog VALUES ('" + Login.GetID+ "','" + Login.passText + "','" + role + "','" + "Account logout" + "', '" + "Notification" + "', '" + currentdatetime + "')";
            }
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void Notification()
        {
            if (Login.passText != "Guest account")
            {
                if (notifyNumber == 0)
                {
                    NotifyBubble.Visibility = Visibility.Collapsed;
                    NotifyText.Visibility = Visibility.Collapsed;
                }
                else
                {
                    NotifyBubble.Visibility = Visibility.Visible;
                    NotifyText.Visibility = Visibility.Visible;
                    NotifyText.Text = $" You have {notifyNumber.ToString()} unread notification ";
                }
            }           
        }

        void Chat()
        {
            if(Login.passText != "Guest account")
            {
                if (unRead == 0)
                {
                    ChatBubble.Visibility = Visibility.Collapsed;
                    NotifyText.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ChatBubble.Visibility = Visibility.Visible;
                    NotifyText.Visibility = Visibility.Visible;
                    NotifyText.Text = $" You have unread chat ";
                }
            }
        }

        //--File Tab--//

        // Home page 
        private void miHome_Click(object sender, RoutedEventArgs e)
        {
            if (Login.passText == "admin" || role =="Lv4")
            {
                new HomeAdmin().Show();
                Application.Current.Windows[0].Close();
            }
            else
            {
                new Home().Show();
                Application.Current.Windows[0].Close();
            }
            count++;
        }

        //Product list
        private void miProduct_Click(object sender, RoutedEventArgs e)
        {
            new ProductList().Show();
            Application.Current.Windows[0].Close();
            count++;
        }

        // Log out 
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.MainWindow.Hide();
            ActivityLog();
            role = null;
            new Login().Show();
            Application.Current.Windows[0].Close();
            //MessageBox.Show("Number of times" + count);
            count++;
        }

        //--Account Tab--//

        //User Account 
        private void miAccount_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.MainWindow.Hide();
            new Account().Show();
            Application.Current.Windows[0].Close();
            //MessageBox.Show("Number of times" + count);
            count++;
        }

        // Product List 
        private void miProductList_Click(object sender, RoutedEventArgs e)
        {
            new ProductListUser().Show();
            Application.Current.Windows[0].Close();
            count++;
        }

        // Account Order
        private void miAccountOrder_Click(object sender, RoutedEventArgs e)
        {
            new AccountOrder().Show();
            Application.Current.Windows[0].Close();
            count++;
        }

        //Permission
        private void miPermisson_Click(object sender, RoutedEventArgs e)
        {
            new Permission().Show();
            Application.Current.Windows[0].Close();
            count++;
        }

        //--Edit Tab--//

        //Open CSV file (MainWindow)
        private void miCSV_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.MainWindow.Hide();
            new MainWindow().Show();
            Application.Current.Windows[0].Close();
            //MessageBox.Show("Number of times" + count);
            count++;
        }

        //Update products
        private void miUpdateProducts_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.MainWindow.Hide();
            new Update().Show();
            Application.Current.Windows[0].Close();
            //MessageBox.Show("Number of times" + count);
            count++;
        }

        //Update data 
        private void miUpdateData_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.MainWindow.Hide();
            new UpdateData().Show();
            Application.Current.Windows[0].Close();
            //MessageBox.Show("Number of times" + count);
            count++;
        }

        //Export data
        private void ExportData_Click(object sender, RoutedEventArgs e)
        {

        }

        //--Admin Tab--//

        // Invoice management
        private void miHomeAdmin_Click(object sender, RoutedEventArgs e)
        {
            new InvoiceManagement().Show();
            Application.Current.Windows[0].Close();
            count++;
        }

        // Order management
        private void miOrderManagement_Click(object sender, RoutedEventArgs e)
        {
            new OrderManagement().Show();
            Application.Current.Windows[0].Close();
            count++;
        }

        // Account requests management
        private void miRequest_Click(object sender, RoutedEventArgs e)
        {
            new AccountRequests().Show();
            Application.Current.Windows[0].Close();
            count++;
        }

        // Account management
        private void miDatabase_Click(object sender, RoutedEventArgs e)
        {
            new AccountManagement().Show();
            Application.Current.Windows[0].Close();
            count++;
        }

        // Product management
        private void miAdminProduct_Click(object sender, RoutedEventArgs e)
        {
            new UpdateProductAdmin().Show();
            Application.Current.Windows[0].Close();
            count++;
        }

        //Notification bell
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Login.passText != "Guest account")
            { 
                NotifyText.Visibility = Visibility.Visible;
                Notification();
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Login.passText != "Guest account")
            {
                NotifyText.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AccountNotification().Show();
            Application.Current.Windows[0].Close();
            count++;
        }

        //Chat
        private void btnChat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnChat_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Login.passText != "Guest account")
            {
                NotifyText.Visibility = Visibility.Visible;
                Chat();
            }
        }

        private void btnChat_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Login.passText != "Guest account")
            {
                NotifyText.Visibility = Visibility.Collapsed;
            }
        }
    }
}
