using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;

namespace Epicoil.Library.Frameworks
{
    public class DBConnection
    {
        private SqlConnection Connecter;
        private string strConnection;

        private OleDbConnection FileConnecter;
        private string strFileConnection;

        public DBConnection()
        {
            strConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        #region Connect to sql

        public Boolean Connecting()
        {
            Connecter = new SqlConnection(strConnection);

            try
            {
                Connecter.Open();
                return true;
            }
            catch (Exception er)
            {
                LogManager.WriteEntry(er);
                return false;
            }
        }

        public void Disconnect()
        {
            Connecter.Close();
        }

        public Boolean ExecuteCommandSQL(string sql)
        {
            SqlCommand userCommand = new SqlCommand(sql, Connecter);

            try
            {
                userCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception er)
            {
                LogManager.WriteEntry(er);
                return false;
            }
        }

        public Boolean ExecuteCommandSQLTransaction(string sql, string transactionName = "UCC Transaction")
        {
            SqlTransaction Transaction;
            SqlCommand userCommand;
            if (Connecter.State != ConnectionState.Open)
            {
                return false;
            }

            Transaction = Connecter.BeginTransaction(transactionName);
            userCommand = new SqlCommand(sql, Connecter, Transaction);

            try
            {
                userCommand.ExecuteNonQuery();
                Transaction.Commit();
                return true;
            }
            catch (Exception er)
            {
                Transaction.Rollback();
                LogManager.WriteEntry(er);
                return false;
            }
        }

        public DataTable ExecuteSelectCommandSQL(string sql)
        {
            DataTable TableResult = new DataTable();

            SqlDataAdapter UserDataAdaptor = new SqlDataAdapter(sql, Connecter);

            try
            {
                UserDataAdaptor.Fill(TableResult);
                return TableResult;
            }
            catch (Exception er)
            {
                LogManager.WriteEntry(er);
                return TableResult;
            }
        }

        #endregion Connect to sql

        #region Connect to File
        private string GetConnectionString(string FileName)
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            // XLSX - Excel 2007, 2010, 2012, 2013
            props["Provider"] = "Microsoft.ACE.OLEDB.12.0";
            props["Extended Properties"] = "Excel 12.0 XML";
            props["Data Source"] = FileName;

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }

            return sb.ToString();
        }
        public Boolean FileConnecting(string FileName)
        {
            strFileConnection = GetConnectionString(FileName);
            FileConnecter = new OleDbConnection(strFileConnection);

            try
            {
                FileConnecter.Open();
                return true;
            }
            catch (Exception er)
            {
                LogManager.WriteEntry(er);
                return false;
            }
        }

        public void FileDisconnect()
        {
            FileConnecter.Close();
        }

        public DataTable FileExecuteSelectCommandSQL(string sql)
        {
            DataTable TableResult = new DataTable();

            OleDbDataAdapter UserDataAdaptor = new OleDbDataAdapter(sql, FileConnecter);

            try
            {
                UserDataAdaptor.Fill(TableResult);
                return TableResult;
            }
            catch (Exception er)
            {
                LogManager.WriteEntry(er);
                return TableResult;
            }
        }

        public string[] FileGetTableList()
        {
            DataTable TableResult = new DataTable();
            String[] excelSheetNames;

            if(FileConnecter.State == ConnectionState.Open)
            {
                FileConnecter.Close();
            }

            try
            {
                FileConnecter.Open();
                TableResult = FileConnecter.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                excelSheetNames = new String[TableResult.Rows.Count];

                int i = 0;
                foreach (DataRow row in TableResult.Rows)
                {
                    excelSheetNames[i] = row["TABLE_NAME"].ToString().Replace("$","").Replace("'","");
                    i++;
                }
                return excelSheetNames;
            }
            catch (Exception er)
            {
                LogManager.WriteEntry(er);
                return null;
            }
        }

        #endregion Connect to File
    }
}