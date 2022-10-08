
-- =============================================
-- Author:		Ruby Labausa
-- Create date: 9/9/2022
-- Description:	
-- =============================================
CREATE   PROCEDURE [dbo].[DeleteAudioFile] 
	-- Add the parameters for the stored procedure here
	@audio_file_id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	DELETE 
	FROM 
		[dbo].[AUDIO_FILES]
	WHERE 
		[AudioFileId] = @audio_file_id

END
