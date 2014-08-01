// Chamando kindle sync
$.ajax({
	url: 'http://localhost:3508/api/notebooks/' + _n.notebook_guid + '/notes'
}).done(function(data, textStatus, jqXHR) {
	console.log(JSON.stringify(data));
	console.log(textStatus);
	console.log(JSON.stringify(jqXHR));
}).fail(function(jqXHR, textStatus, errorThrown) {
	console.log(jqXHR);
	console.log(textStatus);
	console.log(errorThrown);
});