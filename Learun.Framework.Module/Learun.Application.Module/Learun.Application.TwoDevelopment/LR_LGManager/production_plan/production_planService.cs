using Dapper;
using Learun.Application.TwoDevelopment.LR_LGManager;
using Learun.Cache.Mongo;
using Learun.DataBase.Repository;
using Learun.Util;
using MongoDB.Bson;
using MongoDB.Driver;
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
    /// 日 期：2019-09-18 10:52
    /// 描 述：Production_plan
    /// </summary>
    public class Production_planService : RepositoryFactory
    {
        #region  获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        //获取历史工单页面数据
        public IEnumerable<tb_production_planEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                RefreshProdNum();
                var strSql = new StringBuilder();
                string sql;
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.id,
                t.plan_name,
                t.product_name,
                t.plan_amount,
                t.machine_id,
                t.start_time,
                t.create_user,
                t.operation_user,
                t.state,
                t.prod_num
                ");
                strSql.Append("  FROM tb_production_plan t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.create_time >= @startTime AND t.create_time <= @endTime ) ");
                }
                if (!queryParam["plan_name"].IsEmpty())
                {
                    dp.Add("plan_name", "%" + queryParam["plan_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.plan_name Like @plan_name ");
                }
                if (!queryParam["product_name"].IsEmpty())
                {
                    dp.Add("product_name", "%" + queryParam["product_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.product_name Like @product_name ");
                }
                if (!queryParam["machine_id"].IsEmpty())
                {
                    dp.Add("machine_id", queryParam["machine_id"].ToString(), DbType.String);
                    strSql.Append(" AND t.machine_id = @machine_id ");
                }
                if (!queryParam["state"].IsEmpty())
                {
                    string str = queryParam["state"].ToString();
                    string[] condition = { "," };
                    string[] state = str.Split(condition, StringSplitOptions.None);
                    strSql.Append(" AND t.state in (");
                    for (var i = 0; i < state.Length; i++)
                    {
                        if (i == state.Length - 1)
                        {
                            strSql.Append("'" + state[i] + "'");
                            break;
                        }
                        strSql.Append("'" + state[i] + "',");
                    }
                    strSql.Append(")");
                }
                var result = this.BaseRepository("BaseDb1").FindList<tb_production_planEntity>(strSql.ToString(), dp, pagination);
                return result;
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
        //获取工单管理页面数据（只显示新增、开始、暂停）
        public IEnumerable<tb_production_planEntity> GetPlan_managePageList(Pagination pagination, string queryJson)
        {
            try
            {
                RefreshProdNum();
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.id,
                t.plan_name,
                t.product_name,
                t.plan_amount,
                t.machine_id,
                t.start_time,
                t.create_user,
                t.operation_user,
                t.state,
                t.prod_num,
                t.sort,
                t.auto
                ");
                strSql.Append("  FROM tb_production_plan t ");
                strSql.Append("  WHERE 1=1 AND t.state!='变更' AND t.state!='完成'");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.create_time >= @startTime AND t.create_time <= @endTime ) ");
                }
                if (!queryParam["plan_name"].IsEmpty())
                {
                    dp.Add("plan_name", "%" + queryParam["plan_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.plan_name Like @plan_name ");
                }
                if (!queryParam["product_name"].IsEmpty())
                {
                    dp.Add("product_name", "%" + queryParam["product_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.product_name Like @product_name ");
                }
                if (!queryParam["machine_id"].IsEmpty())
                {
                    dp.Add("machine_id", queryParam["machine_id"].ToString(), DbType.String);
                    strSql.Append(" AND t.machine_id = @machine_id ");
                }
                if (!queryParam["state"].IsEmpty())
                {
                    dp.Add("state", queryParam["state"].ToString(), DbType.String);
                    strSql.Append(" AND t.state = @state ");
                }
                var result = this.BaseRepository("BaseDb1").FindList<tb_production_planEntity>(strSql.ToString(), dp, pagination);
                return result;
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
        //历史工单柱状图数据
        public IEnumerable<tb_production_planEntity> GetChartList(string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT top(1000) ");
                strSql.Append(@"
                count(plan_name) as plan_name,
                t.state
                ");
                strSql.Append("  FROM tb_production_plan t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.create_time >= @startTime AND t.create_time <= @endTime ) ");
                }
                if (!queryParam["plan_name"].IsEmpty())
                {
                    dp.Add("plan_name", "%" + queryParam["plan_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.plan_name Like @plan_name ");
                }
                if (!queryParam["product_name"].IsEmpty())
                {
                    dp.Add("product_name", "%" + queryParam["product_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.product_name Like @product_name ");
                }
                if (!queryParam["machine_id"].IsEmpty())
                {
                    dp.Add("machine_id", queryParam["machine_id"].ToString(), DbType.String);
                    strSql.Append(" AND t.machine_id = @machine_id ");
                }
                if (!queryParam["state"].IsEmpty())
                {
                    string str = queryParam["state"].ToString();
                    string[] condition = { "," };
                    string[] state = str.Split(condition, StringSplitOptions.None);
                    strSql.Append(" AND t.state in (");
                    for (var i = 0; i < state.Length; i++)
                    {
                        if (i == state.Length - 1)
                        {
                            strSql.Append("'" + state[i] + "'");
                            break;
                        }
                        strSql.Append("'" + state[i] + "',");
                    }
                    strSql.Append(")");
                }
                strSql.Append("GROUP BY t.state");
                var result = this.BaseRepository("BaseDb1").FindList<tb_production_planEntity>(strSql.ToString(), dp);
                return result;
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
        //历史工单饼图数据
        public IEnumerable<tb_production_planEntity> GetBarList(string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT top(1000) ");
                strSql.Append(@"
                sum(convert(int,t.plan_amount)) as plan_amount,
                t.product_name
                ");
                strSql.Append("  FROM tb_production_plan t ");
                strSql.Append("  WHERE 1=1 AND t.state!='变更'");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.create_time >= @startTime AND t.create_time <= @endTime ) ");
                }
                if (!queryParam["plan_name"].IsEmpty())
                {
                    dp.Add("plan_name", "%" + queryParam["plan_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.plan_name Like @plan_name ");
                }
                if (!queryParam["product_name"].IsEmpty())
                {
                    dp.Add("product_name", "%" + queryParam["product_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.product_name Like @product_name ");
                }
                if (!queryParam["machine_id"].IsEmpty())
                {
                    dp.Add("machine_id", queryParam["machine_id"].ToString(), DbType.String);
                    strSql.Append(" AND t.machine_id = @machine_id ");
                }
                if (!queryParam["state"].IsEmpty())
                {
                    string str = queryParam["state"].ToString();
                    string[] condition = { "," };
                    string[] state = str.Split(condition, StringSplitOptions.None);
                    strSql.Append(" AND t.state in (");
                    for (var i = 0; i < state.Length; i++)
                    {
                        if (i == state.Length - 1)
                        {
                            strSql.Append("'" + state[i] + "'");
                            break;
                        }
                        strSql.Append("'" + state[i] + "',");
                    }
                    strSql.Append(")");
                }
                strSql.Append("GROUP BY t.product_name");
                var result = this.BaseRepository("BaseDb1").FindList<tb_production_planEntity>(strSql.ToString(), dp);
                return result;
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
        //历史工单折线图数据
        public IEnumerable<tb_production_planEntity> GetLineList(string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT top(1000) ");
                strSql.Append(@"
	            CONVERT ( VARCHAR ( 12 ), t.create_time, 111 ) AS create_time,
	            COUNT ( plan_name ) AS plan_name  
                ");
                strSql.Append("  FROM tb_production_plan t ");
                strSql.Append("  WHERE 1=1 AND t.state='完成'");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.create_time >= @startTime AND t.create_time <= @endTime ) ");
                }
                if (!queryParam["plan_name"].IsEmpty())
                {
                    dp.Add("plan_name", "%" + queryParam["plan_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.plan_name Like @plan_name ");
                }
                if (!queryParam["product_name"].IsEmpty())
                {
                    dp.Add("product_name", "%" + queryParam["product_name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.product_name Like @product_name ");
                }
                if (!queryParam["machine_id"].IsEmpty())
                {
                    dp.Add("machine_id", queryParam["machine_id"].ToString(), DbType.String);
                    strSql.Append(" AND t.machine_id = @machine_id ");
                }
                if (!queryParam["state"].IsEmpty())
                {
                    string str = queryParam["state"].ToString();
                    string[] condition = { "," };
                    string[] state = str.Split(condition, StringSplitOptions.None);
                    strSql.Append(" AND t.state in (");
                    for (var i = 0; i < state.Length; i++)
                    {
                        if (i == state.Length - 1)
                        {
                            strSql.Append("'" + state[i] + "'");
                            break;
                        }
                        strSql.Append("'" + state[i] + "',");
                    }
                    strSql.Append(")");
                }
                strSql.Append("GROUP BY CONVERT(VARCHAR(12), t.create_time, 111)");
                var result = this.BaseRepository("BaseDb1").FindList<tb_production_planEntity>(strSql.ToString(), dp);
                return result;
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
        /// 获取tb_production_plan表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public tb_production_planEntity Gettb_production_planEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository("BaseDb1").FindEntity<tb_production_planEntity>(keyValue);
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
        //计划是否重名
        public bool IsExistPlan(string plan_name)
        {
            try
            {
                var isExist = this.BaseRepository("BaseDb1").FindEntity<tb_production_planEntity>(t => t.plan_name == plan_name);
                if (isExist.IsEmpty())
                {
                    return false;
                }
                return true;
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
        //是否存在已经开始的计划
        public bool IsStartPlanExist(int? machine_id, string keyValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    return false;
                }
                var isExist = this.BaseRepository("BaseDb1").FindEntity<tb_production_planEntity>(t => t.machine_id == machine_id && t.state == "开始" && t.id != keyValue);
                if (isExist.IsEmpty())
                {
                    return false;
                }
                return true;
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
                this.BaseRepository("BaseDb1").Delete<tb_production_planEntity>(t => t.id == keyValue && t.state != "开始");

                var collection = MongoCache.GetCollection<tb_production_planEntity>("clc_production_plan");
                var filter = Builders<tb_production_planEntity>.Filter.Eq("id", keyValue);
                var result = collection.DeleteOne(filter);
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
        public void SaveEntity(string keyValue, tb_production_planEntity tb_production_plan, tb_production_planEntity entity)
        {
            try
            {
                //编辑
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //工单由开始变为暂停或开始变为完成时计算产量，并更新mongoDB
                    if ((tb_production_plan.state == "开始" && entity.state == "暂停")
                        || (tb_production_plan.state == "开始" && entity.state == "完成"))
                    {
                        var jsonData = new
                        {
                            StartTime = tb_production_plan.create_time.ToDateTimeString(),
                            machine_id = tb_production_plan.machine_id,
                            _id = tb_production_plan.id
                        };
                        var planProdNum = GetPlanProdNum(jsonData.ToJson());
                        entity.prod_num = planProdNum + GetInitProdNum(jsonData.ToJson());
                    }
                    entity.Modify(keyValue);
                    this.BaseRepository("BaseDb1").Update(entity);

                    var collection = MongoCache.GetCollection<tb_production_planEntity>("clc_production_plan");
                    var filter = Builders<tb_production_planEntity>.Filter.Eq("id", keyValue);
                    var update = Builders<tb_production_planEntity>.Update
                        .Set("plan_amount", entity.plan_amount)
                        .Set("state", entity.state)
                        .Set("create_time", entity.create_time)
                        .Set("operation_user", entity.operation_user)
                        .Set("prod_num", entity.prod_num);
                    var result = collection.UpdateOne(filter, update);
                }
                //新增
                else
                {
                    entity.Create();
                    this.BaseRepository("BaseDb1").Insert(entity);
                    MongoCache.InsertOne("clc_production_plan", entity);
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
        //保存变更的计划
        public void SaveChangeEntity(string keyValue, tb_production_planEntity tb_production_plan, tb_production_planEntity entity)
        {
            try
            {

                var jsonData = new
                {
                    StartTime = tb_production_plan.create_time.ToDateTimeString(),
                    machine_id = tb_production_plan.machine_id,
                    _id = tb_production_plan.id
                };
                var planProdNum = GetPlanProdNum(jsonData.ToJson());
                var initProdNum = planProdNum + GetInitProdNum(jsonData.ToJson());

                if (tb_production_plan.state == "暂停")
                {
                    tb_production_plan.prod_num = planProdNum;

                }
                else if (tb_production_plan.state == "开始")
                {
                    tb_production_plan.prod_num = planProdNum + initProdNum;
                }
                tb_production_plan.Modify(keyValue);
                this.BaseRepository("BaseDb1").Update(tb_production_plan);

                var collection = MongoCache.GetCollection<tb_production_planEntity>("clc_production_plan");
                var filter = Builders<tb_production_planEntity>.Filter.Eq("id", keyValue);
                var update = Builders<tb_production_planEntity>.Update
                    .Set("plan_amount", tb_production_plan.plan_amount)
                    .Set("state", tb_production_plan.state)
                    .Set("create_time", tb_production_plan.create_time)
                    .Set("create_user", tb_production_plan.create_user)
                    .Set("operation_user", tb_production_plan.operation_user)
                    .Set("prod_num", tb_production_plan.prod_num)
                    .Set("sort", 0)
                    .Set("auto", 0);
                var result = collection.UpdateOne(filter, update);

                entity.Create();
                entity.operation_user = tb_production_plan.operation_user;
                entity.prod_num = tb_production_plan.prod_num;
                this.BaseRepository("BaseDb1").Insert(entity);
                MongoCache.InsertOne("clc_production_plan", entity);


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
        //获取暂停计划的产量
        public int GetPlanProdNum(string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.prod_num
                ");
                strSql.Append("  FROM tb_Production_plan t ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                strSql.Append("  WHERE 1=1 ");
                if (!queryParam["_id"].IsEmpty())
                {
                    dp.Add("id", queryParam["_id"].ToString(), DbType.String);
                    strSql.Append(" AND ( t.id = @id) ");
                }
                var result = this.BaseRepository("BaseDb1").FindObject(strSql.ToString(), dp).ToInt();
                return result;
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
        //以时间为起点累加产量
        public int GetInitProdNum(string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                SUM ( p.prod_num ) AS prod_num 
                ");
                strSql.Append("  FROM tb_init_prod_num p ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });

                if (!queryParam["StartTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( p.read_time > @startTime) ");
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("endTime", queryParam["EndTime"].ToDate().AddMinutes(1), DbType.DateTime);
                    strSql.Append(" AND ( p.read_time <= @endTime) ");
                }
                if (!queryParam["machine_id"].IsEmpty())
                {
                    dp.Add("machine_id", queryParam["machine_id"].ToString(), DbType.String);
                    strSql.Append(" AND p.machine_id = @machine_id ");
                }
                var result = this.BaseRepository("BaseDb1").FindObject(strSql.ToString(), dp);
                return result.ToInt();
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
        //获取当前产量
        public int GetNowProdNum(string queryJson)
        {
            try
            {
                var result2 = GetPlanProdNum(queryJson);

                var queryParam = queryJson.ToJObject();
                if (queryParam["state"].ToString().Equals("开始"))
                {
                    var result = GetInitProdNum(queryJson) + result2;
                    return result;
                }

                return result2;

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
        //获取InitProd表最近第一笔产量的时间
        public object GetInitProdReadTime(string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                top(1) p.read_time
                ");
                strSql.Append("  FROM tb_init_prod_num p ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });

                if (!queryParam["StartTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( p.read_time >= @startTime) ");
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("endTime", queryParam["EndTime"].ToDate().AddMinutes(1), DbType.DateTime);
                    strSql.Append(" AND ( p.read_time <= @endTime) ");
                }
                if (!queryParam["machine_id"].IsEmpty())
                {
                    dp.Add("machine_id", queryParam["machine_id"].ToString(), DbType.String);
                    strSql.Append(" AND p.machine_id = @machine_id  order by read_time desc");
                }
                var result = this.BaseRepository("BaseDb1").FindObject(strSql.ToString(), dp);
                return result;
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
        //刷新当前产量
        public void RefreshProdNum()
        {
            try
            {
                this.BaseMGRepository("MongoDB");//WebApi连接mongoDB

                var sql = "SELECT * FROM tb_production_plan WHERE state = '开始'";
                var plans = this.BaseRepository("BaseDb1").FindList<tb_production_planEntity>(sql);
                foreach (var p in plans)
                {
                    var jsonData = new
                    {
                        state = p.state,
                        StartTime = p.create_time.ToString(),
                        machine_id = p.machine_id,
                        _id = p.id
                    };
                    var nowProd = GetNowProdNum(jsonData.ToJson());
                    p.prod_num = nowProd;
                    p.create_time = GetInitProdReadTime(jsonData.ToJson()) == null ? p.create_time : Convert.ToDateTime(GetInitProdReadTime(jsonData.ToJson()));
                    this.BaseRepository("BaseDb1").Update(p);
                    var collection = MongoCache.GetCollection<tb_production_planEntity>("clc_production_plan");
                    var filter = Builders<tb_production_planEntity>.Filter.Eq("id", p.id);
                    var update = Builders<tb_production_planEntity>.Update
                        .Set("prod_num", p.prod_num);
                    var result = collection.UpdateOne(filter, update);
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
        //获取工单管理页面上可以选择自动模式的设备
        public IEnumerable<tb_machine_infoEntity> GetAutoMachine()
        {
            try
            {
                var sql = "SELECT * FROM tb_machine_info t WHERE t.is_main!='YES'";
                var result = this.BaseRepository("BaseDb1").FindList<tb_machine_infoEntity>(sql);
                return result;
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
        //获取排序时所需的下拉列表数据
        public IEnumerable<tb_production_planEntity> GetPlanSelect(string machine_id)
        {
            try
            {
                var sql = "SELECT * FROM tb_production_plan t WHERE machine_id = "+ machine_id + " AND state in ('新增','暂停','开始')";
                var result = this.BaseRepository("BaseDb1").FindList<tb_production_planEntity>(sql);
                return result;
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
        //保存自动模式及排序
        public void SaveAutoRunForm(string keyValue,string checkAuto, List<string> id)
        {
            int m = 0;
            //如果是自动模式
            if (checkAuto.Equals("1"))
            {
                foreach (var data in id)
                {
                    m++;
                    if (!data.IsEmpty())
                    {
                        var tb_production_plan = this.BaseRepository("BaseDb1").FindEntity<tb_production_planEntity>(t => t.id == data);
                        tb_production_plan.sort = m;
                        tb_production_plan.auto = 1;
                        tb_production_plan.operation_user = LoginUserInfo.Get().realName;
                        this.BaseRepository("BaseDb1").Update(tb_production_plan);
                        var collection = MongoCache.GetCollection<tb_production_planEntity>("clc_production_plan");
                        var filter = Builders<tb_production_planEntity>.Filter.Eq("id", data);
                        var update = Builders<tb_production_planEntity>.Update
                            .Set("sort", m)
                            .Set("auto", 1);
                        var result = collection.UpdateOne(filter, update);
                    }
                   
                }
                
            }
            //如果为手动模式
            else
            {
                var machine_id = Convert.ToInt32(keyValue);  
                var tb_production_plan = this.BaseRepository("BaseDb1")
                    .FindList<tb_production_planEntity>
                    (t => t.machine_id == machine_id &&t.state!="完成"&&t.auto==1);
                foreach(var p in tb_production_plan)
                {
                    p.sort = 0;
                    p.auto = 0;
                    p.operation_user = LoginUserInfo.Get().realName;
                    this.BaseRepository("BaseDb1").Update(p);
                    var collection = MongoCache.GetCollection<tb_production_planEntity>("clc_production_plan");
                    var filter = Builders<tb_production_planEntity>.Filter.Eq("id", p.id);
                    var update = Builders<tb_production_planEntity>.Update
                        .Set("sort", 0)
                        .Set("auto", 0);
                    var result = collection.UpdateOne(filter, update);
                }
  
            }

        }
        //获取处于自动模式的设备
        public List<string> AutoRunStateMachine()
        {
            List<string> machine_id = new List<string>();
            var tb_production_plan = this.BaseRepository("BaseDb1").FindList<tb_production_planEntity>(t => t.auto == 1 && t.state != "完成"&& t.state != "变更");
            foreach(var data in tb_production_plan)
            {
                machine_id.Add(data.machine_id.ToString());
            }
            return machine_id.Distinct().ToList();
        }
        //获取编辑排序时的记录SetForm
        public List<tb_production_planEntity> GetAutoRunData(string keyValue)
        {
            var sql = "SELECT * FROM tb_production_plan WHERE machine_id = "+keyValue+ " AND state IN('新增', '暂停','开始') AND sort != 0 ORDER BY sort";
            var tb_production_plan = this.BaseRepository("BaseDb1").FindList<tb_production_planEntity>(sql).ToList();
            return tb_production_plan;
        }
        #endregion

    }
}
