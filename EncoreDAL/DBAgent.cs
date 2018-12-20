using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreDAL
{
    public class DBAgent
    {
        private static DBAgent dbAgent;
        public static DBAgent dbAgentInstance
        {
            get
            {
                if (dbAgent == null)
                {
                    dbAgent = new DBAgent();
                }
                return dbAgent;
            }
        }
        public SqlConnection Connect()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);            
            try
            {
                con.Open();
            }
            catch (Exception sqle)
            {
                throw sqle;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return con;
        }

        public DataSet GetData(string query)
        {
            DataSet ds = new DataSet();
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataAdapter sda = null;
            try
            {
                con = Connect();
                cmd = new SqlCommand(query, con);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception sqle)
            {
                throw sqle;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                if (sda != null)
                {
                    sda = null;
                }
                if (cmd != null)
                {
                    cmd = null;
                }
            }
            return ds;
        }
    }
}
