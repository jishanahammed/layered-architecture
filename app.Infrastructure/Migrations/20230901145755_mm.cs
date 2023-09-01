using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Infrastructure.Migrations
{
    public partial class mm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c5e174e-3b0e-446f-86af-483d56fd7210", "e0701771-8835-4f5a-8d6a-92cba6936ca8", "Admin", "ADMIN" },
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", "76699359-c5b6-480f-891a-cc32c691e1b6", "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreatedBy", "CreatedOn", "Email", "EmailConfirmed", "FullName", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Prefix", "SecurityStamp", "TwoFactorEnabled", "UpdatedBy", "UpdatedOn", "UserName", "UserType" },
                values: new object[,]
                {
                    { "0f04028e-587c-37ad-8b36-6dbd6a059fa10", 0, null, "616a2e8f-dc94-4576-8ec4-c9d75d1df6d9", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "cus.jishan@gmail.com", true, "System Engineers", true, false, null, "CUS.JISHAN@GMAIL.COM", "CUSTOMER", "AQAAAAEAACcQAAAAEE8d8uAFK+zBNJ3j+s3k5c6D+OqrJJqgpV0CF42z2UDwqm/kSD/LWNXN8OAx/56YHg==", "01840019826", true, null, "37QJAUUNCSSXNFFB6ZXI6OJLHSCS5J63", false, null, null, "Customer", 2 },
                    { "0f04028e-587c-47ad-8b36-6dbd6a059fa4", 0, null, "616a2e8f-dc94-4576-8ec4-c9d75d1df6d1", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jishan.bd46@gmail.com", true, "System Admin", true, false, null, "JISHAN.BD46@GMAIL.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEE8d8uAFK+zBNJ3j+s3k5c6D+OqrJJqgpV0CF42z2UDwqm/kSD/LWNXN8OAx/56YHg==", "01840019826", true, null, "37QJAUUNCSSXNFFB6ZXI6OJLHSCS5J6I", false, null, null, "administrator", 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", "0f04028e-587c-37ad-8b36-6dbd6a059fa10" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "0f04028e-587c-47ad-8b36-6dbd6a059fa4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", "0f04028e-587c-37ad-8b36-6dbd6a059fa10" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "0f04028e-587c-47ad-8b36-6dbd6a059fa4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0f04028e-587c-37ad-8b36-6dbd6a059fa10");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0f04028e-587c-47ad-8b36-6dbd6a059fa4");
        }
    }
}
