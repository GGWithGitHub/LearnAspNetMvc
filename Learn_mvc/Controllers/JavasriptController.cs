using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Controllers
{
    public class JavasriptController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            List<CategoryLink> categoryLinks = new List<CategoryLink>() { 
                new CategoryLink(){ 
                    Category="Category 1", 
                    Links=new List<string>(){"Link 1", "Link 2", "Link 3" }
                },
                new CategoryLink(){
                    Category="Category 2",
                    Links=new List<string>(){ "Link 1", "Link 2", "Link 3" }
                }
            };
            var categoryLinksJson = JsonConvert.SerializeObject(categoryLinks);
            ViewBag.CategoryLink = categoryLinksJson;
            return View();
        }
        public ActionResult GetListObject()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetListObject(MyProduct myProduct)
        {
            if (myProduct!=null)
            {
                return Json(true);
            }
            return Json(false);
        }
    }

    public class CategoryLink
    {
        public string Category { get; set; }
        public List<string> Links { get; set; }
    }

    public class MyProduct
    {
        public string ProductSKU { get; set; }
        public List<MyProductDetail> ProductDetail { get; set; }
    }

    public class MyProductDetail
    {
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
    }
}