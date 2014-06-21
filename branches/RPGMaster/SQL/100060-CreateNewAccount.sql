use [rpgmaster]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateNewAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateNewAccount]
GO

CREATE PROCEDURE dbo.CreateNewAccount
   @UserID NVARCHAR(128),
   @Email NVARCHAR(50)
AS
BEGIN

INSERT INTO Account (UserID, Email)
VALUES (@UserID, @Email)

END