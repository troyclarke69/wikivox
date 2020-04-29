USE [Wikivox]
GO

INSERT INTO [dbo].[Musician]
           ([MusicianName]           ,[LastName]
           ,[FirstName]           ,[Bio]
           ,[Birth]           ,[Death]
           ,[isActive]           ,[HomeTown]
           ,[HomeCountry])
     VALUES
           ('Bono', 'Hewson', 'Paul', 'Frontman for U2', '1960', '', 1, 'Dublin', 'Ireland'),
		   ('The Edge', 'Evans', 'Dave', 'Guitarist for U2', '1960', '', 1, 'Dublin', 'Ireland'),
		   ('Adam Clayton', 'Clayton', 'Adam', 'Bassist for U2', '1960', '', 1, 'Dublin', 'Ireland'),
		   ('Larry Mullen Jr.', 'Mullen Jr.', 'Larry', 'Drummer for U2', '1960', '', 1, 'Dublin', 'Ireland')
GO


