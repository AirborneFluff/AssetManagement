using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddedAssetSupplySources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetTag_Assets_AssetId",
                table: "AssetTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetTag",
                table: "AssetTag");

            migrationBuilder.RenameTable(
                name: "AssetTag",
                newName: "AssetTags");

            migrationBuilder.RenameIndex(
                name: "IX_AssetTag_AssetId",
                table: "AssetTags",
                newName: "IX_AssetTags_AssetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetTags",
                table: "AssetTags",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AssetSuppliers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Website = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetSuppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetSupplySources",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    SupplierId = table.Column<string>(type: "TEXT", nullable: false),
                    SupplierReference = table.Column<string>(type: "TEXT", nullable: false),
                    QuantityUnit = table.Column<string>(type: "TEXT", nullable: true),
                    AssetId = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetSupplySources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetSupplySources_AssetSuppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "AssetSuppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetSupplySources_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetSupplySourcePrices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    SupplySourceId = table.Column<string>(type: "TEXT", nullable: false),
                    UnitPrice = table.Column<double>(type: "REAL", nullable: false),
                    Quantity = table.Column<float>(type: "REAL", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_AssetSupplySources_AssetId",
                table: "AssetSupplySources",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetSupplySources_SupplierId",
                table: "AssetSupplySources",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTags_Assets_AssetId",
                table: "AssetTags",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetTags_Assets_AssetId",
                table: "AssetTags");

            migrationBuilder.DropTable(
                name: "AssetSupplySourcePrices");

            migrationBuilder.DropTable(
                name: "AssetSupplySources");

            migrationBuilder.DropTable(
                name: "AssetSuppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetTags",
                table: "AssetTags");

            migrationBuilder.RenameTable(
                name: "AssetTags",
                newName: "AssetTag");

            migrationBuilder.RenameIndex(
                name: "IX_AssetTags_AssetId",
                table: "AssetTag",
                newName: "IX_AssetTag_AssetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetTag",
                table: "AssetTag",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTag_Assets_AssetId",
                table: "AssetTag",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
