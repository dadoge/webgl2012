USE [rpgmaster]

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