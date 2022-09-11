USE [WavShare]
GO

/****** Object:  StoredProcedure [dbo].[UpdateAudioFile]    Script Date: 9/11/2022 11:44:00 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Ruby Labausa
-- Create date: 9/11/2022
-- Description:	Update audio file
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[UpdateAudioFile] 
	-- Add the parameters for the stored procedure here
	@audio_file_id INT, 
	@audio_file_name VARCHAR(100),
	@encoded_audio VARCHAR(MAX),
	@uploaded_by VARCHAR(100)
AS
BEGIN

	-- Return the number of rows affected
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	UPDATE 
		[dbo].AUDIO_FILES
	SET
		[audioFileName] = @audio_file_name,
		[EncodedAudio] = @encoded_audio,
		[UploadedBy] = @uploaded_by,
		[UploadDate] = GETDATE()

	WHERE 
		[AudioFileId] = @audio_file_id
END
GO


