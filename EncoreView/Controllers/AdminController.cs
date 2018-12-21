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
        UserActions ua = new UserActions();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FeedbackList()
        {
            IEnumerable<FeedbackModel> feedbackList = null;
            feedbackList = ua.GetFeedbacks();
            return View(feedbackList);
        }
    }
}