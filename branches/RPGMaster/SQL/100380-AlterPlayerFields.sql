USE [rpgmaster]
GO
/****** Object:  Table [dbo].[Player]    Script Date: 6/18/2014 8:25:30 PM ******/

ALTER TABLE [dbo].[Player] ADD Height NVARCHAR (64) NULL;
GO
ALTER TABLE [dbo].[Player] ADD Weight NVARCHAR (64) NULL;
GO
ALTER TABLE [dbo].[Player] ADD Experience INT NULL;
GO
ALTER TABLE [dbo].[Player] ADD Money NVARCHAR (64) NULL;
GO
ALTER TABLE [dbo].[Player] ADD MaxHitPoints INT NULL;
GO