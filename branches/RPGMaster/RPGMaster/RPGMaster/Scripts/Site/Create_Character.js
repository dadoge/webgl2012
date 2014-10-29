NewCharacter = function (data) {//(race, stats, alignment, gender, name, age, level, history) {
    //Used for knockout model for building Character
    var self = this;
    self.Race = "1";
    self.Stats = ko.observableArray(data.Stats);
    self.Skills = ko.observableArray(data.Skills);
    self.Feats = ko.observableArray(data.Feats);
    self.Class = "1";
    self.Alignment = ko.observable(data.Alignments[3]);    
    self.Gender = ko.observable(data.Genders[0]);
    self.Name = ko.observable("Name");
    self.Age = ko.observable("25");
    self.Level = ko.observable(1);
    self.History = ko.observable("");
    self.HP = ko.observable("12");
    self.Experience = ko.observable("0");
    self.Height = ko.observable("5'8\"");
    self.Weight = ko.observable("145");
    self.Money = ko.observableArray([{ pieces: 0 }, { pieces: 0 }, { pieces: 0 }, { pieces: 10 }]);

    //self.SelectedSkillYo = ko.observableArray([]);

    //for (var i; i < data.Skills.length ; i++) {
    //    self.SelectedSkillYo()[i] = data.Skills[i].SelectedSkills;
    //}

    //function to change selector box for skills and set observable
    self.ChangeSelection = function (mine) {
        var found;
        var count=0;
        while (found != true) {
            found = (self.Skills()[count].Id == mine.Id);
            count++;
        }
        count--;
        self.Skills()[count].SelectedSkill = !mine.SelectedSkill; //(!mine.SelectedSkill);
        var SkillValueID = '#SkillValue-' + mine.Id.toString();
        var SkillCheckboxID = '#SkillCheckbox-' + mine.Id.toString();
        var SkillNameID = '#SkillName-' + mine.Id.toString();
        $(SkillValueID).toggle();
        if (mine.SelectedSkill == 1 || mine.SelectedSkill == true) {
            $(SkillCheckboxID).css({ boxShadow: 'inset -1px -1px 2px rgba(20,20,20,.9)' });
            $(SkillCheckboxID).css({ background: '#FFFFFF' });
            $(SkillNameID).css({ margin: '0em 0em 0em 0em' });
        }
        else {
            $(SkillCheckboxID).css({ boxShadow: 'inset 1px 1px 2px rgba(20,20,20,.9)' });
            $(SkillCheckboxID).css({ background: '#888888' });
            $(SkillNameID).css({ margin: '0em 0em 0em 2.7em' });
        }
    };

    self.UpdateSkillCheckbox = function () {
        var SkillCheckboxID;
        for (var i = 0; i < self.Skills().length; i++) {
            SkillCheckboxID = '#SkillCheckbox-' + self.Skills()[i].Id.toString();
            SkillNameID = '#SkillName-' + self.Skills()[i].Id.toString();
            if (self.Skills()[i].SelectedSkill == 1) {
                $(SkillCheckboxID).css({ boxShadow: 'inset -1px -1px 2px rgba(20,20,20,.9)' });
                $(SkillCheckboxID).css({ background: '#FFFFFF' });
                $(SkillNameID).css({ margin: '0em 0em 0em 0em' });
            }
            else {
                $(SkillCheckboxID).css({ boxShadow: 'inset 1px 1px 2px rgba(20,20,20,.9)' });
                $(SkillCheckboxID).css({ background: '#888888' });
                $(SkillNameID).css({ margin: '0em 0em 0em 2.7em' });
            }
        }
    };

    self.CharacterStats = ko.computed(function () {
        return self.Stats().Id;
    });

    //var returnAlignment = $.parseJSON(ko.toJSON(newCharacter.Alignment));
    //self.AlignmentDescription = ko.computed(function () {
    //    var description = self.Alignment.Description;
    //    return description;
    //});
    self.updateRaceIcon = function () {
        $('#' + self.Race + '').css('backgroundColor', 'rgba(255, 255, 255, 1)');
        $('#' + self.Race + '').css('border-color', '#A8A8A8');
    }
    self.SelectRace = function (data, event) {
        $('#' + self.Race + '').css('backgroundColor', 'rgba(255, 255, 255, .75)');
        $('#' + self.Race + '').css('border-color', '#282828');
        $('#' + event.currentTarget.id + '').css('backgroundColor', 'rgba(255, 255, 255, 1)');
        $('#' + event.currentTarget.id + '').css('border-color', '#A8A8A8');
        self.Race = event.currentTarget.id;
    }
    self.updateClassIcon = function () {
        $('#' + self.Class + '').css('backgroundColor', 'rgba(255, 255, 255, 1)');
        $('#' + self.Class + '').css('border-color', '#A8A8A8');
    }
    self.SelectClass = function (data, event) {
        $('#' + self.Class + '').css('backgroundColor', 'rgba(255, 255, 255, .75)');
        $('#' + self.Class + '').css('border-color', '#282828');
        $('#' + event.currentTarget.id + '').css('backgroundColor', 'rgba(255, 255, 255, 1)');
        $('#' + event.currentTarget.id + '').css('border-color', '#A8A8A8');
        self.Class = event.currentTarget.id;
    }

    self.availableStats = ko.observableArray(data.Stats);
    self.availableClasses = ko.observableArray(data.Classes);
    self.availableAlignments = ko.observableArray(data.Alignments);
    self.availableGenders = ko.observableArray(data.Genders);

}

