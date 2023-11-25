const vid = document.getElementById("botonVideo")
const com = document.getElementById("botonComic")
const fvid = document.getElementById("formularioVideo")
const fcom = document.getElementById("formularioComic")

const acepComic = document.getElementById("aceptarComic");
const acepVideo = document.getElementById("aceptarVideo");
const etq = document.getElementById("etiquetas");

vid.addEventListener("click", function () {
    com.classList.add("d-none");
    fvid.classList.remove("d-none");
});

com.addEventListener("click", function () {
    vid.classList.add("d-none");
    fcom.classList.remove("d-none");
});

document.addEventListener('DOMContentLoaded', function () {
    var botonAceptar = document.getElementById('acpetarVideo');
    botonAceptar.addEventListener('click', function () {
        var miEtiqueta = document.getElementById('etiquetas');
        miEtiqueta.classList.remove('d-none');
    });
});

$(document).ready(function () {
    $("#departamento").change(function () {
        var valorDep = $(this).val();
        $.ajax({
            url: '/Historias/mostrarMunicipios',
            type: 'GET',
            data: { depId: valorDep },
            success: function (data) {
                $('#municipio').empty();
                $('#municipio').append($('<option>', {
                    value: '',
                    text: 'Seleccione el municipio'
                }));
                $.each(data, function (index, item) {
                    $('#municipio').append($('<option>', {
                        value: item.munId,
                        text: item.munNombre
                    }));
                });
            },
        });
    });

    $("#departamento2").change(function () {
        var valorDep = $(this).val();
        $.ajax({
            url: '/Historias/mostrarMunicipios',
            type: 'GET',
            data: { depId: valorDep },
            success: function (data) {
                $('#municipio2').empty();
                $('#municipio2').append($('<option>', {
                    value: '',
                    text: 'Seleccione el municipio'
                }));
                $.each(data, function (index, item) {
                    $('#municipio2').append($('<option>', {
                        value: item.munId,
                        text: item.munNombre
                    }));
                });
            },
        });
    });
});

$(document).ready(function () {
    // Manejar clic en botón Comic
    $('#botonComic').click(function () {
        var valor = $(this).data('valor');
        console.log('Seleccionaste el botón Comic con valor: ' + valor);
        // Puedes hacer algo con el valor aquí
    });

    // Manejar clic en botón Video
    $('#botonVideo').click(function () {
        var valor = $(this).data('valor');
        console.log('Seleccionaste el botón Video con valor: ' + valor);
        // Puedes hacer algo con el valor aquí
    });
});