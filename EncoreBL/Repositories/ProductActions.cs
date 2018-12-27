using AutoMapper;
using EncoreBL.Interfaces;
using EncoreDAL;
using EncoreDAL.Entities;
using EncoreML;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//@AUTHOR ABHISHEK DWIVEDI
//PRODUCT_ACTIONS CLASS IS A BUSINESS LAYER CLASS WHICH IMPLEMENTS IPRODUCT INTERFACE 
//PRODUCT_ACTIONS CLASS MAPS ALL PRODUCT RELATED QUERIES TO THE PRODUCT_ACTIONS_DAL (DAL) CLASS

namespace EncoreBL.Repositories
{
    public class ProductActions : IProduct
    {
        //LOGGER INITIALIZATION
        readonly ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        //GET: INSTANCE OF PRODUCT_ACTIONS_DAL (DAL CLASS)
        ProductActionsDAL db = new ProductActionsDAL();

        //POST: ADD NEW PRODUCT TO THE DATABASE
        public bool AddProduct(ProductModel productModel)
        {
            Product product = new Product();
            bool status = false;
            Mapper.Map(productModel, product);
            try
            {
                status = db.AddProduct(product);
            }
            catch(Exception e)
            {
                //LOG EXCEPTION
                logger.Fatal("AddProduct() in ProductActions Class of Business Layer : ", e);
                return false;
            }
            return status;
        }

