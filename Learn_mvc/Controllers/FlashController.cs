using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Controllers
{
    public class FlashController : Controller
    {
        // GET: Flash
        public ActionResult Index()
        {
            return View();
        }
    }
}