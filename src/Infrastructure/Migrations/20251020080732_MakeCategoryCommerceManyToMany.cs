using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeCategoryCommerceManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Commerces_CommerceId",
                schema: "commerce",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CommerceId",
                schema: "commerce",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CommerceId",
                schema: "commerce",
                table: "Categories");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryCommerce",
                schema: "commerce");

            migrationBuilder.AddColumn<Guid>(
                name: "CommerceId",
                schema: "commerce",
                table: "Categories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
        }
    }
}
