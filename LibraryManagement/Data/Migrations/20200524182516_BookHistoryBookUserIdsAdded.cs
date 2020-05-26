using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManagement.Migrations
{
    public partial class BookHistoryBookUserIdsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowHistories_Books_BookId",
                table: "BorrowHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowHistories_ApplicationUsers_CustomerId",
                table: "BorrowHistories");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "BorrowHistories",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "BorrowHistories",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowHistories_Books_BookId",
                table: "BorrowHistories",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowHistories_ApplicationUsers_CustomerId",
                table: "BorrowHistories",
                column: "CustomerId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowHistories_Books_BookId",
                table: "BorrowHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowHistories_ApplicationUsers_CustomerId",
                table: "BorrowHistories");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "BorrowHistories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "BorrowHistories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowHistories_Books_BookId",
                table: "BorrowHistories",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowHistories_ApplicationUsers_CustomerId",
                table: "BorrowHistories",
                column: "CustomerId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
