using Learn_mvc.Database;
using Learn_mvc.Db_context;
using Learn_mvc.Models;
using Learn_mvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Controllers
{
    public class TestController : Controller,IDisposable
    {
        TestRepository testRepository = new TestRepository();
        private bool disposed = false;
        public ActionResult Index()
        {
            List<product> list_product = testRepository.Get_products();

            //var aa = list_emp.Where(x => x.brand_id == 9).FirstOrDefault().brand.brand_name;
            return View(list_product);
        }

        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult Index3()
        {
            string json = "I am index 3";
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Index4()
        {
            TempData["last4"] = 1111;
            Card card = new Card();
            card.EncryptedCardNumber = "shrhgrer435256";
            return View(card);
        }

        [HttpPost]
        public ActionResult Index4(Card card)
        {
            var last4 = TempData["last4"].ToString();
            if (last4 == card.CardNumber)
            {
                ViewBag.msg = "You did not update card number";
            }
            if (card.CardName!=null && card.CardName!="")
            {
                
            }
            TempData.Keep("last4");
            return View();
        }

        public ActionResult Index5()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index5(TestModel testModel)
        {
            if (ModelState.IsValid)
            {
                //Model is valid save it in database
                return View();
            }
            else
            {
                //Model is not valid show error
                return View(testModel);
            }
        }

        public JsonResult IsEmailAlready(string EmailId)
        {
            bool status;
            if (EmailId == "abc@gmail.com")
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return Json(status, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Index6()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index6Api(string q, string b)
        {

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    testRepository.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}