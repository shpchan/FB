using Dapper;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-11-26 17:40
    /// 描 述：仓库即时库存
    /// </summary>
    public class store_infoService : RepositoryFactory
    {
        #region  获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<tb_store_infoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.id,
                t.materiel_id,
                t.supplier,
                t.store_id,
                t.store_name,
                t.storepos_id,
                t.storepos_name,
                t.buy_time,
                t.quality_period,
                t.due_date,
                t.baseunit,
                t.baseunit_num,
                t.comunit,
                t.comunit_num,
                t.assistunit,
                t.changenum,
                t.assistunit_num
                ");
                strSql.Append("  FROM tb_store_info t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.buy_time >= @startTime AND t.buy_time <= @endTime ) ");
                }
                if (!queryParam["materiel_id"].IsEmpty())
                {
                    dp.Add("materiel_id", "%" + queryParam["materiel_id"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.materiel_id Like @materiel_id ");
                }
                return this.BaseRepository("BaseDb1").FindList<tb_store_infoEntity>(strSql.ToString(),dp, pagination);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取tb_store_info表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public tb_store_infoEntity Gettb_store_infoEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository("BaseDb1").FindEntity<tb_store_infoEntity>(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        #endregion

        #region  提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                this.BaseRepository("BaseDb1").Delete<tb_store_infoEntity>(t=>t.id == keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void SaveEntity(string keyValue, tb_store_infoEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository("BaseDb1").Update(entity);
                }
                else
                {
                    entity.Create();
                    this.BaseRepository("BaseDb1").Insert(entity);
                }
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        #endregion

    }
}
