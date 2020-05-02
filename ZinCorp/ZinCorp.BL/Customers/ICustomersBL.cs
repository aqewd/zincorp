using System.Collections.Generic;
using ZinCorp.DAL.Models;

namespace ZinCorp.BL.Customers
{
    public interface ICustomersBL
    {
        List<DbCustomer> GetCustomers();
    }
}
