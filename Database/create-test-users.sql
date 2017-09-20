use StoreWebAppDB;
GO

----------------------------------------------------------------------------
-- clean
----------------------------------------------------------------------------

delete from [StoreWebAppDB].[dbo].[webpages_UsersInRoles]
GO

delete from [StoreWebAppDB].[dbo].[webpages_Roles]
GO

delete from [StoreWebAppDB].[dbo].[webpages_Membership]
GO

delete from [StoreWebAppDB].[dbo].[UserProfile]
GO

print 'membership tables has been cleaned'
----------------------------------------------------------------------------
-- insert test data
----------------------------------------------------------------------------
INSERT INTO [StoreWebAppDB].[dbo].[UserProfile]
           ([UserName])
     VALUES
           ('a')
GO

INSERT INTO [StoreWebAppDB].[dbo].[webpages_Membership]
           ([UserId]
           ,[IsConfirmed]
           ,[Password]
           ,[PasswordSalt])
     VALUES
           (1
           ,1
           ,'AOynVPVhQL57g9iHBQgOiaSpjYvC0/9F6JNnKOX1xFExrZ0GLRmPPEkhtmfpCWV0Cw=='
           ,'')
GO

INSERT INTO [StoreWebAppDB].[dbo].[webpages_Roles]
           ([RoleName])
     VALUES
           ('admin')
GO

INSERT INTO [StoreWebAppDB].[dbo].[webpages_UsersInRoles]
           ([UserId]
           ,[RoleId])
     VALUES
           ((select [UserId] from [StoreWebAppDB].[dbo].[UserProfile] where [UserName]='a')
           ,(select [RoleId] from [StoreWebAppDB].[dbo].[webpages_Roles] where [RoleName]='admin'))
GO

print 'test admin user has been created'




