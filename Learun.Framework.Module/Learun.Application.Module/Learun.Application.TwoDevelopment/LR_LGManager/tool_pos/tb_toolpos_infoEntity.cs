using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-09-29 14:14
    /// 描 述：设备刀位配置
    /// </summary>
    public class tb_toolpos_infoEntity 
    {
        #region  实体成员
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// toolpos_id
        /// </summary>
        [Column("TOOLPOS_ID")]
        public string toolpos_id { get; set; }
        /// <summary>
        /// toolpos_name
        /// </summary>
        [Column("TOOLPOS_NAME")]
        public string toolpos_name { get; set; }
        /// <summary>
        /// machine_id
        /// </summary>
        [Column("MACHINE_ID")]
        public string machine_id { get; set; }
        /// <summary>
        /// create_time
        /// </summary>
        [Column("CREATE_TIME")]
        public DateTime? create_time { get; set; }
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

