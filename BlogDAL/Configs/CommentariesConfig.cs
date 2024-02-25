using BlogDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogDAL.Configs;

public class CommentariesConfig : IEntityTypeConfiguration<Remark>
{
    public void Configure(EntityTypeBuilder<Remark> builder)
    {
        builder.HasKey(x => x.Id);
    }
}