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