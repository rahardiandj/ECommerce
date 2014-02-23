using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace ecommerce.datamodel
{
    public class ConnectionManager : IConnectionManager
    {
        private SqlCeConnection _sqlConnection;
        private string _connectionString = "E:\\E_Commerce\\DataModel\\ecommerce.datamodel\\db_commerce.sdf";

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        public SqlCeConnection SQLConnection
        {
            get { return _sqlConnection ?? CreateConnectionString(); }
        }


        private SqlCeConnection CreateConnectionString()
        {
            _sqlConnection = new SqlCeConnection("Data Source =" + ConnectionString);

            return _sqlConnection;
        }
    }
}
