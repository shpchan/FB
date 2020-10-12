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
            $('#machine_id').lrDataSourceSelect({ code: 'main_machine',value: 'machine_id',text: 'machine_name' });
            
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_LGManager/Production_plan/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#' + id).jfGridSet('refreshdata', data[id]);
                        }
                        else {
                            $("#plan_name").attr("disabled", "disabled");
                            $("#product_name").attr("disabled", "disabled");
                            $("#machine_id").attr("disabled", "disabled");
                            $("select option[value='新增']").hide();
                            $('[data-table="' + id + '"]').lrSetFormData(data[id]);
                        }
                    }
                });
            } else {
                $("select option[value='完成']").hide();
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
        $.lrSaveForm(top.$.rootUrl + '/LR_LGManager/Production_plan/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
