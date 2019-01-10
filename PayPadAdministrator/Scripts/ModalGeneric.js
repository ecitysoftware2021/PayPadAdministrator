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
