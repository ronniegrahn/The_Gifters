
-- You must first create a new user via the actual webpage.

-- Please use the following:
-- FirstName: Pontus
-- LastName: Wittenmark
-- Email: pontus@mello.com
-- PhoneNumber: 070123456789
-- Password: Abc_123
-- RepeatPassworld: Abc_123

-- You must THEN get the 'Id' from the new user and use below.
-- You must ALSO get 'Id' from Organizations IF you do not have Ids 1-4.

insert into dbo.[Participations] 
    (Amount, ParticipationDate, TimeFrame, ParticipationEndDate, SumGenerated, IsRefundable, IsActive, OrganizationId, CustomerId)
values
    (10000, '2016-03-01', 12, '2023-03-01', null, 1, 1, 1, 3002), -- Detta är en participation insatt 2016 som är active.
    (15000, '2018-05-15', 12, '2020-05-15', null, 1, 0, 3, 3002), -- Detta är en participation som inte är active
    (20000, '2022-01-01', null, null, null,0, 1, 1, 3002) -- Detta är en donation och ej refundable. TimeFrame, participationEndData är null

go

