using ProductStore.DataAccess.Data;
using ProductStore.Models;

namespace ProductStore.DataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            var productDb = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (productDb != null)
            {
                productDb.Name = product.Name;
                productDb.Price = product.Price;
                productDb.Rating = product.Rating;
                productDb.CategoryId= product.CategoryId;
                if (product.ImageUrl != null)
                {
                    productDb.ImageUrl = product.ImageUrl;
                }
            }

        }
    }
}
