using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-09-17 14:56
    /// 描 述：装/卸刀记录
    /// </summary>
    public class tb_toolinstall_recordEntity 
    {
        #region  实体成员
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// tool_no
        /// </summary>
        [Column("TOOL_NO")]
        public string tool_no { get; set; }
        /// <summary>
        /// tool_group
        /// </summary>
        [Column("TOOL_GRP")]
        public string tool_grp { get; set; }
        /// <summary>
        /// tool_pos
        /// </summary>
        [Column("TOOL_POS")]
        public string tool_pos { get; set; }
        /// <summary>
        /// machine_id
        /// </summary>
        [Column("MACHINE_ID")]
        public string machine_id { get; set; }
        /// <summary>
        /// action
        /// </summary>
        [Column("ACTION")]
        public string action { get; set; }
        /// <summary>
        /// record_time
        /// </summary>
        [Column("RECORD_TIME")]
        public DateTime? record_time { get; set; }
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

