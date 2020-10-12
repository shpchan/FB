/* * 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2018 上海力软信息技术有限公司 
 * 创建人：超级管理员
 * 日  期：2019-09-18 10:52
 * 描  述：Production_plan
 */
var acceptClick;
var keyValue = request('keyValue');
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            $('.lr-form-wrap').lrscroll();
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#machine_id').lrDataSourceSelect({ code: 'main_machine', value: 'machine_id', text: 'machine_name' });

        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_LGManager/Plan_manage/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#' + id).jfGridSet('refreshdata', data[id]);
                        }
                        else {
                            if (data[id].state == "新增") {
                                $("select option[value='新增']").hide();
                                $("select option[value='暂停']").hide();
                                $("select option[value='完成']").hide();
                            }
                            else if (data[id].state == "开始" || data[id].state == "暂停") {
                                $("select option[value='新增']").hide();
                            }
                            $("#plan_name").attr("disabled", "disabled");
                            $("#plan_name").attr("style", "background: #CCCCCC");
                            $("#product_name").attr("disabled", "disabled");
                            $("#product_name").attr("style", "background: #CCCCCC");
                            $("#machine_id").attr("disabled", "disabled");
                            $("#machine_id").attr("style", "background: #CCCCCC");

                            var param = param || {};
                            param.StartTime = data[id].create_time;
                            param.machine_id = data[id].machine_id;
                            param.state = data[id].state;
                            param._id = data[id].id;
                            $.ajax({
                                type: "get",
                                dataType: "json",
                                data: { queryJson: JSON.stringify(param) },
                                url: top.$.rootUrl + '/LR_LGManager/Plan_manage/GetNowProdNum',
                                success: function (data) {
                                    $("#prod_num").val(data);
                                    $("#prod_num").attr("style", "background: #CCCCCC");
                                }
                            });
                            
                            $('[data-table="' + id + '"]').lrSetFormData(data[id]);
                        }
                    }
                });
            } else {
                $("#stateSelect").hide();
                //$("select option[value='开始']").hide();
                //$("select option[value='暂停']").hide();
                //$("select option[value='完成']").hide();
                $("#operation").remove();
                $("#prod").hide();
                $("#prod_num").val(0);
                
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').lrValidform()) {
            return false;
        }
        var postData = {
            strEntity: JSON.stringify($('body').lrGetFormData()),
            state: $('#state').val()
        };
        var pa = Number($("#plan_amount").val());
        var pn = Number($("#prod_num").val());
        if (pa > pn||$("#state").val()=='完成')
        {
            $.lrSaveForm(top.$.rootUrl + '/LR_LGManager/Plan_manage/SaveForm?keyValue=' + keyValue, postData, function (res) {
                // 保存成功后才回调
                if (!!callBack) {
                    callBack();
                }
            });
        }
        else {
            learun.alert.error('计划数量小于当前产量');
        }
       
    };
    page.init();
}