CreateNewCharacter = function () { //(raceID, stats, skills, classID, alignmentID, genderID, name, age, level, history) {//(race, stats, alignment, gender, name, age, level, history) {
    this.PlayerTypeID;
    this.UserName;
    this.ImgSrc;
    this.Race = {};
    this.Stats = [];
    this.Skills = [];
    this.Feats = [];
    this.Class = {};
    this.Alignment = {};
    this.Gender = {};
    this.Name;
    this.Age;
    this.Height;
    this.Weight;
    this.Level;
    this.History
    this.Experience;
    this.Money;
    this.MaxHitPoints;
}

//Stores Active Character View Model
ActiveCharacterVMod = function (data) {
    var self = this;
    self.Race = ko.observable(data.Race.Name);
    self.Stats = ko.observableArray(data.Stats);
    self.Skills = ko.observableArray(data.Skills);
    self.Feats = ko.observableArray(data.Feats);
    self.Class = ko.observable(data.Class.Name);;
    self.Alignment = ko.observable(data.Alignment.Name);
    self.Gender = ko.observable(data.Gender.Name);
    self.Name = ko.observable(data.Name);
    self.Age = ko.observable(data.Age);
    self.Level = ko.observable(data.Level);
    self.History = ko.observable(data.History);
    self.HP = ko.observable(data.MaxHitPoints);
    self.Experience = ko.observable(data.Experience);
    self.Height = ko.observable(data.Height);
    self.Weight = ko.observable(data.Weight);
    self.Money = ko.observable(data.Money);
};

