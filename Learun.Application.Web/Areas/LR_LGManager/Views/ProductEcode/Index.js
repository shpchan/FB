﻿/* 创建人：超级管理员
 * 日  期：2019-01-08 13:48
 * 描  述：二维码追溯
 */
var refreshGirdData;
var prod_ecode_info = new Vue({
    el: '#prod_ecode_info_bom',
    data: {
        product_ecode: "",
        printed_time: "",
        begin_time: "",
        end_time: "",
        wshift_date: "",
        wshift_name: "",
        stage_desp: ""
    }
})
var bootstrap = function ($, learun) {   
    "use strict";
    var startTime;
    var endTime;
    var machine_id ;
    var group_id ;
    $("#aLine").change(function () { SelectLineChangeDevice(); });
    SelectLineChangeDevice();
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            // 时间搜索框
            $('#datesearch').lrdate({
                dfdata: [
                    { name: '今天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近7天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近1个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近3个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } }
                ],
                // 月
                mShow: false,
                premShow: false,
                // 季度
                jShow: false,
                prejShow: false,
                // 年
                ysShow: false,
                yxShow: false,
                preyShow: false,
                yShow: false,
                // 默认
                dfvalue: '1',
                selectfn: function (begin, end) {
                    startTime = begin;
                    endTime = end;
                    page.search();
                }
            });
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                machine_id = $('#MachineName').val();
                group_id = $('#aLine').val();
                page.searchs({ keyword: keyword, machine_id: machine_id, group_id: group_id });
                $("a.pagebtn")[0].click();

            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
               learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/LR_LGManager/ProductEcode/Form',
                    width: 600,
                    height: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_FilterTimeId');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_LGManager/ProductEcode/Form?keyValue=' + keyValue,
                        width: 600,
                        height: 400,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_FilterTimeId');
                if (learun.checkrow(keyValue)) {
                   learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_LGManager/ProductEcode/DeleteForm', { keyValue: keyValue}, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 打印
            $('#lr_print').on('click', function () {
                $('#gridtable').jqprintTable();
            });
            // 导出
            $('#lr_outport').on('click', function () {
                learun.layerForm({
                    id: "ExcelExportForm",
                    title: '导出Excel数据',
                    url: top.$.rootUrl + '/Utility/ExcelExportForm?gridId=gridtable&filename=LeaveInfo',
                    width: 500,
                    height: 380,
                    callBack: function (id) {
                        return top[id].acceptClick();
                    },
                    btn: ['导出Excel', '关闭']
                });
            });
        },
        // 初始化列表
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_LGManager/ProductEcode/GetPageList',
                headData: [
                    { label: "追溯码", name: "product_ecode", width: 100, align: "left"},
                    { label: "上线时间", name: "begin_time", width: 150, align: "left" },
                    { label: "下线时间", name: "end_time", width: 120, align: "left" },
                ],
                mainId:'product_ecode',
                isPage: true,
                onSelectRow: function (rowdata) {
                    var _product_ecode = rowdata.product_ecode;
                    console.log("_product_ecode:" + _product_ecode);
                    page.searchone({ keyword: _product_ecode });
                },
            });
            //
            $('#gridtable2').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_LGManager/ProductEcode/GetPageListOne',
                headData: [
                    { label: "日期", name: "calc_date", width: 80, align: "left" },
                    { label: "追溯码", name: "product_ecode", width: 100, align: "left" },
                    { label: "工位名称", name: "stage_mac_id", width: 100, align: "left" },
                    { label: "进站时间", name: "stage_time", width: 150, align: "left" },
                    { label: "出站时间", name: "prodtime", width: 150, align: "left" },
                    { label: "班次日期", name: "wshift_date", width: 80, align: "left" },
                    { label: "班次名称", name: "wshift_name", width: 80, align: "left" },
                    { label: "自动线", name: "stage_group_id", width: 160, align: "left" },
                    { label: "产品编号", name: "product_id", width: 50, align: "left" },
                    //{
                    //    label: "操作人员", name: "stage_emp_id", width: 100, align: "left",
                    //    formatterAsync: function (callback, value, row, op, $cell) {
                    //        learun.clientdata.getAsync('custmerData', {
                    //            url: '/LR_SystemModule/DataSource/GetDataTable?code=' + 'wshift_employee',
                    //            key: value,
                    //            keyId: 'stage_emp_id',
                    //            callback: function (_data) {
                    //                callback(_data['f_realname']);
                    //            }
                    //        });
                    //    }
                    //},
                ],
                mainId: 'stage_mac_id',
                isPage: true
            });
            //
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#gridtable').jfGridSet('reload',{ queryJson: JSON.stringify(param) });
        },
       searchs: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        },
        searchone: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime; 
            $('#gridtable2').jfGridSet('reload', { queryJson: JSON.stringify(param) });

            GetEcodeInfo(param.keyword, param.StartTime, param.EndTime);
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
function GetEcodeInfo(product_ecode, StartTime, EndTime) {
    console.log("GetEcodeInfo:" + product_ecode + "|" + StartTime + "|" + EndTime);
    $.ajax({
        type: "get",
        dataType: "json",
        url: top.$.rootUrl + '/LR_LGManager/ProductEcode/GetListOnce?product_ecode=' + product_ecode + "&StartTime=" + StartTime + "&EndTime=" + EndTime,
        success: function (data) {
            console.log("printed_time:" + data.data.rows[0].printed_time);
            prod_ecode_info.product_ecode = data.data.rows[0].product_ecode;
            prod_ecode_info.printed_time = data.data.rows[0].printed_time;
            prod_ecode_info.begin_time = data.data.rows[0].begin_time;
            prod_ecode_info.end_time = data.data.rows[0].end_time;
            prod_ecode_info.wshift_date = data.data.rows[0].wshift_date;
            prod_ecode_info.wshift_name = data.data.rows[0].wshift_name;
            prod_ecode_info.stage_desp = data.data.rows[0].stage_desp; 
        }
    });    
}
function SelectLineChangeDevice() {
    //获取下拉框选中项的value属性值
    var saLine = $("#aLine").val();
    $.ajax({
        type: "get",
        dataType: "json",
        //url: "/PView/GetSLineDevice/" + saLine,
        url: top.$.rootUrl + '/LR_ReportModule/ReportTemplate/GetSLineDevice/' + saLine,
        success: function (data) {
            var target = $("#MachineName");
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