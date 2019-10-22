using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizMakeFree.Migrations
{
    public partial class ChangeNameLastModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModifedDate",
                table: "Results",
                newName: "LastModifiedDate");

            migrationBuilder.RenameColumn(
                name: "LastModifedDate",
                table: "Quizzes",
                newName: "LastModifiedDate");

            migrationBuilder.RenameColumn(
                name: "LastModifedDate",
                table: "Questions",
                newName: "LastModifiedDate");

            migrationBuilder.RenameColumn(
                name: "LastModifedDate",
                table: "Answers",
                newName: "LastModifiedDate");

            migrationBuilder.AlterColumn<int>(
                name: "ViewCount",
                table: "Quizzes",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "Results",
                newName: "LastModifedDate");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "Quizzes",
                newName: "LastModifedDate");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "Questions",
                newName: "LastModifedDate");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "Answers",
                newName: "LastModifedDate");

            migrationBuilder.AlterColumn<string>(
                name: "ViewCount",
                table: "Quizzes",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
