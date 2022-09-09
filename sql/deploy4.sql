USE [WavShare]
GO

/****** Object:  StoredProcedure [dbo].[CreateAudioFile]    Script Date: 9/9/2022 5:09:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Ruby Labausa
-- Create date: 9/9/2022
-- Description:	Create new audio file
-- =============================================
CREATE OR ALTER   PROCEDURE [dbo].[CreateAudioFile] 
	-- Add the parameters for the stored procedure here
	@audio_file_name VARCHAR(100), 
	@encoded_audio VARCHAR(MAX),
	@uploaded_by VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].AUDIO_FILES 
	(
		[AudioFileName], 
		[EncodedAudio], 
		[UploadedBy]
	)
	OUTPUT inserted.[AudioFileId]
	VALUES (
		@audio_file_name,
		@encoded_audio,
		@uploaded_by
	);

END
GO


