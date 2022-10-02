USE [WavShare]
GO

/****** Object:  StoredProcedure [dbo].[GetAudioFiles]    Script Date: 10/2/2022 11:02:00 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		Ruby Labausa
-- Create date: 9/4/2022
-- Description:	GET all audio files
-- =============================================
CREATE OR ALTER         PROCEDURE [dbo].[GetAudioFiles] 
	-- Add the parameters for the stored procedure here
	@page_cursor INT = 0,
	@page_limit INT = 10,
	@sort_column VARCHAR(50) = 'AudioFileId',
	@sort_direction VARCHAR(4) = 'ASC',
	@file_id int = NULL,
	@file_name VARCHAR(100) = NULL,
	@file_uploaded_by VARCHAR(100) = NULL,
	@ASC VARCHAR(3) = 'ASC',
	@DESC VARCHAR(4) = 'DESC'
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		 [AudioFileId]
		,[AudioFileName]
		,[EncodedAudio]
		,[UploadedBy]
		,[UploadDate]
	FROM
		[dbo].[Audio_Files]
	WHERE
		(ISNULL(@file_id, '') = '' OR [AudioFileId] = @file_id)
		AND (ISNULL(@file_name, '') = '' OR [AudioFileName] LIKE '%' + @file_name + '%')
		AND (ISNULL(@file_uploaded_by, '') = '' OR [UploadedBy] LIKE '%' + @file_uploaded_by + '%')

	ORDER BY 
		CASE WHEN @sort_column = 'AudioFileId' AND @sort_direction = @ASC THEN [AudioFileId] END ASC,
		CASE WHEN @sort_column = 'AudioFileName' AND @sort_direction = @ASC THEN [AudioFileName] END ASC,
		CASE WHEN @sort_column = 'UploadedBy' AND @sort_direction = @ASC THEN [UploadedBy] END ASC,
		CASE WHEN @sort_column = 'UploadDate' AND @sort_direction = @ASC THEN [UploadDate] END ASC,

		CASE WHEN @sort_column = 'AudioFileId' AND @sort_direction = @DESC THEN [AudioFileId] END DESC,
		CASE WHEN @sort_column = 'AudioFileName' AND @sort_direction = @DESC THEN [AudioFileName] END DESC,
		CASE WHEN @sort_column = 'UploadedBy' AND @sort_direction = @DESC THEN [UploadedBy] END DESC,
		CASE WHEN @sort_column = 'UploadDate' AND @sort_direction = @DESC THEN [UploadDate] END DESC
	OFFSET (@page_cursor) * (@page_limit) ROWS
	FETCH NEXT @page_limit ROWS ONLY
	
END
GO


