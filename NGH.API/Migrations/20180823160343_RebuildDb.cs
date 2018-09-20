using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NGH.API.Migrations
{
    public partial class RebuildDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "DeliveryNotes",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "PIC",
                table: "DeliveryNotes",
                newName: "StatusCode");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "DeliveryNoteLines",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DnId",
                table: "DeliveryNoteLines",
                newName: "DeliveryNoteId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Customers",
                newName: "TaxId");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SalesPIC",
                table: "DeliveryNotes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "DeliveryNoteLines",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "DeliveryNoteLines",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdateBy",
                table: "DeliveryNoteLines",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "DeliveryNoteLines",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BranchCode",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerGroupId",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Customers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FaxNo",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNo",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    StatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UIObject",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UIObject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    CreateBy = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    TelNo = table.Column<string>(nullable: true),
                    UpdateBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Username = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UIObjectPermission",
                columns: table => new
                {
                    ObjectId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UIObjectPermission", x => new { x.ObjectId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_UIObjectPermission_UIObject_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "UIObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UIObjectPermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UIObjectPermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryNotes_CustomerId",
                table: "DeliveryNotes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryNotes_StatusId",
                table: "DeliveryNotes",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryNoteLines_DeliveryNoteId",
                table: "DeliveryNoteLines",
                column: "DeliveryNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerGroupId",
                table: "Customers",
                column: "CustomerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UIObjectPermission_PermissionId",
                table: "UIObjectPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UIObjectPermission_RoleId",
                table: "UIObjectPermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerGroup_CustomerGroupId",
                table: "Customers",
                column: "CustomerGroupId",
                principalTable: "CustomerGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryNoteLines_DeliveryNotes_DeliveryNoteId",
                table: "DeliveryNoteLines",
                column: "DeliveryNoteId",
                principalTable: "DeliveryNotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryNotes_Customers_CustomerId",
                table: "DeliveryNotes",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryNotes_Status_StatusId",
                table: "DeliveryNotes",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerGroup_CustomerGroupId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryNoteLines_DeliveryNotes_DeliveryNoteId",
                table: "DeliveryNoteLines");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryNotes_Customers_CustomerId",
                table: "DeliveryNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryNotes_Status_StatusId",
                table: "DeliveryNotes");

            migrationBuilder.DropTable(
                name: "CustomerGroup");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "UIObjectPermission");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UIObject");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryNotes_CustomerId",
                table: "DeliveryNotes");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryNotes_StatusId",
                table: "DeliveryNotes");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryNoteLines_DeliveryNoteId",
                table: "DeliveryNoteLines");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerGroupId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalesPIC",
                table: "DeliveryNotes");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "DeliveryNoteLines");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "DeliveryNoteLines");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "DeliveryNoteLines");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "DeliveryNoteLines");

            migrationBuilder.DropColumn(
                name: "BranchCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerGroupId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FaxNo",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MobileNo",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "DeliveryNotes",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "StatusCode",
                table: "DeliveryNotes",
                newName: "PIC");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DeliveryNoteLines",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "DeliveryNoteId",
                table: "DeliveryNoteLines",
                newName: "DnId");

            migrationBuilder.RenameColumn(
                name: "TaxId",
                table: "Customers",
                newName: "Name");
        }
    }
}
