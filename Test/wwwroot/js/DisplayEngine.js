//function timeConverter(time) {
//	if (time == '0645') return 1;
//	else if (time == '0730') return 2;
//	else if (time == '0815') return 3;
//	else if (time == '0825') return 4;
//	else if (time == '0920') return 5;
//	else if (time == '1005') return 6;
//	else if (time == '1015') return 7;
//	else if (time == '1100') return 8;
//	else if (time == '1145') return 9;
//	else if (time == '1230') return 10;
//	else if (time == '1315') return 11;
//	else if (time == '1400') return 12;
//	else if (time == '1410') return 13;
//	else if (time == '1455') return 14;
//	else if (time == '1505') return 15;
//	else if (time == '1550') return 16;
//	else if (time == '1600') return 17;
//	else if (time == '1645') return 18;
//	else if (time == '1705') return 19;
//	else if (time == '1730') return 20;
//}
//function display(classInfo) {
//	var timeSplit = classInfo.Time.split('-');
//	var startTime = timeConverter(timeSplit[0]);
//	var endTime = timeConverter(timeSplit[1]);
//	// Tim o cua cac lop hoc tren table theo thoi gian
//	var cells = $('#myTable tr').slice(StartTime, EndTime + 1).find('td').eq(classInfo.Weekday - 1);

//	var subjectContainerDiv = $('<div>')
//		.addClass('selectedSubject btn-container')
//		.css({
//			'position': 'relative',
//		});
//	// Tao ra mot div element cho mon hoc
//	var subjectDiv = $('<div>')
//		.html(classInfo.SubjectName + '<br>' + classInfo.ClassId + '<br>' + classInfo.ClassType + '<br>' + classInfo.lassRoom)
//		.addClass('selectedSubject btn')
//		.css({
//			'color': '#000000',
//			'padding': '5px',
//			'border-radius': '5px',
//			'position': 'relative',
//		});
//	// Append close button va subjectDiv
//	subjectContainerDiv.append(subjectDiv);
//	// Thay doi mau o da chon
//	cells.attr('rowspan', endTime - startTime + 1).css({ 'background-color': '#87C4FF' });

//	// Append subjectContainerDiv vao o dau tien
//	cells.first().append(subjectContainerDiv);
//	closeButton.css({
//		'position': 'absolute',
//		'top': '0',
//		'right': '0',
//	});
//})