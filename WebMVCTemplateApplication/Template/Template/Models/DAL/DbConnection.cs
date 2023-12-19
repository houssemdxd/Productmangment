using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Template.Models.DAL
{
    public class DbConnection
    {
        static string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ArticleDB;Data Source=DESKTOP-KSGLTG1";
        public static SqlConnection GetConnection()
        {
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return cn;
        }
    }
}
