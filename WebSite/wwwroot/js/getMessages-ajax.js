
//Смена типа при нажатии на дату
function change() {
    $("#date1").attr("type", "date");
    $("#date2").attr("type", "date");
}


function changeStatus(i) {

    var idSelect = "sel" + i;
    var status = document.getElementById(idSelect).value;

    $.ajax({
        url: "/Admin/EditStatus",
        method: 'post',
        dataType: 'json',
        data: { id: i, status: status},
        success: function (data) {
         
        },
        error: function (er) {
            console.log(er);
        }
    });
}

function addFilter() {
    var status = document.getElementById('filterStatus').value;
    $.ajax({
        url: "/Admin/AddFilter",
        method: 'post',
        dataType: 'json',
        data: { statusName: status },
        success: function (data) {
            $('#myTableId tbody').empty();
            console.log(data.messageStatus);
            console.log(data.statuses);

            for (var i = 0; i < data.messageStatus.length; i++) {
                var idSelect = 'sel' + data.messageStatus[i].id;
                var string = '';

                string += `<tr>` +
                    `<td>${data.messageStatus[i].id}</td>` +
                    `<td>${data.messageStatus[i].date}</td>` +
                    `<td>${data.messageStatus[i].name}</td>` +
                    `<td>${data.messageStatus[i].text}</td>` +
                    `<td>${data.messageStatus[i].contact}</td>` +
                    `<td>`;

                if (data.statuses != null) {
                    string += `<select id="${idSelect}" onchange="changeStatus(${data.messageStatus[i].id});">`;

                    for (var j = 0; j < data.statuses.length; j++) {
                        if (data.statuses[j].name == data.messageStatus[i].status) {
                            string += `<option selected>${data.statuses[j].name}</option>`;
                        }
                        else {
                            string += `<option>${data.statuses[j].name}</option>`;
                        }
                    }
                    string += `</select>`;
                }
                string += `</td></tr>`;
                $('tbody').append(string);

            }

            $('#forThePeriod').empty();
            $('#forThePeriod').append(`${data.messageStatus.length}`);
        },
        error: function (er) {
            console.log(er);
        }
    });
}

$(document).ready(() => {

    $("#divtoday, #divyesterday, #divweek, #divmonth").click(function () {

        if (this.id == 'divtoday') {
            var today = document.getElementById("today").value;
            document.getElementById('divtoday').classList.add('active-date');

            document.getElementById('divyesterday').classList.remove('active-date');
            document.getElementById('divweek').classList.remove('active-date');
            document.getElementById('divmonth').classList.remove('active-date');
        }

        if (this.id == 'divyesterday') {
            var yesterday = document.getElementById("yesterday").value;
            document.getElementById('divyesterday').classList.add('active-date');

            document.getElementById('divtoday').classList.remove('active-date');
            document.getElementById('divweek').classList.remove('active-date');
            document.getElementById('divmonth').classList.remove('active-date');

        }

        if (this.id == 'divweek') {
            var week = document.getElementById("week").value;
            document.getElementById('divweek').classList.add('active-date');

            document.getElementById('divtoday').classList.remove('active-date');
            document.getElementById('divyesterday').classList.remove('active-date');
            document.getElementById('divmonth').classList.remove('active-date');

        }

        if (this.id == 'divmonth') {
            var month = document.getElementById("month").value;
            document.getElementById('divmonth').classList.add('active-date');

            document.getElementById('divtoday').classList.remove('active-date');
            document.getElementById('divweek').classList.remove('active-date');
            document.getElementById('divyesterday').classList.remove('active-date');

        }

        

        $.ajax({
            url: "/Admin/TableData",
            method: 'post',
            dataType: 'json',
            data: { today: today, yesterday: yesterday, week: week, month: month },
            success: function (data) {
                $('#myTableId tbody').empty();
                console.log(data.messageStatus);
                console.log(data.statuses);

                for (var i = 0; i < data.messageStatus.length; i++) {
                    var idSelect = 'sel' + data.messageStatus[i].id;
                    var string = '';

                    var ins = data.messageStatus[i].date;
                    var fields = ins.split('T');
                    var date = fields[0];
                    var time = fields[1];

                    var splitDate = date.split('-');
                    var year = splitDate[0];
                    var month = splitDate[1];
                    var day = splitDate[2];

                    var date = day + '.' + month + '.' + year + ' ' + time.substring(0, 8);

                    string += `<tr>`+
                           `<td>${data.messageStatus[i].id}</td>`+
                           `<td>${date}</td>`+
                           `<td>${data.messageStatus[i].name}</td>`+
                           `<td>${data.messageStatus[i].text}</td>`+
                           `<td>${data.messageStatus[i].contact}</td>`+
                           `<td>`;

                    if (data.statuses != null) {
                        string += `<select id="${idSelect}" onchange="changeStatus(${data.messageStatus[i].id});">`;

                        for (var j = 0; j < data.statuses.length; j++) {
                            if (data.statuses[j].name == data.messageStatus[i].status) {
                                string += `<option selected>${data.statuses[j].name}</option>`;
                            }
                            else {
                                string += `<option>${data.statuses[j].name}</option>`;
                            }
                        }
                        string += `</select>`;
                    }
                    string += `</td></tr>`;
                    $('tbody').append(string);

                }

                $('#forThePeriod').empty();
                $('#forThePeriod').append(`${data.messageStatus.length}`);
            },
            error: function (er) {
                console.log(er);
            }
        });

    });

    //При выборе периода времени
    $('#submitForPeriod').click(function () {

        var date1 = document.getElementById("date1").value;
        var date2 = document.getElementById("date2").value;

        document.getElementById('divtoday').classList.remove('active-date');
        document.getElementById('divyesterday').classList.remove('active-date');
        document.getElementById('divweek').classList.remove('active-date');
        document.getElementById('divmonth').classList.remove('active-date');

        $.ajax({
            url: "/Admin/TableDataPeriod",
            method: 'post',
            dataType: 'json',
            data: { date1: date1, date2: date2 },
            success: function (data) {
                $('#myTableId tbody').empty();
                console.log(data);
                for (var i = 0; i < data.messageStatus.length; i++) {
                    var idSelect = 'sel' + data.messageStatus[i].id;
                    var string = '';

                    string += `<tr>` +
                        `<td>${data.messageStatus[i].id}</td>` +
                        `<td>${data.messageStatus[i].date}</td>` +
                        `<td>${data.messageStatus[i].name}</td>` +
                        `<td>${data.messageStatus[i].text}</td>` +
                        `<td>${data.messageStatus[i].contact}</td>` +
                        `<td>`;

                    if (data.statuses != null) {
                        string += `<select id="${idSelect}" onchange="changeStatus(${data.messageStatus[i].id});">`;

                        for (var j = 0; j < data.statuses.length; j++) {
                            if (data.statuses[j].name == data.messageStatus[i].status) {
                                string += `<option selected>${data.statuses[j].name}</option>`;
                            }
                            else {
                                string += `<option>${data.statuses[j].name}</option>`;
                            }
                        }
                        string += `</select>`;
                    }
                    string += `</td></tr>`;
                    $('tbody').append(string);

                }

                $('#forThePeriod').empty();
                $('#forThePeriod').append(data.length);
            },
            error: function (er) {
                console.log(er);
            }
        });

    });


    

});