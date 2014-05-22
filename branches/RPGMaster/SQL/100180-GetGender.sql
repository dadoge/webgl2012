USE [rpgmaster] 
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetGenders]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetGenders]
GO

CREATE PROCEDURE dbo.GetGenders
   
AS
BEGIN
Select GenderID, Name From Gender;

END