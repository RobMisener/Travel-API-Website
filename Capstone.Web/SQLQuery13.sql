/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [ItinId]
      ,[PlaceId]
      ,[Order]
      ,[Name]
      ,[Address]
      ,[Latitude]
      ,[Longitude]
      ,[Category]
  FROM [Project].[dbo].[Itinerary_Stops]
  ORDER BY ItinId;