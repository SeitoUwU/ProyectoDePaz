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
    $('#botonComic').click(function () {
        var valor = $(this).data('valor');
        console.log('Seleccionaste el botón Comic con valor: ' + valor);
    });
    $('#botonVideo').click(function () {
        var valor = $(this).data('valor');
        console.log('Seleccionaste el botón Video con valor: ' + valor);
    });
});



// Array para almacenar los IDs de las etiquetas seleccionadas
var etiquetasSeleccionadas = [];

function handleCheckboxChange(checkbox) {
    var etqId = checkbox.id; // Obtener el ID del checkbox
    if (checkbox.checked) {
        // Si el checkbox está marcado, agregar el ID al arreglo
        etiquetasSeleccionadas.push(etqId);
    } else {
        // Si el checkbox está desmarcado, eliminar el ID del arreglo
        var index = etiquetasSeleccionadas.indexOf(etqId);
        if (index !== -1) {
            etiquetasSeleccionadas.splice(index, 1);
        }
    }

    // Aquí puedes realizar cualquier acción adicional con el arreglo actualizado
    // Por ejemplo, imprimir los IDs seleccionados en la consola
    console.log("IDs seleccionados:", etiquetasSeleccionadas);
}
