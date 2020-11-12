using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CV_Ads_WebAPI.Migrations
{
    public partial class models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserIdentities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Login = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserIdentities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_UserIdentities_Id",
                        column: x => x.Id,
                        principalTable: "UserIdentities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    LastPaidDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_UserIdentities_Id",
                        column: x => x.Id,
                        principalTable: "UserIdentities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    LastWithdrawedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partners_UserIdentities_Id",
                        column: x => x.Id,
                        principalTable: "UserIdentities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Advertisement",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    PictureExtension = table.Column<string>(nullable: false),
                    ViewsCount = table.Column<long>(nullable: false),
                    ViewsLimit = table.Column<long>(nullable: false),
                    CountryScope = table.Column<string>(nullable: true),
                    CityScope = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisement_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmartDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Mode = table.Column<int>(nullable: false),
                    IsTurnedOn = table.Column<bool>(nullable: false),
                    IsCaching = table.Column<bool>(nullable: false),
                    PartnerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmartDevices_UserIdentities_Id",
                        column: x => x.Id,
                        principalTable: "UserIdentities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SmartDevices_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "HumanLimit",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    MinAge = table.Column<int>(nullable: true),
                    MaxAge = table.Column<int>(nullable: true),
                    AdvertisementId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumanLimit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HumanLimit_Advertisement_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimePeriodLimit",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FromInMinutes = table.Column<int>(nullable: false),
                    ToInMinutes = table.Column<int>(nullable: false),
                    AdvertisementId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimePeriodLimit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimePeriodLimit_Advertisement_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertisementView",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    AudienceCount = table.Column<int>(nullable: false),
                    AdvertisementId = table.Column<Guid>(nullable: true),
                    SmartDeviceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementView", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertisementView_Advertisement_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AdvertisementView_SmartDevices_SmartDeviceId",
                        column: x => x.SmartDeviceId,
                        principalTable: "SmartDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_CustomerId",
                table: "Advertisement",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementView_AdvertisementId",
                table: "AdvertisementView",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementView_SmartDeviceId",
                table: "AdvertisementView",
                column: "SmartDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_HumanLimit_AdvertisementId",
                table: "HumanLimit",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartDevices_PartnerId",
                table: "SmartDevices",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TimePeriodLimit_AdvertisementId",
                table: "TimePeriodLimit",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserIdentities_Login",
                table: "UserIdentities",
                column: "Login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AdvertisementView");

            migrationBuilder.DropTable(
                name: "HumanLimit");

            migrationBuilder.DropTable(
                name: "TimePeriodLimit");

            migrationBuilder.DropTable(
                name: "SmartDevices");

            migrationBuilder.DropTable(
                name: "Advertisement");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "UserIdentities");
        }
    }
}
