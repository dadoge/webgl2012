USE [rpgmaster]

GO
/****** Object:  Table [dbo].[Feat]    Script Date: 6/18/2014 8:25:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feat](
	[FeatID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](25) NULL,
	[Description] [nvarchar](1024) NULL,
	[PrereqMod] [nvarchar](64) NULL,
	[PrereqType] [nvarchar](64) NULL,
	[BenefitMod] [nvarchar](64) NULL,
	[BenefitTye] [nvarchar](64) NULL,
 CONSTRAINT [pk_feat_sid] PRIMARY KEY CLUSTERED 
(
	[FeatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
