﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta.Dal
{
    public static class DALConnection
    {
        public static SqlConnection GetConnectionByName(string naam)
        {
            //haalt connection string op uit het configuratie bestand en maakt er een sqlConnection van
            string conString = ConfigurationManager.ConnectionStrings[naam].ConnectionString;
            return new SqlConnection(conString);
        }
    }
}
