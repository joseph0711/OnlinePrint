$("#confirm_Info").click(function () {
    //subject
    var replace_Subject = $("#Subject").val();
    $("#dis_Subject").replaceWith("<label id='dis_Subject'>" + replace_Subject + "</label>");

    //Print_kind
    var replace_PrintKind = $("#print_Kide").val();
    $("#dis_PrintKind").replaceWith("<label id='dis_PrintKind'>" + replace_PrintKind + "</label>");

    //PickUp_Date
    var replace_PickUpDate = $("#pickUp_Date").val();
    $("#dis_Date").replaceWith("<label id='dis_Date'>" + replace_PickUpDate + "</label>");

    //Authorisation_person //**undebug**
    var auth = document.getElementsByName('Alter');

    for (var i = 0; i < auth.length; i++) {
        if (auth[i].checked) {
            var replace_Auth = auth[i];
            $("#dis_Auth").replaceWith("<label id='dis_Auth'>" + replace_Auth + "</label>");
            break;
        }
    }

    //Print_Name
    var replace_PrintName = $("#print_Name").val();
    $("#dis_PrintName").replaceWith("<label id='dis_PrintName'>" + replace_PrintName + "</label>");

    //File Upload  //**panding //先行建置好SQL相對應置放檔案之地點後再新增此功能


    //Print_Num 
    var replace_PrintNum = $("#print_Num").val();
    $("#dis_PrintNum").replaceWith("<label id='dis_PrintNum'>" + replace_PrintNum + "</label>");

    //Print_Side 
    var replace_PrintSide = $("#print_Side").val();
    $("#dis_PrintSide").replaceWith("<label id='dis_PrintSide'>" + replace_PrintSide + "</label>");

    //Print_Size
    var replace_PrintSize = $("#print_Size").val();
    $("#dis_PrintSize").replaceWith("<label id='dis_PrintSize'>" + replace_PrintSize + "</label>");

    //Memo
    var replace_Memo = $("#memo").val();
    $("#dis_Memo").replaceWith("<label id='dis_Memo'>" + replace_Memo + "</label>");
});
