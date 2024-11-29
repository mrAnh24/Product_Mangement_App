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

        //Load info
        public void LoadData()
        {
            tbUsername.Text = item;
            if (item == "admin")
            {
                tbRole.IsEnabled = false;
            }
            con.Open();
            //tbUsername.Text = AccountManagement.dgAccount.GetValue(DataGridRow[item]);
            SqlCommand cmd = new SqlCommand("Select * from Account where Username = @Username", con);
            cmd.Parameters.AddWithValue("@Username", tbUsername.Text);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                tbEmail.Text = da.GetValue(0).ToString();
                tbUsername.Text = da.GetValue(1).ToString();
                tbRole.Text = da.GetValue(3).ToString();
                tbPhoneNumber.Text = da.GetInt32(4).ToString();
                tbGender.Text = da.GetValue(5).ToString();
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
            tbGender.Clear();
            if (item != "admin")
            {
                tbRole.Clear();
            }
        }

        //Save updated info
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Update Account Set Role = @Role, PhoneNumbers = @PhoneNumbers, Gender = @Gender Where Username = @Username", con);
            if (tbRole.Text == "" || tbPhoneNumber.Text == "" || tbGender.Text == "" )
            {
                System.Windows.MessageBox.Show("All fields need to be fill!", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Username", tbUsername.Text);
                cmd.Parameters.AddWithValue("@Role", tbRole.Text);
                cmd.Parameters.AddWithValue("@PhoneNumbers", tbPhoneNumber.Text);
                cmd.Parameters.AddWithValue("@Gender", tbGender.Text);
                cmd.ExecuteNonQuery();
                con.Close();
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
    }
}
