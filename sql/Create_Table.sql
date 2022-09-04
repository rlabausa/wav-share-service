--DROP TABLE dbo.Audio_Files;
--DROP DATABASE WavShareDB;


IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'WavShareDB')
	BEGIN
		CREATE DATABASE WavShareDB;
	END

USE WavShareDB;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'AUDIO_FILES')
	BEGIN 
		CREATE TABLE AUDIO_FILES(
			AudioFileId INT IDENTITY(1,1) PRIMARY KEY, 
			AudioFileName VARCHAR(255) NOT NULL, 
			EncodedAudio VARCHAR(MAX) NOT NULL,
			UploadedBy VARCHAR(100) NOT NULL,
			UploadDate DateTime NOT NULL DEFAULT(getdate())
		);
	END