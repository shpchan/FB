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
    /// 日 期：2019-09-16 14:32
    /// 描 述：tool_model
    /// </summary>
    public class ToolModelService : RepositoryFactory
    {
        #region  获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<tb_toolmodel_infoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t1.id,
                t1.toolmodel_name,
                t1.toolmodel_id,
                t1.short_name,
                t1.count_type,
                t1.initial_life,
                t1.life_prediction,
                t1.tooltype_id,
                t1.machine_id,
                t1.tool_grp,
                t1.tool_pos,
                t1.state
                ");
                strSql.Append("  FROM tb_tooltype_info t ");
                strSql.Append("  INNER JOIN tb_toolmodel_info t1 ON t1.tooltype_id = t.id ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["tooltype_id"].IsEmpty())
                {
                    dp.Add("tooltype_id", "%" + queryParam["tooltype_id"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t1.tooltype_id Like @tooltype_id ");
                }
                if (!queryParam["toolmodel_name"].IsEmpty())
                {
                    dp.Add("toolmodel_name", "%" + queryParam["toolmodel_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t1.toolmodel_name Like @toolmodel_name ");
                }
                if (!queryParam["short_name"].IsEmpty())
                {
                    dp.Add("short_name", "%" + queryParam["short_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t1.short_name Like @short_name ");
                }
                return this.BaseRepository("BaseDb1").FindList<tb_toolmodel_infoEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取tb_toolmodel_info表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public tb_toolmodel_infoEntity Gettb_toolmodel_infoEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository("BaseDb1").FindEntity<tb_toolmodel_infoEntity>(t=>t.id == keyValue);
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
        /// 获取tb_tooltype_info表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public tb_tooltype_infoEntity Gettb_tooltype_infoEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository("BaseDb1").FindEntity<tb_tooltype_infoEntity>(keyValue);
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
            var db = this.BaseRepository("BaseDb1").BeginTrans();
            try
            {
                db.Delete<tb_toolmodel_infoEntity>(t => t.id == keyValue);
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
        public void SaveEntity(string keyValue, tb_tooltype_infoEntity entity,tb_toolmodel_infoEntity tb_toolmodel_infoEntity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    tb_toolmodel_infoEntity.Modify(keyValue);
                    this.BaseRepository("BaseDb1").Update(tb_toolmodel_infoEntity);
                }
                else
                {
                    tb_toolmodel_infoEntity.Create();
                    this.BaseRepository("BaseDb1").Insert(tb_toolmodel_infoEntity);
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
