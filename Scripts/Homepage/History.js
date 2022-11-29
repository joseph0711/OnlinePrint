﻿$(document).ready(function () {
    $('#Apply_Form').DataTable({
        "ordering": false,
        "searching": false, //搜尋功能, 預設是開啟
        //"pageLength": 10,
        "paging": false,
        "lengthChange": false,

        "language": {
            "processing": "處理中...",
            "loadingRecords": "載入中...",
            "lengthMenu": "顯示 _MENU_ 項結果",
            "zeroRecords": "無資料",
            "info": "顯示第 _START_ 至 _END_ 項結果，共 _TOTAL_ 項",
            "infoEmpty": "顯示第 0 至 0 項結果，共 0 項",
            "infoFiltered": "(從 _MAX_ 項結果中過濾)",
            "infoPostFix": "",
            "search": "搜尋:",
            "paginate": {
                "first": "第一頁",
                "previous": "上一頁",
                "next": "下一頁",
                "last": "最後一頁"
            },
        },
        responsive: true
    });
});

function showModal(i) {
    $("#detailModal" + (i)).modal('show');
}