using ApiPerformanceCompare.DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPerformanceCompare.DataModel.Configuration
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(b => b.BlogId);
            builder.HasMany(b => b.Posts).WithOne(p => p.Blog);
        }
    }
}
