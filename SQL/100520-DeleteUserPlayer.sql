USE [rpgmaster] 
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteUserPlayer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteUserPlayer]
GO
CREATE PROCEDURE dbo.DeleteUserPlayer
   @PlayerID NVARCHAR(MAX)
AS
BEGIN

UPDATE Player SET isDeleted = 1 WHERE PlayerID = @PlayerID

END