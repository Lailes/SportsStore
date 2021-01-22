using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStore.Migrations
{
    public partial class Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "ProductID",
                "Products",
                "ProductId");

            migrationBuilder.CreateTable(
                "Orders",
                table => new
                {
                    OrderID = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(max)", nullable: false),
                    Line1 = table.Column<string>("nvarchar(max)", nullable: false),
                    Line2 = table.Column<string>("nvarchar(max)", nullable: true),
                    Line3 = table.Column<string>("nvarchar(max)", nullable: true),
                    City = table.Column<string>("nvarchar(max)", nullable: false),
                    State = table.Column<string>("nvarchar(max)", nullable: false),
                    Zip = table.Column<string>("nvarchar(max)", nullable: true),
                    Country = table.Column<string>("nvarchar(max)", nullable: false),
                    GiftWrap = table.Column<bool>("bit", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Orders", x => x.OrderID); });

            migrationBuilder.CreateTable(
                "CartLine",
                table => new
                {
                    CartLineId = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>("int", nullable: true),
                    Quantity = table.Column<int>("int", nullable: false),
                    OrderID = table.Column<int>("int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartLine", x => x.CartLineId);
                    table.ForeignKey(
                        "FK_CartLine_Orders_OrderID",
                        x => x.OrderID,
                        "Orders",
                        "OrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_CartLine_Products_ProductId",
                        x => x.ProductId,
                        "Products",
                        "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_CartLine_OrderID",
                "CartLine",
                "OrderID");

            migrationBuilder.CreateIndex(
                "IX_CartLine_ProductId",
                "CartLine",
                "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "CartLine");

            migrationBuilder.DropTable(
                "Orders");

            migrationBuilder.RenameColumn(
                "ProductId",
                "Products",
                "ProductID");
        }
    }
}