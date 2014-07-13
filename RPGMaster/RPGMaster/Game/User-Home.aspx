<%@ Page Title="Player Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User-Home.aspx.cs" Inherits="RPGMaster.Game.User_Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
		<div class="container container-back container-custom">
			<div class="row row-custom">
				<div class="col-xs-3 left-info">
                <div class="background-overlay-2">

                </div>
				<div class="player-options text-center">
                    <div id="Create-New-Character" class="Selection">Create New Character<br /></div>
                    -------<br />
                    <div id="Add-Existing-Character" class="Selection">Add Existing Character<br /></div>
                    -------<br />
                    <div id="Manage-Characters" class="Selection">Manage Characters</div>
                    -------<br />
                    <div id="Map-Editor" class="Selection">Map Editor</div>
				</div>
				</div>
				<!--Interactive Play Screen-->
				<div class="col-xs-6 col-custom">
                    <div class="interactive">
                        <div class="background-overlay-3"></div>
                        <div id="interactive-inner"></div>
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
                        
			            <div class="background-overlay-2">
                        </div>
                        <div class="character-hide">

                        </div>
						<div id="character-icon" class="character-icon">
                            <div class="character-img-wrapper">
                                
							<!--http://gkb3rk.deviantart.com/art/Druid-Master-New-326914153-->
							<%--<img "character-img" src="/Images/druid_master_new_by_brucemashbatart-d5emwkp_edited.jpg" />--%>
                            </div>
                            <div class="character-icon-footer">

                            </div>
						</div>
						<div id="character-info" class="character-info">
							<select id="options-character-info" class="form-control form-control-character input-sm">
								<option id="option-General">General</option>
                                <option id="option-Stats">Ability Scores</option>
								<option id="option-Skills">Skills</option>
								<option id="option-Feats">Feats + Racial/Class Traits</option>
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
	
    <script type="text/javascript" src="../Scripts/jquery-ui-1.10.4.custom.js"> </script>
    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.slimscroll.js"> </script>
	<script type="text/javascript" src="../Scripts/Site/stomp.js"></script>
	<script type="text/javascript" src="../Scripts/Site/moment-min-js.js"></script>
	<script type="text/javascript" src="../Scripts/Site/sockjs-0.3.min.js"></script>
	<script type="text/javascript" src="../Scripts/babylon.1.12.js"></script>
	<script type="text/javascript" src="../Scripts/cannon.js"></script>
	<script type="text/javascript" src="../Scripts/hand-1.3.8.js"></script>

    <script type="text/javascript" src="../Scripts/Site/Create_Character.js"> </script>
    <script type="text/javascript" src="../Scripts/Site/TemplateHelper.js"> </script>
    <script type="text/javascript" src="../Scripts/Site/BabylonScene.js"> </script>
    <script type="text/javascript" src="../Scripts/Site/BabylonMechanics.js"> </script>
	<script type="text/javascript" src="../Scripts/Site/site.js"></script>
</asp:Content>
