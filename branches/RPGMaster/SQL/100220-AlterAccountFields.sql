USE [rpgmaster]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 4/15/2014 8:25:30 PM ******/

ALTER TABLE [dbo].[Account] DROP COLUMN [Chatname]
GO
ALTER TABLE [dbo].[Account] ADD [Chatname] [nvarchar] (36) NULL;
GO

