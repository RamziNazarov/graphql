using System.Linq;
using GraphQLTest.Context;
using GraphQLTest.DTOs;
using GraphQLTest.Models;
using HotChocolate.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace GraphQLTest.GQL
{
    public class Query
    {
        private readonly DatabaseContext _context;

        public Query(DatabaseContext context)
        {
            _context = context;
        }

        [UsePaging(IncludeTotalCount = true, MaxPageSize = 100, DefaultPageSize = 10)]
        [UseFiltering(FilterType = typeof(Category))]
        [UseSorting(SortType = typeof(Category))]
        public IQueryable<Category> Categories => _context.Categories.Include(x=>x.Products);
        
        
        
        
        
        
        public IQueryable<Product> Products => _context.Products.Include(x=>x.Category);

        public PagedResponse<IQueryable<Category>> PagedCategories(int? pageNumber,int? pageSize)
        {
            var categories = _context.Categories.OrderBy(x=>x.Id).Skip(((pageNumber ?? 1) - 1) * (pageSize ?? 10)).Take(pageSize ?? 10).Include(x=>x.Products);
            return new PagedResponse<IQueryable<Category>>
            {
                Data = categories,
                TotalCount = _context.Categories.Count()
            };
        }
    }
}