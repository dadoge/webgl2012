use [rpgmaster]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaveGameMap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SaveGameMap]
GO

CREATE PROCEDURE dbo.SaveGameMap
	@Name NVARCHAR(128),
	@UserName NVARCHAR(128),
	@TilesData NVARCHAR(MAX),
	@isActive BIT
AS
BEGIN
DECLARE @USERID NVARCHAR(MAX)

/*Get UserID with Username that was passed in*/
Select @USERID = s.UserID
From Account s
inner join AspNetUsers ps on ps.Id = s.UserID
where ps.UserName = @UserName

/*INSERT Data into Map Table*/
INSERT INTO Map (Name,CreatedByUserID,Tiles,isActive)
VALUES (@Name, @USERID, @TilesData, @isActive)

END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoadGameMap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[LoadGameMap]
GO

CREATE PROCEDURE dbo.LoadGameMap
	@MapID INT
AS
BEGIN

Select Name, Tiles
From Map where MapID = @MapID

END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPlayerGameMaps]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPlayerGameMaps]
GO

CREATE PROCEDURE dbo.GetPlayerGameMaps
	@UserName NVARCHAR(128)
AS
BEGIN
DECLARE @USERID NVARCHAR(MAX)

/*Get UserID with Username that was passed in*/
Select @USERID = s.UserID
From Account s
inner join AspNetUsers ps on ps.Id = s.UserID
where ps.UserName = @UserName

Select MapID, Name
From Map WHERE CreatedByUserID = @USERID ORDER BY Name
END
GO