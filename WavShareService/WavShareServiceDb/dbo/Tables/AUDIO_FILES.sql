CREATE TABLE [dbo].[AUDIO_FILES] (
    [AudioFileId]   INT           IDENTITY (1, 1) NOT NULL,
    [AudioFileName] VARCHAR (100) NOT NULL,
    [EncodedAudio]  VARCHAR (MAX) NOT NULL,
    [UploadedBy]    VARCHAR (100) NOT NULL,
    [UploadDate]    DATETIME      DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([AudioFileId] ASC)
);

