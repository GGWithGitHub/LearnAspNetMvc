using Learn_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Controllers
{
    public class AjaxHelpersController : Controller
    {
        static List<City> lst_city = new List<City>();
        public ActionResult Index()
        {
            ViewBag.cities = lst_city;
            return View();
        }

        [HttpPost]
        public ActionResult AddCity(City city)
        {
            Random random = new Random();
            //city.Id = new Guid().ToString();
            city.Id = random.Next();
            lst_city.Add(city);
            return PartialView("_ShowCities",lst_city);
        }
    }
}