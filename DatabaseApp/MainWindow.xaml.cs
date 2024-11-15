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

namespace DatabaseApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
                //cbFilter typeItem = (cbFilter)cboType.SelectedItem;
            }
            txtTotal.Text = $"Total records: {dgExcel.Items.Count}";
        }

        //Search for a specific values
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataView dv = dgExcel.ItemsSource as DataView;
                if (dv != null)
                    dv.RowFilter = tbSearchBox.Text;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Message", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }

        //Clear all data
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            dgExcel.ItemsSource = dt.DefaultView;
            txtTotal.Text = $"Total records: {dgExcel.Items.Count}";
        }

        //DataTableCollection tableCollection;
        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DataTable dt = tableCollection[cbFilter.SelectedIndex.ToString()];
            
        }
    }
}
