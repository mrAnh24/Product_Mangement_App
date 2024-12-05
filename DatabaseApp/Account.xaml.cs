using DatabaseApp.Data;
using DatabaseApp.Data.DataModels;
using DatabaseApp.Logic;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using Z.Dapper.Plus;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : System.Windows.Window
    {
        public Account()
        {
            InitializeComponent();
            string accountName = Login.passText;
            con.Open();
            //txtLink1.Text = txtLink2.Text = txtLink3.Text = txtLink4.Text = "PlaceHolderLink";

            if (txtUsername.Text != "")
            {
                txtUsername.Text = accountName;
                SqlCommand cmd = new SqlCommand("Select * from Account where Username = @Username", con);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    tbEmail.Text = da.GetValue(0).ToString();
                    txtUsername.Text = da.GetValue(1).ToString();
                    tbRole.Text = da.GetValue(3).ToString();
                    tbMobile.Text = da.GetValue(4).ToString();
                    tbGender.Text = da.GetValue(5).ToString();
                }
                if(txtUsername.Text == "admin")
                {
                    btnDelete.IsEnabled = false;
                    btnDelete.Visibility = Visibility.Collapsed;
                }
                con.Close();
            }
            LoadLinkedAccount();
            ActivityLog();
        }
        DataTableCollection tableCollection;
        string connectionString = "Data Source=OS-GPCP-GPDN171\\MSSQLSERVER01;Initial catalog=dbdemo;Persist Security info=True;Encrypt=false;;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true";
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        public void ActivityLog()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from ActivityLog", con);
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgActivity.ItemsSource = dt.DefaultView;

            DataView dv = dgActivity.ItemsSource as DataView;
            if (dv != null)
            {
                dv.RowFilter = $"Username LIKE '%{Login.passText}%'";
            }
        }

        public void LoadLinkedAccount()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from AccountLinked where Username = @Username", con);
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                txtLink1.Text = da.GetValue(1).ToString();
                txtLink2.Text = da.GetValue(2).ToString();
                txtLink3.Text = da.GetValue(3).ToString();
                txtLink4.Text = da.GetValue(4).ToString();
            }
            con.Close();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            new Account().Show();
            this.Close();
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
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure? This process is permanent", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
            if (result == MessageBoxResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete Account Where Username = @Username", con);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
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

        private void btnLinked_Click(object sender, RoutedEventArgs e)
        {
            AccountLinkedUpdate accountLinkedUpdate = new AccountLinkedUpdate(this);
            Opacity = 0.2;
            accountLinkedUpdate.ShowDialog();
            Opacity = 1;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            new Home().Show();
            this.Close();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            new Account().Show();
            this.Close();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            string currentdatetime = DateTime.Now.ToString("ddMMyyyyHHmmss");
            string LogFolder = @"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs";
            string queryString = $"SELECT * FROM ActivityLog WHERE Username LIKE '%{Login.passText}%'";
            string filePath = $@"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs\{Login.passText}ActivitiesLog.XLSX";

            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //Create new Excel application and workbook
                            Application excelApp = new Application();
                            Workbook excelWorkbook = excelApp.Workbooks.Add();
                            Worksheet excelWorksheet = excelWorkbook.Worksheets[1];

                            //Add the headers to first row
                            int col = 1;
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                excelWorksheet.Cells[1, col].Value2 = reader.GetName(i);
                                excelWorksheet.Cells[1, col].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                                excelWorksheet.Cells[1, col].Borders.LineStyle = 1;
                                excelWorksheet.Cells[1, col].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                col++;
                            }

                            //Iterate through data start from second row and insert into worksheet
                            int row = 2;
                            while (reader.Read())
                            {
                                col = 1;
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    excelWorksheet.Cells[row, col].EntireColumn.NumberFormat = "@";
                                    excelWorksheet.Cells[row, col].Value2 = reader[i];
                                    excelWorksheet.Cells[row, col].EntireColumn.AutoFit();
                                    excelWorksheet.Cells[row, col].HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                    excelWorksheet.Cells[row, col].Borders.LineStyle = 1;
                                    excelWorksheet.Columns["E"].NumberFormat = "yyyy-MM-dd HH:mm:ss";
                                    col++;
                                }
                                row++;
                            }

                            //UpdateFormat(filePath);
                            //Save workbook and close Excel application
                            excelWorkbook.SaveAs(filePath);
                            System.Windows.MessageBox.Show("File created successfully");
                            excelWorkbook.Close();
                            excelApp.Quit();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                using (StreamWriter sw = File.CreateText(LogFolder + "\\" + "ErrorLog" + currentdatetime + ".log"))
                {
                    sw.WriteLine(exception.ToString());
                }
            }
        }      
    }
}
