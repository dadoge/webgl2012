$(document).ready(function () {
    Function.prototype.bind = function (obj) {
        var method = this,
         temp = function () {
             return method.apply(obj, arguments);
         };

        return temp;
    }

    var templateHelper = new TemplateHelper();
    //var newCharacter = new NewCharacter(); // initialize variable to be used to store info to create character
    $('.Selection').tooltip({
        tooltipClass: "Selection-tooltip",
        track: true
    });
    $.getJSON("http://localhost/rpgsvc/getplayer/1", function (data) {
        
        var Stats_template = templateHelper.getStatsHtml({ Stats: data.Stats });
        var Skills_template = templateHelper.getSkillsHtml({ Skills: data.Skills });

         $('#character-text').html(Stats_template);
         $('#chatname-input').val(data.Name);

         $("#options-character-info").change(function () {
             if (document.getElementById('options-character-info').selectedIndex == 0) {
                 $('#character-text').html(Stats_template);
             }
             else if (document.getElementById('options-character-info').selectedIndex == 1) {
                 $('#character-text').html(Skills_template);
             }

         });
    });

     $('#Create-New-Character').click(function () {
        //setup the buttons
        $('#interactive-inner').html(templateHelper.startCreation());
        $('.Selection').tooltip("close");
        $('#CreateCharacter-Cancel').click(function () { CancelConfirm() });
        $.getJSON("http://localhost/rpgsvc/createnewcharacter", function (data) {
            var viewModel = {
                availableAlignments: ko.observableArray(['Good', 'Bad', 'Neutral'])
            };
            var newCharacter = new NewCharacter(data);
            var available = []; // available entities, such Race, Stats, etc
            var header = []; // var to populate header for screen
            var currentScreen = 0;
            var maxScreen = 2;
            var KObound = [];
            for (var i = 0; i <= maxScreen; i++) {
                KObound[i] = 0;
            }
            var i_init=0; //counter for generating templates
            //initialize all templates and populate using data.*
            createCharacterTemplate = templateHelper.startCreation_btns()
            header[i_init] = templateHelper.selectStats().GetHeader
            available[i_init] = templateHelper.selectStats({ Stats: data.Stats }).GetContent
            i_init++;
            header[i_init] = templateHelper.selectRace().GetHeader
            available[i_init] = templateHelper.selectRace({ Races: data.Races }).GetContent
            i_init++;
            header[i_init] = templateHelper.createIdentity().GetHeader
            available[i_init] = templateHelper.createIdentity({ Alignments: data.Alignments }, { Genders: data.Genders }).GetContent

            $('#startCreation-Yes').click(function () {
                // fill html with Screen 0
                $('#interactive-inner').html(createCharacterTemplate);
                $('#createCharacter-Cancel').click(function () { CancelConfirm() });
                $('#createCharacter-h').html(header[currentScreen]);
                $('#createCharacter-inner').html(available[currentScreen]);
                $('#createCharacter-Back').hide();

                $('#createCharacter-Back').click(function () {
                    if (currentScreen == 2) {
                        //var returnAlignment = $.parseJSON(ko.toJSON(newCharacter.Alignment));
                        //alert(returnAlignment.Name);
                    }
                    if (currentScreen != 0) {
                        currentScreen = currentScreen - 1;
                    }
                    if (currentScreen > 0) {
                        $('#createCharacter-h').html(header[currentScreen]);
                        $('#createCharacter-inner').html(available[currentScreen]);
                    }
                    else if (currentScreen == 0) {
                        $('#createCharacter-h').html(header[currentScreen]);
                        $('#createCharacter-inner').html(available[currentScreen]);
                        $('#createCharacter-Back').hide();
                    }
                    else {

                    }
                });
                $('#createCharacter-Next').click(function () {
                    if (currentScreen != maxScreen) {
                        currentScreen = currentScreen + 1;
                    }
                    if (currentScreen < maxScreen) {
                        $('#createCharacter-h').html(header[currentScreen]);
                        $('#createCharacter-inner').html(available[currentScreen]);
                    }
                    else {
                        $('#createCharacter-h').html(header[currentScreen]);
                        $('#createCharacter-inner').html(available[currentScreen]);
                    }
                    if (currentScreen > 0) {
                        $('#createCharacter-Back').show();
                    }
                        if (currentScreen == 2) {
                            ko.applyBindings(newCharacter,document.getElementById('createIdentity'));
                        }
                });
            });
        });
    });


    //// Activates knockout.js
    // ko.applyBindings(newCharacter, document.getElementById('test'));

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