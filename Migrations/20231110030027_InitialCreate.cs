using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace eCart_Backend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryName = table.Column<string>(type: "text", nullable: false),
                    CategoryImage = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Uid = table.Column<string>(type: "text", nullable: false),
                    isSeller = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaymentId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    OrderTotal = table.Column<decimal>(type: "numeric", nullable: true),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    itemsId = table.Column<int>(type: "integer", nullable: false),
                    ordersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOrder_Items_itemsId",
                        column: x => x.itemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemOrder_Orders_ordersId",
                        column: x => x.ordersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryImage", "CategoryName" },
                values: new object[,]
                {
                    { 1, "https://p2-ofp.static.pub/ShareResource/na/subseries/hero/lenovo-3i-chromebook-15-inch.png", "Laptops" },
                    { 2, "https://i5.walmartimages.com/seo/Beats-Studio-Pro-Wireless-Headphones-Black_482fa5f9-4478-43a6-84ef-5abc2de71109.2d23bdd7708ce99a60e99d6a3750171c.png", "Headphones" },
                    { 3, "https://d12mivgeuoigbq.cloudfront.net/magento-media/members/h0026-nicks-appliance/floating_appliances.png", "Appliances" },
                    { 4, "https://img.xfinitymobile.com/image/upload/c_fit,f_auto,q_auto,fl_lossy/v1696609940/client/v2/images/Apple-iPhone-15-Pro-BAU-Shop-Apple-LP-Placement/Shop_Banner_1280.png", "Phones" },
                    { 5, "https://img.bbystatic.com/BestBuy_US/store/ee/2017/cam/pr/sol-11219-nikon/sol-11219-nikon-d610-dslr-camera-rev.png", "Cameras" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Credit Card" },
                    { 2, "Cash" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "Processing" },
                    { 3, "Shipped" },
                    { 4, "Complete" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "Name", "Phone", "Uid", "isSeller" },
                values: new object[,]
                {
                    { 1, "123 Street", "jack@yahoo.com", "Jack Smith", "615-445-8855", "4d5256236asd6", true },
                    { 2, "856 Road", "Mdaniel@gmail.com", "Mike Daniel", "615-645-2415", "wLR4RTEAKyUr6K8w9ufj1IZM1hC3", false },
                    { 3, "789 Circle", "sbutler@comcast.net", "Steve Butler", "615-746-3641", "4d56a2526sd6", true }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "Name", "Price", "Quantity", "UserId" },
                values: new object[,]
                {
                    { 1, 4, "First Item in the store", "https://phones.mintmobile.com/wp-content/uploads/2023/09/Apple_iPhone_15_Pro_Titanium_Blue_PDP_Image.png", "Iphone", 14.99m, 10, 1 },
                    { 2, 2, "Second Item in the store", "https://pdp.com/cdn/shop/files/052-003_PS5_GAMBITWIRELESS_LEFTQUARTERSHOT.png?v=1696884972&width=640", "Headset", 40.99m, 12, 2 },
                    { 3, 5, "Third Item in the store", "https://i1.adis.ws/i/canon/15_eos_90d_bk_thefront_ef-s18-135mm_3.5-5.6isusm_square_6bd191e26825499fb5fe88e57a763f7a", "Camera", 414.99m, 15, 1 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate", "OrderTotal", "PaymentId", "StatusId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 159.55m, 1, 1, 1 },
                    { 2, new DateTime(2023, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 145.01m, 1, 3, 2 },
                    { 3, new DateTime(2023, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 252.22m, 2, 2, 2 },
                    { 4, new DateTime(2023, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 314.25m, 2, 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrder_ordersId",
                table: "ItemOrder",
                column: "ordersId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UserId",
                table: "Items",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemOrder");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
