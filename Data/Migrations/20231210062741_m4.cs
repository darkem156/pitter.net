using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pitter.Data.Migrations
{
    /// <inheritdoc />
    public partial class m4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "Follows_pk",
                schema: "users",
                table: "Follows");

            migrationBuilder.DropIndex(
                name: "IX_Follows_ID_Follower",
                schema: "users",
                table: "Follows");

            migrationBuilder.AddPrimaryKey(
                name: "Follows_pk",
                schema: "users",
                table: "Follows",
                columns: new[] { "ID_Follower", "ID_Following" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "Follows_pk",
                schema: "users",
                table: "Follows");

            migrationBuilder.AddPrimaryKey(
                name: "Follows_pk",
                schema: "users",
                table: "Follows",
                columns: new[] { "ID_Following", "ID_Follower" });

            migrationBuilder.CreateIndex(
                name: "IX_Follows_ID_Follower",
                schema: "users",
                table: "Follows",
                column: "ID_Follower");
        }
    }
}
