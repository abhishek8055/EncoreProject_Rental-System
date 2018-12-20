using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreDAL
{
    public class UserActionDAL
    {
        public DBAgent dbContext
        {
            get
            {
                return DBAgent.dbAgentInstance;
            }
        }

        public bool RegisterUser(UserLogin user)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsUpdated = 0;
            try
            {
                using (con = dbContext.Connect())
                using (cmd = new SqlCommand("spAddNewUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
                    con.Open();
                    rowsUpdated = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
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

        public DataSet ValidUser(string email, string password)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataAdapter sda = null;
            DataSet ds = new DataSet();
            try
            {
                using (con = dbContext.Connect())
                using (cmd = new SqlCommand("spUserLogin", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    con.Open();
                    sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    con.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return ds;
        }
    }
}
