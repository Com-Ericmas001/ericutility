using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Com.Ericmas001.Data
{
    public class SqlServerConnector
    {
        private string m_Database;

        public string Database
        {
            get { return m_Database; }
            set { m_Database = value; }
        }

        private string m_Server;

        public string Server
        {
            get { return m_Server; }
            set { m_Server = value; }
        }

        private string m_Username;

        public string Username
        {
            get { return m_Username; }
            set { m_Username = value; }
        }

        private string m_Password;

        public string Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }

        public SqlServerConnector(string server, string db, string user, string password)
        {
            m_Database = db;
            m_Server = server;
            m_Username = user;
            m_Password = password;
        }

        public SqlConnection GetConnection()
        {
            SqlConnection myConnection = new SqlConnection("user id=" + m_Username + ";password=" + m_Password + ";server=" + m_Server + ";Trusted_Connection=no;MultipleActiveResultSets=True;database=" + m_Database + "; connection timeout=30");
            myConnection.Open();
            return myConnection;
        }

        public Dictionary<string, object> SelectOneRow(string query, Dictionary<string, object> pars)
        {
            SqlConnection myConnection = null;
            try
            {
                myConnection = GetConnection();
            }
            catch (Exception e)
            {
                throw new Exception("SqlServerConnector.SelectOneRow: Connection Error: " + e.ToString());
            }
            Dictionary<string, object> results = SelectOneRow(myConnection, query, pars);
            if (myConnection != null)
                myConnection.Close();
            return results;
        }

        public Dictionary<string, object> SelectOneRow(SqlConnection connection, string query, Dictionary<string, object> pars)
        {
            Dictionary<string, object> results = new Dictionary<string, object>();

            SqlConnection myConnection = connection;

            if (myConnection != null)
            {
                try
                {
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(query, myConnection);
                    foreach (string s in pars.Keys)
                        myCommand.Parameters.Add(new SqlParameter(s, pars[s]));
                    myReader = myCommand.ExecuteReader();
                    if (myReader.Read())
                    {
                        for (int i = 0; i < myReader.FieldCount; ++i)
                            results.Add(myReader.GetName(i), myReader[i]);
                    }

                    myReader.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("SqlServerConnector.SelectOneRow: Read Error: " + e.ToString());
                }
            }

            return results;
        }

        public List<Dictionary<string, object>> SelectRows(string query, Dictionary<string, object> pars)
        {
            SqlConnection myConnection = null;
            try
            {
                myConnection = GetConnection();
            }
            catch (Exception e)
            {
                throw new Exception("SqlServerConnector.SelectRows: Connection Error: " + e.ToString());
            }
            List<Dictionary<string, object>> results = SelectRows(myConnection, query, pars);
            if (myConnection != null)
                myConnection.Close();
            return results;
        }

        public List<Dictionary<string, object>> SelectRows(SqlConnection connection, string query, Dictionary<string, object> pars)
        {
            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

            SqlConnection myConnection = connection;

            if (myConnection != null)
            {
                try
                {
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(query, myConnection);
                    foreach (string s in pars.Keys)
                        myCommand.Parameters.Add(new SqlParameter(s, pars[s]));
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        Dictionary<string, object> result = new Dictionary<string, object>();
                        for (int i = 0; i < myReader.FieldCount; ++i)
                            result.Add(myReader.GetName(i), myReader[i]);
                        results.Add(result);
                    }

                    myReader.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("SqlServerConnector.SelectRows: Read Error: " + e.ToString());
                }
            }

            return results;
        }

        public void Execute(string nonQuery, Dictionary<string, object> pars)
        {
            SqlConnection myConnection = null;
            try
            {
                myConnection = GetConnection();
            }
            catch (Exception e)
            {
                throw new Exception("SqlServerConnector.Execute: Connection Error: " + e.ToString());
            }
            Execute(myConnection, nonQuery, pars);
            if (myConnection != null)
                myConnection.Close();
        }

        public void Execute(SqlConnection connection, string nonQuery, Dictionary<string, object> pars)
        {
            SqlConnection myConnection = connection;

            if (myConnection != null)
            {
                try
                {
                    SqlCommand myCommand = new SqlCommand(nonQuery, myConnection);
                    foreach (string s in pars.Keys)
                        myCommand.Parameters.Add(new SqlParameter(s, pars[s]));
                    myCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception("SqlServerConnector.Execute: ExecuteNonQuery Error: " + e.ToString());
                }
            }
        }

        public SPResult ExecuteSP(string spName)
        {
            return ExecuteSP(spName, new List<SPParam>());
        }

        public SPResult ExecuteSP(string spName, List<SPParam> pars)
        {
            SqlConnection myConnection = null;
            try
            {
                myConnection = GetConnection();
            }
            catch (Exception e)
            {
                throw new Exception("SqlServerConnector.ExecuteSP: Connection Error: " + e.ToString());
            }
            SPResult res = ExecuteSP(myConnection, spName, pars);
            if (myConnection != null)
                myConnection.Close();
            return res;
        }

        public SPResult ExecuteSP(SqlConnection connection, string spName)
        {
            return ExecuteSP(connection, spName, new List<SPParam>());
        }

        public SPResult ExecuteSP(SqlConnection connection, string spName, List<SPParam> pars)
        {
            SqlConnection myConnection = connection;
            SPResult res = new SPResult();
            if (myConnection != null)
            {
                try
                {
                    SqlCommand myCommand = new SqlCommand(spName, myConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    foreach (SPParam s in pars)
                        myCommand.Parameters.Add(s.Parm);
                    myCommand.ExecuteNonQuery();
                    foreach (SPParam s in pars)
                        res.Parameters.Add(s.Parm.ParameterName, s.Parm.Value);
                }
                catch (Exception e)
                {
                    throw new Exception("SqlServerConnector.ExecuteSP: ExecuteNonQuery Error: " + e.ToString());
                }
            }
            return res;
        }

        public SPResult SelectRowsSP(string spName)
        {
            return SelectRowsSP(spName, new List<SPParam>());
        }

        public SPResult SelectRowsSP(string spName, List<SPParam> pars)
        {
            SqlConnection myConnection = null;
            try
            {
                myConnection = GetConnection();
            }
            catch (Exception e)
            {
                throw new Exception("SqlServerConnector.SelectRowsSP: Connection Error: " + e.ToString());
            }
            SPResult res = SelectRowsSP(myConnection, spName, pars);
            if (myConnection != null)
                myConnection.Close();
            return res;
        }

        public SPResult SelectRowsSP(SqlConnection connection, string spName)
        {
            return SelectRowsSP(connection, spName, new List<SPParam>());
        }

        public SPResult SelectRowsSP(SqlConnection connection, string spName, List<SPParam> pars)
        {
            SPResult res = new SPResult();

            SqlConnection myConnection = connection;

            if (myConnection != null)
            {
                try
                {
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(spName, myConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    foreach (SPParam s in pars)
                        myCommand.Parameters.Add(s.Parm);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        Dictionary<string, object> result = new Dictionary<string, object>();
                        for (int i = 0; i < myReader.FieldCount; ++i)
                            result.Add(myReader.GetName(i), myReader[i]);
                        res.QueryResults.Add(result);
                    }
                    myReader.Close();
                    foreach (SPParam s in pars)
                        res.Parameters.Add(s.Parm.ParameterName, s.Parm.Value);
                }
                catch (Exception e)
                {
                    throw new Exception("SqlServerConnector.SelectRowsSP: Read Error: " + e.ToString());
                }
            }

            return res;
        }
    }
}