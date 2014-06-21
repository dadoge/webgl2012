function TemplateHelper() {

    this.getCharFooterHtml = function(Name,Level){
        var html1 = "<div class=\"character-Name\"> <%= Name %> </div>";
        var html2 = "<div class=\"character-Level\"> Level: <%= Level %> </div>";

        return (_.template(html1, Name) + _.template(html2, Level));
    }

    this.getGeneralHtml = function (Class, Race, Alignment, Age, History) {
        var html1 = "<div class=\"character-Class\"> <div class=\"character-Description\">Class</div> : <%= Class %> </div>";
        var html2 = "<div class=\"character-Race\"> <div class=\"character-Description\">Race</div> : <%= Race %> </div>";
        var html3 = "<div class=\"character-Alignment\"> <div class=\"character-Description\">Alignment</div> : <%= Alignment %> </div>";
        var html4 = "<div class=\"character-Age\"> <div class=\"character-Description\">Age</div> : <%= Age %> </div>";
        var html5 = "<div class=\"character-History\"> <div class=\"character-Description\">History</div> : <%= History %> </div>";

        return (_.template(html1, Class) + _.template(html2, Race) + _.template(html3, Alignment) + _.template(html4, Age) + _.template(html5, History));
    }

    this.getStatsHtml = function(Stats){
        var list = " <div class=\"character-stats-wrapper\"> \
            <% _.each(Stats, function(Stats) { %> \
            <li style=\"list-style-type: none\"> \
                <div class=\"character-stats-name\"> \
                    <%= Stats.Name.substring(0,3) %> \
                </div> \
                <div class=\"character-stats-value\"> \
                    <%= Stats.Value %> \
                </div> \
            </li> \
        <% }); %> \
        </div>";

        return _.template(list, Stats);
    }

    this.getSkillsHtml = function (Skills) {
        var list = " \
            <% _.each(Skills, function(Skills) { %> \
            <li style=\"list-style-type: none\"> \
                <%= Skills.Name %> : <%= Skills.Value %> \
            </li> \
        <% }); %>";

        return _.template(list, Skills);
    }

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
            </select> \
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


}
