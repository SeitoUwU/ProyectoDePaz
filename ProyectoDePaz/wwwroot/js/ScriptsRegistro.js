$(document).ready(function () {
    $('#dep').on('change', function () {
        var valor = $(this).val();
        console.log(valor);
        $.ajax({
            url: '/IngresoUsuario/mostrarMunicipio',
            type: 'GET',
            data: { depId: valor },
            success: function (result) {
                var municipios = $(result).find('#municipio');
                $('#divMunicipios').html(municipios);
            }
        });
    });
});

$(document).ready(function () {
    $('#municipio').on('change', function () {
        var valor = $(this).val();
        console.log(valor);
        $.ajax({
            url: '/IngresoUsuario/mostrarInstitucion',
            type: 'GET',
            data: { munId: valor },
            success: function (result) {
                var instituciones = $(result).find('#ins');
                $('#divInstitucion').html(instituciones);
            }
        });
    });
});