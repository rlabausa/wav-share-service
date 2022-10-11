

-- =============================================
-- Author:		Ruby Labausa
-- Create date: 9/11/2022
-- Description:	Update audio file or insert if none exists
-- =============================================
CREATE     PROCEDURE [dbo].[UpsertAudioFile] 
	-- Add the parameters for the stored procedure here
	@audio_file_id INT, 
	@audio_file_name VARCHAR(100),
	@encoded_audio VARCHAR(MAX),
	@uploaded_by VARCHAR(100)
AS
BEGIN
	BEGIN TRANSACTION

		-- Return the number of rows affected
		SET NOCOUNT OFF;

		-- Insert statements for procedure here
		UPDATE 
			[dbo].AUDIO_FILES
		WITH 
			(UPDLOCK, SERIALIZABLE) 
		SET
			[audioFileName] = @audio_file_name,
			[EncodedAudio] = @encoded_audio,
			[UploadedBy] = @uploaded_by,
			[UploadDate] = GETDATE()
		WHERE 
			[AudioFileId] = @audio_file_id

		IF @@ROWCOUNT = 0
		BEGIN
			SET IDENTITY_INSERT [dbo].[AUDIO_FILES] ON;

			INSERT INTO 
				[dbo].AUDIO_FILES(
					[AudioFileId],
					[AudioFileName],
					[EncodedAudio],
					[UploadDate]
				)
			VALUES(
				@audio_file_id,
				@audio_file_name,
				@encoded_audio,
				GETDATE()
			);

			SET IDENTITY_INSERT [dbo].[AUDIO_FILES] OFF;
		END

	COMMIT TRANSACTION
	
END
