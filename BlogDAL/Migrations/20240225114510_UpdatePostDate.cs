using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogDAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commentaries");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePublic",
                table: "Post",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Remark",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    PostId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Remark_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Remark_PostId",
                table: "Remark",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Remark");

            migrationBuilder.DropColumn(
                name: "DatePublic",
                table: "Post");

            migrationBuilder.CreateTable(
                name: "Commentaries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PostId = table.Column<string>(type: "TEXT", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commentaries_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commentaries_PostId",
                table: "Commentaries",
                column: "PostId");
        }
    }
}
