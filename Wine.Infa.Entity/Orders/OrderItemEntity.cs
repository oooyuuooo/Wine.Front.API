using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine.Infa.Entity.Orders
{
    public class OrderItemEntity
    {
        public int Id { get; set; }

        public int ProductsId { get; set; }

        public int ProductCount { get; set; }

        public decimal ProductsPrice { get; set; }

        public decimal TotalMoney { get; set; }

        public int OrderId { get; set; }
    }
}
