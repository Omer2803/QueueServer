using Customers.BL;
using CustomerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using model = Customers.Common.Models;

namespace CustomerService.Controllers
{
    [RoutePrefix("Customers")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomersController : ApiController
    {
        private readonly QueueCustomersManager _queueCustomersManager;
        public CustomersController()
        {
            _queueCustomersManager = new QueueCustomersManager();
        }

        [Route("GetWaitingList")]
        [HttpGet]
        public IHttpActionResult GetWaitingList()
        {
            try
            {
                IEnumerable<model.Customer> customers = _queueCustomersManager.GetWaitingList();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("InsertToQueue")]
        [HttpPost]
        public IHttpActionResult InsertToQueue([FromBody] string name)
        {
            try
            {
                model.Customer newCustomer = _queueCustomersManager.InsertToQueue(name);
                return Ok(newCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("CallNext")]
        [HttpPost]
        public IHttpActionResult CallNext([FromBody] QueueCustomersResponse queueCustomersResponse)
        {
            try
            {
                model.Customer nextCustomer = _queueCustomersManager.
                    CallNext(queueCustomersResponse.CustomerInService, queueCustomersResponse.NextCustomer);
                return Ok(nextCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("GetCustomerInService")]
        [HttpGet]
        public IHttpActionResult GetCustomerInService()
        {
            try
            {
                model.Customer currentCustomer = _queueCustomersManager.GetCustomerInService();
                return Ok(currentCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
