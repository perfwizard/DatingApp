using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NGH.API.Migrations
{
    public partial class OptionalProductDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CutomerId",
                table: "ProductDiscount",
                newName: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscount_ProductId",
                table: "ProductDiscount",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDiscount_Products_ProductId",
                table: "ProductDiscount",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDiscount_Products_ProductId",
                table: "ProductDiscount");

            migrationBuilder.DropIndex(
                name: "IX_ProductDiscount_ProductId",
                table: "ProductDiscount");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "ProductDiscount",
                newName: "CutomerId");
        }
    }
}
