CREATE PROCEDURE dbo.GetPlayerByPlayerID
   @PlayerID INT
AS
BEGIN
Select p.playerID, p.Name
From Player P
where p.playerID = @PlayerID

END