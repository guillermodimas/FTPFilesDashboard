





CREATE PROCEDURE [dbo].[spInsertGeneratedLink]
     @LinkID int, --not used here
	 @RecordingID varchar(100),
	 @GeneratedLink varchar(500),
	 @CreatedOn datetime,
	 @CreatedBy varchar(100),
	 @ExpirationDate datetime
	
	
AS
begin
	set nocount on;
	
	BEGIN TRY
    BEGIN TRANSACTION
		
		
		BEGIN
			INSERT INTO [dbo].[GeneratedLinks]
			([RecordingID],[GeneratedLink],[CreatedOn],[CreatedBy],[ExpirationDate])			
			VALUES(@RecordingID, @GeneratedLink, @CreatedOn, @CreatedBy, @ExpirationDate)
		END
		
	   

    COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH


	

end