using AutoMapper;
using EncoreBL.Repositories;
using EncoreML;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//@AUTHOR ABHISHEK DWIVEDI
//PRODUCT CONTROLLER MANAGES ALL PRODUCT SPECIFIC VIEWS/TASKS

namespace EncoreView.Controllers
{
    [Route("Product/{action?}/{id?}")]
    public class ProductController : Controller
    {
        //LOGGER INITIALIZATION
        readonly ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        //GET: INSTANCE OF PRODUCTACTIONS CLASS OF BUSINESS LAYER
        ProductActions productActionContext = new ProductActions();

        //NO DEFAULT VIEW 
        public ActionResult Index()
        {
            return View();
        }




        //POST: ADD NEW PRODUCT
        //ONLY VENDOR IS AUTHORISED
        public ActionResult ProductForm()
        {
            try
            {
                IEnumerable<CategoryModel> categoryList = productActionContext.GetCategories();
                ViewBag.Category = categoryList;
            }
            catch(Exception e)
            {
                //LOG EXCEPTION
                logger.Error("Categories not loaded by ProductForm() of ProductController : ", e);
            }
            return View();

        }



        //POST: SAVING NEW PRODUCT
        public ActionResult Save(ProductModel product)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ProductForm");
            }
            UserLoginModel user = (UserLoginModel)HttpContext.Session["USER"];
            product.VendorId = user.Id;
            bool status = productActionContext.AddProduct(product);
            //IF UPLOAD UNSUCCESSFULL
            if (!status)
            {
                TempData["ProductNotAdded"] = true;
                return View("ProductForm", "Product");
            }
            //PRODUCT ADDED SUCCESSFULLY
            TempData["ProductNotAdded"] = false;
            return RedirectToAction("VendorIndex", "User");
        }



        //POST: EDIT PRODUCT
        //ONLY VENDOR AND ADMIN IS AUTHORISED
        public ActionResult EditProduct(int id)
        {
            ProductModel product = null;
            try
            {
                product = productActionContext.GetProductById(id);
            }
            catch(Exception e)
            {
                //LOG EXCEPTION
                logger.Error("Product not loaded by EditProduct() of Product Controller : ", e);
            }
            if (product == null)
            {
                return HttpNotFound();
            }

            IEnumerable<CategoryModel> categoryList = productActionContext.GetCategories();
            ViewBag.Category = categoryList;
            return View(product);
        }





        //POST: UPDATE PRODUCT
        public ActionResult Update(ProductModel uProduct)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("EditProduct");
            }

            AddProductModel updateProduct = new AddProductModel();
            updateProduct.Id = uProduct.PId;
            updateProduct.Name = uProduct.PName;
            updateProduct.Description = uProduct.PDescription;
            updateProduct.UnitPrice = uProduct.PUnitCost;
            updateProduct.StartDate = uProduct.PStartDate;
            updateProduct.EndDate = uProduct.PEndDate;
            updateProduct.UploadImage1 = uProduct.UploadImage1;
            updateProduct.UploadImage2 = uProduct.UploadImage2;
            updateProduct.UploadImage3 = uProduct.UploadImage3;
            updateProduct.Availability = uProduct.PAvailability;
            updateProduct.CategoryId = uProduct.CategoryId;

            ProductModel product = SaveImages(updateProduct);
            product.PId = uProduct.PId;
            if (product == null)
            {
                return RedirectToAction("EditProduct");
            }
            bool status;
            try
            {
                status = productActionContext.UpdateProduct(product);
            }
            catch (Exception e)
            {
                throw e;
            }
            if (status)
            {
                TempData["UpdateFail"] = false;
                return RedirectToAction("RedirectTo", "Account");
            }

            TempData["UpdateFail"] = true;
            return RedirectToAction("EditProduct", "Product");
        }





        //POST: DELETE PRODUCT
        //ONLY ADMIN AND VENDOR IS AUTHORISED
        public ActionResult Delete(int id)
        {
            bool status = productActionContext.DeleteProduct(id);
            if (!status)
            {
                TempData["Deleted"] = false;
            }
            TempData["Deleted"] = true;
            return RedirectToAction("RedirectTo", "Account");
        }





        //GET: GET PRODUCT DETAILS BY PRODUCT ID
        //ONLY CUSTOMER IS AUTHORISED
        public ActionResult ProductDetails(int id)
        {
            ProductModel product =  productActionContext.GetProductById(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        //POST: ADD PRODUCT
        //ONLY VENDOR IS AUTHORISED
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(AddProductModel addProduct)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddProduct");
            }

            ProductModel product = SaveImages(addProduct);

            if (product == null)
            {
                return RedirectToAction("AddProduct");
            }
            product.PAvailability = true;
            UserLoginModel user = (UserLoginModel)HttpContext.Session["USER"];
            product.VendorId = user.Id;
            bool status;
            try
            {
                status = productActionContext.AddProduct(product);
            }
            catch (Exception e)
            {
                throw e;
            }
            if (status)
            {
                return RedirectToAction("VendorIndex", "User");
            }

            ViewBag.Status = true;
            IEnumerable<CategoryModel> categoryList = productActionContext.GetCategories();
            ViewBag.Category = categoryList;
            return View();
        }





        //ADD PRODUCT TO CUSTOMER ACCOUNT I.E RENTED PRODUCT
        public ActionResult RentProduct(ProductModel product)
        {   
            //CREATE INSTANCE OF RENT PRODUCT MODEL CLASS
            RentProductModel rProduct = new RentProductModel()
            {
                ProductId = product.PId,
                VendorId = product.VendorId,
                StartDate = product.PStartDate,
                EndDate = product.PEndDate,
                CategoryId = product.CategoryId,
                ProductImage = product.PImage1,
                ProductName = product.PName
            };
            rProduct.PayStatus = true;
            rProduct.PayableAmount = product.PUnitCost * (product.PEndDate - product.PStartDate).TotalDays;
            rProduct.BookingStatus = false;
            UserLoginModel user = (UserLoginModel)HttpContext.Session["USER"];
            rProduct.UserId = user.Id;

            bool status = productActionContext.RentNewProduct(rProduct);
            if (!status)
            {
                TempData["FailStatus"] = true;
                return RedirectToAction("ProductDetails", "Product", new { id=product.PId} );
            }
            TempData["FailStatus"] = false;
            return RedirectToAction("MyOrders", "User");
        }






        #region SaveImages
        // RETURN PRODUCTMODEL OBJECT
        private ProductModel SaveImages(AddProductModel addProduct)
        {
            ProductModel productModel = null;
            bool imagesSavedSuccessfully = true;
            try
            {
                if (addProduct.UploadImage1 != null)
                {
                    //Save Image 1
                    var file = addProduct.UploadImage1;
                    addProduct.Image1 = SaveImage(file);
                }
                if (addProduct.UploadImage2 != null)
                {
                    //Save Image 2
                    var file = addProduct.UploadImage2;
                    addProduct.Image2 = SaveImage(file);
                }
                if (addProduct.UploadImage3 != null)
                {
                    //Save Image 3
                    var file = addProduct.UploadImage3;
                    addProduct.Image3 = SaveImage(file);
                }
            }
            catch (Exception)
            {
                imagesSavedSuccessfully = false;
            }
            if (imagesSavedSuccessfully)
            {
                productModel = new ProductModel()
                {
                    PId = addProduct.Id,
                    PName = addProduct.Name,
                    PDescription = addProduct.Description,
                    PImage1 = addProduct.Image1,
                    PImage2 = addProduct.Image2,
                    PImage3 = addProduct.Image3,
                    PStartDate = addProduct.StartDate,
                    PEndDate = addProduct.EndDate,
                    CategoryId = addProduct.CategoryId,
                    PUnitCost = addProduct.UnitPrice,
                    PAvailability = addProduct.Availability,
                };
            }
            return productModel;

        }

        private string SaveImage(HttpPostedFileBase file)
        {
            string fName = "";
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                fileName = Guid.NewGuid() + fileName + extension;
                fName = fileName;
                fileName = Path.Combine(Server.MapPath("~/Images"), fileName);
                file.SaveAs(fileName);
            }
            catch (Exception e)
            {

                throw e;
            }

            return fName;
        }
        #endregion
    }
}


