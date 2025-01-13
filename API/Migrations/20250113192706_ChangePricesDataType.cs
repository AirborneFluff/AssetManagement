using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ChangePricesDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetSupplySourcePrices");

            migrationBuilder.AddColumn<string>(
                name: "Prices",
                table: "AssetSupplySources",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prices",
                table: "AssetSupplySources");

            migrationBuilder.CreateTable(
                name: "AssetSupplySourcePrices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    SupplySourceId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Quantity = table.Column<float>(type: "REAL", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
                    UnitPrice = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetSupplySourcePrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetSupplySourcePrices_AssetSupplySources_SupplySourceId",
                        column: x => x.SupplySourceId,
                        principalTable: "AssetSupplySources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetSupplySourcePrices_SupplySourceId",
                table: "AssetSupplySourcePrices",
                column: "SupplySourceId");
        }
    }
}
