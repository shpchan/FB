using Learun.Util;
using Learun.Util.Operat;
using System.Data;
using Learun.Application.TwoDevelopment.LR_LGManager;
using System.Web.Mvc;
using System.Collections.Generic;
using Learun.Application.Base.SystemModule;

namespace Learun.Application.Web.Areas.LR_LGManager.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-11-12 11:21
    /// 描 述：stock_info
    /// </summary>
    public class stock_info_checkController : MvcControllerBase
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
            var data = stock_infoIBLL.GetPageList_Check(paginationobj, queryJson);
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
            //根据操作给出对应操作
            string oprate="";
            string strSql = "";
            string strSql_query = "select oprate from  tb_stock_info  where id='" + keyValue + "'";
            DataTable dt =  new DatabaseLinkBLL().FindTable(dataSourceId, strSql_query);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    oprate= dt.Rows[i]["oprate"].ToString();
                }
            }

                if (oprate == "提交删除")
            {
                 strSql = "update  tb_stock_info set state='7' where id='" + keyValue + "'";//永久删除
                new DatabaseLinkBLL().ExecuteBySql(dataSourceId, strSql);
            }
            else if (oprate.IndexOf("->")>0)
            {
                var TempData = stock_infoIBLL.Gettb_stock_infoEntity(keyValue);
                tb_stock_infoEntity entity = TempData.change_after.ToObject<tb_stock_infoEntity>();
                entity.change_after = "";
                entity.change_before = "";
                entity.oprate = "";
                stock_infoIBLL.SaveEntity(keyValue, entity);////把修改后的内容更新到bom表
                strSql = "update  tb_stock_info set state='3' where id='" + keyValue + "'";
                new DatabaseLinkBLL().ExecuteBySql(dataSourceId, strSql);
            }
            else
            {
                strSql = "update  tb_stock_info set state='3' where id='" + keyValue + "'";//直接进入bom表
                new DatabaseLinkBLL().ExecuteBySql(dataSourceId, strSql);
            }
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
            string change_before = string.IsNullOrEmpty(keyValue) ? "" : tb_stock_infoData.ToJson();
            entity.change_before = change_before;
            string change_after = entity.ToJson();
            entity.change_after = change_after;
            stock_infoIBLL.SaveEntity(keyValue,entity);
            //return Success("保存成功！");
            return BomSuccess("保存成功！", "标准化BOM表", string.IsNullOrEmpty(keyValue) ? OperationType.Create : OperationType.Update, entity.open_id, entity.ToJson(), string.IsNullOrEmpty(keyValue) ? "" : tb_stock_infoData.ToJson());
        }
        #endregion

    }
}
