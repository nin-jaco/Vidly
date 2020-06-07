using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        public List<Customer> Customers { get; set; } = new List<Customer>
        {
            new Customer {Id = 1, Name = "John Smith"},
            new Customer {Id = 2, Name = "Mary Williams"}
        };

        // GET: Customer
        public ActionResult Index()
        {
            return View(Customers);
        }

        public ActionResult Details(int id)
        {
            return Content(Customers.FirstOrDefault(p => p.Id == id)?.Name);
        }
    }
}