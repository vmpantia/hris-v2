CREATE PROCEDURE [dbo].[DeleteContactsByInternalID]
	@InternalID UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM Contact WHERE InternalID = @InternalID
END
