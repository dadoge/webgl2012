$("#footer").click(function () {
	$.getJSON("http://www.antonmorgan.com/dndsvc/getplayer/18", function (data) {
		var change = "Name: " + data.Name + "<br/>Health: " + data.Health + "<br/>INT: " + data.Intelligence;
		document.getElementById('character-info').innerHTML=change;
		//document.getElementById('character-info').innerHTML="Test";
		//$('#character-info').append(data.Name);
	});
});