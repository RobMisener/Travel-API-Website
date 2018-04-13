CREATE DATABASE Project;

GO

USE Project;

GO


CREATE TABLE [dbo].[Users]
(
    [UserId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserName] VARCHAR(MAX) NOT NULL, 
    [PasswordHash] VARCHAR(MAX) NULL, 
    [SecurityStamp] VARCHAR(MAX) NULL
);

CREATE TABLE [dbo].[UserRoles]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[Role] VARCHAR(100) NOT NULL,

	CONSTRAINT pk_UserRoles PRIMARY KEY (UserId, Role),
	CONSTRAINT fk_UserRoles_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
 

 SELECT * FROM Users

 CREATE TABLE [dbo].[Itinerary]

 (
	[ItinId] UNIQUEIDENTIFIER NOT NULL,
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[StartDate] DATE NOT NULL,

	CONSTRAINT pk_Itinerary PRIMARY KEY (ItinId),
	CONSTRAINT fk_Itinerary_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)

 
 );

 CREATE TABLE [dbo].[Itinerary_Stops]

 (
	[ItinId] UNIQUEIDENTIFIER NOT NULL,
	[PlaceId] UNIQUEIDENTIFIER NOT NULL,
	[Order] INT NOT NULL,

	CONSTRAINT pk_Itinerary_Stops PRIMARY KEY (PlaceId),
	CONSTRAINT fk_Itinerary_Stops_Itinerary FOREIGN KEY (ItinId) REFERENCES Itinerary(ItinId)

 );
ALTER TABLE Itinerary_Stops
ADD Name VARCHAR(50) NOT NULL, 
Address VARCHAR(50) NOT NULL, 
Latitude FLOAT NOT NULL, 
Longitude FLOAT NOT NULL, 
Category VARCHAR(50) NOT NULL;
