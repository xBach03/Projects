﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
	// Mang thong tin - problems about time.
	var selectedSubjects = [];
	function showClasses(subjectId) {

		$.ajax({
			url: '/TimeTable/GetClassesBySubjectId',
			type: 'GET',
			datatype: 'json',
			data: { subjectId: subjectId },
			success: function (classData) {
				// console.log(classData);
				// classData.forEach(function (classInfo) {
				// 	console.log(classInfo.time);
				// })
				displaySubjects(classData);
			},
			error: function (error) {
				console.error('Error fetching class data:', error);
			}
		});
	}
	function timeConverter(time) {
		if (time == '0645') return 1;
	else if (time == '0730') return 2;
	else if (time == '0815') return 3;
	else if (time == '0825') return 4;
	else if (time == '0920') return 5;
	else if (time == '1005') return 6;
	else if (time == '1015') return 7;
	else if (time == '1100') return 8;
	else if (time == '1145') return 9;
	else if (time == '1230') return 10;
	else if (time == '1315') return 11;
	else if (time == '1400') return 12;
	else if (time == '1410') return 13;
	else if (time == '1455') return 14;
	else if (time == '1505') return 15;
	else if (time == '1550') return 16;
	else if (time == '1600') return 17;
	else if (time == '1645') return 18;
	else if (time == '1705') return 19;
	else if (time == '1730') return 20;
	}

	function displaySubjects(classData) {
		// Clear existing subject display content without the first row and column
		$('#myTable td[style*="background-color: rgb(224, 244, 255)"]').empty().removeAttr('style').removeAttr('rowspan');

	function classCode(classSearch) {
		var results = [];
		classData.forEach(function (classInfo) {
			if (classInfo.subjectName === classSearch.subjectName && classInfo.time === classSearch.time && classInfo.weekday === classSearch.weekday) {
				results.push(classInfo.classId);
			}
		})
		return results.join('/');
	}
	function findClass(classId) {
			for (var i = 0; i < classData.length; i++) {
				if (classData[i].classId == classId) {
					return classData[i];
				}
			}
	return null;
		}
	// Iterate through classData and create div elements for each class
	classData.forEach(function (classInfo) {
			
			var timeSplit = classInfo.time.split('-');
	var startTime = timeConverter(timeSplit[0]);
	var endTime = timeConverter(timeSplit[1]);

	// Find the corresponding table cells
	var cells = $('#myTable tr').slice(startTime, endTime+1).find('td').eq(classInfo.weekday - 1);

	// Create a div element for the class
	if (!cells.children('.subjectDiv').length && cells.css('background-color') !== 'rgb(135, 196, 255)') {
				
				var subjectDiv = $('<div>')
		.html(classInfo.subjectName + '<br>' + classCode(classInfo))
			.addClass('subjectDiv btn')
			.css({
				'color': '#000000', // Set your desired text color
			'padding': '5px',
			'border-radius': '5px',
					});

			// Set the rowspan attribute to span the correct duration
			cells.attr('rowspan', endTime - startTime + 1).css({'background-color': '#E0F4FF' }) // Set span and background color of cells

			// Append the div to the first cell in the column
			cells.first().append(subjectDiv);

			subjectDiv.on('click', function () {
					// console.log(classCode(classInfo))
					if (classCode(classInfo).split('/').length > 1) {
						var classId = prompt("Select one class id: " + classCode(classInfo));
			var result = findClass(classId);
			if (classId == null || classId == "" || classCode(classInfo).split('/').indexOf(classId) == -1) {
				alert("Invalid class id");
						} else {
				updateTable(result);
			selectedSubjects.push(result);
						}
					} else {
				updateTable(classInfo);
			selectedSubjects.push(classInfo);
					}
			console.log(selectedSubjects);
				});
			}
		});
	}


	function updateTable(classInfo) {
		// Clear existing subject display content without the first row and column
		$('#myTable td[style*="background-color: rgb(224, 244, 255)"]').empty().removeAttr('style').removeAttr('rowspan');
		var timeSplit = classInfo.time.split('-');
		var startTime = timeConverter(timeSplit[0]);
		var endTime = timeConverter(timeSplit[1]);
		// Iterate through selectedSubjects and add them to the table
		var cells = $('#myTable tr').slice(startTime, endTime + 1).find('td').eq(classInfo.weekday - 1);

		var subjectContainerDiv = $('<div>')
			.addClass('selectedSubject btn-container')
			.css({
			'position': 'relative', // Set position to relative
			});
		// Create a div element for the class
		var subjectDiv = $('<div>')
			.html(classInfo.subjectName + '<br>' + classInfo.classId + '<br>' + classInfo.classType + '<br>' + classInfo.classRoom)
			.addClass('selectedSubject btn')
			.css({
				'color': '#000000', // Set your desired text color
				'padding': '5px',
				'border-radius': '5px',
				'position': 'relative',
			});
			var closeButton = $('<button>')
				.addClass('btn-close')
				.on('click', function () {
					// Handle the close button click event
					clearCells(cells, classInfo);
				});

			// Append the close button to the subjectDiv
			subjectContainerDiv.append(subjectDiv, closeButton);
			// Set the rowspan attribute to span the correct duration
			cells.attr('rowspan', endTime - startTime + 1).css({'background-color': '#87C4FF' });

			// Append the div to the first cell in the column
			cells.first().append(subjectContainerDiv);
			closeButton.css({
				'position': 'absolute',
				'top': '0',
				'right': '0',
			});
		
	}
	function clearCells(cells, classInfo) {
		cells.empty().removeAttr('style').removeAttr('rowspan');
		var deleteIndex = selectedSubjects.indexOf(classInfo);
		selectedSubjects.splice(deleteIndex, 1);
		console.log(selectedSubjects);
	}
	function saveTimetable() {
		$.ajax({
			url: '/Timetable/SaveTimetable',
			type: 'POST',
			contentType: 'application/json',
			data: JSON.stringify(selectedSubjects),
			success: function (response) {
				console.log('Selected subjects sent successfully', response);
				alert('Timetable saved successfully');
			},
			error: function (error) {
				console.error('Error sending selected subjects');
			}
		});
}
