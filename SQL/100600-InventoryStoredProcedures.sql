use [rpgmaster]

/*GetPlayerInventory*/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPlayerInventory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPlayerInventory]
GO

CREATE PROCEDURE dbo.GetPlayerInventory
	@PlayerID INT
AS
BEGIN

Select i.Name,i.Description,i.Type,iT.TypeName,i.Cost,i.MaxEffect,i.MinEffect,i.CriticalEffect,i.OtherEffect,i.Range,i.Weight,i.OtherType,i.Path,PIn.ItemQuantity
From Item i
inner join PlayerInventory PIn on Pin.ItemID = i.ItemID
inner join ItemType iT on iT.ItemTypeID = i.Type
where PIn.PlayerID = @PlayerID

END
GO

/*GetAllItems*/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllItems]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllItems]
GO

CREATE PROCEDURE dbo.GetAllItems
AS
BEGIN

Select * From Item ORDER BY [Type]

END
GO

/*GetItemTypes*/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetItemTypes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetItemTypes]
GO

CREATE PROCEDURE dbo.GetItemTypes
AS
BEGIN

Select * From ItemType where 1=1 ORDER BY [ItemTypeID]

END
GO

/*AddToPlayerInventory*/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddToPlayerInventory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddToPlayerInventory]
GO

CREATE PROCEDURE dbo.AddToPlayerInventory
	@PlayerID INT,
	@ItemID INT,
	@ItemQuantity INT
AS
BEGIN

INSERT INTO PlayerInventory (PlayerID,ItemID,ItemQuantity)
VALUES (@PlayerID, @ItemID, @ItemQuantity)

END
GO

/*UpdatePlayerInventory*/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdatePlayerInventory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdatePlayerInventory]
GO

CREATE PROCEDURE dbo.UpdatePlayerInventory
	@PlayerID INT,
	@ItemID INT,
	@ItemQuantity INT
AS
BEGIN

UPDATE PlayerInventory Set ItemQuantity = @ItemQuantity WHERE PlayerID = @PlayerID AND ItemID = @ItemID

END
GO

/*AddItem*/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddItem]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddItem]
GO

CREATE PROCEDURE dbo.AddItem
	@Name NVARCHAR (64),
	@Description NVARCHAR (128),
	@Type INT,
	@Cost INT,
	@MaxEffect NVARCHAR (64),
	@MinEffect NVARCHAR (64),
	@CriticalEffect NVARCHAR (64),
	@OtherEffect NVARCHAR (128),
	@Range NVARCHAR (64),
	@Weight DECIMAL(10,2),
	@OtherType INT,
	@Path NVARCHAR (256)
AS
BEGIN

INSERT INTO Item (Name,Description,Type,Cost,MaxEffect,MinEffect,CriticalEffect,OtherEffect,Range,Weight,OtherType,Path)
VALUES (@Name,@Description,@Type,@Cost,@MaxEffect,@MinEffect,@CriticalEffect,@OtherEffect,@Range,@Weight,@OtherType,@Path)

END
GO