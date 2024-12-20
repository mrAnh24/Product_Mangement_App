using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
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
    /// Interaction logic for AccountUpdateAdmin.xaml
    /// </summary>
    public partial class AccountUpdateAdmin : Window
    {
        public string item = AccountManagement.index;
        public int dgIt = AccountManagement.count;
        public int selected = AccountManagement.selectedIndex;

        public event Action<AccountUpdateAdmin> NextAccount;
        public event Action<AccountUpdateAdmin> PreviousAccount;

        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        public AccountUpdateAdmin(Window parentWindow)
        {
            Owner = parentWindow;
            InitializeComponent();
            LoadData();

        }

        public void ActivityLog()
        {
            con.Open();
            string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "INSERT INTO ActivityLog VALUES ('" + AccountManagement.indexID + "','" + AccountManagement.index + "','" + AccountManagement.indexRole + "','" + "Account information modified" + "', '" + "Admin action" + "', '" + currentdatetime + "')"; ;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //Load info
        public void LoadData()
        {
            tbUsername.Text = item;
            if (item == "admin")
            {
                cbRole.IsEnabled = false;
                cbRole.Text = "admin";
            }
            con.Open();
            //tbUsername.Text = AccountManagement.dgAccount.GetValue(DataGridRow[item]);
            SqlCommand cmd = new SqlCommand("Select * from AccountTest where Username = @Username", con);
            cmd.Parameters.AddWithValue("@Username", tbUsername.Text);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                tbUsername.Text = da.GetValue(2).ToString();
                tbEmail.Text = da.GetValue(3).ToString();
                cbRole.Text = da.GetValue(5).ToString();
                tbPhoneNumber.Text = da.GetValue(6).ToString();
                cbGender.Text = da.GetValue(7).ToString();
            }
            con.Close();
        }

        //Exit current window
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Clear textbox info
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbPhoneNumber.Clear();
            cbGender.Text = "";
            if (item != "admin")
            {
                cbRole.Text = "";
            }
        }

        //Save updated info
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Update AccountTest Set Role = @Role, PhoneNumbers = @PhoneNumbers, Gender = @Gender Where Username = @Username", con);
            if (cbRole.Text == "" || tbPhoneNumber.Text == "" || cbGender.Text == "" )
            {
                System.Windows.MessageBox.Show("All fields need to be fill!", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Username", tbUsername.Text);
                cmd.Parameters.AddWithValue("@Role", cbRole.Text);
                cmd.Parameters.AddWithValue("@PhoneNumbers", tbPhoneNumber.Text);
                cmd.Parameters.AddWithValue("@Gender", cbGender.Text);
                cmd.ExecuteNonQuery();
                con.Close();

                ActivityLog();
                System.Windows.MessageBox.Show("Reload for the change to take action!", "User account updated", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
                this.Close();
            }
        }

        //Action after current window is close
        private void Window_Closed(object sender, EventArgs e)
        {
            AccountManagement.index = null;
        }

        //Go to next account (incomplete)
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            /*if(selected > 0)
            {
                 selected--;
            }*/
            PreviousAccount.Invoke(this);
        }

        //Go to previous account (incomplete)
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            /*if (dgIt - 1 > selected)
            {
                selected++;
            }*/
            NextAccount.Invoke(this);
        }

        private void tbPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fulltext = textBox.Text.Insert(tbPhoneNumber.SelectionStart, e.Text);

            double val;
            e.Handled = !double.TryParse(fulltext, out val);
        }
    }
}
