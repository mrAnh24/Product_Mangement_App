using ClosedXML.Parser;
using DatabaseApp.Data;
using DatabaseApp.Data.DataModels;
using DatabaseApp.Logic;
using DocumentFormat.OpenXml.Presentation;
using FlexCell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for AccountManagement.xaml
    /// </summary>
    public partial class AccountManagement : Window
    {
        List<Accounts> accounts = new List<Accounts>();
        public static string index;
        public static int count;
        public static int selectedIndex;

        public AccountManagement()
        {
            InitializeComponent();
            GetAccounts();
            count = dgAccount.Items.Count;
            selectedIndex = dgAccount.SelectedIndex;
        }

        //Load datagrid
        void GetAccounts()
        {
            var db = new AccountDb();
            accounts = db.Accounts.ToList();
            dgAccount.ItemsSource= accounts;
            txtTotal.Text = $"Total accounts: {dgAccount.Items.Count}";
        }

        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        
        //Load datagrid (for search only)
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("Select * from Account", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgAccount.ItemsSource = dt.DefaultView;
            txtTotal.Text = $"Total accounts: {dgAccount.Items.Count}";
        }

        //Search button
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            dgAccount.ItemsSource = dt.DefaultView;
            LoadGrid();
            try
            {
                DataView dv = dgAccount.ItemsSource as DataView;
                if (dv != null)
                {
                    dv.RowFilter = tbsearch.Text;
                }
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            txtTotal.Text = $"Total accounts: {dgAccount.Items.Count}";

            System.Windows.MessageBox.Show("Reload before choosing account to avoid error!", "Warning", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
        }

        //Reload current window
        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            dgAccount.ItemsSource = dt.DefaultView;
            tbsearch.Text = "";
            GetAccounts();
        }

        //Update an account
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            GetAccounts();
            AccountUpdateAdmin accountUpdate = new AccountUpdateAdmin(this);
            if (index == null)
            {
                System.Windows.MessageBox.Show("choose an account to update", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            else
            {
                Opacity = 0.2;
                accountUpdate.ShowDialog();
                Opacity = 1;
                //this.Close();
            }
        }

        //Choose a row
        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            //int count = dgAccount.SelectedIndex;
            //var index = dgAccount.GetValue((DependencyProperty)dgAccount.SelectedValue);
            var row = sender as DataGridRow;
            var acc = row.DataContext as Accounts;
            index = acc.Username;
            //System.Windows.MessageBox.Show($"Click on {acc.Username}");

            var updateAccount = new AccountUpdateAdmin(this);
            updateAccount.PreviousAccount += Update_PreviousAccount;
            updateAccount.NextAccount += Update_NextAccount;
            updateAccount.LoadData();
        }

        //Go to next account (incomplete)
        private void Update_NextAccount(AccountUpdateAdmin updateAccount)
        {
            if (dgAccount.SelectedIndex > 0) dgAccount.SelectedIndex -= 1;
            var account = dgAccount.SelectedItem as Account;
            updateAccount.LoadData();
        }

        //Go to previous account (incomplete)
        private void Update_PreviousAccount(AccountUpdateAdmin updateAccount)
        {
            if (dgAccount.SelectedIndex+1 < dgAccount.Items.Count) dgAccount.SelectedIndex += 1;
            var account = dgAccount.SelectedItem as Account;
            updateAccount.LoadData();
        }

        //Open add account window
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AccountAdd accountAdd = new AccountAdd(this);
            Opacity = 0.2;
            accountAdd.ShowDialog();
            Opacity = 1;
            //this.Close();
        }

        //Delete an account
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (index == null)
            {
                System.Windows.MessageBox.Show("Choose an account first", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            else if (index == "admin")
            {
                System.Windows.MessageBox.Show("Can not delete admin account", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                index = null;
                GetAccounts();
            }
            else
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure? This process is permanent", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete Account Where Username = @Username", con);
                    cmd.Parameters.AddWithValue("@Username", index);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    System.Windows.MessageBox.Show("Account deleted successfully");
                    index = null;
                    GetAccounts();
                }
                else
                {
                    index = null;
                    GetAccounts();
                }
            }           
        }
    }
}
