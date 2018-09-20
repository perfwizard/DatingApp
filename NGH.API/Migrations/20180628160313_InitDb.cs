using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NGH.API.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Balance = table.Column<decimal>(nullable: false),
                    BillingAddress = table.Column<string>(nullable: true),
                    CreateBy = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ShippingAddress = table.Column<string>(nullable: true),
                    TelNo = table.Column<string>(nullable: true),
                    UpdateBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryNoteLines",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discount = table.Column<decimal>(nullable: false),
                    DnId = table.Column<int>(nullable: false),
                    LineTotal = table.Column<decimal>(nullable: false),
                    ProductCode = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    Qty = table.Column<decimal>(nullable: false),
                    UOM = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryNoteLines", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActualWeight = table.Column<decimal>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    BillingAddress = table.Column<string>(nullable: true),
                    CreateBy = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    DnDate = table.Column<DateTime>(nullable: false),
                    DnNo = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    GoldPercent = table.Column<decimal>(nullable: false),
                    GrandTotal = table.Column<decimal>(nullable: false),
                    InternalMemo = table.Column<string>(nullable: true),
                    IssueInvoice = table.Column<bool>(nullable: false),
                    PIC = table.Column<string>(nullable: true),
                    PaidAmount = table.Column<decimal>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    ShippingAddress = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    StdPrice = table.Column<decimal>(nullable: false),
                    Subtotal = table.Column<decimal>(nullable: false),
                    TradePrice = table.Column<decimal>(nullable: false),
                    UpdateBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    VatAmount = table.Column<decimal>(nullable: false),
                    VatRate = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductDiscount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateBy = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CutomerId = table.Column<int>(nullable: false),
                    DiscPrice = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    IsPercent = table.Column<bool>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    UpdateBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDiscount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateBy = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CustProductCode = table.Column<string>(nullable: true),
                    EstWeight = table.Column<decimal>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    StdPrice = table.Column<decimal>(nullable: false),
                    StdWeight = table.Column<decimal>(nullable: false),
                    UOM = table.Column<string>(nullable: true),
                    UpdateBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Wage = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    DocNo = table.Column<string>(nullable: true),
                    FromLocationId = table.Column<int>(nullable: true),
                    PIC = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    Qty = table.Column<decimal>(nullable: false),
                    ReceiveFlag = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StockDate = table.Column<DateTime>(nullable: false),
                    ToLocationId = table.Column<int>(nullable: true),
                    UOM = table.Column<string>(nullable: true),
                    UpdateBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "DeliveryNoteLines");

            migrationBuilder.DropTable(
                name: "DeliveryNotes");

            migrationBuilder.DropTable(
                name: "ProductDiscount");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "StockTransactions");
        }
    }
}
