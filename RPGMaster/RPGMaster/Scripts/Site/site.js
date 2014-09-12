var ChatName = "";
var ActivePlayerID;
var templateHelper = new TemplateHelper();

$(document).ready(function () {
    LoadUserPlayer(templateHelper);

    $('#Create-New-Character').click(function () { CreateNewCharacterClick(templateHelper); });
    $('#Manage-Characters').click(function () { ManageCharacterClick(templateHelper); });
    $('#Map-Editor').click(function () { startMapEditor(templateHelper); });

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

     var rpgProxy = $.connection.rpgHub;
     rpgProxy.client.broadcastMessage = function (name, message) {
         // Html encode display name and message.
         var encodedName = $('<div />').text(name).html();
         var encodedMsg = $('<div />').text(message).html();
         // Add the message to the page.
         $('#chat').append('<li><strong>' + encodedName
             + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
     };

     $.sendMessage = function sendMessage(name, message) {
         rpgProxy.server.sendMessage(name, message);
     };

     $.connection.hub.start().done(function () {
         $("#chat-input").keypress(function (event) {
             if (event && event.keyCode == 13) {
                 rpgProxy.server.sendMessage('Test', $('#chat-input').val());
                 $('#chat-input').val("");
             }
         });
     });


     ///OLD CHAT CODE BELOW, PLEASE DELETE AFTER REFACTOR
     //var stompClient;
     //var i = 0;
     //var Channel = "/topic/TonysChannel";
     //var socket = new SockJS('http://www.simplert.com:8100/ws');
     //stompClient = Stomp.over(socket);

     //stompClient.connect({}, function (frame) {
     //    stompClient.subscribe(Channel, function (message) {
     //        $('<div/>').text(" [" + moment().format('HH:mm:ss') + "] " + message.body).appendTo('#chat');
     //        $("#chat").scrollTop($("#chat")[0].scrollHeight);
     //    });
     //    firstLogon = $('#chatname-input').val() + " has joined the chat.";
     //    SendData(firstLogon);
     //}, function (error) {
     //    console.log("Connection error " + error);
     //});

     //$("#chat-input").keypress(function (event) {
     //    if (event && event.keyCode == 13) {
     //        if ($('#chatname-input').val() == "") {
     //            alert("Please enter in username.")
     //        }
     //        else {
     //            var senddata = $('#chatname-input').val() + ": " + $('#chat-input').val();
     //            SendData(senddata);
     //            $('#chat-input').val("");
     //        }
     //    }
     //});
     //function SendData(data) {
     //    stompClient.send(Channel, {}, data);
     //    return 0;
     //}

});