using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phuong.eShop.CatalogService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AuditableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CatalogTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CatalogTypes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "CatalogTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "CatalogTypes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CatalogItems",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CatalogItems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "CatalogItems",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "CatalogItems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CatalogBrands",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CatalogBrands",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "CatalogBrands",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "CatalogBrands",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CatalogTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CatalogTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "CatalogTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "CatalogTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CatalogItems");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CatalogItems");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "CatalogItems");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "CatalogItems");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CatalogBrands");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CatalogBrands");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "CatalogBrands");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "CatalogBrands");
        }
    }
}
