<%@ Page Title="Game" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RPGMaster.Game.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
		<div class="container container-back container-custom">
			<div class="row row-custom">
				<div class="col-xs-3 party-info">
				<div id="party-info" style="position:relative;height:inherit">
					<div id="partyMember" class="partyMember">
						<img class="partyMember-icon" src="/Images/druid_master_new_by_brucemashbatart-d5emwkp_edited_small.jpg" />
						<div class="partyMember-info">
							Iargalon<br/>Druid Level 7<br/>
							<div class="playerHealth">28 HP</div>
							<div class="playerOther"></div>
						</div>
					</div>
					<div style="height:10px"></div>
					<div id="partyMember2" class="partyMember">
					<!--http://cdn.obsidianportal.com/assets/101933/Milil.jpg-->
						<img class="partyMember-icon" src="/Images/Xana_edited.jpg" />
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
                            <div class="character-img-wrapper">
							    <img class="character-img" src="/Images/druid_master_new_by_brucemashbatart-d5emwkp_edited.jpg" />
                            </div>
						</div>
						<div id="character-info" class="character-info">
							<select id="options-character-info" class="form-control form-control-character input-sm">
								<option id="option-Stats">Stats</option>
								<option id="option-Skills">Skills</option>
								<option id="option-History">History</option>
							</select>
							<div class="character-tab">
							<div id="character-text">
							
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
	<script type="text/javascript" src="/Scripts/Site/template_character.js" id="template_character"></script>
    <script type="text/javascript" src="/Scripts/Site/getplayer.js"> </script>
	<script type="text/javascript" src="/Scripts/Site/stomp.js"></script>
	<script type="text/javascript" src="/Scripts/Site/moment-min-js.js"></script>
	<script type="text/javascript" src="/Scripts/Site/sockjs-0.3.min.js"></script>
	<script type="text/javascript" src="/Scripts/Site/site.js"></script>
</asp:Content>
