USE [rpgmaster] 
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateDefaultPlayer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateDefaultPlayer]
GO
CREATE PROCEDURE dbo.UpdateDefaultPlayer
   @UserName NVARCHAR(MAX),
   @PlayerID INT
AS
BEGIN
DECLARE @USERID NVARCHAR(MAX)

/*Get UserID with Username that was passed in*/
Select @USERID = s.UserID
From Account s
inner join AspNetUsers ps on ps.Id = s.UserID
where ps.UserName = @UserName

UPDATE Account Set DefaultPlayerID = @PlayerID WHERE UserID = @USERID

END