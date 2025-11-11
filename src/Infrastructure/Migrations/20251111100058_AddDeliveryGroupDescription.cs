using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliveryGroupDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commerces_Users_UserId",
                schema: "commerce",
                table: "Commerces");

            migrationBuilder.DropTable(
                name: "CategoryCommerce",
                schema: "commerce");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "delivery",
                table: "DeliveryCandidates");

            migrationBuilder.RenameColumn(
                name: "RejectionReason",
                schema: "delivery",
                table: "DeliveryCandidates",
                newName: "AdminNotes");

            migrationBuilder.RenameColumn(
                name: "ApplicationStatus",
                schema: "delivery",
                table: "DeliveryCandidates",
                newName: "VehicleDetails");

            migrationBuilder.RenameColumn(
                name: "ApplicationDate",
                schema: "delivery",
                table: "DeliveryCandidates",
                newName: "AppliedDate");

            migrationBuilder.RenameColumn(
                name: "Address",
                schema: "commerce",
                table: "Commerces",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<double>(
                name: "CurrentLatitude",
                schema: "auth",
                table: "Users",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "delivery",
                table: "DeliveryGroups",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "commerce",
                table: "Commerces",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                schema: "commerce",
                table: "Commerces",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "commerce",
                table: "Commerces",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "commerce",
                table: "Commerces",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CommerceId",
                schema: "commerce",
                table: "Categories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Commerces_AddressId",
                schema: "commerce",
                table: "Commerces",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CommerceId",
                schema: "commerce",
                table: "Categories",
                column: "CommerceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Commerces_CommerceId",
                schema: "commerce",
                table: "Categories",
                column: "CommerceId",
                principalSchema: "commerce",
                principalTable: "Commerces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commerces_Addresses_AddressId",
                schema: "commerce",
                table: "Commerces",
                column: "AddressId",
                principalSchema: "shared",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commerces_Users_UserId",
                schema: "commerce",
                table: "Commerces",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Commerces_CommerceId",
                schema: "commerce",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Commerces_Addresses_AddressId",
                schema: "commerce",
                table: "Commerces");

            migrationBuilder.DropForeignKey(
                name: "FK_Commerces_Users_UserId",
                schema: "commerce",
                table: "Commerces");

            migrationBuilder.DropIndex(
                name: "IX_Commerces_AddressId",
                schema: "commerce",
                table: "Commerces");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CommerceId",
                schema: "commerce",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CurrentLatitude",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "delivery",
                table: "DeliveryGroups");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "commerce",
                table: "Commerces");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "commerce",
                table: "Commerces");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "commerce",
                table: "Commerces");

            migrationBuilder.DropColumn(
                name: "CommerceId",
                schema: "commerce",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "VehicleDetails",
                schema: "delivery",
                table: "DeliveryCandidates",
                newName: "ApplicationStatus");

            migrationBuilder.RenameColumn(
                name: "AppliedDate",
                schema: "delivery",
                table: "DeliveryCandidates",
                newName: "ApplicationDate");

            migrationBuilder.RenameColumn(
                name: "AdminNotes",
                schema: "delivery",
                table: "DeliveryCandidates",
                newName: "RejectionReason");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                schema: "commerce",
                table: "Commerces",
                newName: "Address");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "delivery",
                table: "DeliveryCandidates",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "commerce",
                table: "Commerces",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryCommerce",
                schema: "commerce",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommercesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryCommerce", x => new { x.CategoriesId, x.CommercesId });
                    table.ForeignKey(
                        name: "FK_CategoryCommerce_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalSchema: "commerce",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryCommerce_Commerces_CommercesId",
                        column: x => x.CommercesId,
                        principalSchema: "commerce",
                        principalTable: "Commerces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryCommerce_CommercesId",
                schema: "commerce",
                table: "CategoryCommerce",
                column: "CommercesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commerces_Users_UserId",
                schema: "commerce",
                table: "Commerces",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
