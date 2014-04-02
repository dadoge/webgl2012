	$( document ).ready(function() {
		var stompClient;
		var i = 0;
		var Channel = "/topic/TonysChannel";
		var socket = new SockJS('http://www.simplert.com:8100/ws');
		stompClient = Stomp.over(socket);
		
		stompClient.connect({}, function(frame) {
			stompClient.subscribe(Channel, function(message) {
				$('<div/>').text(" [" + moment().format('HH:mm:ss') +"] " + message.body).appendTo('#chat');
				$("#chat").scrollTop($("#chat")[0].scrollHeight);
			});
		  }, function(error) {
			console.log("Connection error " + error);
		  });
			
			$("#chat-input").keypress(function(event){
			  if(event && event.keyCode == 13)
			   {
					if ($('#chatname-input').val()=="") {
						alert("Please enter in username.")
					}
					else {
					var senddata= $('#chatname-input').val() + ": " + $('#chat-input').val();
					SendData(senddata);
					$('#chat-input').val("");
					}
			   }
			}); 
			function SendData(data){
				stompClient.send(Channel, {}, data);
				return 0;
			}
	});