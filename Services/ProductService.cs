using System.Linq;
using GraphQLTest.Context;
using GraphQLTest.Models;

namespace GraphQLTest.Services
{
    public class ProductService
    {
        private readonly DatabaseContext _context;

        public ProductService(DatabaseContext context)
        {
            _context = context;
        }
        
        public IQueryable<Product> GetAll()
        {
            return _context.Products;
        }
    }
}