using System;
using System.Collections.Generic;

namespace ZinCorp.BE
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Rate { get; set; }

        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }

    public class ProductImage
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Base64 { get; set; }
    }
}
