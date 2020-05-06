using System;
using System.Collections.Generic;
using System.Text;
using ZinCorp.BE.Enums;

namespace ZinCorp.BE
{
    public class Customer
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
