Alter PROCEDURE dbo.GetStatsByPlayerID
   @PlayerID INT
AS
BEGIN
Select s.StatID, s.Name, s.Description, ps.Value
From Stat s
inner join PlayerStat ps on ps.statID = s.statID
inner join Player p on p.playerID = ps.PlayerID
where ps.playerID = @PlayerID

END