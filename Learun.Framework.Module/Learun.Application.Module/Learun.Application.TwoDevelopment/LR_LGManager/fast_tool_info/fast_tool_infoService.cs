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
    /// 日 期：2019-12-04 11:37
    /// 描 述：fast刀具管理
    /// </summary>
    public class fast_tool_infoService : RepositoryFactory
    {
        #region  获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<tb_tool_infoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.id,
                t.tool_name,
                t.tool_id,
                t.tool_order,
                t.tool_gg,
                t.tool_cl,
                t.tool_tc,
                t.tool_cj,
                t.tool_mrcj,
                t.tool_mrcs
                ");
                strSql.Append("  FROM tb_tool_info t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["tool_name"].IsEmpty())
                {
                    dp.Add("tool_name",queryParam["tool_name"].ToString(), DbType.String);
                    strSql.Append(" AND t.tool_name = @tool_name ");
                }
                if (!queryParam["tool_id"].IsEmpty())
                {
                    dp.Add("tool_id", "%" + queryParam["tool_id"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.tool_id Like @tool_id ");
                }
                return this.BaseRepository("BaseDb1").FindList<tb_tool_infoEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取tb_tool_info表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public tb_tool_infoEntity Gettb_tool_infoEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository("BaseDb1").FindEntity<tb_tool_infoEntity>(keyValue);
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
                this.BaseRepository("BaseDb1").Delete<tb_tool_infoEntity>(t=>t.id == keyValue);
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
        public void SaveEntity(string keyValue, tb_tool_infoEntity entity)
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
