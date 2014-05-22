USE [rpgmaster] 
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAlignments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAlignments]
GO

CREATE PROCEDURE dbo.GetAlignments
   
AS
BEGIN
Select AlignmentID, Name, Description From Alignment;

END