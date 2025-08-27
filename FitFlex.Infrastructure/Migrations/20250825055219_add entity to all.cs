using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitFlex.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addentitytoall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Users",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "TrainerId",
                table: "Trainers",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Trainers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Trainers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Trainers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Trainers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Trainers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Trainers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Trainers");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Trainers",
                newName: "TrainerId");
        }
    }
}