InventoryVMod = function () {
    var self = this;
    self.PlayerItems = ko.observable();
    self.Items = ko.observableArray([]);
    self.toBuy = ko.observableArray([]); // {Name: '',id: 1,cost: 100, quantity: 1}
    self.Item = ko.observable({
        Id: 0,
        Name: '',
        Description: '',
        Type: 1,
        TypeName: '',
        Cost: [{ pieces: 0 }, { pieces: 0 }, { pieces: 0 }, { pieces: 0 }],
        MaxEffect: '',
        MinEffect: '',
        CriticalEffect: '',
        OtherEffect: '',
        Range: '',
        Weight: 0.0,
        OtherType: 0,
        Path: '',
        ItemQuantity: 0
    });
    self.ItemType = ko.observableArray();

    self.InitItem = function () {
        self.Item().Id = 0;
        self.Item().Name = '';
        self.Item().Description = '';
        self.Item().Type = 1;
        self.Item().TypeName = '';
        self.Item().Cost = [{ pieces: 0 }, { pieces: 0 }, { pieces: 0 }, { pieces: 0 }];
        self.Item().MaxEffect = '';
        self.Item().MinEffect = '';
        self.Item().CriticalEffect = '';
        self.Item().OtherEffect = '';
        self.Item().Range = '';
        self.Item().Weight = 0.0;
        self.Item().OtherType = 0;
        self.Item().Path = '';
        self.Item().ItemQuantity = 0;
    };

    self.UpdateToBuy = function (mine) {
        var count = 0;
        var found, exceededCount;
        //Get index of array if found
        var ItemName = mine.id.substring(6, mine.id.length).replace(/-/g, ' ').replace(/_/g, '\'');
        var itemMax = self.Items().length;
        //Loop Items until Name is found or end of array
        while (found != true && exceededCount != true) {
            found = (self.Items()[count].Name == ItemName);
            exceededCount = (count > itemMax);
            count++;
        }
        count--;
        //Check to see if Item exists in the toBuy array
        var indexofToBuy = self.toBuy().indexOf(count);
        if (indexofToBuy < 0) {
            //Add to Array if not in ToBuy array
            self.toBuy().push(count);
            //Set ItemQuantity to atleast 1
            if (self.Items()[count].ItemQuantity==0) {
                $('#input-ToBuy-' + mine.id.substring(6, mine.id.length)).val(1).change();
            }
            $('.buy-glyph').show();
        }
        else {
            var newArray = self.toBuy();
            newArray.splice(indexofToBuy, 1);
            self.toBuy(newArray);
            if (newArray.length < 1) {
                $('.buy-glyph').hide();
            }
        }        
    };
    self.getCostText = function (Cost,Weight) {
        var Money = gameRules.convertMoneyToObject(Cost);
        var outputText = "";
        var pOut = [[Money[0].pieces.toString() + "pp "],
            [Money[1].pieces.toString() + "gp "],
            [Money[2].pieces.toString() + "sp "],
            [Money[3].pieces.toString() + "cp"]];

        for (var i = 0; i < pOut.length; i++) {
            if (Money[i].pieces == 0) {
                pOut[i]="";
            }
        }

        return [outputText + pOut[0] + pOut[1] + pOut[2] + pOut[3] + '<div class=\"viewList-Weight\">' + Weight + 'lbs</div>'];
    };
};

var createCharacterViewModel = {
    availableAlignments: ko.observableArray()
};

ManageCharacters = function () {
    this.Mine;
};

CancelConfirm = function () {
    alertBox('Are you sure you want to cancel?', function () {
        $('#interactive-inner').fadeOut(400, function () {
            $('#interactive-inner').html("");
            $('#interactive-inner').toggle();
        });
    });
};

SelectRace = function (PreviousSelection) {
    PreviousSelection.style.backgroundColor=(rgba(255, 255, 255, .75));
    this.style.backgroundColor = rgba(255, 255, 255, 1);
    return this;
};

