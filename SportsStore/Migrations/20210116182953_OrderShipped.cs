using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStore.Migrations {
    public partial class OrderShipped : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<bool>(
                "Shipped",
                "Orders",
                "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                "Shipped",
                "Orders");
        }
    }
}