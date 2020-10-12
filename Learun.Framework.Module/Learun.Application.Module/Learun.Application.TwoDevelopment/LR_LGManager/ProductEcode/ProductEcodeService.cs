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
    /// 日 期：2019-01-08 13:48
    /// 描 述：二维码追溯
    /// </summary>
    public class ProductEcodeService : RepositoryFactory
    {
        #region  获取数据
        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<tb_product_hisEntity> GetPageListOriginal(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@" select top 100 percent a.calc_date,a.product_ecode,a.stage_time,a.prodtime,
                                    b.product_no as product_id,
                                    a.calc_date+'-'+Convert(nvarchar(50),wshift_id) as  stage_emp_id,
                                    d.machine_name as stage_mac_id,
                                    e.group_name as stage_group_id
                                    from tb_product_his a
                                    inner join tb_product_info b on a.product_id = b.product_id
                                    inner join tb_machine_info d on a.stage_mac_id = d.machine_id
                                    inner join tb_macgroup_info e on a.stage_group_id = e.group_id");

                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( a.stage_time >= @startTime AND a.stage_time <= @endTime ) ");
                    if (!queryParam["keyword"].IsEmpty())
                    {
                        dp.Add("keyword", queryParam["keyword"].ToString(), DbType.String);
                        strSql.Append(" AND ( a.product_ecode = @keyword) ");
                    }
                    if (!queryParam["group_id"].IsEmpty())
                    {
                        dp.Add("group_id", queryParam["group_id"].ToString(), DbType.String);
                        strSql.Append(" AND ( a.stage_group_id = @group_id) ");
                    }
                    if (!queryParam["machine_id"].IsEmpty() && queryParam["machine_id"].ToString() != "0")
                    {
                        dp.Add("machine_id", queryParam["machine_id"].ToString(), DbType.String);
                        strSql.Append(" AND ( a.stage_mac_id = @machine_id) ");
                    }
                }
                strSql.Append(" order by a.stage_time desc ");
                return this.BaseRepository("BaseDb1").FindList<tb_product_hisEntity>(strSql.ToString(), dp, pagination);
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
        public IEnumerable<tb_product_hisEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                //码
                strSql.Append(@"select top 2000 product_ecode,
		                                min(stage_time) begin_time,
                                        max(prodtime) end_time
	                                from (
                                select  a.product_ecode,
		                                stage_time ,
                                        [prodtime] 
                                FROM   tb_product_his a 
                                where 1=1 ");

                var queryParam = queryJson.ToJObject();
                // 参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( a.stage_time >= @startTime AND a.stage_time <= @endTime ) ");
                    if (!queryParam["keyword"].IsEmpty())
                    {
                        dp.Add("keyword", queryParam["keyword"].ToString(), DbType.String);
                        strSql.Append(" AND ( a.product_ecode = @keyword ) ");
                    }
                    if (!queryParam["group_id"].IsEmpty())
                    {
                        dp.Add("group_id", queryParam["group_id"].ToString(), DbType.String);
                        strSql.Append(" AND ( a.stage_group_id = @group_id) ");
                    }
                    if (!queryParam["machine_id"].IsEmpty() && queryParam["machine_id"].ToString() != "0")
                    {
                        dp.Add("machine_id", queryParam["machine_id"].ToString(), DbType.String);
                        strSql.Append(" AND ( a.stage_mac_id = @machine_id) ");
                    }
                }
                strSql.Append(" ) v GROUP BY product_ecode  ");
                return this.BaseRepository("BaseDb1").FindList<tb_product_hisEntity>(strSql.ToString(),dp, pagination);
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
        public IEnumerable<tb_product_hisEntity> GetPageListOne(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();

                strSql.Append(@"select top 100 percent a.calc_date,a.product_ecode,a.stage_time,a.prodtime,
		                            b.product_no as product_id,
		                            d.machine_name as stage_mac_id,
		                            e.group_name as stage_group_id,
		                            [wshift_date],f.[wshift_name]
                                from tb_product_his a with(nolock)
                                inner join tb_product_info b with(nolock) on a.product_id = b.product_id
                                inner join tb_macgroup_info e with(nolock) on a.stage_group_id = e.group_id
	                            inner join tb_machine_info d with(nolock) on a.stage_mac_id = d.machine_id
                                INNER JOIN tb_shift_info f with(nolock) ON f.wshift_id=a.wshift_id
                                WHERE 1=1  ");

                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["keyword"].IsEmpty())
                {
                    dp.Add("keyword", queryParam["keyword"].ToString(), DbType.String);
                    strSql.Append(@" AND  a.product_ecode = @keyword ");
                }
                //strSql.Append(" ORDER BY a.stage_time ");
                return this.BaseRepository("BaseDb1").FindList<tb_product_hisEntity>(strSql.ToString(), dp, pagination);
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


        public IEnumerable<tb_product_hisEntity> GetListOnce(string product_ecode, string StartTime, string EndTime)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT MIN(a.calc_date)     calc_date,
                                       MIN(a.product_ecode)   product_ecode,
                                       MIN(a.stage_time)          begin_time,
                                       MAX(a.prodtime)            end_time,
                                       MIN([wshift_date])         wshift_date,
                                       MIN(f.[wshift_name])       wshift_name,
                                       [dbo].[f_getPrinterTime](MIN(a.product_ecode)) printed_time,'合格' stage_desp
                                FROM   tb_product_his a with(nolock)
                                       INNER JOIN tb_shift_info f with(nolock) ON  f.wshift_id = a.wshift_id
                                WHERE  1 = 1  ");

                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!string.IsNullOrEmpty(product_ecode))
                {
                    dp.Add("keyword", product_ecode.ToString(), DbType.String);
                    strSql.Append(@" AND  a.product_ecode = @keyword ");
                }
                return this.BaseRepository("BaseDb1").FindList<tb_product_hisEntity>(strSql.ToString(), dp);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw new Exception(ex.Message + ex.InnerException + ex.StackTrace);
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取tb_product_his表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public tb_product_hisEntity Gettb_product_hisEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository("BaseDb").FindEntity<tb_product_hisEntity>(keyValue);
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
                this.BaseRepository("BaseDb").Delete<tb_product_hisEntity>(t=>t.id == keyValue);
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
        public void SaveEntity(string keyValue, tb_product_hisEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository("BaseDb").Update(entity);
                }
                else
                {
                    entity.Create();
                    this.BaseRepository("BaseDb").Insert(entity);
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
