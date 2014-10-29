USE [rpgmaster]

GO
/****** Object:  Table [dbo].[Map]    Script Date: 6/18/2014 8:25:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Map](
	[MapID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NULL,
	[CreatedByUserID] [nvarchar](128) NULL,
	[Tiles] [nvarchar] (MAX) NULL,
	[DateCreated] [datetime2] NOT NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [pk_Map_sid] PRIMARY KEY CLUSTERED 
(
	[MapID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/*Foreign Keys for PlayerEquipped Table*/
ALTER TABLE [dbo].[Map]  WITH CHECK ADD  CONSTRAINT [fk_CreatedByUserID_psid] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Map] CHECK CONSTRAINT [fk_CreatedByUserID_psid]
GO
ALTER TABLE [dbo].[Map] ADD CONSTRAINT DateCreatedDefult DEFAULT GETDATE() FOR DateCreated
GO