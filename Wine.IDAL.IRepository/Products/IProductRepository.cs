using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.Infa.Criteria.Products;
using Wine.Infa.Entity.Products;

namespace Wine.IDAL.IRepository.Products
{
    public interface IProductRepository
    {
        List<ProductEntity> GetAllProducts();
        List<ProductSearchCriteria> SearchProducts(ProductSearchCriteria criteria);
        ProductEntity GetProductById(int id);
    }
}
