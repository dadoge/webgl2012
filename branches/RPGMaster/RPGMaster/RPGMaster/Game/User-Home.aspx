<%@ Page Title="Player Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User-Home.aspx.cs" Inherits="RPGMaster.Game.User_Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
		<div class="container container-back container-custom">
			<div class="row row-custom">
				<div class="col-xs-3 party-info">
				<div id="party-info" style="position:relative;height:inherit">

				</div>
				</div>
				<!--Interactive Play Screen-->
				<div class="col-xs-6 col-custom">

					<div class="chat" style="font-family: Consolas">
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
	
    <script type="text/javascript" src="/Scripts/Site/getplayer.js"> </script>
	<script type="text/javascript" src="/Scripts/Site/stomp.js"></script>
	<script type="text/javascript" src="/Scripts/Site/moment-min-js.js"></script>
	<script type="text/javascript" src="/Scripts/Site/sockjs-0.3.min.js"></script>
	<script type="text/javascript" src="/Scripts/Site/chat.js"></script>
</asp:Content>
