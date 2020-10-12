using Learun.Application.TwoDevelopment.LR_LGManager;
using Learun.Util;
using System;
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
    public class Production_planBLL : Production_planIBLL
    {
        private Production_planService production_planService = new Production_planService();

        #region  获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<tb_production_planEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return production_planService.GetPageList(pagination, queryJson);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        public IEnumerable<tb_production_planEntity> GetPlan_managePageList(Pagination pagination, string queryJson)
        {
            try
            {
                return production_planService.GetPlan_managePageList(pagination, queryJson);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        public IEnumerable<tb_production_planEntity> GetChartList(string queryJson)
        {
            try
            {
                return production_planService.GetChartList(queryJson);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        public IEnumerable<tb_production_planEntity> GetBarList(string queryJson)
        {
            try
            {
                return production_planService.GetBarList(queryJson);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        public IEnumerable<tb_production_planEntity> GetLineList(string queryJson)
        {
            try
            {
                return production_planService.GetLineList(queryJson);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
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
                return production_planService.Gettb_production_planEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
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
                production_planService.DeleteEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        public bool IsExistPlan(string plan_name)
        {
            try
            {
                return production_planService.IsExistPlan(plan_name);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        public bool IsStartPlanExist(int? machine_id, string keyValue)
        {
            try
            {
                return production_planService.IsStartPlanExist(machine_id, keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
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
                production_planService.SaveEntity(keyValue, tb_production_plan, entity);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        public void SaveChangeEntity(string keyValue, tb_production_planEntity tb_production_plan, tb_production_planEntity entity)
        {
            try
            {
                production_planService.SaveChangeEntity(keyValue, tb_production_plan, entity);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        public int GetNowProdNum(string queryJson)
        {
            try
            {
                return production_planService.GetNowProdNum(queryJson);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        public IEnumerable<tb_machine_infoEntity> GetAutoMachine()
        {
            try
            {
                return production_planService.GetAutoMachine();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        public IEnumerable<tb_production_planEntity> GetPlanSelect(string machine_id)
        {
            try
            {
                return production_planService.GetPlanSelect(machine_id);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        public void SaveAutoRunForm(string keyValue,string checkAuto, List<string> id)
        {
            try
            {
                production_planService.SaveAutoRunForm(keyValue,checkAuto, id);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        public List<string> AutoRunStateMachine()
        {
            try
            {
                return production_planService.AutoRunStateMachine();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        public List<tb_production_planEntity> GetAutoRunData(string keyValue)
        {
            try
            {
                return production_planService.GetAutoRunData(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        
        public void RefreshProdNum()
        {
            try
            {
                 production_planService.RefreshProdNum();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion

    }
}
