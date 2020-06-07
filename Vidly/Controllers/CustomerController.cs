using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;

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
            var customer = Context.Customers.FirstOrDefault(p => p.Id == id);
            if (customer == null) return HttpNotFound();

            return View(customer);
        }
    }
}