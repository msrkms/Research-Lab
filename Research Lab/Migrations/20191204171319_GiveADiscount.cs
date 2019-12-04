using Microsoft.EntityFrameworkCore.Migrations;

namespace Research_Lab.Migrations
{
    public partial class GiveADiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"CREATE PROCEDURE [dbo].[GiveDiscount]
@Id int,@Cupon varchar(50)
AS

declare @totalDiscount float;
declare @totalcost float;
select @totalcost= totalCost from LabUseCosts where id=@Id

		BEGIN
		if(@Cupon='Guest') 
		set @totalDiscount=@totalcost;		
		else if(@Cupon='Teacher') 
		set @totalDiscount=@totalcost*.5;
		else if(@Cupon='Member') 
		set @totalDiscount=@totalcost*.1;
		else 
		set @totalDiscount=0;
		END

		BEGIN
		 update LabUseCosts set totalCost=(@totalcost-@totalDiscount) WHERE id=@Id;
		END";
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
