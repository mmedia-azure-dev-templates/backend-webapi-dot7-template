CREATE TABLE  [web].[Teams](
	[Id] [nvarchar](450) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[HierarchyId] hierarchyid NOT NULL,
	[OldHyerarchyId] hierarchyid NOT NULL,
	[DataKey] [nvarchar](450) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[Dateupdated] [datetime2](7) NULL,
 CONSTRAINT [PK_Teams] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
))