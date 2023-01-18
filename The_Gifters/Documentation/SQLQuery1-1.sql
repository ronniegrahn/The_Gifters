-- Kör drop table dbo.Contributions först om den redan finns
--drop table dbo.Contributions

insert into dbo.Customers (FirstName, LastName, Email, PhoneNumber)
values ('Johan', 'Johansson', 'johansson@gmail.com', '070 123 45 67'),
       ('Alfred', 'Spenat', 'k-alfreds@spenat.nu', '076-765-43-21'),
       ('Bobby', 'Dylan', 'likearolling@stone.com', '0702223355');

insert into dbo.Organizations (OrganizationName, Description)
values ('Red Cross', 'The International Committee of the Red Cross (ICRC) is a humanitarian organization based in Geneva, Switzerland, whose stated mission is to protect the lives and dignity of victims of war and internal violence and to provide them with assistance. It is the oldest and most honored organization within the movement known as the International Red Cross and Red Crescent Movement. The ICRC is a private, independent, neutral organization that works to protect and assist people affected by armed conflict and other situations of violence. It directs and coordinates the international relief activities conducted by the Movement in situations of conflict, and it carries out relief and protection activities in countries affected by armed conflicts that are not covered by other Red Cross or Red Crescent societies.'),
       ('UNHCR', 'The United Nations High Commissioner for Refugees (UNHCR) is a United Nations agency with the mandate to protect and support refugees, asylum-seekers, and stateless people. The agency was established in 1950, during the aftermath of World War II and the displacement of millions of people, and is one of the oldest UN agencies. Its primary purpose is to safeguard the rights and well-being of refugees. It provides life-saving assistance such as shelter, food, water, and medical care, as well as long-term support for refugees to help them rebuild their lives. It also works to secure durable solutions for refugees, such as helping them to return home if it is safe to do so, or helping them to integrate in the country where they have sought asylum. Additionally, the organization work to prevent and reduce statelessness, working to ensure people have a nationality and the legal documents to prove it. UNHCR is an international organization that operates in over 130 countries, with a staff of around 15,000 people.'),
       ('UNICEF', 'UNICEF is an agency of the United Nations responsible for providing humanitarian and developmental aid to children worldwide. The agency is among the most widespread and recognizable social welfare organizations in the world, with a presence in 192 countries and territories. UNICEF''s activities include providing immunizations and disease prevention, administering treatment for children and mothers with HIV, enhancing childhood and maternal nutrition, improving sanitation, promoting education, and providing emergency relief in response to disasters.'),
       ('Women to Women', 'Women for Women is a nonprofit humanitarian organization that provides practical and moral support to female survivors of war. WfWI helps such women rebuild their lives after war''s devastation through a year-long tiered program that begins with direct financial aid and emotional counseling and includes life skills (e.g., literacy, numeracy) training if necessary, rights awareness education, health education, job skills training and small business development.');

insert into dbo.[Participations] (Amount, ParticipationDate, TimeFrame, ParticipationEndDate, SumGenerated, OrganizationId, CustomerId)
values (15000, '2022-01-01', 12, '2022-12-31', 1250, 1, 1),
       (500, '2022-01-01', 6, '2022-06-30', 300, 2, 2),
       (2000, '2022-01-01', null, null, null, 3, 3);


select * from dbo.Customers
select * from dbo.Contributions
select * from dbo.Organizations

alter table dbo.Customers --AspNetUsers
Add [AspNetUsersId] int NOT NULL REFERENCES AspNetUsers(Id)


CREATE TABLE [dbo].[Participations] (
    [Id]                  INT      IDENTITY (1, 1) NOT NULL,
    [Amount]              Money    NOT NULL,
    [ParticipationDate]   DATETIME NOT NULL,
    [TimeFrame]           INT      NULL,
    [ParticipationEndDate] DATE     NULL,
    [SumGenerated]        MONEY    NULL,
    [OrganizationId]      INT      NOT NULL REFERENCES [dbo].[Organizations] ([Id]),
    [CustomerId]          INT      NOT NULL REFERENCES [dbo].[Customers] ([Id])
    PRIMARY KEY CLUSTERED ([Id] ASC),
);


--CREATE TABLE [dbo].[Contributions] (
--    [Id]                  INT      IDENTITY (1, 1) NOT NULL,
--    [Amount]              INT      NOT NULL,
--    [ContributionDate]    DATETIME NOT NULL,
--    [TimeFrame]           INT      NULL,
--    [ContributionEndDate] DATE     NULL,
--    [SumGenerated]        MONEY    NULL,
--    [OrganizationId]      INT      NOT NULL REFERENCES [dbo].[Organizations] ([Id]),
--    [CustomerId]          INT      NOT NULL REFERENCES [dbo].[Customers] ([Id])
--    PRIMARY KEY CLUSTERED ([Id] ASC),
--);

--delete from dbo.Customers
--delete from dbo.Contributions
--delete from dbo.Organizations