drop table dbo.Participations

CREATE TABLE [dbo].[Participations] (
    [Id]                  INT      IDENTITY (1, 1) NOT NULL,
    [Amount]              Money    NOT NULL,
    [ParticipationDate]   DATETIME NOT NULL,
    [TimeFrame]           INT      NULL,
    [ParticipationEndDate] DATE     NULL,
    [SumGenerated]        MONEY    NULL,
    [IsRefundable] bit  not null,
    [IsActive] bit  not null,
    [OrganizationId]      INT      NOT NULL REFERENCES [dbo].[Organizations] ([Id]),
    [CustomerId]          INT      NOT NULL REFERENCES [dbo].[Customers] ([Id])
    PRIMARY KEY CLUSTERED ([Id] ASC),
);

select * from Participations