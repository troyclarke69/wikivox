/****** Script for SelectTopNRows command from SSMS  ******/
SELECT DISTINCT(al.Name)
--a.ArtistName, al.Name, s.Name 
from 
AlbumSong asong

inner join Artist a
on asong.ArtistId = a.Id

inner join Album al
on asong.albumId = al.Id

inner join Song s
on asong.SongId = s.Id

