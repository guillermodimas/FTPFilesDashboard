CREATE PROCEDURE [dbo].[spLoadZoomRecordingsByKeyword]
@keyword varchar(50)
AS
begin
	  
	  SELECT R.[id] as RecordingID
	  ,MeetingRecordID = M.uuid
	  ,M.id as MeetingID
	  ,M.host_id as HostID
	  ,Host = Users.first_name + ' ' + Users.last_name 
	  ,M.topic as Topic
	  ,Convert(date, m.start_time) as MeetingDate
      ,[BackupLocation]
      ,[LastModifield]
	  ,R.file_size as FileSize
	  ,CAST(R.file_size / 1048576.0 AS DECIMAL(10, 2)) as FileSizeMB
	  ,R.file_type as FileType
	  ,Links.LinkID 
	  ,Links.GeneratedLink 
	  ,Links.CreatedOn as LinkCreatedOn
	  ,Links.ExpirationDate as LinkExpirationDate
  FROM [ZoomDownloads].[dbo].[RecordingFiles]  as R WITH (NOLOCK)
  INNER JOIN [ZoomDownloads].[dbo].[MeetingWithDownloads] as M WITH (NOLOCK)
  ON R.meeting_id = M.uuid
  LEFT JOIN dbo.Users WITH (NOLOCK)
  ON M.host_id = dbo.Users.id
  LEFT JOIN dbo.GeneratedLinks as Links WITH (NOLOCK)
  ON R.id = Links.RecordingID
  WHERE ((RTRIM(LTRIM(M.id)) LIKE  '%' + @keyword + '%')  or ((RTRIM(LTRIM(Users.first_name)) + ' ' + RTRIM(LTRIM(Users.last_name))) LIKE  '%' + @keyword + '%')  or (RTRIM(LTRIM(M.topic)) LIKE  '%' + @keyword + '%') )
  ORDER BY M.id, Convert(date, m.start_time) DESC


end