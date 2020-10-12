var alarm_daynum = 0;

$(document).ready(function () {
    ReRealRunState();
    ReRealProdNum();
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
        url: "../PView/GetBDeviceRunState",
        success: procRefreshRunState
    });
}

function procRefreshRunState(data) {
    var target = $("#draw_state");
    var str_tar = "";
    target.empty();
    $.each(data, function (vi, item) {
        str_tar += "<img class=\"repng2\" src=\"/Areas/Visual/" + item.pic_path + "\" style=\"height:768px;width:1366px;\"/>";
    });
    //alert(str_tar);
    target.append(str_tar);
}
function ReRealProdNum() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "../PView/GetBRealProdNum",
        success: procRealProdNum
    });
}

function procRealProdNum(data) {
    var plan_num = $("#plan_num");
    var str_plan = "";
    plan_num.empty();
    var target = $("#prod_num");
    var str_tar = "";
    var prod_num = 0;
    var pass_prod_num = 0;
    var prod_rate = 0;
    var tot_num = 0;
    target.empty();
    $.each(data, function (vi, item) {
        prod_num += item.prod_num;
        pass_prod_num += item.pass_prod_num;
        tot_num = item.tot_prod_num;
        prod_rate = (prod_num * 100.0 / item.plan_prod_num).toFixed(1);
        str_plan = item.plan_prod_num;
    });

    str_tar += "<tr><td class=\"sheet1\">实际产量/日</td><td class=\"sheet2\">" + prod_num + "</td></tr>"
               + "<tr><td class=\"sheet1\">合格产量/日</td><td class=\"sheet2\">" + pass_prod_num + "</td></tr>"
               + "<tr><td class=\"sheet1\">报废数量/日</td><td class=\"sheet2\">" + (prod_num - pass_prod_num) + "</td></tr>"
               + "<tr><td class=\"sheet1\">加工达成率</td><td class=\"sheet2\">" + prod_rate + "%</td></tr>"
               + "<tr><td class=\"sheet1\">报废率</td><td class=\"sheet2\">"
                    + ((prod_num == 0) ? 0 : (prod_num - pass_prod_num) * 100.0 / prod_num).toFixed(1) + "%</td></tr>"
               + "<tr><td class=\"sheet1\">累计产量/月</td><td class=\"sheet2\">" + tot_num + "</td></tr>";
    //alert(str_tar);
    plan_num.append(str_plan);
    target.append(str_tar);
}

function ReRealAlarmNum() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "../PView/GetBRealAlarmNum",
        success: procRealAlarmNum
    });
}

function procRealAlarmNum(data) {
    var str_tar = "";
    alarm_daynum = 0;
    
    var target = $("#a2op20-2");
    for (var i = 0; i < data.length; i++) {
        if (data[i].machine_name == '3#OP20') {
            target = $("#a2op20-2");
            str_tar = "<tr><td colspan=2 >3#op20</td></tr><tr><td style=\"width:80px\">报警次数</td><td >" + data[i].alarm_num + "</td></tr>";
            target.empty();
            target.append(str_tar);
            alarm_daynum += data[i].alarm_num;
        }

        if (data[i].machine_name == '4#OP20') {
            target = $("#a2op20-1");
            str_tar = "<tr><td colspan=2 >4#op20</td></tr><tr><td style=\"width:80px\">报警次数</td><td >" + data[i].alarm_num + "</td></tr>";
            target.empty();
            target.append(str_tar);
            alarm_daynum += data[i].alarm_num;
        }

        if (data[i].machine_name == '4#OP10') {
            target = $("#a2op10-2");
            str_tar = "<tr><td colspan=2 >4#op10</td></tr><tr><td style=\"width:80px\">报警次数</td><td >" + data[i].alarm_num + "</td></tr>";
            target.empty();
            target.append(str_tar);
            alarm_daynum += data[i].alarm_num;
        }

        if (data[i].machine_name == '3#OP10') {
            target = $("#a2op10-1");
            str_tar = "<tr><td colspan=2 >3#op10</td></tr><tr><td style=\"width:80px\">报警次数</td><td >" + data[i].alarm_num + "</td></tr>";
            target.empty();
            target.append(str_tar);
            alarm_daynum += data[i].alarm_num;
        }

        if (data[i].machine_name == '1#OP20') {
            target = $("#a1op20-2");
            str_tar = "<tr><td colspan=2 >1#op20</td></tr><tr><td style=\"width:80px\">报警次数</td><td >" + data[i].alarm_num + "</td></tr>";
            target.empty();
            target.append(str_tar);
            alarm_daynum += data[i].alarm_num;
        }

        if (data[i].machine_name == '2#OP20') {
            target = $("#a1op20-1");
            str_tar = "<tr><td colspan=2 >2#op20</td></tr><tr><td style=\"width:80px\">报警次数</td><td >" + data[i].alarm_num + "</td></tr>";
            target.empty();
            target.append(str_tar);
            alarm_daynum += data[i].alarm_num;
        }

        if (data[i].machine_name == '2#OP10') {
            target = $("#a1op10-2");
            str_tar = "<tr><td colspan=2 >2#op10</td></tr><tr><td style=\"width:80px\">报警次数</td><td >" + data[i].alarm_num + "</td></tr>";
            target.empty();
            target.append(str_tar);
            alarm_daynum += data[i].alarm_num;
        }

        if (data[i].machine_name == '1#OP10') {
            target = $("#a1op10-1");
            str_tar = "<tr><td colspan=2 >1#op10</td></tr><tr><td style=\"width:80px\">报警次数</td><td >" + data[i].alarm_num + "</td></tr>";
            target.empty();
            target.append(str_tar);
            alarm_daynum += data[i].alarm_num;
        }
    }
}
function ReRealAlarmTotNum() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "../PView/GetBRealAlarmTotNum",
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

    str_tar += "<tr><td class=\"sheet1\">总报警次数/日</td></tr><tr><td class=\"sheet2\">" + alarm_daynum + "</td></tr>"
            + "<tr><td class=\"sheet1\">总报警时间/日</td></tr><tr><td class=\"sheet2\">" + cur_alarm_str + "</td></tr>"
            + "<tr><td class=\"sheet1\">报警累计时间/月</td></tr><tr><td class=\"sheet2\">" + mon_alarm_str + "</td></tr>"
            + "<tr><td class=\"sheet1\">停机累计时间/月</td></tr><tr><td class=\"sheet2\">" + mon_stop_str + "</td></tr>"

    target.append(str_tar);
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
        url: "../Home/UpdateBPlanProdNum",
        success: alert('success')
    });
    $('#modal-6').modal('hide');
}
