USE [rpgmaster] 
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetFeats]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetFeats]
GO

CREATE PROCEDURE dbo.GetFeats
   
AS
BEGIN
Select FeatID, Name, Description From Feat;

END