using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatabaseClient.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Account Page";
            return View();
        }
    }
}