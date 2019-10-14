using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ConsoleApp14
{
    class Program
    {
        static void Main(string[] args)
        {
            
            SqlConnection sqlCon = new SqlConnection();
            sqlCon.ConnectionString =
            @"Data Source=;" +
            "Initial Catalog=;" +
            "User id=;" +
            "Password=;";
            sqlCon.Open();

            SqlCommand sqlCmd = new SqlCommand(
                "Select * from test2", sqlCon);
            SqlDataReader reader = sqlCmd.ExecuteReader();

            string fileName = "test3.csv";
            StreamWriter sw = new StreamWriter(fileName);
            object[] output = new object[reader.FieldCount];

            for (int i = 0; i < reader.FieldCount; i++)
                output[i] = reader.GetName(i);

            sw.WriteLine(string.Join(",", output));

            while (reader.Read())
            {
                reader.GetValues(output);
                sw.WriteLine(string.Join(",", output));
            }
            sw.Close();
            reader.Close();
            sqlCon.Close();
        }
    }
}
