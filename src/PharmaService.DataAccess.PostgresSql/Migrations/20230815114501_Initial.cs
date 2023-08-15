using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaService.DataAccess.PostgresSql.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pharmacies",
                columns: table => new
                {
                    pharmacy_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pharmacies", x => x.pharmacy_id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    shelf_life = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.product_id);
                });

            migrationBuilder.CreateTable(
                name: "warehouses",
                columns: table => new
                {
                    warehouse_id = table.Column<Guid>(type: "uuid", nullable: false),
                    pharmacy_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    PharmacyId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouses", x => x.warehouse_id);
                    table.ForeignKey(
                        name: "FK_warehouses_pharmacies_PharmacyId1",
                        column: x => x.PharmacyId1,
                        principalTable: "pharmacies",
                        principalColumn: "pharmacy_id");
                    table.ForeignKey(
                        name: "FK_warehouses_pharmacies_pharmacy_id",
                        column: x => x.pharmacy_id,
                        principalTable: "pharmacies",
                        principalColumn: "pharmacy_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "batches",
                columns: table => new
                {
                    batch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_count = table.Column<long>(type: "bigint", nullable: false),
                    arrived_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    expired_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    warehouse_id = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_batches", x => x.batch_id);
                    table.ForeignKey(
                        name: "FK_batches_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_batches_warehouses_WarehouseId1",
                        column: x => x.WarehouseId1,
                        principalTable: "warehouses",
                        principalColumn: "warehouse_id");
                    table.ForeignKey(
                        name: "FK_batches_warehouses_warehouse_id",
                        column: x => x.warehouse_id,
                        principalTable: "warehouses",
                        principalColumn: "warehouse_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_batches_product_id",
                table: "batches",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_batches_warehouse_id",
                table: "batches",
                column: "warehouse_id");

            migrationBuilder.CreateIndex(
                name: "IX_batches_WarehouseId1",
                table: "batches",
                column: "WarehouseId1");

            migrationBuilder.CreateIndex(
                name: "IX_warehouses_pharmacy_id",
                table: "warehouses",
                column: "pharmacy_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouses_PharmacyId1",
                table: "warehouses",
                column: "PharmacyId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "batches");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "warehouses");

            migrationBuilder.DropTable(
                name: "pharmacies");
        }
    }
}
