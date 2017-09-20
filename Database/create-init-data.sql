use StoreWebAppDB;
GO

----------------------------------------------------------------------------
-- clean
----------------------------------------------------------------------------

delete from [StoreWebAppDB].[dbo].[Parameters]
GO

print 'tables has been cleaned'
----------------------------------------------------------------------------
-- insert test data
----------------------------------------------------------------------------
INSERT INTO [StoreWebAppDB].[dbo].[Parameters]
           ([Name], [Value], [Descr])
     VALUES
           ('0group', '1', 'Group number displayed as default page for catalog')
GO


print 'test data has been created'




