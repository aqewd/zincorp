using System.ComponentModel.DataAnnotations.Schema;

namespace ZinCorp.DAL.Models
{
    [Table("ProductImages")]
    public class DbProductImage
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Base64 { get; set; }

        public long ProductId { get; set; }

        public DbProduct Product { get; set; }
    }
}
