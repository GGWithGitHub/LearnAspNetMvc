using Learn_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Controllers
{
    public class InterpolationWithJsCshtmlController : Controller
    {
        static List<Employee3> employees = new List<Employee3>();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEmployeeDetails()
        {
            return Json(
                new { Data = employees }, JsonRequestBehavior.AllowGet
            );
        }

        public PartialViewResult AddMoreEmployee()
        {
            return PartialView("_AddMoreEmployee");
        }

        [HttpPost]
        public PartialViewResult AddMoreEmployee(Employee3 emp)
        {
            Random random = new Random();
            emp.EmpId = random.Next();
            employees.Add(emp);
            return PartialView("_AddMoreEmployee", emp);
        }
    }
}