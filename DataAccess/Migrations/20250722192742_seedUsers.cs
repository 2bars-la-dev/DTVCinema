using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 22, 19, 27, 39, 950, DateTimeKind.Utc).AddTicks(5418), "admin@dtv.com", true, "Admin", "$2a$11$2yOaX7nsbHmk.0Hi9eM9LeDo3vmZ5fZKnGHp74dxchldBY9Xk5E3C", "Admin" },
                    { 2, new DateTime(2025, 7, 22, 19, 27, 39, 950, DateTimeKind.Utc).AddTicks(5422), "manager@dtv.com", true, "Manager", "$2a$11$2yOaX7nsbHmk.0Hi9eM9LeDo3vmZ5fZKnGHp74dxchldBY9Xk5E3C", "Manager" },
                    { 3, new DateTime(2025, 7, 22, 19, 27, 39, 950, DateTimeKind.Utc).AddTicks(5425), "staff@dtv.com", true, "Staff", "$2a$11$2yOaX7nsbHmk.0Hi9eM9LeDo3vmZ5fZKnGHp74dxchldBY9Xk5E3C", "Staff" },
                    //user đã bị đổi thành customer@dtv
                    { 4, new DateTime(2025, 7, 22, 19, 27, 39, 950, DateTimeKind.Utc).AddTicks(5428), "user@dtv.com", true, "Customer", "$2a$11$2yOaX7nsbHmk.0Hi9eM9LeDo3vmZ5fZKnGHp74dxchldBY9Xk5E3C", "Customer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
