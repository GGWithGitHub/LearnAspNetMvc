using Learn_mvc.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Controllers
{
    public class GenericRepoAndUOFPatternController : Controller
    {
        // GET: GenericRepoAndUOFPattern
        public ActionResult Index()
        {
            var customers = new RepoPatternViewRepository().GetCustomers();
            return View(customers);
        }
    }
}