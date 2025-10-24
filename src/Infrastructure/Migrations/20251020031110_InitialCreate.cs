using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: MigrationConstants.Schemas.Shared);

            migrationBuilder.EnsureSchema(
                name: MigrationConstants.Schemas.Commerce);

            migrationBuilder.EnsureSchema(
                name: MigrationConstants.Schemas.Delivery);

            migrationBuilder.EnsureSchema(
                name: MigrationConstants.Schemas.Order);

            migrationBuilder.EnsureSchema(
                name: MigrationConstants.Schemas.Auth);

            migrationBuilder.EnsureSchema(
                name: MigrationConstants.Schemas.Payments);

            migrationBuilder.CreateTable(
                name: "Images",
                schema: MigrationConstants.Schemas.Shared,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    Url = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    Title = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: true),
                    AltText = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: true),
                    OwnerId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    OwnerType = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: MigrationConstants.Schemas.Auth,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    Name = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: MigrationConstants.Schemas.Auth,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    FirebaseId = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    Email = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    Name = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: MigrationConstants.Schemas.Shared,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    Street = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    City = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    State = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    ZipCode = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    Country = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    Apartment = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: true),
                    Notes = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: true),
                    UserId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: MigrationConstants.Schemas.Auth,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commerces",
                schema: MigrationConstants.Schemas.Commerce,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    Name = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    Address = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    UserId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commerces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commerces_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: MigrationConstants.Schemas.Auth,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryCandidates",
                schema: MigrationConstants.Schemas.Delivery,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    UserId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    ApplicationStatus = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    RejectionReason = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: true),
                    ApplicationDate = table.Column<DateTime>(type: MigrationConstants.DataTypes.TimestampTz, nullable: false),
                    Notes = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryCandidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryCandidates_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: MigrationConstants.Schemas.Auth,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryUsers",
                schema: MigrationConstants.Schemas.Delivery,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    UserId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    Status = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    VehicleDetails = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    CurrentLatitude = table.Column<double>(type: "double precision", nullable: false),
                    CurrentLongitude = table.Column<double>(type: "double precision", nullable: false),
                    IsActive = table.Column<bool>(type: MigrationConstants.DataTypes.Boolean, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryUsers_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: MigrationConstants.Schemas.Auth,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: MigrationConstants.Schemas.Shared,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    UserId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    Message = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    IsRead = table.Column<bool>(type: MigrationConstants.DataTypes.Boolean, nullable: false),
                    Date = table.Column<DateTime>(type: MigrationConstants.DataTypes.TimestampTz, nullable: false),
                    NotificationType = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: MigrationConstants.Schemas.Auth,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                schema: MigrationConstants.Schemas.Auth,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    Token = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    Expires = table.Column<DateTime>(type: MigrationConstants.DataTypes.TimestampTz, nullable: false),
                    Revoked = table.Column<DateTime>(type: MigrationConstants.DataTypes.TimestampTz, nullable: true),
                    UserId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: MigrationConstants.Schemas.Auth,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: MigrationConstants.Schemas.Auth,
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    UsersId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RolesId",
                        column: x => x.RolesId,
                        principalSchema: MigrationConstants.Schemas.Auth,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UsersId",
                        column: x => x.UsersId,
                        principalSchema: MigrationConstants.Schemas.Auth,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: MigrationConstants.Schemas.Commerce,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    Name = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    Description = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    CommerceId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Commerces_CommerceId",
                        column: x => x.CommerceId,
                        principalSchema: MigrationConstants.Schemas.Commerce,
                        principalTable: "Commerces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryGroups",
                schema: MigrationConstants.Schemas.Delivery,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    Name = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    CommerceId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryGroups_Commerces_CommerceId",
                        column: x => x.CommerceId,
                        principalSchema: MigrationConstants.Schemas.Commerce,
                        principalTable: "Commerces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: MigrationConstants.Schemas.Order,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    UserId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    CommerceId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    OrderDate = table.Column<DateTime>(type: MigrationConstants.DataTypes.TimestampTz, nullable: false),
                    Status = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    Total = table.Column<decimal>(type: MigrationConstants.DataTypes.Numeric, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Commerces_CommerceId",
                        column: x => x.CommerceId,
                        principalSchema: MigrationConstants.Schemas.Commerce,
                        principalTable: "Commerces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: MigrationConstants.Schemas.Auth,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: MigrationConstants.Schemas.Commerce,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    Name = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    Description = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    Price = table.Column<decimal>(type: MigrationConstants.DataTypes.Numeric, nullable: false),
                    ImageUrl = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    CommerceId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    CategoryId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: MigrationConstants.Schemas.Commerce,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Commerces_CommerceId",
                        column: x => x.CommerceId,
                        principalSchema: MigrationConstants.Schemas.Commerce,
                        principalTable: "Commerces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryGroupUsers",
                schema: MigrationConstants.Schemas.Delivery,
                columns: table => new
                {
                    DeliveryGroupId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    DeliveryUserId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    Id = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryGroupUsers", x => new { x.DeliveryGroupId, x.DeliveryUserId });
                    table.ForeignKey(
                        name: "FK_DeliveryGroupUsers_DeliveryGroups_DeliveryGroupId",
                        column: x => x.DeliveryGroupId,
                        principalSchema: MigrationConstants.Schemas.Delivery,
                        principalTable: "DeliveryGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryGroupUsers_DeliveryUsers_DeliveryUserId",
                        column: x => x.DeliveryUserId,
                        principalSchema: MigrationConstants.Schemas.Delivery,
                        principalTable: "DeliveryUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatusHistories",
                schema: MigrationConstants.Schemas.Order,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    OrderId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    Status = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    ChangeDate = table.Column<DateTime>(type: MigrationConstants.DataTypes.TimestampTz, nullable: false),
                    Notes = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderStatusHistories_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: MigrationConstants.Schemas.Order,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: MigrationConstants.Schemas.Payments,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    OrderId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    Amount = table.Column<decimal>(type: MigrationConstants.DataTypes.Numeric, nullable: false),
                    PaymentMethod = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    Status = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: false),
                    ProviderTransactionId = table.Column<string>(type: MigrationConstants.DataTypes.Text, nullable: true),
                    TransactionDate = table.Column<DateTime>(type: MigrationConstants.DataTypes.TimestampTz, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: MigrationConstants.Schemas.Order,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                schema: MigrationConstants.Schemas.Order,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    OrderId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    ProductId = table.Column<Guid>(type: MigrationConstants.DataTypes.Uuid, nullable: false),
                    Quantity = table.Column<int>(type: MigrationConstants.DataTypes.Integer, nullable: false),
                    UnitPrice = table.Column<decimal>(type: MigrationConstants.DataTypes.Numeric, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: MigrationConstants.Schemas.Order,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: MigrationConstants.Schemas.Commerce,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                schema: MigrationConstants.Schemas.Shared,
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CommerceId",
                schema: MigrationConstants.Schemas.Commerce,
                table: "Categories",
                column: "CommerceId");

            migrationBuilder.CreateIndex(
                name: "IX_Commerces_UserId",
                schema: MigrationConstants.Schemas.Commerce,
                table: "Commerces",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryCandidates_UserId",
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryCandidates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryGroups_CommerceId",
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroups",
                column: "CommerceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryGroupUsers_DeliveryUserId",
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryGroupUsers",
                column: "DeliveryUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryUsers_UserId",
                schema: MigrationConstants.Schemas.Delivery,
                table: "DeliveryUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                schema: MigrationConstants.Schemas.Shared,
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                schema: MigrationConstants.Schemas.Order,
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                schema: MigrationConstants.Schemas.Order,
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CommerceId",
                schema: MigrationConstants.Schemas.Order,
                table: "Orders",
                column: "CommerceId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                schema: MigrationConstants.Schemas.Order,
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusHistories_OrderId",
                schema: MigrationConstants.Schemas.Order,
                table: "OrderStatusHistories",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                schema: MigrationConstants.Schemas.Commerce,
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CommerceId",
                schema: MigrationConstants.Schemas.Commerce,
                table: "Products",
                column: "CommerceId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                schema: MigrationConstants.Schemas.Auth,
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OrderId",
                schema: MigrationConstants.Schemas.Payments,
                table: "Transactions",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UsersId",
                schema: MigrationConstants.Schemas.Auth,
                table: "UserRoles",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses",
                schema: MigrationConstants.Schemas.Shared);

            migrationBuilder.DropTable(
                name: "DeliveryCandidates",
                schema: MigrationConstants.Schemas.Delivery);

            migrationBuilder.DropTable(
                name: "DeliveryGroupUsers",
                schema: MigrationConstants.Schemas.Delivery);

            migrationBuilder.DropTable(
                name: "Images",
                schema: MigrationConstants.Schemas.Shared);

            migrationBuilder.DropTable(
                name: "Notifications",
                schema: MigrationConstants.Schemas.Shared);

            migrationBuilder.DropTable(
                name: "OrderDetails",
                schema: MigrationConstants.Schemas.Order);

            migrationBuilder.DropTable(
                name: "OrderStatusHistories",
                schema: MigrationConstants.Schemas.Order);

            migrationBuilder.DropTable(
                name: "RefreshTokens",
                schema: MigrationConstants.Schemas.Auth);

            migrationBuilder.DropTable(
                name: "Transactions",
                schema: MigrationConstants.Schemas.Payments);

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: MigrationConstants.Schemas.Auth);

            migrationBuilder.DropTable(
                name: "DeliveryGroups",
                schema: MigrationConstants.Schemas.Delivery);

            migrationBuilder.DropTable(
                name: "DeliveryUsers",
                schema: MigrationConstants.Schemas.Delivery);

            migrationBuilder.DropTable(
                name: "Products",
                schema: MigrationConstants.Schemas.Commerce);

            migrationBuilder.DropTable(
                name: "Orders",
                schema: MigrationConstants.Schemas.Order);

            migrationBuilder.DropTable(
                name: "Roles",
                schema: MigrationConstants.Schemas.Auth);

            migrationBuilder.DropTable(
                name: "Categories",
                schema: MigrationConstants.Schemas.Commerce);

            migrationBuilder.DropTable(
                name: "Commerces",
                schema: MigrationConstants.Schemas.Commerce);

            migrationBuilder.DropTable(
                name: "Users",
                schema: MigrationConstants.Schemas.Auth);
        }
    }
}