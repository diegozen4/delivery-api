using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliveryFunctionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirebaseId",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ApartmentNumber",
                schema: "shared",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "shared",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "shared",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "District",
                schema: "shared",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "shared",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "shared",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "NotesOrReference",
                schema: "shared",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "SecondaryAddressLine",
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

            migrationBuilder.RenameColumn(
                name: "StateOrRegion",
                schema: "shared",
                table: "Addresses",
                newName: "State");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "auth",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "auth",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryUserId",
                schema: "order",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                schema: "shared",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                schema: "shared",
                table: "Addresses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryUserId",
                schema: "order",
                table: "Orders",
                column: "DeliveryUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_DeliveryUserId",
                schema: "order",
                table: "Orders",
                column: "DeliveryUserId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_DeliveryUserId",
                schema: "order",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryUserId",
                schema: "order",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeliveryUserId",
                schema: "order",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "shared",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "State",
                schema: "shared",
                table: "Addresses",
                newName: "StateOrRegion");

            migrationBuilder.AddColumn<string>(
                name: "FirebaseId",
                schema: "auth",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                schema: "shared",
                table: "Addresses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ApartmentNumber",
                schema: "shared",
                table: "Addresses",
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

            migrationBuilder.AddColumn<string>(
                name: "District",
                schema: "shared",
                table: "Addresses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                schema: "shared",
                table: "Addresses",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                schema: "shared",
                table: "Addresses",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "NotesOrReference",
                schema: "shared",
                table: "Addresses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryAddressLine",
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
    }
}
