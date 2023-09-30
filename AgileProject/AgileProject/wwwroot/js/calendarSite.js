// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {

    //var host = "https://localhost:7152/"; // document.getElementById('hostName').value;
    var host = document.location.protocol + '//' + location.host + '/';
    //console.log(host);

    var arrayData = []; //arrayBirthdays.concat(arrayHolidays).concat(arrayVacations).concat(arraySessions).concat(arrayOthers);
    var eventData = []; //arrayBirthdays.concat(arrayHolidays).concat(arrayVacations).concat(arraySessions).concat(arrayOthers);
    //
    var myteamsList = document.getElementById("myteamsList");
    myteamsList.addEventListener('change', onChangeMyTeamList);

    var myeventTypeList = document.getElementById("eventTypeList");
    myeventTypeList.addEventListener('change', setModalEventType);

    var myabrirVentana = document.getElementById("abrirVentana");
    myabrirVentana.addEventListener('click', openVentana);


    //console.log(arrayData);
    var calendarEl = document.getElementById('calendar');
    var calendarMiniEl = document.getElementById('calendarMini');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        locale: 'es',
        timeZone: 'local', // the default (unnecessary to specify), 
        handleWindowResize: true,
        initialView: "dayGridYear",
        //multiMonthMaxColumns: 1, // force a single column
        //initialDate: '2023-01-12',
        weekends: true,
        editable: true,
        dayMaxEvents: true, // allow "more" link when too many events
        displayEventTime: true,
        nowIndicator: true,
        navLinks: true, // can click day/week names to navigate views
        selectMirror: true,
        selectable: true,
        //weekNumbers: true,
        customButtons: {
            // btnExport: {
            //     text: '',
            //     click: function () {
            //         exportToExcel("xlsx");
            //     }
            // },
            //btnFilter: {
            //    text: '',
            //    click: function () {
            //        viewFilter();
            //    }
            //}
        }, 
        headerToolbar: {
            left: 'title', //,btnExport //'prev,next today,btnFilter'
            center: 'multiMonthYear dayGridYear timeGridWeek timeGridDay listYear',
            right: 'prev,today,next' //,listYear',timeGridDay, dayGridMonth

        },
        buttonText: {
            multiMonthYear: 'año',
            dayGridMonth: 'mes',
            dayGridYear: 'mes',
            timeGridWeek: 'semana',
            timeGridDay: 'día',
            listYear: 'lista',
            today: 'hoy'
        },
        buttonIcons: {
            //multiMonthYear: 'fa-clock-o',
            //dayGridMonth: 'chevron-left',
            //dayGridYear: 'chevron-left',
            //timeGridWeek: 'chevron-left',
            //timeGridDay: 'chevron-left',
            //listYear: ""
        },        
        eventAdd: function (arg) {
            //console.log(arg); //si funciona
            //alert("debe agregar el elemento al array y la BD");
            
            //var newid = document.getElementById("new-event--id").value;
            //console.log(newid);

            //arg.event.setProp('id', newid);
            //arg.event.setProp('publicId', newid);

            //console.log(event);
        },
        eventChange: function (arg) {
           
            //color = arg.event._def.ui.backgroundColor;
            //id = arg.event._def.publicId;
            //title = arg.event._def.title;
            //start = arg.event._instance.range.start;
            //end = arg.event._instance.range.end
            //allDay = arg.event._def.allDay;
            //type = arg.event._def.extendedProps.type;
            //description = arg.event._def.extendedProps.description;

            //arrayData.forEach((element, index) => {  
            //    if (element.id == id) {

            //        element.color = color;
            //        element.title = title;
            //        element.start = start;
            //        element.end = end;
            //        element.allDay = allDay;
            //        element.description = description;
            //        element.type = type;

            //        return;
            //    }
            //});

            //calendar.render();

        },
        eventRemove: function () {
            //alert("debe remover el elemento del array y la BD");
        },
        eventDrop: function (arg) {
            var myteamsList = document.getElementById("myteamsList");
            var valueTeamsList = myteamsList.value;
            var textTeamsList = myteamsList.options[myteamsList.selectedIndex].text;

            color = arg.event._def.ui.backgroundColor;
            id = arg.event._def.publicId;
            title = arg.event._def.title;
            start = arg.event._instance.range.start;
            end = arg.event._instance.range.end
            allDay = arg.event._def.allDay;
            type = arg.event._def.extendedProps.type;
            description = arg.event._def.extendedProps.description;

            ////console.log("type", type);

            //falta que idEventType llegue como campo como los demas en lugar de calcularlo
            var eventType = document.getElementById("eventTypeList");
            var idEventType;
            //var nameEventType;
            for (var i = 0; i < eventType.length; i++) {
                var opt = eventType[i];
                if (opt.id == color) {
                    idEventType = opt.value;
                    //nameEventType = opt.text;
                }
            }


            var newstart = start;
            var newend = end;

            if (allDay) {
                newstart = new Date(start).addHours(5);
                newend = new Date(end).addHours(5);

            }
            //console.log("newstart", newstart);

            var data =
            {
                id: id,
                title: title,
                start: newstart,
                end: newend,
                allDay: allDay,
                description: description,
                color: color,
                EventTypeId: idEventType,
                type: type,
                CalendarTypeId: valueTeamsList,
                CalendarTypeName: textTeamsList
                //UserCreate: '1'

            };

            
            postEventUpd(data);
        },  
        eventResize: function (arg) {
            var myteamsList = document.getElementById("myteamsList");
            var valueTeamsList = myteamsList.value;
            var textTeamsList = myteamsList.options[myteamsList.selectedIndex].text;

            color = arg.event._def.ui.backgroundColor;
            id = arg.event._def.publicId;
            title = arg.event._def.title;
            start = arg.event._instance.range.start;
            end = arg.event._instance.range.end
            allDay = arg.event._def.allDay;
            type = arg.event._def.extendedProps.type;
            description = arg.event._def.extendedProps.description;

            ////console.log("type", type);

            //falta que idEventType llegue como campo como los demas en lugar de calcularlo
            var eventType = document.getElementById("eventTypeList");
            var idEventType;
            //var nameEventType;
            for (var i = 0; i < eventType.length; i++) {
                var opt = eventType[i];
                if (opt.id == color) {
                    idEventType = opt.value;
                    //nameEventType = opt.text;
                }
            }


            var newstart = start;
            var newend = end;

            if (allDay) {
                newstart = new Date(start).addHours(5);
                newend = new Date(end).addHours(5);

            }
            //console.log("newstart", newstart);

            var data =
            {
                id: id,
                title: title,
                start: newstart,
                end: newend,
                allDay: allDay,
                description: description,
                color: color,
                EventTypeId: idEventType,
                type: type,
                CalendarTypeId: valueTeamsList,
                CalendarTypeName: textTeamsList
                //UserCreate: '1'

            };


            postEventUpd(data);
        }, 
        select: function (arg) { //add event, click empty space --NEW

            //var fecha = new Date(arg.start);
            //var dias = 1;
            //fecha.setDate(fecha.getDate() + dias);
            /////

            
            $("#new-event--start").attr("disabled", true);
            $("#new-event--end").attr("disabled", false);

            var btnEliminar = document.getElementById("btnEliminar");
            if (!isNullOrEmpty(btnEliminar)) {
                
                document.getElementById("btnEliminar").style.display = "none";
            }
            document.getElementById("btnAgregar").style.display = "block";
            document.getElementById("btnActualizar").style.display = "none";

            

            var eventNameTitleCRUD = document.getElementById("eventNameTitleCRUD");
            eventNameTitleCRUD.innerHTML = "New Event";


            
            //var username = document.getElementById("new-event--username").value;


            if (arg.event == undefined) {
                var startStrFormat = moment(arg.start).local().format('DD/MM/YYYY hh:mm:ss a');
                var endStrFormat = moment(arg.end).local().format('DD/MM/YYYY hh:mm:ss a');

                var startStr = arg.startStr;
                var endStr = arg.endStr;


                document.getElementById("new-event--id").value = ""; //MFG
                document.getElementById("new-event--title").value = ""; // username + " - " + nameEventType; //username by default MFG
                document.getElementById("new-event--start").value = startStrFormat;
                document.getElementById("new-event--start-h").value = startStr;
                document.getElementById("new-event--end").value = endStrFormat;
                document.getElementById("new-event--end-h").value = endStrFormat; // arg.end; // endStr;
                document.getElementById("new-event--allDay").checked = true;
                document.getElementById("new-event--description").value = "";

                //$("input[name=event-tag][value=" + "#54B4D3" + "]").prop('checked', true);

                //var fechaStartDate = new Date(arg.start);
                //var fecharesta = restarDias(arg.end);
                //if (fechaStartDate == fecharesta) {
                //    document.getElementById("new-event--end").style.display = 'none';
                //}
                //document.getElementById("new-event--end").style.display = 'none';

                $('#new-event').modal('show');

                //console.log(arg);
                setArrayEventData(arg);

                evalCalendarView();
                setModalEventType();
                evalCheckAllDay();

            }
            /*
            calendar.unselect()
            */
        },
        //select: function (startDate, endDate, jsEvent, view) {

        //    var today = moment();
        //    var startDate;
        //    var endDate;
        //    var currentViewType = calendar.currentData.currentViewType;
        //    //console.log(currentViewType);

        //    if (currentViewType == "month") {
        //        startDate.set({ hours: today.hours(), minute: today.minutes() });
        //        startDate = moment(startDate).format('ddd DD MMM YYYY HH:mm');
        //        endDate = moment(endDate).subtract('days', 1);
        //        endDate.set({ hours: today.hours() + 1, minute: today.minutes() });
        //        endDate = moment(endDate).format('ddd DD MMM YYYY HH:mm');
        //    } else {
        //        startDate = moment(startDate).format('ddd DD MMM YYYY HH:mm');
        //        endDate = moment(endDate).format('ddd DD MMM YYYY HH:mm');
        //    }

        //    var $contextMenu = $("#contextMenu");

        //    var HTMLContent = '<ul class="dropdown-menu dropNewEvent" role="menu" aria-labelledby="dropdownMenu" style="display:block;position:static;margin-bottom:5px;">' +
        //        '<li onclick=\'newEvent("' + startDate + '","' + endDate + '","' + "Appointment" + '")\'> <a tabindex="-1" href="#">Add Appointment</a></li>' +
        //        '<li onclick=\'newEvent("' + startDate + '","' + endDate + '","' + "Check-in" + '")\'> <a tabindex="-1" href="#">Add Check-In</a></li>' +
        //        '<li onclick=\'newEvent("' + startDate + '","' + endDate + '","' + "Checkout" + '")\'> <a tabindex="-1" href="#">Add Checkout</a></li>' +
        //        '<li onclick=\'newEvent("' + startDate + '","' + endDate + '","' + "Inventory" + '")\'> <a tabindex="-1" href="#">Add Inventory</a></li>' +
        //        '<li onclick=\'newEvent("' + startDate + '","' + endDate + '","' + "Valuation" + '")\'> <a tabindex="-1" href="#">Add Valuation</a></li>' +
        //        '<li onclick=\'newEvent("' + startDate + '","' + endDate + '","' + "Viewing" + '")\'> <a tabindex="-1" href="#">Add Viewing</a></li>' +
        //        '<li class="divider"></li>' +
        //        '<li><a tabindex="-1" href="#">Close</a></li>' +
        //        '</ul>';

        //    $(".fc-body").unbind('click');  
        //    //console.log($(".fc-body"));

        //    document.getElementById('contextMenu').innerHTML = (HTMLContent);

        //    $contextMenu.addClass("contextOpened");
        //    $contextMenu.css({
        //        display: "block",
        //        left: 100, //e.pageX,
        //        top: 100 //e.pageY
        //    });
        //    document.getElementById('contextMenu').sty.display = "block";

        //    //$(".fc-body").on('click', 'td', function (e) {
        //    //    //console.log("click");

        //    //    document.getElementById('contextMenu').innerHTML = (HTMLContent);

        //    //    $contextMenu.addClass("contextOpened");
        //    //    $contextMenu.css({
        //    //        display: "block",
        //    //        left: e.pageX,
        //    //        top: e.pageY
        //    //    });
        //    //    return false;

        //    //});

        //    $contextMenu.on("click", "a", function (e) {
        //        e.preventDefault();
        //        $contextMenu.removeClass("contextOpened");
        //        $contextMenu.hide();
        //    });

        //    $('body').on('click', function () {
        //        $contextMenu.hide();
        //        $contextMenu.removeClass("contextOpened");
        //    });

        //    //newEvent(startDate, endDate);


        //},
        eventClick: function (arg) { //select event --EDIT
            $("#new-event--start").attr("disabled", true);
            $("#new-event--end").attr("disabled", false);

            var btnEliminar = document.getElementById("btnEliminar");
            if (!isNullOrEmpty(btnEliminar)) {
                
                document.getElementById("btnEliminar").style.display = "block";
            }
            document.getElementById("btnAgregar").style.display = "none";
            document.getElementById("btnActualizar").style.display = "block";
           

            var eventNameTitleCRUD = document.getElementById("eventNameTitleCRUD");
            eventNameTitleCRUD.innerHTML = "Edit";


            document.getElementById("new-event--id").value = arg.event._def.publicId;
            document.getElementById("new-event--title").value = arg.event._def.title //startStr fecha str
            document.getElementById("new-event--allDay").checked = arg.event.allDay
            document.getElementById("new-event--start-h").value = arg.event.startStr; //fecha str
            

            var startStrFormat = moment(arg.event.start).local().format('DD/MM/YYYY hh:mm:ss a');
            var endStrFormat = moment(arg.event.end).local().format('DD/MM/YYYY hh:mm:ss a');
            if (endStrFormat == 'Invalid date') {
                var fecha = new Date(arg.event.start);
                var dias = 1;
                fecha.setDate(fecha.getDate() + dias);
                endStrFormat = moment(fecha).local().format('DD/MM/YYYY hh:mm:ss a');
            }

            document.getElementById("new-event--start").value = startStrFormat;
            document.getElementById("new-event--end").value = endStrFormat;
            document.getElementById("new-event--description").value = arg.event._def.extendedProps.description;
            

            let colorValue = arg.event._def.ui.backgroundColor;
            //$("input[name=event-tag][value=" + colorValue + "]").prop('checked', true); MMFG

            //idEventType deve llegar como arg en lugar de calcularlo
            var eventType = document.getElementById("eventTypeList");
            var idEventType;
            for (var i = 0; i < eventType.length; i++) {
                var opt = eventType[i];
                
                if (opt.id == colorValue) {

                    idEventType = opt.value;
                }
            }

            eventType.value = idEventType;

            evalCalendarView();
            setModalEventType();
            evalCheckAllDay();

            $('#new-event').modal('show');


        },
        viewDidMount: function (event, element) {

            //evalCalendarView();

            
            //console.log(obj)
        },
        eventDidMount: function (event, element) {
            // Add icon before the title
            //setIconsHeader();
            //evalCalendarView();

        },
        //eventDidMount: function (info) {
        //    //console.log(info);
        //    if (info.view.type != 'timeGridDay') {
        //        console.log($(info.event.title));
        //        $(info.el).tooltip({ title: info.event.title });
        //    }
        //},
        dayCellDidMount: function (arg) {
            // Add icon before the title

            //setIconsHeader();
            //evalCalendarView();
            setCustomButtons();

            
            //if (arg.el.classList.contains("fc-day-future")) {
            //    var theElement = arg.el.querySelectorAll(".fc-daygrid-day-frame > .fc-daygrid-day-events")[0]
            //    setTimeout(function () {
            //        if (theElement.querySelectorAll(".fc-daygrid-event-harness").length === 0) { // check there's no event
            //            //theElement.innerHTML = theElement.innerHTML + '<div class="text-center"><i class="calendar-icon fas fa-plus"></i></span></div>';
            //        }
            //    }, 10)
            //}
            
        },
        dateClick: function (arg) {
            //console.log(arg.date.toUTCString()); // use *UTC* methods on the native Date Object
            //console.log(arg.date.toISOString());
            // will output something like 'Sat, 01 Sep 2018 00:00:00 GMT'
        },

        //textColor: 'white',

    });

    calendar.render();

    var calendarMini = new FullCalendar.Calendar(calendarMiniEl, {
        locale: 'es',
        timeZone: 'local', // the default (unnecessary to specify), // the default (unnecessary to specify)
        handleWindowResize: true,
        initialView: "dayGridMonth",
        weekends: true,
        editable: true,
        dayMaxEvents: true, // allow "more" link when too many events
        displayEventTime: false,
        nowIndicator: true,
        navLinks: false, // can click day/week names to navigate views
        selectMirror: true,
        selectable: true,
        //contentHeight: 600,
        headerToolbar: {
            left: 'title', 
            center: '',
            right: 'prev,today,next'
        },
        //defaultDate: '2015-02-12',
        buttonText: {
            today: 'hoy'
        },

        select: function (info) {
 
            syncCalendar(calendar, info.start);
        }
        
    });

    calendarMini.render();


    //$("#external-events").draggable({ handle: "header" });
    //$(".external-events").resizable();
    evalCumples();

    getCalendarAsync();

    //setCustomButtons();
    setCustomButtonsCalendarMini();
    setModalEventType();


    function openVentana() {
        url = "https://app.mural.co/t/ayllu6528/m/ayllu6528/1689787122035/ce7b2f691678e53e1c3433b8a37242a381615582?sender=u62f81034f0f69618b8640045";
        var width = 1200; // Ancho de la ventana emergente
        var height = 800; // Altura de la ventana emergente
        var left = (window.innerWidth - width) / 2; // Centra horizontalmente
        var top = (window.innerHeight - height) / 2; // Centra verticalmente
        var opciones = 'width=' + width + ',height=' + height + ',left=' + left + ',top=' + top;
        window.open(url, 'VentanaEmergente', opciones);
    }

    function syncCalendar(otherCalendar, startDate) {
        if (otherCalendar.getDate().toDateString() != startDate.toDateString()) {
            //console.log(otherCalendar);
            //otherCalendar.gotoDate(startDate);
            //otherCalendar.select();
        }
    }

    function onChangeMyTeamList() {

        var e = document.getElementById("myteamsList");
        var value = e.value;
        var text = e.options[e.selectedIndex].text;
        //document.getElementById("lblNombreSquad").innerHTML = text;

        evalCumples();

        evalCheckedTypes();

    }

    function viewFilter() {
        var pnl = document.getElementById("divFilter");
        pnl.style.display = pnl.style.display === 'none' ? '' : 'none';

       
        
    }

    function exportToExcel(type, fn, dl) {
        //fc-multimonth-header-table
        //fc-scrollgrid
        //fc-scrollgrid
        //fc-list-table
        var elt = document.getElementsByClassName("fc-list-table")[0];
        if (elt === undefined)
            elt = document.getElementsByClassName("fc-scrollgrid")[0];
        if (elt === undefined)
            elt = document.getElementsByClassName("fc-list-table")[0];

        //console.log(elt);

        if (elt !== undefined) {
            //console.log(elt);
            //var elt = document.getElementById('tbl_exporttable_to_xls');
            var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });
            return dl ?
                XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
                XLSX.writeFile(wb, fn || ('MySheetName.' + (type || 'xlsx')));
        }

    }

    function evalCumples() {
        var myteamsList = document.getElementById("myteamsList");
        var valueTeamsList = myteamsList.value;
        var textTeamsList = myteamsList.options[myteamsList.selectedIndex].text;


        //var div = document.getElementsByClassName("divContainerCumples");  

        var idCumples = "divCumple_" + valueTeamsList;
        //var divCumple = document.getElementById(idCumples).style.display = "none";


        var elm = {};
        var elms = document.getElementById("divContainerCumples").getElementsByTagName("div");


        //console.log(elms);
        for (var i = 0; i < elms.length; i++) {
            //console.log(elms[i].id);
            if (elms[i].id === idCumples) {
                elms[i].style.display = '';
                //break;
            }
            else {
                elms[i].style.display = 'none';
            }
        }


        //divAsientos.innerHTML = "<b>Tus asientos:</b> " + asientos.join(',');



    }
    function evalCheckedTypes() {
        var searchInput = document.getElementById("searchInput").value;

        var myteamsList = document.getElementById("myteamsList");
        var valueTeamsList = myteamsList.value;
        var textTeamsList = myteamsList.options[myteamsList.selectedIndex].text;

        var listaEventTpyes = $('input[type=checkbox][name="event-tag-chkEventType"]');
        var checkboxes = document.querySelectorAll('input[type=checkbox][name="event-tag-chkEventType"]')
        
        //console.log("arrayData", arrayData);

        var data_arr = calendar.getEvents();
        
       // console.log("getEvents",arrayData);

        calendar.getEvents().forEach(event => event.remove());

        for (var i = 0; i < checkboxes.length; i++) {
            var valor = checkboxes[i].value;
            var isChecked = checkboxes[i].checked;

            var name = checkboxes[i].id;
            //console.log("valueTeamsList", valueTeamsList);
            //console.log("data_arr", data_arr);
            //console.log("name/type", name);
            if (isChecked) {
                
                data_arr = arrayData.filter(function (x) {
                    return x.type == name
                        && x.calendarTypeId == valueTeamsList
                        && (x.title.toLowerCase().indexOf(searchInput.toLowerCase()) > -1
                        || x.description.toLowerCase().indexOf(searchInput.toLowerCase()) > -1)
                        //|| x.userName.toLowerCase().indexOf(searchInput.toLowerCase()) > -1 )
                });
                //console.log("data_arr", data_arr);
                calendar.batchRendering(() => {
                    data_arr.forEach(event => calendar.addEvent(event));
                });

            }


        }


    }


    function getCalendarAsync() {
        //console.log("aqui INICIO");
        calendar.getEvents().forEach(event => event.remove());

        var myteamsList = document.getElementById("myteamsList");
        var value = myteamsList.value;
        var text = myteamsList.options[myteamsList.selectedIndex].text;

        var valueConcat = '';
        for (var i = 0; i < myteamsList.length; i++) {
            valueConcat += myteamsList[i].value +",";
        }
        valueConcat = valueConcat.substring(0, valueConcat.length - 1);
        //console.log(valueConcat);

        var urlApi = host + "Calendar/GetCalendarAsync";
        $.ajax({
            type: 'GET',
            data: { 'idList': valueConcat },
            contentType: 'application/json; charset=utf-8',
            url: urlApi,
            headers: {
                'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9',
                'Content-Type': 'application/json'
            },
            success: function (data, status, xhr) {
                arrayData = []
                arrayData = arrayData.concat(data) ;
                //console.log('data Async: ', arrayData);

                // batch every modification into one re-render
                calendar.batchRendering(() => {
                    // remove all events
                    //calendar.getEvents().forEach(event => event.remove());
                    //add events from array concat
                    //console.log(arrayData);
                    arrayData.forEach(event => calendar.addEvent(event));

                    //$('input#event-tag-chk-holiday').prop('checked', false);
                    //$("input#event-tag-chk-holiday").removeAttr('checked');

                   

                });

                evalCheckedTypes(); // ya lo hace el change del combo


            },
            error: function (error) {
                console.log(error)
            }
        });
    }

    function postEventAdd(obj) {
        var urlApi = host + "Calendar/PostEventAddAsync";
        $.ajax({
            type: "POST",
            url: urlApi,
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            crossDomain: true,
            dataType: "json",
            success: function (data, status, jqXHR) {
                getCalendarAsync();
                ////console.log(data);
                //obj.id = data.id;
                //document.getElementById("new-event--id").value = data.id;

                //////MFG
                //var newstart = obj.start;
                //var newend = obj.end;

                //if (obj.allDay) {
                //    newstart = new Date(obj.start).addHours(5);
                //    newend = new Date(obj.end).addHours(5);

                //}

                //obj.start = newstart;
                //obj.end = newend;
                ////console.log(obj);
                
                ////console.log(calendar.getEvents());
                ////add to see in calendar
                //calendar.batchRendering(() => {
                //    calendar.addEvent(obj)

                //});
                ////MMFG
                ////getallAgain
                ////getCalendarAsync();


            },

            error: function (jqXHR, status) {
                // error handler
                console.log(jqXHR);
                alert('fail: ' + status.code);
            }
        });

    }

    function postEventUpd(obj) {
        //console.log(obj);

        var urlApi = host + "Calendar/PostEventUpdAsync";
        $.ajax({
            type: "POST",
            url: urlApi,
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            crossDomain: true,
            dataType: "json",
            success: function (data, status, jqXHR) {
                getCalendarAsync();
                //alert("success");// write success in " "


            },

            error: function (jqXHR, status) {
                // error handler
                console.log(jqXHR);
                alert('fail: ' + status.code);
            }
        });

    }

    function postEventDel(obj) {
        var urlApi = host + "Calendar/PostEventDelAsync";
        $.ajax({
            type: "POST",
            url: urlApi,
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            crossDomain: true,
            dataType: "json",
            success: function (data, status, jqXHR) {

                //alert("success");// write success in " "
            },

            error: function (jqXHR, status) {
                // error handler
                console.log(jqXHR);
                alert('fail: ' + status.code);
            }
        });

    }

    function evalCalendarView() {
        var type = calendar.currentData.currentViewType;
        
        var checkAllDay = document.getElementById("new-event--allDay");
        var labelAllDay = document.getElementById("label-new-event--allDay");

        var start = document.getElementById("new-event--start").value;
        var end = document.getElementById("new-event--end").value;

        var strfechaInicio = strToDate(start);
        var strfechaFin = strToDate(end);

        var fechaInicio = new Date(strfechaInicio);
        var fechaFin = new Date(strfechaFin);

        var diff = fechaFin - fechaInicio;
        var dias = diff / (1000 * 60 * 60 * 24);
       
        ////
        //console.log("dias", dias);
        //console.log("type", type);

        if (type == 'multiMonthYear' || type == 'dayGridYear' || type == 'dayGridMonth') {
            if (dias > 1) {
                //console.log("false", dias);
                checkAllDay.checked = false;
            }
            else {
                //console.log("true", dias);
                checkAllDay.checked = true;
            }
        }
        else if (type == 'timeGridWeek' || type == 'timeGridDay') {
            checkAllDay.checked = false;
           // labelAllDay.style.display = 'none';
            
        }



    }

    function sumarDias(myfecha, dias) {
        var fecha = new Date(myfecha);
        fecha.setDate(fecha.getDate() + dias);

        //var dd = formatDate(fecha);

        return fecha;
    }

    function restarDias(myfecha, dias) {
        var fecha = new Date(myfecha);
        fecha.setDate(fecha.getDate() - dias);

        //var dd = formatDate(fecha);

        return fecha;
    }

    function padLeft(n) {
        return ("00" + n).slice(-2);
    }

    function isNullOrEmpty(str) {
        var returnValue = false;
        if (!str
            || str == null
            || str === 'null'
            || str === ''
            || str === '{}'
            || str === 'undefined'
            || str === '-Select-'
            || str.length === 0) {
            returnValue = true;
        }
        return returnValue;
    }

    function setModalEventType() {
        
        var e = document.getElementById("eventTypeList");
        var idEventType = e.value;
        var nameEventType = e.options[e.selectedIndex].text;
        var eventColor = e.options[e.selectedIndex].id;

        //flagEvent
        var flag = document.getElementById("iconFlag");
        flag.style.color = eventColor;


        //modal
        var modal = document.getElementById("modal-content");
        //modal.style.borderWidth = "15px";
        //modal.style.borderBlockStyle = 'solid';
        //modal.style.borderColor = eventColor;



        //var username = document.getElementById("new-event--username").value;
        //document.getElementById("new-event--title").value = username; // + " - " + nameEventType; //username by default MFG

        //var evenTag = document.getElementById("event-tag");

        //var eventNameTitle = document.getElementById("eventNameTitle");
        //eventNameTitle.innerHTML = nameEventType;

       


    }

    function evalCheckAllDay() {

        var divEndDateNew = document.getElementById("divEndDateNew");
        var isChecked = document.getElementById("new-event--allDay").checked;

        if (isChecked == true) {
            divEndDateNew.style.display = 'none'

        }
        else {
            divEndDateNew.style.display = ''
        }
    }


    function setCustomButtons() {
        //var elements = document.getElementById("calendar").getElementsByClassName("fc-header-toolbar")[0].getElementsByClassName("fc-button-primary");
        var el = document.getElementById("calendar").getElementsByClassName("fc-button-primary");
        let ii;

        for (ii = 0; ii < el.length; ii++) {

            if (el[ii] instanceof HTMLElement) {
                el[ii].style.backgroundColor = "white";
                el[ii].style.color = "black";
                el[ii].style.borderColor = "white";
            }

        }


        var type = calendar.currentData.currentViewType;
        var elements = document.getElementById("calendar").getElementsByClassName("fc-header-toolbar")[0].getElementsByClassName("fc-button-primary");
        
        let i;
        for (i = 0; i < elements.length; i++) {

            if (elements[i] instanceof HTMLElement) {
                elements[i].style.borderColor = "white";
            }

        }


        if (type == 'multiMonthYear') {
            //elements[0].style.borderColor = 'black';
            elements[0].focus();
        }
        else if (type == 'dayGridYear') {
            //elements[1].style.borderColor = 'black';
            elements[1].focus();
        }
        else if (type == 'dayGridMonth') {
            //elements[1].style.borderColor = 'black';
            elements[1].focus();
        }

        else if (type == 'timeGridWeek') {
            //elements[2].style.borderColor = 'black';
            elements[2].focus();
        }
            
        else if (type == 'timeGridDay') {
            //elements[3].style.borderColor = 'black';
            elements[3].focus();
        }

        else if (type == 'listYear') {
            //elements[4].style.borderColor = 'black';
            elements[4].focus();
        }
        
        
    }

    function setCustomButtonsCalendarMini() {
        //var elements = document.getElementById("calendar").getElementsByClassName("fc-header-toolbar")[0].getElementsByClassName("fc-button-primary");
        var el = document.getElementById("calendarMini").getElementsByClassName("fc-button-primary");
        let ii;

        for (ii = 0; ii < el.length; ii++) {

            if (el[ii] instanceof HTMLElement) {
                el[ii].style.backgroundColor = "#ddd";
                el[ii].style.color = "black";
                el[ii].style.borderColor = "#ddd";
            }

        }

        

    }

    function setArrayEventData(arg) {
        eventData = arg
    }

    function strToDate(dtStr) {
        if (!dtStr) return null
        let dateParts = dtStr.split("/");
        //console.log(dateParts);

        let timeParts = dateParts[2].split(" ")[1].split(":");
        dateParts[2] = dateParts[2].split(" ")[0];

        var ampm = dtStr.split(' ')[2];

        var hh = timeParts[0];
        var mm = timeParts[1];
        if (ampm == 'pm' && hh < 12)
            hh = parseInt(timeParts[0]) + 12;

        //new Date( Date.parse("05/12/05 11:11:11") );
        //console.log(hh);
        // month is 0-based, that's why we need dataParts[1] - 1
        var dateObject = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0], hh, mm, timeParts[2]);
        //console.log(dateObject);
        var dateObj = new Date(dateObject);
        var ret = moment(dateObj).local().format('DD/MM/YYYY HH:mm:ss');


        var localTime = moment(dateObj).format('YYYY-MM-DD'); // store localTime
        var proposedDate = localTime + "THH:MM:00Z";
        proposedDate = proposedDate.replace("HH", hh).replace("MM", mm)

        return proposedDate; //ret;
    }

    $('#btnEliminar').click(function () {
        var id = document.getElementById("new-event--id").value;
        var event = calendar.getEventById(id);
        //console.log(event);

        if (confirm('Are you sure you want to delete this event?')) {
            event.remove(); //remove from calendar

            //arrayData.forEach((element, index) => {   //remove from initial array (concat)
            //    if (element.id == id) {
            //        arrayData.splice(index, 1);
            //        return;
            //    }
            //});

            ////call ajax
            var data =
            {
                Id: id
            };

            postEventDel(data);

            calendar.render();

        }

        
    });

    $('#btnAgregar').click(function () {

        var myteamsList = document.getElementById("myteamsList");
        var valueTeamsList = myteamsList.value;
        var textTeamsList = myteamsList.options[myteamsList.selectedIndex].text;


        //var start_format = document.getElementById("new-event--start").value;
        var dateEnd_picker = document.getElementById("new-event--end").value;
        var dateEndOld = document.getElementById("new-event--end-h").value // arg.event.endStr //startStr fecha str

        //var start = document.getElementById("new-event--start-h").value;
        //var end = document.getElementById("new-event--end-h").value;
        var allDay = document.getElementById("new-event--allDay");

        var title = document.getElementById("new-event--title").value;
        var description = document.getElementById("new-event--description").value;

        var eventType = document.getElementById("eventTypeList");
        var idEventType = eventType.value;
        var nameEventType = eventType.options[eventType.selectedIndex].text;
        var colorEventType = eventType.options[eventType.selectedIndex].id;



        ////MFG
        var newstart = "";
        var newend = "";


        //var startStrFormat = moment(arg.start).local().format('DD/MM/YYYY hh:mm:ss a');
        var endArgFormat = new moment(dateEnd_picker).local().format('DD/MM/YYYY hh:mm:ss a');
        var end_format_date = new moment(eventData.end).local().format('DD/MM/YYYY hh:mm:ss a');
        //var end_format_date = end_format_date.toISOString();

        newstart = new Date(eventData.start).addHours(-5);
        newend = new Date(eventData.end).addHours(-5);

        //console.log("dateEnd_picker",dateEnd_picker);
        var type = calendar.currentData.currentViewType;
        if (type == 'multiMonthYear' || type == 'dayGridYear' || type == 'dayGridMonth') {

            var strdateEnd_picker = strToDate(dateEnd_picker);
            var strddateEndOld = strToDate(dateEndOld);
            if (strdateEnd_picker != strddateEndOld) {
                var newdateEnd_picker = new Date(strdateEnd_picker);
                newend = new Date(eventData.end);
                newend.setDate(newdateEnd_picker.getDate() + 1); //sumar 1 dia solo cuando es manual; controlar cuando es manual
                //console.log("diff");
                //console.log("strddateEndOld");
            }
            else {
                //console.log("same");
               // console.log("strddateEndOld");
            }
            //console.log("strdateEnd_picker", strdateEnd_picker);
            //console.log("strddateEndOld", strddateEndOld);

           
        }
        

        eventData.id = 0;
        eventData.title = title;
        eventData.start = newstart;
        eventData.end = newend;
        //eventData.allDay = allDay.checked;
        eventData.description = description;
        eventData.color = colorEventType;
        eventData.EventTypeId = idEventType;
        eventData.type = nameEventType;
        eventData.CalendarTypeId = valueTeamsList;
        eventData.CalendarTypeName = textTeamsList;
        eventData.UserCreate = '';
        eventData.UserName = '';


        //console.log("eventData",eventData);
        //arrayData.push(eventData);

        postEventAdd(eventData);
        
       
    });
 
    $('#btnActualizar').click(function () {

        var myteamsList = document.getElementById("myteamsList");
        var valueTeamsList = myteamsList.value;
        var textTeamsList = myteamsList.options[myteamsList.selectedIndex].text;

        var id = document.getElementById("new-event--id").value;
        var event = calendar.getEventById(id);
        //console.log(event);
        var title = document.getElementById("new-event--title").value;
        var description = document.getElementById("new-event--description").value;
        var allDay = document.getElementById("new-event--allDay").checked;
        //var allDayBoolean = (allDay === 'true');

       
        var eventType = document.getElementById("eventTypeList");
        var idEventType = eventType.value;
        var nameEventType = eventType.options[eventType.selectedIndex].text;
        var colorEventType = eventType.options[eventType.selectedIndex].id;


        var start = document.getElementById("new-event--start").value;
        var end = document.getElementById("new-event--end").value;

        var start_format_date = strToDate(start);
        var end_format_date = strToDate(end);

        if (start_format_date > end_format_date) {
            alert("Invalid range");
            return false;
        }
        if (start_format_date == end_format_date) {
            alert("The dates are the same");
            return false;
        }

        //newstart = new Date(eventData.start).addHours(-5);
        //newend = new Date(eventData.end).addHours(-5);


        event.setProp('title', title);
        event.setProp('color', colorEventType)
        //event.setStart(start_format_date);
        //event.setEnd(end_format_date);
        event.setExtendedProp('description', description)
        //event.setProp('allDay', allDayBoolean); //el allday viene del calendar no del check, el check es solo para abrir o  cerrar date end


        //var type = evalTypeColor(eventColor);

        var type = calendar.currentData.currentViewType;
        if (type == 'multiMonthYear' || type == 'dayGridYear' || type == 'dayGridMonth') {
            allDay = true;
        }

        var data =
        {
            id: id,
            title: title,
            start: start_format_date,
            end: end_format_date,
            allDay: allDay,
            description: description,
            color: colorEventType,
            EventTypeId: idEventType,
            type: nameEventType,
            CalendarTypeId: valueTeamsList,
            CalendarTypeName: textTeamsList,
            UserCreate: '',
            UserName: ''

        };



        //intento MMFG
        for (var i = 0; i < arrayData.length; i++) {
            if (arrayData[i].id == data.id) {
                arrayData[i].title = data.title;
                arrayData[i].start = data.start;
                arrayData[i].end = data.end;
                arrayData[i].allDay = data.allDay;
                arrayData[i].color = data.color;
                arrayData[i].type = data.type;
                arrayData[i].description = data.description;
                arrayData[i].eventTypeId = data.EventTypeId;
            }
        }

        postEventUpd(data);


    });


    $("#new-event").on('shown.bs.modal', function () {
        $(this).find('#new-event--title').focus();

    });

    $('#datetimepicker1').datetimepicker({
        format: 'DD/MM/YYYY hh:mm:ss a',

    });

    $('#datetimepicker2').datetimepicker({
        format: 'DD/MM/YYYY hh:mm:ss a',
        //minDate: new Date(),

    });

 
    
    $('input[type=checkbox][name="event-tag-chkEventType"]').change(function () {
        var isChecked = $(this).is(':checked');
        evalCheckedTypes();
    });

    $('input[type=checkbox][name="new-event--allDay"]').change(function () {
        
        evalCheckAllDay();
    });

    Date.prototype.addHours = function (h) {
        this.setHours(this.getHours() + h);
        return this;
    }

    $(document).ready(function () {

        //getEventTypes();



    });

    $("#searchInput").keypress(function (e) {
        if (e.which == 13) {
            evalCheckedTypes();
        }
    });

    $("#btnSearchFilter").click(function (e) {
        evalCheckedTypes();
    });
    

    //$("#searchInput").keyup(function () {
    //    //var source = storeCalendar.getEvents();
    //    var input = this.value;
    //    //var newSource = source.filter(elem => (elem.title.toLowerCase().indexOf(input.toLowerCase()) > -1));
    //    refreshCalendar(input);




    //});


    //var events = storeCalendar.getEvents()
    //var data_arr = []
    //events.forEach((item, index) => {
    //    data_arr.push({
    //        'title': item.title,
    //        'start': item.start,
    //        'end': item.end,
    //        'description': item.description,
    //    })
    //})



   

   
});