LoadPlayer = function () {

    $.getJSON(["http://www.antonmorgan.com/rpgsvc/rpgsvc/getplayer/" + ActivePlayerID], function (data) {
        var PlayerInventory=[];
        if (data.Inventory != undefined) {
            PlayerInventory = data.Inventory;
        }
        var PlayerMoney = data.Money;
        var Stats_template = templateHelper.getStatsHtml({ Stats: data.Stats });
        var Skills_template = templateHelper.getSkillsHtml({ Skills: data.Skills });
        var Feats_template = templateHelper.getFeatsHtml({ Feats: data.Feats });
        var Inventory_template = templateHelper.getInventoryHtml({ Inventory: data.Inventory }, gameRules.convertMoneyToObject(data.Money), { Name: data.Name });
        var General_template = templateHelper.getGeneralHtml({ Class: data.Class.Name }, { Race: data.Race.Name }, { Alignment: data.Alignment.Name }, { Age: data.Age }, { Height: data.Height }, { Weight: data.Weight }, { History: data.History });
        

        $('.character-img-wrapper').css("background-image", ["url(../Images/" + data.ImgSrc + ")"]);
        $('#character-text').html(General_template);
        $('.character-icon-footer').html(templateHelper.getCharFooterHtml({ Name: data.Name }, { Level: data.Level }))
        $("#options-character-info").val("General");

        $("#options-character-info").change(function () {
            if (document.getElementById('options-character-info').selectedIndex == 0) {
                $('#character-text').html(General_template);
            }
            else if (document.getElementById('options-character-info').selectedIndex == 1) {
                $('#character-text').html(Stats_template);
            }
            else if (document.getElementById('options-character-info').selectedIndex == 2) {
                $('#character-text').html(Skills_template);
                $('.viewList').click(function (data, event) {
                    $('#character-Skills-Description-' + data.currentTarget.id).toggle();
                })
            }
            else if (document.getElementById('options-character-info').selectedIndex == 3) {
                $('#character-text').html(Feats_template);
                $('.viewList').click(function (data, event) {
                    $('#character-Feats-Description-' + data.currentTarget.id).toggle();
                })
            }
            else if (document.getElementById('options-character-info').selectedIndex == 4) {
                $('#character-text').html(Inventory_template);
                var inventoryVMod = new InventoryVMod();
                //Load Player Inventory
                $.getJSON(["http://www.antonmorgan.com/rpgsvc/rpgsvc/GetPlayerInventory/" + ActivePlayerID], function (data) {
                    PlayerInventory = data;
                    $('.playerInventory').html(templateHelper.getPlayerInventoryHtml({ Inventory: data }));
                    //click function to expand information
                    $('.viewList').click(function (data, event) {
                        $('#character-Item-Description-' + data.currentTarget.id).toggle();
                    })
                });
                //set click function for changing settings
                $('.edit-glyph').click(function (data, event) {
                    inventoryVMod.InitItem();
                    $.getJSON("http://www.antonmorgan.com/rpgsvc/rpgsvc/GetItemTypes", function (data) {
                        var ItemType = [];
                        for (var i = 0; i < data.Name.length; i++) {
                            ItemType.push({ Name: data.Name[i], TypeID: data.TypeID[i] })
                        }
                        inventoryVMod.ItemType(ItemType);
                        alertBox(templateHelper.addItemHtml(), function () {
                            var Error = 0;
                            var data = inventoryVMod.Item();
                            if (data.Name == '') {
                                $('#alertError').text("Please enter in a Name.");
                                Error = 1;
                            }
                            else if (gameRules.convertMoneyToSingle(data.Cost) <= 0 || isNaN(gameRules.convertMoneyToSingle(data.Cost))) {
                                $('#alertError').text("Please enter in a valid Cost.");
                                Error = 1;
                            }
                            else if (data.Description == '') {
                                $('#alertError').text("Please enter in a Description.");
                                Error = 1;
                            }
                            else if (data.Weight <= 0 || isNaN(data.Weight)) {
                                $('#alertError').text("Please enter in a valid Weight.");
                                Error = 1;
                            }
                            else {
                                data.Cost = gameRules.convertMoneyToSingle(data.Cost);
                                $.ajax({
                                    url: 'http://www.antonmorgan.com/rpgsvc/rpgsvc/AddItem',
                                    type: 'POST',
                                    data: JSON.stringify(data),
                                    contentType: 'application/json',
                                    dataType: 'json'
                                });
                                Error = 0;
                            }
                            return Error;
                        }, undefined, 1, inventoryVMod, 'createItem');
                    });
                });
                //set click function for search
                $('#item-search').click(function (data, event) {
                    var keyword = $('.input-searchItems').val();
                    if ($('.itemList').is(":visible") == 0) {
                        $.getJSON(["http://www.antonmorgan.com/rpgsvc/rpgsvc/GetAllItems"], function (data) {
                            inventoryVMod.Items(data);
                            var Items_template = templateHelper.getItemListHtml({ Items: data });
                            $('.itemList').html(Items_template);

                            ko.applyBindings(inventoryVMod, document.getElementById('ul-itemList'));
                            //set function for search
                            $('.itemSearchList').click(function (data, event) {
                                //check to see the buy selector is the target
                                if (data.target.id.search('ToBuy-') == 0) {
                                    var test = $('#' + data.target.id).css('background-color');
                                    inventoryVMod.UpdateToBuy(data.target);
                                    if (test == 'rgb(136, 136, 136)') {
                                        $('#' + data.target.id).css({ 'background-color': '#FFFFFF' });
                                        $('#' + data.target.id).css({ boxShadow: 'inset -1px -1px 2px rgba(20,20,20,.9)' });
                                        $('#input-' + data.target.id).toggle();
                                    }
                                    else {
                                        $('#' + data.target.id).css({ 'background-color': '#888888' });
                                        $('#' + data.target.id).css({ boxShadow: 'inset 1px 1px 2px rgba(20,20,20,.9)' });
                                        $('#input-' + data.target.id).toggle();
                                    }
                                }
                                else if ((data.target.id.search('input-ToBuy-') != 0)) {
                                    $('#ItemList-Description-' + data.currentTarget.id).toggle();
                                }
                            })
                        });
                        $('.itemList').show();
                    }
                    else {
                        $('.itemList').hide();
                        $('.itemList').html('');
                    }
                })
                //create click function to buy the items
                $('.buy-glyph').click(function () {
                    alertBox("Would you like to buy these items?", function () {
                        var itemsToAdd = [];
                        var itemsToUpdate = [];
                        var newItemQuantity = [];
                        var found = 0;
                        var j = 0;
                        //check to see which items to buy need added and which ones need updated
                        for (var i = 0; i < inventoryVMod.toBuy().length; i++) {
                            j = 0; found = false;
                            while (found != true && j < PlayerInventory.length) {
                                found = (inventoryVMod.Items()[inventoryVMod.toBuy()[i]].Name == PlayerInventory[j].Name);
                                j++;
                            }
                            if (found == true) {
                                itemsToUpdate.push(inventoryVMod.toBuy()[i]);
                                newItemQuantity.push(parseInt(inventoryVMod.Items()[inventoryVMod.toBuy()[i]].ItemQuantity) + parseInt(PlayerInventory[j - 1].ItemQuantity));
                            }
                            else {
                                itemsToAdd.push(inventoryVMod.toBuy()[i]);
                            }
                        }
                        //Call Post
                        var jsonData = [{}];
                        jsonData[0].PlayerID = 0;
                        for (i = 0; i < itemsToAdd.length; i++) {
                            jsonData[0].PlayerID = ActivePlayerID;
                            jsonData[0].ItemID = inventoryVMod.Items()[itemsToAdd[i]].Id;
                            jsonData[0].ItemQuantity = parseInt(inventoryVMod.Items()[itemsToAdd[i]].ItemQuantity);
                            if (itemsToUpdate.length < 1 && i >= (itemsToAdd.length - 1)) {
                                $.ajax({
                                    url: 'http://www.antonmorgan.com/rpgsvc/rpgsvc/AddToPlayerInventory',
                                    type: 'POST',
                                    data: JSON.stringify(jsonData),
                                    contentType: 'application/json',
                                    dataType: 'json',
                                    complete: function () {
                                        //Reload Player Inventory
                                        $.getJSON(["http://www.antonmorgan.com/rpgsvc/rpgsvc/GetPlayerInventory/" + ActivePlayerID], function (data) {
                                            PlayerInventory = data;
                                            $('.playerInventory').html(templateHelper.getPlayerInventoryHtml({ Inventory: data }));
                                            //reapply click function
                                            $('.viewList').click(function (data, event) {
                                                $('#character-Item-Description-' + data.currentTarget.id).toggle();
                                            })
                                        });
                                    }
                                });
                            }
                            else {
                                $.ajax({
                                    url: 'http://www.antonmorgan.com/rpgsvc/rpgsvc/AddToPlayerInventory',
                                    type: 'POST',
                                    data: JSON.stringify(jsonData),
                                    contentType: 'application/json',
                                    dataType: 'json'
                                });
                            }
                        }
                        for (i = 0; i < itemsToUpdate.length; i++) {
                            jsonData[0].PlayerID = ActivePlayerID;
                            jsonData[0].ItemID = inventoryVMod.Items()[itemsToUpdate[i]].Id;
                            jsonData[0].ItemQuantity = newItemQuantity[i];
                            if (i >= (itemsToUpdate.length - 1)) {
                                $.ajax({
                                    url: 'http://www.antonmorgan.com/rpgsvc/rpgsvc/UpdatePlayerInventory',
                                    type: 'POST',
                                    data: JSON.stringify(jsonData),
                                    contentType: 'application/json',
                                    dataType: 'json',
                                    complete: function () {
                                        //Reload Player Inventory
                                        $.getJSON(["http://www.antonmorgan.com/rpgsvc/rpgsvc/GetPlayerInventory/" + ActivePlayerID], function (data) {
                                            PlayerInventory = data;
                                            $('.playerInventory').html(templateHelper.getPlayerInventoryHtml({ Inventory: data }));
                                            //reapply click function
                                            $('.viewList').click(function (data, event) {
                                                $('#character-Item-Description-' + data.currentTarget.id).toggle();
                                            })
                                        });
                                    }
                                });
                            }
                            else {
                                $.ajax({
                                    url: 'http://www.antonmorgan.com/rpgsvc/rpgsvc/UpdatePlayerInventory',
                                    type: 'POST',
                                    data: JSON.stringify(jsonData),
                                    contentType: 'application/json',
                                    dataType: 'json'
                                });
                            }
                        }

                        //Remove Item Search and clear
                        $('.itemList').hide();
                        $('.itemList').html('');
                        $('.buy-glyph').hide();
                        inventoryVMod.toBuy([]);
                    });
                })
            }

        });
    });
}

