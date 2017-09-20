create database StoreWebApp;
GO

use StoreWebApp;
GO

CREATE TABLE [MediaDrafts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Media] [image] NULL,
	[FileSize] [int] NULL,
	[FileDateTime] [datetime] NULL,
	[ServerFilePath] [text] NULL,
	[GroupId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [ProdCatalog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Caption] [nvarchar](50) NULL,
	[Url] [nvarchar](200) NULL,
 CONSTRAINT [PK_ProdCatalog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [MediaDrafts]  WITH CHECK ADD  CONSTRAINT [FK_MediaDrafts_ProdCatalog] FOREIGN KEY([GroupId])
REFERENCES [ProdCatalog] ([Id])
ON UPDATE SET NULL
ON DELETE SET NULL
GO

ALTER TABLE [MediaDrafts] CHECK CONSTRAINT [FK_MediaDrafts_ProdCatalog]
GO


CREATE PROCEDURE LoadPartMedia
	@GroupId int,
	@From int,
	@Count int
AS
BEGIN
	SET NOCOUNT ON;

    select 
		[Id]
      ,[GroupId]
      ,[Media]
      ,[FileSize]
      ,[FileDateTime]
      ,[ServerFilePath]
from 
    (
    select row_number() over(order by (select 1)) as RN, [Id]
      ,[GroupId]
      ,[Media]
      ,[FileSize]
      ,[FileDateTime]
      ,[ServerFilePath]
    from MediaDrafts 
    ) q
where RN between @From and (@From + @Count - 1) 
	and GroupId = @GroupId

END
GO

CREATE PROCEDURE LoadAllMedia
AS
BEGIN
	SET NOCOUNT ON;

    select 
		[Id]
      ,[GroupId]
      ,[Media]
      ,[FileSize]
      ,[FileDateTime]
      ,[ServerFilePath]
	 from MediaDrafts 
END
GO

CREATE PROCEDURE LoadSingleMedia
	@Id int
AS
BEGIN
	SET NOCOUNT ON;

    select 
		[Id]
      ,[GroupId]
      ,[Media]
      ,[FileSize]
      ,[FileDateTime]
      ,[ServerFilePath]
	 from MediaDrafts 
	 where Id=@Id
END
GO

CREATE PROCEDURE SaveMedia
	@Media image,
	@ServerFilePath text,
	@FileSize int
AS
BEGIN
	INSERT INTO MediaDrafts ([Media], [FileDateTime], [ServerFilePath], [FileSize])
	VALUES (@Media, getdate(), @ServerFilePath, @FileSize)
END
GO

CREATE PROCEDURE DeleteMedia
	@Id int
AS
BEGIN
	DELETE FROM MediaDrafts WHERE Id=@Id
END
GO

CREATE PROCEDURE UpdateMedia
	@Id int,
	@GroupId int
AS
BEGIN
	UPDATE MediaDrafts SET GroupId=@GroupId WHERE Id=@Id
END
GO


CREATE PROCEDURE LoadCatalog	
AS
BEGIN
	SET NOCOUNT ON;

    select 
		[Id]
      ,[Caption]
      ,[Url]
	 from ProdCatalog
	 order by Caption
END
GO

