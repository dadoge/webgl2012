USE [rpgmaster]

CREATE TABLE Player
(
PlayerID INT IDENTITY(1,1),
Name NVARCHAR(25)
);
CREATE TABLE Skill
(
SkillID INT IDENTITY(1,1),
Name NVARCHAR(25),
Description NVARCHAR(255),
);
CREATE TABLE Stat
(
StatID INT IDENTITY(1,1),
Name NVARCHAR(25),
Description NVARCHAR(255),
);
CREATE TABLE PlayerSkill
(
PlayerSkillID INT IDENTITY(1,1),
PlayerID INT,
SkillID INT,
Value decimal(18,2)
);
CREATE TABLE PlayerStat
(
PlayerStatID INT IDENTITY(1,1),
PlayerID INT,
StatID INT,
Value decimal(18,2)
);

ALTER TABLE Player
ADD CONSTRAINT pk_player_pid PRIMARY KEY(PlayerID)
GO

ALTER TABLE Skill
ADD CONSTRAINT pk_skill_sid PRIMARY KEY(SkillID)
GO

ALTER TABLE Stat
ADD CONSTRAINT pk_stat_sid PRIMARY KEY(StatID)
Go

ALTER TABLE PlayerStat
ADD CONSTRAINT pk_playerStat_psid PRIMARY KEY(PlayerStatID)
Go

ALTER TABLE PlayerSkill
ADD CONSTRAINT pk_playerSkill_psid PRIMARY KEY(PlayerSkillID)
Go

ALTER TABLE PlayerSkill
ADD CONSTRAINT fk_productSkill_psid FOREIGN KEY(PlayerID)REFERENCES Player(PlayerID)
GO
ALTER TABLE PlayerStat
ADD CONSTRAINT fk_productStat_psid FOREIGN KEY(PlayerID)REFERENCES Player(PlayerID)
GO
