using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Learn_mvc.Controllers
{
    public class AutoCompleteAddressController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AutoSearchModel()
        {
            return View();
        }

        public ActionResult FetchModels(string query)
        {
            var models = new List<ModelMachine>() { 
                new ModelMachine { ModelNumber="M101", ModelDesc= "This is M101 model machine" },
                new ModelMachine { ModelNumber="M102", ModelDesc= "This is M102 model machine" }
            };
            var dictionaries = models.ToDictionary(prop => prop.ModelNumber, prop => prop.ModelDesc).ToList();
            return Json(dictionaries, JsonRequestBehavior.AllowGet);
        }
    }

    public class ModelMachine
    {
        public string ModelNumber { get; set; }
        public string ModelDesc { get; set; }
    }
}