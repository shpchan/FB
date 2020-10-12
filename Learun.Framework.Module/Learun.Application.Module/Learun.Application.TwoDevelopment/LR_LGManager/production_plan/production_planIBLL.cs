using Learun.Application.TwoDevelopment.LR_LGManager;
using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-09-18 10:52
    /// 描 述：Production_plan
    /// </summary>
    public interface Production_planIBLL
    {
        #region  获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<tb_production_planEntity> GetPageList(Pagination pagination, string queryJson);

        IEnumerable<tb_production_planEntity> GetPlan_managePageList(Pagination pagination, string queryJson);
        IEnumerable<tb_production_planEntity> GetChartList(string queryJson);
        IEnumerable<tb_production_planEntity> GetBarList(string queryJson);
        IEnumerable<tb_production_planEntity> GetLineList(string queryJson);
        int GetNowProdNum(string queryJson);
        /// <summary>
        /// 获取tb_production_plan表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        tb_production_planEntity Gettb_production_planEntity(string keyValue);
        bool IsExistPlan(string plan_name);

        bool IsStartPlanExist(int? machine_id, string keyvalue);
        IEnumerable<tb_machine_infoEntity> GetAutoMachine();
        IEnumerable<tb_production_planEntity> GetPlanSelect(string machine_id);
        void SaveAutoRunForm(string keyValue,string checkAuto, List<string> id);
        List<string> AutoRunStateMachine();
        List<tb_production_planEntity> GetAutoRunData(string keyValue);
        void RefreshProdNum();
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
        void SaveEntity(string keyValue, tb_production_planEntity tb_production_plan, tb_production_planEntity entity);

        void SaveChangeEntity(string keyValue, tb_production_planEntity tb_production_plan, tb_production_planEntity entity);
        #endregion

    }
}
