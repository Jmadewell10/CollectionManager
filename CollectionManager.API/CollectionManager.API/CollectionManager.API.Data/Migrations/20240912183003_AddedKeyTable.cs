using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectionManager.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedKeyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Key_Accounts_AccountId",
                table: "Key");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Key",
                table: "Key");

            migrationBuilder.RenameTable(
                name: "Key",
                newName: "Keys");

            migrationBuilder.RenameIndex(
                name: "IX_Key_AccountId",
                table: "Keys",
                newName: "IX_Keys_AccountId");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Keys",
                table: "Keys",
                column: "KeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Keys_Accounts_AccountId",
                table: "Keys",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keys_Accounts_AccountId",
                table: "Keys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Keys",
                table: "Keys");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Keys",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_Keys_AccountId",
                table: "Key",
                newName: "IX_Key_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Key",
                table: "Key",
                column: "KeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Key_Accounts_AccountId",
                table: "Key",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
