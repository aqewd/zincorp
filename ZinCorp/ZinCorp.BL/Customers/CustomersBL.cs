using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZinCorp.DAL;
using ZinCorp.DAL.Models;

namespace ZinCorp.BL.Customers
{
    public class CustomersBL : ICustomersBL
    {
        private DBContext _context;

        public CustomersBL(DBContext context)
        {
            _context = context;
        }

        public List<DbCustomer> GetCustomers()
        {
            return _context.Customers.ToList();
        }
    }
}
