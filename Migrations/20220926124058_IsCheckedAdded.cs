using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersWebApp.Migrations
{
    public partial class IsCheckedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "AspNetUsers");
        }
    }
}
