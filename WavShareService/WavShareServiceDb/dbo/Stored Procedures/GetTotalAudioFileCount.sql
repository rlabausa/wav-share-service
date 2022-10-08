


-- =============================================
-- Author:		Ruby Labausa
-- Create date: 9/6/2022
-- Description:	Count the total number of records for given search criteria.
-- =============================================
CREATE   PROCEDURE [dbo].[GetTotalAudioFileCount] 
	-- Add the parameters for the stored procedure here
	@file_id INT = NULL,
	@file_name VARCHAR(100) = NULL, 
	@file_uploaded_by VARCHAR(100) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT (*) 
	FROM 
		[dbo].AUDIO_FILES
	WHERE 
		(ISNULL(@file_id, '') = '' OR [AudioFileId] = @file_id) 
		AND (ISNULL(@file_name, '') = '' OR [AudioFileName] LIKE '%' + @file_name + '%')
		AND (ISNULL(@file_uploaded_by, '') = '' OR [UploadedBy] LIKE '%' + @file_uploaded_by + '%')
END
