using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace KnowledgeBaseCreation
{
    class DatabaseConnection
    {
        public static OracleConnection getDatabaseConnection(string host, string port, string databaseName, string username, string password)
        {
            string connString = "Data Source=" + host + ":" + port + "/" + databaseName + ";"
                                + "User Id=" + username + ";" + "Password=" + password + ";";
            OracleConnection conn = new OracleConnection(connString);
            return conn;
        }

        public static OracleConnection getDatabaseConnection(string connString)
        {
            OracleConnection conn = new OracleConnection(connString);
            return conn;
        }
    }
}
