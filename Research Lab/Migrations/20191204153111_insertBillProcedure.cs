using Microsoft.EntityFrameworkCore.Migrations;

namespace Research_Lab.Migrations
{
    public partial class insertBillProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[InsertBill]
                        @UseDate datetime2(7),@hour int,@min int,@total float,@appUserID int,@ComputerID int
                            AS

                    		BEGIN 
	
                                INSERT INTO [dbo].[LabUseCosts]
           ([UseDate]
           ,[hour]
           ,[minute]
           ,[totalCost]
           ,[appUserID]
           ,[CId])
     VALUES
           (@UseDate
           ,@hour
           ,@min
           ,@total
           ,@appUserID
           ,@ComputerID)
		END;";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
