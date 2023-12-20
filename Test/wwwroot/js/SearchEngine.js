function userSearch(){
	var userName = document.getElementById("userName").value;
	var selected = $("input[name='flexRadioDefault']:checked").val();
	if (selected == 'users') {
		$.ajax({
			url: '/Search/UserSearch',
			type: 'GET',
			dataType: 'json',
			data: { userName: userName },
			success: function (usersData) {
				console.log(usersData);
				displayUser(usersData);
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
function displayUser(usersData) {
	var div = document.getElementById("displayer");
	var table = document.createElement("table");
	table.setAttribute('class', 'table');
	table.setAttribute('id', 'myTable')
	div.appendChild(table);
	usersData.forEach(function (userData) {
		var row = table.insertRow();
		var cell = row.insertCell();
		cell.innerHTML = userData.userName + " - " + userData.major + " - " + userData.course;
	});
}
