using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine.Infa.Entity.Orders
{
    public class OrderEntity
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public DateOnly OrderDate { get; set; }

        public decimal TotalMoney { get; set; }

        public string Status { get; set; }
    }
}
