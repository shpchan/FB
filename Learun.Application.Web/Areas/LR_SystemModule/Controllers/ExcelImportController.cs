using Learun.Application.Excel;
using System.Collections.Generic;
using System.Web.Mvc;
using Learun.Util;
using System.Data;
using Learun.Application.Base.SystemModule;
using System;
using System.Drawing;
using System.Text;
using Learun.DataBase.Repository;
using System.Data.SqlClient;
using Learun.DataBase;
using System.Configuration;
using System.Linq;

namespace Learun.Application.Web.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创建人：陈彬彬
    /// 日 期：2017.04.01
    /// 描 述：Excel导入管理
    /// </summary>
    public class ExcelImportController : MvcControllerBase
    {
        private ExcelImportIBLL excelImportIBLL = new ExcelImportBLL();
        private AnnexesFileIBLL annexesFileIBLL = new AnnexesFileBLL();
        #region  视图功能
        /// <summary>
        /// 导入模板管理页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 导入模板管理表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 设置字段属性
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetFieldForm()
        {
            return View();
        }

        /// <summary>
        /// 导入页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ImportForm()
        {
            return View();
        }

        #endregion

        #region  获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = excelImportIBLL.GetPageList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records,
            };
            return JsonResult(jsonData);
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="moduleId">功能模块主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetList(string moduleId)
        {
            var data = excelImportIBLL.GetList(moduleId);
            return JsonResult(data);
        }
        /// <summary>
        /// 获取表单数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            ExcelImportEntity entity = excelImportIBLL.GetEntity(keyValue);
            IEnumerable<ExcelImportFieldEntity> list = excelImportIBLL.GetFieldList(keyValue);
            var data = new
            {
                entity = entity,
                list = list
            };
            return JsonResult(data);
        }
        #endregion

        #region  提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string strEntity, string strList)
        {
            ExcelImportEntity entity = strEntity.ToObject<ExcelImportEntity>();
            List<ExcelImportFieldEntity> filedList = strList.ToObject<List<ExcelImportFieldEntity>>();
            excelImportIBLL.SaveEntity(keyValue, entity, filedList);
            return Success("保存成功！");
        }
        /// <summary>
        /// 删除表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            excelImportIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 更新表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult UpdateForm(string keyValue, ExcelImportEntity entity)
        {
            excelImportIBLL.UpdateEntity(keyValue, entity);
            return Success("操作成功！");
        }
        #endregion

        #region  扩展方法
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void DownSchemeFile(string keyValue)
        {
            ExcelImportEntity templateInfo = excelImportIBLL.GetEntity(keyValue);
            IEnumerable<ExcelImportFieldEntity> fileds = excelImportIBLL.GetFieldList(keyValue);

            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig();
            excelconfig.FileName = Server.UrlDecode(templateInfo.F_Name) + ".xls";
            excelconfig.IsAllSizeColumn = true;
            excelconfig.ColumnEntity = new List<ColumnModel>();
            //表头
            DataTable dt = new DataTable();
            foreach (var col in fileds)
            {
                if (col.F_RelationType != 1 && col.F_RelationType != 4 && col.F_RelationType != 5 && col.F_RelationType != 6 && col.F_RelationType != 7)
                {
                    excelconfig.ColumnEntity.Add(new ColumnModel()
                    {
                        Column = col.F_Name,
                        ExcelColumn = col.F_ColName,
                        Alignment = "center",
                    });
                    dt.Columns.Add(col.F_Name, typeof(string));
                }
            }
            ExcelHelper.ExcelDownload(dt, excelconfig);
        }

        /// <summary>
        /// excel文件导入（通用）
        /// </summary>
        /// <param name="templateId">模板Id</param>
        /// <param name="fileId">文件主键</param>
        /// <param name="chunks">分片数</param>
        /// <param name="ext">文件扩展名</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExecuteImportExcel(string templateId, string fileId, int chunks,string ext)
        {
            string errinfo = "";
            UserInfo userInfo = LoginUserInfo.Get();
            string path = annexesFileIBLL.SaveAnnexes(fileId, fileId + "."+ ext, chunks, userInfo);
            if (!string.IsNullOrEmpty(path))
            {
                DataTable dt = ExcelHelper.ExcelImport(path);
                //string res = excelImportIBLL.ImportTable(templateId, fileId, dt);//导入
                string res = "";
                if (templateId == "7c4ceb22-0c23-449c-895a-ecb860cb24c0")
                {
                   
                     res = InsertSqlBulkCopy(dt, "tb_stock_info", ref errinfo);//导入
                }
                else {
                     res = excelImportIBLL.ImportTable(templateId, fileId, dt);//导入
                }
                var data = new
                {
                    Success = res.Split('|')[0],
                    Fail = res.Split('|')[1]
                };
                if (errinfo != "")
                {
                    return Fail("导入数据失败!" + errinfo);
                }
                else
                {
                    return JsonResult(data);
                }
            }
            else
            {
                return Fail("导入数据失败!"+ errinfo);
            }
        }

        /// <summary>
        /// excel文件导入（通用）
        /// </summary>
        /// <param name="templateId">模板Id</param>
        /// <param name="fileId">文件主键</param>
        /// <param name="chunks">分片数</param>
        /// <param name="ext">文件扩展名</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExecuteImportExcel_bak(string templateId, string fileId, int chunks, string ext)
        {
            string errinfo = "";
            RepositoryFactory BR = new RepositoryFactory();
            BR.BaseRepository("BaseDb1").ExecuteByProc("pro_stock_info_bak");
            UserInfo userInfo = LoginUserInfo.Get();
            string path = annexesFileIBLL.SaveAnnexes(fileId, fileId + "." + ext, chunks, userInfo);
            if (!string.IsNullOrEmpty(path))
            {
                DataTable dt = ExcelHelper.ExcelImport(path);
                // string res = excelImportIBLL.ImportTable(templateId, fileId, dt);//导入
                
                string res = InsertSqlBulkCopy(dt, "tb_stock_info", ref errinfo);//导入
                var data = new
                {
                    Success = res.Split('|')[0],
                    Fail = res.Split('|')[1]
                };
                if (errinfo != "") {
                    return Fail("导入数据失败!"+errinfo);
                }
                else {
                    return JsonResult(data);
                }
            }
            else
            {
                return Fail("导入数据失败!");
            }
        }
        public string InsertSqlBulkCopy(DataTable dt, string tableName, ref string errinfo)
        {
            if (dt.Rows.Count < 1)
            {
                return "1|0";
            }

            string[] s= GetColumnsByDataTable(dt);
            if (HasRepeatData(dt, s[0]))
            {
                errinfo += "网格中存在重复的展开ID!" + ""; 
                return "0|1"; 
            }
            if (HasRepeatDataInBase(dt, s[0]))
            {
                errinfo += "系统中已存在重复的展开ID!" + "";
                return "0|1";
            }
            string connectionString = ConfigurationManager.ConnectionStrings["BaseDb1"].ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction sqlTran = conn.BeginTransaction(); // 开始事务
                //开始事务
                using (SqlBulkCopy bcp = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, sqlTran))
                {
                    string sql = @"
                    select  F_ColName, F_Name from LR_Excel_ImportFileds where F_ImportId='7c4ceb22-0c23-449c-895a-ecb860cb24c0' order by F_SortCode ";
                    string dataSourceId = "f2d587de-43e5-4310-b968-4544f4961a39";
                    DataTable Importsolution = new DatabaseLinkBLL().FindTable(dataSourceId, sql);
                    foreach (DataRow dr in Importsolution.Rows)
                    {
                        if (dr["F_Name"].ToString() == "id") { }
                        else
                        {
                            bcp.ColumnMappings.Add(dr["F_ColName"].ToString(), dr["F_Name"].ToString());
                        }
                    }

                    //指定目标数据库的表名
                    bcp.DestinationTableName = tableName;

                    try
                    {
                        //bcp.BulkCopyTimeout = 180;
                        //写入数据库表?dt?是数据源DataTable
                        bcp.WriteToServer(dt);
                        sqlTran.Commit();

                    }
                    catch (Exception ex)
                    {
                        errinfo += ex.Message + "";
                        sqlTran.Rollback();
                        return "0|1";
                    }
                    finally
                    {
                        sqlTran.Dispose();
                    }

                }
            }
            return "1|0";
        }

        #region 根据datatable获得列名     public static string[] GetColumnsByDataTable(DataTable dt)
        /// <summary>
        /// 根据datatable获得列名
        /// </summary>
        /// <param name="dt">表对象</param>
        /// <returns>返回结果的数据列数组</returns>
        public static string[] GetColumnsByDataTable(DataTable dt)
        {
            string[] strColumns = null;


            if (dt.Columns.Count > 0)
            {
                int columnNum = 0;
                columnNum = dt.Columns.Count;
                strColumns = new string[columnNum];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    strColumns[i] = dt.Columns[i].ColumnName;
                }
            }


            return strColumns;
        }
        #endregion

        public bool HasRepeatData(DataTable dt, string colName)
        {
            bool flag = false;
            if (dt.DefaultView.ToTable(true, colName).Rows.Count < dt.Rows.Count)
            {
                flag = true;//有重复数据
            }
            return flag;
        }

        public bool HasRepeatDataInBase(DataTable dt, string colName)
        {
            bool flag = false;
            string sql = @"
                    select  open_id  from tb_stock_info  where state not in ('7') or state is null ";
            string dataSourceId = "0d48c6ea-f8d0-4c27-a826-8d63df4d84c1";
            DataTable Importsolution = new DatabaseLinkBLL().FindTable(dataSourceId, sql);
            foreach (DataRow dr in dt.DefaultView.ToTable(true, colName).Rows)
            {
                if (Importsolution.Select("open_id='" + dr[colName].ToString() + "'").Length > 0)
                {
                    flag = true;//有重复数据
                }
            }
            return flag;
        }

        /// <summary>
        /// 下载文件(导入文件未被导入的数据)
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void DownImportErrorFile(string fileId,string fileName)
        {
            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig();
            excelconfig.FileName = Server.UrlDecode("未导入错误数据【" + fileName + "】") + ".xls";
            excelconfig.IsAllSizeColumn = true;
            excelconfig.ColumnEntity = new List<ColumnModel>();
            //表头
            DataTable dt = excelImportIBLL.GetImportError(fileId);
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName == "导入错误")
                {
                    excelconfig.ColumnEntity.Add(new ColumnModel()
                    {
                        Column = col.ColumnName,
                        ExcelColumn = col.ColumnName,
                        Alignment = "center",
                        Background = Color.Red
                    });
                }
                else
                {
                    excelconfig.ColumnEntity.Add(new ColumnModel()
                    {
                        Column = col.ColumnName,
                        ExcelColumn = col.ColumnName,
                        Alignment = "center",
                    });
                }

              
            }
            ExcelHelper.ExcelDownload(dt, excelconfig);
        }
        #endregion
    }
}