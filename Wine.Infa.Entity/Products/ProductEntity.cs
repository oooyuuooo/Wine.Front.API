using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine.Infa.Entity.Products
{
    public class ProductEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Year { get; set; }

        public string Category { get; set; }

        public string Origin { get; set; }

        public string Capacity { get; set; }

        public string Taste { get; set; }

        public decimal Price { get; set; }

        public string ImageLink { get; set; }

        public int Count { get; set; }
    }
}
