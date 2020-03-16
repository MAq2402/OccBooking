using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OccBooking.Persistence.Migrations
{
    public partial class RelationChangesToARs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuOrders_Menus_MenuId",
                table: "MenuOrders");

            migrationBuilder.DropIndex(
                name: "IX_MenuOrders_MenuId",
                table: "MenuOrders");

            migrationBuilder.AlterColumn<Guid>(
                name: "MenuId",
                table: "MenuOrders",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "MenuOrders",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "MenuOrders");

            migrationBuilder.AlterColumn<Guid>(
                name: "MenuId",
                table: "MenuOrders",
                nullable: true,
                oldClrType: typeof(Guid));

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
                onDelete: ReferentialAction.Restrict);
        }
    }
}
