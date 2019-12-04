using Microsoft.EntityFrameworkCore.Migrations;

namespace Research_Lab.Migrations
{
    public partial class insertFiveComputerProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[InsertFiveComputer]
                     @LabID int,@IsAvailable bit
                      AS
		                declare @initalStart int = 0;
		                WHILE @initalStart < 5  
		                    BEGIN 
		                        set @initalStart=@initalStart+1;
		                        INSERT INTO [dbo].[Computer]
                                    ([IsAvailable],[LabID])
		                        VALUES
                                     (@IsAvailable,@LabID);
		                    END;";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
