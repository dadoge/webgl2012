use [rpgmaster]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddPlayer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddPlayer]
GO

CREATE PROCEDURE dbo.AddPlayer
	@UserName NVARCHAR (MAX),
	@Name NVARCHAR(25),
	@ImgSrc NVARCHAR(MAX),
	@PlayerTypeID INT,
	@ClassID INT,
	@RaceID INT,
	@GenderID INT,
	@AlignmentID INT,
	@Level INT,
	@Age INT,
	@History NVARCHAR(1024),
	@Height NVARCHAR(64),
	@Weight NVARCHAR(64),
	@Experience INT,
	@Money NVARCHAR(64),
	@MaxHitPoints INT
AS
BEGIN
DECLARE @USERID NVARCHAR(MAX), @DEFAULTPLAYERID INT, @NewPlayerID INT

/*INSERT Data into Player Table*/
INSERT INTO Player (Name, ImgSrc, PlayerTypeID, ClassID, RaceID, GenderID, AlignmentID, Level, Age, History, Height, Weight, Experience, Money, MaxHitPoints)
VALUES (@Name, @ImgSrc, @PlayerTypeID, @ClassID, @RaceID, @GenderID, @AlignmentID, @Level, @Age, @History, @Height, @Weight, @Experience, @Money, @MaxHitPoints)
/*Get Newly Inserted Rows Primary ID*/
SELECT @NewPlayerID = SCOPE_IDENTITY();

/*Get UserID with Username that was passed in*/
Select @USERID = s.UserID
From Account s
inner join AspNetUsers ps on ps.Id = s.UserID
where ps.UserName = @UserName

/*Insert into associative table for what user owns what player*/
INSERT INTO UserPlayer (PlayerID, UserID)
VALUES (@NewPlayerID,@USERID)

/*Set DefaultPlayerID to newly created PlayerID if one did not already exist*/
SELECT @DEFAULTPLAYERID = a.DefaultPlayerId
FROM Account a WHERE a.UserID = @USERID

UPDATE Account SET DefaultPlayerId = @NewPlayerID WHERE UserID = @USERID AND DefaultPlayerId IS NULL

RETURN @NewPlayerID
END