using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.Infa.Dto.Products;

namespace Wine.Infa.Dto.Orders
{
    public class OrderItemDto
    {
        public int Id { get; set; }

        public int ProductsId { get; set; }

        public int ProductCount { get; set; }

        public decimal ProductsPrice { get; set; }

        public decimal TotalMoney { get; set; }

        public int OrderId { get; set; }

        public ProductDto ProductInfo { get; set; }
    }
}
