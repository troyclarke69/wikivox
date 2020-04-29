USE [Wikivox]
GO

INSERT INTO [dbo].[Artist]
           ([ArtistName]           ,[LastName]
           ,[FirstName]           ,[Bio]
           ,[YrFormed]           ,[YrEnded]
           ,[isActive]           ,[HomeTown]
           ,[HomeCountry])
     VALUES
           ('U2', '', '', 'Rock band from Ireland established in 1978 with over 150 million albums sold',
				'1978', '', 1, 'Dublin', 'Ireland')
GO


