﻿// <auto-generated />
using System;
using DeliveryShop.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RottenRun.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DeliveryShop.Database.Models.Addresses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ApartmentNumber")
                        .HasColumnType("integer");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Home")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AddressesId")
                        .HasColumnType("integer");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<int>("ProductID")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AddressesId");

                    b.HasIndex("ProductID");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Categories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Manufacturers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Orders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BasketId")
                        .HasColumnType("integer");

                    b.Property<int>("DeliveryAddressesId")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.HasIndex("DeliveryAddressesId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UsersId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Products", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ManufacturersId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<string>("Specifications")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ManufacturersId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Ratings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ProductsID")
                        .HasColumnType("integer");

                    b.Property<int?>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProductsID");

                    b.HasIndex("UsersId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Statuses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Basket", b =>
                {
                    b.HasOne("DeliveryShop.Database.Models.Addresses", null)
                        .WithMany("ListBasket")
                        .HasForeignKey("AddressesId");

                    b.HasOne("DeliveryShop.Database.Models.Products", "Product")
                        .WithMany("ListBasket")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Orders", b =>
                {
                    b.HasOne("DeliveryShop.Database.Models.Basket", "Basket")
                        .WithMany("OrdersList")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeliveryShop.Database.Models.Addresses", "DeliveryAddresses")
                        .WithMany("OrdersList")
                        .HasForeignKey("DeliveryAddressesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeliveryShop.Database.Models.Statuses", "Status")
                        .WithMany("OrdersList")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeliveryShop.Database.Models.Users", "Users")
                        .WithMany("OrdersList")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Basket");

                    b.Navigation("DeliveryAddresses");

                    b.Navigation("Status");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Products", b =>
                {
                    b.HasOne("DeliveryShop.Database.Models.Categories", "Category")
                        .WithMany("ProductsList")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeliveryShop.Database.Models.Manufacturers", null)
                        .WithMany("ProductsList")
                        .HasForeignKey("ManufacturersId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Ratings", b =>
                {
                    b.HasOne("DeliveryShop.Database.Models.Products", null)
                        .WithMany("RatingsList")
                        .HasForeignKey("ProductsID");

                    b.HasOne("DeliveryShop.Database.Models.Users", null)
                        .WithMany("RatingsList")
                        .HasForeignKey("UsersId");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Users", b =>
                {
                    b.HasOne("DeliveryShop.Database.Models.Roles", "Role")
                        .WithMany("UsersList")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Addresses", b =>
                {
                    b.Navigation("ListBasket");

                    b.Navigation("OrdersList");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Basket", b =>
                {
                    b.Navigation("OrdersList");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Categories", b =>
                {
                    b.Navigation("ProductsList");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Manufacturers", b =>
                {
                    b.Navigation("ProductsList");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Products", b =>
                {
                    b.Navigation("ListBasket");

                    b.Navigation("RatingsList");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Roles", b =>
                {
                    b.Navigation("UsersList");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Statuses", b =>
                {
                    b.Navigation("OrdersList");
                });

            modelBuilder.Entity("DeliveryShop.Database.Models.Users", b =>
                {
                    b.Navigation("OrdersList");

                    b.Navigation("RatingsList");
                });
#pragma warning restore 612, 618
        }
    }
}
