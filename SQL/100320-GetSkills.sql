USE [rpgmaster]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSkills]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSkills]
GO

CREATE PROCEDURE dbo.GetSkills
   
AS
BEGIN
Select SkillID, Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special From Skill;

END