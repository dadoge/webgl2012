use [rpgmaster]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetGenderByPlayerID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetGenderByPlayerID]
GO

CREATE PROCEDURE dbo.GetGenderByPlayerID
   @PlayerID INT
AS
BEGIN

Select g.Name, g.ImgSrc
From Gender G
inner join Player ps on ps.GenderID = g.GenderID
where ps.PlayerID = @PlayerID

END