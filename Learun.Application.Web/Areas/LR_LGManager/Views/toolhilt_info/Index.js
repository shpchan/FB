/* * 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2018 上海力软信息技术有限公司 
 * 创建人：超级管理员
 * 日  期：2019-12-04 16:13
 * 描  述：toolhilt_info
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
            $('#toolHilt_name').lrDataItemSelect({ code: 'toolHilt_name' });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
               learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/LR_LGManager/toolhilt_info/Form',
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
                        url: top.$.rootUrl + '/LR_LGManager/toolhilt_info/Form?keyValue=' + keyValue,
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
                            learun.deleteForm(top.$.rootUrl + '/LR_LGManager/toolhilt_info/DeleteForm', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/LR_LGManager/toolhilt_info/GetPageList',
                headData: [
                    { label: "刀柄名称", name: "toolHilt_name", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'toolHilt_name',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                    { label: "刀柄码", name: "toolHilt_id", width: 100, align: "left"},
                    { label: "RFID", name: "rfid", width: 100, align: "left"},
                    { label: "刀柄类型", name: "toolblade_type", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'toolblade_type',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                    { label: "厂家", name: "tool_cj", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'hilt_cj',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
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
