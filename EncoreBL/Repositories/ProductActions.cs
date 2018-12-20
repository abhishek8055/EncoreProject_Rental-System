using AutoMapper;
using EncoreBL.Interfaces;
using EncoreDAL;
using EncoreML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreBL.Repositories
{
    public class ProductActions : IProduct
    {
        ProductActionsDAL db = new ProductActionsDAL();

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
                throw e;
            }
            return status;
        }

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

                throw e;
            }
            return productModel;
        }

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

                throw e;
            }
            finally
            {

            }
            return productList;
        }

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

                throw e;
            }
            return categoryList;
        }

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
                throw e;
            }
            return status;
        }

        public bool DeleteProduct(int id)
        {
            bool status = false;
            try
            {
                status = db.DeleteProduct(id);
            }
            catch (Exception e)
            {
                throw e;
            }
            return status;
        }
    }
}
