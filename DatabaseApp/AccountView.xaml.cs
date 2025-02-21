using DocumentFormat.OpenXml.Office.Word;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : Window
    {
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        public List<string> listOfProduct = new List<string>();

        public string ViewID;
        public System.DateTime ViewDate;
        public string ActionTaken;
        public string name;

        public AccountView()
        {
            InitializeComponent();
            GetUploaderInfo();
            GetLinkedInfo();
            GetProductNumber();
            GetContribution();

            txtName.Text = name + " information";
            //txtName.Text = ProductDetail.uploader + " information";

            if (Login.GetRole == "admin" || Login.GetRole == "Lv4")
            {
                btnRequest.Visibility = Visibility.Visible;
            }
        }

        void GetUploaderInfo()
        {
            if (ProductDetail.uploader != null)
            {
                name = ProductDetail.uploader;
            }
            else if (HomeAdmin.userName != null)
            {
                name = HomeAdmin.userName;
            }

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from AccountTest where Username = @Username", con);
            cmd.Parameters.AddWithValue("@Username", name);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                ViewID = da.GetValue(1).ToString();
                txtUsername.Text = da.GetValue(2).ToString();
                txtEmail.Text = da.GetValue(3).ToString();
                txtRole.Text = da.GetValue(5).ToString();
                txtMobile.Text = da.GetValue(6).ToString();
                txtGender.Text = da.GetValue(7).ToString();
                ViewDate = Convert.ToDateTime(da.GetValue(8));
            }
            con.Close();

            CultureInfo fr = new CultureInfo("fr-FR");
            string frDate = fr.DateTimeFormat.ShortDatePattern;
            txtDate.Text = ViewDate.ToString(frDate);            
        }

        void GetLinkedInfo()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from AccountLinked where AccountID = @AccountID", con);
            cmd.Parameters.AddWithValue("@AccountID", ViewID);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                txtLink1.Text = da.GetValue(2).ToString();
                txtLink2.Text = da.GetValue(3).ToString();
                txtLink3.Text = da.GetValue(4).ToString();
                txtLink4.Text = da.GetValue(5).ToString();
            }
            con.Close();
        }

        void GetProductNumber()
        {
            listOfProduct.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from ProductLists", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string Uploader = dr.GetValue(7).ToString();
                if (Uploader == name)
                {
                    listOfProduct.Add(Uploader);
                }
            }
            con.Close();
            string number = listOfProduct.Count.ToString();
            txtProduct.Text = $"{number} products added";
        }

        void GetContribution()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT count(AccountID) FROM ActivityLog WHERE Category != 'Notification' AND @AccountID = AccountID", con);
            cmd.Parameters.AddWithValue("@AccountID", ViewID);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                ActionTaken = da.GetValue(0).ToString();
            }
            con.Close();

            txtAction.Text = $"{ActionTaken} action taken";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            
            if(name == ProductDetail.uploader)
            {
                ProductDetail.uploader = null;
                new ProductList().Show();
                this.Close();
            }
            else if (name == HomeAdmin.userName)
            {
                HomeAdmin.userName = null;
                new HomeAdmin().Show();
                this.Close();
            }
        }

        private void btnRequest_Click(object sender, RoutedEventArgs e)
        {
            new AccountRequests().Show();
            this.Close();
        }
    }
}
