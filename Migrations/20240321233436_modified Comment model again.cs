using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumProject.Migrations
{
    /// <inheritdoc />
    public partial class modifiedCommentmodelagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentAuthor",
                table: "PostComment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentAuthor",
                table: "PostComment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
