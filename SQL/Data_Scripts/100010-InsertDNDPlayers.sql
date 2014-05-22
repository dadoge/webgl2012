USE [rpgmaster]

INSERT INTO Alignment (Name, Description)
VALUES ('Lawful Good','This character believes that an orderly, strong society with a well-organized government can work to make life better for the majority of the people. An honest and hard-working serf, a kind and wise king, or a stern, but forthright minister of justice are examples of lawful good.');
INSERT INTO Alignment (Name, Description)
VALUES ('Lawful Neutral','Order and organization are very important to this alignment. They believe in a strong, well-ordered government. An inquisitor determined to ferret out traitors at any cost or a soldier who never questions his orders are examples of lawful neutral.');
INSERT INTO Alignment (Name, Description)
VALUES ('Lawful Evil','These characters will use society and its laws to benefit themselves. Lawful evil characters support laws and societies that protect their own concerns. An iron-fisted tyrant and a devious, greedy merchant are examples lawful evil.');
INSERT INTO Alignment (Name, Description)
VALUES ('Neutral Good','These characters believe that the balance of forces is important, but not governed by laws and chaos. They believe that the search for good will not upset the balance and could even help maintain. A baron who violates the orders of his king to destroy something he sees as evil is an example of neutral good.');
INSERT INTO Alignment (Name, Description)
VALUES ('True Neutral','These Characters believe in the ultimate balance of forces and do not see any actions as good or evil. They do not make judgments and are very rare. These characters will take the side of the underdog and will even switch sides if it is necessary to ensure no group is too powerful. If their party defeats a tribe of evil trolls and takes them to the brink of destruction, a true neutral character, might then take sides with the trolls to ensure balance of power.');
INSERT INTO Alignment (Name, Description)
VALUES ('Neutral Evil','These characters are primarily concerned with themselves and their own advancement. They have no problem with working with a group or by themselves as long as it means they will get ahead. They will take allegiance based on power and money. An immoral mercenary, a common thief, and a double-crossing informer who betrays people to the authorities to protect and advance himself are examples of neutral evil.');
INSERT INTO Alignment (Name, Description)
VALUES ('Chaotic Good','These characters are strong individualists marked with a streak of kindness and benevolence. They believe in all virtues of goodness and right, but have little use for laws and regulation. Their actions are guided by their own moral compass, which may not always be in agreement with society. A brave frontiersman forever moving on as settlers follow in his wake is an example of chaotic good.');
INSERT INTO Alignment (Name, Description)
VALUES ('Chaotic Neutral','These character believe thier is no order to anything, even their own actions. Following this principle, they tend to follow whatever whim strikes them at the time. Chaotic neutral characters are difficult to deal with and totally totally unreliable. This is perhaps the most difficult alignment to play. Lunatics and madmen are examples of chaotic neutral.');
INSERT INTO Alignment (Name, Description)
VALUES ('Chaotic Evil','These characters are the bane of all that is good and organized. They are motivated by the desire for personal gain and pleasure. They see nothing wrong with taking whatever they want by whatever means necessary. They believe the strong have a right to take to want they want and the weak are there to be exploited. A group of chaotic evil creatures are usually brought together by a strong leader, who would be replaced at the first sign of weakness. Bloodthirsty buccaneers and monsters of low intelligence are examples of chaotic evil.');

INSERT INTO Gender (Name)
VALUES ('Male');
INSERT INTO Gender (Name)
VALUES ('Female');
INSERT INTO Gender (Name)
VALUES ('Undetermined');

