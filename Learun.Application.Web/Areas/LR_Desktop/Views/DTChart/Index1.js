﻿/* * 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2018 上海力软信息技术有限公司 
 * 创建人：超级管理员
 * 日  期：2018-09-25 11:32
 * 描  述：图标配置
 */
var refreshGirdData;
var selectedRow;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
                selectedRow = null;
                learun.layerForm({
                    id: "Form",
                    title: "添加",
                    url: top.$.rootUrl + "/LR_Desktop/DTChart/Form1",
                    width: 600,
                    height: 500,
                    btn: null
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                selectedRow = $("#gridtable").jfGridGet("rowdata");
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: "Form",
                        title: "编辑",
                        url: top.$.rootUrl + "/LR_Desktop/DTChart/Form1",
                        width: 600,
                        height: 500,
                        btn: null
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_Desktop/DTChart/DeleteForm1', { keyValue: keyValue}, function () {
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
                url: top.$.rootUrl + '/LR_Desktop/DTChart/GetPageList1',
                headData: [{
                        label: "名称",
                        name: "F_Name",
                        width: 150,
                        align: "left"
                    },
                    {
                        label: "图标",
                        name: "F_Icon",
                        width: 60,
                        align: "center",
                        formatter: function (d) {
                            return '<i class="' + d + '" ></i>';
                        }
                    },
                    {
                        label: "创建用户",
                        name: "F_CreateUserName",
                        width: 100,
                        align: "left"
                    },
                    {
                        label: "创建时间",
                        name: "F_CreateDate",
                        width: 130,
                        align: "left" 
                    },
                    {
                        label: "说明",
                        name: "F_Description",
                        width: 130,
                        align: "left"
                    }],
                mainId:'F_Id',
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
