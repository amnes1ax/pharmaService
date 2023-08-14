using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaService.DataAccess.PostgresSql.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_warehouses_pharmacy_id",
                table: "warehouses",
                column: "pharmacy_id");

            migrationBuilder.CreateIndex(
                name: "IX_batches_product_id",
                table: "batches",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_batches_warehouse_id",
                table: "batches",
                column: "warehouse_id");

            migrationBuilder.AddForeignKey(
                name: "FK_batches_products_product_id",
                table: "batches",
                column: "product_id",
                principalTable: "products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_batches_warehouses_warehouse_id",
                table: "batches",
                column: "warehouse_id",
                principalTable: "warehouses",
                principalColumn: "warehouse_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_warehouses_pharmacies_pharmacy_id",
                table: "warehouses",
                column: "pharmacy_id",
                principalTable: "pharmacies",
                principalColumn: "pharmacy_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_batches_products_product_id",
                table: "batches");

            migrationBuilder.DropForeignKey(
                name: "FK_batches_warehouses_warehouse_id",
                table: "batches");

            migrationBuilder.DropForeignKey(
                name: "FK_warehouses_pharmacies_pharmacy_id",
                table: "warehouses");

            migrationBuilder.DropIndex(
                name: "IX_warehouses_pharmacy_id",
                table: "warehouses");

            migrationBuilder.DropIndex(
                name: "IX_batches_product_id",
                table: "batches");

            migrationBuilder.DropIndex(
                name: "IX_batches_warehouse_id",
                table: "batches");
        }
    }
}
