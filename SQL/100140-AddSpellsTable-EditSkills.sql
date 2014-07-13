USE [rpgmaster]

/****** Object:  Table [dbo].[Spell]    Script Date: 4/28/2014 8:25:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spell](
	[SpellID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar] (25) NOT NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [pk_Spell_pid] PRIMARY KEY CLUSTERED 
(
	[SpellID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[PlayerSpell]    Script Date: 4/28/2014 8:25:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerSpell](
	[PlayerSpellID] [int] IDENTITY(1,1) NOT NULL,
	[PlayerID] [int] NULL,
	[SpellID] [int] NULL,
	[Value] [decimal](18, 2) NULL,
	[Min] [decimal](18, 2) NULL,
	[Max] [decimal](18, 2) NULL,
 CONSTRAINT [pk_PlayerSpell_pid] PRIMARY KEY CLUSTERED 
(
	[PlayerSpellID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Skill]    Script Date: 4/28/2014 8:25:30 PM ******/

ALTER TABLE [dbo].[PlayerSkill] ADD [Min] [decimal](18, 2) NULL;
GO
ALTER TABLE [dbo].[PlayerSkill] ADD [Max] [decimal](18, 2) NULL;
GO

/****** Set Foreign Keys to Altered and new Tables*************************************/
GO
ALTER TABLE [dbo].[PlayerSpell]  WITH CHECK ADD  CONSTRAINT [fk_PlayerId_psid] FOREIGN KEY(PlayerID)
REFERENCES [dbo].[Player] ([PlayerID])
GO
ALTER TABLE [dbo].PlayerSpell CHECK CONSTRAINT [fk_PlayerId_psid]
GO

GO
ALTER TABLE [dbo].[PlayerSpell]  WITH CHECK ADD  CONSTRAINT [fk_SpellId_psid] FOREIGN KEY(SpellID)
REFERENCES [dbo].[Spell] ([SpellID])
GO
ALTER TABLE [dbo].PlayerSpell CHECK CONSTRAINT [fk_SpellId_psid]
GO