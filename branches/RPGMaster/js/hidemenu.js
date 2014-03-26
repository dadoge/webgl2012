$( "#menu-tab").click(function() {
	if (document.getElementById('menu-container').style.borderStyle=="solid") {
		document.getElementById('menu-container').style.borderStyle="none";
		$( "#menu-container" ).animate( {"left":"-=160px"}, "slow" );
	}
	else {
		document.getElementById('menu-container').style.borderStyle="solid";
		$( "#menu-container" ).animate( {"left":"+=160px"}, "slow" );
	}
});
/* $( "#character-hide").click(function() {
	// Set the effect type
    var effect = 'slide';
 
    // Set the options for the effect type chosen
    var options = { direction: 'right' };
 
    // Set the duration (default: 400 milliseconds)
    var duration = 700;
 
    $("#character-all").toggle(effect, options, duration);
}); */