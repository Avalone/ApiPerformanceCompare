using ApiPerformanceCompare.DataModel;
using ApiPerformanceCompare.DataModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiPerformanceCompare.GraphQL.GraphQL
{
    public class Query
    {
        public IQueryable<Post> GetPosts([Service] DataContext dataContext, int blogId) =>
            dataContext.Posts
                .AsNoTracking()
                .Include(p => p.Blog)
                .Where(p => p.Blog.BlogId == blogId)
                .AsQueryable();
    }
}
