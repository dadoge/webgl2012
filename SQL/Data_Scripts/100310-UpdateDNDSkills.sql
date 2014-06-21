USE [rpgmaster]

ALTER TABLE Skill
ALTER COLUMN Description NVARCHAR (1024) NULL;
GO

UPDATE Skill SET Name = 'Appraise' WHERE SkillID=1;
UPDATE Skill SET Description = 'You can appraise common or well-known objects with a DC 12 Appraise check. Failure means that you estimate the value at 50% to 150% (2d6+3 times 10%,) of its actual value. Appraising a rare or exotic item requires a successful check against DC 15, 20, or higher. If the check is successful, you estimate the value correctly; failure means you cannot estimate the item’s value. A magnifying glass gives you a +2 circumstance bonus on Appraise checks involving any item that is small or highly detailed, such as a gem. A merchant’s scale gives you a +2 circumstance bonus on Appraise checks involving any items that are valued by weight, including anything made of precious metals. These bonuses stack. Appraising an item takes 1 minute (ten consecutive full-round actions).' Where SkillID=1;
UPDATE Skill SET ImgSrc = '' WHERE SkillID=1;
UPDATE Skill SET KeyStatID = '4' WHERE SkillID=1;
UPDATE Skill SET Trained = '0' WHERE SkillID=1;
UPDATE Skill SET ACPenalty = '0' WHERE SkillID=1;
UPDATE Skill SET Retry = '0' WHERE SkillID=1;
UPDATE Skill SET OpposingSkillID = '0' WHERE SkillID=1;
UPDATE Skill SET Special = '+2 to metals and stones for dwarves. +3 for master of a raven fimiliar. +2 for Diligent feat.' WHERE SkillID=1;

UPDATE Skill SET Name = 'Autohypnosis' WHERE SkillID=2;
UPDATE Skill SET Description = '' Where SkillID=2;
UPDATE Skill SET ImgSrc = '' WHERE SkillID=2;
UPDATE Skill SET KeyStatID = '5' WHERE SkillID=2;
UPDATE Skill SET Trained = '1' WHERE SkillID=2;
UPDATE Skill SET ACPenalty = '0' WHERE SkillID=2;
UPDATE Skill SET Retry = '1' WHERE SkillID=2;
UPDATE Skill SET OpposingSkillID = '0' WHERE SkillID=2;
UPDATE Skill SET Special = '' WHERE SkillID=2;

INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Balance','','','2','0','1','1','0','+2 on Balance Checks for Agile:Feat');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Bluff','','','6','0','0','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Climb','','','1','0','1','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Concentration','','','3','0','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Control Shape','','','5','0','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Craft','','','4','0','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Decipher Script','','','4','1','0','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Diplomacy','','','6','0','0','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Disable Device','','','4','1','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Disguise','','','6','0','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Escape Artist','','','2','0','1','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Forgery','','','4','0','0','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Gather Information','','','6','0','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Handle Animal','','','6','1','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Heal','','','5','0','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Hide','','','2','0','1','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Intimidate','','','6','0','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Jump','','','1','0','1','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Knowledge Arcana','','','4','1','0','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Knowledge Architecture and Engineering','','','4','1','0','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Knowledge Dungeoneering','','','4','1','0','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Knowledge Geography','','','4','1','0','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Knowledge History','','','4','1','0','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Knowledge Local','','','4','1','0','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Knowledge Nature','','','4','1','0','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Knowledge Nobility and Royalty','','','4','1','0','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Knowledge Religion','','','4','1','0','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Knowledge The Planes','','','4','1','0','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Listen','','','5','0','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Move Silently','','','2','0','1','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Open Lock','','','2','1','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Perform','','','6','0','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Psicraft','','','4','1','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Profession','','','5','1','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Ride','','','2','0','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Search','','','4','0','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Sense Motive','','','5','0','0','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Sleight of Hand','','','2','1','1','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Speak Language','','','0','1','0','0','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Spellcraft','','','4','1','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Spot','','','5','0','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Survival','','','5','0','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Swim','','','1','0','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Tumble','','','2','1','1','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Use Magic Device','','','6','1','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Use Psionic Device','','','6','1','0','1','0','');
INSERT INTO Skill (Name, Description, ImgSrc, KeyStatID, Trained, ACPenalty, Retry, OpposingSkillID, Special)
VALUES ('Use Rope','','','2','0','0','1','0','');

