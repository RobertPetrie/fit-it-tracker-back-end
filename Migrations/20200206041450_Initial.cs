using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace fix_it_tracker_back_end.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 60, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 10, nullable: true),
                    City = table.Column<string>(maxLength: 15, nullable: true),
                    Province = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Faults",
                columns: table => new
                {
                    FaultID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faults", x => x.FaultID);
                });

            migrationBuilder.CreateTable(
                name: "ItemTypes",
                columns: table => new
                {
                    ItemTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Model = table.Column<string>(maxLength: 30, nullable: false),
                    Manufacturer = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.ItemTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Resolutions",
                columns: table => new
                {
                    ResolutionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resolutions", x => x.ResolutionID);
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    RepairID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOpened = table.Column<DateTime>(nullable: false),
                    DateCompleted = table.Column<DateTime>(nullable: true),
                    CustomerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.RepairID);
                    table.ForeignKey(
                        name: "FK_Repairs_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Serial = table.Column<string>(maxLength: 50, nullable: false),
                    ItemTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK_Items_ItemTypes_ItemTypeID",
                        column: x => x.ItemTypeID,
                        principalTable: "ItemTypes",
                        principalColumn: "ItemTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairItems",
                columns: table => new
                {
                    RepairItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepairID = table.Column<int>(nullable: false),
                    ItemID = table.Column<int>(nullable: false),
                    DateRepaired = table.Column<DateTime>(nullable: true),
                    DateShipped = table.Column<DateTime>(nullable: true),
                    CourierTrackingID = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairItems", x => x.RepairItemID);
                    table.ForeignKey(
                        name: "FK_RepairItems_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairItems_Repairs_RepairID",
                        column: x => x.RepairID,
                        principalTable: "Repairs",
                        principalColumn: "RepairID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairItemFaults",
                columns: table => new
                {
                    RepairItemID = table.Column<int>(nullable: false),
                    FaultID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairItemFaults", x => new { x.RepairItemID, x.FaultID });
                    table.ForeignKey(
                        name: "FK_RepairItemFaults_Faults_FaultID",
                        column: x => x.FaultID,
                        principalTable: "Faults",
                        principalColumn: "FaultID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairItemFaults_RepairItems_RepairItemID",
                        column: x => x.RepairItemID,
                        principalTable: "RepairItems",
                        principalColumn: "RepairItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairItemResolutions",
                columns: table => new
                {
                    RepairItemID = table.Column<int>(nullable: false),
                    ResolutionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairItemResolutions", x => new { x.RepairItemID, x.ResolutionID });
                    table.ForeignKey(
                        name: "FK_RepairItemResolutions_RepairItems_RepairItemID",
                        column: x => x.RepairItemID,
                        principalTable: "RepairItems",
                        principalColumn: "RepairItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairItemResolutions_Resolutions_ResolutionID",
                        column: x => x.ResolutionID,
                        principalTable: "Resolutions",
                        principalColumn: "ResolutionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemTypeID",
                table: "Items",
                column: "ItemTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_RepairItemFaults_FaultID",
                table: "RepairItemFaults",
                column: "FaultID");

            migrationBuilder.CreateIndex(
                name: "IX_RepairItemResolutions_ResolutionID",
                table: "RepairItemResolutions",
                column: "ResolutionID");

            migrationBuilder.CreateIndex(
                name: "IX_RepairItems_ItemID",
                table: "RepairItems",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_RepairItems_RepairID",
                table: "RepairItems",
                column: "RepairID");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_CustomerID",
                table: "Repairs",
                column: "CustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepairItemFaults");

            migrationBuilder.DropTable(
                name: "RepairItemResolutions");

            migrationBuilder.DropTable(
                name: "Faults");

            migrationBuilder.DropTable(
                name: "RepairItems");

            migrationBuilder.DropTable(
                name: "Resolutions");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "ItemTypes");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
