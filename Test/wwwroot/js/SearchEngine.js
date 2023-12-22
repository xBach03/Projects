function userSearch(){
	var userName = document.getElementById("userName").value;
	var selected = $("input[name='flexRadioDefault']:checked").val();
	if (selected == 'users') {
		$.ajax({
			url: '/Search/UserSearch',
			type: 'GET',
			dataType: 'json',
			data: { userName: userName },
			success: function (url) {
				window.location.href = url;
			},
			error: function (error) {
				console.log(error);
			}
		});
	}
	if (selected == 'terms') {
		$.ajax({
			url: '/Search/TermSearch',
			type: 'GET',
			data: { userName: userName },
			success: function (users) {
				console.log(users);
			},
			error: function (error) {
				console.log(error);
			}
		});
	}
}
function displayTable(timetableId) {

}
