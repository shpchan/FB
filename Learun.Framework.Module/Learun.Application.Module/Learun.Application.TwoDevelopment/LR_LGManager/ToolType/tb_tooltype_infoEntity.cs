using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-09-16 13:44
    /// 描 述：tool_type
    /// </summary>
    public class tb_tooltype_infoEntity 
    {
        #region  实体成员
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// tooltype_id
        /// </summary>
        [Column("TOOLTYPE_ID")]
        public string tooltype_id { get; set; }
        /// <summary>
        /// tooltype_name
        /// </summary>
        [Column("TOOLTYPE_NAME")]
        public string tooltype_name { get; set; }
        /// <summary>
        /// short_name
        /// </summary>
        [Column("SHORT_NAME")]
        public string short_name { get; set; }
        /// <summary>
        /// parent_id
        /// </summary>
        [Column("PARENT_ID")]
        public string parent_id { get; set; }
        #endregion

        #region  扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.id = keyValue;
        }
        #endregion
        #region  扩展字段
        #endregion
    }
}

