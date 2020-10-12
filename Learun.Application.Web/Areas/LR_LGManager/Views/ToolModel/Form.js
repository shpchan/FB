/* * 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2018 上海力软信息技术有限公司 
 * 创建人：超级管理员
 * 日  期：2019-09-16 14:32
 * 描  述：tool_model
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
            $('#tooltype_id').lrDataSourceSelect({ code: 'tooltype_info',value: 'id',text: 'tooltype_name' });
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_LGManager/ToolModel/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#' + id ).jfGridSet('refreshdata', data[id]);
                        }
                        else {
                            $('[data-table="' + id + '"]').lrSetFormData(data[id]);
                        }
                    }
                });
            }
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
        $.lrSaveForm(top.$.rootUrl + '/LR_LGManager/ToolModel/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
