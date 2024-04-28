using Learn_mvc.Core.Models;
using Learn_mvc.Data.Repositories;
using Learn_mvc.Data.Repository;
using Learn_mvc.Logic;
using Learn_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Controllers
{
    public class GenericRepoAndUOFPatternController : Controller
    {
        public GenericRepoAndUOFPatternController()
        {
        }

        public ActionResult Index()
        {
            var customers = new RepoPatternViewRepository().GetCustomers();
            return View(customers);
        }

        public ActionResult Index2()
        {
            return RedirectToAction("GetAllCustomers");
        }

        public ActionResult GetAllCustomers()
        {
            List<CustomerModel> customers = null;
            using (IUnitOfWork _unitOfWork = new UnitOfWork())
            {
                customers = _unitOfWork.Customers.GetAllCustomers();
            }
            return View(customers);
        }

        public ActionResult AddCustomer()
        {
            CustomerModel model = new CustomerModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer(CustomerModel model)
        {
            using (IUnitOfWork _unitOfWork = new UnitOfWork())
            {
                var isCustomerAdded = _unitOfWork.Customers.AddCustomer(model);
                if (isCustomerAdded)
                {
                    _unitOfWork.SaveChanges();
                    return RedirectToAction("GetAllCustomers");
                }
            }
            return View(model);
        }

        public ActionResult UpdateCustomer(int id)
        {
            CustomerModel customer = null;
            using (IUnitOfWork _unitOfWork = new UnitOfWork())
            {
                customer = _unitOfWork.Customers.GetCustomer(id);
            }
                
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCustomer(CustomerModel model)
        {
            using (IUnitOfWork _unitOfWork = new UnitOfWork()) 
            {
                var isCustomerUpdated = _unitOfWork.Customers.UpdateCustomer(model);
                if (isCustomerUpdated)
                {
                    _unitOfWork.SaveChanges();
                    return RedirectToAction("GetAllCustomers");
                }
            }
                
            return View(model);
        }

        public ActionResult DeleteCustomer(int id)
        {
            CustomerModel customer = null;
            using (IUnitOfWork _unitOfWork = new UnitOfWork())
            {
                customer = _unitOfWork.Customers.GetCustomer(id);
            }
                
            return View(customer);
        }

        [HttpPost, ActionName("DeleteCustomer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomerConfirm(int id)
        {
            using (IUnitOfWork _unitOfWork = new UnitOfWork())
            {
                var isCustomerDeleted = _unitOfWork.Customers.DeleteCustomer(id);
                if (isCustomerDeleted)
                {
                    _unitOfWork.SaveChanges();
                    return RedirectToAction("GetAllCustomers");
                }
            }
                
            return View();
        }
    }
}