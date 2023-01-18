
-- Skapa en user om det inte finns någon. Genom create på webbsidan.

-- Hitta en user och notera "CustomerId", till usern som du vill lägga in gammal data på
select * from dbo.Customers

-- Kolla vad "OrganizationId" har för nummer i databasen du vill lägga till gammal data på.
select * from dbo.Participations



-- i exemplet nedan är CustomerId 4009 och organizationId är 1006
insert into dbo.[Participations] 
    (Amount, ParticipationDate, TimeFrame, ParticipationEndDate, SumGenerated, IsRefundable, IsActive, OrganizationId, CustomerId)
values
    (10000, '2016-03-01', 12, '2023-03-01', null, 1, 1, 1006, 4009), -- Detta är en participation insatt 2016 som är active.
    (15000, '2018-05-15', 12, '2020-05-15', null, 1, 0, 1006, 4009), -- Detta är en participation som inte är active
    (20000, '2022-01-01', null, null, null,0, 1, 1006, 4009) -- Detta är en donation och ej refundable. TimeFrame, participationEndData är null


-- SumGenerated är satt till null på alla och räknas ut i participationsService.