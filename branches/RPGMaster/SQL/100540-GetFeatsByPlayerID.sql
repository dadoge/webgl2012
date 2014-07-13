use [rpgmaster]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetFeatsByPlayerID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetFeatsByPlayerID]
GO

CREATE PROCEDURE dbo.GetFeatsByPlayerID
   @PlayerID INT
AS
BEGIN
Select s.FeatID, s.Name, s.Description
From Feat s
inner join PlayerFeat ps on ps.FeatID = s.FeatID
inner join Player p on p.playerID = ps.PlayerID
where ps.playerID = @PlayerID

END