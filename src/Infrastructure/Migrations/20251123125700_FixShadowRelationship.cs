using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TempooERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixShadowRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Orders_OrderId1",
                schema: "sales",
                table: "OrderLines");

            migrationBuilder.DropIndex(
                name: "IX_OrderLines_OrderId1",
                schema: "sales",
                table: "OrderLines");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                schema: "sales",
                table: "OrderLines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId1",
                schema: "sales",
                table: "OrderLines",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderId1",
                schema: "sales",
                table: "OrderLines",
                column: "OrderId1");

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
    }
}
