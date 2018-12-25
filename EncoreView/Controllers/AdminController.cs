using EncoreBL.Repositories;
using EncoreML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncoreView.Controllers
{
    public class AdminController : Controller
    {
        UserActions userActionContext = new UserActions();
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