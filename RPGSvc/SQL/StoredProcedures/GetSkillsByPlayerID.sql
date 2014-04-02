IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSkillsByPlayerID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSkillsByPlayerID]
GO

CREATE PROCEDURE dbo.GetSkillsByPlayerID
   @PlayerID INT
AS
BEGIN
Select s.SkillID, s.Name, s.Description, ps.Value
From Skill s
inner join PlayerSkill ps on ps.skillID = s.skillID
inner join Player p on p.playerID = ps.PlayerID
where ps.playerID = @PlayerID

END