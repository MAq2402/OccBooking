using Microsoft.EntityFrameworkCore.Migrations;

namespace OccBooking.Persistence.Migrations
{
    public partial class ForeginKeyForMenuInMenuOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MenuOrders_MenuId",
                table: "MenuOrders",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuOrders_Menus_MenuId",
                table: "MenuOrders",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuOrders_Menus_MenuId",
                table: "MenuOrders");

            migrationBuilder.DropIndex(
                name: "IX_MenuOrders_MenuId",
                table: "MenuOrders");
        }
    }
}
