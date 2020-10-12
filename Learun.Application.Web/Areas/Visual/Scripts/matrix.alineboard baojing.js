var alarm_daynum = 0;

$(document).ready(function () {
    ReRealRunState();
    
    ReRealProdNum();
   /// alert("111111");
    ReRealAlarmNum();
    ReRealAlarmTotNum();

    setInterval(ReRealRunState, 3000);
    setInterval(ReRealProdNum, 3000);
    setInterval(ReRealAlarmNum, 3000);
    setInterval(ReRealAlarmTotNum, 3000);

    $('#plan_target').click(function () {
        jQuery('#modal-6').modal('show', { backdrop: 'static' });
    });
    $('#plan_num').click(function () {
        jQuery('#modal-6').modal('show', { backdrop: 'static' });
    });
});

function ReRealRunState() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "../PView/GetADeviceRunState",
        success: procRefreshRunState
    });
}

function procRefreshRunState(data) {
    var target = $("#draw_state");
    var target1 = "";
    var str_tar = "";
    var pith = "";
    target.empty();
    $.each(data,function(vi,item){
        //str_tar += "<img class=\"repng2\" src=\"/Areas/Visual/" + item.pic_path + "\" style=\"height:768px;width:1366px;\"/>";
        //alert(item.machine_name);
        switch (item.run_state) {
           
            case 1:
                pith = "/Areas/Visual/Images/run.png";//D:\TjXb\SiteJnrs\Areas\Visual\Images
                break;
            case 2:
                pith = "/Areas/Visual/Images/alarm.png";//D:\TjXb\SiteJnrs\Areas\Visual\Images
                break;
            case 3:
                pith = "/Areas/Visual/Images/pause.png";//D:\TjXb\SiteJnrs\Areas\Visual\Images
                break;
            case 4:
                pith = "/Areas/Visual/Images/ready.png";//D:\TjXb\SiteJnrs\Areas\Visual\Images
                break;
            case 0:
                pith = "/Areas/Visual/Images/stop.png";//D:\TjXb\SiteJnrs\Areas\Visual\Images
                break;
            default:
                break;
        }
        if (item.machine_name == 'OP40-F') {
            target = $("#OP40-F-I");
            target1 = $("#OP40-H-I");
        }
        if (item.machine_name == 'OP40-E') {
            target = $("#OP40-E-I");
            target1 = $("#OP40-G-I");
        }
        if (item.machine_name == 'OP30-F') {
            target = $("#OP30-F-I");
            target1 = $("#OP30-H-I");
        }
        if (item.machine_name == 'OP30-E') {
            target = $("#OP30-E-I");
            target1 = $("#OP30-G-I");
        }
        if (item.machine_name == 'OP20-F') {
            target = $("#OP20-F-I");
            target1 = $("#OP20-H-I");
        }
        if (item.machine_name == 'OP20-E') {
            target = $("#OP20-E-I");
            target1 = $("#OP20-G-I");
        }
        if (item.machine_name == 'OP10-EF') {
           // alert("222");
            target = $("#OP10-EF-I");
            target1 = $("#OP10-GH-I");
        }
        if (item.machine_name == 'Robot-2') {
            target = $("#Robot-2-I");
            target1 = $("#Robot-1-I");
        }

        if (item.machine_name == 'OP40-H') {
            target = $("#OP40-H-I");
        }
        if (item.machine_name == 'OP40-G') {
            target = $("#OP40-G-I");
        }
        if (item.machine_name == 'OP30-H') {
            target = $("#OP30-H-I");
        }
        if (item.machine_name == 'OP30-G') {
            target = $("#OP30-G-I");
        }
        if (item.machine_name == 'OP20-H') {
            target = $("#OP20-H-I");
        }
        if (item.machine_name == 'OP20-G') {
            target = $("#OP20-G-I");
        }
        if (item.machine_name == 'OP10-GH') {
            target = $("#OP10-GH-I");
        }
        if (item.machine_name == 'Robot-1') {
            target = $("#Robot-1-I");
        }
        //alert(target.find("img")[0].src);
        target.attr("src", pith);
        target1.attr("src", pith);
    });
   
    target.append(str_tar);
}
function ReRealProdNum() {
   // alert("55555");
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "../PView/GetARealProdNum",
        success: procRealProdNum
       // error: procRealProdNum
        
    });
}

