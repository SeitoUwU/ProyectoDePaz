const vid = document.getElementById("botonVideo")
const com = document.getElementById("botonComic")
const fvid = document.getElementById("formularioVideo")
const fcom = document.getElementById("formularioComic")

const acepComic = document.getElementById("aceptarComic");
const acepVideo = document.getElementById("aceptarVideo");
const etq = document.getElementById("etiquetas");

var tipoDeDoc;

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
    var botonAceptar2 = document.getElementById('aceptarComic');
    botonAceptar2.addEventListener('click', function () {
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
        tipoDeDoc = valor;
        console.log('Seleccionaste el botón Comic con valor: ' + valor);
    });
    $('#botonVideo').click(function () {
        var valor = $(this).data('valor');
        tipoDeDoc = valor;
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
}

$(document).ready(function () {
    $('#enviarFormulario').click(function () {
        if (tipoDeDoc == 1) {
            var titulo = document.getElementById('tituloVideo').value;
            var municipio = document.getElementById('municipio2').value;
            var descripcion = document.getElementById('descripcionVideo').value;
            var check = document.getElementById('anonimoVideo').checked;
            var url = document.getElementById('urlVideo').value;

            var data = {
                titulo: titulo,
                municipio: municipio,
                descripcion: descripcion,
                check: check,
                etiquetas: etiquetasSeleccionadas,
                url: url
            }

            $.ajax({
                url: '/Historias/RegistrarVideo',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(data)
            });

        } else if (tipoDeDoc == 2) {
            var titulo = document.getElementById('tituloComic').value;
            var municipio = document.getElementById('municipio').value;
            var descripcion = document.getElementById('descripcionComic').value;
            var check = document.getElementById('anonimoComic').checked;
            var documento = document.getElementById('documentoComic').files[0];
            var etiquetasJSON = JSON.stringify(etiquetasSeleccionadas);
            var formData = new FormData(); 
            formData.append('titulo', titulo);
            formData.append('municipio', municipio);
            formData.append('descripcion', descripcion);
            formData.append('check', check);
            formData.append('etiquetas', etiquetasJSON);
            formData.append('documento', documento);

            $.ajax({
                url: '/Historias/RegistrarComic',
                type: 'POST',
                processData: false, // Evita que jQuery procese los datos
                contentType: false, // Evita que jQuery configure el tipo de contenido
                data: formData
            });
        }
    });
});