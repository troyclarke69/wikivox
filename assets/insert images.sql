select * from Entity
select * from Artist
select * from [Image]
/*
insert into [Image] (EntityId, ResourceId, ImgUrl) values
(2, 2, 'images/beatles1.jpg')
*/
--show the primaryImage for ARTIST 2
select a.*, ImgUrl from [Image] i
inner join Entity e on i.EntityId = e.Id
inner join Artist a on a.Id = e.Id
where i.ResourceId = 2 and i.PrimaryImage = 1

and e.Id = 1 --the ArtistId here
