using BlogDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogDAL.Configs;

public class RemarkConfig : IEntityTypeConfiguration<Remark>
{
    public void Configure(EntityTypeBuilder<Remark> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User)
            .WithMany(x => x.Remarks)
            .HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Post)
            .WithMany(x => x.Remarks)
            .HasForeignKey(x => x.PostId);
    }
}