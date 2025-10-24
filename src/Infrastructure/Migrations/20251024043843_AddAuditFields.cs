using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "payments",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "payments",
                table: "Transactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "payments",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "payments",
                table: "Transactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "auth",
                table: "RefreshTokens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "auth",
                table: "RefreshTokens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "auth",
                table: "RefreshTokens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "auth",
                table: "RefreshTokens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "auth",
                table: "RefreshTokens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "commerce",
                table: "Products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "commerce",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "commerce",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "commerce",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "commerce",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "order",
                table: "OrderStatusHistories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "order",
                table: "OrderStatusHistories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "order",
                table: "OrderStatusHistories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "order",
                table: "OrderStatusHistories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "order",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "order",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "order",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "order",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "order",
                table: "OrderDetails",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "order",
                table: "OrderDetails",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "order",
                table: "OrderDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "order",
                table: "OrderDetails",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "order",
                table: "OrderDetails",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "shared",
                table: "Notifications",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "shared",
                table: "Notifications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "shared",
                table: "Notifications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "shared",
                table: "Notifications",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "shared",
                table: "Notifications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "shared",
                table: "Images",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "shared",
                table: "Images",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "shared",
                table: "Images",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "shared",
                table: "Images",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "shared",
                table: "Images",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "delivery",
                table: "DeliveryUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "delivery",
                table: "DeliveryUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "delivery",
                table: "DeliveryUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "delivery",
                table: "DeliveryUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "delivery",
                table: "DeliveryGroupUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "delivery",
                table: "DeliveryGroupUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "delivery",
                table: "DeliveryGroupUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "delivery",
                table: "DeliveryGroupUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "delivery",
                table: "DeliveryGroupUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "delivery",
                table: "DeliveryGroups",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "delivery",
                table: "DeliveryGroups",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "delivery",
                table: "DeliveryGroups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "delivery",
                table: "DeliveryGroups",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "delivery",
                table: "DeliveryGroups",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "delivery",
                table: "DeliveryCandidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "delivery",
                table: "DeliveryCandidates",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "delivery",
                table: "DeliveryCandidates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "delivery",
                table: "DeliveryCandidates",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "delivery",
                table: "DeliveryCandidates",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "commerce",
                table: "Commerces",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "commerce",
                table: "Commerces",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "commerce",
                table: "Commerces",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "commerce",
                table: "Commerces",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "commerce",
                table: "Commerces",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "commerce",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "commerce",
                table: "Categories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "commerce",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "commerce",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "commerce",
                table: "Categories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "shared",
                table: "Addresses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "shared",
                table: "Addresses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "shared",
                table: "Addresses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "shared",
                table: "Addresses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "shared",
                table: "Addresses",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "payments",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "payments",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "payments",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "payments",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "auth",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "auth",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "auth",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "auth",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "auth",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "commerce",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "commerce",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "commerce",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "commerce",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "commerce",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "order",
                table: "OrderStatusHistories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "order",
                table: "OrderStatusHistories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "order",
                table: "OrderStatusHistories");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "order",
                table: "OrderStatusHistories");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "order",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "order",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "order",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "order",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "order",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "order",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "order",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "order",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "order",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "shared",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "shared",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "shared",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "shared",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "shared",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "shared",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "shared",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "shared",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "shared",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "shared",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "delivery",
                table: "DeliveryUsers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "delivery",
                table: "DeliveryUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "delivery",
                table: "DeliveryUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "delivery",
                table: "DeliveryUsers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "delivery",
                table: "DeliveryGroupUsers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "delivery",
                table: "DeliveryGroupUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "delivery",
                table: "DeliveryGroupUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "delivery",
                table: "DeliveryGroupUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "delivery",
                table: "DeliveryGroupUsers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "delivery",
                table: "DeliveryGroups");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "delivery",
                table: "DeliveryGroups");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "delivery",
                table: "DeliveryGroups");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "delivery",
                table: "DeliveryGroups");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "delivery",
                table: "DeliveryGroups");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "delivery",
                table: "DeliveryCandidates");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "delivery",
                table: "DeliveryCandidates");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "delivery",
                table: "DeliveryCandidates");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "delivery",
                table: "DeliveryCandidates");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "delivery",
                table: "DeliveryCandidates");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "commerce",
                table: "Commerces");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "commerce",
                table: "Commerces");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "commerce",
                table: "Commerces");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "commerce",
                table: "Commerces");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "commerce",
                table: "Commerces");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "commerce",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "commerce",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "commerce",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "commerce",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "commerce",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "shared",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "shared",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "shared",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "shared",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "shared",
                table: "Addresses");
        }
    }
}
