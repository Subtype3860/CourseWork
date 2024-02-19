using BlogDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogDAL.Configs;

public class CommentariesConfig : IEntityTypeConfiguration<Commentaries>
{
    public void Configure(EntityTypeBuilder<Commentaries> builder)
    {
        builder.HasKey(x => x.Id);
    }
}