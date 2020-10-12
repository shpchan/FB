using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-12-04 11:37
    /// 描 述：fast刀具管理
    /// </summary>
    public class tb_tool_infoEntity 
    {
        #region  实体成员
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// tool_name
        /// </summary>
        [Column("TOOL_NAME")]
        public string tool_name { get; set; }
        /// <summary>
        /// tool_id
        /// </summary>
        [Column("TOOL_ID")]
        public string tool_id { get; set; }
        /// <summary>
        /// tool_order
        /// </summary>
        [Column("TOOL_ORDER")]
        public string tool_order { get; set; }
        /// <summary>
        /// tool_gg
        /// </summary>
        [Column("TOOL_GG")]
        public string tool_gg { get; set; }
        /// <summary>
        /// tool_cl
        /// </summary>
        [Column("TOOL_CL")]
        public string tool_cl { get; set; }
        /// <summary>
        /// tool_tc
        /// </summary>
        [Column("TOOL_TC")]
        public string tool_tc { get; set; }
        /// <summary>
        /// tool_cj
        /// </summary>
        [Column("TOOL_CJ")]
        public string tool_cj { get; set; }
        /// <summary>
        /// tool_mrcj
        /// </summary>
        [Column("TOOL_MRCJ")]
        public string tool_mrcj { get; set; }
        /// <summary>
        /// tool_mrcs
        /// </summary>
        [Column("TOOL_MRCS")]
        public int? tool_mrcs { get; set; }
        /// <summary>
        /// create_time
        /// </summary>
        [Column("CREATE_TIME")]
        public DateTime? create_time { get; set; }
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

