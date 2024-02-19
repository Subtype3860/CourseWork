using BlogDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogDAL.Configs
{
    internal class PostTagConfig : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
            builder.HasKey(x => new { x.PostId, x.TagId });
            builder
                .HasOne(x => x.Post)
                .WithMany(x => x.PostTags)
                .HasForeignKey(x => x.PostId);
            builder
                .HasOne(x => x.Tag)
                .WithMany(x=>x.PostTags)
                .HasForeignKey(x=>x.TagId);
        }
    }
}
