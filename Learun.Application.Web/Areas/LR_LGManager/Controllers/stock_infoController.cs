using Learun.Util;
using Learun.Util.Operat;
using System.Data;
using Learun.Application.TwoDevelopment.LR_LGManager;
using System.Web.Mvc;
using System.Collections.Generic;
using Learun.Application.Base.SystemModule;
using Newtonsoft.Json;
using System.Linq;
using System.Text;

namespace Learun.Application.Web.Areas.LR_LGManager.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-11-12 11:21
    /// 描 述：stock_info
    /// </summary>
    public class stock_infoController : MvcControllerBase
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
            var data = stock_infoIBLL.GetPageList(paginationobj, queryJson);
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
            if (tb_stock_infoData.state == "2") { return Fail("已提交审核，无法删除！"); }
            //stock_infoIBLL.DeleteEntity(keyValue);
            string dataSourceId = "0d48c6ea-f8d0-4c27-a826-8d63df4d84c1";
            string strSql = "update  tb_stock_info set state='5', oprate = '提交删除' where id='" + keyValue + "'";
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
            if (tb_stock_infoData.state == "2") { return Fail("已提交审核，无需再次提交！"); }
            if (tb_stock_infoData.state == "3" || string.IsNullOrEmpty(tb_stock_infoData.state)) { return Fail("没有做任何编辑，无法提交审核！"); }
            string dataSourceId = "0d48c6ea-f8d0-4c27-a826-8d63df4d84c1";
            string strSql = "update  tb_stock_info set state='2' where id='"+ keyValue + "'";
            new DatabaseLinkBLL().ExecuteBySql(dataSourceId, strSql);
            //return Success("提交成功！");
            return BomSuccess("提交成功！", "标准化BOM表", OperationType.Submit, tb_stock_infoData.open_id, "", tb_stock_infoData.ToJson());
        }

        /// <summary>
        /// 比较两个Json对象
        /// </summary>
        /// <param name="SourceContentJson">前json</param>
        /// <param name="ExecuteResultJson">后Json</param>
        /// <returns></returns>
        public static string JsonDifferent(string SourceContentJson, string ExecuteResultJson)
        {
            Dictionary<string, string> dictS = JsonConvert.DeserializeObject<Dictionary<string, string>>(SourceContentJson);
            Dictionary<string, string> dictE = JsonConvert.DeserializeObject<Dictionary<string, string>>(ExecuteResultJson);

            var dict3 = dictE.Where(entry => dictS[entry.Key] != entry.Value)
                 .ToDictionary(entry => entry.Key, entry => entry.Value);
            var dict4 = dictS.Where(entry => dictE[entry.Key] != entry.Value)
                 .ToDictionary(entry => entry.Key, entry => entry.Value);

            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            //先把键都合并到dict中，值都是新创建的 
            foreach (var key in dict3.Keys)
            {
                if (!dict.ContainsKey(key))
                    dict.Add(key, new List<string>());
            }
            foreach (var key in dict4.Keys)
            {
                if (!dict.ContainsKey(key))
                    dict.Add(key, new List<string>());
            }
            //分别将值添加进去 
            foreach (var ele in dict4)
            {
                dict[ele.Key].Add(ele.Value);
            }
            foreach (var ele in dict3)
            {
                dict[ele.Key].Add(ele.Value);
            }
            StringBuilder str = new StringBuilder();
            foreach (var item in dict)
            {
                if(item.Key=="id"|| item.Key == "state"||item.Key == "change_after" || item.Key == "change_before" || item.Key == "oprate" || item.Key == "diff_number" || item.Key == "store_number")
                { }
                else{
                    str.Append(ToChineseName(item.Key) + ":" + item.Value[0] + "->" + item.Value[1] + ";");
                }
            }
            return str.ToString();
        }

        /// <summary>
        /// 返回中文字段名
        /// </summary>
        /// <param name="English">英文字段</param>
        /// <returns></returns>
        public static string ToChineseName(string English)
        {
            string str = "";
            string sql = @"
                    select  F_ColName, F_Name from LR_Excel_ImportFileds where F_ImportId='7c4ceb22-0c23-449c-895a-ecb860cb24c0' order by F_SortCode ";
            string dataSourceId = "f2d587de-43e5-4310-b968-4544f4961a39";
            DataTable Importsolution = new DatabaseLinkBLL().FindTable(dataSourceId, sql);
            foreach (DataRow dr in Importsolution.Rows)
            {
                if (dr["F_Name"].ToString() == English)
                {

                    str=dr["F_ColName"].ToString();
                }
            }
            return str;
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
            var tb_stock_infoData_compare = tb_stock_infoData;
            if (!string.IsNullOrEmpty(keyValue))
            {
                tb_stock_infoData_compare.change_after = "";
                tb_stock_infoData_compare.change_before = "";
                tb_stock_infoData_compare.diff_number = "";
                tb_stock_infoData_compare.store_number = "";
                if (tb_stock_infoData.state == "2") { return Fail("已提交审核，无法修改！"); }
            }
            tb_stock_infoEntity entity = strEntity.ToObject<tb_stock_infoEntity>();
            string state = string.IsNullOrEmpty(keyValue) ? "1" : "6";
            entity.state = state;
            string Change_result = string.IsNullOrEmpty(keyValue) ? "":JsonDifferent(tb_stock_infoData_compare.ToJson(), entity.ToJson());
            string oprate = string.IsNullOrEmpty(keyValue) ? "提交新增" : Change_result;
            entity.oprate = oprate;
            string change_before = string.IsNullOrEmpty(keyValue) ? "" : tb_stock_infoData.ToJson();
            entity.change_before = change_before;
            string change_after = entity.ToJson();
            entity.change_after = change_after;
            if (string.IsNullOrEmpty(keyValue))
            {
                stock_infoIBLL.SaveEntity(keyValue, entity);//新增
            }
            else
            {
                tb_stock_infoData.change_after = entity.ToJson();//编辑
                tb_stock_infoData.change_before = tb_stock_infoData.ToJson();
                tb_stock_infoData.oprate = oprate;
                tb_stock_infoData.state = state;
                stock_infoIBLL.SaveEntity(keyValue, tb_stock_infoData);//老的
            }
            //return Success("保存成功！");
            return BomSuccess("保存成功！", "标准化BOM表", string.IsNullOrEmpty(keyValue) ? OperationType.Create : OperationType.Update, entity.open_id, entity.ToJson(), string.IsNullOrEmpty(keyValue) ? "" : Change_result);
        }
        #endregion

        #region  验证数据
        /// <summary>
        /// 展开ID不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="opne_id">展开ID</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ExistOpen_id(string keyValue, string Open_id)
        {
            bool res = stock_infoIBLL.ExistAccount(Open_id, keyValue);
            return JsonResult(res);
        }
        #endregion
    }
}
