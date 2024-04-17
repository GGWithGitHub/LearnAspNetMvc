using Learn_mvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Controllers
{
    public class CRUDModalController : Controller
    {
        static List<Employee2> std_list = new List<Employee2>() {
            new Employee2(){ EmpId=1, EmpName="Chinu", EmpEmail="chinu@gmail.com", EmpAddress="chinu address"}
        };
        public ActionResult Index()
        {
            return View(std_list);
        }

        public PartialViewResult CreateEmployee()
        {
            return PartialView("_CreateEmployee");
        }

        [HttpPost]
        public PartialViewResult CreateEmployee(Employee2 emp)
        {
            Random random = new Random();
            emp.EmpId = random.Next();
            std_list.Add(emp);
            return PartialView("_CreateEmployee", emp);
        }

        public JsonResult GetAllEmployeeList()
        {
            var model = std_list;
            var view = RenderViewToString(this, "_EmployeeList", model);
            var data = new List<dynamic>
            {
                view
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // ------------------------------------------------------------------------------------------ // 
        public string RenderViewToString(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);

                if (viewResult.View is null)
                    viewResult = ViewEngines.Engines.FindView(controller.ControllerContext, viewName, null);

                if (viewResult.View is null)
                    throw new FileNotFoundException("View cannot be found.");

                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}