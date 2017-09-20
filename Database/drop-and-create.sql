EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'StoreWebAppDB'
GO
USE [master]
GO
ALTER DATABASE [StoreWebAppDB] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
USE [master]
GO
DROP DATABASE [StoreWebAppDB]
GO


create database StoreWebAppDB;
GO

use StoreWebAppDB;
GO