LoadUserPlayer = function () {
    $.getJSON(["http://www.antonmorgan.com/rpgsvc/rpgsvc/getuserinfo/" + $('#UID').html()], function (data) {
        ChatName = data.ChatName;
        $('#chatname-input').val(ChatName);
        $.connection.hub.start().done(function () {
            $.sendFirstLogon($('#chatname-input').val());
            $("#chat-input").keypress(function (event) {
                if (event && event.keyCode == 13) {
                    if ($('#chatname-input').val() == "") {
                        alertBox("Please enter in username.")
                    }
                    else {
                        rpgProxy.server.sendMessage($('#chatname-input').val(), $('#chat-input').val());
                        $('#chat-input').val("");
                    }
                }
            });
        });
        ActivePlayerID = data.ActivePlayer;
        LoadPlayer();
    });
}

CreateNewCharacterClick = function (templateHelper) {
    //setup the buttons
    $('#interactive-inner').html(templateHelper.startCreation());
    //$('.Selection').tooltip("close");
    $('#CreateCharacter-Cancel').click(function () { CancelConfirm() });
    $.getJSON("http://www.antonmorgan.com/rpgsvc/rpgsvc/createnewcharacter", function (data) {
        var newCharacter = new NewCharacter(data);
        var available = []; // available entities, such Race, Stats, etc
        var elementID = [];
        var header = []; // var to populate header for screen
        var currentScreen = 0;
        var maxScreen = 2;
        var i_init = 0; //counter for generating templates
        var fadeSpeed = 300;
        //initialize all templates and populate using data.*
        createCharacterTemplate = templateHelper.startCreation_btns()
        header[i_init] = templateHelper.selectStats().GetHeader
        available[i_init] = templateHelper.selectStats({ Stats: data.Stats }).GetContent
        elementID[i_init] = 'createStats';
        i_init++;
        header[i_init] = templateHelper.selectRace().GetHeader
        available[i_init] = templateHelper.selectRace({ Races: data.Races }).GetContent
        elementID[i_init] = 'createRace';
        i_init++;
        header[i_init] = templateHelper.selectClass().GetHeader
        available[i_init] = templateHelper.selectClass({ Classes: data.Classes }).GetContent
        elementID[i_init] = 'createClass';
        i_init++;
        header[i_init] = templateHelper.createIdentity().GetHeader
        available[i_init] = templateHelper.createIdentity({ Alignments: data.Alignments }, { Genders: data.Genders }).GetContent
        elementID[i_init] = 'createIdentity';
        i_init++;
        header[i_init] = templateHelper.selectSkills().GetHeader
        available[i_init] = templateHelper.selectSkills({ Skills: data.Skills }).GetContent
        elementID[i_init] = 'selectSkills';
        i_init++;
        header[i_init] = templateHelper.selectFeats().GetHeader
        available[i_init] = templateHelper.selectFeats({ Feats: data.Feats }).GetContent
        elementID[i_init] = 'selectFeats';

        maxScreen = i_init; //Set max number of screens to i_init (used above)

        $('#startCreation-Yes').click(function () {
            // fill html with Screen 0
            $('#interactive-inner').fadeOut(fadeSpeed, function () {

                $('#interactive-inner').html(createCharacterTemplate);
                $('#createCharacter-Cancel').click(function () { CancelConfirm() });
                $('#createCharacter-h').html(header[currentScreen]);
                $('#createCharacter-inner').html(available[currentScreen]);
                $('#createCharacter-Complete').hide();
                $('#createCharacter-Back').hide();
                ko.applyBindings(newCharacter, document.getElementById(elementID[currentScreen]));

                // Back Function
                $('#createCharacter-Back').click(function () {
                    if (currentScreen == maxScreen) {
                        //var returnAlignment = $.parseJSON(ko.toJSON(newCharacter.Alignment));
                        //alert(returnAlignment.Name);
                    }
                    if (currentScreen != 0) {
                        currentScreen = currentScreen - 1;
                    }
                    if (currentScreen != maxScreen) {
                        $('#createCharacter-Complete').hide();
                        $('#createCharacter-Next').show();
                    }
                    
                    $('#createCharacter-inner').fadeOut(fadeSpeed, function () {
                        if (currentScreen > 0) {
                            $('#createCharacter-h').html(header[currentScreen]);
                            $('#createCharacter-inner').html(available[currentScreen]);
                        }
                        else if (currentScreen == 0) {
                            $('#createCharacter-h').html(header[currentScreen]);
                            $('#createCharacter-inner').html(available[currentScreen]);
                            $('#createCharacter-Back').fadeOut(fadeSpeed/2);
                        }
                        else {

                        }
                        ko.applyBindings(newCharacter, document.getElementById(elementID[currentScreen]));
                        if (currentScreen == 1) {
                            newCharacter.updateRaceIcon();
                        }
                        else if (currentScreen == 2) {
                            newCharacter.updateClassIcon();
                        }
                        else if (currentScreen == 4) {
                            newCharacter.UpdateSkillCheckbox();
                        }
                        $('#createCharacter-inner').fadeIn(fadeSpeed);
                    });
                });
                // End Back Function

                // Next Function
                $('#createCharacter-Next').click(function () {
                    if (currentScreen != maxScreen) {
                        currentScreen = currentScreen + 1;
                    }
                    if (currentScreen == maxScreen) {
                        $('#createCharacter-Complete').show();
                        $('#createCharacter-Next').hide();
                    }
                    $('#createCharacter-inner').fadeOut(fadeSpeed, function () {
                        if (currentScreen < maxScreen) {
                            $('#createCharacter-h').html(header[currentScreen]);
                            $('#createCharacter-inner').html(available[currentScreen]);
                        }
                        else {
                            $('#createCharacter-h').html(header[currentScreen]);
                            $('#createCharacter-inner').html(available[currentScreen]);
                        }
                        if (currentScreen > 0) {
                            $('#createCharacter-Back').fadeIn(fadeSpeed);
                        }
                        ko.applyBindings(newCharacter, document.getElementById(elementID[currentScreen]));
                        if (currentScreen == 1) {
                            newCharacter.updateRaceIcon();
                        }
                        else if (currentScreen == 2) {
                            newCharacter.updateClassIcon();
                        }
                        else if (currentScreen == 4) {
                            newCharacter.UpdateSkillCheckbox();
                        }
                        $('#createCharacter-inner').fadeIn(fadeSpeed);
                    });
                });
                //End Next Function

                // Complete Function
                $('#createCharacter-Complete').click(function () {
                    alertBox("Done Creating Character?", function () {
                        var i;
                        var createNewCharacter = new CreateNewCharacter();
                        createNewCharacter.Race.Id = parseInt(newCharacter.Race);
                        for (i = 0; i < newCharacter.Stats().length; i++) {
                            createNewCharacter.Stats.push({ Id: newCharacter.Stats()[i].Id, Value: newCharacter.Stats()[i].Value });
                        }
                        for (i = 0; i < newCharacter.Skills().length; i++) {
                            if (newCharacter.Skills()[i].SelectedSkill == 1) {
                                createNewCharacter.Skills.push({ Id: newCharacter.Skills()[i].Id, Value: newCharacter.Skills()[i].Value });
                            }
                        }
                        for (i = 0; i < newCharacter.Feats().length; i++) {
                            if (newCharacter.Feats()[i].Selected == 1) {
                                createNewCharacter.Feats.push({ Id: newCharacter.Feats()[i].Id });
                            }
                        }
                        createNewCharacter.Class.Id = parseInt(newCharacter.Class);
                        createNewCharacter.Alignment.Id = newCharacter.Alignment().Id;
                        createNewCharacter.Gender.Id = newCharacter.Gender().Id;
                        createNewCharacter.Name = newCharacter.Name();
                        createNewCharacter.Age = parseInt(newCharacter.Age());
                        createNewCharacter.Height = newCharacter.Height();
                        createNewCharacter.Weight = newCharacter.Weight();
                        createNewCharacter.Level = parseInt(newCharacter.Level());
                        createNewCharacter.History = newCharacter.History();
                        createNewCharacter.Experience = parseInt(newCharacter.Experience());
                        createNewCharacter.Money = parseInt(gameRules.convertMoneyToSingle(newCharacter.Money()));
                        createNewCharacter.MaxHitPoints = parseInt(newCharacter.HP());
                        createNewCharacter.PlayerTypeID = 1;
                        createNewCharacter.ImgSrc = "";
                        createNewCharacter.UserName = $('#UID').html();

                        $('#interactive-inner').html("");
                        $.ajax({
                            url: 'http://www.antonmorgan.com/rpgsvc/rpgsvc/AddPlayer',
                            type: 'POST',
                            data: JSON.stringify(createNewCharacter),
                            contentType: 'application/json',
                            dataType: 'json'
                        });
                        //$.post("", sendvar, 'json');
                    });
                });
                // End Complete Function

                $('#interactive-inner').fadeIn(fadeSpeed);
            });
        });
    });
}

