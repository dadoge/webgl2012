NewCharacter = function (data) {//(race, stats, alignment, gender, name, age, level, history) {
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
    self.Money = ko.observable("0pp 0gp 0sp 0cp");

    //self.SelectedSkillYo = ko.observableArray([]);

    //for (var i; i < data.Skills.length ; i++) {
    //    self.SelectedSkillYo()[i] = data.Skills[i].SelectedSkills;
    //}

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
            $(SkillCheckboxID).css({ background: '#EFEFEF' });
            $(SkillNameID).css({ margin: '0em 0em 0em 0em' });
        }
        else {
            $(SkillCheckboxID).css({ boxShadow: 'inset 1px 1px 2px rgba(20,20,20,.9)' });
            $(SkillCheckboxID).css({ background: '#AAAAAA' });
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
                $(SkillCheckboxID).css({ background: '#EFEFEF' });
                $(SkillNameID).css({ margin: '0em 0em 0em 0em' });
            }
            else {
                $(SkillCheckboxID).css({ boxShadow: 'inset 1px 1px 2px rgba(20,20,20,.9)' });
                $(SkillCheckboxID).css({ background: '#AAAAAA' });
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

var createCharacterViewModel = {
    availableAlignments: ko.observableArray()
};

CancelConfirm = function () {
    Canceled = confirm("Are you sure you want to cancel?")
    if (Canceled == true) {
        $('#interactive-inner').html("");
    }
}

SelectRace = function (PreviousSelection) {
    PreviousSelection.style.backgroundColor=(rgba(255, 255, 255, .75));
    this.style.backgroundColor = rgba(255, 255, 255, 1);
    return this;
}

