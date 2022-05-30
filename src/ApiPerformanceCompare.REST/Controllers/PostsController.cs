using ApiPerformanceCompare.DataModel;
using ApiPerformanceCompare.DataModel.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPerformanceCompare.REST.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly ILogger<PostsController> logger;
        private readonly DataContext context;

        public PostsController(ILogger<PostsController> logger, DataContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        [HttpGet("api/v1/posts/{blogId}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts(int blogId)
        {
            return Ok(
                await context.Posts
                    .AsNoTracking()
                    .Include(p => p.Blog)
                    .Where(p => p.Blog.BlogId == blogId)
                    .ToListAsync()
                    .ConfigureAwait(false)
                    );
        }
    }
}