

var Helper = {
    AjaxCall: function (url, paramters, callingType, returnFormat, success, falier) {
        $.ajax({
            url: url,
            data: paramters,
            dataType: returnFormat,
            success: function (data) {
                success(data);
                return data;
            },
            error: function () {
                falier();
                $("#info").html("<p>An error has occurred</p>");
                return 0;
            },
            type: callingType
        });
    },
    AjaxCallPost: function (url, paramters, success, falier) {
        $.ajax({
            url: url,
            data: paramters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "post",
            success: function (data) {
                success(data);
                return data;
            },
            error: function (xhr, err) {
                //alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                //alert("responseText: " + xhr.responseText);
                // falier();
                $("#info").html("<p>An error has occurred</p>");
                return 0;
            },
            type: "POST"
        });
    },
    AjaxCallPut: function (url, paramters, success, falier) {
        $.ajax({
            url: url,
            data: paramters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                success(data);
                return data;
            },
            error: function (xhr, err) {
                //alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                //alert("responseText: " + xhr.responseText);
                falier();
                $("#info").html("<p>An error has occurred</p>");
                return 0;
            },
            type: "PUT"
        });
    },
    AjaxCallGet: function (url, paramters, returnFormat, success, falier) {
        $.ajax({
            url: url,
            data: paramters,
            contentType: "application/json; charset=utf-8",
            dataType: returnFormat,
            success: function (data) {
                success(data);
                return data;
            },
            //error: function () {
            //    falier();
            //    $('#info').html('<p>An error has occurred</p>');
            //    return 0;
            //},
            error: function (xhr, status, error) {
                var errorMessage = xhr.status + ": " + xhr.statusText + ": " + xhr.responseText + ": ";
                console.log("Error - " + errorMessage);
            },
            type: "GET",

        });
    },
    AjaxCallDelete: function (url, paramters, returnFormat, success, falier) {
        $.ajax({
            url: url,
            data: paramters,
            contentType: "application/json; charset=utf-8",
            dataType: returnFormat,
            success: function (data) {
                success(data);
                return true;
            },
            //error: function () {
            //    falier();
            //    $('#info').html('<p>An error has occurred</p>');
            //    return 0;
            //},
            error: function (xhr, status, error) {
                var errorMessage = xhr.status + ": " + xhr.statusText + ": " + xhr.responseText + ": ";
                console.log("Error - " + errorMessage);
                return false;
            },
            type: "Delete",

        });
    },
    AjaxCallGetSync: function (url, paramters, returnFormat, success, falier) {
        $.ajax({
            url: url,
            data: paramters,
            contentType: "application/json; charset=utf-8",
            dataType: returnFormat,
            success: function (data) {
                success(data);
                return data;
            },
            error: function () {
                falier();
                $("#info").html("<p>An error has occurred</p>");
                return 0;
            },
            type: "GET",
            async: false
        });
    },
    ClosePopUp: function () {
        parent.jQuery.colorbox.close();
    },
    CallingServer: function (expretion, data) {
        __doPostBack("__Page", expretion + "_" + data);
    },
    BootStrapModal(modalId) {
        $("#" + modalId + "").modal({
            show: true,
            backdrop: "static",
            keyboard: true
        });
    },
    GetQueryString: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf("?") + 1).split("&");
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split("=");
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    SearchQueryString: function (str) {
        console.log(str);
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf(str) + str.length + 1).split("/");
        return hashes;
    },


    SearchNewQueryString: function (myParam) {
        const urlParams = new URLSearchParams(window.location.search);
        const retuVal = urlParams.get(myParam);
        return retuVal;

    },
    GetFormattedDate: function (date) {
        var msec = Date.parse(date);
        var d = new Date(msec);
        var curr_date = d.getDate();
        var curr_month = d.getMonth();
        curr_month++;
        var curr_year = d.getFullYear();
        var newDate = curr_date + "/" + curr_month + "/" + curr_year;
        return newDate;
    },
    GetDaysMonthDate: function (date) {
        var msec = Date.parse(date);
        var d = new Date(msec);
        var curr_date = d.getDate();
        var curr_month = d.getMonth();
        curr_month++;
        var curr_year = d.getFullYear();
        var newDate = {};
        newDate.days = curr_date;
        newDate.month = curr_month;
        newDate.year = curr_year;
        return newDate;
    },
    GetmonthDate: function (date) {
        var mydate = this.GetDaysMonthDate(date);
        switch (mydate.month) {
            case 1:
                mydate.month = "Jan"; break;
            case 2:
                mydate.month = "Feb"; break;
            case 3:
                mydate.month = "Mar"; break;
            case 4:
                mydate.month = "Apr"; break;
            case 5:
                mydate.month = "May"; break;
            case 6:
                mydate.month = "Jun"; break;
            case 7:
                mydate.month = "Jul"; break;
            case 8:
                mydate.month = "Aug"; break;
            case 9:
                mydate.month = "Sep"; break;
            case 10:
                mydate.month = "Oct"; break;
            case 11:
                mydate.month = "Nov"; break;
            case 12:
                mydate.month = "Dec"; break;
        }

        return mydate;
    },
    Getvideourl: function (videoUrl) {
        var videoId = videoUrl.substring(videoUrl.lastIndexOf("/") + 1);
        return "https://www.youtube.com/watch?v=" + videoId;
    },
    getUrlParameter: function (sParam) {
        var sPageURL = window.location.search.substring(1),
            sURLVariables = sPageURL.split("&"),
            sParameterName,
            i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split("=");

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
            }
        }
    },
    getScript: function (source, callback) {
        var script = document.createElement("script");
        var prior = document.getElementsByTagName("script")[0];
        script.async = 1;

        script.onload = script.onreadystatechange = function (_, isAbort) {
            if (isAbort || !script.readyState || /loaded|complete/.test(script.readyState)) {
                script.onload = script.onreadystatechange = null;
                script = undefined;

                if (!isAbort && callback) setTimeout(callback, 0);
            }
        };

        script.src = source;
        prior.parentNode.insertBefore(script, prior);
    },
    waitForEl: function (selector, callback) {
        if ($(selector).length) {
            callback;
        } else {
            setTimeout(function () {
                Helper.waitForEl(selector, callback);
            }, 100);
        }
    },
    ChangeMetaContent: function (metaName, newMetaContent) {
        $("meta").each(function () {
            if ($(this).attr("name") == metaName) {
                $(this).remove();
                $("head").append('<meta name="' + metaName + '" content="' + newMetaContent + '">');
                //   $(this).attr("content", newMetaContent);
            }
        });
    },
    groupBy: function (collection, property) {
        var val, index,
            values = [], result = [];
        for (var i = 0; i < collection.length; i++) {
            val = collection[i][property];
            index = values.indexOf(val);
            if (index > -1)
                result[index].push(collection[i]);
            else {
                values.push(val);
                result.push([collection[i]]);
            }
        }
        return result;
    }
};

