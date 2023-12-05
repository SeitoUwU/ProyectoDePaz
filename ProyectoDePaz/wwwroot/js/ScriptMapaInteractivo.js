$(document).ready(function () {
    $('.Florencia').on('click', function () {
        var valor = $(this).data('value');
        $.ajax({
            url: "/VerDocumento/VerHistoria",
            type: "POST",
            data: { id: valor },
        });
    });
});