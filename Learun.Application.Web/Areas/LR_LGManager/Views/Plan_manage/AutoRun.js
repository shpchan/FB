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

            $("#sort").empty();
            for (var i = 0; i < 10; i++) {
                $("#sort").append("<div class='col-xs-12 lr-form-item' data-table='tb_production_plan'><div class='lr-form-item-title'>计划-" + (i + 1) + "<font face='宋体'>*</font></div><div><select id='machinePlan" + i + "' style='width:300px;height:28px'></select></div ></div> ");
            }

            $.ajax({
                type: "post",
                dataType: "json",
                data: { machine_id: keyValue},
                url: top.$.rootUrl + '/LR_LGManager/Plan_manage/GetPlanSelect',
                success: addSelect

            });
            function addSelect(data) {
                for (var j = 0; j < 10; j++) {
                    $("#machinePlan" + j + "").append(" <option > </option >");
                    for (var i = 0; i < data.data.length; i++) {
                        if (data.data[i].state == "开始"||data.data[i].sort==1) {
                            $("#machinePlan" + j + "").append(" <option value=" + data.data[i].id + " disabled='disabled'> " + data.data[i].plan_name + "</option >");
                        }
                        else {
                            $("#machinePlan" + j + "").append(" <option value=" + data.data[i].id + "> " + data.data[i].plan_name + "</option >");
                        }
                        
                    }
                }   
            }
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_LGManager/Plan_manage/GetAutoRunData?keyValue=' + keyValue, function (data) {
                    for (var i = 0; i < data.length; i++) { 
                        $("#machinePlan" + i + "").find("option[value = '" + data[i].id + "']").attr("selected", "selected");
                        if (data[i].state == "开始" || data[i].sort==1) {
                            $("#machinePlan" + i + "").attr("disabled", "disabled");
                            $("#machinePlan" + i + "").attr("style", "width:300px;height:28px;background: #CCCCCC");
                        }  
                    }
                });
            } 
        }
    };
    // 保存数据
    acceptClick = function (callBack,data) {
        if (!$('body').lrValidform()) {
            return false;
        }
        var postData = {
            strEntity: JSON.stringify($('body').lrGetFormData()),
            state: $('#state').val()
        };     
        $.lrSaveForm(top.$.rootUrl + '/LR_LGManager/Plan_manage/SaveAutoRunForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
