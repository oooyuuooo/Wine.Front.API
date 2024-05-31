using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.IDAL.IRepository.Products;
using Wine.Infa.Criteria.Products;
using Wine.Infa.EFModel.Models;
using Wine.Infa.Entity.Products;

namespace Wine.DAL.EFRepository.Products
{
    public class ProductEFRepository : IProductRepository
    {
        private AppDbContext _db;

        public ProductEFRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<ProductEntity> GetAllProducts()
        {
            var products = _db.Products
                .AsNoTracking()
                .Include(x => x.Cap)
                .Include(x => x.Cat)
                .Include(x => x.Orig)
                .Include(x => x.Tastes)
                .Select(x => new ProductEntity
                {
                    Id = x.Id,
                    Name = x.Name,
                    Year = x.Year,
                    Capacity = x.Cap.Capacity1,
                    Category = x.Cat.Category1,
                    Origin = x.Orig.Origin1,
                    Taste = x.Tastes.Taste1,
                    Price = x.Price,
                    ImageLink = x.ImageLink,
                    Count = x.Count
                }).ToList();

            return products;
        }

        public ProductEntity GetProductById(int id)
        {
            var product = _db.Products
                .AsNoTracking()
                .Include(x => x.Cap)
                .Include(x => x.Cat)
                .Include(x => x.Orig)
                .Include(x => x.Tastes)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (product == null)
            {
                return null;
            }

            var entity = new ProductEntity
            {
                Id = product.Id,
                Name = product.Name,
                Year = product.Year,
                Capacity = product.Cap.Capacity1,
                Category = product.Cat.Category1,
                Origin = product.Orig.Origin1,
                Taste = product.Tastes.Taste1,
                Price = product.Price,
                ImageLink = product.ImageLink,
                Count = product.Count
            };

            return entity;
        }

        public List<ProductSearchCriteria> SearchProducts(ProductSearchCriteria criteria)
        {
            var query = _db.Products
                .AsNoTracking()
                .Include(x => x.Cap)
                .Include(x => x.Cat)
                .Include(x => x.Orig)
                .Include(x => x.Tastes)
                .Select(x => new ProductSearchCriteria
                {
                    Id = x.Id,
                    Name = x.Name,
                    Year = x.Year,
                    Capacity = x.Cap.Capacity1,
                    Category = x.Cat.Category1,
                    Origin = x.Orig.Origin1,
                    Taste = x.Tastes.Taste1,
                    Price = x.Price,
                    ImageLink = x.ImageLink,
                });

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(x => x.Name.Contains(criteria.Name));
            }

            if (!string.IsNullOrEmpty(criteria.Origin))
            {
                query = query.Where(x => x.Origin.Contains(criteria.Origin));
            }

            if (!string.IsNullOrEmpty(criteria.Category))
            {
                query = query.Where(x => x.Category.Contains(criteria.Category));
            }

            if (!string.IsNullOrEmpty(criteria.Taste))
            {
                query = query.Where(x => x.Taste.Contains(criteria.Taste));
            }

            if (!string.IsNullOrEmpty(criteria.Capacity))
            {
                query = query.Where(x => x.Capacity.Contains(criteria.Capacity));
            }

            if (!string.IsNullOrEmpty(criteria.Year))
            {
                query = query.Where(x => x.Year.Contains(criteria.Year));
            }

            return query.ToList();
        }
    }
}
