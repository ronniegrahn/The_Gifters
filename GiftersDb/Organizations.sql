CREATE TABLE [dbo].[Organizations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [OrganizationName] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL
)
