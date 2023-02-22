CREATE TABLE  [web].[Teams](
	[Id] UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[HierarchyId] hierarchyid NOT NULL,
	[DataKey] [nvarchar](450) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[Dateupdated] [datetime2](7) NULL,
)