INSERT INTO Race (Name,Description)
VALUES ('Dwarf',' Dwarves are short and stocky and are recognized for their stature and size. On average they are between 4'' to 4''8" tall. They have dark eyes, dark hair, and rosy cheeks. Dwarves usually live up to 350 years old. They come from mountainous or hilly regions and do not like the sea. They treasure things of the earth such as gems, rubies, diamonds and most of all, gold. They are non-magical creatures but are more suited for fighting, warcraft and scientific arts such as engineering.');
INSERT INTO Race (Name,Description)
VALUES ('Elf','Elves are somewhat shorter and slimmer than humans. Their physical features are finely chiseled and delicate, and they speak in melodic tones. Although they appear to be weak and frail, but are instead quick and strong. Elves can live for over 1,200 years, although they often feel it is time to leave the realms of men and mortals well before this time. It is uncertain, where they go, but it is an irresistible urge of the elven race. There are five main types of elves: aquatic, gray, high, wood and dark. High elves are the most common. Elves find magic and swordplay fascinating and if they have any weaknesses, it is in these interests.');
INSERT INTO Race (Name,Description)
VALUES ('Gnome','Gnomes are significantly shorter than their relative dwarven race and are less round, as they proudly maintain. Their noses, however, are significantly larger and have dark tan or brown skin and white hair. Gnomes typically live up to 350 years old. Gnomes are lively and have a sly sense of humor and have a great love for living things and finely wrought. They love precious stones and are masters at gem polishing and cutting. Gnomes tend to live in rolling, rocky hills and wooded areas that are uninhabited by humans.');
INSERT INTO Race (Name,Description)
VALUES ('Half-Elf','Half-Elves are the most common mixed-race beings. Half-Elves usually take on physical characteristics of their elven parent. Half-Elves are only such, if their ancestors are the equal or more elven than human. They are on average 5''6" and live 160 years. Half-Elves tend to mingle with both groups of humans and elves and do not have a language of their own. They have a mixture of qualities of that of humans and elf.');
INSERT INTO Race (Name,Description)
VALUES ('Halfling','Halflings are much like small humans and have round and flushed complexions, with curly hair and hair on their feet. They prefer to be barefoot and typically live up to 150 years old. Halflings enjoy peaceful and quite lives and overall enjoy their homes to a dangerous journey. They are generous and hardworking and are observant and conversational in friendly company. There are three type of halflings: Hairfeets, Tallfellows, and Stouts, where Hairfeets being the most common type.');
INSERT INTO Race (Name,Description)
VALUES ('Human','Humans are exactly as we find them on Earth, ranging from pale to very dark skin. Average height of 5’10” and can live up to 120 years old. Humans are the most social and tolerant of all the other races.  Due to their natural qualities, they tend to be major powers in the world and have ruled empires, whereas the other races, due to their own racial qualities, would find it difficult to achieve.');

INSERT INTO Class (Name,Description)
VALUES('Bard','A Bard is a Rogue that has a love for music and a natural charmer. Their main Stats are Dexterity and Charisma.');
INSERT INTO Class (Name,Description)
VALUES('Cleric','A Cleric is a priest that follows a divine entity and are sturdy soldiers. The main Stat of a Cleric is Wisdom.');
INSERT INTO Class (Name,Description)
VALUES('Druid','Druids are another type of Priest that follow Gaia, and are guardians of nature and the wildernes. Their primary Stats are Wisdom and Charisma.');
INSERT INTO Class (Name,Description)
VALUES('Fighter','A Fighter''s main Stat is Strength and is an expert in weapons, and if clever, strategy and tactics.');
INSERT INTO Class (Name,Description)
VALUES('Illusionist','An Illusionist is a Wizard that has specialized in the school of illusion. Tir main Stat is Intelligence.');
INSERT INTO Class (Name,Description)
VALUES('Mage','A Mage can learn any spells and do not specialize. Their main Stat is Intelligence.');
INSERT INTO Class (Name,Description)
VALUES('Paladin','A Paladin is a noble and heroic warrior, who''s primary Stats is Strength and Charisma.');
INSERT INTO Class (Name,Description)
VALUES('Ranger','A Ranger''s primary Stats are Stength, Dexterity, and Wisdom. They are hunters and woodsman, who live by their word and wits.');
INSERT INTO Class (Name,Description)
VALUES('Thief','The Thief is the most common Rogue, and their primary Stat is Dexterity.');

INSERT INTO PlayerType (Name, Description)
VALUES('PC','Playable Character who is played by a person.');
INSERT INTO PlayerType (Name, Description)
VALUES('NPC','Non-Playable Character who is controlled by the DM or other non-party member.');

INSERT INTO Player (Name, ImgSrc, PlayerTypeID, ClassID, RaceID, GenderID, AlignmentID, Level, Age, History)
VALUES ('Iargalon','/Images/druid_master_new_by_brucemashbatart-d5emwkp_edited_small.jpg',1,3,4,1,4,5,158,'Son of Seconial, Father (Elf), and Buerhundruen, Mother (Human). Siblings from eldeset to youngest, Geyoub, brother (Human), Gridallmurh, brother (Human), and Vian, sister (Half-Elf). An uncle names Kason who taught him his warrior skills and a faithful hound Dahlim.');
