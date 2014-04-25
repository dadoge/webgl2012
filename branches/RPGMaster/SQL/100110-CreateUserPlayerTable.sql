USE [rpgmaster]

/****** Object:  Table [dbo].[UserPlayer]    Script Date: 4/20/2014 8:25:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPlayer](
	[UserPlayerID] [int] IDENTITY(1,1) NOT NULL,
	[PlayerID] [int] NULL,
	[UserID] [nvarchar](128) NULL,
 CONSTRAINT [pk_UserPlayer_psid] PRIMARY KEY CLUSTERED 
(
	[UserPlayerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[UserPlayer]  WITH CHECK ADD  CONSTRAINT [fk_PlayerUserID_psid] FOREIGN KEY([PlayerID])
REFERENCES [dbo].[Player] ([PlayerID])
GO
ALTER TABLE [dbo].[UserPlayer] CHECK CONSTRAINT [fk_PlayerUserID_psid]
GO
ALTER TABLE [dbo].[UserPlayer]  WITH CHECK ADD  CONSTRAINT [fk_UserPlayerID_psid]  FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UserPlayer] CHECK CONSTRAINT [fk_UserPlayerID_psid] 
GO