function procRealProdNum(data) {
    //alert(data.length);
    var plan_num = $("#plan_num");
    var str_plan = "";
    plan_num.empty();
    var target = $("#prod_num");
    var str_tar = "";
    var str_tar1 = "";
    var prod_num = 0;
    var pass_prod_num = 0;
    var prod_rate = 0;
    var tot_num = 0;
    target.empty();
    $.each(data, function (vi, item) {
        prod_num += item.prod_num;
        //alert(prod_num);
        pass_prod_num += item.pass_prod_num;
        tot_num = item.tot_prod_num;
      //  alert(tot_num);
        prod_rate = (prod_num * 100.0 / item.plan_prod_num).toFixed(1);
        str_plan = item.plan_prod_num;
    });
    
    str_tar += "<div style=\"position:absolute;width:300px;left:70px;top:200px;font-size:20px;font-family:songti;color:#ffffff\">"+
        "<b>工件名称:</b>"+
        "<b style=\"width:80px;font-family:songti;font-size:18px;color:#ff6a00\">大众离壳</b>"+
    "</div> "+
        "<div style = \"position:absolute;width:300px;left:70px;top:230px;font-size:20px;font-family:songti;color:#ffffff\" > "+
        "<b> 实际产量/日：</b> "+
        "<b style = \"width:80px;font-family:songti;font-size: 18px;color:#ff6a00\" >" + prod_num+"</b> "+
        "</div> "+
        "<div style = \"position:absolute;width:300px;left:70px;top:260px;font-size:20px;font-family:songti;color:#ffffff\" > "+
        "<b> 合格产量/日：</b> "+
        "<b style = \"width:80px;font-family:songti;font-size:18px;color:#ff6a00\" >" + pass_prod_num+"</b> "+
        "</div> "+
        "<div style = \"position:absolute;width:300px;left:70px;top:290px;font-size:20px;font-family:songti;color:#ffffff\" > "+
        "<b> 报废数量/日：</b> "+
        "<b style=\"width:80px;font-family:songti;font-size:18px;color:#ff6a00\">" + (prod_num - pass_prod_num)+"</b>"+
        "</div> "+
        "<div style = \"position:absolute;width:300px;left:70px;top:320px;font-size:20px;font-family:songti;color:#ffffff\" > "+
        "<b > 累计产量/月:</b> "+
        "<b style = \"width:80px;font-family:songti;font-size:18px;color:#ff6a00\" >" + tot_num+"</b> "+
        "</div> ";
    
    str_tar1 += "<div style = \"position:absolute;width:300px;left:70px;top:370px;font-size:20px;font-family:songti;color:#ffffff\" > " +
        "<b> 合格产量/周：</b> " +
        "<b style = \"width:80px;font-family:songti;font-size:18px;color:#ff6a00\" >" + Math.ceil(tot_num / 4) + "</b> " +
        "</div> " +
        "<div style = \"position:absolute;width:300px;left:70px;top:400px;font-size:20px;font-family:songti;color:#ffffff\" > " +
        "<b> 报废数量/月：</b> " +
        "<b style=\"width:80px;font-family:songti;font-size:18px;color:#ff6a00\">" + tot_num + "</b>" +
        "</div> " +
        "<div style = \"position:absolute;width:300px;left:70px;top:430px;font-size:20px;font-family:songti;color:#ffffff\" > " +
        "<b > 累计产量/年:</b> " +
        "<b style = \"width:80px;font-family:songti;font-size:18px;color:#ff6a00\" >" + tot_num*12 + "</b> " +
        "</div> ";

    //alert(str_tar);
    plan_num.html(str_tar);
    target.html(str_tar1);
}

function ReRealAlarmNum() {
   /// alert("222222");
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "../PView/GetARealAlarmNum",
        success: procRealAlarmNum
    });
}

