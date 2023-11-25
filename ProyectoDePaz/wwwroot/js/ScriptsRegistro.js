$(document).ready(function () {
    $('#dep').change(function () {
        var valorDep = $(this).val();
        $.ajax({
            url: '/IngresoUsuario/mostrarMunicipio',
            type: 'GET',
            data: { depId: valorDep },
            success: function (data) {
                $('#municipio').empty();
                $('#institucion').empty();
                $('#municipio').append($('<option>', {
                    value: '',
                    text: 'Seleccione el municipio'
                }));
                $('#institucion').append($('<option>', {
                    value: '',
                    text: 'Seleccione la institucion'
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

    $('#municipio').change(function () {
        var valorMun = $(this).val();
        $.ajax({
            url: '/IngresoUsuario/mostrarInstitucion',
            type: 'GET',
            data: { munId: valorMun },
            success: function (data) {
                console.log(data);
                $('#institucion').empty();
                $('#institucion').append($('<option>', {
                    value: '',
                    text: 'Seleccione institucion'
                }));
                $.each(data, function (index, item) {
                    $('#institucion').append($('<option>', {
                        value: item.insId,
                        text: item.insInstitucion
                    }));
                });
            },
        });
    })
});