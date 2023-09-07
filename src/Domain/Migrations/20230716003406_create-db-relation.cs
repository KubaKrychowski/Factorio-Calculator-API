using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class createdbrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "Ingregients",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingregients_RecipeId",
                table: "Ingregients",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingregients_Recipes_RecipeId",
                table: "Ingregients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingregients_Recipes_RecipeId",
                table: "Ingregients");

            migrationBuilder.DropIndex(
                name: "IX_Ingregients_RecipeId",
                table: "Ingregients");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Ingregients");
        }
    }
}
