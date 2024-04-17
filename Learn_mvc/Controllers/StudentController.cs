using AutoMapper;
using Learn_mvc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Controllers
{
    public class StudentController : Controller
    {
        Comman_class comman_Class = new Comman_class();
        public StudentController()
        {
        }

        // GET: Home
        public ActionResult Get_student_list()
        {
            //object obj = comman_Class.Get_student_collection();
            //if ((obj is Model_Response))
            //{
            //    ViewBag.Students = new List<Student>();
            //}
            //else
            //{
            //    ViewBag.Students = obj;
            //}
            Load_student_list();
            return View();

            // ------------------------------------------------------------------ // 

            //BigViewModel bigViewModel = new BigViewModel();
            //object obj = comman_Class.Get_student_collection();
            //if ((obj is Model_Response))
            //{
            //    bigViewModel.StudentViewModels = new List<Student>();
            //}
            //else
            //{
            //    bigViewModel.StudentViewModels = obj as List<Student>;
            //}
            //return View(bigViewModel);
        }

        public ActionResult Create_student()
        {
            BigViewModel bigViewModel = new BigViewModel();
            return View(bigViewModel);
        }

        [HttpPost]
        public ActionResult Create_student(BigViewModel bigViewModel)
        {
            
            var student_model = bigViewModel.StudentViewModel;
            bool isValidModel = comman_Class.Validate_model(student_model);
            if (isValidModel)
            {
                if (bigViewModel.StudentViewModel.Id <= 0)
                {
                    // add
                    Model_Response model_Response = comman_Class.Insert_student(student_model);
                    if (model_Response.Response_Message == "Record inserted successfully.")
                    {
                        return RedirectToAction("Get_student_list");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    // update
                    Model_Response model_Response = comman_Class.Update_student(student_model);
                    if (model_Response.Response_Message == "Record updated successfully.")
                    {
                        return RedirectToAction("Get_student_list");
                    }
                    else
                    {
                        return View();
                    }
                }
                
            }
            return View();
        }

        public ActionResult Edit_student(int id)
        {
            var std = comman_Class.Get_student_by_id(id);
            BigViewModel bigViewModel = new BigViewModel();
            bigViewModel.StudentViewModel = std as Student;
            if ((std is Model_Response))
            {
                return View("Create_student");
            }
            else
            {
                return View("Create_student", bigViewModel);
            }
        }

        [HttpPost]
        public ActionResult Delete_student(int id)
        {
            Model_Response model_Response = comman_Class.Delete_student(id);
            if (model_Response.Response_Message == "Record deleted successfully.")
            {
                //string message = "SUCCESS";
                //return Json(new { Message = message, JsonRequestBehavior.AllowGet });

                return Load_student_list();
                //return View();
            }
            else
            {
                //string message = "Error";
                //return Json(new { Message = message, JsonRequestBehavior.AllowGet });

                return View();
            }
        }

        [HttpGet]
        public ActionResult Load_student_list()
        {
            object obj = comman_Class.Get_student_collection();
            if ((obj is Model_Response))
            {
                ViewBag.Students = new List<Student>();
            }
            else
            {
                ViewBag.Students = obj;
            }
            //return PartialView("~/Views/Shared/Load_student_list");
            return PartialView("Load_student_list");
        }
    }
}