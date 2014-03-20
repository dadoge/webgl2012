	function HideMenu(){			
		if (document.getElementById('menu-options').style.visibility=="visible"){
			document.getElementById('menu-options').style.visibility="hidden";
		}
		else {
			document.getElementById('menu-options').style.visibility="visible";
		}
		if (document.getElementById('menu-container').style.width=="200px") {
			document.getElementById('menu-container').style.width="40px";
		}
		else {
			document.getElementById('menu-container').style.width="200px";
		}
	}
	function HideCharacterMenu(){
	
	}