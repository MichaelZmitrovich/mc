CREATE TABLE [dbo].[PropertyRate] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [PropertyId] UNIQUEIDENTIFIER NOT NULL,
    [Name]       NVARCHAR (100)   NULL,
    CONSTRAINT [PK_PropertyRate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PropertyRate_Property] FOREIGN KEY ([PropertyId]) REFERENCES [dbo].[Property] ([Id])
);

