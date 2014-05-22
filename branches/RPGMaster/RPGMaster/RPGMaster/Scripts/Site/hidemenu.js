$("#menu-tab").click(function () {
    if (document.getElementById('menu-container').style.borderStyle == "solid") {
        document.getElementById('menu-container').style.borderStyle = "none";
        $("#menu-container").animate({ "left": "-=160px" }, "slow");
    }
    else {
        document.getElementById('menu-container').style.borderStyle = "solid";
        $("#menu-container").animate({ "left": "+=160px" }, "slow");
        //document.getElementById('#menu-input').focus();
    }
});

$("#character-hide").click(function () {
    if (document.getElementById('character-display').style.borderStyle == "solid") {
        document.getElementById('character-display').style.borderStyle = "none";
        $("#character-display").animate({ width: "toggle" }, 1000);
        //$("#character-display").animate({ "left": "+100px" }, "slow").removeClass('visible');
      //  $("#character-display").hide({ direction: "right" }, "slow");
    }
    else {
        document.getElementById('character-display').style.borderStyle = "solid";
        $("#character-display").animate({ width: "toggle" }, 1000);
        //$("#character-display").animate({"left":"0px"}, "slow").addClass('visible');
        //document.getElementById('#menu-input').focus();
    }
});