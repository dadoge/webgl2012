USE [rpgmaster]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 4/15/2014 8:25:30 PM ******/

ALTER TABLE [dbo].[Account] ADD [Nickname] [nvarchar](25) NULL;
GO
ALTER TABLE [dbo].[Account] ADD [Chatname] [int] NULL;
GO
ALTER TABLE [dbo].[Account] ADD [DefaultPlayerId] [int] NULL;
GO

GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [fk_DefaultPlayerId_psid] FOREIGN KEY(DefaultPlayerId)
REFERENCES [dbo].[Player] ([PlayerID])
GO
ALTER TABLE [dbo].Account CHECK CONSTRAINT [fk_DefaultPlayerId_psid]
GO