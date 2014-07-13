USE [rpgmaster]

INSERT INTO Race (Name,Description,ImgSrc)
VALUES ('Drow','Drow are the most evil race in existence, though rarely a good Drow comes into existance due to their harsh childhood, forcing them into opposition of Drow culture. They are feared and reviled and will do almost anything to gain power. Drows look almost identical to elves, except their skin is very dark and their hair is usually white. Even if a Drow is able to escape the their harsh upbringing, the surface can be just as harsh, as few people are willing to accept them.','Race_Drow.png');
INSERT INTO Race (Name,Description,ImgSrc)
VALUES ('Half-Orc','Half-Orcs are quick to anger and act out of impulse rather than thought. They are a spawn of a Human and an Orc who have been mixed in the cross hairs of war. They are usually between 6 to 7 feet tall and typically have a strong physique, dominant jaw, and prominent teeth. Due to their Orc ancestry, who are sworn enemies of the dwarves and elves, Half-Orcs have a more difficult time getting along with these races. ','Race_Half-Orc.png');

INSERT INTO Class (Name, Description, ImgSrc)
VALUES ('Barbarian','Barbarians are raging fighters that are never lawful and come from uncivilized culture. Their main stats is Strength and Dexterity, Wisdom, and Constitution are very helpful as well.','Class_Barbarian.png')

INSERT INTO Class (Name, Description, ImgSrc)
VALUES ('Monk','Monks are fighters who are trained to fight without armor or a weapon and use their qi to harness magical-like abilities. Monks primary stats are Wisdom and Dexterity, in addition to Strength as a supplementary stats.','Class_Monk.png')

UPDATE Class SET Description = 'A Paladin is a noble and heroic warrior whose primary stats are Strength and Charisma.' WHERE Name = 'Paladin'

UPDATE Class SET Name = 'Rogue', Description = 'Rogues are skillful at obtaining what others do not want them to have. Their primary stat is Dexterity. Intelligence and Wisdom follow.', ImgSrc = 'Class_Rogue.png' WHERE Name = 'Thief'

UPDATE Class SET Name = 'Sorcerer', Description = 'A Sorcerer is a natural spellcaster who has honed his abilities through practice. Their main stat is Charisma, supplemental stats are Dexterity and Constitution.', ImgSrc = 'Class_Sorcerer.png' WHERE Name = 'Illusionist'

UPDATE Class SET Name = 'Wizard', Description = 'Wizards gained their knowledge through study and perfect it as though it is an art. Their main stat is Intelligence, and Dexterity and Constitution will help in combat.', ImgSrc = 'Class_Wizard.png' WHERE Name = 'Mage'
