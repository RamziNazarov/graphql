using System.Collections.Generic;
using GraphQLTest.DTOs.Product;

namespace GraphQLTest.DTOs.Category
{
    public class ProductsIncludeCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductResponse> Products { get; set; }
    }
}