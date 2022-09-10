USE [WavShare]
GO

/****** Object:  StoredProcedure [dbo].[DeleteAudioFile]    Script Date: 9/9/2022 6:58:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Ruby Labausa
-- Create date: 9/9/2022
-- Description:	
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[DeleteAudioFile] 
	-- Add the parameters for the stored procedure here
	@audio_file_id INT
AS
BEGIN
	-- Return number of rows affected by statement
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	DELETE 
	FROM 
		[dbo].[AUDIO_FILES]
	WHERE 
		[AudioFileId] = @audio_file_id

END
GO


