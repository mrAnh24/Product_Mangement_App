using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Winforms = System.Windows.Forms;
using DatabaseApp.View;
using System.Windows.Controls.Primitives;
using System.Data;
using System.Security.Cryptography;
using System.Windows.Forms;
using DatabaseApp.Logic;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using FlexCell;
using DatabaseApp.View.UserControls;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System.Data.SqlClient;

namespace DatabaseApp
{
    public partial class MainWindow : Window
    {
        public int count = 0;
        public MainWindow()
        {
            InitializeComponent();
            cbFilter.IsEnabled = false;
        }
        SqlConnection con = new SqlConnection("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");

        public void ActivityLog()
        {
            con.Open();
            string currentdatetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "INSERT INTO ActivityLog VALUES ('" + Login.GetID + "','" + Login.passText + "','" + Login.GetRole + "','" + "Upload a CSV file" + "', '" + "Data modified" + "', '" + currentdatetime + "')"; ;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //Choose a file
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog filedialog = new OpenFileDialog();
            filedialog.Filter = "CSV file|*.csv";
            filedialog.Multiselect = false;

            if(filedialog.ShowDialog() == true)
            {
                var excelData = Excel.GetExcelData(filedialog.FileName);
                dgExcel.ItemsSource = excelData;
                cbFilter.IsEnabled = true;
                ActivityLog();
                //cbFilter typeItem = (cbFilter)cboType.SelectedItem;
            }
            int number = dgExcel.Items.Count;
            count = number;
            txtTotal.Text = $"Total records: {dgExcel.Items.Count}";
        }

        //Search for a specific values
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataView dv = dgExcel.ItemsSource as DataView;
                if (count != 0)
                {
                    if (dv != null)
                    {
                        if (cbFilter.SelectedIndex == 0)
                        {
                            dv.RowFilter = $"Product LIKE '%{tbSearchBox.Text}%'";
                            if (dgExcel.Items.Count == 0)
                            {
                                System.Windows.MessageBox.Show("No items found", "Error");
                            }
                        }
                        else if (cbFilter.SelectedIndex == 1)
                        {
                            dv.RowFilter = $"ProductCode LIKE '%{tbSearchBox.Text}%'";
                            if (dgExcel.Items.Count == 0)
                            {
                                System.Windows.MessageBox.Show("No items found", "Error");
                            }
                        }
                        else if (cbFilter.SelectedIndex == 2)
                        {
                            dv.RowFilter = $"Price LIKE '%{tbSearchBox.Text}%'";
                            if (dgExcel.Items.Count == 0)
                            {
                                System.Windows.MessageBox.Show("No items found", "Error");
                            }
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Select a filter", "Error");
                        }
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Data grid empty", "Error");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }

        }

        //Clear all data
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        //DataTableCollection tableCollection;
        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DataTable dt = tableCollection[cbFilter.SelectedIndex.ToString()];
            switch (cbFilter.SelectedIndex.ToString())
            {
                case "0":
                    cbFilter.Text = "by name";
                    break;
                case "1":
                    cbFilter.Text = "by code";
                    break;
                case "2":
                    cbFilter.Text = "by price";
                    break;
            }
        }

        private void btnSearchClear_Click(object sender, RoutedEventArgs e)
        {
            tbSearchBox.Clear();
        }
    }
}
