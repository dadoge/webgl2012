IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetStatsByPlayerID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetStatsByPlayerID]
GO

CREATE PROCEDURE dbo.GetStatsByPlayerID
   @PlayerID INT
AS
BEGIN
Select s.StatID, s.Name, s.Description, ps.Value
From Stat s
inner join PlayerStat ps on ps.statID = s.statID
inner join Player p on p.playerID = ps.PlayerID
where ps.playerID = @PlayerID

END