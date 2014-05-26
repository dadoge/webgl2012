function TemplateHelper() {

    this.getStatsHtml = function(Stats){
        var list = " \
            <% _.each(Stats, function(Stats) { %> \
            <li style=\"list-style-type: none\"> \
                <%= Stats.Name.substring(0,3) %> : <%= Stats.Value %> \
            </li> \
        <% }); %>";

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
            var htmlTemplate = "<div id=\"Race_div\">\
                <% _.each(Races, function(Races) { %> \
                    <div id=\"<%= Races.Id %>\" class=\"RaceSelector\" title=\"<%= Races.Description %>\"> \
                        <img id=\"RaceImg\" src=\"/Images/<%= Races.ImgSrc %>\" />\
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


    this.selectStats = function (Stats) {
        var returnObj = new Object();
        var GetHeader = function () {
            return "<h3>Select Stats</h3>";
        }

        var GetContent = function (Stats) {
            var htmlTemplate = "<% _.each(Stats, function(Stats) { %> \
                <div id=\"<%= Stats.Id %>\" class=\"StatsSelector\" title=\"<%= Stats.Description %>\"> \
                    <%= Stats.Name %> \
                </div> \
            <% }); %>";

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
            //var htmlTemplate = "Alignment <select data-bind=\"value: Alignment\" id=\"Alignment-options\" class=\"form-control input-sm\"> \
            //        <% _.each(Alignments, function(Alignments) { %> \
            //                <option value=\"<%= Alignments.Id %>\" id=\"<%= Alignments.Id %>\" title=\"<%= Alignments.Description %>\"><%= Alignments.Name %> </option> \
            //            </div> \
            //        <% }); %> \
            //</select>";
            var htmlTemplate = "<div id=\"createIdentity\">Alignment <select data-bind=\"options: availableAlignments, value: Alignment, optionsText: 'Name'\" id=\"Alignment-options\" class=\"form-control input-sm\"> \
            </select>";
            var htmlTemplate2 = "Gender <select data-bind=\"options: availableGenders, value: Gender, optionsText: 'Name'\" id=\"Gender-options\" class=\"form-control input-sm\"> \
            </select> \
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
