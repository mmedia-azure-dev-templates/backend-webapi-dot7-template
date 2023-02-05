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