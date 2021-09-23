






CREATE PROCEDURE [dbo].[spUpdateGeneratedLink]
     @LinkID int, 
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
			UPDATE [dbo].[GeneratedLinks]
			SET [GeneratedLink] = @GeneratedLink,
			    [CreatedOn] = @CreatedOn,
				[CreatedBy] = @CreatedBy,
				[ExpirationDate] = @ExpirationDate			
			WHERE [LinkID] = @LinkID
		END
		
	   

    COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH


	

end