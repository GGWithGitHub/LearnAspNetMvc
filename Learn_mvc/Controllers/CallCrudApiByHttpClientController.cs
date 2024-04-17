using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Controllers
{
    public class CallCrudApiByHttpClientController : Controller
    {
        private readonly string baseUrl = "https://localhost:44338/api/Encrypt_decrypt/";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetStudents()
        {
            //For handling error- "Could not establish trust relationship for SSL/TLS secure channel"
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            
            //List<HttpClientStudent> resultantObject = null;
            Result<List<HttpclientStudent>> resultantObject = new Result<List<HttpclientStudent>>();
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage APIResponse = new HttpResponseMessage();
                
                var url = baseUrl+"Get_student_list";

                APIResponse = httpClient.GetAsync(url).GetAwaiter().GetResult();

                var resultEmpsJson = APIResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                //resultantObject = JsonConvert.DeserializeObject<List<HttpClientStudent>>(
                //                    JsonConvert.DeserializeObject<string>(resultEmpsJson)
                //                  );
                resultantObject = JsonConvert.DeserializeObject<Result<List<HttpclientStudent>>>(
                                    JsonConvert.DeserializeObject<string>(resultEmpsJson)
                                  );
            }
            catch (Exception ex)
            {
                resultantObject.Response_Message = ex.Message + Environment.NewLine + ex.InnerException.Message + Environment.NewLine + ex.StackTrace;
                //resultantObject = new List<HttpClientStudent>()
                //{
                //    new HttpClientStudent()
                //    {
                //        ErrorDetails = new HttpClientError()
                //        {
                //            Message = ex.Message + Environment.NewLine + ex.InnerException.Message + Environment.NewLine + ex.StackTrace
                //        }
                //    }
                //};
            }
            //if (resultantObject.FirstOrDefault().ErrorDetails != null)
            //{
            //    var errorObject = resultantObject.FirstOrDefault().ErrorDetails;
            //    resultantObject.FirstOrDefault().ErrorDetails.Message = errorObject.ErrorCode + " : " + errorObject.StatusCode + " : " + errorObject.Message;
            //}
            var json = new { resultantObject };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStudent(string id)
        {
            //For handling error- "Could not establish trust relationship for SSL/TLS secure channel"
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            Result<HttpclientStudent> resultantObject = new Result<HttpclientStudent>();
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage APIResponse = new HttpResponseMessage();

                var url = baseUrl+ "Get_student?id=" +id;

                APIResponse = httpClient.GetAsync(url).GetAwaiter().GetResult();

                var resultEmpsJson = APIResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                resultantObject = JsonConvert.DeserializeObject<Result<HttpclientStudent>>(
                                    JsonConvert.DeserializeObject<string>(resultEmpsJson)
                                  );
            }
            catch (Exception ex)
            {
                resultantObject.Response_Message = ex.Message + Environment.NewLine + ex.InnerException.Message + Environment.NewLine + ex.StackTrace;
                //resultantObject = new HttpClientStudent()
                //{
                //    ErrorDetails = new HttpClientError()
                //    {
                //        Message = ex.Message + Environment.NewLine + ex.InnerException.Message + Environment.NewLine + ex.StackTrace
                //    }
                //};
            }
            //if (resultantObject.ErrorDetails != null)
            //{
            //    var errorObject = resultantObject.ErrorDetails;
            //    resultantObject.ErrorDetails.Message = errorObject.ErrorCode + " : " + errorObject.StatusCode + " : " + errorObject.Message;
            //}
            var json = new { resultantObject };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddUpdateStudent(HttpclientStudent std)
        {
            //For handling error- "Could not establish trust relationship for SSL/TLS secure channel"
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            Result<HttpclientStudent> resultantObject = new Result<HttpclientStudent>();
            try
            {
                var url = baseUrl;
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage APIResponse = new HttpResponseMessage();

                if (ModelState.IsValid)
                {
                    //var s = new { name = std.name, age = std.age, subject = std.subject };
                    var stdJson = JsonConvert.SerializeObject(std);
                    var stringContent = new StringContent(stdJson, UnicodeEncoding.UTF8, "application/json");

                    if (!string.IsNullOrEmpty(std.id))
                    {
                        url = baseUrl+ "Get_student?id=" + std.id;

                        APIResponse = httpClient.GetAsync(url).GetAwaiter().GetResult();

                        var resultStdJson = APIResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        var isStudent = JsonConvert.DeserializeObject<Result<HttpclientStudent>>(
                                            JsonConvert.DeserializeObject<string>(resultStdJson)
                                          );
                        if (isStudent.Status_Code == "200")
                        {
                            url = baseUrl+ "Update_student?id="+std.id;

                            APIResponse = httpClient.PutAsync(url, stringContent).GetAwaiter().GetResult();

                            var resultEmpUpdateJson = APIResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            resultantObject = JsonConvert.DeserializeObject<Result<HttpclientStudent>>(
                                                                JsonConvert.DeserializeObject<string>(resultEmpUpdateJson)
                                                            );
                        }
                    }
                    else
                    {
                        url = baseUrl+ "Add_student";

                        APIResponse = httpClient.PostAsync(url, stringContent).GetAwaiter().GetResult();

                        var resultEmpAddJson = APIResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        resultantObject = JsonConvert.DeserializeObject<Result<HttpclientStudent>>(
                                                            JsonConvert.DeserializeObject<string>(resultEmpAddJson)
                                                        );
                    }
                }

            }
            catch (Exception ex)
            {
                resultantObject.Response_Message = ex.Message;
                //resultantObject = new HttpClientStudent()
                //{
                //    ErrorDetails = new HttpClientError()
                //    {
                //        Message = ex.Message + Environment.NewLine + ex.InnerException.Message + Environment.NewLine + ex.StackTrace
                //    }
                //};
            }
            //if (resultantObject.ErrorDetails != null)
            //{
            //    var errorObject = resultantObject.ErrorDetails;
            //    resultantObject.ErrorDetails.Message = errorObject.ErrorCode + " : " + errorObject.StatusCode + " : " + errorObject.Message;
            //}
            var json = new { resultantObject };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteStudent(string id)
        {
            //For handling error- "Could not establish trust relationship for SSL/TLS secure channel"
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            Result<HttpclientStudent> resultantObject = new Result<HttpclientStudent>();
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage APIResponse = new HttpResponseMessage();

                var url = baseUrl + "Delete_student?id=" + id;

                APIResponse = httpClient.DeleteAsync(url).GetAwaiter().GetResult();

                var resultEmpsJson = APIResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                resultantObject = JsonConvert.DeserializeObject<Result<HttpclientStudent>>(
                                    JsonConvert.DeserializeObject<string>(resultEmpsJson)
                                  );
            }
            catch (Exception ex)
            {
                resultantObject.Response_Message = ex.Message;
            }
            var json = new { resultantObject };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }

    public class Result<T>
    {
        public string Response_Message { get; set; }
        public string Status_Code { get; set; }
        public T Data { get; set; }
    }
    public class HttpclientStudent
    {
        public string id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public int age { get; set; }

        [Required]
        public string subject { get; set; }
    }
}