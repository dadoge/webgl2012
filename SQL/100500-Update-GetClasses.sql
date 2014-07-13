USE [rpgmaster] 
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetClasses]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetClasses]
GO

CREATE PROCEDURE dbo.GetClasses
   
AS
BEGIN
Select ClassID, Name, Description, ImgSrc From Class ORDER BY Name

END