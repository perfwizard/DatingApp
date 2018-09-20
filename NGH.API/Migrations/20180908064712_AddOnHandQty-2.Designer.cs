﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using NGH.API.DataAccess;
using System;

namespace NGH.API.Migrations
{
    [DbContext(typeof(NGHDataContext))]
    [Migration("20180908064712_AddOnHandQty-2")]
    partial class AddOnHandQty2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NGH.API.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Balance");

                    b.Property<string>("BillingAddress");

                    b.Property<string>("BranchCode");

                    b.Property<string>("CompanyName");

                    b.Property<string>("ContactName");

                    b.Property<string>("CreateBy");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CustomerCode")
                        .IsRequired();

                    b.Property<int?>("CustomerGroupId");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Email");

                    b.Property<string>("FaxNo");

                    b.Property<string>("MobileNo");

                    b.Property<string>("ShippingAddress");

                    b.Property<string>("TaxId");

                    b.Property<string>("TelNo");

                    b.Property<string>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("CustomerGroupId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("NGH.API.Models.CustomerGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("GroupName");

                    b.HasKey("Id");

                    b.ToTable("CustomerGroup");
                });

            modelBuilder.Entity("NGH.API.Models.DeliveryNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("ActualWeight");

                    b.Property<decimal>("Balance");

                    b.Property<string>("BillingAddress");

                    b.Property<string>("CreateBy");

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("CustomerId");

                    b.Property<decimal>("Discount");

                    b.Property<DateTime>("DnDate");

                    b.Property<string>("DnNo");

                    b.Property<DateTime>("DueDate");

                    b.Property<decimal>("GoldPercent");

                    b.Property<decimal>("GrandTotal");

                    b.Property<string>("InternalMemo");

                    b.Property<bool>("IssueInvoice");

                    b.Property<decimal>("PaidAmount");

                    b.Property<string>("Remark");

                    b.Property<string>("SalesPIC");

                    b.Property<string>("ShippingAddress");

                    b.Property<string>("StatusCode");

                    b.Property<int>("StatusId");

                    b.Property<decimal>("StdPrice");

                    b.Property<decimal>("Subtotal");

                    b.Property<decimal>("TradePrice");

                    b.Property<string>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<decimal>("VatAmount");

                    b.Property<decimal>("VatRate");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StatusId");

                    b.ToTable("DeliveryNotes");
                });

            modelBuilder.Entity("NGH.API.Models.DeliveryNoteLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreateBy");

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("DeliveryNoteId");

                    b.Property<decimal>("Discount");

                    b.Property<int>("LineNumber");

                    b.Property<decimal>("LineTotal");

                    b.Property<string>("ProductCode");

                    b.Property<string>("ProductId");

                    b.Property<string>("ProductName");

                    b.Property<decimal>("Qty");

                    b.Property<string>("UOM");

                    b.Property<decimal>("UnitPrice");

                    b.Property<string>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<decimal>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryNoteId");

                    b.ToTable("DeliveryNoteLines");
                });

            modelBuilder.Entity("NGH.API.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Name");

                    b.HasKey("Id");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("NGH.API.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreateBy");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CustProductCode");

                    b.Property<bool>("Deleted");

                    b.Property<decimal>("EstWeight");

                    b.Property<string>("ImagePath");

                    b.Property<decimal>("OnHandQty");

                    b.Property<string>("ProductCode");

                    b.Property<string>("ProductName");

                    b.Property<decimal>("StdPrice");

                    b.Property<decimal>("StdWeight");

                    b.Property<string>("UOM");

                    b.Property<string>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<decimal>("Wage");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("NGH.API.Models.ProductDiscount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreateBy");

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("CutomerId");

                    b.Property<decimal>("DiscPrice");

                    b.Property<decimal>("Discount");

                    b.Property<bool>("IsPercent");

                    b.Property<int>("ProductId");

                    b.Property<string>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("ProductDiscount");
                });

            modelBuilder.Entity("NGH.API.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("NGH.API.Models.Status", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("StatusName");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("NGH.API.Models.StockTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("CreateBy");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("DocNo");

                    b.Property<int?>("FromLocationId");

                    b.Property<string>("PIC");

                    b.Property<int>("ProductId");

                    b.Property<decimal>("Qty");

                    b.Property<bool>("ReceiveFlag");

                    b.Property<int>("Status");

                    b.Property<DateTime>("StockDate");

                    b.Property<int?>("ToLocationId");

                    b.Property<string>("UOM");

                    b.Property<string>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<decimal>("Weight");

                    b.HasKey("Id");

                    b.ToTable("StockTransactions");
                });

            modelBuilder.Entity("NGH.API.Models.UIObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("UIObject");
                });

            modelBuilder.Entity("NGH.API.Models.UIObjectPermission", b =>
                {
                    b.Property<int>("ObjectId");

                    b.Property<int>("PermissionId");

                    b.Property<int?>("RoleId");

                    b.HasKey("ObjectId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("UIObjectPermission");
                });

            modelBuilder.Entity("NGH.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("CreateBy");

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName");

                    b.Property<string>("ImagePath");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("TelNo");

                    b.Property<string>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NGH.API.Models.UserRole", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("NGH.API.Models.Customer", b =>
                {
                    b.HasOne("NGH.API.Models.CustomerGroup", "CustomerGroup")
                        .WithMany()
                        .HasForeignKey("CustomerGroupId");
                });

            modelBuilder.Entity("NGH.API.Models.DeliveryNote", b =>
                {
                    b.HasOne("NGH.API.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NGH.API.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NGH.API.Models.DeliveryNoteLine", b =>
                {
                    b.HasOne("NGH.API.Models.DeliveryNote", "DeliveryNote")
                        .WithMany("DeliveryNoteLines")
                        .HasForeignKey("DeliveryNoteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NGH.API.Models.UIObjectPermission", b =>
                {
                    b.HasOne("NGH.API.Models.UIObject", "Object")
                        .WithMany("ObjectPermissions")
                        .HasForeignKey("ObjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NGH.API.Models.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NGH.API.Models.Role")
                        .WithMany("ObjectPermissions")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("NGH.API.Models.UserRole", b =>
                {
                    b.HasOne("NGH.API.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NGH.API.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
