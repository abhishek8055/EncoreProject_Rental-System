﻿using EncoreBL.Repositories;
using EncoreML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncoreView.Controllers
{
    public class UserController : Controller
    {
        UserActions userActionContext = new UserActions();
        ProductActions productActionContext = new ProductActions();

        // GET: HOME PAGE FOR CUSTOMER
        public ActionResult Index()
        {        
            IEnumerable<ProductModel> productList = null;
            productList = productActionContext.GetProducts();
            return View(productList);
        }

        // GET: HOME PAGE FOR VENDOR
        public ActionResult VendorIndex()
        {
            UserLoginModel user = (UserLoginModel)HttpContext.Session["USER"];
            //VENDOR ROLE_ID IS 2
            if(user == null || user.RoleId != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            int vendorId = user.Id;
            IEnumerable<ProductModel> productList = null;
            productList = productActionContext.GetProductsByVendorId(vendorId);
            return View(productList);
        }

        //POST: ADD NEW USER (CUSTOMER/VENDOR)
        public ActionResult RegisterUser(string email, string password, string rpassword, int userType)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddProduct");
            }

            UserLoginModel user = new UserLoginModel();
            user.Email = email;
            user.Password = password;
            user.RoleId = Convert.ToInt32(userType);
            //IF ENTERED PASSWORD NOT MATCH
            if (password != rpassword)
            {
                TempData["PasswordMissMatch"] = true;
                return RedirectToAction("Index", "Account");
            }
            bool status = userActionContext.AddUser(user);
            //IF REGISTRATION FAILS
            if (!status)
            {
                TempData["EmailExists"] = true;
                return RedirectToAction("Index", "Account");
            }
            //IF REGISTRATION SUCCEED
            else
            {
                TempData["EmailExists"] = false;
            }
            return RedirectToAction("Index", "Account");
        }

        public ActionResult UserDetails(string email, int roleId)
        {
            UserModel user = new UserModel();
            user = userActionContext.GetUserByEmail(email, roleId);
            return View(user);
        }

        public ActionResult UpdateUserProfile(UserModel user)
        {
            int roleId = Convert.ToInt32(HttpContext.Session["ROLE"]);
            user.URoleId = roleId;

            bool status = false;
            if(user.UId == 0)
            {
                UserModel nUser = SaveImages(user);
                status = userActionContext.AddUserDetails(nUser);
            }
            else
            {
                UserModel nUser = SaveImages(user);
                status = userActionContext.UpdateUserDetails(nUser);
            }
            if (!status)
            {
                TempData["UpdateFail"] = true;
                return RedirectToAction("UserDetails", "User");
            }
            //IF PROFILE UPDATION SUCCEED
            else
            {
                TempData["UpdateFail"] = false;
            }
            return RedirectToAction("UserDetails", new { email=user.UEmail, roleId=user.URoleId});
        }

        public ActionResult MyOrders()
        {
            UserLoginModel user = (UserLoginModel)HttpContext.Session["USER"];
            int id = user.Id;
            IEnumerable<RentProductModel> rentedProductList = null;
            rentedProductList = productActionContext.GetRentedProductsByUserId(id);
            return View(rentedProductList);
        }

        public ActionResult ProductOrderList()
        {
            UserLoginModel user = (UserLoginModel)HttpContext.Session["USER"];
            int id = user.Id;
            IEnumerable<RentProductModel> productList = null;
            productList = productActionContext.GetRentedProductsByVendorId(id);
            return View(productList);
        }

        public ActionResult ApproveOrder(int id, int productId)
        {
            bool status = productActionContext.ApproveBookingStatus(id);
            
            bool productLock = productActionContext.MakeProductUnavailable(productId);
            if (!productLock || !status)
            {
                TempData["ERROR"] = true;
            }
            TempData["ERROR"] = false;
            return RedirectToAction("ProductOrderList", "User");
        }


        #region SaveProfilePhoto
        // return ProductModel object
        private UserModel SaveImages(UserModel user)
        {
            bool imagesSavedSuccessfully = true;
            try
            {
                if (user.UploadImage != null)
                {
                    //Save Image 1
                    var file = user.UploadImage;
                    user.UPhoto = SaveImage(file);
                }
            }
            catch (Exception)
            {
                imagesSavedSuccessfully = false;
            }
            return user;

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
                fileName = Path.Combine(Server.MapPath("~/Images/Users"), fileName);
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