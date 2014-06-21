use [rpgmaster]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUserInfo]
GO

CREATE PROCEDURE dbo.GetUserInfo
   @UserName NVARCHAR(MAX)
AS
BEGIN
Select s.UserID, s.ChatName, s.DefaultPlayerId
From Account s
inner join AspNetUsers ps on ps.Id = s.UserID
where ps.UserName = @UserName

END