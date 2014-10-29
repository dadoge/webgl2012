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
        $('#options-character-info').show();
        $('.character-tab').show();
        $('.character-info').css({ padding: "5px 5px 0px 20px" });
        $("#character-display").animate({ width: '25%' }, 800, function () {
            $('.character-icon-footer').fadeIn(200);
        });
        //$("#character-display").animate({ "left": "+100px" }, "slow").removeClass('visible');
      //  $("#character-display").hide({ direction: "right" }, "slow");
    }
    else {
        document.getElementById('character-display').style.borderStyle = "solid";
        $('.character-icon-footer').fadeOut(400);
        $('#options-character-info').fadeOut(400, function () {
            $('.character-info').css({ padding: "5px 5px 0px 5px" });
        });
        $('.character-tab').fadeOut(400);
        $("#character-display").animate({ width: "25px",right:0 }, 800);
        //$("#character-display").animate({"left":"0px"}, "slow").addClass('visible');
        //document.getElementById('#menu-input').focus();
    }
});