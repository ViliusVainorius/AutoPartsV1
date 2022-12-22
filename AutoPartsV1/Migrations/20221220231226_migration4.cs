using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoPartsV1.Migrations
{
    /// <inheritdoc />
    public partial class migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarParts_Cars_CarId",
                table: "CarParts");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_BrandId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_CarParts_CarId",
                table: "CarParts");

            migrationBuilder.DropColumn(
                name: "CreationDateYear",
                table: "CarParts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CarParts");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "CarParts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "CarParts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "CarParts");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "CarParts");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateYear",
                table: "CarParts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "CarParts",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BrandId",
                table: "Cars",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CarParts_CarId",
                table: "CarParts",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarParts_Cars_CarId",
                table: "CarParts",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
