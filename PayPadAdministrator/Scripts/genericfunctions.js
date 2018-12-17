$(function () {

    $("#valid input").each(function () {    	
        if ($(this).attr("data-type") == "number") {
            $(this).on("keypress", function () {
                if (!soloNumeros(event)) {
                    return false;
                }
            })
        }

        if ($(this).attr("data-type") == "money") {
            $(this).on("keypress", function () {
                if (!soloNumeros(event)) {
                    return false;
                }
                $(this).blur(function () {
                    var valor = parseFloat($(this).val().replace("$ ", "").replace("$", "").replace(",", "").replace(".00", ""));
                    $(this).val("$ " + valor.formatMoney(2, '.', ','));

                })
            })
        }



        if ($(this).attr("data-type") == "text") {
            $(this).on("keypress", function () {

                if (!soloLetras(event)) {
                    return false;
                }
            })
        }

        if ($(this).attr("data-type") == "TextNumber") {
            $(this).on("keypress", function () {

                if (!soloLetrasNumeros(event)) {
                    return false;
                }
            })
        }

        if ($(this).attr("data-type") == "Unidades") {
            $(this).on("keypress", function () {
                if (!validarUnidades(event)) {
                    return false;
                }
            })
        }

        if ($(this).attr("data-type") == "TextCaracter") {
            $(this).on("keypress", function () {
                if (!textocaracter(event)) {
                    return false;
                }
            })
        }

        if ($(this).attr("data-type") == "number") {
            $(this).on("keypress", function () {
                if (!validarnumeros(event)) {
                    return false;
                }
            })
        }


        $(this).on("click", function () {
            $("#txtCorreoError").css("display", "none");
        })


        $(this).on("click", function () {
            $("#" + $(this).attr("id") + "Error").css("display", "none");

        })

        if ($(this).attr("data-type") == "email") {
            $(this).on("blur", function () {
                if (!validarEmail($(this).val())) {
                    $("#" + $(this).attr("id") + "Error").html("Correo Invalido");
                    $("#" + $(this).attr("id") + "Error").css("display", "block");
                    $("#" + $(this).attr("id") + "div").removeClass('has-success');
                    $("#" + $(this).attr("id") + "div").addClass('has-error');

                } else {
                    $("#" + $(this).attr("id") + "Error").css("display", "none");
                    $("#" + $(this).attr("id") + "div").removeClass('has-error');
                    $("#" + $(this).attr("id") + "div").addClass('has-success');
                }
            })

            $(this).on("click", function () {
                $("#txtCorreoError").css("display", "none");
            })
        }

    })
});

Number.prototype.formatMoney = function (c, d, t) {
    var n = this, c = isNaN(c = Math.abs(c)) ? 2 : c, d = d == undefined ? "," : d, t = t == undefined ? "." : t, s = n < 0 ? "-" : "",
        i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "", j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t)
        + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

function validarEmail(email) {
    expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!expr.test(email)) {
        return false;
    } else {
        return true;
    }
}



function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
    especiales = "8-37-39-46";
    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    } else {
        return true;
    }
}

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    return ((key >= 48 && key <= 57) || (key == 8))
}

function validarnumeros(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "1234567890";
    especiales = "8-37-39-46";
    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    } else {
        return true;
    }
}


function validarUnidades(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "1234567890,;.:-_{[^}]+´¨?'\¡¿!|°¬~+/&*%$#=()";
    especiales = "8-37-39-46";
    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    } else {
        return true;
    }
}

function textocaracter(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúabcdefghijklmnñopqrstuvwxyz,;.:-_{[^}]+´¨?'\¡¿!|°¬~+/&*%$#=()";
    especiales = "8-37-39-46";
    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    } else {
        return true;
    }
}


function soloLetrasNumeros(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúabcdefghijklmnñopqrstuvwxyz1234567890";
    especiales = "8-37-39-46";
    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    } else {
        return true;
    }
}


function Caracters(e) {
    var regex = new RegExp("ñÑáéíóúÁÉÍÓÚa-zA-Z0-9\u00BF\u0021\u0022\u0023\u0024\u0025\u0026\u0027\u0028\u0029\u002A\u002B\u002C\u002D\u002E\u002F\u003A\u003B\u003C\u003D\u003E\u003F\u0040\u005B\u005C\u005D\u005E\u005F\u0060\u007B\u007C\u007D\u007E (\r\n|\r|\n)");
    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (regex.test(str)) {
        return true;
    }
    e.preventDefault();
    return false;
}

function ValidarMinlength(valor, cant) {
    var val = valor;
    cantidad = cant
    if (val.length < cantidad) {
        return false;
    } else {
        return true;
    }
}

function today() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }

    if (mm < 10) {
        mm = '0' + mm;
    }

    return today = dd + '/' + mm + '/' + yyyy;
}

function validate_fechaMayorQue(today, fecha) {
    valuesStart = today.split("/");
    valuesEnd = fecha.split("/");

    // Verificamos que la fecha no sea posterior a la actual
    var dateStart = new Date(valuesStart[2], (valuesStart[1] - 1), valuesStart[0]);
    var dateEnd = new Date(valuesEnd[2], (valuesEnd[1] - 1), valuesEnd[0]);
    if (dateStart >= dateEnd) {
        return true;
    }

    return false;
}

