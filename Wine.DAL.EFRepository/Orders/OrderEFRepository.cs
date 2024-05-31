using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.IDAL.IRepository.Orders;
using Wine.Infa.EFModel.Models;
using Wine.Infa.Entity.Orders;

namespace Wine.DAL.EFRepository.Orders
{
    public class OrderEFRepository : IOrderRepository
    {
        private AppDbContext _db;
        public OrderEFRepository(AppDbContext db)
        {
            _db = db;
        }

        public void AddOrderItem(OrderItemEntity entity)
        {
            OrderItem orderItem = new OrderItem
            {
                Id = entity.Id,
                ProductsId = entity.ProductsId,
                ProductCount = entity.ProductCount,
                ProductsPrice = entity.ProductsPrice,
                TotalMoney = entity.TotalMoney,
                OrderId = entity.OrderId
            };

            _db.Add(orderItem);
            _db.SaveChanges();
        }

        public int CreateOrder(OrderEntity entity)
        {
            Order newOrder = new Order
            {
                Id = entity.Id,
                MemberId = entity.Id,
                OrderDate = entity.OrderDate,
                StateId = 5, //購買中
                TotalMoney = entity.TotalMoney
            };

            _db.Add(newOrder);
            _db.SaveChanges();

            return newOrder.Id;
        }

        public void DeleteOrderItem(int productId)
        {
            var item = _db.OrderItems
                .AsNoTracking()
                .Where(x => x.ProductsId == productId)
                .FirstOrDefault();

            if (item != null)
            {
                _db.Remove(item);
                _db.SaveChanges();
            }
        }

        public OrderEntity GetOrder(int memberId)
        {
            var memberOrder = _db.Orders
                .AsNoTracking()
                .Include(x => x.State)
                .Where(x => x.MemberId == memberId && x.State.StateName == "處理中")
                .Select(x => new OrderEntity
                {
                    Id = x.Id,
                    MemberId = x.MemberId,
                    Status = x.State.StateName,
                    OrderDate = x.OrderDate,
                    TotalMoney = x.TotalMoney
                })
                .FirstOrDefault();

            return memberOrder;
        }

        public OrderItemEntity GetOrderItemByProductId(int ProductId)
        {
            var item = _db.OrderItems
                .AsNoTracking()
                .Where(x => x.ProductsId == ProductId)
                .Select(x => new OrderItemEntity
                {
                    Id = x.Id,
                    ProductsId = x.ProductsId,
                    ProductCount = x.ProductCount,
                    ProductsPrice = x.ProductsPrice,
                    TotalMoney = x.TotalMoney,
                    OrderId = x.OrderId
                })
                .FirstOrDefault();

            return item;
        }

        public List<OrderItemEntity> GetOrderItems(int orderId)
        {
            var items = _db.OrderItems
                .AsNoTracking()
                .Where(x => x.OrderId == orderId)
                .Select(x => new OrderItemEntity
                {
                    Id = x.Id,
                    ProductsId = x.ProductsId,
                    ProductCount = x.ProductCount,
                    ProductsPrice = x.ProductsPrice,
                    TotalMoney = x.TotalMoney,
                    OrderId = x.OrderId
                })
                .ToList();

            return items;
        }

        public void UpdateOrderItemCount(int orderItemId, int count)
        {
            var item = _db.OrderItems
                .AsNoTracking()
                .Include(x => x.Products)
                .Where(x => x.Id == orderItemId)
                .FirstOrDefault();

            if (item != null)
            {
                item.ProductCount = count;
                item.TotalMoney = item.ProductsPrice * count;
                _db.SaveChanges();
            }
        }
    }
}
