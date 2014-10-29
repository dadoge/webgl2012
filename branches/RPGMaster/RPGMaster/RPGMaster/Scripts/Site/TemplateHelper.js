function TemplateHelper() {

    this.getCharFooterHtml = function(Name,Level){
        var html1 = "<div class=\"character-Name\"> <%= Name %> </div>";
        var html2 = "<div class=\"character-Level\"> Level: <%= Level %> </div>";

        return (_.template(html1, Name) + _.template(html2, Level));
    }

    this.getGeneralHtml = function (Class, Race, Alignment, Age, Height, Weight, History) {
        var html1 = "<div class=\"character-Description\">Class<div id=\"character-Class\" class=\"character-DescriptionName\"><%= Class %></div></div>";
        var html2 = "<div class=\"character-Description\">Race<div id=\"character-Race\" class=\"character-DescriptionName\"><%= Race %></div></div>";
        var html3 = "<div class=\"character-Description\">Alignment<div id=\"character-Alignment\" class=\"character-DescriptionName\"><%= Alignment %></div></div>";
        var html4 = "<div class=\"character-Description2\">Age<div id=\"character-Age\" class=\"character-DescriptionName\"><%= Age %></div></div>";
        var html5 = "<div class=\"character-Description2\">Height<div id=\"character-Height\" class=\"character-DescriptionName\"><%= Height %></div></div>";
        var html6 = "<div class=\"character-Description2\">Weight<div id=\"character-Weight\" class=\"character-DescriptionName\"><%= Weight %></div></div>";
        var html7 = "<div id=\"character-History\"> <div class=\"character-Description2\">History</div><%= History %></div>";

        return (_.template(html1, Class) + _.template(html2, Race) + _.template(html3, Alignment) + _.template(html4, Age) + _.template(html5, Height) + _.template(html6, Weight) + _.template(html7, History));
    }

    this.getStatsHtml = function (Stats) {
        Stats.Stats[0].Mod = 0;
        for (var i = 0; i < Stats.Stats.length; i++) {
            Stats.Stats[i].Mod = gameRules.calcModifier(Stats.Stats[i].Value);
        }
        var list = " <div class=\"character-stats-wrapper\"> \
            <% _.each(Stats, function(Stats) { %> \
            <li style=\"list-style-type: none\"> \
                <div class=\"character-stats-name\"> \
                    <%= Stats.Name.substring(0,3) %> \
                </div> \
                <div class=\"character-stats-value\"> \
                    <%= Stats.Value %> \
                </div> \
                <div class=\"character-stats-Text\">Mod</div>\
                <div class=\"character-stats-modifier\"> \
                    <%= Stats.Mod %> \
                </div> \
            </li> \
        <% }); %> \
        </div>";

        return _.template(list, Stats);
    }

    this.getSkillsHtml = function (Skills) {
        var list = " \
            <% _.each(Skills, function(Skills) { %> \
            <li class=\"viewList\" id=\"<%= Skills.Id %>\"> \
                <div class=\"viewList-Name\"><%= Skills.Name %></div><div class=\"viewList-Value\"><%= Skills.Value %></div> \
            </li> \
            <div class=\"viewList-Description\" id=\"character-Skills-Description-<%= Skills.Id %>\"><%= Skills.Description %></div> \
        <% }); %>";

        return _.template(list, Skills);
    }

    this.getFeatsHtml = function (Feats) {
        var list = " \
            <% _.each(Feats, function(Feats) { %> \
            <li class=\"viewList\" id=\"<%= Feats.Id %>\"> \
                <div class=\"viewList-Name\"><%= Feats.Name %></div> \
            </li> \
            <div class=\"viewList-Description\" id=\"character-Feats-Description-<%= Feats.Id %>\"><%= Feats.Description %></div> \
        <% }); %>";

        return _.template(list, Feats);
    }

    this.getInventoryHtml = function (Inventory, Money, Name) {
        var list = " \
            <div class=\"editList\"><span class=\"glyphicon glyphicon-cog edit-glyph\" title=\"Create new items.\"></span><span class=\"glyphicon glyphicon-stats buy-glyph\" title=\"Click to purchase items.\"></span><input class=\"input-searchItems\" / placeholder=\"Items to Buy\"><span id =\"item-search\" class=\"glyphicon glyphicon-search search-glyph\" title=\"Search items to buy.\"></span></div>\
            <div class=\"moneyDiv\"><%= Money[0].pieces %><b>pp</b> <%= Money[1].pieces %><b>gp</b> <%= Money[2].pieces %><b>sp</b> <%= Money[3].pieces %><b>cp</b></div>\
            <div class=\"itemList\" id=\"itemList\"></div> ";
        var list2 = " \
            <div><%= Name %>'s Items </div>";
        var list3 = " \
            <ul class=\"playerInventory\"> \
            </ul> ";

        return _.template(list, { 'Money': Money }) + _.template(list2, Name) + _.template(list3, Inventory);
    }
    this.getPlayerInventoryHtml = function (Inventory) {
        var list = " \
            <% _.each(Inventory, function(Inventory) { %> \
            <li class=\"viewList\" id=\"<%= Inventory.Name.replace(/\\s+/g, '-').replace(/'/g, '_') %>\"> \
                <div class=\"viewList-Name\"><%= Inventory.Name %></div><div class=\"viewList-Value\">x <%= Inventory.ItemQuantity %></div> \
            </li> \
            <div class=\"viewList-Description\" id=\"character-Item-Description-<%= Inventory.Name.replace(/\\s+/g, '-').replace(/'/g, '_') %>\"><b>Description:</b> <%= Inventory.Description %></div> \
        <% }); %>";

        return _.template(list, Inventory);
    }
    this.getItemListHtml = function (Items) {
        var list = "<ul id=\"ul-itemList\" data-bind=\"foreach: Items\"> \
            <li class=\"viewList itemSearchList\" data-bind=\"attr: {id: (Name.replace(/\\s+/g, '-').replace(/'/g, '_'))}\"> \
                <div class=\"viewList-Name itemList-Name\" data-bind=\"text: Name\"></div> <div class=\"itemList-ToBuy\" data-bind=\"attr: {id: ('ToBuy-' + Name.replace(/\\s+/g, '-').replace(/'/g, '_'))}\"></div><input class=\"input-buyItem\" data-bind=\"value: ItemQuantity,attr: {id: ('input-ToBuy-' + Name.replace(/\\s+/g, '-').replace(/'/g, '_'))}\" /> \
            </li> \
            <div class=\"viewList-Cost\" data-bind=\"html: $root.getCostText(Cost,Weight)\"></div> \
            <div class=\"viewList-Description\" data-bind=\"attr: {id: ('ItemList-Description-' + Name.replace(/\\s+/g, '-').replace(/'/g, '_'))},html: ('<b>Description: </b>' + Description)\"></div> \
        </ul> ";

        return _.template(list, Items);
    }
    //Name,Description,Type,Cost,MaxEffect,MinEffect,CriticalEffect,OtherEffect,Range,Weight,OtherType,Path
    this.addItemHtml = function () {
        var list = "\
                <div id=\"createItem\">Create a new Item:<br /><br />\
                <div class=\"inlineWrapper\">Item: <input class=\"input-addItem\" data-bind=\"value: Item().Name\" placeholder=\"Name\"/></div>\
                <div class=\"inlineWrapper\">Cost: <input class=\"input-createMoney\" data-bind=\"value: Item().Cost[1].pieces\" />gp<input class=\"input-createMoney\" data-bind=\"value: Item().Cost[2].pieces\" />sp<input class=\"input-createMoney\" data-bind=\"value: Item().Cost[3].pieces\" />cp</div>\
                <br /><div class=\"inlineWrapper\">Type: <select data-bind=\"options: ItemType, value: Item().Type, optionsValue: 'TypeID', optionsText: 'Name'\" class=\"form-control input-sm input-ItemType\" /></div>\
                <div class=\"inlineWrapper\">Weight: <input class=\"input-createMoney\" data-bind=\"value: Item().Weight\" />lbs.</div>\
                <div class=\"inlineWrapper\">Range: <textarea placeholder=\"i.e.: 0-30yds\" class=\"input-itemOther\" data-bind=\"value: Item().Range\" /></div>\
                <br /><div class=\"inlineWrapper\">Description: <textarea placeholder=\"Item's description\" class=\"input-itemDescription\" data-bind=\"value: Item().Description\" /></div>\
                <br /><div class=\"inlineWrapper\">Max Effect: <textarea placeholder=\"i.e.: +12HP\" class=\"input-itemOther\" data-bind=\"value: Item().MaxEffect\" /></div>\
                <div class=\"inlineWrapper\">Min Effect: <textarea placeholder=\"i.e.: 0HP\" class=\"input-itemOther\" data-bind=\"value: Item().MinEffect\" /></div>\
                <br /><div class=\"inlineWrapper\">Critical Effect: <textarea placeholder=\"i.e.: x2\" class=\"input-itemOther\" data-bind=\"value: Item().CriticalEffect\" /></div>\
                <div class=\"inlineWrapper\">Other Effect: <textarea placeholder=\"i.e.: Allows wearer to Polymorph\" class=\"input-itemOther2\" data-bind=\"value: Item().OtherEffect\" /></div>\
                <br /><div id=\"alertError\" class=\"inlineWrapper\"></div>\
                </div>";

        return list;
    }


    //Used for Character Creation
    this.startCreation = function () {
        var htmlTemplate = " \
            <div class=\"interactive-inner\"> \
                <div class=\"text-shadow\"><h5>Create New Character?</h5> </div> \
                <div class=\"btn-group\"> \
                    <button id=\"startCreation-Yes\" type=\"button\" class=\"btn btn-default btn-width btn-shadow\">Yes</button> \
                </div> \
                <div class=\"btn-group\"> \
                    <button id=\"CreateCharacter-Cancel\" type=\"button\" class=\"btn btn-default btn-width btn-shadow\">Cancel</button> \
                </div> \
            </div> ";
            
        return htmlTemplate;
    }

    this.startCreation_btns = function () {
        var htmlTemplate = " <div id=\"createCharacter-h\" class=\"text-shadow\"></div> \
            <div class=\"createCharacter-btns\"> \
                <div class=\"btn-group\"> \
                    <button id=\"createCharacter-Back\" type=\"button\" class=\"btn btn-default btn-width btn-shadow\">Back</button> \
                </div> \
                <div class=\"btn-group\"> \
                    <button id=\"createCharacter-Complete\" type=\"button\" class=\"btn btn-default btn-width btn-shadow\">Done</button> \
                </div> \
                <div class=\"btn-group\"> \
                    <button id=\"createCharacter-Next\" type=\"button\" class=\"btn btn-default btn-width btn-shadow\">Next</button> \
                </div> \
                <div class=\"btn-group\"> \
                    <button id=\"createCharacter-Cancel\" type=\"button\" class=\"btn btn-default btn-width btn-shadow\">Cancel</button> \
                </div> \
            </div> \
            <div id=\"createCharacter-inner\"> \
            </div> ";

        return htmlTemplate;
    }

    this.selectRace = function (Races) {
        var returnObj = new Object();
        var GetHeader = function () {
            return "<h3>Select Race</h3>";
        }

        var GetContent = function (Races) {
            var htmlTemplate = "<div id=\"createRace\">\
                <% _.each(Races, function(Races) { %> \
                    <div class=\"RaceSelector\" title=\"<%= Races.Description %>\"> \
                        <img data-bind=\"click: SelectRace\" id=\"<%= Races.Id %>\" class=\"RaceImg\" src=\"../Images/<%= Races.ImgSrc %>\" />\
                        <%= Races.Name %> \
                    </div> \
                <% }); %> \
            </div> ";

            return _.template(htmlTemplate, Races);
        }
        returnObj.GetContent = GetContent(Races);
        returnObj.GetHeader = GetHeader();

        return returnObj;
    }

    this.selectClass = function (Classes) {
        var returnObj = new Object();
        var GetHeader = function () {
            return "<h3>Select Class</h3>";
        }

        var GetContent = function (Classes) {
            var htmlTemplate = "<div id=\"createClass\">\
                <% _.each(Classes, function(Classes) { %> \
                    <div class=\"RaceSelector\" title=\"<%= Classes.Description %>\"> \
                        <img data-bind=\"click: SelectClass\" id=\"<%= Classes.Id %>\" class=\"RaceImg\" src=\"../Images/<%= Classes.ImgSrc %>\" />\
                        <%= Classes.Name %> \
                    </div> \
                <% }); %> \
            </div> ";

            return _.template(htmlTemplate, Classes);
        }
        returnObj.GetContent = GetContent(Classes);
        returnObj.GetHeader = GetHeader();

        return returnObj;
    }

    this.selectSkills = function (Skills) {
        var returnObj = new Object();
        var GetHeader = function () {
            return "<h3>Select Skills</h3>";
        }

        var GetContent = function (Skills) {
            var htmlTemplate = "<div id=\"selectSkills\">\
                <ul data-bind=\"foreach: Skills\"> \
                    <li> \
                        <div class=\"checkbox-div\" data-bind=\"click: $parent.ChangeSelection,attr: {id: ('SkillCheckbox-' + Id)}\"> </div>\
                        <input class=\"input-skillsValue\" data-bind=\"value: Value,visible: SelectedSkill,attr: {id: ('SkillValue-' + Id)}\" /> \
                        <div class=\"selectSkills-Name\" data-bind=\"text: Name,attr: {id: ('SkillName-' + Id)}\"> </div> \
                    </li> \
                </ul> \
            </div> ";

            return _.template(htmlTemplate, Skills);
        }
        returnObj.GetContent = GetContent(Skills);
        returnObj.GetHeader = GetHeader();

        return returnObj;
    }
    this.selectFeats = function (Feats) {
        var returnObj = new Object();
        var GetHeader = function () {
            return "<h3>Select Feats</h3>";
        }

        var GetContent = function (Feats) {
            var htmlTemplate = "<div id=\"selectFeats\">\
                <ul data-bind=\"foreach: Feats\"> \
                    <li> \
                        <input id=\"selectedFeats-input\" type=\"checkbox\" data-bind=\"checked: Selected\" /> \
                        <div id=\"selectFeats-Name\" data-bind=\"text: Name\"> </div> \
                        <div data-bind=\"attr: {title: Description}\" class=\"info-Description\"><span class=\"glyphicon glyphicon-info-sign\"></span></div> \
                    </li> \
                </ul> \
            </div> ";

            return _.template(htmlTemplate, Feats);
        }
        returnObj.GetContent = GetContent(Feats);
        returnObj.GetHeader = GetHeader();

        return returnObj;
    }

    this.selectStats = function (Stats) {
        var returnObj = new Object();
        var GetHeader = function () {
            return "<h3>Select Stats</h3>";
        }

        var GetContent = function (Stats) {
            var htmlTemplate = "<div id=\"createStats\"> \
                <div data-bind=\"foreach: Stats\"> \
                    <div class=\"div-createStats\"> \
                        <h5 class=\"h-createStats\" data-bind=\"text: Name, attr: {title: Description}\"></h5><input class=\"input-createStats\" data-bind=\"value: Value, attr: {title: Description}\" /> \
                    </div> \
                </div> \
            </div> ";

            return _.template(htmlTemplate, Stats);
        }
        returnObj.GetContent = GetContent(Stats);
        returnObj.GetHeader = GetHeader();

        return returnObj;
    }

    this.createIdentity = function (Alignments, Genders) {
        var returnObj = new Object();
        var GetHeader = function () {
            return "<h3>Define Character</h3>";
        }

        var GetContent = function (Alignments, Genders) {
            var htmlTemplate = "<div id=\"createIdentity\"> \
            <div id=\"createIdentity-title\" >Name </div><input class=\"input-createName\" data-bind=\"value: Name\" /> <br /> \
            <div id=\"createIdentity-title\" >Alignment </div><select data-bind=\"options: availableAlignments, value: Alignment, optionsText: 'Name'\" id=\"Alignment-options\" class=\"form-control input-sm  info-Description\"> \
            </select> \
            <div data-bind=\"attr: {title: Alignment().Description}\" class=\"info-Description\"><span class=\"glyphicon glyphicon-info-sign\"></span></div>";
            var htmlTemplate2 = "<br /><div id=\"createIdentity-title\" >Age </div><input class=\"input-createAge\" data-bind=\"value: Age\" /> Height  <input class=\"input-createAge\" data-bind=\"value: Height\" /> Weight  <input class=\"input-createAge\" data-bind=\"value: Weight\" />\
            <br /><div id=\"createIdentity-title\" >Gender </div><select data-bind=\"options: availableGenders, value: Gender, optionsText: 'Name'\" id=\"Gender-options\" class=\"form-control input-sm  info-Description\"> \
            </select> <br />\
            <div id=\"createIdentity-title\" >Level </div><input class=\"input-createAge\" data-bind=\"value: Level\" /> Experience  <input class=\"input-createAge\" data-bind=\"value: Experience\" /> \
            <br /><div id=\"createIdentity-title\" >Money </div><input class=\"input-createMoneyPlat\" data-bind=\"value: Money()[0].pieces\" />pp<input class=\"input-createMoney\" data-bind=\"value: Money()[1].pieces\" />gp<input class=\"input-createMoney\" data-bind=\"value: Money()[2].pieces\" />sp<input class=\"input-createMoney\" data-bind=\"value: Money()[3].pieces\" />cp\
            <br /><div id=\"createIdentity-title\" >History </div><br /><textarea class=\"input-createHistory\" data-bind=\"value: History\" /> \
            </div>";

            
            return (_.template(htmlTemplate, Alignments) + _.template(htmlTemplate2, Genders));
        }
        
        var GetNameField = function () {
            return "<div class=\"input-group createCharacter-input-group\"> \
						Character Name <input id=\"createCharacter-Name-input\" type=\"text\" class=\"form-control\" placeholder=\"Name\"> \
					</div> ";
        }
        
        returnObj.GetContent = GetContent(Alignments, Genders);
        returnObj.GetHeader = GetHeader();
        returnObj.GetNameFiled = GetNameField();

        return returnObj;
    }
    //End Character Creation section

    //Manage Character section
    this.manageCharacter_btns = function () {
        var htmlTemplate = " <div id=\"Character-h\" class=\"text-shadow\"></div> \
            <div class=\"createCharacter-btns\"> \
                <div class=\"btn-group\"> \
                    <button id=\"Character-Cancel\" type=\"button\" class=\"btn btn-default btn-width btn-shadow\">Cancel</button> \
                </div> \
            </div> \
            <div id=\"Character-inner\"> \
            </div> ";

        return htmlTemplate;
    }

    this.ManageCharacter = function () {
        var returnObj = new Object();
        var GetHeader = function () {
            return "<h3>Manage Characters</h3>";
        }

        var GetContent = function () {
            var htmlTemplate = "<div data-bind=\"foreach: Players\" id=\"manageCharacters\">\
                    <div class=\"manageCharacter-Main\" data-bind=\"attr: {id: ('manageCharacter-' + Id)}\"> \
                        <div class=\"manageCharacter-Img\" data-bind=\"attr: {id: ('manageCharacter-Img-' + Id)},style: { 'background-image': 'url(../Images/' + ImgSrc + ')' }\" style=\"background-size: auto 100%\" > </div> \
                        <div class=\"manageCharacter-Name\" data-bind=\"text: Name\"></div> \
                        <div class=\"manageCharacter-Edit\" ><div class=\"manageCharacter-SetActive\" data-bind=\"click: $parent.ChangeDefaultPlayer, attr: {id: ('manageCharacter-SetActive-' + Id)}\"></div><span data-bind=\"click: $parent.DeleteCharacter\" class=\"glyphicon glyphicon-trash manageCharacter-Delete-glyph\"></span><span class=\"glyphicon glyphicon-pencil manageCharacter-Edit-glyph\"></span></div> \
                    </div> \
            </div> ";

            return htmlTemplate; // _.template(htmlTemplate, Players);
        }

        returnObj.GetContent = GetContent();//Players);
        returnObj.GetHeader = GetHeader();

        return returnObj;
    }
    //End Manage Character section

    this.alertBoxDefault = function () {
        var htmlTemplate = "<div class=\"btn-group\"> \
                    <button id=\"alertBox-Ok\" type=\"button\" class=\"btn btn-default btn-width btn-shadow\">Ok</button> \
                </div> \
                <div class=\"btn-group\"> \
                    <button id=\"alertBox-Cancel\" type=\"button\" class=\"btn btn-default btn-width btn-shadow\">Cancel</button> \
                </div>";

        return htmlTemplate;
    }

    //Map Editor
    //Manage Character section
    this.mapEditor_btns = function () {
        var htmlTemplate = " <div id=\"Character-h\" class=\"text-shadow\"></div> \
            <div class=\"createCharacter-btns\"> \
                <div class=\"btn-group\"> \
                    <button id=\"mapEditor-Save\" type=\"button\" class=\"btn btn-default btn-width btn-shadow\">Save</button> \
                </div> \
                <div class=\"btn-group\"> \
                    <button id=\"mapEditor-Load\" type=\"button\" class=\"btn btn-default btn-width btn-shadow\">Load</button> \
                </div> \
                <div class=\"btn-group\"> \
                    <button id=\"mapEditor-Cancel\" type=\"button\" class=\"btn btn-default btn-width btn-shadow\">Cancel</button> \
                </div> \
            </div> \
            <div id=\"Character-inner\"> \
            </div> ";

        return htmlTemplate;
    }
    //End Map Editor section

}
