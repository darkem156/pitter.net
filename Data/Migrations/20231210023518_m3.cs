using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pitter.Data.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "repitts_AspNetUsers_Id_fk",
                schema: "content",
                table: "repitts",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "repitts_AspNetUsers_Id_fk",
                schema: "content",
                table: "repitts");
        }
    }
}
