/* function AppendChat() {
	$( "#chat" ).append( "<p>Test</p>" );
	$("#chat").scrollTop($("#chat")[0].scrollHeight);
}; */

	$( document ).ready(function() {
		var stompClient;
		var i = 0;
		var Channel = "/topic/TonysChannel";
		(function() {
			var socket = new SockJS('http://www.simplert.com:8100/ws');
			stompClient = Stomp.over(socket);
			
			stompClient.connect({}, function(frame) {
				stompClient.subscribe(Channel, function(message) {
					//var today = new Date();
					$('<div/>').text(" [" + moment().format('HH:mm:ss') +"] " + message.body).appendTo('#chat');
					$("#chat").scrollTop($("#chat")[0].scrollHeight);
					//$( "#chat" ).append( message.body);
					//+ $('#chatname-input').val() + ": "
				});
			  }, function(error) {
				console.log("Connection error " + error);
			  });
			
			/*$('#character-hide').on('click', function(){
				 stompClient.send(Channel, {}, $('#chat-input').val());
				 //
			 });
			*/
				$("#chat-input").keypress(function(event){
				  if(event && event.keyCode == 13)
				   {
						var senddata= $('#chatname-input').val() + ": " + $('#chat-input').val();
						stompClient.send(Channel, {}, senddata);
						$('#chat-input').val("");
				   }
				}); 
		//	$('#chat-input').val()
				
				//
			
		})();
	});