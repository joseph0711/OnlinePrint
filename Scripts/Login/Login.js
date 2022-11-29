$(document).ready(function () {
    $("input[id^=order]").click(function () {
        $("input[id^=order]").parent().css("background-color", "#fff");
        $("input[id^=order]").parent().css("color", "#6c757d");
        $(this).parent().css("background-color", "#6c757d");
        $(this).parent().css("color", "#fff");
    });

    $("input[id = 'order1']").click(function () {
        $('#account').val('demo');
        $('#password').val('0123456789');
    });

    $("input[id = 'order2']").click(function () {
        $('#account').val('test02');
        $('#password').val('123456789');
    });
});

$(function () {
    'use strict';
    window.addEventListener('load', function () {
        // Fetch all the forms we want to apply custom Bootstrap validation styles to
        var forms = document.getElementsByClassName('needs-validation');
        // Loop over them and prevent submission
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                else if (form.checkValidity() === true) {
                    $(".loader").show();
                    $("#account").attr("readonly", true);
                    $("#password").attr("readonly", true);
                    $("#validation").attr("readonly", true);
                    $("#login").attr("disabled", true);
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
});

