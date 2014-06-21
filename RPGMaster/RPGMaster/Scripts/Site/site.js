$(document).ready(function () {
    var templateHelper = new TemplateHelper();
    var ChatName = "";
    var ActivePlayerID;
    $.getJSON(["http://www.antonmorgan.com/rpgsvc/rpgsvc/getuserinfo/" + $('#UID').html()], function (data) {
        
        ChatName = data.ChatName;
        $('#chatname-input').val(ChatName);
        ActivePlayerID = data.ActivePlayer;

        $.getJSON(["http://www.antonmorgan.com/rpgsvc/rpgsvc/getplayer/" + ActivePlayerID], function (data) {
        
            var Stats_template = templateHelper.getStatsHtml({ Stats: data.Stats });
            var Skills_template = templateHelper.getSkillsHtml({ Skills: data.Skills });
            var General_template = templateHelper.getGeneralHtml({ Class: data.Class.Name }, { Race: data.Race.Name }, { Alignment: data.Alignment.Name }, { Age: data.Age }, { History: data.History });
            
            $('.character-img-wrapper').css("background-image", ["url(../Images/" + data.ImgSrc + ")"]);
            $('#character-text').html(General_template);
             $('.character-icon-footer').html(templateHelper.getCharFooterHtml({ Name: data.Name }, { Level: data.Level }))

             $("#options-character-info").change(function () {
                 if (document.getElementById('options-character-info').selectedIndex == 0) {
                     $('#character-text').html(General_template);
                 }
                 else if (document.getElementById('options-character-info').selectedIndex == 1) {
                     $('#character-text').html(Stats_template);
                 }
                 else if (document.getElementById('options-character-info').selectedIndex == 2) {
                     $('#character-text').html(Skills_template);
                 }

             });
        });
    });

     $('#Create-New-Character').click(function () {
        //setup the buttons
        $('#interactive-inner').html(templateHelper.startCreation());
        //$('.Selection').tooltip("close");
        $('#CreateCharacter-Cancel').click(function () { CancelConfirm() });
        $.getJSON("http://www.antonmorgan.com/rpgsvc/rpgsvc/createnewcharacter", function (data) {
            var newCharacter = new NewCharacter(data);
            var available = []; // available entities, such Race, Stats, etc
            var elementID = [];
            var header = []; // var to populate header for screen
            var currentScreen = 0;
            var maxScreen = 2;
            var i_init = 0; //counter for generating templates
            var fadeSpeed = 300;
            //initialize all templates and populate using data.*
            createCharacterTemplate = templateHelper.startCreation_btns()
            header[i_init] = templateHelper.selectStats().GetHeader
            available[i_init] = templateHelper.selectStats({ Stats: data.Stats }).GetContent
            elementID[i_init] = 'createStats';
            i_init++;
            header[i_init] = templateHelper.selectRace().GetHeader
            available[i_init] = templateHelper.selectRace({ Races: data.Races }).GetContent
            elementID[i_init] = 'createRace';
            i_init++;
            header[i_init] = templateHelper.selectClass().GetHeader
            available[i_init] = templateHelper.selectClass({ Classes: data.Classes }).GetContent
            elementID[i_init] = 'createClass';
            i_init++;
            header[i_init] = templateHelper.createIdentity().GetHeader
            available[i_init] = templateHelper.createIdentity({ Alignments: data.Alignments }, { Genders: data.Genders }).GetContent
            elementID[i_init] = 'createIdentity';
            i_init++;
            header[i_init] = templateHelper.selectSkills().GetHeader
            available[i_init] = templateHelper.selectSkills({ Skills: data.Skills }).GetContent
            elementID[i_init] = 'selectSkills';
            i_init++;
            header[i_init] = templateHelper.selectFeats().GetHeader
            available[i_init] = templateHelper.selectFeats({ Feats: data.Feats }).GetContent
            elementID[i_init] = 'selectFeats';

            maxScreen = i_init; //Set max number of screens to i_init (used above)

            $('#startCreation-Yes').click(function () {
                // fill html with Screen 0
                $('#interactive-inner').fadeOut(fadeSpeed, function () {

                $('#interactive-inner').html(createCharacterTemplate);
                $('#createCharacter-Cancel').click(function () { CancelConfirm() });
                $('#createCharacter-h').html(header[currentScreen]);
                $('#createCharacter-inner').html(available[currentScreen]);
                $('#createCharacter-Complete').hide();
                $('#createCharacter-Back').hide();
                ko.applyBindings(newCharacter, document.getElementById(elementID[currentScreen]));

                // Back Function
                $('#createCharacter-Back').click(function () {
                    if (currentScreen == maxScreen) {
                        //var returnAlignment = $.parseJSON(ko.toJSON(newCharacter.Alignment));
                        //alert(returnAlignment.Name);
                    }
                    if (currentScreen != 0) {
                        currentScreen = currentScreen - 1;
                    }
                    if (currentScreen != maxScreen) {
                        $('#createCharacter-Complete').hide();
                        $('#createCharacter-Next').show();
                    }
                    
                    $('#createCharacter-inner').fadeOut(fadeSpeed, function () {
                        if (currentScreen > 0) {
                            $('#createCharacter-h').html(header[currentScreen]);
                            $('#createCharacter-inner').html(available[currentScreen]);
                        }
                        else if (currentScreen == 0) {
                            $('#createCharacter-h').html(header[currentScreen]);
                            $('#createCharacter-inner').html(available[currentScreen]);
                            $('#createCharacter-Back').fadeOut(fadeSpeed/2);
                        }
                        else {

                        }
                        ko.applyBindings(newCharacter, document.getElementById(elementID[currentScreen]));
                        if (currentScreen == 1) {
                            newCharacter.updateRaceIcon();
                        }
                        else if (currentScreen == 2) {
                            newCharacter.updateClassIcon();
                        }
                        else if (currentScreen == 4) {
                            newCharacter.UpdateSkillCheckbox();
                        }
                        $('#createCharacter-inner').fadeIn(fadeSpeed);
                    });
                });
                // End Back Function

                // Next Function
                $('#createCharacter-Next').click(function () {
                    if (currentScreen != maxScreen) {
                        currentScreen = currentScreen + 1;
                    }
                    if (currentScreen == maxScreen) {
                        $('#createCharacter-Complete').show();
                        $('#createCharacter-Next').hide();
                    }
                    $('#createCharacter-inner').fadeOut(fadeSpeed, function () {
                        if (currentScreen < maxScreen) {
                            $('#createCharacter-h').html(header[currentScreen]);
                            $('#createCharacter-inner').html(available[currentScreen]);
                        }
                        else {
                            $('#createCharacter-h').html(header[currentScreen]);
                            $('#createCharacter-inner').html(available[currentScreen]);
                        }
                        if (currentScreen > 0) {
                            $('#createCharacter-Back').fadeIn(fadeSpeed);
                        }
                        ko.applyBindings(newCharacter, document.getElementById(elementID[currentScreen]));
                        if (currentScreen == 1) {
                            newCharacter.updateRaceIcon();
                        }
                        else if (currentScreen == 2) {
                            newCharacter.updateClassIcon();
                        }
                        else if (currentScreen == 4) {
                            newCharacter.UpdateSkillCheckbox();
                        }
                        $('#createCharacter-inner').fadeIn(fadeSpeed);
                    });
                });
                //End Next Function

                // Complete Function
                $('#createCharacter-Complete').click(function () {
                    var Completed = confirm("Done Creating Character?");
                    var i;
                    if (Completed == true) {
                        var createNewCharacter = new CreateNewCharacter();
                        createNewCharacter.RaceID = newCharacter.Race;
                        for (i = 0; i < newCharacter.Stats().length; i++) {
                            createNewCharacter.Stats.push({ Id: newCharacter.Stats()[i].Id, Value: newCharacter.Stats()[i].Value });
                        }
                        for (i = 0; i < newCharacter.Skills().length; i++) {
                            if (newCharacter.Skills()[i].SelectedSkill == 1) {
                                createNewCharacter.Skills.push({ Id: newCharacter.Skills()[i].Id, Value: newCharacter.Skills()[i].Value });
                            }
                        }
                        for (i = 0; i < newCharacter.Feats().length; i++) {
                            if (newCharacter.Feats()[i].Selected == 1) {
                                createNewCharacter.Feats.push({ Id: newCharacter.Feats()[i].Id});
                            }
                        }
                        createNewCharacter.ClassID = newCharacter.Class;
                        createNewCharacter.AlignmentID = newCharacter.Alignment().Id;
                        createNewCharacter.GenderID = newCharacter.Gender().Id;
                        createNewCharacter.Name = newCharacter.Name();
                        createNewCharacter.Age = newCharacter.Age();
                        createNewCharacter.Height = newCharacter.Height();
                        createNewCharacter.Weight = newCharacter.Weight();
                        createNewCharacter.Level = newCharacter.Level();
                        createNewCharacter.History = newCharacter.History();
                        
                        alert(JSON.stringify(createNewCharacter));
                    }
                });
                // End Complete Function

                $('#interactive-inner').fadeIn(fadeSpeed);
                });
            });
        });
     });

    //var newCharacter = new NewCharacter(); // initialize variable to be used to store info to create character
    //$('#interactive-inner').tooltip({
    //    tooltipClass: "Selection-tooltip",
    //    track: true
    //});
    //document.addEventListener("mouseup", function () {
    //    $('#interactive-inner').tooltip("close");
    //});

     $(".chat").resizable({
         handles: 'n'
     });

     //$("#character-info").slimScroll({
     //    height: '84%'
     //});
     $("#party-info").slimScroll({
         height: 'auto',
         railVisible: false,
     });
     $("#chat").slimScroll({
         height: '100%'
     });

    var stompClient;
    var i = 0;
    var Channel = "/topic/TonysChannel";
    var socket = new SockJS('http://www.simplert.com:8100/ws');
    stompClient = Stomp.over(socket);

    stompClient.connect({}, function (frame) {
        stompClient.subscribe(Channel, function (message) {
            $('<div/>').text(" [" + moment().format('HH:mm:ss') + "] " + message.body).appendTo('#chat');
            $("#chat").scrollTop($("#chat")[0].scrollHeight);
        });
        firstLogon = $('#chatname-input').val() + " has joined the chat.";
        SendData(firstLogon);
    }, function (error) {
        console.log("Connection error " + error);
    });

    $("#chat-input").keypress(function (event) {
        if (event && event.keyCode == 13) {
            if ($('#chatname-input').val() == "") {
                alert("Please enter in username.")
            }
            else {
                var senddata = $('#chatname-input').val() + ": " + $('#chat-input').val();
                SendData(senddata);
                $('#chat-input').val("");
            }
        }
    });
    function SendData(data) {
        stompClient.send(Channel, {}, data);
        return 0;
    }
});