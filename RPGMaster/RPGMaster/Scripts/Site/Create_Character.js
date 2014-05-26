NewCharacter = function (data) {//(race, stats, alignment, gender, name, age, level, history) {
    this.Race=1;
    this.Stats=0;
    this.Alignment = ko.observable(data.Alignments[3]);    
    this.Gender = ko.observable(data.Genders[0]);
    this.Name="";
    this.Age=20;
    this.Level=1;
    this.History = "";

    //var returnAlignment = $.parseJSON(ko.toJSON(newCharacter.Alignment));
    this.availableAlignments = ko.observableArray(data.Alignments);
    this.availableGenders = ko.observableArray(data.Genders);
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

