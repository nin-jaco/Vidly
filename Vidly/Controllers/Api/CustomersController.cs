using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext Context { get; } = new ApplicationDbContext();
        
        // GET api/customers
        public IHttpActionResult Get()
        {
            var customerDtos = Context.Customers
                .Include("MembershipType")
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customerDtos);
        }

        // GET api/customers/5
        public IHttpActionResult Get(int id)
        {
            var customer = Context.Customers.FirstOrDefault(p => p.Id == id);
            if (customer == null) return NotFound();
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST api/customers
        [HttpPost]
        public IHttpActionResult Post(CustomerDto customerDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            Context.Customers.Add(customer);
            Context.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        // PUT api/customers/5
        [HttpPut]
        public IHttpActionResult Put(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var custDb = Context.Customers.FirstOrDefault(p => p.Id == id);
            if(custDb == null) return NotFound();

            Mapper.Map(customerDto, custDb);
            Context.SaveChanges();
            //return customer;
            return Ok(customerDto);
        }

        // DELETE api/customers/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var custDb = Context.Customers.FirstOrDefault(p => p.Id == id);
            if (custDb == null) return NotFound();
            Context.Customers.Remove(custDb);
            Context.SaveChanges();
            return Ok(Mapper.Map<Customer, CustomerDto>(custDb));
        }
    }
}