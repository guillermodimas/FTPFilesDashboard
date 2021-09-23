CREATE TABLE [dbo].[GeneratedLinks] (
    [LinkID]         INT           IDENTITY (1, 1) NOT NULL,
    [RecordingID]    VARCHAR (100) NOT NULL,
    [GeneratedLink]  VARCHAR (500) NULL,
    [CreatedOn]      DATETIME      NULL,
    [CreatedBy]      VARCHAR (100) NULL,
    [ExpirationDate] DATETIME      NULL,
    CONSTRAINT [PK_GeneratedLinks] PRIMARY KEY CLUSTERED ([LinkID] ASC),
    CONSTRAINT [FK_GeneratedLinks_RecordingFiles] FOREIGN KEY ([RecordingID]) REFERENCES [dbo].[RecordingFiles] ([id])
);

