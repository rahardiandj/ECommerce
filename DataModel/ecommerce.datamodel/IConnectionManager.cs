using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace ecommerce.datamodel
{
    public interface IConnectionManager
    {
        string ConnectionString { get; set; }

        SqlCeConnection SQLConnection { get; }

    }
}
