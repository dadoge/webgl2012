USE [rpgmaster]

INSERT INTO ItemType (TypeName)
VALUES ('None');
INSERT INTO ItemType (TypeName)
VALUES ('Container');
INSERT INTO ItemType (TypeName)
VALUES ('Consumable');
INSERT INTO ItemType (TypeName)
VALUES ('NonConsumable');
INSERT INTO ItemType (TypeName)
VALUES ('Weapon');
INSERT INTO ItemType (TypeName)
VALUES ('Clothing');
INSERT INTO ItemType (TypeName)
VALUES ('Armor');

/*Container Items*/
INSERT INTO [dbo].[Item] (Name,Description,Type,Cost,MaxEffect,MinEffect,CriticalEffect,OtherEffect,Range,Weight,OtherType)
VALUES ('Backpack','Backpack for general storage',2,2000000,'200lbs','0lbs','','','',2.0,0);
INSERT INTO [dbo].[Item] (Name,Description,Type,Cost,MaxEffect,MinEffect,CriticalEffect,OtherEffect,Range,Weight,OtherType)
VALUES ('Pouch Belt','Pouch belt for those smaller items',2,1000000,'20lbs','0lbs','','','',0.5,0);
INSERT INTO [dbo].[Item] (Name,Description,Type,Cost,MaxEffect,MinEffect,CriticalEffect,OtherEffect,Range,Weight,OtherType)
VALUES ('Sack','Small sack for carrying small to medium items',2,1000,'80lbs','0lbs','','','',0.5,0);

/*Consumable Items*/
INSERT INTO [dbo].[Item] (Name,Description,Type,Cost,MaxEffect,MinEffect,CriticalEffect,OtherEffect,Range,Weight,OtherType)
VALUES ('Trail Ration','Yummy food for on the go',3,5000,'1 day','1 day','','','',1.0,0);
INSERT INTO [dbo].[Item] (Name,Description,Type,Cost,MaxEffect,MinEffect,CriticalEffect,OtherEffect,Range,Weight,OtherType)
VALUES ('Diamond Dust','Remnants of a beautiful gem',3,250000000,'','','','','',0.5,0);
INSERT INTO [dbo].[Item] (Name,Description,Type,Cost,MaxEffect,MinEffect,CriticalEffect,OtherEffect,Range,Weight,OtherType)
VALUES ('Soap','Stay fresh, daily',3,5000,'','','','','',1,0);
INSERT INTO [dbo].[Item] (Name,Description,Type,Cost,MaxEffect,MinEffect,CriticalEffect,OtherEffect,Range,Weight,OtherType)
VALUES ('Paper','Mhmmm, smells like trees',3,4000,'','','','','',0.0,0);
INSERT INTO [dbo].[Item] (Name,Description,Type,Cost,MaxEffect,MinEffect,CriticalEffect,OtherEffect,Range,Weight,OtherType)
VALUES ('Ink','This is ink, for writing or things',3,8000000,'','','','','',0.1,0);

/*NonConsumable Items*/
INSERT INTO [dbo].[Item] (Name,Description,Type,Cost,MaxEffect,MinEffect,CriticalEffect,OtherEffect,Range,Weight,OtherType)
VALUES ('Tent','Protection from the elements',4,10000000,'','','','','',20.0,0);
INSERT INTO [dbo].[Item] (Name,Description,Type,Cost,MaxEffect,MinEffect,CriticalEffect,OtherEffect,Range,Weight,OtherType)
VALUES ('Bedroll','Comfy bed for the outdoors',4,1000,'','','','','',5.0,0);
INSERT INTO [dbo].[Item] (Name,Description,Type,Cost,MaxEffect,MinEffect,CriticalEffect,OtherEffect,Range,Weight,OtherType)
VALUES ('Winter Blanket','A very warm blanket',4,5000,'','','','','',3.0,0);
INSERT INTO [dbo].[Item] (Name,Description,Type,Cost,MaxEffect,MinEffect,CriticalEffect,OtherEffect,Range,Weight,OtherType)
VALUES ('Mirror','Hey! I see you...me',4,10000000,'','','','','',0.5,0);
INSERT INTO [dbo].[Item] (Name,Description,Type,Cost,MaxEffect,MinEffect,CriticalEffect,OtherEffect,Range,Weight,OtherType)
VALUES ('Iron Pot','Good for fixing up some grub',4,5000,'','','','','',10.0,0);
INSERT INTO [dbo].[Item] (Name,Description,Type,Cost,MaxEffect,MinEffect,CriticalEffect,OtherEffect,Range,Weight,OtherType)
VALUES ('Climber''s Kit','Gear to help you up the face',4,80000000,'','','','','',5.0,0);
