$(document).ready(function () {
    $('#subMenuDep').on('mouseover', function () {
        $(this).find('#menuDep').addClass('show');
    }).on('mouseout', function () {
        $(this).find('#menuDep').removeClass('show');
    });
    $('#subMenuTipDoc').on('mouseover', function () {
        $(this).find('#menuDep').addClass('show');
    }).on('mouseout', function () {
        $(this).find('#menuDep').removeClass('show');
    });
    $('#subMenuEtq').on('mouseover', function () {
        $(this).find('#menuDep').addClass('show');
    }).on('mouseout', function () {
        $(this).find('#menuDep').removeClass('show');
    });
});


$(document).ready(function () {
    $(document).on('click', '.etiqueta', function () {
        var valor = $(this).data('value');
        $('#mostrarHistorias').addClass('d-none');
        $.ajax({
            url: '/Historias/filtrarHistoria',
            type: 'POST',
            data: {
                idEtiquetas: valor,
                idTipo: null,
                idDep: null
            },
            success: function (response) {
                $('#historiasFiltradas').html(response);
                $('#historiasFiltradas').removeClass('d-none');
            }
        });
    });
    $(document).on('click', '.departamento', function () {
        var valor = $(this).data('value');
        $('#mostrarHistorias').addClass('d-none');
        $.ajax({
            url: '/Historias/filtrarHistoria',
            type: 'POST',
            data: {
                idEtiquetas: null,
                idTipo: null,
                idDep: valor
            },
            success: function (response) {
                $('#historiasFiltradas').html(response);
                $('#historiasFiltradas').removeClass('d-none');
            }
        });
    });
    $(document).on('click', '.tipo', function () {
        var valor = $(this).data('value');
        $('#mostrarHistorias').addClass('d-none');
        $.ajax({
            url: '/Historias/filtrarHistoria',
            type: 'POST',
            data: {
                idEtiquetas: null,
                idTipo: valor,
                idDep: null
            },
            success: function (response) {
                $('#historiasFiltradas').html(response);
                $('#historiasFiltradas').removeClass('d-none');
            }
        });
    });
});

$(document).ready(function () {

    $(document).on('click', '.historia', function () {
        var valor = $(this).data('value');
        $.ajax({
            url: '/Historias/VerHistoria',
            type: 'POST',
            data: {
                id: valor
            },
        });
    });
});