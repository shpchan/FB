/* * 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2018 上海力软信息技术有限公司 
 * 创建人：超级管理员
 * 日  期：2019-12-04 16:43
 * 描  述：ToolInspection
 */
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            $('#multiple_condition_query').lrMultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 400);
            $('#wf_id').lrDataItemSelect({ code: 'wf_id' });
            $('#toolhilt_id').lrDataSourceSelect({ code: 'group_name',value: 'group_id',text: 'group_name' });
            $('#tool_id').lrDataSourceSelect({ code: 'group_name',value: 'group_id',text: 'group_name' });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
               learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/LR_LGManager/ToolInspection/Form',
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
                        url: top.$.rootUrl + '/LR_LGManager/ToolInspection/Form?keyValue=' + keyValue,
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
                            learun.deleteForm(top.$.rootUrl + '/LR_LGManager/ToolInspection/DeleteForm', { keyValue: keyValue}, function () {
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
        },
        // 初始化列表
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_LGManager/ToolInspection/GetPageList',
                headData: [
                    { label: "生产线", name: "machine_group", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('custmerData', {
                                 url: '/LR_SystemModule/DataSource/GetDataTable?code=' + 'group_name',
                                 key: value,
                                 keyId: 'group_id',
                                 callback: function (_data) {
                                     callback(_data['group_name']);
                                 }
                             });
                        }},
                    { label: "产品编码", name: "product_id", width: 100, align: "left"},
                    { label: "流程编号", name: "wf_id", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'wf_id',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                    { label: "刀柄码", name: "toolhilt_id", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('custmerData', {
                                 url: '/LR_SystemModule/DataSource/GetDataTable?code=' + 'group_name',
                                 key: value,
                                 keyId: 'group_id',
                                 callback: function (_data) {
                                     callback(_data['group_name']);
                                 }
                             });
                        }},
                    { label: "刀具码", name: "tool_id", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('custmerData', {
                                 url: '/LR_SystemModule/DataSource/GetDataTable?code=' + 'group_name',
                                 key: value,
                                 keyId: 'group_id',
                                 callback: function (_data) {
                                     callback(_data['group_name']);
                                 }
                             });
                        }},
                    { label: "刀片编号", name: "toolblade_id", width: 100, align: "left"},
                    { label: "刀位码", name: "toolpos_id", width: 100, align: "left"},
                    { label: "长度", name: "toollength", width: 100, align: "left"},
                    { label: "直径", name: "tooldia", width: 100, align: "left"},
                    { label: "寿命", name: "toollife", width: 100, align: "left"},
                    { label: "操作人员", name: "oprater", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('user', {
                                 key: value,
                                 callback: function (_data) {
                                     callback(_data.name);
                                 }
                             });
                        }},
                    { label: "剩余寿命", name: "rest_life", width: 100, align: "left"},
                ],
                mainId:'id',
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#gridtable').jfGridSet('reload',{ queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
