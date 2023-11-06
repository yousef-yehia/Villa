using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Villa_API.Migrations
{
    /// <inheritdoc />
    public partial class addVillaIDtoVillaNumbers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 6, 6, 26, 9, 573, DateTimeKind.Local).AddTicks(4692));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 6, 6, 26, 9, 573, DateTimeKind.Local).AddTicks(4739));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 6, 6, 26, 9, 573, DateTimeKind.Local).AddTicks(4741));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 6, 6, 26, 9, 573, DateTimeKind.Local).AddTicks(4743));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 6, 6, 26, 9, 573, DateTimeKind.Local).AddTicks(4746));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 6, 6, 20, 26, 434, DateTimeKind.Local).AddTicks(1979));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 6, 6, 20, 26, 434, DateTimeKind.Local).AddTicks(2026));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 6, 6, 20, 26, 434, DateTimeKind.Local).AddTicks(2028));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 6, 6, 20, 26, 434, DateTimeKind.Local).AddTicks(2031));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 6, 6, 20, 26, 434, DateTimeKind.Local).AddTicks(2033));
        }
    }
}
