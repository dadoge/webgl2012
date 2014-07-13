use [rpgmaster]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddPlayerFeat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddPlayerFeat]
GO

CREATE PROCEDURE dbo.AddPlayerFeat
	@PlayerID INT,
	@FeatID INT
AS
BEGIN

/*INSERT Data into PlayerSkill Table*/
INSERT INTO PlayerFeat (PlayerID,FeatID)
VALUES (@PlayerID, @FeatID)

END