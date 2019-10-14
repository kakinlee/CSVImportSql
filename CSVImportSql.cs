using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient; 
namespace CSVImportSql
{
    class Program
    {
        public string Connstring
        {
            get
            {
                StreamReader file = new StreamReader("config.txt");
                string Connstring = file.ReadLine();
                file.Close();
                return Connstring;
            }
            set
            {
                Connstring = value;
            }
        }
        static void Main(string[] args)
        {
            var m = new Program();

        }
        public static string filename;
        public static string Filename
        {
            get
            {
                return filename;     
            }
            set
            {
                filename = value;
            }
        }
        private string[] CsvFiles = Directory.GetFiles(@".", "*.csv");
        public void DeleteData()
        {
            string cnnstring = Connstring;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = cnnstring;
            SqlCommand command;
            connection.Open();
            foreach(var file in CsvFiles)
            {
                string sFileName = file;
                string tblname = Path.GetFileNameWithoutExtension(sFileName);
                command = new SqlCommand(" DELETE FROM " + tblname, connection);
                command.ExecuteNonQuery();
                CsvRead(sFileName);
            }
            connection.Close();
        }
        public void CsvRead(string filename)
        {
            string tblname = Path.GetFileNameWithoutExtension(filename);
            var lines = File.ReadAllLines(filename);

            if (lines.Count() == 0) return;
            var columns =lines[0].Split(',');
            var table = new DataTable();
            foreach (var c in columns)
                table.Columns.Add(c);
            for (int i = 1; i < lines.Count(); i++)
                table.Rows.Add(lines[i].Split(','));
            var sqlBulk = new SqlBulkCopy(Connstring);
            sqlBulk.DestinationTableName = tblname;
            sqlBulk.WriteToServer(table);

        }
        


        
    }
}
