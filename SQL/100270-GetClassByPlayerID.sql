use [rpgmaster]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetClassByPlayerID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetClassByPlayerID]
GO

CREATE PROCEDURE dbo.GetClassByPlayerID
   @PlayerID INT
AS
BEGIN

Select c.Name, c.ImgSrc, c.Description
From Class C
inner join Player ps on ps.ClassID = c.ClassID
where ps.PlayerID = @PlayerID

END