/* * 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2018 上海力软信息技术有限公司 
 * 创建人：超级管理员
 * 日  期：2019-06-04 14:05
 * 描  述：刀具寿命管理
 */
var acceptClick;
var keyValue = request('keyValue');
var bootstrap = function ($, learun) {
    "use strict";
    $("#machine_id").change(function () { SelectLineChangeDevice(); });
    SelectLineChangeDevice();
    var page = {
        init: function () {
            $('.lr-form-wrap').lrscroll();
            page.bind();
            page.initData();
        },
        bind: function () {
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_LGManager/ToolModel/GetZhuangdaoData?keyValue=' + keyValue, function (data) {
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
        $.lrSaveForm(top.$.rootUrl + '/LR_LGManager/ToolModel/SaveForm_ZD?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
function SelectLineChangeDevice() {

    //获取下拉框选中项的value属性值
    var saLine = $("#machine_id").val();
    //alert(saLine);
    $.ajax({
        type: "get",
        dataType: "json",
        //url: "/PView/GetSLineDevice/" + saLine,
        url: top.$.rootUrl + '/LR_ReportModule/ReportTemplate/GetToolGrp/' + saLine,
        success: function (data) {
            var target = $("#tool_grp");
            target.empty();
            var tr_txt = " <option value=" + 0 + ">" + "请选择..." + "</option>";
            $.each(data, function (i, item) {
                tr_txt += " <option value=" + item.machine_id + ">" + item.machine_number + "</option>";
            });
            //alert(tr_txt);
            target.append(tr_txt);
        }
    });
    $.ajax({
        type: "get",
        dataType: "json",
        //url: "/PView/GetSLineDevice/" + saLine,
        url: top.$.rootUrl + '/LR_ReportModule/ReportTemplate/GetToolPos/' + saLine,
        success: function (data) {
            var target = $("#tool_pos");
            target.empty();
            var tr_txt = " <option value=" + 0 + ">" + "请选择..." + "</option>";
            $.each(data, function (i, item) {
                tr_txt += " <option value=" + item.machine_id + ">" + item.machine_number + "</option>";
            });
            //alert(tr_txt);
            target.append(tr_txt);
        }
    });
}
