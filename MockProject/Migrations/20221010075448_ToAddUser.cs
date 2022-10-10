using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MockProject.Migrations
{
	public partial class ToAddUser : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("INSERT INTO AspNetRoles" +
				"(Id,Name,NormalizedName,ConcurrencyStamp) " +
				"Values ('10ec2761-9882-4997-b666-970ea36df8c9','Admin','Admin','87a74b68-9a52-4231-b9c8-89422d7a5ba3')");


			//hashPassword : Abcde@123
			migrationBuilder.Sql("INSERT INTO AspNetUsers" +
				"(Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,Discriminator,Name)" +
				"Values ('9bc3c7b6-cf05-4f45-946c-2f11b72725d5','trunghauchannhan@gmail.com','TRUNGHAUCHANNHAN@GMAIL.COM','trunghauchannhan@gmail.com','TRUNGHAUCHANNHAN@GMAIL.COM',1,'AQAAAAEAACcQAAAAECo5gbaW9s60nIwYdaoZfaX5TeZtJ+qN8BWXTqx4JM5it9aJXU9XVUWHcX4NeivbEA==','3OQ5JBOLERAK6NWRZHGVP2SGQXFQP73W','c032349d-e3ed-494b-ab30-45aef25bf231','0123456789',0,0,NULL,1,0,'ApplicationUser','NTH')");

			migrationBuilder.Sql("INSERT INTO AspNetUserRoles" +
				"(UserId,RoleId) " +
				"Values ('9bc3c7b6-cf05-4f45-946c-2f11b72725d5','10ec2761-9882-4997-b666-970ea36df8c9')");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("DELETE AspNetUserRoles" +
								"WHERE UserId= '9bc3c7b6-cf05-4f45-946c-2f11b72725d5' and RoleId='10ec2761-9882-4997-b666-970ea36df8c9'");
			migrationBuilder.Sql("DELETE AspNetRoles" +
								"WHERE Id= '10ec2761-9882-4997-b666-970ea36df8c9'");
			migrationBuilder.Sql("DELETE AspNetUsers" +
								"WHERE Id= '9bc3c7b6-cf05-4f45-946c-2f11b72725d5'");
		}
	}
}
