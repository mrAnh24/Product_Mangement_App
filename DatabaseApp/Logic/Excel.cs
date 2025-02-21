using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp.Logic
{
    public class Excel
    {
        public static DataView GetExcelData(string path)
        {
            DataTable dt = new DataTable();
            TextFieldParser parser = new TextFieldParser(path);
            parser.SetDelimiters(",");

            if(!parser.EndOfData)
            {
                var columns = parser.ReadFields();

                foreach (var col in columns)
                {
                    dt.Columns.Add(col);
                }
            }

            while (!parser.EndOfData)
            {
                var row = parser.ReadFields();
                dt.Rows.Add(row);
            }
            return dt.DefaultView;
        }
    }
}
