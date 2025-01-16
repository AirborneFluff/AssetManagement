using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddedSupplySourceRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetSupplySources_Assets_AssetId",
                table: "AssetSupplySources");

            migrationBuilder.AlterColumn<string>(
                name: "AssetId",
                table: "AssetSupplySources",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetSupplySources_Assets_AssetId",
                table: "AssetSupplySources",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetSupplySources_Assets_AssetId",
                table: "AssetSupplySources");

            migrationBuilder.AlterColumn<string>(
                name: "AssetId",
                table: "AssetSupplySources",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetSupplySources_Assets_AssetId",
                table: "AssetSupplySources",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id");
        }
    }
}
