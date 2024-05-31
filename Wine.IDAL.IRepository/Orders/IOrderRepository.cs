using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.Infa.Entity.Orders;

namespace Wine.IDAL.IRepository.Orders
{
    public interface IOrderRepository
    {
        List<OrderItemEntity> GetOrderItems(int orderId);
        OrderEntity GetOrder(int memberId);
        OrderItemEntity GetOrderItemByProductId(int ProductId);
        int CreateOrder(OrderEntity entity);
        void AddOrderItem(OrderItemEntity entity);
        void DeleteOrderItem(int productId);
        void UpdateOrderItemCount(int orderItemId,int count);
    }
}
