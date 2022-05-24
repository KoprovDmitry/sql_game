using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace sql_neural.Data.SQLConnection
{
    class SQLConnections
    {
        private SqlConnection sqlConnections = null;
        public SQLConnections(string connection)
        {
            this.sqlConnections = new SqlConnection(connection);
        }

        public void openConnection()
        {
            if (this.sqlConnections.State != ConnectionState.Open)
                this.sqlConnections.OpenAsync();
        }

        public void closeConnection()
        {
            if (this.sqlConnections.State != ConnectionState.Closed)
                this.sqlConnections.Close();
        }

        ~SQLConnections()
        {
            if (this.sqlConnections.State != ConnectionState.Closed)
                this.sqlConnections.Close();

            this.sqlConnections.Dispose();
        }
    }
}
