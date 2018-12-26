using EncoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//@AUTHOR ABHISHEK DWIVEDI

//DBAGENT CLASS IS A DATA ACCESS LAYER CLASS
//MAINTAINS THE DATABASE CONNECTIVITY
//HANDLES SOME COMMON DATABASE QUERIES

namespace EncoreDAL
{
    public class DBAgent
    {
        //SINGLETON DESIGN PATTERN
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

        //DATABASE CONNECTIVITY METHOD
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
                //LOG EXCEPTION
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

        //GET: METHOD
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
                //LOG EXCEPTION
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

        //METHOD TO INSERT FEEDBACK FROM USERS
        public bool Feedback(Feedback feedback)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsUpdated = 0;
            try
            {
                //AUTO DISPOSABLE
                //STORED PROCEDURE spFeedback
                using (con = Connect())
                using (cmd = new SqlCommand("spFeedback", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", feedback.Name);
                    cmd.Parameters.AddWithValue("@Email", feedback.Email);
                    cmd.Parameters.AddWithValue("@Subject", feedback.Subject);
                    cmd.Parameters.AddWithValue("@Description", feedback.Description);
                    con.Open();
                    rowsUpdated = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                //LOG EXCEPTION
                throw e;
                //return false;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            if (rowsUpdated == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
