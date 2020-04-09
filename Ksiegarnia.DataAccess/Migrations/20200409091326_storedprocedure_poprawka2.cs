using Microsoft.EntityFrameworkCore.Migrations;

namespace Ksiegarnia.DataAccess.Migrations
{
    public partial class storedprocedure_poprawka2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROC usp_GetOkladki 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.Okladki 
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_GetOkladka 
                                    @Id int 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.Okladki  WHERE  (Id = @Id) 
                                    END ");

            migrationBuilder.Sql(@"CREATE PROC usp_UpdateOkladka
	                                @Id int,
	                                @Nazwa varchar(100)
                                    AS 
                                    BEGIN 
                                     UPDATE dbo.Okladki
                                     SET  Nazwa = @Nazwa
                                     WHERE  Id = @Id
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_DeleteOkladka
	                                @Id int
                                    AS 
                                    BEGIN 
                                     DELETE FROM dbo.Okladki
                                     WHERE  Id = @Id
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_CreateOkladka
                                   @Nazwa varchar(100)
                                   AS 
                                   BEGIN 
                                    INSERT INTO dbo.Okladki(Nazwa)
                                    VALUES (@Nazwa)
                                   END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetOkladki");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetOkladka");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_UpdateOkladka");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_DeleteOkladka");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_CreateOkladka");
        }
    }
}
