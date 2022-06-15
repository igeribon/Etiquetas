using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace API.DataAccess
{
    public class Connection
    {
        private static readonly Connection _instancia = new Connection();

        public static Connection Instancia
        {
            get
            {
                return Connection._instancia;
            }
        }
        
        public SqlConnection SetConnection()
        {
            try
            {
                SqlConnection _Cnn = new SqlConnection();

                //LOCAL
                //string servidor = "localhost";
                //string database = "Shippings";
                //string user = "sa";
                //string clave = "root";

                //CLOUD
                //string servidor = "A2NWPLSK14SQL-v03.shr.prod.iad2.secureserver.net";
                //string database = "Shippings";
                //string user = "milgenial";
                //string clave = "m1L63nI4l!";

                string servidor = "SQL8003.site4now.net";
                string database = "db_a870f2_shippings";
                string user = "db_a870f2_shippings_admin";
                string clave = "m1L63nI4l!";



                _Cnn.ConnectionString = "Data Source=" + servidor + "; Initial Catalog = " +
                    database + "; User ID =" + user + " ; Password =" + clave;

    
                return _Cnn;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