ManageCharacter = function (data) {
    self = this;
    self.Players = ko.observableArray(data);

    self.ChangeDefaultPlayer = function (mine) {
        //Change css for manageCharacter-SetActive
        //Confirm, Update database and re-load new default player
        //manageCharacters.Mine=mine;
        alertBox("Are you sure you want to set this character as your Default Player?", function (mine) {

            $('#manageCharacter-SetActive-' + ActivePlayerID).css({ boxShadow: 'inset 1px 1px 2px rgba(20,20,20,.9)' });
            $('#manageCharacter-SetActive-' + ActivePlayerID).css({ background: '#888888' });

            ActivePlayerID = mine.Id;
            $('#manageCharacter-SetActive-' + ActivePlayerID).css({ boxShadow: 'inset -1px -1px 2px rgba(20,20,20,.9)' });
            $('#manageCharacter-SetActive-' + ActivePlayerID).css({ background: '#FFFFFF' });

            //Reload html
            LoadPlayer();

            var Update = { ActivePlayer: mine.Id,
                            UserName: $('#UID').html() };

            $.ajax({
                url: 'http://www.antonmorgan.com/rpgsvc/rpgsvc/UpdateDefaultPlayer',
                type: 'POST',
                data: JSON.stringify(Update),
                contentType: 'application/json',
                dataType: 'json'
            });
        },mine);
    }

    self.DeleteCharacter = function (mine) {
        alertBox("Are you sure you want to delete this character?", function (mine) {

            $.ajax({
                url: 'http://www.antonmorgan.com/rpgsvc/rpgsvc/DeleteUserPlayer',
                type: 'POST',
                data: JSON.stringify(mine.Id),
                contentType: 'application/json',
                dataType: 'json'
            });

            $('#manageCharacter-' + mine.Id.toString()).fadeOut(400);

        },mine);
    }
}

