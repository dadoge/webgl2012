use [rpgmaster]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAlignmentByPlayerID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAlignmentByPlayerID]
GO

CREATE PROCEDURE dbo.GetAlignmentByPlayerID
   @PlayerID INT
AS
BEGIN
Select a.Name, a.ImgSrc, a.Description
From Alignment a
inner join Player ps on ps.AlignmentID = a.AlignmentID
where ps.PlayerID = @PlayerID

END