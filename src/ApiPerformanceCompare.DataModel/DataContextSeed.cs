using ApiPerformanceCompare.DataModel.Entities;
using Bogus;

namespace ApiPerformanceCompare.DataModel
{
    public static class DataContextSeed
    {
        public readonly static List<Blog> Blogs = new();
        public readonly static List<Post> Posts = new();

        public static void Init(int count)
        {
            var postId = 1;
            var postFaker = new Faker<Post>()
               .RuleFor(p => p.PostId, _ => postId++)
               .RuleFor(p => p.Title, f => f.Hacker.Phrase())
               .RuleFor(p => p.Content, f => f.Lorem.Sentence());

            var blogId = 1;
            var blogFaker = new Faker<Blog>()
               .RuleFor(b => b.BlogId, _ => blogId++)
               .RuleFor(b => b.Url, f => f.Internet.Url())
               .RuleFor(b => b.Posts, (f, b) =>
               {
                   postFaker.RuleFor(p => p.BlogId, _ => b.BlogId);
                   var posts = postFaker.GenerateBetween(3, 5);
                   DataContextSeed.Posts.AddRange(posts);
#pragma warning disable CS8603 // Possible null reference return.
                   return null;
#pragma warning restore CS8603 // Possible null reference return.
               });

            var blogs = blogFaker.Generate(count);
            DataContextSeed.Blogs.AddRange(blogs);
        }
    }
}
