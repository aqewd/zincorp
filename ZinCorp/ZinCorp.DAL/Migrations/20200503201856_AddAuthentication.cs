using Microsoft.EntityFrameworkCore.Migrations;

namespace ZinCorp.DAL.Migrations
{
    public partial class AddAuthentication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HashPassword",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Salt",
                table: "Customers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashPassword",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Customers");
        }
    }
}