var Message = {
    ShowDescription: function (description, title) {
        $("#lbDescription").empty();
        $("#exampleModalLongTitle").empty();
        $("#lbDescription").append(description);
        $("#exampleModalLongTitle").append(title);
        Helper.BootStrapModal("DescriptionPopup");
    },
    ShowPopup: function (title, data) {
        $.confirm({
            title: title,
            content: "",
            onContentReady: function () {
                var self = this;
                this.setContentPrepend(data);
            },
            columnClass: "medium",
            buttons: {
                cancelAction: {
                    btnClass: "btn-dark",
                    text: Resources.lbClose,
                    function() {
                    }
                }
            }
        });
    },
    ShowNotification: function (message, errorType) {
        Lobibox.notify(errorType, {
            size: "normal",
            closeButton: false,                  // Show close button or not
            rounded: false,
            delayIndicator: true,
            position: "top right",
            msg: message

        });
    },
    ShowConfirm: function (title, message, errorType) {
        Lobibox.alert(errorType,
            {
                msg: message,
                title: title
            });
    },
    TakeConfirmation: function (text, txtCancel, messageHeader, messageBody, url) {
        $.confirm({
            icon: "fa fa-warning",
            animation: "Rotate",
            closeAnimation: "Rotate",
            animationBounce: 1.5,
            title: messageHeader,
            content: messageBody,
            autoClose: "cancelAction|8000",
            buttons: {
                deleteUser: {
                    text: text,
                    btnClass: "btn-danger",
                    action: function () {
                        window.location = url;
                    }
                },
                cancelAction: {
                    btnClass: "btn-dark",
                    text: txtCancel,
                    function() {
                    }
                }
            }
        });
    },
    fullScreenConfirm: function (txtConfirm, txtCancel, messageHeader, messageBody, url) {
        $.confirm({
            icon: "fa fa-warning",
            theme: "supervan",
            title: messageHeader,
            content: messageBody,
            buttons: {
                confirm: {
                    btnClass: " btn-purple",
                    text: txtConfirm,
                    action: function () {
                        window.location = url;
                    }
                },
                cancelAction: {
                    text: txtCancel,
                    function() {
                    }
                }
            }
        });
    },
};



