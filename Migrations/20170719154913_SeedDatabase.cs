using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vega.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Makes (Name) VALUES ('Ford') ");
            migrationBuilder.Sql("Insert into Makes (Name) VALUES ('Fiat') ");
            migrationBuilder.Sql("Insert into Makes (Name) VALUES ('Citreon') ");

            migrationBuilder.Sql("Insert into Model (Name, MakeId) VALUES ('Focus', (SELECT ID from Makes where Name = 'Ford')) ");
            migrationBuilder.Sql("Insert into Model (Name, MakeId) VALUES ('Fiesta', (SELECT ID from Makes where Name = 'Ford')) ");
            migrationBuilder.Sql("Insert into Model (Name, MakeId) VALUES ('Ka', (SELECT ID from Makes where Name = 'Ford')) ");

            migrationBuilder.Sql("Insert into Model (Name, MakeId) VALUES ('Punto', (SELECT ID from Makes where Name = 'Fiat')) ");
            migrationBuilder.Sql("Insert into Model (Name, MakeId) VALUES ('Panda', (SELECT ID from Makes where Name = 'Fiat')) ");
            migrationBuilder.Sql("Insert into Model (Name, MakeId) VALUES ('Speed', (SELECT ID from Makes where Name = 'Fiat')) ");

            migrationBuilder.Sql("Insert into Model (Name, MakeId) VALUES ('C3', (SELECT ID from Makes where Name = 'Citreon')) ");
            migrationBuilder.Sql("Insert into Model (Name, MakeId) VALUES ('Picasso', (SELECT ID from Makes where Name = 'Citreon')) ");
            migrationBuilder.Sql("Insert into Model (Name, MakeId) VALUES ('Art', (SELECT ID from Makes where Name = 'Citreon')) ");



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("Delete from Model");
            migrationBuilder.Sql("Delete from Makes");

        }
    }
}
