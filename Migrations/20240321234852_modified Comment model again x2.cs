using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumProject.Migrations
{
    /// <inheritdoc />
    public partial class modifiedCommentmodelagainx2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentAuthor",
                table: "PostComment",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentAuthor",
                table: "PostComment");
        }
    }
}
