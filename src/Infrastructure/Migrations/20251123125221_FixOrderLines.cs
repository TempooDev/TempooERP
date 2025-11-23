using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TempooERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderLines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Orders_OrderId1",
                schema: "sales",
                table: "OrderLines");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId1",
                schema: "sales",
                table: "OrderLines",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Orders_OrderId1",
                schema: "sales",
                table: "OrderLines",
                column: "OrderId1",
                principalSchema: "sales",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Orders_OrderId1",
                schema: "sales",
                table: "OrderLines");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId1",
                schema: "sales",
                table: "OrderLines",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Orders_OrderId1",
                schema: "sales",
                table: "OrderLines",
                column: "OrderId1",
                principalSchema: "sales",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
