-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ruby Labausa
-- Create date: 9/4/2022
-- Description:	GET all audio files
-- =============================================
CREATE PROCEDURE GetAudioFiles 
	-- Add the parameters for the stored procedure here
	@page_cursor int = 1,
	@page_limit int = 10,
	@sort_column varchar(50) = 'AudioFileId',
	@sort_direction varchar(4) = 'ASC',
	@search_file_id int = NULL,
	@search_file_name varchar(255) = NULL,
	@search_file_uploaded_by varchar(100) = NULL,
	@ASC varchar(3) = 'ASC',
	@DESC varchar(4) = 'DESC'
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
		1 = 1
		AND [AudioFileId] = @search_file_id OR ISNULL(@search_file_id, '') = ''
		AND [AudioFileName] LIKE '%' + @search_file_name + '%' OR ISNULL(@search_file_name, '') = ''
		AND [UploadedBy] LIKE '%' + @search_file_uploaded_by + '%' OR ISNULL(@search_file_uploaded_by, '') = ''

	ORDER BY 
		CASE WHEN @sort_column = 'AudioFileId' AND @sort_direction = @ASC THEN [AudioFileId] END ASC,
		CASE WHEN @sort_column = 'AudioFileName' AND @sort_direction = @ASC THEN [AudioFileName] END ASC,
		CASE WHEN @sort_column = 'UploadedBy' AND @sort_direction = @ASC THEN [UploadedBy] END ASC,
		CASE WHEN @sort_column = 'UploadDate' AND @sort_direction = @ASC THEN [UploadDate] END ASC,

		CASE WHEN @sort_column = 'AudioFileId' AND @sort_direction = @DESC THEN [AudioFileId] END DESC,
		CASE WHEN @sort_column = 'AudioFileName' AND @sort_direction = @DESC THEN [AudioFileName] END DESC,
		CASE WHEN @sort_column = 'UploadedBy' AND @sort_direction = @DESC THEN [UploadedBy] END DESC,
		CASE WHEN @sort_column = 'UploadDate' AND @sort_direction = @DESC THEN [UploadDate] END DESC
	OFFSET (@page_cursor -1) * (@page_limit) ROWS
	FETCH NEXT @page_limit ROWS ONLY
		
END
GO
