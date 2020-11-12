using Microsoft.EntityFrameworkCore.Migrations;

namespace CV_Ads_WebAPI.Migrations
{
    public partial class limits_tables_renamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HumanLimit_Advertisements_AdvertisementId",
                table: "HumanLimit");

            migrationBuilder.DropForeignKey(
                name: "FK_TimePeriodLimit_Advertisements_AdvertisementId",
                table: "TimePeriodLimit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimePeriodLimit",
                table: "TimePeriodLimit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HumanLimit",
                table: "HumanLimit");

            migrationBuilder.RenameTable(
                name: "TimePeriodLimit",
                newName: "TimePeriodLimits");

            migrationBuilder.RenameTable(
                name: "HumanLimit",
                newName: "HumanLimits");

            migrationBuilder.RenameIndex(
                name: "IX_TimePeriodLimit_AdvertisementId",
                table: "TimePeriodLimits",
                newName: "IX_TimePeriodLimits_AdvertisementId");

            migrationBuilder.RenameIndex(
                name: "IX_HumanLimit_AdvertisementId",
                table: "HumanLimits",
                newName: "IX_HumanLimits_AdvertisementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimePeriodLimits",
                table: "TimePeriodLimits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HumanLimits",
                table: "HumanLimits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HumanLimits_Advertisements_AdvertisementId",
                table: "HumanLimits",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimePeriodLimits_Advertisements_AdvertisementId",
                table: "TimePeriodLimits",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HumanLimits_Advertisements_AdvertisementId",
                table: "HumanLimits");

            migrationBuilder.DropForeignKey(
                name: "FK_TimePeriodLimits_Advertisements_AdvertisementId",
                table: "TimePeriodLimits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimePeriodLimits",
                table: "TimePeriodLimits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HumanLimits",
                table: "HumanLimits");

            migrationBuilder.RenameTable(
                name: "TimePeriodLimits",
                newName: "TimePeriodLimit");

            migrationBuilder.RenameTable(
                name: "HumanLimits",
                newName: "HumanLimit");

            migrationBuilder.RenameIndex(
                name: "IX_TimePeriodLimits_AdvertisementId",
                table: "TimePeriodLimit",
                newName: "IX_TimePeriodLimit_AdvertisementId");

            migrationBuilder.RenameIndex(
                name: "IX_HumanLimits_AdvertisementId",
                table: "HumanLimit",
                newName: "IX_HumanLimit_AdvertisementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimePeriodLimit",
                table: "TimePeriodLimit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HumanLimit",
                table: "HumanLimit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HumanLimit_Advertisements_AdvertisementId",
                table: "HumanLimit",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimePeriodLimit_Advertisements_AdvertisementId",
                table: "TimePeriodLimit",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
