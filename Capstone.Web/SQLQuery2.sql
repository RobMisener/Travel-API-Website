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

  
  
  INSERT INTO Itinerary_Stops (ItinId, PlaceId, [Order], Name, Latitude, Longitude, Category) VALUES (, @PlaceId, @Order, @Name, @Latitude, @Longitude, @Category)
