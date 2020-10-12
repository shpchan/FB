/* * 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2018 上海力软信息技术有限公司 
 * 创建人：超级管理员
 * 日  期：2019-11-12 11:21
 * 描  述：stock_info
 */
var refreshGirdData;
var refreshGirdData1;
var bootstrap = function ($, learun) {
    "use strict";
    var processId = '';
    var startTime;
    var endTime;
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
            $('#multiple_condition_query').lrMultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 400);
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
               learun.layerForm({
                    id: 'form',
                    title: '新增',
                   url: top.$.rootUrl + '/LR_LGManager/stock_info_diff/Form',
                    width: 600,
                    height: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData1);
                    }
                   //callBack: function (id) {

                   //    var res = true;
                   //    // 保存数据
                   //    if (res) {
                   //        processId = keyValue;
                   //        res = top[id].save(processId, refreshGirdData);
                   //    }
                   //    return res;
                   //}
                });
            });
            // 查看详情
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('id');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm_Read({
                        id: 'form',
                        title: '查看详情',
                        url: top.$.rootUrl + '/LR_LGManager/stock_info_diff/Form?keyValue=' + keyValue,
                        width: 600,
                        height: 400,
                        //callBack: function (id) {
                        //    return top[id].acceptClick(refreshGirdData);
                        //}
                        callBack: function (id) {

                            var res = true;
                            // 保存数据
                            if (res) {
                                processId = keyValue;
                                res = top[id].save(processId, refreshGirdData1);
                            }
                            return res;
                        }
                    });
                }
            });
            // 提交审核
            $('#lr_submit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('id');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否提交审核该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_LGManager/stock_info_diff/SubmitForm', { keyValue: keyValue }, function () {
                                refreshGirdData1();
                            });
                        }
                    });
                }
            });
            // 刷新库存
            $('#lr_calc').on('click', function () {
                    learun.layerConfirm('是否手动刷新即时库存？', function (res) {
                        if (res) {
                            learun.postForm(top.$.rootUrl + '/LR_LGManager/stock_info_diff/Recalculator', function () {
                                refreshGirdData1();
                            });
                        }
                    });
            });
            // 通过审核
            $('#lr_agree').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('id');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否通过审核该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_LGManager/stock_info_diff/AgreeForm', { keyValue: keyValue }, function () {
                                refreshGirdData1();
                            });
                        }
                    });
                }
            });
            // 驳回审核
            $('#lr_disagree').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('id');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否驳回审核该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_LGManager/stock_info_diff/DisAgreeForm', { keyValue: keyValue }, function () {
                                refreshGirdData1();
                            });
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
                            learun.deleteForm(top.$.rootUrl + '/LR_LGManager/stock_info_diff/DeleteForm', { keyValue: keyValue}, function () {
                                refreshGirdData1();
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
                learun.download({
                    method: 'POST',
                    url: '/Utility/ExportExcel',
                    param: {
                        fileName: '标准化BOM表头',
                        columnJson: JSON.stringify($('#gridtable').jfGridGet('settingInfo').headData),
                        dataJson: JSON.stringify($('#gridtable').jfGridGet('settingInfo').rowdatas)
                    }
                });
            });
        },
        // 初始化列表
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_LGManager/stock_info_diff/GetPageList',
                headData: [
                    { label: "展开ID", name: "open_id", width: 100, align: "left"},
                    { label: "下发时间", name: "start_time", width: 100, align: "left"},
                    { label: "展开时间", name: "open_time", width: 100, align: "left"},
                    { label: "平台名称", name: "platform_name", width: 100, align: "left"},
                    { label: "设计员", name: "designer", width: 100, align: "left"},
                    { label: "平台编码", name: "platform_id", width: 100, align: "left"},
                    { label: "单元名称", name: "unit_name", width: 100, align: "left"},
                    { label: "单元编码", name: "unit_id", width: 100, align: "left"},
                    { label: "序号", name: "order_number", width: 100, align: "left"},
                    { label: "类型", name: "type", width: 100, align: "left"},
                    { label: "零件编码", name: "element_id", width: 100, align: "left"},
                    { label: "名称", name: "element_name", width: 100, align: "left"},
                    { label: "材质", name: "element_cz", width: 100, align: "left"},
                    { label: "型号规格", name: "element_gg", width: 100, align: "left"},
                    { label: "重量", name: "element_zl", width: 100, align: "left"},
                    { label: "单套数量", name: "single_num", width: 100, align: "left"},
                    { label: "单位", name: "element_unit", width: 100, align: "left"},
                    { label: "表面处理", name: "skin_stress", width: 100, align: "left"},
                    { label: "热处理", name: "hot_stress", width: 100, align: "left"},
                    { label: "颜色", name: "element_color", width: 100, align: "left"},
                    { label: "供应商", name: "element_supplier", width: 100, align: "left" },
                    { label: "安全库存", name: "safe_number", width: 100, align: "left" },
                    { label: "即时库存", name: "store_number", width: 100, align: "left" },
                    { label: "差异", name: "diff_number", width: 100, align: "left" },
                    { label: "状态", name: "state", width: 100, align: "left" },
                ],
                mainId:'id',
                isPage: true
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            if (request('platform_name') != null && request('platform_name') != undefined && request('platform_name') != "") {
                param.platform_name = decodeURI(request('platform_name'));
            }
            if (request('unit_name') != null && request('unit_name') != undefined && request('unit_name') != "") {
                param.unit_name = decodeURI(request('unit_name'));
            }
            $('#gridtable').jfGridSet('reload',{ queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData1 = function () {
        page.search();
    };
    // 保存数据后回调刷新
    refreshGirdData = function (res, postData) {
        if (res.code == 200) {
            // 发起流程
            learun.workflowapi.create({
                isNew: true,
                schemeCode: 'stockbom',
                processId: processId,
                processName: '标准化BOM流程',
                processLevel: '1',
                description: '发起修改',
                formData: JSON.stringify(postData),
                callback: function (res, data) {
                }
            });

            console.log(res, processId);
            page.search();
        }


    }
    page.init();
}
