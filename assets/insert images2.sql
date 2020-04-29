/****** Script for SelectTopNRows command from SSMS  ******/
select * from [Image]
select * from Album
select * from AlbumSong
select * from Entity
--delete from Album where id = 15

-- insert ALBUM images for U2
declare @ent int, @pImg int
set @ent = 1
set @pImg = 1
insert into [Image](EntityId, ResourceId, ImgUrl, PrimaryImage) values
	(@ent, 2, '/images/albums/u2/u2-october.jpg', @pImg),
	(@ent, 3, '/images/albums/u2/u2-war.jpg', @pImg),
	(@ent, 4, '/images/albums/u2/u2-fire.jpg', @pImg),
	(@ent, 5, '/images/albums/u2/u2-joshuatree.jpg', @pImg),
	(@ent, 6, '/images/albums/u2/u2-rattle.jpg', @pImg),
	(@ent, 7, '/images/albums/u2/u2-achtungbaby.jpg', @pImg),
	(@ent, 8, '/images/albums/u2/u2-zooropa.jpg', @pImg),
	(@ent, 9, '/images/albums/u2/u2-pop.jpg', @pImg),
	(@ent, 10, '/images/albums/u2/u2-allthatyoucan.jpg', @pImg),
	(@ent, 11, '/images/albums/u2/u2-bomb.jpg', @pImg),
	(@ent, 12, '/images/albums/u2/u2-noline.jpg', @pImg),
	(@ent, 13, '/images/albums/u2/u2-innocence.jpg', @pImg),
	(@ent, 14, '/images/albums/u2/u2-experience.jpg', @pImg)

-- insert ARTIST image(s) for L.Cohen
select id from Artist where ArtistName = 'Leonard Cohen'
declare @ent int, @pImg int, @res int
set @ent = 2
set @pImg = 2
set @res = (select id from Artist where ArtistName = 'Leonard Cohen')
insert into [Image](EntityId, ResourceId, ImgUrl, PrimaryImage) values
	(@ent, @res, '/images/artists/leonardcohen/cohen-1.jpg', @pImg)

	--delete from [Image] where id = 25