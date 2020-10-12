var alarm_daynum = 0;

$(document).ready(function () {
    ReRealRunState();
    //
    ReRealAlarmTotNum();//获取周产量，月产量，年产量
    //
    setInterval(ReRealRunState, 10000);
    //
    setInterval(ReRealAlarmTotNum, 10000);//获取周产量，月产量，年产量
    //
    $('#plan_target').click(function () {
        jQuery('#modal-6').modal('show', { backdrop: 'static' });
    });
    $('#plan_num').click(function () {
        jQuery('#modal-6').modal('show', { backdrop: 'static' });
    });
});

function ReRealRunState() {
//alert("333");
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "../ReportTemplate/GetTLRDeviceRunState",
        success: procRefreshRunState
    });
}

function procRefreshRunState(data) {
	console.log(data);
    var target = $("#draw_state");
    var target1 = "";
    var str_tar = "";
    var pith = "";
    target.empty();
    $.each(data,function(vi,item){
        //
        switch (item.run_state) {
           
            case 1:
                pith = "/Areas/Visual/"+ item.pic_path;//D:\TjXb\SiteJnrs\Areas\Visual\Images
                break;
            case 2:
                pith = "/Areas/Visual/"+ item.pic_path;//
                break;
            case 3:
                pith = "/Areas/Visual/"+ item.pic_path;//
                break;
            case 4:
                pith = "/Areas/Visual/"+ item.pic_path;//
                break;
            case 0:
                pith = "/Areas/Visual/"+ item.pic_path;//
                break;
            default:
                break;
        }
        if (item.machine_id == 103) {
            target = $("#OP10-1").attr("src", pith);
        }
        if (item.machine_id == 104) {
            target = $("#OP10-2").attr("src", pith);
        }
        if (item.machine_id == 105) {
            target = $("#OP10-3").attr("src", pith);
        }
        if (item.machine_id == 106) {
            target = $("#OP10-4").attr("src", pith);
        }
        if (item.machine_id == 109) {
            target = $("#OP20-1").attr("src", pith);
        }
        if (item.machine_id == 110) {
            target = $("#OP20-2").attr("src", pith);
        }
        if (item.machine_id == 201) {
            target = $("#OP30-1").attr("src", pith);
        }
        if (item.machine_id == 202) {
            target = $("#OP30-2").attr("src", pith);
        }

        if (item.machine_id == 301) {
            target = $("#OP50-1").attr("src", pith);
        }
        if (item.machine_id == 302) {
            target = $("#OP50-2").attr("src", pith);
        }
        if (item.machine_id == 303) {
            target = $("#OP50-3").attr("src", pith);
        }
        if (item.machine_id == 304) {
            target = $("#OP50-4").attr("src", pith);
        }
        if (item.machine_id == 305) {
            target = $("#OP50-5").attr("src", pith);
        }
    });
   
    target.append(str_tar);
}
//function ReRealProdNum() {
//   // alert("55555");
//    $.ajax({
//        type: "GET",
//        dataType: "json",
//        url: "../ReportTemplate/GetARealProdNum",
//        success: procRealProdNum
//       // error: procRealProdNum
        
//    });
//}

//function procRealProdNum(data) {

//    $.each(data, function (vi, item) {
//        var plan_prod_num = item.plan_prod_num;
//        var prod_num = item.prod_num;
//        var W_prod_num = item.plan_prod_num - item.prod_num;
//        var prod_rate = item.prod_rate;
//        var pp = $('#PPN');
//        pp.html(plan_prod_num + '件');
//        var pdd = $('#pd')
//        pdd.html(prod_num + '件');
//        $('#WP').html(W_prod_num + '件');
//        $('#pr').html(prod_rate + '%');
//        //console.log(plan_prod_num, prod_num, W_prod_num, prod_rate);
//    });

//}

function ReRealAlarmNum() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "../ReportTemplate/GetARealAlarmNum",
        success: procRealAlarmNum
    });
}

