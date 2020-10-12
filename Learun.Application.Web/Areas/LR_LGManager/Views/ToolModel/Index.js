/* * 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2018 上海力软信息技术有限公司 
 * 创建人：超级管理员
 * 日  期：2019-09-16 14:32
 * 描  述：tool_model
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
            // 初始化左侧树形数据
            $('#dataTree').lrtree({
                url: top.$.rootUrl + '/LR_SystemModule/DataSource/GetTree?code=tooltype_info&parentId=parent_id&Id=id&showId=tooltype_name',
                nodeClick: function (item) {
                    //alert(item.value);
                    page.search({ tooltype_id: item.value });
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
                    url: top.$.rootUrl + '/LR_LGManager/ToolModel/Form',
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
                        url: top.$.rootUrl + '/LR_LGManager/ToolModel/Form?keyValue=' + keyValue,
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
                            learun.deleteForm(top.$.rootUrl + '/LR_LGManager/ToolModel/DeleteForm', { keyValue: keyValue}, function () {
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
            // 装刀
            $('#lr_zd').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('id');
                var state = $('#gridtable').jfGridValue('state');
                if (state == "已装刀") {
                    alert("已装刀,请先卸刀！");
                    return;
                }
                else {
                    if (learun.checkrow(keyValue)) {
                        learun.layerForm({
                            id: 'form',
                            title: '装刀',
                            url: top.$.rootUrl + '/LR_LGManager/ToolModel/zhuangdao?keyValue=' + keyValue,
                            width: 600,
                            height: 400,
                            callBack: function (id) {
                                return top[id].acceptClick(refreshGirdData);
                            }
                        });
                    }
                }
            });
            // 卸刀
            $('#lr_xd').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('id');
                var state = $('#gridtable').jfGridValue('state');
                if (state == "" || state == "已卸刀" || state == null) {
                    alert("已卸刀,请先装刀！");
                    return;
                }
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '卸刀',
                        url: top.$.rootUrl + '/LR_LGManager/ToolModel/xiedao?keyValue=' + keyValue,
                        width: 600,
                        height: 400,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 修改
            $('#lr_change').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('id');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '修改',
                        url: top.$.rootUrl + '/LR_LGManager/ToolModel/change?keyValue=' + keyValue,
                        width: 600,
                        height: 400,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
        },
        // 初始化列表
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_LGManager/ToolModel/GetPageList',
                headData: [
                    { label: "刀具名称", name: "toolmodel_name", width: 100, align: "left"},
                    { label: "刀具编号", name: "toolmodel_id", width: 100, align: "left"},
                    { label: "前缀", name: "short_name", width: 100, align: "left"},
                    { label: "计数方式", name: "count_type", width: 100, align: "left"},
                    { label: "初始寿命", name: "initial_life", width: 100, align: "left"},
                    { label: "预警值", name: "life_prediction", width: 100, align: "left"},
                    { label: "刀具类型", name: "tooltype_id", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('custmerData', {
                                 url: '/LR_SystemModule/DataSource/GetDataTable?code=' + 'tooltype_info',
                                 key: value,
                                 keyId: 'id',
                                 callback: function (_data) {
                                     callback(_data['tooltype_name']);
                                 }
                             });
                        }
                    },
                    { label: "分组", name: "tool_grp", width: 100, align: "left" },
                    { label: "刀位", name: "tool_pos", width: 100, align: "left" },
                    { label: "设备", name: "machine_id", width: 100, align: "left" },
                    { label: "状态", name: "state", width: 100, align: "left" }
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
