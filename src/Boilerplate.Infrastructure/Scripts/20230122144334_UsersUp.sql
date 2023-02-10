USE [Jiban]
GO
INSERT [authp].[RoleToPermissions] ([RoleName], [Description], [PackedPermissionsInRole], [RoleType]) VALUES (N'Role1', NULL, N'', 0)
INSERT [authp].[RoleToPermissions] ([RoleName], [Description], [PackedPermissionsInRole], [RoleType]) VALUES (N'Role2', NULL, N'', 0)
INSERT [authp].[RoleToPermissions] ([RoleName], [Description], [PackedPermissionsInRole], [RoleType]) VALUES (N'Role3', NULL, N'', 0)
INSERT [authp].[RoleToPermissions] ([RoleName], [Description], [PackedPermissionsInRole], [RoleType]) VALUES (N'SuperAdmin', NULL, N'￿', 100)
GO
INSERT [authp].[AuthUsers] ([UserId], [Email], [UserName], [TenantId], [IsDisabled]) VALUES (N'0db1a0f4-3364-402f-accd-26446dc43132', N'ROBERTO.LOPEZ@MAD.EC', N'ROBERTO.LOPEZ@MAD.EC', NULL, 0)
INSERT [authp].[AuthUsers] ([UserId], [Email], [UserName], [TenantId], [IsDisabled]) VALUES (N'64dc2241-dc53-4b41-bfcc-6145e1a79feb', N'ADRIANA.CHALCO@MAD.EC', N'ADRIANA.CHALCO@MAD.EC', NULL, 0)
INSERT [authp].[AuthUsers] ([UserId], [Email], [UserName], [TenantId], [IsDisabled]) VALUES (N'9d3fc0b1-8002-44b7-ba9b-daecdc28df7c', N'HONORIO.JIMENEZ@MAD.EC', N'HONORIO.JIMENEZ@MAD.EC', NULL, 0)
INSERT [authp].[AuthUsers] ([UserId], [Email], [UserName], [TenantId], [IsDisabled]) VALUES (N'9ea49dfb-9c3f-4b17-bfb1-439855208350', N'DENNY.AYALA@MAD.EC', N'DENNY.AYALA@MAD.EC', NULL, 0)
INSERT [authp].[AuthUsers] ([UserId], [Email], [UserName], [TenantId], [IsDisabled]) VALUES (N'a49cf027-915b-4933-9fce-30def6d67037', N'RAUL.FLORES@MAD.EC', N'RAUL.FLORES@MAD.EC', NULL, 0)
GO
INSERT [authp].[UserToRoles] ([UserId], [RoleName]) VALUES (N'0db1a0f4-3364-402f-accd-26446dc43132', N'Role1')
INSERT [authp].[UserToRoles] ([UserId], [RoleName]) VALUES (N'64dc2241-dc53-4b41-bfcc-6145e1a79feb', N'Role1')
INSERT [authp].[UserToRoles] ([UserId], [RoleName]) VALUES (N'9d3fc0b1-8002-44b7-ba9b-daecdc28df7c', N'Role1')
INSERT [authp].[UserToRoles] ([UserId], [RoleName]) VALUES (N'0db1a0f4-3364-402f-accd-26446dc43132', N'Role2')
INSERT [authp].[UserToRoles] ([UserId], [RoleName]) VALUES (N'9d3fc0b1-8002-44b7-ba9b-daecdc28df7c', N'Role2')
INSERT [authp].[UserToRoles] ([UserId], [RoleName]) VALUES (N'9d3fc0b1-8002-44b7-ba9b-daecdc28df7c', N'Role3')
INSERT [authp].[UserToRoles] ([UserId], [RoleName]) VALUES (N'a49cf027-915b-4933-9fce-30def6d67037', N'SuperAdmin')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [LegacyId], [FirstName], [LastName], [LastLogin]) VALUES (N'0db1a0f4-3364-402f-accd-26446dc43132', N'ROBERTO.LOPEZ@MAD.EC', N'ROBERTO.LOPEZ@MAD.EC', N'ROBERTO.LOPEZ@MAD.EC', N'ROBERTO.LOPEZ@MAD.EC', 0, N'AQAAAAIAAYagAAAAEM9qoaElf89MSMZ+dm8p+5dJX3gnAsmadZ+4dXcw05prvmDj+v+y6x67c0VOXqJyZA==', N'DOPNINPJDP4E23MAJDKQULGTZQJUVRIK', N'fef005bb-53e2-441a-9a3f-a3be6abf126d', NULL, 0, 0, NULL, 1, 0, 182, N'ROBERT PATRICIO', N'LOPEZ FREIRE', CAST(N'2023-02-03T14:04:41.4433043' AS DateTime2))
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [LegacyId], [FirstName], [LastName], [LastLogin]) VALUES (N'64dc2241-dc53-4b41-bfcc-6145e1a79feb', N'ADRIANA.CHALCO@MAD.EC', N'ADRIANA.CHALCO@MAD.EC', N'ADRIANA.CHALCO@MAD.EC', N'ADRIANA.CHALCO@MAD.EC', 0, N'AQAAAAIAAYagAAAAELXqqaQs5b4YQc7Tl6lswjzgDUGO5BXDP+AWSX3Z4p/KFc/nRB83EJlzvdD1Ciu04w==', N'B7MGA3C2R4SQBCJODEDPA67ZAVOSICE7', N'54b1f45d-b83c-47b3-bf92-34f687e9712c', NULL, 0, 0, NULL, 1, 0, 2, N'ADRIANA BELEN', N'CHALCO CEVALLOS', CAST(N'2023-02-03T14:04:41.1679699' AS DateTime2))
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [LegacyId], [FirstName], [LastName], [LastLogin]) VALUES (N'9d3fc0b1-8002-44b7-ba9b-daecdc28df7c', N'HONORIO.JIMENEZ@MAD.EC', N'HONORIO.JIMENEZ@MAD.EC', N'HONORIO.JIMENEZ@MAD.EC', N'HONORIO.JIMENEZ@MAD.EC', 0, N'AQAAAAIAAYagAAAAEGBXPViFvP8/bM8NBO5F3gW4j4i6pKQbs0iRUH6bUoSGvP0B9Ieo72ay/HgyHODbng==', N'DJ3CZGW6M6OQBNZWUANPD4EADUB2YXK6', N'27c423e5-2016-4937-a6b6-508c61f05454', NULL, 0, 0, NULL, 1, 0, 178, N'ORFE HONORIO', N'JIMENEZ JIMENEZ', CAST(N'2023-02-03T14:04:41.6972025' AS DateTime2))
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [LegacyId], [FirstName], [LastName], [LastLogin]) VALUES (N'9ea49dfb-9c3f-4b17-bfb1-439855208350', N'DENNY.AYALA@MAD.EC', N'DENNY.AYALA@MAD.EC', N'DENNY.AYALA@MAD.EC', N'DENNY.AYALA@MAD.EC', 0, N'AQAAAAIAAYagAAAAEGhjHl5/uRGS+k0NQ6mxU5K+yCxSSYE7vvRfyCJDSKkBrV3oH0s1osmWwYZ+J4j2jA==', N'G6H5WIARD37SMLVZNH2FYWSI2BYDGHJF', N'46443da6-f387-4afa-86e1-ceb88ebf5dd9', NULL, 0, 0, NULL, 1, 0, 180, N'DENNY JOSEFA', N'AYALA ERAS', CAST(N'2023-02-03T14:04:40.3553682' AS DateTime2))
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [LegacyId], [FirstName], [LastName], [LastLogin]) VALUES (N'a49cf027-915b-4933-9fce-30def6d67037', N'RAUL.FLORES@MAD.EC', N'RAUL.FLORES@MAD.EC', N'RAUL.FLORES@MAD.EC', N'RAUL.FLORES@MAD.EC', 0, N'AQAAAAIAAYagAAAAEFBn4fvB16h3V9u7jTkU7soTekTtZhX3yKbfbsBWlrdzQJdswhRAytsQNJoZodYQ6w==', N'4PKVGH6GN3S2YGYKDIPNP7PBWWJGVNGM', N'1cdec64d-48b0-4332-b8d8-8fc44e7da209', NULL, 0, 0, NULL, 1, 0, 1, N'RAÚL DAVID', N'FLORES SERRANO', CAST(N'2023-02-03T14:04:42.0101957' AS DateTime2))
GO

