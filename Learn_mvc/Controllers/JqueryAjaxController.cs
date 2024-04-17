using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Controllers
{
    public class JqueryAjaxController : Controller
    {
        private static List<JAEmp> emps = new List<JAEmp>() {
                new JAEmp(){ EmpId="101", EmpName="Aman", EmpSalary="1000"},
                new JAEmp(){ EmpId="102", EmpName="Bman", EmpSalary="2000"},
                new JAEmp(){ EmpId="103", EmpName="Cman", EmpSalary="3000"},
                new JAEmp(){ EmpId="104", EmpName="Dman", EmpSalary="4000"}
            };

        public JqueryAjaxController()
        {
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEmps()
        {
            object data = "";
            var msg = "";
            if (emps != null)
            {
                data = emps;
                msg = "SUCCESS";
            }
            var json = new { Data = data, Message = msg };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddEmp(JAEmp emp)
        {
            object data = false;
            var msg = "";
            if (ModelState.IsValid)
            {
                emp.EmpId = Guid.NewGuid().ToString();
                if (!emps.Select(x=>x.EmpId).Contains(emp.EmpId))
                {
                    emps.Add(emp);
                    data = true;
                    msg = "SUCCESS";
                }
            }
            var json = new { Data = data, Message = msg };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteEmp(string empId)
        {
            object data = false;
            var msg = "";
            if (empId != null && empId != "")
            {
                var obj = emps.Where(x => x.EmpId == empId).FirstOrDefault();
                if (obj != null)
                {
                    emps.Remove(obj);
                    data = true;
                    msg = "SUCCESS";
                }
            }
            var json = new { Data = data, Message = msg };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateEmp(string empId, JAEmp emp)
        {
            object data = false;
            var msg = "";
            if (empId != null && empId != "")
            {
                if (ModelState.IsValid)
                {
                    var obj = emps.Where(x => x.EmpId == empId).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.EmpName = emp.EmpName;
                        obj.EmpSalary = emp.EmpSalary;
                        data = true;
                        msg = "SUCCESS";
                    }
                } 
            }
            var json = new { Data = data, Message = msg };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }

    public class JAEmp
    {
        public string EmpId { get; set; }

        [Required]
        public string EmpName { get; set; }

        [Required]
        public string EmpSalary { get; set; }
    }
}