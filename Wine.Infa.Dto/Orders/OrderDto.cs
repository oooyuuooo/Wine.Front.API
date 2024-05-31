using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine.Infa.Dto.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public DateOnly OrderDate { get; set; }

        public decimal TotalMoney { get; set; }

        public string Status { get; set; }
    }
}
