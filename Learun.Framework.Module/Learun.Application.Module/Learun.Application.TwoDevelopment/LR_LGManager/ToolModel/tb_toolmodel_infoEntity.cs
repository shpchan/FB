using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-09-16 14:32
    /// 描 述：tool_model
    /// </summary>
    public class tb_toolmodel_infoEntity 
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
        /// toolmodel_id
        /// </summary>
        [Column("TOOLMODEL_ID")]
        public string toolmodel_id { get; set; }
        /// <summary>
        /// toolmodel_name
        /// </summary>
        [Column("TOOLMODEL_NAME")]
        public string toolmodel_name { get; set; }
        /// <summary>
        /// tool_grp
        /// </summary>
        [Column("TOOL_GRP")]
        public int? tool_grp { get; set; }
        /// <summary>
        /// tool_pos
        /// </summary>
        [Column("TOOL_POS")]
        public int? tool_pos { get; set; }
        /// <summary>
        /// machine_id
        /// </summary>
        [Column("MACHINE_ID")]
        public int? machine_id { get; set; }
        /// <summary>
        /// state
        /// </summary>
        [Column("STATE")]
        public string state { get; set; }
        /// <summary>
        /// short_name
        /// </summary>
        [Column("SHORT_NAME")]
        public string short_name { get; set; }
        /// <summary>
        /// count_type
        /// </summary>
        [Column("COUNT_TYPE")]
        public string count_type { get; set; }
        /// <summary>
        /// initial_life
        /// </summary>
        [Column("INITIAL_LIFE")]
        public decimal? initial_life { get; set; }
        /// <summary>
        /// life_prediction
        /// </summary>
        [Column("LIFE_PREDICTION")]
        public decimal? life_prediction { get; set; }
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
    }
}

