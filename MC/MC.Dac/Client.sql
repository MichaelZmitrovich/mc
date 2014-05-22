CREATE TABLE [dbo].[Client] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [AccountNumber] VARCHAR (50)     NULL,
    [FirstName]     NVARCHAR (50)    NULL,
    [LastName]      NVARCHAR (50)    NULL,
    [LoginName]     NVARCHAR (50)    NULL,
    [Password]      VARCHAR (50)     NULL,
    CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED ([Id] ASC)
);

