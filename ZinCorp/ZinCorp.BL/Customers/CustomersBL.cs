using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using ZinCorp.BE;
using ZinCorp.BE.Enums;
using ZinCorp.Common;
using ZinCorp.DAL;
using ZinCorp.DAL.Models;

namespace ZinCorp.BL.Customers
{
    public class CustomersBL : ICustomersBL
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public CustomersBL(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<DbCustomer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer CreateCustomer(Customer customer)
        {
            var salt = Password.CreateRandomSalt();
            var pwd = new Password(customer.Password, salt);

            var password = pwd.ComputeSaltedHash();
            var newCustomer = new DbCustomer
            {
                FirstName = customer.FirstName,
                Address = customer.Address,
                LastName = customer.LastName,
                Email = customer.Email,
                Login = customer.Login,
                PhoneNumber = customer.PhoneNumber,
                Role = Roles.Admin,
                Salt = salt,
                HashPassword = password
            };

            _context.Customers.Add(newCustomer);
            _context.SaveChanges();
            
            return customer;
        }

        public Customer Auth(string userName, string password)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Email == userName);

            if (customer == null) return null;

            var pwd = new Password(password, customer.Salt);

            return customer.HashPassword == pwd.ComputeSaltedHash() ? _mapper.Map<Customer>(customer) : null;
        }
    }
}
