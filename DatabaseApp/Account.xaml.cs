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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : Window
    {
        public Account()
        {
            InitializeComponent();
            string accountName = Login.passText;
            SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
            con.Open();

            if (txtAUsername.Text != "")
            {
                txtAUsername.Text = accountName;
                SqlCommand cmd = new SqlCommand("Select * from Account where Username = @Username", con);
                cmd.Parameters.AddWithValue("@Username", txtAUsername.Text);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    txtAEmail.Text = da.GetValue(0).ToString();
                    txtAUsername.Text = da.GetValue(1).ToString();
                    txtARole.Text = da.GetValue(3).ToString();
                    txtAMobile.Text = da.GetValue(4).ToString();
                    txtAGender.Text = da.GetValue(5).ToString();
                }
                if(txtAUsername.Text == "admin")
                {
                    btnDelete.IsEnabled = false;
                    btnDelete.Visibility = Visibility.Collapsed;
                }
                con.Close();
            }
        }

        //Update account
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //new AccountUpdate().Show();
            AccountUpdate accountUpdate = new AccountUpdate(this);
            Opacity = 0.2;
            accountUpdate.ShowDialog();
            Opacity = 1;
        }

        //Delete account
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure? This process is permanent", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
            if (result == MessageBoxResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete Account Where Username = @Username", con);
                cmd.Parameters.AddWithValue("@Username", txtAUsername.Text);
                cmd.ExecuteNonQuery();
                con.Close();

                System.Windows.MessageBox.Show("Account deleted successfully", "Completed", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
                new Login().Show();
                this.Close();
            }
        }

        //Change account password
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            PasswordConfirmation passwordConfirmation = new PasswordConfirmation(this);
            Opacity = 0.2;
            //new PasswordConfirmation().Show();
            passwordConfirmation.ShowDialog();
            Opacity = 1;
        }
    }
}
