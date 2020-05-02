using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZinCorp.DAL.Models
{
    [Table("Products")]
    public class DbProduct
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Rate { get; set; }

        public int Count { get; set; }

        public ICollection<DbProductImage> ProductImages { get; set; } = new List<DbProductImage>();
    }
}
