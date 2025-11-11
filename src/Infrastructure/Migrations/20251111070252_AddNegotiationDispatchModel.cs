using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNegotiationDispatchModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CurrentLongitude",
                schema: "auth",
                table: "Users",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DispatchMode",
                schema: "order",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ProposedDeliveryFee",
                schema: "order",
                table: "Orders",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                schema: "commerce",
                table: "Commerces",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                schema: "commerce",
                table: "Commerces",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "DeliveryOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    OfferAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryOffers_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "order",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryOffers_Users_DeliveryUserId",
                        column: x => x.DeliveryUserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOffers_DeliveryUserId",
                table: "DeliveryOffers",
                column: "DeliveryUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOffers_OrderId",
                table: "DeliveryOffers",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryOffers");

            migrationBuilder.DropColumn(
                name: "CurrentLongitude",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DispatchMode",
                schema: "order",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProposedDeliveryFee",
                schema: "order",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "commerce",
                table: "Commerces");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "commerce",
                table: "Commerces");
        }
    }
}
