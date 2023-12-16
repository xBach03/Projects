function userSearch(){
	var userName = document.getElementById("UserName").value;
	$.ajax({
		url: '/Search/UserSearch',
		type: 'GET',

	});
}
