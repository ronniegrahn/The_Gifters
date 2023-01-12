CREATE TABLE [dbo].[Participations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Amount] INT NOT NULL, 
    [ContributionDate] DATETIME NOT NULL, 
    [TimeFrame] INT NULL, 
    [ContributionEndDate] DATE NULL, 
    [SumGenerated] MONEY NULL, 
    [OrganizationId] INT NOT NULL REFERENCES Organizations(Id),
    [CustomerId] INT NOT NULL REFERENCES Customers(Id) 
)
