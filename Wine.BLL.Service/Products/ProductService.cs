using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.IDAL.IRepository.Products;
using Wine.Infa.Criteria.Products;
using Wine.Infa.Dto.Products;

namespace Wine.BLL.Service.Products
{
    public class ProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public List<ProductDto> GetAllProducts()
        {
            var products = _repo.GetAllProducts();

            List<ProductDto> dto = products
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Year = x.Year,
                    Capacity = x.Capacity,
                    Category = x.Category,
                    Origin = x.Origin,
                    ImageLink = x.ImageLink,
                    Taste = x.Taste,
                    Price = x.Price,
                    Count = x.Count
                }).ToList();

            return dto;
        }

        public List<ProductSearchCriteria> SearchProducts(ProductSearchCriteria criteria)
        {
            var products = _repo.SearchProducts(criteria);
            return products;
        }

        public ProductDto GetProductById(int id)
        {
            var product = _repo.GetProductById(id);

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Year = product.Year,
                Capacity = product.Capacity,
                Category = product.Category,
                Origin = product.Origin,
                ImageLink = product.ImageLink,
                Taste = product.Taste,
                Price = product.Price,
                Count = product.Count
            };
        }
    }
}
