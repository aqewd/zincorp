using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ZinCorp.BL.Customers;
using ZinCorp.DAL.Models;

namespace ZinCorp.WebAPI.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public ICustomersBL _customerBL;

        public CustomersController(ICustomersBL customerBL)
        {
            _customerBL = customerBL;
        }

        [HttpGet]
        public List<DbCustomer> Get()
        {
            return _customerBL.GetCustomers();
        }
    }
}
