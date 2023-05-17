using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserTokensFixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserToken",
                table: "UserTokens",
                newName: "Token");

            migrationBuilder.RenameIndex(
                name: "IX_UserTokens_UserToken",
                table: "UserTokens",
                newName: "IX_UserTokens_Token");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "UserTokens",
                newName: "UserToken");

            migrationBuilder.RenameIndex(
                name: "IX_UserTokens_Token",
                table: "UserTokens",
                newName: "IX_UserTokens_UserToken");
        }
    }
}
