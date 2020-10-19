using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api_project.Models;
namespace api_project.Controllers
{
    public class CustomerController : ApiController
    {
        //Get-retrive data
        public IHttpActionResult GetAllCustomer()
        {
            IList<CustomerViewModel> customer = null;
            using (var x = new webapiEntities())
            {
                customer = x.Customers
                    .Select(c => new CustomerViewModel()
                    {
                        Id = c.id,
                        Name = c.name,
                        Email = c.email,
                        Address = c.address,
                        Phone = c.phone
                    }).ToList<CustomerViewModel>();


                if (customer.Count ==0)
                    return NotFound();
                return Ok(customer);
            }
        }
        //post-insert new record
        public IHttpActionResult PostNewCustomer(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invaliddata.please recheck");
            using (var x = new webapiEntities())
            {
                x.Customers.Add(new Customer()
                {
                    name = customer.Name,
                    email = customer.Email,
                    address = customer.Address,
                    phone = customer.Phone
                });
                x.SaveChanges();
            }

            return Ok();
        }
        //put-update database on id
        public IHttpActionResult PutCustomer(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("This is invalid model.please recheck");
            using (var x = new webapiEntities())
            {
                var checkExsitingCustomer = x.Customers.Where(c => c.id == customer.Id)
                    .FirstOrDefault<Customer>();
                if(checkExsitingCustomer!=null)
                {
                    checkExsitingCustomer.name = customer.Name;
                    checkExsitingCustomer.address = customer.Address;
                    checkExsitingCustomer.phone = customer.Phone;
                    x.SaveChanges();
                }
                else
                return NotFound();
            }

            return Ok();
        }
        //delete-delete a record base on id
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("please enter valid customer id");
            using (var x = new webapiEntities())
            {
                var customer = x.Customers
                    .Where(c => c.id == id).FirstOrDefault();
                x.Entry(customer).State = System.Data.Entity.EntityState.Deleted;
                x.SaveChanges();
            }
            return Ok();


        }

    }
}
