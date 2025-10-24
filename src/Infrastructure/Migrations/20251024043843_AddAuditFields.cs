using System;
using Infrastructure.Persistence;
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
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Payments,
                table: "Transactions",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Payments,
                table: "Transactions",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Payments,
                table: "Transactions",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Payments,
                table: "Transactions",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Auth,
                table: "RefreshTokens",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Auth,
                table: "RefreshTokens",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Auth,
                table: "RefreshTokens",
                type: MigrationConstants.DataTypes.Integer,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Auth,
                table: "RefreshTokens",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Auth,
                table: "RefreshTokens",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Products",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Products",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Products",
                type: MigrationConstants.DataTypes.Integer,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Products",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Products",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderStatusHistories",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderStatusHistories",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderStatusHistories",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderStatusHistories",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Order,
                table: "Orders",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Order,
                table: "Orders",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Order,
                table: "Orders",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Order,
                table: "Orders",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderDetails",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderDetails",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderDetails",
                type: MigrationConstants.DataTypes.Integer,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderDetails",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderDetails",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Shared,
                table: "Notifications",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Shared,
                table: "Notifications",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Shared,
                table: "Notifications",
                type: MigrationConstants.DataTypes.Integer,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Shared,
                table: "Notifications",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Shared,
                table: "Notifications",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Shared,
                table: "Images",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Shared,
                table: "Images",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Shared,
                table: "Images",
                type: MigrationConstants.DataTypes.Integer,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Shared,
                table: "Images",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Shared,
                table: "Images",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryUsers",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryUsers",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryUsers",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryUsers",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroupUsers",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroupUsers",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroupUsers",
                type: MigrationConstants.DataTypes.Integer,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroupUsers",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroupUsers",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroups",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroups",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroups",
                type: MigrationConstants.DataTypes.Integer,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroups",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroups",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryCandidates",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryCandidates",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryCandidates",
                type: MigrationConstants.DataTypes.Integer,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryCandidates",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryCandidates",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Commerces",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Commerces",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Commerces",
                type: MigrationConstants.DataTypes.Integer,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Commerces",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Commerces",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Categories",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Categories",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Categories",
                type: MigrationConstants.DataTypes.Integer,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Categories",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Categories",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Shared,
                table: "Addresses",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Shared,
                table: "Addresses",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Shared,
                table: "Addresses",
                type: MigrationConstants.DataTypes.Integer,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Shared,
                table: "Addresses",
                type: MigrationConstants.DataTypes.TimestampTz,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Shared,
                table: "Addresses",
                type: MigrationConstants.DataTypes.Text,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Payments,
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Payments,
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Payments,
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Payments,
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Auth,
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Auth,
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Auth,
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Auth,
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Auth,
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Products");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Products");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Products");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Products");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Products");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderStatusHistories");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderStatusHistories");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderStatusHistories");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderStatusHistories");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Order,
                table: "Orders");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Order,
                table: "Orders");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Order,
                table: "Orders");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Order,
                table: "Orders");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Order,
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Shared,
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Shared,
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Shared,
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Shared,
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Shared,
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Shared,
                table: "Images");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Shared,
                table: "Images");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Shared,
                table: "Images");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Shared,
                table: "Images");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Shared,
                table: "Images");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryUsers");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryUsers");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryUsers");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryUsers");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroupUsers");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroupUsers");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroupUsers");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroupUsers");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroupUsers");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroups");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroups");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroups");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroups");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroups");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryCandidates");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryCandidates");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryCandidates");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryCandidates");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryCandidates");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Commerces");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Commerces");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Commerces");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Commerces");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Commerces");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Categories");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Categories");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Categories");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Categories");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Commerce,
                table: "Categories");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedAt,
                schema: MigrationConstants.Schemas.Shared,
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.CreatedBy,
                schema: MigrationConstants.Schemas.Shared,
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.Status,
                schema: MigrationConstants.Schemas.Shared,
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedAt,
                schema: MigrationConstants.Schemas.Shared,
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: MigrationConstants.Columns.UpdatedBy,
                schema: MigrationConstants.Schemas.Shared,
                table: "Addresses");
        }
    }
}