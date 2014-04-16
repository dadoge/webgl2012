$("#menu-tab").click(function () {
    if (document.getElementById('menu-container').style.borderStyle == "solid") {
        document.getElementById('menu-container').style.borderStyle = "none";
        $("#menu-container").animate({ "left": "-=160px" }, "slow");
    }
    else {
        document.getElementById('menu-container').style.borderStyle = "solid";
        $("#menu-container").animate({ "left": "+=160px" }, "slow");
        document.getElementById('#menu-input').focus();
    }
});