using System.Linq;
using GraphQLTest.Context;
using GraphQLTest.Models;

namespace GraphQLTest.Services
{
    public class CategoryService
    {
        private readonly DatabaseContext _context;

        public CategoryService(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories;
        }
    }
}