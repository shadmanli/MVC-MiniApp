using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_MiniApp.Migrations
{
    /// <inheritdoc />
    public partial class creatingtablework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Work_Categories_CategoryId",
                table: "Work");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkImage_Work_WorkId",
                table: "WorkImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkImage",
                table: "WorkImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Work",
                table: "Work");

            migrationBuilder.RenameTable(
                name: "WorkImage",
                newName: "workImages");

            migrationBuilder.RenameTable(
                name: "Work",
                newName: "Works");

            migrationBuilder.RenameIndex(
                name: "IX_WorkImage_WorkId",
                table: "workImages",
                newName: "IX_workImages_WorkId");

            migrationBuilder.RenameIndex(
                name: "IX_Work_CategoryId",
                table: "Works",
                newName: "IX_Works_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_workImages",
                table: "workImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Works",
                table: "Works",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_workImages_Works_WorkId",
                table: "workImages",
                column: "WorkId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Categories_CategoryId",
                table: "Works",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_workImages_Works_WorkId",
                table: "workImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_Categories_CategoryId",
                table: "Works");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Works",
                table: "Works");

            migrationBuilder.DropPrimaryKey(
                name: "PK_workImages",
                table: "workImages");

            migrationBuilder.RenameTable(
                name: "Works",
                newName: "Work");

            migrationBuilder.RenameTable(
                name: "workImages",
                newName: "WorkImage");

            migrationBuilder.RenameIndex(
                name: "IX_Works_CategoryId",
                table: "Work",
                newName: "IX_Work_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_workImages_WorkId",
                table: "WorkImage",
                newName: "IX_WorkImage_WorkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Work",
                table: "Work",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkImage",
                table: "WorkImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Work_Categories_CategoryId",
                table: "Work",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkImage_Work_WorkId",
                table: "WorkImage",
                column: "WorkId",
                principalTable: "Work",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
