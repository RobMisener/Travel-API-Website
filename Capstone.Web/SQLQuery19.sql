/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [ItinId]
      ,[UserId]
      ,[StartDate]
      ,[ItinName]
  FROM [Project].[dbo].[Itinerary]

  SELECT * FROM Itinerary WHERE UserId = '562FB55B-0AFA-46CD-A856-BF4FAB6C23D0' ORDER BY StartDate;

