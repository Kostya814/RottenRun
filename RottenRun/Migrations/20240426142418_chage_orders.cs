using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RottenRun.Migrations
{
    /// <inheritdoc />
    public partial class chage_orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Orders");
        }
    }
}
