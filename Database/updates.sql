ALTER TABLE [dbo].[Groups] ADD [Visible] [bit] NOT NULL DEFAULT 0
GO

CREATE TABLE [dbo].[Parameters] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max),
	[Value] [nvarchar](max),
    CONSTRAINT [PK_dbo.Parameters] PRIMARY KEY ([Id])
)
GO

ALTER TABLE [dbo].[Items] ADD [Price] [decimal](18, 2) NOT NULL DEFAULT 0
GO
