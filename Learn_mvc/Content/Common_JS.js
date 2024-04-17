function Ajax_call(type, url, data) {
    var msg = "";
    var response = "";
    $.ajax({
        type: type,
        url: url,
        data: data,
        async: false,
        success: function (res) {
            msg = "success";
            response = res;
        },
        error: function (err) {
            msg = "error";
            response = err;
        }
    });

    //$.ajax({
    //    type: "POST",
    //    url: $(this).attr('href'),
    //    data: '',
    //    dataType: "json",
    //    contentType: "application/json; charset=utf-8",
    //    success: function (res) {

    //    },
    //    error: function (err) {
    //        alert(JSON.stringify(err));
    //    }
    //});

    return { msg, response };
}

function Delete_student(type, url, data, e) {
    e.preventDefault();

    if (confirm("Are you sure you wish to delete this article?")) {

        var response = Ajax_call(type, url, data);
        if (response.msg === "success" ) {
            $("#container_load_student_list_partial").html(response.response);
        }
        else {
            alert(JSON.stringify(response.response));
        }
    }
}

function Loading() {
    bootbox.dialog({ message: '<div class="text-center"><i class="fa fa-refresh fa-spin" style="color:green;"></i> Loading...</div>' })
}

function Hide() {
    bootbox.hideAll();
}

function Confirm(title, msg) {
    //var res = "";
    //bootbox.confirm({
    //    title: title,
    //    message: msg,
    //    buttons: {
    //        cancel: {
    //            label: '<i class="fa fa-times"></i> Cancel'
    //        },
    //        confirm: {
    //            label: '<i class="fa fa-check"></i> Confirm'
    //        }
    //    },
    //    callback: function (result) {
    //        res = result;
    //    }
    //});
    //return { res };

    return new Promise((resolve, reject) => {
        bootbox.confirm({
            title: title,
            message: msg,
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> Cancel'
                },
                confirm: {
                    label: '<i class="fa fa-check"></i> Confirm'
                }
            },
            callback: function (result) {
                resolve(result);
            }
        });
    });
}