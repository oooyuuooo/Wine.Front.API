using Microsoft.AspNetCore.Mvc;
using Wine.BLL.Service.Products;
using Wine.DAL.EFRepository.Products;
using Wine.IDAL.IRepository.Products;
using Wine.Infa.Criteria.Products;
using Wine.Infa.Dto.Products;
using Wine.Infa.EFModel.Models;

namespace Wine.Front.API.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repo;
        private readonly ProductService _service;

        public ProductsController(AppDbContext context)
        {
            _repo = new ProductEFRepository(context);
            _service = new ProductService(_repo);
        }

        [HttpGet("GetAllProducts")]
        public List<ProductDto> GetAllProducts()
        {
            return _service.GetAllProducts();
        }

        [HttpGet("SearchProducts")]
        public List<ProductSearchCriteria> SearchProducts([FromQuery] ProductSearchCriteria criteria)
        {
            return _service.SearchProducts(criteria);
        }

        [HttpGet("GetProductById")]
        public ProductDto GetProductById(int id)
        {
            return _service.GetProductById(id);
        }
    }
}
