using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext Context { get; } = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            Context.Dispose();
        }
        
        //private List<Customer> GetCustomers() 
        //{
        //    return new List<Customer>
        //    {
        //        new Customer {Id = 1, Name = "John Smith"},
        //        new Customer {Id = 2, Name = "Mary Williams"}
        //    };
        //}

        // GET: Customer
        public ActionResult Index()
        {
            var customers = Context.Customers.Include(p => p.MembershipType).ToList();
            
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = Context.Customers.Include(p => p.MembershipType).FirstOrDefault(p => p.Id == id);
            if (customer == null) return HttpNotFound();

            return View(customer);
        }

        public ActionResult New()
        {
            var membershipTypes = Context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel {MembershipTypes = membershipTypes};

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = Context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            if (customer.Id == 0)
            {
                Context.Customers.Add(customer);
            }
            else
            {
                var dbCust = Context.Customers.FirstOrDefault(p => p.Id == customer.Id);
                //TryUpdateModel(dbCust);
                //automapper
                dbCust.Name = customer.Name;
                dbCust.DateOfBirth = customer.DateOfBirth;
                dbCust.MembershipTypeId = customer.MembershipTypeId;
                dbCust.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            Context.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Edit(int id)
        {
            var customer = Context.Customers.FirstOrDefault(p => p.Id == id);
            if (customer == null) return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = Context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);

        }
    }
}