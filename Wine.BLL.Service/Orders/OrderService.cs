using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.BLL.Service.Products;
using Wine.DAL.EFRepository.Products;
using Wine.IDAL.IRepository.Orders;
using Wine.IDAL.IRepository.Products;
using Wine.Infa.Dto.Orders;
using Wine.Infa.Dto.Products;
using Wine.Infa.EFModel.Models;
using Wine.Infa.Entity.Orders;

namespace Wine.BLL.Service.Orders
{
    public class OrderService
    {
        private readonly IOrderRepository _repo;
        private readonly IProductRepository _productRepo;
        private AppDbContext context;
        public OrderService(IOrderRepository repo, AppDbContext _context)
        {
            _repo = repo;
            context = _context;
            _productRepo = new ProductEFRepository(context);
        }

        public List<OrderItemDto> GetOrder(int memberId)
        {
            var order = _repo.GetOrder(memberId);
            var newOrder = new OrderEntity
            {
                MemberId = memberId,
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                TotalMoney = 0,
                Status = "購買中"
            };

            //假如目前帳號沒有訂單
            if (order == null)
            {
                var id = _repo.CreateOrder(newOrder);
                return GetOrderItems(id);
            }
            else
            {
                return GetOrderItems(order.Id);
            }
        }

        private List<OrderItemDto> GetOrderItems(int orderId)
        {
            var items = _repo.GetOrderItems(orderId);

            List<OrderItemDto> dto = items.Select(x => new OrderItemDto
            {
                Id = x.Id,
                ProductsId = x.ProductsId,
                ProductCount = x.ProductCount,
                ProductsPrice = x.ProductsPrice,
                TotalMoney = x.TotalMoney,
                OrderId = x.OrderId,
                ProductInfo = GetProductInfo(x.ProductsId)
            }).ToList();

            return dto;
        }

        //獲取訂單每一件商品訊息
        private ProductDto GetProductInfo(int productId)
        {
            ProductService productService = new ProductService(_productRepo);
            var product = productService.GetProductById(productId);
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Year = product.Year,
                Category = product.Category,
                Capacity = product.Capacity,
                Origin = product.Origin,
                Taste = product.Taste,
                Price = product.Price,
                ImageLink = product.ImageLink,
                Count = product.Count
            };
        }

        //更新購物車
        public string UpdateCartItem(int orderId, int productId, int count)
        {
            var isProductExist = _repo.GetOrderItemByProductId(productId);

            if (isProductExist != null)
            {
                //如果產品已存在，直接更新數量
                _repo.UpdateOrderItemCount(isProductExist.Id, count);

                //當產品數量為0，刪除
                if (isProductExist.ProductCount == 0)
                {
                    _repo.DeleteOrderItem(productId);

                    return "商品已刪除";
                }

                return "商品數量已更新";
            }
            else
            {
                //如果產品不存在，新增
                var result = AddtoCart(orderId, productId, count);
                return result;
            }
        }

        private string AddtoCart(int orderId, int productId, int count)
        {
            var product = GetProductInfo(productId);
            OrderItemEntity entity = new OrderItemEntity
            {
                ProductsId = productId,
                ProductsPrice = product.Price,
                ProductCount = count,
                TotalMoney = product.Price * count,
                OrderId = orderId
            };
            _repo.AddOrderItem(entity);

            return "商品成功加入購物車";
        }

        public string DeleteOrderItem(int productId)
        {
            _repo.DeleteOrderItem(productId);
            return "商品已刪除";
        }
    }
}
