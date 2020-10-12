/* * 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2018 上海力软信息技术有限公司 
 * 创建人：超级管理员
 * 日  期：2019-06-04 14:05
 * 描  述：刀具寿命管理
 */
var acceptClick;
var keyValue = request('keyValue');
var mydata = "";
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            $('.lr-form-wrap').lrscroll();
            page.bind();
            page.initData();
            setInterval(page.initMyData, 100);
        },
        bind: function () {
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_LGManager/ToolModel/GetChangeData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#' + id ).jfGridSet('refreshdata', data[id]);
                        }
                        else {
                            $('[data-table="' + id + '"]').lrSetFormData(data[id]);
                        }
                    }
                    mydata = $("#machine_id").val();
                });
            }
        },
        initMyData: function () {
            $("#f_machine_id").val(mydata);
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').lrValidform()) {
            return false;
        }
        var postData = {};
        postData.strtb_toolmodel_infoEntity = JSON.stringify($('[data-table="tb_toolmodel_info"]').lrGetFormData());
        postData.strEntity = JSON.stringify($('[data-table="tb_tooltype_info"]').lrGetFormData());
        $.lrSaveForm(top.$.rootUrl + '/LR_LGManager/ToolModel/SaveForm_Change?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
