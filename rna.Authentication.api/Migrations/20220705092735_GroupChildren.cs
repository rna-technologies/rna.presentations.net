using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Application.Migrations
{
    public partial class GroupChildren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var query = @"[dbo].[GroupChildren] (@groupIds NVARCHAR(MAX)) 
RETURNS TABLE 
AS 
RETURN  
(	
	WITH GroupRelations (ID, SuperGroupID, Name, Type, Level) 
	AS	
	(	
		SELECT g.ID, g.SuperGroupID, g.Name, g.Type, 0 AS Level FROM dbo.[Group] AS g 
		WHERE  ID IN ( SELECT value FROM STRING_SPLIT(@groupIds,',') ) 
		UNION ALL 
		SELECT g.ID, g.SuperGroupID, g.Name, g.Type,Level + 1	
		FROM dbo.[Group] AS g	
		INNER JOIN GroupRelations AS d ON g.SuperGroupID = d.ID 
	) 
	SELECT * FROM GroupRelations AS r 
) ;".CREATE_FUNCTION();
            migrationBuilder.Sql(query);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
