using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//@AUTHOR ABHISHEK DWIVEDI

//USER_ACTION_DAL IS A DATA ACCESS LAYER CLASS
//IT MANAGES ALL THE CUSTOMER/VENDOR/ADMIN SPECIFIC QUERIES

namespace EncoreDAL
{
    public class UserActionDAL
    {
        //GET : INSTANCE OF DBAGENT CLASS
        public DBAgent dbContext
        {
            get
            {
                return DBAgent.dbAgentInstance;
            }
        }

        //POST: ADD NEW USER (CUSTOMER/VENDOR) TO THE DATABASE
        public bool RegisterUser(UserLogin user)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsUpdated = 0;
            try
            {
                //AUTO DISPOSABLE
                //STORED PROCEDURE : spAddNewUser
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
                //LOG EXCEPTION
                //***********//
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

        //GET: VALIDATING USER AUTHENTICATION (LOGING)
        public DataSet ValidUser(string email, string password)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataAdapter sda = null;
            DataSet ds = new DataSet();
            try
            {
                //AUTO DISPOSABLE
                //STORED PROCEDURE : spUserLogin
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
                //LOG EXCEPTION
                throw e;
                //return null;
            }
            return ds;
        }

        //POST: ADD USER DETAILS
        public bool AddUserDetails(User user)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsUpdated = 0;
            try
            {
                //AUTO DISPOSABLE
                //STORED PROCEDURE : spAddUserDetails
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
                //LOG EXCEPTION
                //throw e;
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
        
        //POST: UPDATE USER DETAILS
        public bool UpdateUserDetails(User user)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsUpdated = 0;
            try
            {
                //AUTO DISPOSABLE
                //STORED PROCEDURE : spUpdateUserDetails
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
                //LOG EXCEPTION
                //throw e;
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
