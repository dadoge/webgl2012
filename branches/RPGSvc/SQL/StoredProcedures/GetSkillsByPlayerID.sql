Alter PROCEDURE dbo.GetSkillsByPlayerID
   @PlayerID INT
AS
BEGIN
Select s.SkillID, s.Name, s.Description, ps.Value
From Skill s
inner join PlayerSkill ps on ps.skillID = s.skillID
inner join Player p on p.playerID = ps.PlayerID
where ps.playerID = @PlayerID

END