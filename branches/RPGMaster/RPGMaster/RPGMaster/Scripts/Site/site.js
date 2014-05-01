$(document).ready(function () {
     $.getJSON("http://localhost/rpgsvc/getplayer/1", function (data) {
         var list = Character_Stats();
         var Stats_template = _.template(Character_Stats(), { Stats: data.Stats });
         var Skills_template = _.template(Character_Skills(), { Skills: data.Skills });

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
         //document.getElementById('character-stats').innerHTML = _.template(Character_Stats(), {Stats:data.Stats});
        //var list = "<% _.each(people, function(name) { %> <li><%= name %></li> <% }); %>";
        //document.getElementById('character-stats').innerHTML = "Test";


        //document.getElementById('character-stats').innerHTML = data.Stats;
        //document.getElementById('character-stats').innerHTML = _.template(list, { Stats: [{ Name: 'DEX', Value: 18 }, {Name: 'STR', Value: 18}]});
        //document.getElementById('character-info').innerHTML="Test";
        //$('#character-info').append(data.Name);
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