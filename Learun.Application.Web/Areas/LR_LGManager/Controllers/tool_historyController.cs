using Learun.Util;
using System.Data;
using Learun.Application.TwoDevelopment.LR_LGManager;
using System.Web.Mvc;
using System.Collections.Generic;
using Learun.Application.Base.SystemModule;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Learun.DataBase.Repository;
using MongoDB.Bson;
using MachineDesign.Models;

namespace Learun.Application.Web.Areas.LR_LGManager.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-06-04 14:05
    /// 描 述：刀具寿命管理
    /// </summary>
    public class tool_historyController : MvcControllerBase
    {
        private tool_historyIBLL tool_historyIBLL = new tool_historyBLL();

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
        /// <summary>
        /// 表单页-装刀
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult zhuangdao()
        {
            ReadData rd = new ReadData();
            ViewData["ListDeviceNum"] = rd.readDeviceNum();
            return View();
        }
        /// <summary>
        /// 表单页-卸刀
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult xiedao()
        {
            return View();
        }
        /// <summary>
        /// 表单页-修改装卸刀
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult change()
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
            var data = tool_historyIBLL.GetPageList(paginationobj, queryJson);
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
            var tb_mac_tool_historyData = tool_historyIBLL.Gettb_mac_tool_historyEntity( keyValue );
            var jsonData = new {
                tb_mac_tool_history = tb_mac_tool_historyData,
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取表单数据-装刀
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetZhuangdaoData(string keyValue)
        {
            var tb_mac_tool_historyData = tool_historyIBLL.Gettb_mac_tool_historyEntity(keyValue);
            var jsonData = new
            {
                tb_mac_tool_history = tb_mac_tool_historyData,
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取表单数据-卸刀
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetXiedaoData(string keyValue)
        {
            var tb_mac_tool_historyData = tool_historyIBLL.Gettb_mac_tool_historyEntity(keyValue);
            var jsonData = new
            {
                tb_mac_tool_history = tb_mac_tool_historyData,
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取表单数据-修改
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetChangeData(string keyValue)
        {
            var tb_mac_tool_historyData = tool_historyIBLL.Gettb_mac_tool_historyEntity(keyValue);
            var jsonData = new
            {
                tb_mac_tool_history = tb_mac_tool_historyData,
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
            tool_historyIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity)
        {
            tb_mac_tool_historyEntity entity = strEntity.ToObject<tb_mac_tool_historyEntity>();
            tool_historyIBLL.SaveEntity(keyValue,entity);
            return Success("保存成功！");
        }
        /// <summary>
        /// 保存实体数据（装刀）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm_ZD(string keyValue, string strEntity)
        {
            tb_mac_tool_historyEntity entity = strEntity.ToObject<tb_mac_tool_historyEntity>();
            tool_historyIBLL.SaveEntity(keyValue, entity);
            string dataSourceId = "0d48c6ea-f8d0-4c27-a826-8d63df4d84c1";
            string id= Guid.NewGuid().ToString();
            string tool_no = entity.tool_no.ToString();
            string machine_id = entity.machine_id.ToString();
            string tool_grp = entity.tool_grp.ToString();
            string tool_pos = entity.tool_pos.ToString();
            string action = "装刀";
            DateTime record_time = System.DateTime.Now;
            string strSql = "insert into tb_toolinstall_record values('"+id+ "','"+tool_no+"','"+tool_grp+"','"+tool_pos+"', '"+machine_id+"','"+action+"','"+record_time + "')";
            new DatabaseLinkBLL().ExecuteBySql(dataSourceId, strSql);
            BsonDocument doc = new BsonDocument
            {
                {"time_stamp", record_time.Ticks.ToString()},
                {"time_second", (record_time.Ticks - (record_time.Ticks % TimeSpan.TicksPerSecond)).ToString()},
                {"calc_date", record_time.ToString("yyyyMMdd")},
                {"tool_no", tool_no},
                {"tool_grp",tool_grp},
                {"tool_pos", tool_pos},
                {"machine_id", machine_id},
                {"action", action},
                {"record_time", record_time}
            };
            RepositoryFactory BR = new RepositoryFactory();
            BR.BaseRepository("MongoDB").InsertOne<BsonDocument>("clc_init_install_tool", doc);
            return Success("保存成功！");
        }
        /// <summary>
        /// 保存实体数据（卸刀）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm_XD(string keyValue, string strEntity)
        {
            tb_mac_tool_historyEntity entity = strEntity.ToObject<tb_mac_tool_historyEntity>();
            string machine_id = entity.machine_id.ToString();
            string tool_grp = entity.tool_grp.ToString();
            string tool_pos = entity.tool_pos.ToString();
            entity.machine_id = 0;//卸刀，设备置为0
            entity.tool_pos = 0;//卸刀，刀位置为0
            tool_historyIBLL.SaveEntity(keyValue, entity);
            string dataSourceId = "0d48c6ea-f8d0-4c27-a826-8d63df4d84c1";
            string id = Guid.NewGuid().ToString();
            string tool_no = entity.tool_no.ToString();
            string action = "卸刀";
            DateTime record_time = System.DateTime.Now;
            string strSql = "insert into tb_toolinstall_record values('" + id + "','" + tool_no + "','" + tool_grp + "','" + tool_pos + "', '" + machine_id + "','" + action + "','" + record_time + "')";
            new DatabaseLinkBLL().ExecuteBySql(dataSourceId, strSql);
            return Success("保存成功！");
        }
        /// <summary>
        /// 保存实体数据（修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm_Change(string keyValue, string strEntity)
        {
            tb_mac_tool_historyEntity entity = strEntity.ToObject<tb_mac_tool_historyEntity>();
            JObject obj = (JObject)JsonConvert.DeserializeObject(strEntity);
 
            string machine_id = entity.machine_id.ToString();

            string f_machine_id = obj["f_machine_id"].ToString();

            tool_historyIBLL.SaveEntity(keyValue, entity);
            string dataSourceId = "0d48c6ea-f8d0-4c27-a826-8d63df4d84c1";
            string id = Guid.NewGuid().ToString();
            string tool_no = entity.tool_no.ToString();
            string tool_grp = entity.tool_grp.ToString();
            string tool_pos = entity.tool_pos.ToString();
            string action = "卸刀";
            DateTime record_time = System.DateTime.Now;
            string strSql = "insert into tb_toolinstall_record values('" + id + "','" + tool_no + "','" + tool_grp + "','" + tool_pos + "', '" + f_machine_id + "','" + action + "','" + record_time + "')";
            new DatabaseLinkBLL().ExecuteBySql(dataSourceId, strSql);

             action = "装刀";
            id = Guid.NewGuid().ToString();
            strSql = "insert into tb_toolinstall_record values('" + id + "','" + tool_no + "','" + tool_grp + "','" + tool_pos + "', '" + machine_id + "','" + action + "','" + record_time + "')";
            new DatabaseLinkBLL().ExecuteBySql(dataSourceId, strSql);
            BsonDocument doc = new BsonDocument
            {
                {"time_stamp", record_time.Ticks.ToString()},
                {"time_second", (record_time.Ticks - (record_time.Ticks % TimeSpan.TicksPerSecond)).ToString()},
                {"calc_date", record_time.ToString("yyyyMMdd")},
                {"tool_no", tool_no},
                {"tool_grp",tool_grp},
                {"tool_pos", tool_pos},
                {"machine_id", machine_id},
                {"action", action},
                {"record_time", record_time}
            };
            RepositoryFactory BR = new RepositoryFactory();
            BR.BaseRepository("MongoDB").InsertOne<BsonDocument>("clc_init_install_tool", doc);
            return Success("保存成功！");
        }
        #endregion

    }
}
