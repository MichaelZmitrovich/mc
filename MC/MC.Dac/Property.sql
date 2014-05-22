CREATE TABLE [dbo].[Property] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Name]      NVARCHAR (100)   NULL,
    [Latitude]  FLOAT (53)       NULL,
    [Longitude] FLOAT (53)       NULL,
    [ClientId]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK__Property__3214EC077F60ED59] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Property_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id])
);

