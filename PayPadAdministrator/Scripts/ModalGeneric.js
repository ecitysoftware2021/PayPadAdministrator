function ShowModalDelete(message, data, url) {
    swal({
        title: "¿Estás seguro?",
        text: message,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Eliminar",
        closeOnConfirm: false
    },
        function () {
            DeleteItem(data, url);
        });
}



function DeleteItem(data, url) {
    $.ajax({
        type: 'POST',
        url: url,
        dataType: 'json',
        data: { data: data },
        success: function (data) {
            if (data.CodeError == 200) {
                MessageSuccess("Buen trabajo!", data.Message);
            } else {
                ModalMessageError("Error", data.Message);
            }
        },
        error: function (ex) {
            ModalMessageError("Error", 'Failed ' + ex);
        }
    });
}




function MessageInfo(title, message) {
    swal({
        title: title,
        text: message,
        type: "info",
        showCancelButton: false,
        confirmButtonColor: "#DD6B55",
        closeOnConfirm: false,
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            CloseModal();
        }
    });
}

function MessageSuccess(title, message) {
    swal({
        title: title,
        text: message,
        type: "success",
        showCancelButton: false,
        confirmButtonColor: "#DD6B55",
        closeOnConfirm: false,
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            CloseModal();
        }
    });
}

function ModalMessageError(title, description) {
    swal({
        title: title,
        text: description,
        type: 'error',
        showCancelButton: false,
        confirmButtonColor: "#DD6B55",
        closeOnConfirm: false,
        closeOnConfirm: false,
        closeOnCancel: false
    });
}

function ModalMessageSuccess(title, description) {
    swal({
        title: title,
        text: description,
        type: 'success',
        showCancelButton: false,
        confirmButtonColor: "#DD6B55",
        closeOnConfirm: false,
        closeOnConfirm: false,
        closeOnCancel: false
    });
}

function ModalMessageWarning(title, description) {
    swal({
        title: title,
        text: description,
        type: 'warning',
        showCancelButton: false,
        confirmButtonColor: "#DD6B55",
        closeOnConfirm: false,
        closeOnConfirm: false,
        closeOnCancel: false
    });
}

function CloseModal() {
    var href = location.href;
    location.href = href;
}
