using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Controllers
{
    public class PrintController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoadDynamicHtml(PrintC obj)
        {
            ViewBag.Students = obj;
            return PartialView("_Print");
        }
    }

    public class PrintC
    {
        public string Img { get; set; }
        public List<PrintStd> StdList { get; set; }
    }

    public class PrintStd
    {
        public string fn { get; set; }
        public string ln { get; set; }
        public string age { get; set; }
    }
}