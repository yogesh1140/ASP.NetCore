﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamuraiApp.Data.Migrations
{
    public partial class UpdatedContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "SecretIdentity",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "SecretIdentity",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "BetterName_Created",
                table: "Samurais",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "GivenName",
                table: "Samurais",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BetterName_LastModified",
                table: "Samurais",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SurName",
                table: "Samurais",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Samurais",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Samurais",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "SamuraiBattle",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "SamuraiBattle",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Quotes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Quotes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Battles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Battles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "SecretIdentity");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "SecretIdentity");

            migrationBuilder.DropColumn(
                name: "BetterName_Created",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "GivenName",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "BetterName_LastModified",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "SurName",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "SamuraiBattle");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "SamuraiBattle");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Battles");
        }
    }
}
