using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class ExtendedUserClassWithDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentCity",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InterestedIn",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LookingFor",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RaisedCity",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelationshipStatus",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CurrentCity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InterestedIn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LookingFor",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RaisedCity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RelationshipStatus",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "Users");
        }
    }
}
