using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime.Serialization;

namespace Epicoil.Library.Frameworks
{
    public class Repository
    {
        #region Singleton implementation

        // Create unique instance
        private static readonly Repository _instance = new Repository();

        // Make constructor private to avoid instantiation from the outside
        private Repository()
        {
        }

        // Expose unique instance
        public static Repository Instance
        {
            get { return _instance; }
        }

        #endregion Singleton implementation

        /// <summary>
        /// Execute SQL command
        /// </summary>
        /// <param name="sql">SQL command</param>
        public void Execute(string sql)
        {
            DBConnection dbHelper = new DBConnection();

            if (dbHelper.Connecting() == true)
            {
                dbHelper.ExecuteCommandSQL(sql);
            }

            if (dbHelper.Connecting())
                dbHelper.Disconnect();
        }

        /// <summary>
        /// Excute SQL command with transaction
        /// </summary>
        /// <param name="sql">SQL command</param>
        /// <param name="transName">Transaction name</param>
        public void ExecuteWithTransaction(string sql, string transName)
        {
            DBConnection dbHelper = new DBConnection();

            if (dbHelper.Connecting() == true)
            {
                dbHelper.ExecuteCommandSQLTransaction(sql, transName);
            }

            if (dbHelper.Connecting())
                dbHelper.Disconnect();
        }

        /// <summary>
        /// Get data rows from database
        /// </summary>
        /// <typeparam name="T">Type of expected result</typeparam>
        /// <param name="sql">SQL command</param>
        /// <returns>Expected type list</returns>
        public IEnumerable<T> GetMany<T>(string sql)
        {
            DBConnection dbHelper = new DBConnection();
            DataTable dataTable;

            if (dbHelper.Connecting() == true)
            {
                dataTable = dbHelper.ExecuteSelectCommandSQL(sql);

                if (dataTable.Rows.Count > 0)
                {
                    if (dbHelper.Connecting())
                        dbHelper.Disconnect();

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        yield return this.InvokeBinding<T>(dataTable.Rows[i]);
                    }
                }
            }

            if (dbHelper.Connecting())
                dbHelper.Disconnect();
        }

        /// <summary>
        /// Get data rows from file
        /// </summary>
        /// <typeparam name="T">Type of expected result</typeparam>
        /// <param name="sql">SQL command</param>
        /// <param name="file">File name</param>
        /// <returns></returns>
        public IEnumerable<T> GetManyForExcel<T>(string sql, string file)
        {
            DBConnection dbHelper = new DBConnection();
            DataTable dataTable;

            if (dbHelper.FileConnecting(file) == true)
            {
                dataTable = dbHelper.FileExecuteSelectCommandSQL(sql);

                if (dataTable.Rows.Count > 0)
                {
                    if (dbHelper.FileConnecting(file))
                        dbHelper.FileDisconnect();

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        yield return this.InvokeBinding<T>(dataTable.Rows[i]);
                    }
                }
            }

            if (dbHelper.FileConnecting(file))
                dbHelper.FileDisconnect();
        }

        /// <summary>
        /// Get data column from database
        /// </summary>
        /// <typeparam name="T">Type of expected result</typeparam>
        /// <param name="sql">SQL command</param>
        /// <param name="name">Column name</param>
        /// <returns>Expected type</returns>
        public T GetOne<T>(string sql, string name)
        {
            DBConnection dbHelper = new DBConnection();
            DataTable dataTable;

            T result = default(T);

            if (dbHelper.Connecting() == true)
            {
                dataTable = dbHelper.ExecuteSelectCommandSQL(sql);

                if (dataTable.Rows.Count > 0)
                {
                    if (dbHelper.Connecting())
                        dbHelper.Disconnect();

                    result = (T)this.Binding<T>(dataTable.Rows[0], name);
                }
            }

            if (dbHelper.Connecting())
                dbHelper.Disconnect();

            return result;
        }

        /// <summary>
        /// Get data column from database
        /// </summary>
        /// <typeparam name="T">Type of expected result</typeparam>
        /// <param name="sql">SQL command</param>
        /// <param name="name">Column index</param>
        /// <returns>Expected type</returns>
        public T GetOne<T>(string sql, int index)
        {
            DBConnection dbHelper = new DBConnection();
            DataTable dataTable;

            T result = default(T);

            if (dbHelper.Connecting() == true)
            {
                dataTable = dbHelper.ExecuteSelectCommandSQL(sql);

                if (dataTable.Rows.Count > 0)
                {
                    if (dbHelper.Connecting())
                        dbHelper.Disconnect();

                    result = (T)this.Binding<T>(dataTable.Rows[0]);
                }
            }

            if (dbHelper.Connecting())
                dbHelper.Disconnect();

            return result;
        }

        /// <summary>
        /// Get data column from database
        /// </summary>
        /// <typeparam name="T">Type of expected result</typeparam>
        /// <param name="sql">SQL command</param>
        /// <returns>Expected type</returns>
        public T GetOne<T>(string sql)
        {
            DBConnection dbHelper = new DBConnection();
            DataTable dataTable;

            T result = default(T);

            if (dbHelper.Connecting() == true)
            {
                dataTable = dbHelper.ExecuteSelectCommandSQL(sql);

                if (dataTable.Rows.Count > 0)
                {
                    if (dbHelper.Connecting())
                        dbHelper.Disconnect();

                    result = (T)this.InvokeBinding<T>(dataTable.Rows[0]);
                }
            }

            if (dbHelper.Connecting())
                dbHelper.Disconnect();

            return result;
        }

        private T Mock<T>()
        {
            return (T)FormatterServices.GetUninitializedObject(typeof(T));
        }

        private T InvokeBinding<T>(DataRow row)
        {
            object[] parameters = new object[] { row };
            string methodName = "DataBind";
            var dummy = this.Mock<T>();
            var method = typeof(T).GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (method != null)
            {
                method.Invoke(dummy, parameters);
            }

            return dummy;
        }

        private T Binding<T>(DataRow row, int index = 0)
        {
            var generic = default(T);

            switch (typeof(T).FullName)
            {
                case "System.String":
                    string stringValue = (string)row[index];
                    return (T)(object)stringValue;

                case "System.Int32":
                    int intValue = (int)row[index];
                    return (T)(object)intValue;

                case "System.Decimal":
                    decimal decimalValue = (decimal)row[index];
                    return (T)(object)decimalValue;
            }

            return generic;
        }

        private T Binding<T>(DataRow row, string name)
        {
            var generic = default(T);

            switch (typeof(T).FullName)
            {
                case "System.String":
                    string stringValue = (string)row[name];
                    return (T)(object)stringValue;

                case "System.Int32":
                    int intValue = (int)row[name];
                    return (T)(object)intValue;

                case "System.Int64":
                    Int64 intValue64 = (Int64)row[name];
                    return (T)(object)intValue64;

                case "System.Double":
                    Double doubleValue = (Double)row[name];
                    return (T)(object)doubleValue;

                case "System.Decimal":
                    decimal decimalValue = (decimal)row[name];
                    return (T)(object)decimalValue;
      
            }

            return generic;
        }
    }
}