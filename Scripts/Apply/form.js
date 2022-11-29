$(document).ready(function () {
    $("#pickUp_Date").datepicker({
        minDate: 1,
        maxDate: "+14D",
    });

    //根據領取日期來使領取時間的欄位disable
    $("#pickUp_Date").on('change', function () {
        if ($(this).val() == null) {
            $("#pickUp_Time").attr('disabled', 'disabled');
        }
        else {
            $("#pickUp_Time").attr('disabled', false);
        }
    });

    //表單輸入欄位字元驗證
    $('#phone').keyup(function () {
        var $th = $(this);
        $th.val(
            $th.val().replace(/[^0-9]/g,
                function (str) {
                    $("#phoneInvalid").text("無效字元，請重新輸入！")
                    return '';
                }
            ));
    });

    $('#phone').keyup(function () {
        var $th = $(this);
        $th.val(
            $th.val().replace(/[^0-9]/g,
                function (str) {
                    $("#phoneInvalid").text("無效字元，請重新輸入！")
                    return '';
                }
            ));
    });
    
    $('#print_Num').keyup(function () {
        var $th = $(this);
        $th.val(
            $th.val().replace(/[^0-9]/g,
                function (str) {
                    $("#printNumInvalid").text("無效字元，請重新輸入！")
                    return '';
                }
            )
        )
    });
});

//Time picker configuration
$("#pickUp_Time").timepicker({
    timeFormat: "h:mm p", // 時間隔式
    interval: 30, //時間間隔
    minTime: "8", //最小時間
    maxTime: "17:00", //最大時間
    startTime: "8:00", // 開始時間
    dynamic: true, //是否顯示項目，使第一個項目按時間順序緊接在所選時間之後
    dropdown: true, //是否顯示時間條目的下拉列表
    scrollbar: false //是否顯示捲軸
});

//Date picker configuration
$("#pickUp_Date").datepicker({
    minDate: 1,
    maxDate: "+14D",
    dateFormat: 'yy-mm-dd',
    changeYear: false,
    changeMonth: false,
});

//modal
function Modal_Time_Detail() {
    $("#time_Detail").modal('show');
}

function Modal_Date_Detail() {
    $("#date_Detail").modal('show');
}

//答案卷張數根據是否點選來顯示
$("#add_AnswerPaper").change(function () {
    if ($("#add_AnswerPaper").is(':checked') == true) {
        $("#ansPaperNum").attr('disabled', false);
    }
    else {
        $("#ansPaperNum").attr('disabled', true);
        $("#ansPaperNum").val('');
    }
});

//檢查申請表是否輸入完全
(function () {
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
                    $("#loader").show();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();