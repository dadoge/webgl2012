USE [rpgmaster] 
GO

/****** Object:  Table [dbo].[UserPlayer]    Script Date: 6/24/2014 8:25:30 PM ******/

ALTER TABLE [dbo].[Player] ADD isDeleted BIT NOT NULL DEFAULT 0;
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserPlayers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUserPlayers]
GO
CREATE PROCEDURE dbo.GetUserPlayers
   @UserName NVARCHAR(MAX)
AS
BEGIN
DECLARE @USERID NVARCHAR(MAX)

/*Get UserID with Username that was passed in*/
Select @USERID = s.UserID
From Account s
inner join AspNetUsers ps on ps.Id = s.UserID
where ps.UserName = @UserName

Select s.PlayerID, s.Name, s.ImgSrc
From Player s
inner join UserPlayer ps on ps.PlayerID = s.PlayerID
WHERE ps.UserID = @USERID AND s.isDeleted = 0 ORDER BY s.Name

END