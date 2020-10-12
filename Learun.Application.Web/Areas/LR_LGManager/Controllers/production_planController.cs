using Learun.Application.TwoDevelopment.LR_LGManager;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_LGManager.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-09-18 10:52
    /// 描 述：Production_plan
    /// </summary>
    public class Production_planController : MvcControllerBase
    {
        private Production_planIBLL production_planIBLL = new Production_planBLL();

        #region  视图功能

        /// <summary>
        /// 主页面
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region  获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = production_planIBLL.GetPageList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        public ActionResult GetChartList(string queryJson)
        {
            var data = production_planIBLL.GetChartList(queryJson);
            var resultList = new List<dynamic>();

            foreach (var d in data)
            {
                var jsonData = new
                {
                    name = d.state,
                    value = d.plan_name
                };
                resultList.Add(jsonData);
            };
            return Success(resultList.ToArray());
        }
        public ActionResult GetBarList(string queryJson)
        {
            var data = production_planIBLL.GetBarList(queryJson);
            var productList = new List<dynamic>();
            var amountList = new List<dynamic>();
            foreach (var d in data)
            {
                productList.Add(d.product_name);
                amountList.Add(d.plan_amount);
            };
            var jsonData = new
            {
                name = productList.ToArray(),
                value = amountList.ToArray()
            };
            return Success(jsonData);
        }

        public ActionResult GetLineList(string queryJson)
        {
            var data = production_planIBLL.GetLineList(queryJson);
            var endList = new List<dynamic>();
            var timeList = new List<dynamic>();

            foreach (var d in data)
            {
                endList.Add(d.plan_name);
                timeList.Add(Convert.ToDateTime(d.create_time).ToString("yy-MM-dd"));
            }

            var jsonData = new
            {
                time = timeList.ToArray(),
                end = endList.ToArray()
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取表单数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            var tb_production_planData = production_planIBLL.Gettb_production_planEntity(keyValue);
            if (tb_production_planData.state.Equals("完成") || tb_production_planData.state.Equals("变更"))
            {
                return Fail("任务已结束，无法编辑！");
            }
            var jsonData = new
            {
                tb_production_plan = tb_production_planData,
            };
            return Success(jsonData);
        }
        #endregion

        #region  提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            var tb_production_planData = production_planIBLL.Gettb_production_planEntity(keyValue);
            if (tb_production_planData.state.Equals("开始"))
            {
                return Fail("任务已开始，无法删除！");
                ;
            }
            production_planIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string strEntity, string state)
        {
            if (keyValue.IsEmpty())
            {
                tb_production_planEntity entity = strEntity.ToObject<tb_production_planEntity>();
                entity.state = state;
                var isExistPlan = production_planIBLL.IsExistPlan(entity.plan_name);
                var isStartPlanExist = production_planIBLL.IsStartPlanExist(entity.machine_id, entity.state);
                if (isExistPlan)
                {
                    return Fail("计划已存在！");
                }
                if (isStartPlanExist)
                {
                    return Fail("该设备已存在开始计划！");
                }
                production_planIBLL.SaveEntity(keyValue, null, entity);
                return Success("保存成功！");
            }
            else
            {
                var tb_production_planData = production_planIBLL.Gettb_production_planEntity(keyValue);
                tb_production_planEntity entity = strEntity.ToObject<tb_production_planEntity>();
                entity.state = state;
                var isStartPlanExist = production_planIBLL.IsStartPlanExist(entity.machine_id, entity.state);
                if (isStartPlanExist)
                {
                    return Fail("该设备已存在开始计划！");
                }
                if (tb_production_planData.create_user != entity.create_user
                    || tb_production_planData.plan_amount != entity.plan_amount)
                {
                    tb_production_planData.state = "变更";
                    production_planIBLL.SaveChangeEntity(keyValue, tb_production_planData, entity);
                    return Success("计划变更成功！");
                }
                production_planIBLL.SaveEntity(keyValue, tb_production_planData, entity);
                return Success("保存成功！");
            }

        }
        #endregion

    }
}
