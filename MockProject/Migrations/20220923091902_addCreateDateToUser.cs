using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MockProject.Migrations
{
    public partial class addCreateDateToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AddColumn<DateTime>(
				name: "DateCreated",
				table: "AspNetUsers",
				nullable: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "DateCreated",
				table: "AspNetUsers");
		}
	}
}
