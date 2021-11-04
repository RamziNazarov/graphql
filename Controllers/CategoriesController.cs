using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLTest.Context;
using GraphQLTest.DTOs.Category;
using GraphQLTest.DTOs.Product;
using GraphQLTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraphQLTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CategoriesController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductsIncludeCategoryResponse>> GetAll()
        {
            return await _context.Categories.Include(x=>x.Products).Select(x=> new ProductsIncludeCategoryResponse
            {
                Id = x.Id,
                Name = x.Name,
                Products = x.Products.Select(p=> new ProductResponse
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList()
            }).ToListAsync();
        }
    }
}