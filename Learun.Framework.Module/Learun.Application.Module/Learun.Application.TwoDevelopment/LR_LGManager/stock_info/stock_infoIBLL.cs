using Learun.Util;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-11-12 11:21
    /// 描 述：stock_info
    /// </summary>
    public interface stock_infoIBLL
    {
        #region  获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<tb_stock_infoEntity> GetPageList(Pagination pagination, string queryJson);
        IEnumerable<tb_stock_infoEntity> GetPageList_Check(Pagination pagination, string queryJson);
        IEnumerable<tb_stock_infoEntity> GetPageList_Edit(Pagination pagination, string queryJson);
        IEnumerable<tb_stock_infoEntity> GetPageList_Diff(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取tb_stock_info表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        tb_stock_infoEntity Gettb_stock_infoEntity(string keyValue);
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
        void SaveEntity(string keyValue, tb_stock_infoEntity entity);
        #endregion

        #region  验证数据
        /// <summary>
        /// 账户不能重复
        /// </summary>
        /// <param name="account">账户值</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistAccount(string account, string keyValue);
        #endregion
    }
}
