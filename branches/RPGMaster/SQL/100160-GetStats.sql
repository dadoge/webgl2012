USE [rpgmaster] 
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetStats]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetStats]
GO

CREATE PROCEDURE dbo.GetStats
   
AS
BEGIN
Select StatID, Name, Description From Stat;

END