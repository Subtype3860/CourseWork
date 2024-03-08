using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogDAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostLook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Look",
                table: "Post",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Look",
                table: "Post");
        }
    }
}
