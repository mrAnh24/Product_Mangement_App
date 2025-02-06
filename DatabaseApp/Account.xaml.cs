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
using Application = Microsoft.Office.Interop.Excel.Application;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Globalization;
using System.Security.RightsManagement;

namespace DatabaseApp
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : System.Windows.Window
    {
        List<Activity> activities = new List<Activity>();
        public static string index;
        public static string type;
        public static string time;
        public static string query;
        public static string tableName;      

        public Account()
        {
            InitializeComponent();
            //txtLink1.Text = txtLink2.Text = txtLink3.Text = txtLink4.Text = "PlaceHolderLink";

            if (txtUsername.Text != "")
            {
                txtUsername.Text = Login.passText;
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from AccountTest where Username = @Username", con);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    txtUsername.Text = da.GetValue(2).ToString();
                    tbEmail.Text =" " + da.GetValue(3).ToString();
                    tbRole.Text =" " + da.GetValue(5).ToString();
                    tbMobile.Text =" " + da.GetValue(6).ToString();
                    tbGender.Text =" " + da.GetValue(7).ToString();
                }
                if(txtUsername.Text == "admin")
                {
                    btnDelete.IsEnabled = false;
                    btnDelete.Visibility = Visibility.Collapsed;
                }
                con.Close();
            }
            GetActivity();
            LoadLinkedAccount();
            //ActivityLog();
        }
        DataTableCollection tableCollection;
        string connectionString = "Data Source=OS-GPCP-GPDN171\\MSSQLSERVER01;Initial catalog=dbdemo;Persist Security info=True;Encrypt=false;;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true";
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        void GetActivity()
        {
            var db = new ActivityDb();
            activities = db.activities.ToList();
            dgActivity.ItemsSource = activities;
        }

        public void ActivityLog()
        {
            con.Open();
            string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            query = "INSERT INTO ActivityLog VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + "Account deleted" + "', '" + "Notification" + "', '" + currentdatetime + "')"; ;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void ShowResult()
        {
            dgActivity.Columns[0].Visibility = Visibility.Hidden;
            dgActivity.Columns[1].Visibility = Visibility.Hidden;
            dgActivity.Columns[2].Visibility = Visibility.Hidden;
            dgActivity.Columns[3].Visibility = Visibility.Hidden;
            dgActivity.Columns[4].Visibility = Visibility.Hidden;
            dgActivity.Columns[6].Visibility = Visibility.Hidden;
            activities.RemoveAll(x => x.AccountID != Login.GetID);
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            var row = sender as DataGridRow;
            var select = row.DataContext as Activity;
            index = select.Action;
            type = select.Category;
            time = select.TimeStamp.ToString();
        }

        public void LoadLinkedAccount()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from AccountLinked where Username = @Username", con);
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
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

        void Refresh()
        {
            new Account().Show();
            this.Close();
        }

        public void DeleteAccount()
        {
            query = ($"DELETE FROM {tableName} WHERE Username = @Username");
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", Login.passText);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        //Update account
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //new AccountUpdate().Show();
            AccountUpdate accountUpdate = new AccountUpdate(this);
            Opacity = 0.2;
            accountUpdate.ShowDialog();
            Opacity = 1;
            Refresh();
        }

        //Delete account
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure? This process is permanent", "Warning", (MessageBoxButton)MessageBoxButtons.YesNo, (MessageBoxImage)MessageBoxIcon.Warning);
            if (result == MessageBoxResult.Yes)
            {
                tableName = "AccountLinked";
                DeleteAccount();

                tableName = "AccountTest";
                DeleteAccount();

                tableName = "CustomerList";
                DeleteAccount();

                tableName = "CustomerPreOrder";
                DeleteAccount();

                ActivityLog();

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
            Refresh();
        }

        //Redirect to personal list
        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            new ProductListUser().Show();
            this.Close();
        }

        //Redirect to list history
        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            new AccountOrder().Show();
            this.Close();
        }

        private void btnLinked_Click(object sender, RoutedEventArgs e)
        {
            AccountLinkedUpdate accountLinkedUpdate = new AccountLinkedUpdate(this);
            Opacity = 0.2;
            accountLinkedUpdate.ShowDialog();
            Opacity = 1;
            Refresh();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            new Home().Show();
            this.Close();
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            ShowResult();
            if (index == null)
            {
                System.Windows.MessageBox.Show("Choose an activity first", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            else
            {
                AccountActivity accountActivity = new AccountActivity(this);
                Opacity = 0.2;
                accountActivity.ShowDialog();
                Opacity = 1;
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            string currentdatetime = DateTime.Now.ToString("ddMMyyyyHHmmss");
            string LogFolder = @"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs";
            string queryString = $"SELECT * FROM ActivityLog WHERE AccountID LIKE '%{Login.GetID}%'";
            string filePath = $@"D:\TDA_intern\Projects\DatabaseApp\Product_Mangement_App-master\Logs\Activities\{Login.passText} ActivitiesLog.XLSX";

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
                            for (int i = 3; i < reader.FieldCount; i++)
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
                                for (int i = 3; i < reader.FieldCount; i++)
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

        private void dgActivity_AutoGeneratedColumns(object sender, EventArgs e)
        {
            ShowResult();
        }
    }
}