ManageCharacterClick = function (templateHelper) {
    //setup the buttons

    $.getJSON(["http://www.antonmorgan.com/rpgsvc/rpgsvc/getuserplayers/" + $('#UID').html()], function (data) {
        var manageCharacter = new ManageCharacter(data);

        var fadeSpeed = 300;
        //var test = manageCharacter.Players()[0].Id;
        var players = data;
        var UserPlayers = templateHelper.ManageCharacter().GetContent;
        var UserPlayers_Header = templateHelper.ManageCharacter().GetHeader;
        var SetActiveID;

        $('#interactive-inner').fadeOut(fadeSpeed, function () {
            $('#interactive-inner').html(templateHelper.manageCharacter_btns);
            $('#Character-h').html(UserPlayers_Header);
            $('#Character-inner').html(UserPlayers);

            $('#Character-Cancel').click(function () { CancelConfirm() });
            ko.applyBindings(manageCharacter, document.getElementById('manageCharacters'));

            SetActiveID = '#manageCharacter-SetActive-' + ActivePlayerID.toString();
            $(SetActiveID).css({ boxShadow: 'inset -1px -1px 2px rgba(20,20,20,.9)' });
            $(SetActiveID).css({ background: '#FFFFFF' });

            $('#interactive-inner').fadeIn(fadeSpeed);

        });

        //$('.Selection').tooltip("close");
        //$('#CreateCharacter-Cancel').click(function () { CancelConfirm() });

    });


}

