using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedUsers2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 22, 20, 14, 39, 74, DateTimeKind.Utc).AddTicks(7478), "$2a$11$H0v.RNqYSPfRrleWiBOXUe3GdN5ifTwBqhBzp5SKjxyE.BAx.B5sW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 22, 20, 14, 39, 74, DateTimeKind.Utc).AddTicks(7484), "$2a$11$H0v.RNqYSPfRrleWiBOXUe3GdN5ifTwBqhBzp5SKjxyE.BAx.B5sW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 22, 20, 14, 39, 74, DateTimeKind.Utc).AddTicks(7489), "$2a$11$H0v.RNqYSPfRrleWiBOXUe3GdN5ifTwBqhBzp5SKjxyE.BAx.B5sW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 22, 20, 14, 39, 74, DateTimeKind.Utc).AddTicks(7494), "$2a$11$H0v.RNqYSPfRrleWiBOXUe3GdN5ifTwBqhBzp5SKjxyE.BAx.B5sW" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 22, 19, 27, 39, 950, DateTimeKind.Utc).AddTicks(5418), "$2a$11$2yOaX7nsbHmk.0Hi9eM9LeDo3vmZ5fZKnGHp74dxchldBY9Xk5E3C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 22, 19, 27, 39, 950, DateTimeKind.Utc).AddTicks(5422), "$2a$11$2yOaX7nsbHmk.0Hi9eM9LeDo3vmZ5fZKnGHp74dxchldBY9Xk5E3C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 22, 19, 27, 39, 950, DateTimeKind.Utc).AddTicks(5425), "$2a$11$2yOaX7nsbHmk.0Hi9eM9LeDo3vmZ5fZKnGHp74dxchldBY9Xk5E3C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 22, 19, 27, 39, 950, DateTimeKind.Utc).AddTicks(5428), "$2a$11$2yOaX7nsbHmk.0Hi9eM9LeDo3vmZ5fZKnGHp74dxchldBY9Xk5E3C" });
        }
    }
}
