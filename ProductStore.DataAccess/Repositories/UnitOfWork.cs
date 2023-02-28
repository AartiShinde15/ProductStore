using ProductStore.DataAccess.Repositories;
using ProductStore.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public IProductRepository Product { get; private set; }

        public ICategoryRepository Category { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Product = new ProductRepository(context);
            Category = new CategoryRepository(context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
