CREATE TABLE [dbo].[Todos] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Description] NVARCHAR (500)   NOT NULL,
    [IsDone]      BIT              NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

