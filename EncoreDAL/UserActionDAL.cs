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
                }
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
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

        public bool AddUserDetails(User user)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsUpdated = 0;
            try
            {
                using (con = dbContext.Connect())
                using (cmd = new SqlCommand("spAddUserDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UName", user.UName);
                    cmd.Parameters.AddWithValue("@UEmail", user.UEmail);
                    cmd.Parameters.AddWithValue("@UContact", user.UContact);
                    cmd.Parameters.AddWithValue("@UAge", user.UAge);
                    cmd.Parameters.AddWithValue("@UAddress", user.UAddress);
                    cmd.Parameters.AddWithValue("@UPaymentId", user.UPaymentId);
                    cmd.Parameters.AddWithValue("@UValid", user.UValid);
                    cmd.Parameters.AddWithValue("@UPhoto", user.UPhoto);
                    cmd.Parameters.AddWithValue("@URoleId", user.URoleId);
                    con.Open();
                    rowsUpdated = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
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

        public bool UpdateUserDetails(User user)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsUpdated = 0;
            try
            {
                using (con = dbContext.Connect())
                using (cmd = new SqlCommand("spUpdateUserDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UId", user.UId);
                    cmd.Parameters.AddWithValue("@UName", user.UName);
                    cmd.Parameters.AddWithValue("@UEmail", user.UEmail);
                    cmd.Parameters.AddWithValue("@UContact", user.UContact);
                    cmd.Parameters.AddWithValue("@UAge", user.UAge);
                    cmd.Parameters.AddWithValue("@UAddress", user.UAddress);
                    cmd.Parameters.AddWithValue("@UPaymentId", user.UPaymentId);
                    cmd.Parameters.AddWithValue("@UPhoto", user.UPhoto);
                    con.Open();
                    rowsUpdated = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
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