        //GET: GET PRODUCT BY PRODUCT ID
        public ProductModel GetProductById(int productId)
        {
            ProductModel productModel = new ProductModel();
            string query = "SELECT * FROM Products WHERE PId="+ productId;
            DataSet ds = null;
            try
            {
                ds = db.dbContext.GetData(query);
                
                productModel.PId = Convert.ToInt32(ds.Tables[0].Rows[0]["PId"]);
                productModel.VendorId = Convert.ToInt32(ds.Tables[0].Rows[0]["VendorId"]);
                productModel.PName = Convert.ToString(ds.Tables[0].Rows[0]["PName"]);
                productModel.PDescription = Convert.ToString(ds.Tables[0].Rows[0]["PDescription"]);
                productModel.PImage1 = Convert.ToString(ds.Tables[0].Rows[0]["PImage1"]);
                productModel.PImage2 = Convert.ToString(ds.Tables[0].Rows[0]["PImage2"]);
                productModel.PImage3 = Convert.ToString(ds.Tables[0].Rows[0]["PImage3"]);
                productModel.PAvailability = Convert.ToBoolean(ds.Tables[0].Rows[0]["PAvailability"]);
                productModel.PStartDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["PStartDate"]);
                productModel.PEndDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["PEndDate"]);
                productModel.PUnitCost = Convert.ToDouble(ds.Tables[0].Rows[0]["PUnitCost"]);
                productModel.CategoryId = Convert.ToInt32(ds.Tables[0].Rows[0]["CategoryId"]);
            }
            catch (Exception e)
            {
                //LOG EXCEPTION
                logger.Fatal("GetProductById() in ProductActions Class of Business Layer : ", e);
                return null;
            }
            return productModel;
        }

        //GET: GET ALL PRODUCTS FROM DATABASE
        public IEnumerable<ProductModel> GetProducts()
        {
            string query = "SELECT * FROM Products WITH (NOLOCK)";
            List<ProductModel> productList = new List<ProductModel>();
            DataSet ds = null;
            try
            {
                ds = db.dbContext.GetData(query);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    productList.Add(new ProductModel {
                        PId = Convert.ToInt32(dr["PId"]),
                        VendorId = Convert.ToInt32(dr["VendorId"]),
                        PName = Convert.ToString(dr["PName"]),
                        PDescription = Convert.ToString(dr["PDescription"]),
                        PImage1 = Convert.ToString(dr["PImage1"]),
                        PImage2 = Convert.ToString(dr["PImage2"]),
                        PImage3 = Convert.ToString(dr["PImage3"]),
                        PAvailability = Convert.ToBoolean(dr["PAvailability"]),
                        PStartDate = Convert.ToDateTime(dr["PStartDate"]),
                        PEndDate = Convert.ToDateTime(dr["PEndDate"]),
                        PUnitCost = Convert.ToDouble(dr["PUnitCost"]),
                        CategoryId = Convert.ToInt32(dr["CategoryId"])
                    });
                }
            }
            catch (Exception e)
            {
                //LOG EXCEPTION
                logger.Fatal("GetProducts() in ProductActions Class of Business Layer : ", e);
                //return null;
            }
            return productList;
        }

        //GET: GET ALL PRODUCTS OF A PERTICULAR VENDOR
        public IEnumerable<ProductModel> GetProductsByVendorId(int vendorId)
        {
            string query = "SELECT * FROM Products WHERE VendorId="+ vendorId;
            List<ProductModel> productList = new List<ProductModel>();
            DataSet ds = null;
            try
            {
                ds = db.dbContext.GetData(query);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    productList.Add(new ProductModel
                    {
                        PId = Convert.ToInt32(dr["PId"]),
                        VendorId = Convert.ToInt32(dr["VendorId"]),
                        PName = Convert.ToString(dr["PName"]),
                        PDescription = Convert.ToString(dr["PDescription"]),
                        PImage1 = Convert.ToString(dr["PImage1"]),
                        PImage2 = Convert.ToString(dr["PImage2"]),
                        PImage3 = Convert.ToString(dr["PImage3"]),
                        PAvailability = Convert.ToBoolean(dr["PAvailability"]),
                        PStartDate = Convert.ToDateTime(dr["PStartDate"]),
                        PEndDate = Convert.ToDateTime(dr["PEndDate"]),
                        PUnitCost = Convert.ToDouble(dr["PUnitCost"]),
                        CategoryId = Convert.ToInt32(dr["CategoryId"])
                    });
                }
            }
            catch (Exception e)
            {
                //LOG EXCEPTION
                logger.Fatal("GetProductsByVendorId() in ProductActions Class of Business Layer : ", e);
                return null;
            }
            return productList;
        }

        //GET: GET ALL THE PRODUCT CATEGORIES AVAILABLE
        public IEnumerable<CategoryModel> GetCategories()
        {
            string query = "SELECT * FROM Category WITH (NOLOCK)";
            List<CategoryModel> categoryList = new List<CategoryModel>();
            DataSet ds = null;
            try
            {
                ds = db.dbContext.GetData(query);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    categoryList.Add(new CategoryModel
                    {
                        CId = Convert.ToInt32(dr["CId"].ToString()),
                        CName = dr["CName"].ToString(),
                    });
                }
            }
            catch (Exception e)
            {
                //LOG EXCEPTION
                logger.Fatal("GetCategories() in ProductActions Class of Business Layer : ", e);
                return null;
            }
            return categoryList;
        }

        //POST: UPDATE EXISTING PRODUCT
        public bool UpdateProduct(ProductModel productModel)
        {
            Product product = new Product();
            bool status = false;
            Mapper.Map(productModel, product);
            try
            {
                status = db.UpdateProduct(product);
            }
            catch (Exception e)
            {
                //LOG EXCEPTION
                logger.Fatal("UpdateProduct() in ProductActions Class of Business Layer : ", e);
                return false;
            }
            return status;
        }

        //POST: DELETE PRODUCT FORM DATABASE
        public bool DeleteProduct(int id)
        {
            bool status = false;
            try
            {
                status = db.DeleteProduct(id);
            }
            catch (Exception e)
            {
                //LOG EXCEPTION
                logger.Fatal("DeleteProduct() in ProductActions Class of Business Layer : ", e);
                return false;
            }
            return status;
        }

        //POST: ADD PRODUCT TO RENTED_PRODUCTS
        public bool RentNewProduct(RentProductModel rentProductModel)
        {
            RentProduct rentProduct = new RentProduct();
            bool status = false;
            Mapper.Map(rentProductModel, rentProduct);
            try
            {
                status = db.RentNewProduct(rentProduct);
            }
            catch (Exception e)
            {
                //LOG EXCEPTION
                logger.Fatal("RentNewProduct() in ProductActions Class of Business Layer : ", e);
                return false;
            }
            return status;
        }

        //POST: GET ALL PRODUCTS RENTED ON BY A PERTICULAR CUSTOMER
        public IEnumerable<RentProductModel> GetRentedProductsByUserId(int id)
        {
            string query = "SELECT * FROM RentProduct WHERE UserId="+id;
            List<RentProductModel> productList = new List<RentProductModel>();
            DataSet ds = null;
            try
            {
                ds = db.dbContext.GetData(query);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    productList.Add(new RentProductModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        UserId = Convert.ToInt32(dr["UserId"]),
                        CategoryId = Convert.ToInt32(dr["CategoryId"]),
                        VendorId = Convert.ToInt32(dr["VendorId"]),
                        ProductName = Convert.ToString(dr["ProductName"]),
                        StartDate = Convert.ToDateTime(dr["StartDate"]),
                        EndDate = Convert.ToDateTime(dr["EndDate"]),
                        PayableAmount = Convert.ToInt32(dr["PayableAmount"]),
                        PayStatus = Convert.ToBoolean(dr["PayStatus"]),
                        ProductImage = Convert.ToString(dr["ProductImage"]),                       
                        BookingStatus = Convert.ToBoolean(dr["BookingStatus"])
                    });
                }
            }
            catch (Exception e)
            {
                //LOG EXCEPTION
                logger.Fatal("GetRentedProductByUserId() in ProductActions Class of Business Layer : ", e);
                return null;
            }
            return productList;
        }

        //POST: GET ALL PRODUCTS HOSTED BY A PERTICULAR VENDOR
        public IEnumerable<RentProductModel> GetRentedProductsByVendorId(int vendorId)
        {
            string query = "SELECT * FROM RentProduct WHERE VendorId=" + vendorId;
            List<RentProductModel> productList = new List<RentProductModel>();
            DataSet ds = null;
            try
            {
                ds = db.dbContext.GetData(query);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    productList.Add(new RentProductModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        UserId = Convert.ToInt32(dr["UserId"]),
                        CategoryId = Convert.ToInt32(dr["CategoryId"]),
                        VendorId = Convert.ToInt32(dr["VendorId"]),
                        ProductName = Convert.ToString(dr["ProductName"]),
                        StartDate = Convert.ToDateTime(dr["StartDate"]),
                        EndDate = Convert.ToDateTime(dr["EndDate"]),
                        PayableAmount = Convert.ToInt32(dr["PayableAmount"]),
                        PayStatus = Convert.ToBoolean(dr["PayStatus"]),
                        ProductImage = Convert.ToString(dr["ProductImage"]),
                        BookingStatus = Convert.ToBoolean(dr["BookingStatus"])
                    });
                }
            }
            catch (Exception e)
            {
                //LOG EXCEPTION
                logger.Fatal("GetRentedProductsByVendorId() in ProductActions Class of Business Layer : ", e);
                return null;
            }
            return productList;
        }

        //POST: UPDATE BOOKING_STATUS TO TRUE (BY VENDOR ONLY)
        public bool ApproveBookingStatus(int productId)
        {
            bool status = false;
            try
            {
                status = db.ApproveBookingStatus(productId);
            }
            catch (Exception e)
            {
                //LOG EXCEPTION
                logger.Fatal("ApproveBookingStatus() in ProductActions Class of Business Layer : ", e);
                return false;
            }
            return status;
        }

        //POST: UPDATE PRODUCT_AVAILABILITY_STATUS TO TRUE (BY VENDOR ONLY)
        public bool MakeProductUnavailable(int productId)
        {
            bool status = false;
            try
            {
                status = db.MakeProductUnavailable(productId);
            }
            catch (Exception e)
            {
                //LOG EXCEPTION
                logger.Fatal("MakeProductUnavailable() in ProductActions Class of Business Layer : ", e);
                return false;
            }
            return status;
        }
    }
}
