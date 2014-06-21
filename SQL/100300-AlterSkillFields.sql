USE [rpgmaster]
GO
/****** Object:  Table [dbo].[Skill]    Script Date: 6/12/2014 8:25:30 PM ******/

ALTER TABLE [dbo].[Skill] ADD KeyStatID INT NULL;
GO
ALTER TABLE [dbo].[Skill] ADD Trained INT NULL;
GO
ALTER TABLE [dbo].[Skill] ADD ACPenalty INT NULL;
GO	
ALTER TABLE [dbo].[Skill] ADD Retry INT NULL;
GO
ALTER TABLE [dbo].[Skill] ADD OpposingSkillID INT NULL;
GO
ALTER TABLE [dbo].[Skill] ADD Special NVARCHAR (1024) NULL;
GO

/****** Object:  Table [dbo].[ClassSkill]    Script Date: 6/12/2014 8:25:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassSkill](
	[ClassSkillID] [int] IDENTITY(1,1) NOT NULL,
	[ClassID] [int] NULL,
	[SkillID] [int] NULL,
 CONSTRAINT [pk_ClassSkill_psid] PRIMARY KEY CLUSTERED 
(
	[ClassSkillID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ClassSkill]  WITH CHECK ADD  CONSTRAINT [fk_ClassSkillID_psid] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Class] ([ClassID])
GO
ALTER TABLE [dbo].[ClassSkill] CHECK CONSTRAINT [fk_ClassSkillID_psid]
GO
ALTER TABLE [dbo].[ClassSkill]  WITH CHECK ADD  CONSTRAINT [fk_SkillClassID_psid]  FOREIGN KEY([SkillID])
REFERENCES [dbo].[Skill] ([SkillID])
GO
ALTER TABLE [dbo].[ClassSkill] CHECK CONSTRAINT [fk_SkillClassID_psid] 
GO
