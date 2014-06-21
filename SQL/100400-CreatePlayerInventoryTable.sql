USE [rpgmaster]

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
 CONSTRAINT [pk_PlayerInventory_sid] PRIMARY KEY CLUSTERED 
(
	[PlayerInventoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Item]    Script Date: 6/18/2014 8:25:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NULL,
	[Description] [nvarchar](1024) NULL,
	[Type] int NULL,
	[Cost] [nvarchar](64) NULL,
	[MaxEffect] [nvarchar] (64) NULL,
	[MinEffect] [nvarchar] (64) NULL,
	[CriticalEffect] [nvarchar] (64) NULL,
	[OtherEffect] [nvarchar] (128) NULL,
	[Range] [nvarchar] (64) NULL,
	[Weight] [nvarchar] (64) NULL,
	[OtherType] [int] NULL,
 CONSTRAINT [pk_Item_sid] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/*Foreign Keys for PlayerInventory Table*/
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