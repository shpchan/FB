using Dapper;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-11-12 11:21
    /// 描 述：stock_info
    /// </summary>
    public class stock_infoService : RepositoryFactory
    {
        #region  获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<tb_stock_infoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.id,
                t.open_id,
                t.start_time,
                t.open_time,
                t.platform_name,
                t.designer,
                t.platform_id,
                t.unit_name,
                t.unit_id,
                t.order_number,
                t.type,
                t.element_id,
                t.element_name,
                t.element_cz,
                t.element_gg,
                t.element_zl,
                t.single_num,
                t.element_unit,
                t.skin_stress,
                t.hot_stress,
                t.element_color,
                t.element_supplier,
                case when t.state='2' then '审核中'  when t.state='1' then '新增'  when t.state='3' then '通过同意' when t.state='4' then '审核驳回' when t.state='5' then '删除' when t.state='6' then '修改' else '' end as state
                ");
                strSql.Append("  FROM tb_stock_info t ");
                strSql.Append("  WHERE 1=1 and (state not in ('7') or state is null) ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.start_time >= @startTime AND t.start_time <= @endTime ) ");
                }
                if (!queryParam["platform_name"].IsEmpty())
                {
                    dp.Add("platform_name", "%" + queryParam["platform_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.platform_name Like @platform_name ");
                }
                if (!queryParam["unit_name"].IsEmpty())
                {
                    dp.Add("unit_name", "%" + queryParam["unit_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.unit_name Like @unit_name ");
                }
                if (!queryParam["element_id"].IsEmpty())
                {
                    dp.Add("element_id", "%" + queryParam["element_id"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.element_id Like @element_id ");
                }
                return this.BaseRepository("BaseDb1").FindList<tb_stock_infoEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<tb_stock_infoEntity> GetPageList_Check(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.id,
                t.open_id,
                t.start_time,
                t.open_time,
                t.platform_name,
                t.designer,
                t.platform_id,
                t.unit_name,
                t.unit_id,
                t.order_number,
                t.type,
                t.element_id,
                t.element_name,
                t.element_cz,
                t.element_gg,
                t.element_zl,
                t.single_num,
                t.element_unit,
                t.skin_stress,
                t.hot_stress,
                t.element_color,
                t.element_supplier,
                t.oprate,
                case when t.state='2' then '审核中'  when t.state='1' then '新增'  when t.state='3' then '通过同意' when t.state='4' then '审核驳回' when t.state='5' then '删除' when t.state='6' then '修改' else '' end as state
                ");
                strSql.Append("  FROM tb_stock_info t ");
                strSql.Append("  WHERE 1=1 and state  in ('2') ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.start_time >= @startTime AND t.start_time <= @endTime ) ");
                }
                if (!queryParam["platform_name"].IsEmpty())
                {
                    dp.Add("platform_name", "%" + queryParam["platform_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.platform_name Like @platform_name ");
                }
                if (!queryParam["unit_name"].IsEmpty())
                {
                    dp.Add("unit_name", "%" + queryParam["unit_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.unit_name Like @unit_name ");
                }
                if (!queryParam["element_id"].IsEmpty())
                {
                    dp.Add("element_id", "%" + queryParam["element_id"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.element_id Like @element_id ");
                }
                return this.BaseRepository("BaseDb1").FindList<tb_stock_infoEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<tb_stock_infoEntity> GetPageList_Edit(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.id,
                t.open_id,
                t.start_time,
                t.open_time,
                t.platform_name,
                t.designer,
                t.platform_id,
                t.unit_name,
                t.unit_id,
                t.order_number,
                t.type,
                t.element_id,
                t.element_name,
                t.element_cz,
                t.element_gg,
                t.element_zl,
                t.single_num,
                t.element_unit,
                t.skin_stress,
                t.hot_stress,
                t.element_color,
                t.element_supplier,
                t.safe_number,
                t.store_number,
                t.diff_number,
                case when t.state='2' then '审核中'  when t.state='1' then '新增'  when t.state='3' then '通过同意' when t.state='4' then '审核驳回' when t.state='5' then '删除' when t.state='6' then '修改' else '' end as state
                ");
                strSql.Append("  FROM tb_stock_info t ");
                strSql.Append("  WHERE 1=1 and (state is null or state in('2','3','5','6') or (state='4' and oprate<>'提交新增')) ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.start_time >= @startTime AND t.start_time <= @endTime ) ");
                }
                if (!queryParam["platform_name"].IsEmpty())
                {
                    dp.Add("platform_name", "%" + queryParam["platform_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.platform_name Like @platform_name ");
                }
                if (!queryParam["unit_name"].IsEmpty())
                {
                    dp.Add("unit_name", "%" + queryParam["unit_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.unit_name Like @unit_name ");
                }
                if (!queryParam["element_id"].IsEmpty())
                {
                    dp.Add("element_id", "%" + queryParam["element_id"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.element_id Like @element_id ");
                }
                return this.BaseRepository("BaseDb1").FindList<tb_stock_infoEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<tb_stock_infoEntity> GetPageList_Diff(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.id,
                t.open_id,
                t.start_time,
                t.open_time,
                t.platform_name,
                t.designer,
                t.platform_id,
                t.unit_name,
                t.unit_id,
                t.order_number,
                t.type,
                t.element_id,
                t.element_name,
                t.element_cz,
                t.element_gg,
                t.element_zl,
                t.single_num,
                t.element_unit,
                t.skin_stress,
                t.hot_stress,
                t.element_color,
                t.element_supplier,
                t.safe_number,
                t.store_number,
                t.diff_number,
                case when t.state='2' then '审核中'  when t.state='1' then '新增'  when t.state='3' then '通过同意' when t.state='4' then '审核驳回' when t.state='5' then '删除' when t.state='6' then '修改' else '' end as state
                ");
                strSql.Append("  FROM tb_stock_info t ");
                strSql.Append("  WHERE 1=1 and convert(int,diff_number)<0  and (state is null or state in('2','3','5','6') or (state='4' and oprate<>'提交新增')) ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.start_time >= @startTime AND t.start_time <= @endTime ) ");
                }
                if (!queryParam["platform_name"].IsEmpty())
                {
                    dp.Add("platform_name", "%" + queryParam["platform_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.platform_name Like @platform_name ");
                }
                if (!queryParam["unit_name"].IsEmpty())
                {
                    dp.Add("unit_name", "%" + queryParam["unit_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.unit_name Like @unit_name ");
                }
                if (!queryParam["element_id"].IsEmpty())
                {
                    dp.Add("element_id", "%" + queryParam["element_id"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.element_id Like @element_id ");
                }
                return this.BaseRepository("BaseDb1").FindList<tb_stock_infoEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取tb_stock_info表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public tb_stock_infoEntity Gettb_stock_infoEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository("BaseDb1").FindEntity<tb_stock_infoEntity>(keyValue);
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
                this.BaseRepository("BaseDb1").Delete<tb_stock_infoEntity>(t=>t.id == keyValue);
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
        public void SaveEntity(string keyValue, tb_stock_infoEntity entity)
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

        #region  验证数据
        /// <summary>
        /// 账户不能重复
        /// </summary>
        /// <param name="account">账户值</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistAccount(string account, string keyValue)
        {
            try
            {
                var expression = LinqExtensions.True<tb_stock_infoEntity>();
                expression = expression.And(t => t.open_id == account);
                if (!string.IsNullOrEmpty(keyValue))
                {
                    expression = expression.And(t => t.open_id != keyValue);
                }
                return this.BaseRepository("BaseDb1").IQueryable(expression).Count() == 0 ? true : false;
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
