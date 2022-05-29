using ApiPerformanceCompare.DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPerformanceCompare.DataModel.Configuration
{
    public class PostConfuguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.PostId);
            builder.HasOne(p => p.Blog).WithMany(b => b.Posts);
        }
    }
}
