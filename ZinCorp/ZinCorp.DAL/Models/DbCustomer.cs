using System.ComponentModel.DataAnnotations.Schema;
using ZinCorp.BE.Enums;

namespace ZinCorp.DAL.Models
{
    [Table("Customers")]
    public class DbCustomer
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public string HashPassword { get; set; }
        public int Salt { get; set; }
        public Roles Role { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
