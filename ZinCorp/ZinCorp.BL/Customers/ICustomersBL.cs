using System.Collections.Generic;
using ZinCorp.BE;
using ZinCorp.DAL.Models;

namespace ZinCorp.BL.Customers
{
    public interface ICustomersBL
    {
        List<DbCustomer> GetCustomers();

        Customer CreateCustomer(Customer customer);

        Customer Auth(string userName, string password);
    }
}
