using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddressEntityForPeru : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                schema: "order",
                table: "Orders",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                schema: "order",
                table: "OrderDetails",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "State",
                schema: "shared",
                table: "Addresses",
                newName: "StateOrRegion");

            migrationBuilder.RenameColumn(
                name: "Notes",
                schema: "shared",
                table: "Addresses",
                newName: "SecondaryAddressLine");

            migrationBuilder.RenameColumn(
                name: "Apartment",
                schema: "shared",
                table: "Addresses",
                newName: "NotesOrReference");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                schema: "order",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                schema: "order",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ApartmentNumber",
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

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                schema: "order",
                table: "Orders",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "order",
                table: "OrderDetails",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "StateOrRegion",
                schema: "shared",
                table: "Addresses",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "SecondaryAddressLine",
                schema: "shared",
                table: "Addresses",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "NotesOrReference",
                schema: "shared",
                table: "Addresses",
                newName: "Apartment");

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
        }
    }
}
