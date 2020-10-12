using Learun.Application.TwoDevelopment.LR_LGManager;
using Learun.Application.TwoDevelopment.LR_LGManager.Production_plan;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_LGManager.Controllers
{
    public class Plan_manageController : MvcControllerBase
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
        [HttpGet]
        public ActionResult AutoRun()
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
        public ActionResult GetPlan_managePageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = production_planIBLL.GetPlan_managePageList(paginationobj, queryJson);
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
        public ActionResult GetNowProdNum(string queryJson)
        {
            var data = production_planIBLL.GetNowProdNum(queryJson);
            if (Request.IsAjaxRequest())
            {

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
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
            if (tb_production_planData.auto == 1)
            {
                return Fail("请先取消自动状态，再编辑!");
            }
            var jsonData = new
            {
                tb_production_plan = tb_production_planData,
            };
            return Success(jsonData);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetAutoRunData(string keyValue)
        {
            var data= production_planIBLL.GetAutoRunData(keyValue);
            return Success(data);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult GetAutoMachine()
        {
            var data = production_planIBLL.GetAutoMachine();
            return Success(data);
        }
        [HttpPost]
        [AjaxOnly]
        public ActionResult GetPlanSelect(string machine_id)
        {
            var data = production_planIBLL.GetPlanSelect(machine_id);
            return Success(data);
        }
        [HttpPost]
        [AjaxOnly]
        public ActionResult AutoRunStateMachine()
        {
            var data = production_planIBLL.AutoRunStateMachine();
            return Success(data);
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
            if (!tb_production_planData.state.Equals("新增"))
            {
                return Fail("无法删除！");
            }
            if (tb_production_planData.auto == 1)
            {
                return Fail("请先取消自动状态，再删除!");
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
            //新增
            if (keyValue.IsEmpty())
            {
                tb_production_planEntity entity = strEntity.ToObject<tb_production_planEntity>();
                entity.state = state;
                var isExistPlan = production_planIBLL.IsExistPlan(entity.plan_name);
                var isStartPlanExist = production_planIBLL.IsStartPlanExist(entity.machine_id, keyValue);
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
            //编辑
            else
            {
                var tb_production_planData = production_planIBLL.Gettb_production_planEntity(keyValue);
                tb_production_planEntity entity = strEntity.ToObject<tb_production_planEntity>();
                entity.state = state;
                if (entity.state == "开始")
                {
                    var isStartPlanExist = production_planIBLL.IsStartPlanExist(entity.machine_id, keyValue);
                    if (isStartPlanExist)
                    {
                        return Fail("该设备已存在开始计划！");
                    }
                }
                //改变产量时将此工单改为变更，并新增修改后的工单
                if (tb_production_planData.plan_amount != entity.plan_amount
                    && tb_production_planData.state != "新增")
                {
                    tb_production_planData.state = "变更";
                    production_planIBLL.SaveChangeEntity(keyValue, tb_production_planData, entity);
                    return Success("计划变更成功！");
                }
                production_planIBLL.SaveEntity(keyValue, tb_production_planData, entity);
                return Success("保存成功！");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveAutoRunForm(string keyValue, string strEntity)
        {
            AutoRunEntity entity = strEntity.ToObject<AutoRunEntity>();
            //获取十条计划的id
            List<string> id = new List<string>();
            id.Add(entity.machinePlan0);
            id.Add(entity.machinePlan1);
            id.Add(entity.machinePlan2);
            id.Add(entity.machinePlan3);
            id.Add(entity.machinePlan4);
            id.Add(entity.machinePlan5);
            id.Add(entity.machinePlan6);
            id.Add(entity.machinePlan7);
            id.Add(entity.machinePlan8);
            id.Add(entity.machinePlan9);

            var length = id.Where(d => d != "").ToList();
            var distinctLength = id.Where(d => d != "").Distinct().ToList();
            if(length.Count!= distinctLength.Count)
            {
                return Fail("有重复计划");
            }
            if (entity.checkAuto.Equals("1"))
            {
                if (id.All(t => t.Equals("")))
                {
                    return Fail("请选择计划");
                }
                if (length.Count < 2)
                {
                    return Fail("至少选择两条工单");
                }
                production_planIBLL.SaveAutoRunForm(keyValue, entity.checkAuto, id);
                return Success("开启自动模式");
            }
            production_planIBLL.SaveAutoRunForm(keyValue, entity.checkAuto, id);
            return Success("开启手动模式");

        }
        #endregion

    }
}