using Microsoft.EntityFrameworkCore.Migrations;

namespace CV_Ads_WebAPI.Migrations
{
    public partial class AdvertisementViews_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementView_Advertisements_AdvertisementId",
                table: "AdvertisementView");

            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementView_SmartDevices_SmartDeviceId",
                table: "AdvertisementView");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdvertisementView",
                table: "AdvertisementView");

            migrationBuilder.RenameTable(
                name: "AdvertisementView",
                newName: "AdvertisementViews");

            migrationBuilder.RenameIndex(
                name: "IX_AdvertisementView_SmartDeviceId",
                table: "AdvertisementViews",
                newName: "IX_AdvertisementViews_SmartDeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_AdvertisementView_AdvertisementId",
                table: "AdvertisementViews",
                newName: "IX_AdvertisementViews_AdvertisementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdvertisementViews",
                table: "AdvertisementViews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementViews_Advertisements_AdvertisementId",
                table: "AdvertisementViews",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementViews_SmartDevices_SmartDeviceId",
                table: "AdvertisementViews",
                column: "SmartDeviceId",
                principalTable: "SmartDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementViews_Advertisements_AdvertisementId",
                table: "AdvertisementViews");

            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementViews_SmartDevices_SmartDeviceId",
                table: "AdvertisementViews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdvertisementViews",
                table: "AdvertisementViews");

            migrationBuilder.RenameTable(
                name: "AdvertisementViews",
                newName: "AdvertisementView");

            migrationBuilder.RenameIndex(
                name: "IX_AdvertisementViews_SmartDeviceId",
                table: "AdvertisementView",
                newName: "IX_AdvertisementView_SmartDeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_AdvertisementViews_AdvertisementId",
                table: "AdvertisementView",
                newName: "IX_AdvertisementView_AdvertisementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdvertisementView",
                table: "AdvertisementView",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementView_Advertisements_AdvertisementId",
                table: "AdvertisementView",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementView_SmartDevices_SmartDeviceId",
                table: "AdvertisementView",
                column: "SmartDeviceId",
                principalTable: "SmartDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
