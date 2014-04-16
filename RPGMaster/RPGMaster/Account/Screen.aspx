<%@ Page Title="Game Screen" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Screen.aspx.cs" Inherits="RPGMaster.Account.Screen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
		<div class="container container-custom">
			<div class="row row-custom">
				<div class="col-xs-3 party-info">
				<div id="party-info" style="position:relative;height:inherit">
					<div id="partyMember" class="partyMember">
						<img class="partyMember-icon" src="/Images/druid_master_new_by_brucemashbatart-d5emwkp_edited_small.jpg"></img>
						<div class="partyMember-info">
							Iargalon<br/>Druid Level 7<br/>
							<div class="playerHealth">28 HP</div>
							<div class="playerOther"></div>
						</div>
					</div>
					<div style="height:10px"></div>
					<div id="partyMember2" class="partyMember">
					<!--http://cdn.obsidianportal.com/assets/101933/Milil.jpg-->
						<img class="partyMember-icon" src="/Images/Xana_edited.jpg"></img>
						<div class="partyMember-info">
							Xana<br/>Bard Level 8<br/>
							<div class="playerHealth">32 HP</div>
							<div class="playerOther"></div>
						</div>
					</div>
				</div>
				</div>
				<!--Interactive Play Screen-->
				<div class="col-xs-6 col-custom">
					<div class="interactive">
						Interactive Table
						<!--Added table later. Need to research 2D or 3D-->
					</div>
					<div class="chat">
						<div id="chat">
							CHAT
						</div>
					</div>
					<div class="input-group chat-input-group">
						<span class="input-group-addon input-chat">You: </span>
						<input id="chat-input" type="text" class="form-control input-chat" placeholder="chat" >
					</div>
					<div class="input-group chatname-input-group">
						<input id="chatname-input" type="text" class="form-control input-chat" placeholder="Type Username">
					</div>
				</div>
				<!--/span-->
				<div id="character-display" class="col-xs-3 character-display">
					
						<div id="character-icon" class="character-icon">
							<!--http://gkb3rk.deviantart.com/art/Druid-Master-New-326914153-->
							<img class="character-img" src="/Images/druid_master_new_by_brucemashbatart-d5emwkp_edited.jpg"></img>
						</div>
						<div id="character-info" class="character-info">
							<select class="form-control form-control-character input-sm">
								<option>Stats</option>
								<option>Skills</option>
								<option>History</option>
							</select>
							<div class="character-tab">
							<div id="character-stats">
							
							</div>
							<div id="character-skills">
							
							</div>
							<div id="character-history">
							
							</div>
							</div>
						</div>
					
					<div id="character-hide" class="character-hide">
						<div class="character-hide-circle"></div>
						<div class="character-hide-circle"></div>
						<div class="character-hide-circle"></div>
					</div>
				</div>
				<!--/span-->
			</div>
			<!--/row-->
		</div>
	
	<div id="menu-container" class="menu-container">
		<div id="menu-options" class="menu-options">
		Player Settings</br></br>
		Party Settings</br></br>
		Options		
		</div>
		<div id="menu-tab" class="menu-tab">
			M</br>
			E</br>
			N</br>
			U</br>
		</div>
	</div>

	<script>
	    $("#menu-container").animate({ "left": "-=160px" }, 0);
	    document.getElementById('menu-container').style.borderStyle = "none";
	    //$( "#menu-tab" ).toggle( "slide");
	</script>
	
	<script type="text/javascript" src="/Scripts/Site/hidemenu.js"></script>
	
    <script type="text/javascript" src="/Scripts/Site/getplayer.js"> </script>
	<script type="text/javascript" src="/Scripts/Site/stomp.js"></script>
	<script type="text/javascript" src="/Scripts/Site/moment-min-js.js"></script>
	<script type="text/javascript" src="/Scripts/Site/sockjs-0.3.min.js"></script>
	<script type="text/javascript" src="/Scripts/Site/chat.js"></script>
</asp:Content>
