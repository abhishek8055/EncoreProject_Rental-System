using EncoreBL.Repositories;
using EncoreML;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


//@AUTHOR ABHISHEK DWIVEDI
//ADMIN CONTROLLER MANAGES ALL ADMIN RELATED TASKS


namespace EncoreView.Controllers
{
    public class AdminController : Controller
    {
        //LOGGER INITIALIZATION
        readonly ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        //GET: INSTANCE OF USERACTIONS CLASS OF BUSINESS LAYER
        UserActions userActionContext = new UserActions();

        //GET: INSTANCE OF PRODUCTACTIONS CLASS OF BUSINESS LAYER
        ProductActions productActionContext = new ProductActions();

        // GET: GET ALL PRODUCTS
        public ActionResult Index()
        {
            IEnumerable<ProductModel> productList = null;
            try
            {
                productList = productActionContext.GetProducts();
            }
            catch(Exception e)
            {
                //LOG EXCEPTION
                logger.Error("Index() of Admin Controller : ", e);
                //***********************//
            }
            return View(productList);
        }

        //GET: GET ALL FEEDBACKS
        public ActionResult FeedbackList()
        {
            IEnumerable<FeedbackModel> feedbackList = null;
            try
            {
                feedbackList = userActionContext.GetFeedbacks();
            }
            catch(Exception e)
            {
                //LOG EXCEPTION
                logger.Error("FeedbackList() in Admin Controller : ", e);
                //***********************//
            }
            return View(feedbackList);
        }
    }
}