var HelperDate = {
    date_diff_indays: function (date1, date2) {
        dt1 = new Date(date1);
        dt2 = new Date(date2);
        return Math.floor((Date.UTC(dt2.getFullYear(), dt2.getMonth(), dt2.getDate()) - Date.UTC(dt1.getFullYear(), dt1.getMonth(), dt1.getDate())) / (1000 * 60 * 60 * 24));
    },

    getStringDateTime: function (dateString) {
        year = dateString.slice(0, 4);
        month = dateString.slice(5, 7);
        day = dateString.slice(8, 10);
        hour = parseInt(dateString.slice(11, 13));
        minites = dateString.slice(14, 16);
        period = 'AM';
        if (hour >= 12) {
            hour -= 12;
            period = 'PM';
        }
        if (hour == 0) {
            hour = 12;
        }

        return `${day}/${month.toString()}/${year} ${hour.toString()}:${minites} ${period}`;
    },

    DaysCountChanged: function (days) {
        if (days != '') {
            var startDateString = $("#start-date").val();
            if (startDateString.length < 11) {
                startDateString += ' - 12:00 AM';
            }
            var utc = this.getUTCDate(startDateString).toISOString();
            var newDate = new Date(utc);
            newDate.setDate(newDate.getDate() + parseInt(days));
            $("#end-date").val(this.ISODateToString(newDate.toISOString()));
        }
        else {
            $("#end-date").val($("#start-date").val());
        }
    },

    ISODateToString: function (dateString) {
        year = dateString.slice(0, 4);
        month = dateString.slice(5, 7);
        day = dateString.slice(8, 10);
        hour = parseInt(dateString.slice(11, 13));
        minites = dateString.slice(14, 16);
        period = 'AM';
        if (hour >= 12) {
            period = 'PM';
            hour -= 12;
        }
        if (hour.length == 1) {
            hour = '0' + hour;
        }

        return `${year}-${month}-${day} - ${hour}:${minites} ${period}`;
    },

    getUTCDate: function (dateString) {
        year = dateString.slice(0, 4);
        month = parseInt(dateString.slice(5, 7)) - 1;
        day = dateString.slice(8, 10);
        hour = parseInt(dateString.slice(13, 15));
        minites = dateString.slice(16, 18);
        period = dateString.slice(19, 21);
        if (period == 'PM')
            hour += 12;

        return new Date(Date.UTC(year, month, day, hour, minites, 0, 0));//.toUTCString();
    },

    stringToDate: function (dateString) {
        year = dateString.slice(0, 4);
        month = parseInt(dateString.slice(5, 7)) - 1;
        day = dateString.slice(8, 10);
        hour = parseInt(dateString.slice(13, 15));
        minites = dateString.slice(16, 18);
        period = dateString.slice(19, 21);
        if (period == 'PM')
            hour += 12;

        return new Date(year, month, day, hour, minites, 0);
    },

    ISOToDate: function (dateString) {
        year = dateString.slice(0, 4);
        month = parseInt(dateString.slice(5, 7));
        day = dateString.slice(8, 10);
        hour = parseInt(dateString.slice(11, 13));
        minites = dateString.slice(14, 16);


        return new Date(year, month, day, hour, minites, 0);
    },

    utc_date_diff_indays: function (date1, date2) {
        dt1 = this.getUTCDate(date1);
        dt2 = this.getUTCDate(date2);
        return Math.floor((Date.UTC(dt2.getFullYear(), dt2.getMonth(), dt2.getDate()) - Date.UTC(dt1.getFullYear(), dt1.getMonth(), dt1.getDate())) / (1000 * 60 * 60 * 24));
    },

    DateChanged: function () {
        var startDate = document.getElementById("start-date").value;
        var endDate = document.getElementById("end-date").value;
        if (startDate.length < 11) {
            startDate += ' - 12:00 AM';
        }
        if (endDate.length < 11) {
            endDate += ' - 12:00 AM';
        }
        document.getElementById("taskDays").value = HelperDate. utc_date_diff_indays(startDate, endDate);
    },


}
var Generally = {
    SuccessAlert: function (title, text) {
        setTimeout(function () {
            Swal.fire({
                title: title,
                text: text,
                confirmButtonText: lang == "en" ? "ok" : "حسناً",
                icon: "success"
            }).then((result) => {
            });
        }, 500);
    },

    FailureAlert: function (title, text) {
        setTimeout(function () {
            Swal.fire({
                title: title,
                text: text,
                confirmButtonText: lang == "en" ? "ok" : "حسناً",
                icon: "error"
            }).then((result) => {
            });
        }, 500);
    },
    // JQuery Data table without buttons (print - PDF - Exel - ......)
    DataTable: function (id, title) {
        var lbSearch = "",
            lbLengthMenu = "",
            lbInfoEmpty = "",
            lbInfoFiltered = "",
            lbZeroRecords = "",
            lbInfo = "",
            lbPrevious = "",
            lbNext = "",
            lbFirst = "",
            lbLast = "",
            lbCopy = "",
            lbExcel = "اكسل",
            lbPdf = "بي دي اف",
            lbPrint = "طباعة",
            lbColvis = "فرز حسب";

        if (lang == "en") {
            lbSearch = "Search :";
            lbLengthMenu = `view _MENU_ ${title} for page`;
            lbInfoEmpty = "No data available ";
            lbInfoFiltered = `(Filter from _MAX_total of ${title}) `;
            lbZeroRecords = "Sorry, no data to display";
            lbInfo = "view page _PAGE_ from _PAGES_";
            lbPrevious = "Previous";
            lbNext = "Next";
            lbFirst = "First";
            lbLast = "Last";
            lbCopy = "Copy";
            lbExcel = "Excel";
            lbPdf = "PDF";
            lbPrint = "Print";
            lbColvis = "Sort by";
        }
        else {
            lbSearch = "بحث :";
            lbLengthMenu = `عرض _MENU_ ${title} لكل صفحة`;
            lbInfoEmpty = "لا توجد بيانات متاحة";
            lbInfoFiltered = `(تصفية من  _MAX_ اجمالي ${title})`;
            lbZeroRecords = "عفوا لا توجد بيانات للعرض";
            lbInfo = "عرض الصفحة _PAGE_ من _PAGES_";
            lbPrevious = "السابق";
            lbNext = "التالي";
            lbFirst = "الاول";
            lbLast = "الاخير";
            lbCopy = "نسخ";
            lbExcel = "اكسل";
            lbPdf = "بي دي اف";
            lbPrint = "طباعة";
            lbColvis = "فرز حسب";
        }

        $(`#${id}`).DataTable(
            {
                buttons: ["copy", "csv", "excel", "pdf", "print", "colvis"],
                "responsive": true,
                "lengthChange": true,
                "autoWidth": false,
                "jQueryUI": true,
                "ordering": false,
                "paging": true,
                "serach": true,
                "language": {
                    "lengthMenu": lbLengthMenu,
                    "zeroRecords": lbZeroRecords,
                    "info": lbInfo,
                    "infoEmpty": lbInfoEmpty,
                    "infoFiltered": lbInfoFiltered,
                    "search": lbSearch,
                    "oPaginate": {
                        "sPrevious": lbPrevious,
                        "sNext": lbNext,
                        "sFirst": lbFirst,
                        "sLast": lbLast,
                        "copy": lbCopy
                    },
                    buttons: {
                        "copy": lbCopy,
                        "csv": "CSV",
                        "excel": lbExcel,
                        "pdf": lbPdf,
                        "print": lbPrint,
                        "colvis": lbColvis
                    }
                }
            }
        );

    },
    // JQuery Data table with buttons (print - PDF - Exel - ......)
    DataTableButtons: function (id, title) {
        var lbSearch = "",
            lbLengthMenu = "",
            lbInfoEmpty = "",
            lbInfoFiltered = "",
            lbZeroRecords = "",
            lbInfo = "",
            lbPrevious = "",
            lbNext = "",
            lbFirst = "",
            lbLast = "",
            lbCopy = "",
            lbExcel = "اكسل",
            lbPdf = "بي دي اف",
            lbPrint = "طباعة",
            lbColvis = "فرز حسب";

        if (lang == "en") {
            lbSearch = "Search :";
            lbLengthMenu = `view _MENU_ ${title} for page`;
            lbInfoEmpty = "No data available ";
            lbInfoFiltered = `(Filter from _MAX_total of ${title}) `;
            lbZeroRecords = "Sorry, no data to display";
            lbInfo = "view page _PAGE_ from _PAGES_";
            lbPrevious = "Previous";
            lbNext = "Next";
            lbFirst = "First";
            lbLast = "Last";
            lbCopy = "Copy";
            lbExcel = "Excel";
            lbPdf = "PDF";
            lbPrint = "Print";
            lbColvis = "Sort by";
        }
        else {
            lbSearch = "بحث :";
            lbLengthMenu = `عرض _MENU_ ${title} لكل صفحة`;
            lbInfoEmpty = "لا توجد بيانات متاحة";
            lbInfoFiltered = `(تصفية من  _MAX_ اجمالي ${title})`;
            lbZeroRecords = "عفوا لا توجد بيانات للعرض";
            lbInfo = "عرض الصفحة _PAGE_ من _PAGES_";
            lbPrevious = "السابق";
            lbNext = "التالي";
            lbFirst = "الاول";
            lbLast = "الاخير";
            lbCopy = "نسخ";
            lbExcel = "اكسل";
            lbPdf = "بي دي اف";
            lbPrint = "طباعة";
            lbColvis = "فرز حسب";
        }

        $(`#${id}`).DataTable(
            {
                "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
                "responsive": true,
                "lengthChange": true,
                "autoWidth": false,
                "jQueryUI": true,
                "ordering": false,
                "paging": true,
                "serach": true,
                "language": {
                    "lengthMenu": lbLengthMenu,
                    "zeroRecords": lbZeroRecords,
                    "info": lbInfo,
                    "infoEmpty": lbInfoEmpty,
                    "infoFiltered": lbInfoFiltered,
                    "search": lbSearch,
                    "oPaginate": {
                        "sPrevious": lbPrevious,
                        "sNext": lbNext,
                        "sFirst": lbFirst,
                        "sLast": lbLast,
                        "copy": lbCopy
                    },
                    buttons: {
                        "copy": lbCopy,
                        "csv": "CSV",
                        "excel": lbExcel,
                        "pdf": lbPdf,
                        "print": lbPrint,
                        "colvis": lbColvis
                    }
                }
            }
        ).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');

    }

}

