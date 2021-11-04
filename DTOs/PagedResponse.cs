namespace GraphQLTest.DTOs
{
    public class PagedResponse<T> 
    {
        public int TotalCount { get; set; }
        public T Data { get; set; }
    }
}