function procRealAlarmNum(data) {
    //alert(data.length);
    var str_tar = "";
    alarm_daynum = 0;
    var target = $("#OP40-F-B");
    
}
function ReRealAlarmTotNum() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "../ReportTemplate/GetMachineProdNum",//获取周产量，月产量，年产量
        success: procRealAlarmTotNum
    });
}
function ReRealAlarmTotNum_B() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "../ReportTemplate/GetBRealAlarmTotNum",
        success: procRealAlarmTotNum_B
    });
}

function procRealAlarmTotNum(data) {
    var Robot_1_week = 0;
    var Robot_1_month = 0;
    var Robot_1_year = 0;
    var Robot_2_week = 0;
    var Robot_2_month = 0;
    var Robot_2_year = 0;
    var Robot_1_day = 0;
    var Robot_2_day = 0;
    var Robot_1_plan = 0;
    var Robot_2_plan = 0;
    $.each(data, function (vi, item) {
		
        console.log("item.machine_id:" + item.machine_id);
		
        if (item.prod_num == "") {
            M_day_prod = "0";
        } else if (item.prod_num == 0) {
            M_day_prod = "0";
        } else {
            M_day_prod = item.prod_num;
        }        

        if (item.machine_id == 11) {
            Robot_1_week = item.week_prod_num;
            Robot_1_month = item.month_prod_num;
            Robot_1_year = item.year_prod_num;
            Robot_1_day = item.prod_num;
            Robot_1_plan = item.plan_prod_num;
        }        

        if (item.machine_id == 28) {
            Robot_2_week = item.week_prod_num;
            Robot_2_month = item.month_prod_num;
            Robot_2_year = item.year_prod_num;
            Robot_2_day = item.prod_num;
            Robot_2_plan = item.plan_prod_num;
        }
    });
	
	var target_lt = $("#prod_num_toplt");	
    target_lt.empty();
    var str_tar_lt = "";
	str_tar_lt += "<tr><td class=\"sheet1\">目标产量/日</td><td class=\"sheet2\">" + Robot_1_plan + "</td></tr>"
	    +"<tr><td class=\"sheet1\">实际产量/日</td><td class=\"sheet2\">" + Robot_1_day + "</td></tr>"
        + "<tr><td class=\"sheet1\">合格产量/日</td><td class=\"sheet2\">" + Robot_1_day + "</td></tr>"
        + "<tr><td class=\"sheet1\">报废数量/日</td><td class=\"sheet2\">" + 0 + "</td></tr>"
        + "<tr><td class=\"sheet1\">加工达成率</td><td class=\"sheet2\">" + ((Robot_1_plan == 0) ? 0 : (Robot_1_day*100.0/Robot_1_plan)).toFixed(0) + "%</td></tr>"
        + "<tr><td class=\"sheet1\">报废率</td><td class=\"sheet2\">"
                    + 0 + "%</td></tr>"
        + "<tr><td class=\"sheet1\">累计产量/月</td><td class=\"sheet2\">" + Robot_1_month + "</td></tr>";
    //alert(str_tar_lt);
    target_lt.append(str_tar_lt);
	
	var target_lb = $("#prod_num_toplb");
    target_lb.empty();
    var str_tar_lb = "";
	str_tar_lb += "<tr><td class=\"sheet1\">目标产量/日</td><td class=\"sheet2\">" + Robot_2_plan + "</td></tr>"
	    +"<tr><td class=\"sheet1\">实际产量/日</td><td class=\"sheet2\">" + Robot_2_day + "</td></tr>"
        + "<tr><td class=\"sheet1\">合格产量/日</td><td class=\"sheet2\">" + Robot_2_day + "</td></tr>"
        + "<tr><td class=\"sheet1\">报废数量/日</td><td class=\"sheet2\">" + 0 + "</td></tr>"
        + "<tr><td class=\"sheet1\">加工达成率</td><td class=\"sheet2\">" + ((Robot_2_plan == 0) ? 0 : (Robot_2_day*100.0/Robot_2_plan)).toFixed(0) + "%</td></tr>"
        + "<tr><td class=\"sheet1\">报废率</td><td class=\"sheet2\">"
                    + 0 + "%</td></tr>"
        + "<tr><td class=\"sheet1\">累计产量/月</td><td class=\"sheet2\">" + Robot_2_month + "</td></tr>";
    //alert(str_tar_lb);
    target_lb.append(str_tar_lb);
	
    var plan_prod = Robot_1_plan + Robot_2_plan;
    var prod_num = Robot_1_day + Robot_2_day;
    var week_prod = Robot_1_week + Robot_2_week;
    var month_prod = Robot_1_month + Robot_2_month;
    var year_prod = Robot_1_year + Robot_2_year;
    var w_prod = plan_prod - prod_num;
    var b_peod = (prod_num * 100 / plan_prod).toFixed(2)    
	
	var target_r = $("#prod_num_top");
    target_r.empty();
    var str_tar_r = "";
	str_tar_r += "<tr><td class=\"sheet3\">目标产量/日</td><td class=\"sheet4\">" + plan_prod + "</td></tr>"
	    +"<tr><td class=\"sheet3\">实际产量/日</td><td class=\"sheet4\">" + prod_num + "</td></tr>"
        + "<tr><td class=\"sheet3\">合格产量/日</td><td class=\"sheet4\">" + prod_num + "</td></tr>"
        + "<tr><td class=\"sheet3\">报废数量/日</td><td class=\"sheet4\">" + 0 + "</td></tr>"
        + "<tr><td class=\"sheet3\">加工达成率</td><td class=\"sheet4\">" + ((plan_prod == 0) ? 0 : (prod_num*100.0/plan_prod)).toFixed(0) + "%</td></tr>"
        + "<tr><td class=\"sheet3\">报废率</td><td class=\"sheet4\">"
                    + 0 + "%</td></tr>"
        + "<tr><td class=\"sheet3\">累计产量/月</td><td class=\"sheet4\">" + month_prod + "</td></tr>";
    //alert(str_tar_r);
    target_r.append(str_tar_r);

}
function procRealAlarmTotNum_B(data) {
    var target = $("#alarm_totnum_1");
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

    str_tar += "<div style=\"position: absolute; width: 300px; left: 10px; top: 490px; font-size: 18px; font-family: songti; color: #FFFFFF; z - index: 2\">" +
    "<b> 总报警次数 / 日：</b >" +
        "<b style=\"width: 80px; font - family: songti; font-size: 20px; color: #ff6a00\">" + alarm_daynum+"</b>" +
    "</div>" +
    "<div style=\"position: absolute; width: 300px; left: 10px; top: 530px; font-size: 18px; font-family: songti; color: #FFFFFF; z - index: 2\">" +
    "<b>总报警时间/日：</b>" +
        "<b style=\"width: 80px; font - family: songti; font - size: 20px; color: #ff6a00\">" + cur_alarm_str+"</b>" +
    "</div>" +
    "<div style=\"position: absolute; width: 300px; left: 10px; top: 570px; font-size: 18px; font-family: songti; color: #FFFFFF; z - index: 2\">" +
    "<b>报警累计时间/月：</b>" +
        "<b style=\"width: 80px; font - family: songti; font-size: 20px; color: #ff6a00\">" + mon_alarm_str+"</b>" +
    "</div>" +
    "<div style=\"position: absolute; width: 300px; left: 10px; top: 610px; font-size: 18px; font-family: songti; color: #FFFFFF; z - index: 2\">" +
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
        url: "../ReportTemplate/UpdateAPlanProdNum",
        success: alert('success')
    });
    $('#modal-6').modal('hide');
}
