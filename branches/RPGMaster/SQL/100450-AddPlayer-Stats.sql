use [rpgmaster]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddPlayerStat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddPlayerStat]
GO

CREATE PROCEDURE dbo.AddPlayerStat
	@PlayerID INT,
	@StatID INT,
	@StatValue INT
AS
BEGIN

/*INSERT Data into PlayerSkill Table*/
INSERT INTO PlayerStat (PlayerID,StatID,Value)
VALUES (@PlayerID, @StatID, @StatValue)

END