/* * 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2018 上海力软信息技术有限公司 
 * 创建人：超级管理员
 * 日  期：2019-12-04 11:37
 * 描  述：fast刀具管理
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
            $('#tool_name').lrDataItemSelect({ code: 'tool_name' });
            $('#tool_cl').lrDataItemSelect({ code: 'tool_cl' });
            $('#tool_tc').lrDataItemSelect({ code: 'tool_tc' });
            $('#tool_cj').lrDataItemSelect({ code: 'tool_cj' });
            $('#tool_mrcj').lrDataItemSelect({ code: 'tool_rmcj' });
            $('#tool_mrcs').lrDataItemSelect({ code: 'tool_rmcs' });
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_LGManager/fast_tool_info/GetFormData?keyValue=' + keyValue, function (data) {
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
        var postData = {
            strEntity: JSON.stringify($('body').lrGetFormData())
        };
        $.lrSaveForm(top.$.rootUrl + '/LR_LGManager/fast_tool_info/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
