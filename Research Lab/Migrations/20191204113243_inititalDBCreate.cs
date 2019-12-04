using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Research_Lab.Migrations
{
    public partial class inititalDBCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    AppUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    IsVerified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.AppUserId);
                    table.ForeignKey(
                        name: "FK_AppUser_UserRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UserRole",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingInfo",
                columns: table => new
                {
                    Biid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cid = table.Column<int>(nullable: false),
                    AppUserId = table.Column<int>(nullable: false),
                    BookingDate = table.Column<DateTime>(nullable: false),
                    BookingStartTime = table.Column<DateTime>(nullable: false),
                    BookingEndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingInfo", x => x.Biid);
                    table.ForeignKey(
                        name: "FK_BookingInfo_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabUseCosts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UseDate = table.Column<DateTime>(nullable: false),
                    hour = table.Column<int>(nullable: false),
                    minute = table.Column<int>(nullable: false),
                    totalCost = table.Column<double>(nullable: false),
                    appUserID = table.Column<int>(nullable: false),
                    CId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabUseCosts", x => x.id);
                    table.ForeignKey(
                        name: "FK_LabUseCosts_AppUser_appUserID",
                        column: x => x.appUserID,
                        principalTable: "AppUser",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearchLab",
                columns: table => new
                {
                    Rlid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LabName = table.Column<string>(nullable: true),
                    LabLoction = table.Column<string>(nullable: true),
                    LabAssistant = table.Column<int>(nullable: false),
                    LabAssistantNavigationAppUserId = table.Column<int>(nullable: true),
                    LabUseCostid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchLab", x => x.Rlid);
                    table.ForeignKey(
                        name: "FK_ResearchLab_AppUser_LabAssistantNavigationAppUserId",
                        column: x => x.LabAssistantNavigationAppUserId,
                        principalTable: "AppUser",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResearchLab_LabUseCosts_LabUseCostid",
                        column: x => x.LabUseCostid,
                        principalTable: "LabUseCosts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Computer",
                columns: table => new
                {
                    Cid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsAvailable = table.Column<bool>(nullable: false),
                    LabId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computer", x => x.Cid);
                    table.ForeignKey(
                        name: "FK_Computer_ResearchLab_LabId",
                        column: x => x.LabId,
                        principalTable: "ResearchLab",
                        principalColumn: "Rlid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabCostRates",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Rlid = table.Column<int>(nullable: false),
                    costperminitue = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabCostRates", x => x.id);
                    table.ForeignKey(
                        name: "FK_LabCostRates_ResearchLab_Rlid",
                        column: x => x.Rlid,
                        principalTable: "ResearchLab",
                        principalColumn: "Rlid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_RoleId",
                table: "AppUser",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingInfo_AppUserId",
                table: "BookingInfo",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingInfo_Cid",
                table: "BookingInfo",
                column: "Cid");

            migrationBuilder.CreateIndex(
                name: "IX_Computer_LabId",
                table: "Computer",
                column: "LabId");

            migrationBuilder.CreateIndex(
                name: "IX_LabCostRates_Rlid",
                table: "LabCostRates",
                column: "Rlid");

            migrationBuilder.CreateIndex(
                name: "IX_LabUseCosts_CId",
                table: "LabUseCosts",
                column: "CId");

            migrationBuilder.CreateIndex(
                name: "IX_LabUseCosts_appUserID",
                table: "LabUseCosts",
                column: "appUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchLab_LabAssistantNavigationAppUserId",
                table: "ResearchLab",
                column: "LabAssistantNavigationAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchLab_LabUseCostid",
                table: "ResearchLab",
                column: "LabUseCostid");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingInfo_Computer_Cid",
                table: "BookingInfo",
                column: "Cid",
                principalTable: "Computer",
                principalColumn: "Cid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LabUseCosts_Computer_CId",
                table: "LabUseCosts",
                column: "CId",
                principalTable: "Computer",
                principalColumn: "Cid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUser_UserRole_RoleId",
                table: "AppUser");

            migrationBuilder.DropForeignKey(
                name: "FK_LabUseCosts_AppUser_appUserID",
                table: "LabUseCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ResearchLab_AppUser_LabAssistantNavigationAppUserId",
                table: "ResearchLab");

            migrationBuilder.DropForeignKey(
                name: "FK_LabUseCosts_Computer_CId",
                table: "LabUseCosts");

            migrationBuilder.DropTable(
                name: "BookingInfo");

            migrationBuilder.DropTable(
                name: "LabCostRates");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "Computer");

            migrationBuilder.DropTable(
                name: "ResearchLab");

            migrationBuilder.DropTable(
                name: "LabUseCosts");
        }
    }
}
