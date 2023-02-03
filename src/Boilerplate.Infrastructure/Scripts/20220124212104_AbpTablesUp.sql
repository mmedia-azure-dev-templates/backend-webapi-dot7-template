  IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Jiban')
  BEGIN
    CREATE DATABASE [Jiban]


    END
    GO
       USE [Jiban]
    GO
--You need to check if the schema exists

IF NOT EXISTS ( SELECT  * FROM    sys.schemas  WHERE   name = N'web' ) 
BEGIN
      EXEC('CREATE SCHEMA [web]');
END
GO
-- INSERT [dbo].[__AuthPermissionsMigrationHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210707095141_Initial', N'7.0.2')
-- INSERT [dbo].[__AuthPermissionsMigrationHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211215111952_Version2', N'7.0.2')
-- INSERT [dbo].[__AuthPermissionsMigrationHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220323172428_Version3', N'7.0.2')
-- INSERT [dbo].[__AuthPermissionsMigrationHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220419104438_Version3-2-0', N'7.0.2')
-- GO