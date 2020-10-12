/* * 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2018 上海力软信息技术有限公司 
 * 创建人：超级管理员
 * 日  期：2019-09-18 10:52
 * 描  述：Production_plan
 */
var refreshGirdData;
var autoRefresh;
var bootstrap = function ($, learun) {
    "use strict";
    var startTime;
    var endTime;
    var page = {
        init: function () {
            page.initGird();
            page.bind();
            autoRefresh();
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
            $('#btn_Search').on('click', function () {
                var queryJson = $('#multiple_condition_query').lrGetFormData();
                page.search(queryJson);
            });
            $('#machine_id').lrDataSourceSelect({ code: 'main_machine', value: 'machine_id', text: 'machine_name' });
            $('#state').lrDataItemSelect({ code: '' });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
                learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/LR_LGManager/Plan_manage/Form',
                    width: 600,
                    height: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('id');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_LGManager/Plan_manage/Form?keyValue=' + keyValue,
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
                var keyValue = $('#gridtable').jfGridValue('id');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_LGManager/Plan_manage/DeleteForm', { keyValue: keyValue }, function () {
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

            $("#autoMachine").empty();
            $.ajax({
                type: "post",
                dataType: "json",
                url: top.$.rootUrl + '/LR_LGManager/Plan_manage/GetAutoMachine',
                success: addButton
            });
            function addButton(data) {
                $("#autoMachine").empty();
                for (var i = 0; i < data.data.length; i++) {
                    $("#autoMachine").append("<a class='btn btn-tool btn-sm' name='auto' id=" + data.data[i].machine_id + "><i class='fa fa-cogs'></i>&nbsp;" + data.data[i].machine_name + "</a>&nbsp;");
                }
            }
            
            $(document).on('click', '.btn-tool', function () {
                var keyValue = $(this).attr('id');
                learun.layerForm({
                    id: 'form',
                    title: '排序',
                    url: top.$.rootUrl + '/LR_LGManager/Plan_manage/AutoRun?keyValue=' + keyValue,
                    width: 450,
                    height: 550,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
        },
        // 初始化列表
        initGird: function () {
            $(".lr-layout-grid").height($(window).height() - 110);
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_LGManager/Plan_manage/GetPlan_managePageList',
                headData: [
                    //{
                    //    label: "状态", name: "state", width: 55, align: "center",
                    //    formatterAsync: function (callback, value, row, op, $cell) {
                    //        //改行满足的条件
                    //        if (value == "开始") {
                    //            //设置满足条件行的背景颜色
                    //            $($cell).css("background", "#00FA9A");
                    //        } else if (value == "新增") {
                    //            $($cell).css("background", "#3CB2EF");
                    //        }
                    //        else if (value == "暂停") {
                    //            $($cell).css("background", "#FFF065");
                    //        }
                    //        else if (value == "完成") {
                    //            $($cell).css("background", "#A9A9A9");
                    //        }
                    //        else if (value == "变更") {
                    //            $($cell).css("background", "#F37570");
                    //        }
                    //        //设置满足条件行的字体颜色
                    //        $($cell).css("color", "black");
                    //        callback(value);
                    //    }
                    //},
                    { label: "生产工单号", name: "plan_name", width: 100, align: "left" },
                    { label: "生产序列", name: "sort", width: 100, align: "left"},
                    {
                        label: "生产线编码", name: "machine_id", width: 100, align: "left"
                        //formatterAsync: function (callback, value, row, op, $cell) {
                        //    learun.clientdata.getAsync('custmerData', {
                        //        url: '/LR_SystemModule/DataSource/GetDataTable?code=' + 'machineinfo',
                        //        key: value,
                        //        keyId: 'machine_id',
                        //        callback: function (_data) {
                        //            callback(_data['machine_name']);
                        //        }
                        //    });
                        //}
                    },
                    { label: "零件编码", name: "product_name", width: 100, align: "left" },
                    { label: "工单计划数量", name: "plan_amount", width: 100, align: "left" },
                    { label: "当前产量", name: "prod_num", width: 100, align: "left" },
                    
                    { label: "计划生产日期", name: "start_time", width: 150, align: "left" },
                    //{ label: "创建人员", name: "create_user", width: 100, align: "left" },
                    //{ label: "操作人员", name: "operation_user", width: 100, align: "left" },
                    //{
                    //    label: "排序", name: "sort", width: 100, align: "left",
                    //    formatterAsync: function (callback, value, row, op, $cell) {
                    //        //改行满足的条件
                    //        if (value == "0") {
                    //            value = "无";
                    //        }
                    //        callback(value);
                    //    }
                    //},
                    //{
                    //    label: "自动/手动", name: "auto", width: 100, align: "left",
                    //    formatterAsync: function (callback, value, row, op, $cell) {
                    //        //改行满足的条件
                    //        if (value == "1") {
                    //            value = "自动";
                    //        }
                    //        else {
                    //            value = "手动";
                    //        }
                    //        callback(value);
                    //    }
                    //},
                ],
                reloadSelected: true,
                mainId: 'id',
                isAutoHeight: true,
                isPage: true,
                rows: 15
            });
        },

        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        }
       
    };
    refreshGirdData = function () {
        page.search();
    };
    autoRefresh = function () {
        $("#btn_Search").click();
        $.ajax({
            type: "post",
            dataType: "json",
            url: top.$.rootUrl + '/LR_LGManager/Plan_manage/AutoRunStateMachine',
            success: changeButtonColor
        });
        function changeButtonColor(data) {
           
            $(".btn-tool").css({ 'background-color': '#F08080' });
            $(".btn-tool").css({ 'border-color': '#F08080' });
            for (var i = 0; i < data.data.length; i++)
            {  
                $("#" + data.data[i] + "").css({ 'background-color': '#00FF7F' });
                $("#" + data.data[i] + "").css({ 'border-color': '#00FF7F' });
            }
            
           
        }
        setTimeout(autoRefresh, 30000);
    };
    page.init();
}
