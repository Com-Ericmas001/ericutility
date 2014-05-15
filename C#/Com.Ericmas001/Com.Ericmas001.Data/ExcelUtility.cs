using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;

namespace Com.Ericmas001.Data
{
    public static class ExcelUtility
    {
        public static IEnumerable<string> GetSheetNames(string excelFile)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;

            try
            {
                String connString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"", excelFile);
                objConn = new OleDbConnection(connString);
                objConn.Open();
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                    return null;

                List<string> sheets = new List<string>();
                foreach (DataRow row in dt.Rows)
                {
                    string name = row["TABLE_NAME"].ToString();
                    if (name.EndsWith("$"))
                        sheets.Add(name.Remove(name.Length - 1));
                }

                return sheets;
            }
            catch //(Exception ex)
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        public static DataTable GetSheetData(string excelFile, string sheetname)
        {
            try
            {
                String connString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"", excelFile);
                OleDbConnection excelConnection = new OleDbConnection(connString);
                excelConnection.Open();

                string strSQL = String.Format("SELECT * FROM [{0}$]", sheetname);
                OleDbCommand dbCommand = new OleDbCommand(strSQL, excelConnection);
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(dbCommand);
                DataTable dTable = new DataTable();
                dataAdapter.Fill(dTable);

                dTable.Dispose();
                dataAdapter.Dispose();
                dbCommand.Dispose();
                excelConnection.Close();
                excelConnection.Dispose();

                return dTable;
            }
            catch //(Exception ex)
            {
                return null;
            }
        }
    }
}
