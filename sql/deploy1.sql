IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'WavShare')
	BEGIN
		CREATE DATABASE WavShare;
	END
GO

USE WavShare;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AUDIO_FILES')
	BEGIN 
		CREATE TABLE AUDIO_FILES(
			AudioFileId INT IDENTITY(1,1) PRIMARY KEY, 
			AudioFileName VARCHAR(100) NOT NULL, 
			EncodedAudio VARCHAR(MAX) NOT NULL,
			UploadedBy VARCHAR(100) NOT NULL,
			UploadDate DateTime NOT NULL DEFAULT(getdate())
		);
	END
GO

IF NOT EXISTS (SELECT * FROM [dbo].AUDIO_FILES)
	BEGIN
		INSERT INTO [dbo].AUDIO_FILES (
			AudioFileName,
			EncodedAudio, 
			UploadedBy
		)
		VALUES 
            ('TEST1.wav', 'random-encoded-audio-1', 'user1'),
            ('TEST2.wav', 'random-encoded-audio-2', 'user1'),
            ('TEST3.wav', 'random-encoded-audio-3', 'user2'),
            ('TEST4.wav', 'random-encoded-audio-4', 'user3'),
            ('TEST5.wav', 'random-encoded-audio-5', 'user3'),
            ('TEST6.wav', 'random-encoded-audio-6', 'user4'),
            ('TEST7.wav', 'random-encoded-audio-7', 'user4'),
            ('TEST8.wav', 'random-encoded-audio-8', 'user5'),
            ('TEST9.wav', 'random-encoded-audio-9', 'user1'),
            ('TEST10.wav', 'random-encoded-audio-10', 'user2'),
            ('TEST11.wav', 'random-encoded-audio-11', 'user3'),
            ('TEST12.wav', 'random-encoded-audio-12', 'user3'),
            ('TEST13.wav', 'random-encoded-audio-13', 'user3'),
            ('TEST14.wav', 'random-encoded-audio-14', 'user4'),
            ('TEST15.wav', 'random-encoded-audio-15', 'user3'),
            ('TEST16.wav', 'random-encoded-audio-16', 'user3'),
            ('TEST17.wav', 'random-encoded-audio-17', 'user5'),
            ('TEST18.wav', 'random-encoded-audio-18', 'user3'),
            ('TEST19.wav', 'random-encoded-audio-19', 'user1'),
            ('TEST20.wav', 'random-encoded-audio-20', 'user2')
	END
GO