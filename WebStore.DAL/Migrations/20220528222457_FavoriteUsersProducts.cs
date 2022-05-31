using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.DAL.Migrations
{
    public partial class FavoriteUsersProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductUser",
                columns: table => new
                {
                    FavoriteProductsId = table.Column<int>(type: "int", nullable: false),
                    UsersMarkedFavoriteId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUser", x => new { x.FavoriteProductsId, x.UsersMarkedFavoriteId });
                    table.ForeignKey(
                        name: "FK_ProductUser_AspNetUsers_UsersMarkedFavoriteId",
                        column: x => x.UsersMarkedFavoriteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductUser_Products_FavoriteProductsId",
                        column: x => x.FavoriteProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductUser_UsersMarkedFavoriteId",
                table: "ProductUser",
                column: "UsersMarkedFavoriteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductUser");
        }
    }
}
