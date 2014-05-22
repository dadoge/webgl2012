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
                <div class=\"text-shadow\">Create New Character? </div> \
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
                <div id=\"createCharacter-Back class=\"btn-group\"> \
                </div> \
                <div class=\"btn-group\"> \
                    <button id=\"CreateCharacter-Next\" type=\"button\" class=\"btn btn-default btn-width btn-shadow\">Next</button> \
                </div> \
                <div class=\"btn-group\"> \
                    <button id=\"CreateCharacter-Cancel\" type=\"button\" class=\"btn btn-default btn-width btn-shadow\">Cancel</button> \
                </div> \
            </div> \
            <div id=\"createCharacter-inner\"> \
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
            var htmlTemplate = "<% _.each(Races, function(Races) { %> \
                <div id=\"<%= Races.Id %>\" class=\"RaceSelector\" title=\"<%= Races.Description %>\"> \
                    <%= Races.Name %> \
                </div> \
            <% }); %>";

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
        var temp = "<div class=\"text-shadow\">Select Stats </div> \
            <div class=\"btn-group\"> \
                <button id=\"CreateCharacter-Next\" type=\"button\" class=\"btn btn-default btn-width btn-shadow\">Next</button> \
            </div> \
            <div class=\"btn-group\"> \
                <button id=\"CreateCharacter-Cancel\" type=\"button\" class=\"btn btn-default btn-width btn-shadow\">Cancel</button> \
            </div> \
        <% _.each(Stats, function(Stats) { %> \
            <div id=\"<%= Stats.Id %>\" class=\"StatsSelector\" title=\"<%= Stats.Description %>\"> \
                <%= Stats.Name %> \
            </div> \
        <% }); %>";

        return _.template(temp, Stats);
    }


}
