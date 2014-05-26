USE [rpgmaster] 
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRaces]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetRaces]
GO

CREATE PROCEDURE dbo.GetRaces
   
AS
BEGIN
Select RaceID, Name, Description, ImgSrc From Race;

END