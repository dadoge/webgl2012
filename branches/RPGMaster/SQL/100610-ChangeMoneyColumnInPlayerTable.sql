USE [rpgmaster]

/****** Object:  Table [dbo].[Player]    Script Date: 10/26/2014 10:45:30 PM ******/

ALTER TABLE [dbo].[Player] DROP COLUMN [Money]
GO
ALTER TABLE [dbo].[Player] ADD [Money] bigint NOT NULL DEFAULT 0
GO
ALTER TABLE [dbo].[Player] ADD [DateCreated] [datetime2] NOT NULL DEFAULT GETDATE()
GO