﻿using Learun.Util;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-04-22 10:08
    /// 描 述：班次计划产量维护
    /// </summary>
    public interface Plan_WshiftIBLL
    {
        #region  获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<tb_plan_wshift_dataEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取tb_plan_wshift_data表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        tb_plan_wshift_dataEntity Gettb_plan_wshift_dataEntity(string keyValue);
        #endregion

        #region  提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        void SaveEntity(string keyValue, tb_plan_wshift_dataEntity entity);
        #endregion

    }
}
