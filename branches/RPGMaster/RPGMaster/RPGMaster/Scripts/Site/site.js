$(document).ready(function () {
    var templateHelper = new TemplateHelper();
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
        $('#interactive-inner').html(templateHelper.startCreation());
        $('.Selection').tooltip("close");
        $('#CreateCharacter-Cancel').click(function (){CancelConfirm()});
        var newCharacter = NewCharacter();
        $.getJSON("http://localhost/rpgsvc/createnewcharacter", function (data) {
            var available = [];
            var header = [];
            var placeHolder = 0;
            var maxScreen = 2;
            createCharacterTemplate = templateHelper.startCreation_btns()
            header[0] = templateHelper.selectRace().GetHeader
            available[0] = templateHelper.selectRace({ Races: data.Races }).GetContent
            header[1] = templateHelper.selectStats().GetHeader
            available[1] = templateHelper.selectStats({ Stats: data.Stats }).GetContent
            //available[2] = templateHelper.createIdentity({ Alignments: data.Alignments, Genders: data.Genders })
            //var availableAlignments = templateHelper.selectAlignment({ Alignments: data.Alignments })

            $('#startCreation-Yes').click(function () {

                $('#interactive-inner').html(createCharacterTemplate);
                $('#createCharacter-Cancel').click(function () { CancelConfirm() });
                $('#createCharacter-h').html(header[placeHolder]);
                $('#createCharacter-inner').html(available[placeHolder]);
                $('#createCharacter-Back').hide();

                $('#createCharacter-Back').click(function () {
                    placeHolder = placeHolder - 1;
                    if (placeHolder > 0) {
                        $('#createCharacter-h').html(header[placeHolder]);
                        $('#createCharacter-inner').html(available[placeHolder]);
                    }
                    else if (placeHolder == 0) {
                        $('#createCharacter-h').html(header[placeHolder]);
                        $('#createCharacter-inner').html(available[placeHolder]);
                        $('#createCharacter-Back').hide();
                    }
                    else {

                    }
                });
                $('#createCharacter-Next').click(function () {
                    placeHolder=placeHolder+1;
                    if (placeHolder < maxScreen) {
                        $('#createCharacter-h').html(header[placeHolder]);
                        $('#createCharacter-inner').html(available[placeHolder]);
                    }
                    else {

                    }
                    if (placeHolder > 0) {
                        $('#createCharacter-Back').show();
                    }
                });
            });
        });
    });

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