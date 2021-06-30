using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class RandomizedStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaximumAbilityPower",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumAttack",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumDefense",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumMagicResist",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumSpeed",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimumAbilityPower",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimumAttack",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimumDefense",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimumMagicResist",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimumSpeed",
                table: "Items",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumAbilityPower",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MaximumAttack",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MaximumDefense",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MaximumMagicResist",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MaximumSpeed",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MinimumAbilityPower",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MinimumAttack",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MinimumDefense",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MinimumMagicResist",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MinimumSpeed",
                table: "Items");
        }
    }
}
