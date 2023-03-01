CREATE TABLE [web].[GeographicLocations](
[Id] int identity PRIMARY KEY NOT NULL,
[Code] [varchar](50) NULL,
[Name] [varchar](150) NULL,
[Parent] [int] NULL,
[Parroquia] [int] NULL
)

SET IDENTITY_INSERT [web].[GeographicLocations] ON

INSERT INTO [web].[GeographicLocations]
(
[Id]
,[Code]
,[Name]
,[Parent]
,[Parroquia]
)
		      
select 
[Id]
,[Code]
,[Name]
,[Parent]
,[Parroquia]
from [dbo].[GeographicLocations]
			
GO

SET IDENTITY_INSERT [web].[GeographicLocations] OFF
GO