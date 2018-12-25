using EncoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreDAL
{
    public class ProductActionsDAL 
    {
        public DBAgent dbContext
        {
            get
            {
                return DBAgent.dbAgentInstance;
            }
        }

        public bool AddProduct(Product product)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsUpdated = 0;

            try
            {
                using (con = dbContext.Connect())
                using (cmd = new SqlCommand("spAddNewProduct", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VendorId", product.VendorId);
                    cmd.Parameters.AddWithValue("@Name", product.PName);
                    cmd.Parameters.AddWithValue("@Description", product.PDescription);
                    cmd.Parameters.AddWithValue("@Image1", product.PImage1);
                    cmd.Parameters.AddWithValue("@Image2", product.PImage2);
                    cmd.Parameters.AddWithValue("@Image3", product.PImage3);
                    cmd.Parameters.AddWithValue("@Availability", product.PAvailability);
                    cmd.Parameters.AddWithValue("@StartDate", product.PStartDate);
                    cmd.Parameters.AddWithValue("@EndDate", product.PEndDate);
                    cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    cmd.Parameters.AddWithValue("@UnitPrice", product.PUnitCost);
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
                {
                    con.Close();
                }
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

        public bool UpdateProduct(Product product)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsUpdated = 0;

            try
            {
                using (con = dbContext.Connect())
                using (cmd = new SqlCommand("spUpdateProduct", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", product.PId);
                    cmd.Parameters.AddWithValue("@Name", product.PName);
                    cmd.Parameters.AddWithValue("@Description", product.PDescription);
                    cmd.Parameters.AddWithValue("@Image1", product.PImage1);
                    cmd.Parameters.AddWithValue("@Image2", product.PImage2);
                    cmd.Parameters.AddWithValue("@Image3", product.PImage3);
                    cmd.Parameters.AddWithValue("@Availability", product.PAvailability);
                    cmd.Parameters.AddWithValue("@StartDate", product.PStartDate);
                    cmd.Parameters.AddWithValue("@EndDate", product.PEndDate);
                    cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    cmd.Parameters.AddWithValue("@UnitPrice", product.PUnitCost);
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
                {
                    con.Close();
                }
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

        public bool DeleteProduct(int id)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsUpdated = 0;

            try
            {
                using (con = dbContext.Connect())
                using (cmd = new SqlCommand("spDeleteProduct", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
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
                {
                    con.Close();
                }
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

        public bool RentNewProduct(RentProduct product)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsUpdated = 0;

            try
            {
                using (con = dbContext.Connect())
                using (cmd = new SqlCommand("spRentNewProduct", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                    cmd.Parameters.AddWithValue("@UserId", product.UserId);
                    cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    cmd.Parameters.AddWithValue("@VendorId", product.VendorId);
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@StartDate", product.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", product.EndDate);
                    cmd.Parameters.AddWithValue("@PayableAmount", product.PayableAmount);
                    cmd.Parameters.AddWithValue("@PayStatus", product.PayStatus);
                    cmd.Parameters.AddWithValue("@ProductImage", product.ProductImage);
                    cmd.Parameters.AddWithValue("@BookingStatus", product.BookingStatus);
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
                {
                    con.Close();
                }
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

        public bool ApproveBookingStatus(int id)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsUpdated = 0;

            try
            {
                using (con = dbContext.Connect())
                using (cmd = new SqlCommand("spApproveBooking", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    rowsUpdated = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                //USE LOGGER
                return false;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
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

        public bool MakeProductUnavailable(int id)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsUpdated = 0;

            try
            {
                using (con = dbContext.Connect())
                using (cmd = new SqlCommand("spMakeProductUnavailable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    rowsUpdated = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                //USE LOGGER
                return false;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
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
