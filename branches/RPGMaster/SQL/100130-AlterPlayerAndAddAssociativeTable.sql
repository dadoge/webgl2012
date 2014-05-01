USE [rpgmaster]
GO
/****** Object:  Table [dbo].[Player]    Script Date: 4/24/2014 8:25:30 PM ******/

ALTER TABLE [dbo].[Player] ADD [PlayerTypeID] [int] NOT NULL;
GO
ALTER TABLE [dbo].[Player] ADD [ClassID] [int] NOT NULL;
GO
ALTER TABLE [dbo].[Player] ADD [RaceID] [int] NOT NULL;
GO
ALTER TABLE [dbo].[Player] ADD [GenderID] [int] NOT NULL;
GO
ALTER TABLE [dbo].[Player] ADD [AlignmentID] [int] NOT NULL;
GO
ALTER TABLE [dbo].[Player] ADD [Level] [smallint] NULL;
GO
ALTER TABLE [dbo].[Player] ADD [Age] [smallint] NULL;
GO
ALTER TABLE [dbo].[Player] ADD [History] [nvarchar](1024) NULL;
GO

/****** Object:  Table [dbo].[PlayerType]    Script Date: 4/24/2014 8:25:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerType](
	[PlayerTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar] (25) NOT NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [pk_Type_pid] PRIMARY KEY CLUSTERED 
(
	[PlayerTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Class]    Script Date: 4/24/2014 8:25:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[ClassID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar] (25) NOT NULL,
	[Description] [nvarchar](1024) NULL,
 CONSTRAINT [pk_Class_pid] PRIMARY KEY CLUSTERED 
(
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Race]    Script Date: 4/24/2014 8:25:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Race](
	[RaceID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar] (25) NOT NULL,
	[Description] [nvarchar](1024) NULL,
 CONSTRAINT [pk_Race_pid] PRIMARY KEY CLUSTERED 
(
	[RaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Gender]    Script Date: 4/24/2014 8:25:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[GenderID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar] (25) NOT NULL,
 CONSTRAINT [pk_Gender_pid] PRIMARY KEY CLUSTERED 
(
	[GenderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Alignment]    Script Date: 4/24/2014 8:25:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alignment](
	[AlignmentID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar] (25) NOT NULL,
	[Description] [nvarchar](1024) NULL,
 CONSTRAINT [pk_Alignment_pid] PRIMARY KEY CLUSTERED 
(
	[AlignmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[PhysicalFeatures]    Script Date: 4/24/2014 8:25:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhysicalFeatures](
	[PhysicalFeaturesID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar] (25) NOT NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [pk_PhysicalFeatures_pid] PRIMARY KEY CLUSTERED 
(
	[PhysicalFeaturesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Set Foreign Keys to Altered and new Tables*************************************/
GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [fk_PlayerTypeId_psid] FOREIGN KEY(PlayerTypeID)
REFERENCES [dbo].[PlayerType] ([PlayerTypeID])
GO
ALTER TABLE [dbo].Player CHECK CONSTRAINT [fk_PlayerTypeId_psid]
GO

GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [fk_ClassId_psid] FOREIGN KEY(ClassID)
REFERENCES [dbo].[Class] ([ClassID])
GO
ALTER TABLE [dbo].Player CHECK CONSTRAINT [fk_ClassId_psid]
GO

GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [fk_RaceId_psid] FOREIGN KEY(RaceID)
REFERENCES [dbo].[Race] ([RaceID])
GO
ALTER TABLE [dbo].Player CHECK CONSTRAINT [fk_RaceId_psid]
GO

GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [fk_GenderId_psid] FOREIGN KEY(GenderID)
REFERENCES [dbo].[Gender] ([GenderID])
GO
ALTER TABLE [dbo].Player CHECK CONSTRAINT [fk_GenderId_psid]
GO

GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [fk_AlignmentId_psid] FOREIGN KEY(AlignmentID)
REFERENCES [dbo].[Alignment] ([AlignmentID])
GO
ALTER TABLE [dbo].Player CHECK CONSTRAINT [fk_AlignmentId_psid]
GO
