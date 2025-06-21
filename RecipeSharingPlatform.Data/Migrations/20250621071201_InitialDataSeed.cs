using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeSharingPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd", 0, "ae0d7607-833c-419d-9ab2-42e466d252e3", "admin@recipesharing.com", true, false, null, "ADMIN@RECIPESHARING.COM", "ADMIN@RECIPESHARING.COM", "AQAAAAIAAYagAAAAEE4cZ2nrjR0BdOZPKZ7AnsdXzpX946BVAJcfrdEjpMGr5NqAJU/lHhgdRZh6vH/qyA==", null, false, "a862e0f9-d0d5-4366-a783-dc10f185c373", false, "admin@recipesharing.com" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Appetizer" },
                    { 2, "Main Dish" },
                    { 3, "Dessert" },
                    { 4, "Soup" },
                    { 5, "Salad" },
                    { 6, "Beverage" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CreatedOn", "ImageUrl", "Instructions", "Title" },
                values: new object[,]
                {
                    { 1, "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd", 1, new DateTime(2025, 6, 21, 10, 12, 0, 833, DateTimeKind.Local).AddTicks(5938), "https://www.unicornsinthekitchen.com/wp-content/uploads/2024/04/Bruschetta-sq-500x500.jpg", "Chop tomatoes, mix with basil and garlic, then spoon onto toasted bread.", "Classic Bruschetta" },
                    { 2, "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd", 2, new DateTime(2025, 6, 21, 10, 12, 0, 833, DateTimeKind.Local).AddTicks(5995), "https://feelgoodfoodie.net/wp-content/uploads/2025/04/Grilled-Salmon-09-500x500.jpg", "Season salmon with herbs and grill skin-side down for 6–8 minutes.", "Grilled Salmon" },
                    { 3, "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd", 3, new DateTime(2025, 6, 21, 10, 12, 0, 833, DateTimeKind.Local).AddTicks(5997), "https://www.cookingclassy.com/wp-content/uploads/2022/02/molten-lava-cake-17-500x500.jpg", "Prepare cake mix, bake at high heat for 12 min. Serve warm with ice cream.", "Chocolate Lava Cake" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
