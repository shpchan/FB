using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-12-04 16:13
    /// 描 述：toolhilt_info
    /// </summary>
    public class tb_toolhilt_infoEntity 
    {
        #region  实体成员
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// toolHilt_name
        /// </summary>
        [Column("TOOLHILT_NAME")]
        public string toolHilt_name { get; set; }
        /// <summary>
        /// toolHilt_id
        /// </summary>
        [Column("TOOLHILT_ID")]
        public string toolHilt_id { get; set; }
        /// <summary>
        /// rfid
        /// </summary>
        [Column("RFID")]
        public string rfid { get; set; }
        /// <summary>
        /// toolblade_type
        /// </summary>
        [Column("TOOLBLADE_TYPE")]
        public string toolblade_type { get; set; }
        /// <summary>
        /// tool_cj
        /// </summary>
        [Column("TOOL_CJ")]
        public string tool_cj { get; set; }
        /// <summary>
        /// create_time
        /// </summary>
        [Column("CREATE_TIME")]
        public DateTime? create_time { get; set; }
        /// <summary>
        /// create_man
        /// </summary>
        [Column("CREATE_MAN")]
        public string create_man { get; set; }
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

