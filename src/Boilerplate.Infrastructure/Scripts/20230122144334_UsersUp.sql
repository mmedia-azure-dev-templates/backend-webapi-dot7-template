USE [Jiban]
GO
UPDATE [dbo].[AspNetUsers] SET [LegacyId] = 1, [FirstName] = 'Raul', [LastName] = 'Flores' WHERE UserName = 'Super@g1.com';
UPDATE [dbo].[AspNetUsers] SET [LegacyId] = 2, [FirstName] = 'Adriana', [LastName] = 'Chalco' WHERE UserName = 'P1@g1.com';
UPDATE [dbo].[AspNetUsers] SET [LegacyId] = 178, [FirstName] = 'Honorio', [LastName] = 'Jimenez' WHERE UserName = 'NoP@g1.com';
UPDATE [dbo].[AspNetUsers] SET [LegacyId] = 180, [FirstName] = 'Deny', [LastName] = 'Ayala' WHERE UserName = 'P2@g1.com';
UPDATE [dbo].[AspNetUsers] SET [LegacyId] = 182, [FirstName] = 'Roberto', [LastName] = 'Lopez' WHERE UserName = 'P3@g1.com';
GO