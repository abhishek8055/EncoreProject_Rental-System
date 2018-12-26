using EncoreBL.Repositories;
using EncoreML;
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
        //GET: INSTANCE OF USERACTIONS CLASS OF BUSINESS LAYER
        UserActions userActionContext = new UserActions();
        //GET: INSTANCE OF PRODUCTACTIONS CLASS OF BUSINESS LAYER
        ProductActions productActionContext = new ProductActions();

        // GET: GET ALL PRODUCTS
        public ActionResult Index()
        {
            IEnumerable<ProductModel> productList = null;
            productList = productActionContext.GetProducts();
            return View(productList);
        }

        //GET: GET ALL FEEDBACKS
        public ActionResult FeedbackList()
        {
            IEnumerable<FeedbackModel> feedbackList = null;
            feedbackList = userActionContext.GetFeedbacks();
            return View(feedbackList);
        }
    }
}