INSERT INTO [dbo].[AspNetUsers]
           ([Id]
           ,[UserName]
           ,[NormalizedUserName]
           ,[Email]
           ,[NormalizedEmail]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[ConcurrencyStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEnd]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[LegacyId]
           ,[FirstName]
           ,[LastName]
           ,[LastLogin])
     select 
			LOWER(NEWID()) as Id, 
			Email as UserName,
			UPPER(Email) as NormalizedUserName, 
			Email as Email, 
			UPPER(Email) as NormalizedEmail,
			0 as EmailConfirmed, 
			'AQAAAAIAAYagAAAAEF/D8Rxkhv4Rhv9lpqbX1P6EVpitSOcBRo0fNLWmuSmEE4rD7ecZekv5/7/IQniINg==',
			NEWID() as SecurityStamp,
			NEWID() as ConcurrencyStamp,
			null as PhoneNumber,
			0 as PhoneNumberConfirmed,
			0 as TwoFactorEnabled,
			null as LockoutEnd,
			IsActive as LockoutEnabled,
			0 as AccessFailedCount,
			Id as LegacyId,
			Name as FirstName,
			Surname as LastName,
			LastLogin from web.Users
			
		where Id not in (1,2,178,180,182)
GO


INSERT [authp].[AuthUsers] ([UserId], [Email], [UserName], [TenantId], [IsDisabled]) 
SELECT [Id]
      ,[UserName]
      ,[NormalizedUserName]
      ,null
	  ,0
FROM [dbo].[AspNetUsers] where LegacyId not in (1,2,178,180,182)

DECLARE @id INT = (SELECT ISNULL(MAX(LegacyId),0) FROM [dbo].[AspNetUsers]);
Print (@id);
DBCC checkident('dbo.AspNetUsers', reseed, @id);
GO