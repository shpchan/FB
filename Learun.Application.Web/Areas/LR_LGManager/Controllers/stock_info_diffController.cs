using Learun.Util;
using Learun.Util.Operat;
using System.Data;
using Learun.Application.TwoDevelopment.LR_LGManager;
using System.Web.Mvc;
using System.Collections.Generic;
using Learun.Application.Base.SystemModule;
using Learun.DataBase.Repository;

namespace Learun.Application.Web.Areas.LR_LGManager.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-11-12 11:21
    /// 描 述：stock_info
    /// </summary>
    public class stock_info_diffController : MvcControllerBase
    {
        private stock_infoIBLL stock_infoIBLL = new stock_infoBLL();

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
            var data = stock_infoIBLL.GetPageList_Diff(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
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
            var tb_stock_infoData = stock_infoIBLL.Gettb_stock_infoEntity( keyValue );
            var jsonData = new {
                tb_stock_info = tb_stock_infoData,
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
            var tb_stock_infoData = stock_infoIBLL.Gettb_stock_infoEntity(keyValue);
            //stock_infoIBLL.DeleteEntity(keyValue);
            string dataSourceId = "0d48c6ea-f8d0-4c27-a826-8d63df4d84c1";
            string strSql = "update  tb_stock_info set state='5' where id='" + keyValue + "'";
            new DatabaseLinkBLL().ExecuteBySql(dataSourceId, strSql);
            //return Success("删除成功！");
            return BomSuccess("删除成功！", "标准化BOM表", OperationType.Delete, tb_stock_infoData.open_id,"", tb_stock_infoData.ToJson());
        }
        /// <summary>
        /// 提交审核实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SubmitForm(string keyValue)
        {
            var tb_stock_infoData = stock_infoIBLL.Gettb_stock_infoEntity(keyValue);
            string dataSourceId = "0d48c6ea-f8d0-4c27-a826-8d63df4d84c1";
            string strSql = "update  tb_stock_info set state='2' where id='"+ keyValue + "'";
            new DatabaseLinkBLL().ExecuteBySql(dataSourceId, strSql);
            //return Success("提交成功！");
            return BomSuccess("提交成功！", "标准化BOM表", OperationType.Submit, tb_stock_infoData.open_id, "", tb_stock_infoData.ToJson());
        }
        /// <summary>
        /// 手动刷新即时库存
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult Recalculator()
        {
            RepositoryFactory BR = new RepositoryFactory();
            BR.BaseRepository("BaseDb1").ExecuteByProc("pro_stock_info_store");
            return Success("刷新完成！");
        }
        /// <summary>
        /// 通过审核实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult AgreeForm(string keyValue)
        {
            var tb_stock_infoData = stock_infoIBLL.Gettb_stock_infoEntity(keyValue);
            string dataSourceId = "0d48c6ea-f8d0-4c27-a826-8d63df4d84c1";
            string strSql = "update  tb_stock_info set state='3' where id='" + keyValue + "'";
            new DatabaseLinkBLL().ExecuteBySql(dataSourceId, strSql);
            //return Success("提交成功！");
            return BomSuccess("审核成功！", "标准化BOM表", OperationType.Agree, tb_stock_infoData.open_id, "", tb_stock_infoData.ToJson());
        }
        /// <summary>
        /// 驳回审核实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DisAgreeForm(string keyValue)
        {
            var tb_stock_infoData = stock_infoIBLL.Gettb_stock_infoEntity(keyValue);
            string dataSourceId = "0d48c6ea-f8d0-4c27-a826-8d63df4d84c1";
            string strSql = "update  tb_stock_info set state='4' where id='" + keyValue + "'";
            new DatabaseLinkBLL().ExecuteBySql(dataSourceId, strSql);
            //return Success("提交成功！");
            return BomSuccess("驳回成功！", "标准化BOM表", OperationType.DisAgree, tb_stock_infoData.open_id, "", tb_stock_infoData.ToJson());
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string strEntity)
        {
            var tb_stock_infoData = stock_infoIBLL.Gettb_stock_infoEntity(keyValue);
            tb_stock_infoEntity entity = strEntity.ToObject<tb_stock_infoEntity>();
            string state = string.IsNullOrEmpty(keyValue) ? "1" : "6";
            entity.state = state;
            stock_infoIBLL.SaveEntity(keyValue,entity);
            //return Success("保存成功！");
            return BomSuccess("保存成功！", "标准化BOM表", string.IsNullOrEmpty(keyValue) ? OperationType.Create : OperationType.Update, entity.open_id, entity.ToJson(), string.IsNullOrEmpty(keyValue) ? "" : tb_stock_infoData.ToJson());
        }
        #endregion

    }
}
