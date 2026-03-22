using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectionManager.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCollectionName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CollectionName",
                table: "Collections",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectionName",
                table: "Collections");
        }
    }
}
