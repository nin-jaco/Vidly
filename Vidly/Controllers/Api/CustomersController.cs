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
        public IEnumerable<CustomerDto> Get()
        {
            return Context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        // GET api/customers/5
        public CustomerDto Get(int id)
        {
            var customer = Context.Customers.FirstOrDefault(p => p.Id == id);
            if (customer == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Mapper.Map<Customer, CustomerDto>(customer);
        }

        // POST api/customers
        [HttpPost]
        public CustomerDto Post(CustomerDto customerDto)
        {
            if(!ModelState.IsValid) throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            Context.Customers.Add(customer);
            Context.SaveChanges();

            customerDto.Id = customer.Id;
            return customerDto;
        }

        // PUT api/customers/5
        [HttpPut]
        public void Put(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid) throw new HttpResponseException(HttpStatusCode.BadRequest);
            var custDb = Context.Customers.FirstOrDefault(p => p.Id == id);
            if(custDb == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, custDb);
            Context.SaveChanges();
            //return customer;
        }

        // DELETE api/customers/5
        [HttpDelete]
        public void Delete(int id)
        {
            var custDb = Context.Customers.FirstOrDefault(p => p.Id == id);
            if (custDb == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            Context.Customers.Remove(custDb);
            Context.SaveChanges();
        }
    }
}