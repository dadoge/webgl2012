USE [rpgmaster]

DROP TABLE [dbo].[PlayerInventory]
DROP TABLE [dbo].[PlayerEquipped]
DROP TABLE [dbo].[Item]

/****** Object:  Table [dbo].[Item]    Script Date: 6/18/2014 8:25:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NULL,
	[Description] [nvarchar](1024) NULL,
	[Type] [INT] NOT NULL,
	[Cost] [INT] NULL,
	[MaxEffect] [nvarchar] (64) NULL,
	[MinEffect] [nvarchar] (64) NULL,
	[CriticalEffect] [nvarchar] (64) NULL,
	[OtherEffect] [nvarchar] (128) NULL,
	[Range] [nvarchar] (64) NULL,
	[Weight] [DECIMAL](10,2) NULL,
	[OtherType] [INT] NULL,
	[Path] [nvarchar] (256) NULL,
	[DateCreated] [datetime2] NOT NULL,
 CONSTRAINT [pk_ItemID_sid] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ItemType]    Script Date: 6/18/2014 8:25:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemType](
	[ItemTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](64) NULL,
 CONSTRAINT [pk_ItemTypeID_sid] PRIMARY KEY CLUSTERED 
(
	[ItemTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[PlayerEquipped]    Script Date: 6/18/2014 8:25:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerEquipped](
	[PlayerEquippedID] [int] IDENTITY(1,1) NOT NULL,
	[PlayerID] [int] NULL,
	[ItemID] [int] NULL,
	[ItemSlot] [int] NULL,
 CONSTRAINT [pk_PlayerEquipped_sid] PRIMARY KEY CLUSTERED 
(
	[PlayerEquippedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[PlayerInventory]    Script Date: 6/18/2014 8:25:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerInventory](
	[PlayerInventoryID] [int] IDENTITY(1,1) NOT NULL,
	[PlayerID] [int] NULL,
	[ItemID] [int] NULL,
	[ItemQuantity] [int] NOT NULL,
 CONSTRAINT [pk_PlayerInventory_sid] PRIMARY KEY CLUSTERED 
(
	[PlayerInventoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/*Foreign Keys/Constraints for Item Table*/
ALTER TABLE [dbo].[Item] ADD CONSTRAINT DateCreatedDefault DEFAULT GETDATE() FOR DateCreated
GO
ALTER TABLE [dbo].[Item] ADD CONSTRAINT TypeDefault DEFAULT 0 FOR [Type]
GO
ALTER TABLE [dbo].Item  WITH CHECK ADD  CONSTRAINT [fk_ItemType_psid] FOREIGN KEY([Type])
REFERENCES [dbo].[ItemType] ([ItemTypeID])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [fk_ItemType_psid]
GO

/*Foreign Keys for PlayerEquipped Table*/
ALTER TABLE [dbo].PlayerEquipped  WITH CHECK ADD  CONSTRAINT [fk_PlayerEquippedID_psid] FOREIGN KEY([PlayerID])
REFERENCES [dbo].[Player] ([PlayerID])
GO
ALTER TABLE [dbo].[PlayerEquipped] CHECK CONSTRAINT [fk_PlayerEquippedID_psid]
GO

ALTER TABLE [dbo].[PlayerEquipped]  WITH CHECK ADD  CONSTRAINT [fk_ItemEquippedID_psid] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[PlayerEquipped] CHECK CONSTRAINT [fk_ItemEquippedID_psid]
GO

/*Foreign Keys for PlayerInventory Table*/
ALTER TABLE [dbo].[PlayerInventory] ADD CONSTRAINT ItemQuantityDefault DEFAULT 0 FOR ItemQuantity
GO
ALTER TABLE [dbo].[PlayerInventory]  WITH CHECK ADD  CONSTRAINT [fk_PlayerInventoryID_psid] FOREIGN KEY([PlayerID])
REFERENCES [dbo].[Player] ([PlayerID])
GO
ALTER TABLE [dbo].[PlayerInventory] CHECK CONSTRAINT [fk_PlayerInventoryID_psid]
GO

ALTER TABLE [dbo].[PlayerInventory]  WITH CHECK ADD  CONSTRAINT [fk_ItemInventoryID_psid] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[PlayerInventory] CHECK CONSTRAINT [fk_ItemInventoryID_psid]
GO