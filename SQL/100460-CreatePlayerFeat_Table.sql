use [rpgmaster]

CREATE TABLE [dbo].[PlayerFeat](
	[PlayerFeatID] [int] IDENTITY(1,1) NOT NULL,
	[PlayerID] [int] NULL,
	[FeatID] [int] NULL
 CONSTRAINT [pk_PlayerFeat_pid] PRIMARY KEY CLUSTERED 
(
	[PlayerFeatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Set Foreign Keys to Altered and new Tables*************************************/
GO
ALTER TABLE [dbo].[PlayerFeat]  WITH CHECK ADD  CONSTRAINT [fk_FeatPlayerId_psid] FOREIGN KEY(PlayerID)
REFERENCES [dbo].[Player] ([PlayerID])
GO
ALTER TABLE [dbo].PlayerFeat CHECK CONSTRAINT [fk_FeatPlayerId_psid]
GO

GO
ALTER TABLE [dbo].[PlayerFeat]  WITH CHECK ADD  CONSTRAINT [fk_FeatId_psid] FOREIGN KEY(FeatID)
REFERENCES [dbo].[Feat] ([FeatID])
GO
ALTER TABLE [dbo].PlayerFeat CHECK CONSTRAINT [fk_FeatId_psid]
GO