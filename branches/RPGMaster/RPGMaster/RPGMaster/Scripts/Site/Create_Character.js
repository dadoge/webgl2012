NewCharacter = function () {

    var Race;
    var Stats;
    var Alignment;
    var Gender;
    var Name;
    var Age;
    var Level;
    var History;

}

CancelConfirm = function () {
    Canceled = confirm("Are you sure you want to cancel?")
    if (Canceled == true) {
        $('#interactive-inner').html("");
    }
}