using Microsoft.EntityFrameworkCore.Migrations;

namespace FloraEducationAPI.Migrations
{
    public partial class RemoveUserFromBadgeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Badges_Users_OwnerUsername",
                table: "Badges");

            migrationBuilder.DropIndex(
                name: "IX_Badges_OwnerUsername",
                table: "Badges");

            migrationBuilder.DropColumn(
                name: "OwnerUsername",
                table: "Badges");

            migrationBuilder.DropColumn(
                name: "PlantName",
                table: "Badges");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Badges",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Badges");

            migrationBuilder.AddColumn<string>(
                name: "OwnerUsername",
                table: "Badges",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlantName",
                table: "Badges",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Badges_OwnerUsername",
                table: "Badges",
                column: "OwnerUsername");

            migrationBuilder.AddForeignKey(
                name: "FK_Badges_Users_OwnerUsername",
                table: "Badges",
                column: "OwnerUsername",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
