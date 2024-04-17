using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Learn_mvc.Models
{
    public class Comman_class
    {
        static List<Student> lst_student = new List<Student>();

        public Comman_class()
        {

        }

        public void Create_student_collection()
        {
            lst_student.Add(new Student { Id = 1, Name = "Golu", Email = "Golu@gmail.com", Phone = "987898767" });
            lst_student.Add(new Student { Id = 2, Name = "Molu", Email = "Molu@gmail.com", Phone = "887898767" });
            lst_student.Add(new Student { Id = 3, Name = "Lolu", Email = "Lolu@gmail.com", Phone = "787898767" });
            lst_student.Add(new Student { Id = 4, Name = "Nolu", Email = "Nolu@gmail.com", Phone = "687898767" });
            lst_student.Add(new Student { Id = 5, Name = "Dolu", Email = "Dolu@gmail.com", Phone = "587898767" });
        }

        public bool Validate_model(object model)
        {
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            if (Validator.TryValidateObject(model, context, results, true))
            {
                return true;
            }
            return false;
        }

        public object Get_student_collection()
        {
            Model_Response model_Response = new Model_Response();
            
            try
            {
                if (lst_student == null || lst_student.Count == 0)
                {
                    //int a = 1;
                    //int b = 0;
                    //int c = a / b;
                    Create_student_collection();
                }
            }
            catch (Exception ex)
            {
                model_Response.Response_Message = ex.Message.ToString().Trim();
            }
            finally
            {

            }
            if (model_Response.Response_Message == null)
            {
                return lst_student;
            }
            else
            {
                return model_Response;
            }
        }

        public Model_Response Insert_student(Student std)
        {
            Model_Response model_Response = new Model_Response();
            try
            {
                if (std != null)
                {
                    lst_student.Add(std);

                    model_Response.Response_Message = "Record inserted successfully.";
                }
                else
                {
                    model_Response.Response_Message = "Please check parameters is not in correct format.";
                }
            }
            catch (Exception ex)
            {
                model_Response.Response_Message = ex.Message.ToString().Trim();
            }
            finally
            {

            }
            return model_Response;
        }

        public object Get_student_by_id(int? id)
        {
            Model_Response model_Response = new Model_Response();
            Student std = new Student();

            try
            {
                if (lst_student != null || lst_student.Count != 0)
                {
                    std = lst_student.Where(x => x.Id == id).SingleOrDefault();
                }
                else
                {
                    model_Response.Response_Message = "Records not found.";
                }
            }
            catch (Exception ex)
            {
                model_Response.Response_Message = ex.Message.ToString().Trim();
            }
            finally
            {

            }
            if (model_Response.Response_Message == null)
            {
                return std;
            }
            else
            {
                return model_Response;
            }
        }

        public Model_Response Update_student(Student std)
        {
            Model_Response model_Response = new Model_Response();

            try
            {
                if (std != null)
                {
                    var s = lst_student.Where(x => x.Id == std.Id).SingleOrDefault();
                    s.Name = std.Name;
                    s.Email = std.Email;
                    s.Phone = std.Phone;
                    
                    model_Response.Response_Message = "Record updated successfully.";
                }
                else
                {
                    model_Response.Response_Message = "Please check parameters is not in correct format.";
                }
            }
            catch (Exception ex)
            {
                model_Response.Response_Message = ex.Message.ToString().Trim();
            }
            finally
            {

            }
            return model_Response;
        }

        public Model_Response Delete_student(int id)
        {
            Model_Response model_Response = new Model_Response();
            try
            {
                var s = lst_student.Where(x => x.Id == id).SingleOrDefault();
                lst_student.Remove(s);

                model_Response.Response_Message = "Record deleted successfully.";
            }
            catch (Exception ex)
            {
                model_Response.Response_Message = ex.Message.ToString().Trim();
            }
            finally
            {

            }
            return model_Response;
        }
    }
}