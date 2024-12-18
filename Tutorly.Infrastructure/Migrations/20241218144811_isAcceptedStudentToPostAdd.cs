using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutorly.Infrastructure.Migrations
{
    public partial class isAcceptedStudentToPostAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "PostsStudents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "PostsStudents");
        }
    }
}