function procRealAlarmNum(data) {
   // alert(data.length);
    var str_tar = "";
    alarm_daynum = 0;
    var target = $("#OP40-F-B");
    for (var i = 0; i < data.length; i++) {
        //alert(data[i].tot_alarm_num);
        if (data[i].machine_name == 'OP40-F') {
            target = $("#OP40-F-B");
            target.html(data[i].tot_alarm_num + "次");
            target = $("#OP40-H-B");
            target.html(data[i].tot_alarm_num + "次");
            alarm_daynum += data[i].tot_alarm_num;
        }

        if (data[i].machine_name == 'OP40-E') {
            target = $("#OP40-E-B");
            target.html(data[i].tot_alarm_num + "次");
            target = $("#OP40-G-B");
            target.html(data[i].tot_alarm_num + "次");
            alarm_daynum += data[i].tot_alarm_num;
        }

        if (data[i].machine_name == 'OP30-F') {
            target = $("#OP30-F-B");
            target.html(data[i].tot_alarm_num + "次");
            target = $("#OP30-H-B");
            target.html(data[i].tot_alarm_num + "次");
            alarm_daynum += data[i].tot_alarm_num;
        }

        if (data[i].machine_name == 'OP30-E') {
            target = $("#OP30-E-B");
            target.html(data[i].tot_alarm_num + "次");
            target = $("#OP30-G-B");
            target.html(data[i].tot_alarm_num + "次");
            alarm_daynum += data[i].tot_alarm_num;
        }

        if (data[i].machine_name == 'OP20-F') {
            target = $("#OP20-F-B");
            target.html(data[i].tot_alarm_num + "次");
            target = $("#OP20-H-B");
            target.html(data[i].tot_alarm_num + "次");
            alarm_daynum += data[i].tot_alarm_num;
        }

        if (data[i].machine_name == 'OP20-E') {
            target = $("#OP20-E-B");
            target.html(data[i].tot_alarm_num + "次");
            target = $("#OP20-G-B");
            target.html(data[i].tot_alarm_num + "次");
            alarm_daynum += data[i].tot_alarm_num;
        }

        if (data[i].machine_name == 'OP10-EF') {
            target = $("#OP10-EF-B");
            target.html(data[i].tot_alarm_num + "次");
            target = $("#OP10-GH-B");
            target.html(data[i].tot_alarm_num + "次");
            alarm_daynum += data[i].tot_alarm_num;
        }

        if (data[i].machine_name == 'Robot-2') {
            target = $("#Robot-2-B");
            target.html(data[i].tot_alarm_num + "次");
            target = $("#Robot-1-B");
            target.html(data[i].tot_alarm_num + "次");
            alarm_daynum += data[i].tot_alarm_num;
        }

        if (data[i].machine_name == 'OP40-H') {
           // alert(data[i].machine_name);
            target = $("#OP40-H-B");
            target.html(data[i].tot_alarm_num + "次");
            alarm_daynum += data[i].tot_alarm_num;
        }

        if (data[i].machine_name == 'OP40-G') {
            target = $("#OP40-G-B");
            target.html(data[i].tot_alarm_num + "次");
            alarm_daynum += data[i].tot_alarm_num;
        }
        if (data[i].machine_name == 'OP30-H') {
            target = $("#OP30-H-B");
            target.html(data[i].tot_alarm_num + "次");
            alarm_daynum += data[i].tot_alarm_num;
        }
        if (data[i].machine_name == 'OP30-G') {
            target = $("#OP30-G-B");
            target.html(data[i].tot_alarm_num + "次");
            alarm_daynum += data[i].tot_alarm_num;
        }
        if (data[i].machine_name == 'OP20-H') {
            target = $("#OP20-H-B");
            target.html(data[i].tot_alarm_num + "次");
            alarm_daynum += data[i].tot_alarm_num;
        }
        if (data[i].machine_name == 'OP20-G') {
            target = $("#OP20-G-B");
            target.html(data[i].tot_alarm_num + "次");
            alarm_daynum += data[i].tot_alarm_num;
        }
        if (data[i].machine_name == 'OP10-GH') {
            target = $("#OP10-GH-B");
            target.html(data[i].tot_alarm_num + "次");
            alarm_daynum += data[i].tot_alarm_num;
        }
        if (data[i].machine_name == 'Robot-1') {
            target = $("#Robot-1-B");
            target.html(data[i].tot_alarm_num + "次");
            alarm_daynum += data[i].tot_alarm_num;
        }
    }
}
function ReRealAlarmTotNum() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "../PView/GetARealAlarmTotNum",
        success: procRealAlarmTotNum
    });
}

