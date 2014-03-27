fuction AppendChat() {
	$( "#chat" ).append( "<p>Test</p>" );
	$("#chat").scrollTop($("#chat")[0].scrollHeight);
}