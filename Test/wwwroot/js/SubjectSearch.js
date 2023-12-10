function subjectSearch() {
	var subjectId = document.getElementById("Subject").value;
	console.log(subjectId);
	$.ajax({
		url: '/TimeTable/subjectSearch',
		type: 'GET',
		datatype: 'json',
		data: { subjectId: subjectId },
		success: function (classData) {
			// console.log(classData);
			// classData.forEach(function (classInfo) {
			// 	console.log(classInfo.time);
			// })
			displaySubjects(classData);
			console.log(classData);
		},
		error: function (error) {
			console.log('Error fetching class data:', error);
		}
	});
}
var table = document.createElement("table");
document.body.appendChild(table);
function displaySubjects(classData) {
	var row = table.insertRow();
	var cell = row.insertCell();
	cell.innerHTML = classData.subjectId + ": " + classData.subjectName;
}