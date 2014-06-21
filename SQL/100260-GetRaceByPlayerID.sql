use [rpgmaster]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRaceByPlayerID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetRaceByPlayerID]
GO

CREATE PROCEDURE dbo.GetRaceByPlayerID
   @PlayerID INT
AS
BEGIN

Select r.Name, r.ImgSrc, r.Description
From Race R
inner join Player ps on ps.RaceID = r.RaceID
where ps.PlayerID = @PlayerID

END