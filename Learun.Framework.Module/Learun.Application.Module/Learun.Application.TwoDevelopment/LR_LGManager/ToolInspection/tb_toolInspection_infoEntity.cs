using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-12-04 16:43
    /// 描 述：ToolInspection
    /// </summary>
    public class tb_toolInspection_infoEntity 
    {
        #region  实体成员
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// machine_group
        /// </summary>
        [Column("MACHINE_GROUP")]
        public string machine_group { get; set; }
        /// <summary>
        /// product_id
        /// </summary>
        [Column("PRODUCT_ID")]
        public string product_id { get; set; }
        /// <summary>
        /// wf_id
        /// </summary>
        [Column("WF_ID")]
        public string wf_id { get; set; }
        /// <summary>
        /// toolhilt_id
        /// </summary>
        [Column("TOOLHILT_ID")]
        public string toolhilt_id { get; set; }
        /// <summary>
        /// tool_id
        /// </summary>
        [Column("TOOL_ID")]
        public string tool_id { get; set; }
        /// <summary>
        /// toolblade_id
        /// </summary>
        [Column("TOOLBLADE_ID")]
        public string toolblade_id { get; set; }
        /// <summary>
        /// toolpos_id
        /// </summary>
        [Column("TOOLPOS_ID")]
        public string toolpos_id { get; set; }
        /// <summary>
        /// toollength
        /// </summary>
        [Column("TOOLLENGTH")]
        public string toollength { get; set; }
        /// <summary>
        /// tooldia
        /// </summary>
        [Column("TOOLDIA")]
        public string tooldia { get; set; }
        /// <summary>
        /// toollife
        /// </summary>
        [Column("TOOLLIFE")]
        public string toollife { get; set; }
        /// <summary>
        /// oprater
        /// </summary>
        [Column("OPRATER")]
        public string oprater { get; set; }
        /// <summary>
        /// rest_life
        /// </summary>
        [Column("REST_LIFE")]
        public string rest_life { get; set; }
        /// <summary>
        /// string_01
        /// </summary>
        [Column("STRING_01")]
        public string string_01 { get; set; }
        /// <summary>
        /// string_02
        /// </summary>
        [Column("STRING_02")]
        public string string_02 { get; set; }
        /// <summary>
        /// string_03
        /// </summary>
        [Column("STRING_03")]
        public string string_03 { get; set; }
        /// <summary>
        /// string_04
        /// </summary>
        [Column("STRING_04")]
        public string string_04 { get; set; }
        /// <summary>
        /// string_05
        /// </summary>
        [Column("STRING_05")]
        public string string_05 { get; set; }
        /// <summary>
        /// string_06
        /// </summary>
        [Column("STRING_06")]
        public string string_06 { get; set; }
        /// <summary>
        /// string_07
        /// </summary>
        [Column("STRING_07")]
        public string string_07 { get; set; }
        /// <summary>
        /// string_08
        /// </summary>
        [Column("STRING_08")]
        public string string_08 { get; set; }
        /// <summary>
        /// string_09
        /// </summary>
        [Column("STRING_09")]
        public string string_09 { get; set; }
        /// <summary>
        /// string_10
        /// </summary>
        [Column("STRING_10")]
        public string string_10 { get; set; }
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

