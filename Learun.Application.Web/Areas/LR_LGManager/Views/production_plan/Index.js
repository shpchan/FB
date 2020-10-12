/* * 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2018 上海力软信息技术有限公司 
 * 创建人：超级管理员
 * 日  期：2019-09-18 10:52
 * 描  述：Production_plan
 */
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var startTime;
    var endTime;
    var page = {
        init: function () {
            page.initGird();
            var param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            page.initChart(param);
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
            $('#btn_Search').on('click', function () {
                var queryJson = $('#multiple_condition_query').lrGetFormData();
                page.search(queryJson);
            });
            $('#machine_id').lrDataSourceSelect({ code: 'main_machine', value: 'machine_id', text: 'machine_name' });
            //$('#state').lrDataItemSelect({ code: '' });
            $('#state').lrRadioCheckbox({
                type: 'checkbox',
                code: 'planState'
            }); 
            $("[name = 'state']").prop("checked", false);
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
                learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/LR_LGManager/Production_plan/Form',
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
                        url: top.$.rootUrl + '/LR_LGManager/Production_plan/Form?keyValue=' + keyValue,
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
                            learun.deleteForm(top.$.rootUrl + '/LR_LGManager/Production_plan/DeleteForm', { keyValue: keyValue }, function () {
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
                    url: top.$.rootUrl + '/Utility/ExcelExportForm?gridId=gridtable&filename=WorkOrder',
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
            $(".lr-layout-grid").height($(window).height() - 110);
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_LGManager/Production_plan/GetPageList',
                headData: [
                    {
                        label: "状态", name: "state", width: 55, align: "center",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            //改行满足的条件
                            if (value == "开始") {
                                //设置满足条件行的背景颜色
                                $($cell).css("background", "#00FA9A");
                            } else if (value == "新增") {
                                $($cell).css("background", "#3CB2EF");
                            }
                            else if (value == "暂停") {
                                $($cell).css("background", "#FFF065");
                            }
                            else if (value == "完成") {
                                $($cell).css("background", "#A9A9A9");
                            }
                            else if (value == "变更") {
                                $($cell).css("background", "#F37570");
                            }
                            //设置满足条件行的字体颜色
                            $($cell).css("color", "black");
                            callback(value);
                        }
                    },
                    { label: "计划名称", name: "plan_name", width: 100, align: "left" },
                    { label: "产品名称", name: "product_name", width: 100, align: "left" },
                    { label: "计划数量", name: "plan_amount", width: 100, align: "left" },
                    { label: "产量", name: "prod_num", width: 100, align: "left" },
                    {
                        label: "所属设备", name: "machine_id", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('custmerData', {
                                url: '/LR_SystemModule/DataSource/GetDataTable?code=' + 'machineinfo',
                                key: value,
                                keyId: 'machine_id',
                                callback: function (_data) {
                                    callback(_data['machine_name']);
                                }
                            });
                        }
                    },
                    { label: "计划时间", name: "start_time", width: 150, align: "left" },
                    { label: "创建人员", name: "create_user", width: 100, align: "left" },
                    { label: "操作人员", name: "operation_user", width: 100, align: "left" },
                ],
                reloadSelected: true,
                mainId: 'id',
                isAutoHeight: true,
                isPage: true,
                rows: 10
            });
        },
        initChart: function (queryJson) {
            loadmain(queryJson);
            loadmain1(queryJson);
            loadmain2(queryJson);
            function loadmain(queryJson) {
                var myChart = echarts.init(document.getElementById('main'));
                $.ajax({
                    url: top.$.rootUrl + '/LR_LGManager/Production_plan/GetChartList',
                    type: "post",
                    dataType: "json",
                    data: { queryJson: JSON.stringify(queryJson) },
                    success: function (data) {
                        var option = {
                            title: {
                                text: '生产计划完成情况分析图',
                                x: 'center'
                            },
                            tooltip: {
                                trigger: 'item',
                                formatter: "{a} <br/>{b} : {c} ({d}%)"
                            },
                            legend: {
                                orient: 'vertical',
                                left: 'left',
                                itemGap: 7,
                                data: data.data
                            },
                            series: [
                                {
                                    name: '生产计划',
                                    type: 'pie',
                                    radius: '65%',
                                    center: ['50%', '58%'],
                                    data: data.data,
                                    itemStyle: {
                                        emphasis: {
                                            shadowBlur: 10,
                                            shadowOffsetX: 0,
                                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                                        }
                                    }
                                }
                            ]
                        };
                        myChart.setOption(option);
                    }
                });
            }
            function loadmain1() {
                var myChart = echarts.init(document.getElementById('main1'));
                $.ajax({
                    url: top.$.rootUrl + '/LR_LGManager/Production_plan/GetBarList',
                    type: "post",
                    dataType: "json",
                    data: { queryJson: JSON.stringify(queryJson) },
                    success: function (data) {
                        var option = {
                            title: {
                                text: '生产计划产量统计图',
                                x: 'center'
                            },
                            tooltip: {
                                trigger: 'axis'
                            },
                            grid: {
                                left: '5%',
                                right: '5%',
                                bottom: '3%',
                                containLabel: true
                            },
                            xAxis: {
                                type: 'category',
                                data: data.data.name
                            },
                            yAxis: {
                                type: 'value'
                            },
                            series: [
                                {
                                    name: '计划产量',
                                    type: 'bar',
                                    stack: '总量',
                                    data: data.data.value,
                                    itemStyle: {
                                        normal: {
                                            //每根柱子颜色设置
                                            color: function (params) {
                                                let colorList = [
                                                    "#c23531",
                                                    "#2f4554",
                                                    "#61a0a8",
                                                    "#d48265",
                                                    "#91c7ae",
                                                    "#749f83",
                                                    "#ca8622",
                                                    "#bda29a",
                                                    "#6e7074",
                                                    "#546570",
                                                    "#c4ccd3",
                                                    "#4BABDE",
                                                    "#FFDE76",
                                                    "#E43C59",
                                                    "#37A2DA"
                                                ];
                                                return colorList[params.dataIndex];
                                            }
                                        }
                                    }
                                }
                            ]
                        };
                        myChart.setOption(option);
                    }
                });
            }
            function loadmain2() {
                var myChart = echarts.init(document.getElementById('main2'));
                $.ajax({
                    url: top.$.rootUrl + '/LR_LGManager/Production_plan/GetLineList',
                    type: "post",
                    dataType: "json",
                    data: { queryJson: JSON.stringify(queryJson) },
                    success: function (data) {
                        var option = {
                            title: {
                                text: '生产计划完成趋势图',
                                x: 'center'
                            },
                            tooltip: {
                                trigger: 'axis'
                            },
                            legend: {
                                orient: 'vertical',
                                left: 'left',
                                itemGap: 7,
                                data: ['完成']
                            },
                            grid: {
                                top: '20%',
                                left: '5%',
                                right: '7%',
                                bottom: '3%',
                                containLabel: true
                            },
                            xAxis: {
                                type: 'category',
                                boundaryGap: false,
                                axisLabel: {
                                    interval: 0,
                                    rotate: 45
                                },
                                data: data.data.time
                            },
                            yAxis: {
                                type: 'value'
                            },
                            series: [

                                {
                                    name: '完成',
                                    type: 'line',
                                    data: data.data.end
                                }
                            ]
                        };

                        myChart.setOption(option);
                    }
                });
            }
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
            page.initChart(param);
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
