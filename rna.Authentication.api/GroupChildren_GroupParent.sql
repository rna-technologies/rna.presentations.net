BEGIN TRANSACTION;
GO

CREATE FUNCTION [dbo].[GroupChildren] (@groupIds NVARCHAR(MAX)) 
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
) ;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220705092735_GroupChildren', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE FUNCTION [dbo].[GroupParents] (@groupIds NVARCHAR(MAX)) 
RETURNS TABLE 
AS 
RETURN 
(
	WITH GroupRelations(ID, SuperGroupID, Name, Type, Level) 
	AS 
	( 
		SELECT g.ID, g.SuperGroupID, g.Name, g.Type, 0 AS Level FROM dbo.[Group] AS g 
		WHERE g.ID IN (SELECT value FROM STRING_SPLIT(@groupIds,',')) 
		UNION ALL 
		SELECT g.ID, g.SuperGroupID, g.Name, g.Type, Level + 1 FROM dbo.[Group] AS g 
		INNER JOIN GroupRelations AS d ON g.ID = d.SuperGroupID 
	) 
	SELECT * FROM GroupRelations AS d 
) ;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220705092813_GroupParents', N'6.0.2');
GO

COMMIT;
GO

