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

  SELECT * FROM Itinerary_Stops JOIN Itinerary on Itinerary_Stops.ItinId = Itinerary.ItinId WHERE UserId = '2F1F3685-026C-4A86-B5EB-2064F93ED30F' ORDER BY Itinerary_Stops.ItinId, [Order];

SELECT * FROM Itinerary_Stops JOIN Itinerary on Itinerary_Stops.ItinId = Itinerary.ItinId WHERE UserId = '562FB55B-0AFA-46CD-A856-BF4FAB6C23D0' AND Itinerary_Stops.ItinId = '18' ORDER BY Itinerary_Stops.ItinId, [Order];


