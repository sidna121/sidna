IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('AddHistory'))
	DROP PROCEDURE AddHistory
GO

CREATE PROCEDURE AddHistory
	@ID BIGINT,
	@Date NVARCHAR(MAX),
	@Str FLOAT (53),
	@End FLOAT (53),
	@Hig FLOAT (53),
	@Low FLOAT (53),
	@Avg FLOAT (53),
	@Type TINYINT ,
	@SlopeID BIGINT,
	@Slope FLOAT (53)

WITH ENCRYPTION
AS
BEGIN

	INSERT INTO [dbo].[History]([ID], [Date], [Str], [End], [Hig], [Low], [Avg], [Type], [Slope] )
	VALUES(@ID, CAST(@Date AS DateTime), @Str, @End, @Hig, @Low, @Avg, @Type,0)

	IF @SlopeID > 0
	BEGIN
		UPDATE dbo.History SET Slope = @Slope WHERE ID = @SlopeID  
	END
END
