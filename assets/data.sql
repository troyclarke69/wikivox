
  --insert into Entity (Name) values
  --('Album'),  ('Artist'),  ('Genre'),  ('Instrument'),  ('Musician'),  ('Song')
  SELECT TOP (1000) [Id],[Name] FROM [Wikivox].[dbo].[Entity]
  --insert into Genre (Name) values
  --('Rock'), ('Country'), ('Blues'), ('Jazz'), ('Classical'), ('Pop'), ('House'), ('Heavy Metal'), ('Punk Rock'),
  --('Post-Punk Rock'), ('Alternative'), ('Thrash'), ('Grunge'), ('Rap')
  SELECT TOP (1000) [Id],[Name] FROM [Wikivox].[dbo].[Genre]
  --insert into Instrument (Name) values
  --('Vocal'), ('Electric Guitar'), ('Bass Guitar'), ('Drums'), ('Piano'), ('Keyboards'), ('Violin'), ('Cello'), ('Trumpet'),
  --('Flute'), ('Harmonica'), ('Acoustic Guitar'), ('Slide Guitar'), ('Banjo')
  SELECT TOP (1000) [Id],[Name] FROM [Wikivox].[dbo].[Instrument]
  SELECT * FROM [Wikivox].[dbo].[Musician]
  SELECT * FROM [Wikivox].[dbo].[Artist]
  SELECT * FROM [Wikivox].[dbo].[Album]
  /* insert into Song (Name, Duration) values
	('Winter', '4:00')  */
  SELECT * FROM [Wikivox].[dbo].[Song]
 -- insert into AlbumSong (AlbumId, SongId) values
	--(5,1),(5,2),(5,3),(5,4),(5,5),(5,6),(5,7),(5,8),(5,9),(5,10) 
  SELECT * FROM [Wikivox].[dbo].[AlbumSong]
 /* insert into ArtistGenre (ArtistId, GenreId) values
	(4,1), (5,2), (6,3), (7,4), (8,5) */
  SELECT * FROM [Wikivox].[dbo].[ArtistGenre]
 /* insert into ArtistMusician (ArtistId, MusicianId) values
	(4,1), (5,2), (6,3), (7,4), (8,5) */
  SELECT * FROM [Wikivox].[dbo].[ArtistMusician]  -- need isActive
 -- insert into MusicianInstrument (MusicianId, InstrumentId) values
	--(1,1), (2,1), (2,2), (2,5), (2,12), (3,2), (3,3), (4, 4)
  SELECT * FROM [Wikivox].[dbo].[MusicianInstrument]
 -- insert into [Image] (EntityId, ResourceId, ImgUrl) values
	--(1,5,'/images/001.jpg') -- Album: Joshua Tree
	--, (2, 1, '/images/002.jpg') --Artist: U2
	--, (6, 1, '/images/003.jpg') --Song: Where the streets
  SELECT * FROM [Wikivox].[dbo].[Image]
  /* insert into ArtistSong (ArtistId, SongId) values
	(1, 11)  */
  SELECT * FROM [Wikivox].[dbo].[ArtistSong]

  select * from Artist a 
	join ArtistMusician am on a.Id = am.ArtistId
	join Musician m on am.MusicianId = m.Id

select * from Artist a
	join Album b on a.Id = b.ArtistId
	join AlbumSong c on c.AlbumId = b.Id
	join Song d on d.Id = c.SongId
	left join ArtistSong art on art.ArtistId = a.Id and art.SongId = d.Id

select * from Artist a
	join ArtistSong b on b.ArtistId = a.Id
	join Song c on c.Id = b.SongId

select * from Genre
