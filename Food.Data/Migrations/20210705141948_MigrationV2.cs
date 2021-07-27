using Microsoft.EntityFrameworkCore.Migrations;

namespace Food.Data.Migrations
{
    public partial class MigrationV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Nutrition_NutritionId",
                table: "Food");

            migrationBuilder.AlterColumn<long>(
                name: "NutritionId",
                table: "Food",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NutritionId",
                table: "Bag",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.InsertData(
                table: "Nutrition",
                columns: new[] { "Id", "Fat", "FoodId", "SaturatedFat", "Sugar" },
                values: new object[,]
                {
                    { 1L, 3m, 0L, 2m, 2m },
                    { 2L, 4m, 0L, 2.5m, 2m },
                    { 3L, 2m, 0L, 1m, 2.5m },
                    { 4L, 9m, 0L, 4m, 3.5m },
                    { 5L, 8m, 0L, 5m, 2.7m }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "Surname", "UserName" },
                values: new object[,]
                {
                    { 2L, "John", "Smith", "Jonny" },
                    { 1L, "Fred", "Flintstone", "Fred" }
                });

            migrationBuilder.InsertData(
                table: "Bag",
                columns: new[] { "Id", "ConstituentsJSON", "Name", "NutritionId", "UserId" },
                values: new object[,]
                {
                    { 1L, null, "Milky Bag", 4L, 1L },
                    { 2L, null, "Mixed Bag", 5L, 2L }
                });

            migrationBuilder.InsertData(
                table: "Food",
                columns: new[] { "Id", "Name", "NutritionId", "UserId" },
                values: new object[,]
                {
                    { 1L, "Milky Way", 1L, 1L },
                    { 2L, "Malteser Bunny", 2L, 1L },
                    { 3L, "Twisted Creme", 3L, 2L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bag_NutritionId",
                table: "Bag",
                column: "NutritionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bag_Nutrition_NutritionId",
                table: "Bag",
                column: "NutritionId",
                principalTable: "Nutrition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Nutrition_NutritionId",
                table: "Food",
                column: "NutritionId",
                principalTable: "Nutrition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bag_Nutrition_NutritionId",
                table: "Bag");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_Nutrition_NutritionId",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Bag_NutritionId",
                table: "Bag");

            migrationBuilder.DeleteData(
                table: "Bag",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Bag",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Nutrition",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Nutrition",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Nutrition",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Nutrition",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Nutrition",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DropColumn(
                name: "NutritionId",
                table: "Bag");

            migrationBuilder.AlterColumn<long>(
                name: "NutritionId",
                table: "Food",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Nutrition_NutritionId",
                table: "Food",
                column: "NutritionId",
                principalTable: "Nutrition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