function procRealAlarmTotNum(data) {
    var target = $("#alarm_totnum");
    var str_tar = "";
    target.empty();

    var cur_alarm_hour = Math.floor(data.curDayMaxAlarmDur * 1.0 / 3600);
    var cur_alarm_min = Math.floor((data.curDayMaxAlarmDur % 3600) * 1.0 / 60);
    var cur_alarm_str = cur_alarm_hour + "小时" + cur_alarm_min + "分钟";

    var mon_alarm_hour = Math.floor(data.monthMaxAlarmDur * 1.0 / 3600);
    var mon_alarm_min = Math.floor((data.monthMaxAlarmDur % 3600) * 1.0 / 60);
    var mon_alarm_str = mon_alarm_hour + "小时" + mon_alarm_min + "分钟";

    var mon_stop_hour = Math.floor(data.monthMaxStopDur * 1.0 / 3600);
    var mon_stop_min = Math.floor((data.monthMaxStopDur % 3600) * 1.0 / 60);
    var mon_stop_str = mon_stop_hour + "小时" + mon_stop_min + "分钟";

    str_tar += "<div style=\"position: absolute; width: 300px; left: 70px; top: 490px; font-size: 18px; font-family: songti; color: #FFFFFF; z - index: 2\">" +
    "<b> 总报警次数 / 日：</b >" +
        "<b style=\"width: 80px; font - family: songti; font-size: 20px; color: #ff6a00\">" + alarm_daynum+"</b>" +
    "</div>" +
    "<div style=\"position: absolute; width: 300px; left: 70px; top: 530px; font-size: 18px; font-family: songti; color: #FFFFFF; z - index: 2\">" +
    "<b>总报警时间/日：</b>" +
        "<b style=\"width: 80px; font - family: songti; font - size: 20px; color: #ff6a00\">" + cur_alarm_str+"</b>" +
    "</div>" +
    "<div style=\"position: absolute; width: 300px; left: 70px; top: 570px; font-size: 18px; font-family: songti; color: #FFFFFF; z - index: 2\">" +
    "<b>报警累计时间/月：</b>" +
        "<b style=\"width: 80px; font - family: songti; font-size: 20px; color: #ff6a00\">" + mon_alarm_str+"</b>" +
    "</div>" +
    "<div style=\"position: absolute; width: 300px; left: 70px; top: 610px; font-size: 18px; font-family: songti; color: #FFFFFF; z - index: 2\">" +
    "<b>停机累计时间/月：</b>" +
        "<b style=\"width:80px;font-family:songti;font-size: 20px;color:#ff6a00\">" + mon_stop_str+"</b>"+
    "</div>";

    target.html(str_tar);
}

function updatePlanProdNum() {
    //var plan_prod_num = $('#m_plan_num').val();
    var plan_prod_mor = $('#m_plan_mor').val();
    var plan_prod_mid = $('#m_plan_mid').val();
    var plan_prod_nig = $('#m_plan_nig').val();

    $.ajax({
        type: "post",
        dataType: "json",
        data: {
            //plan_prod_num: plan_prod_num,
            plan_prod_mor: plan_prod_mor,
            plan_prod_mid: plan_prod_mid,
            plan_prod_nig: plan_prod_nig,
        },
        url: "../Home/UpdateAPlanProdNum",
        success: alert('success')
    });
    $('#modal-6').modal('hide');
}
