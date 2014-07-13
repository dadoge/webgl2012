use [rpgmaster]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddPlayerSkill]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddPlayerSkill]
GO

CREATE PROCEDURE dbo.AddPlayerSkill
	@PlayerID INT,
	@SkillID INT,
	@SkillValue INT
AS
BEGIN

/*INSERT Data into PlayerSkill Table*/
INSERT INTO PlayerSkill (PlayerID,SkillID,Value)
VALUES (@PlayerID, @SkillID, @SkillValue)

END