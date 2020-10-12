using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-11-26 18:06
    /// 描 述：采购订单
    /// </summary>
    public class tb_order_infoEntity 
    {
        #region  实体成员
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// contract_id
        /// </summary>
        [Column("CONTRACT_ID")]
        public string contract_id { get; set; }
        /// <summary>
        /// supplier
        /// </summary>
        [Column("SUPPLIER")]
        public string supplier { get; set; }
        /// <summary>
        /// order_time
        /// </summary>
        [Column("ORDER_TIME")]
        public DateTime? order_time { get; set; }
        /// <summary>
        /// order_id
        /// </summary>
        [Column("ORDER_ID")]
        public string order_id { get; set; }
        /// <summary>
        /// change_time
        /// </summary>
        [Column("CHANGE_TIME")]
        public DateTime? change_time { get; set; }
        /// <summary>
        /// change_reason
        /// </summary>
        [Column("CHANGE_REASON")]
        public string change_reason { get; set; }
        /// <summary>
        /// materiel_id
        /// </summary>
        [Column("MATERIEL_ID")]
        public string materiel_id { get; set; }
        /// <summary>
        /// materiel_name
        /// </summary>
        [Column("MATERIEL_NAME")]
        public string materiel_name { get; set; }
        /// <summary>
        /// materiel_gg
        /// </summary>
        [Column("MATERIEL_GG")]
        public string materiel_gg { get; set; }
        /// <summary>
        /// materiel_unit
        /// </summary>
        [Column("MATERIEL_UNIT")]
        public string materiel_unit { get; set; }
        /// <summary>
        /// money_type
        /// </summary>
        [Column("MONEY_TYPE")]
        public string money_type { get; set; }
        /// <summary>
        /// materiel_num
        /// </summary>
        [Column("MATERIEL_NUM")]
        public int? materiel_num { get; set; }
        /// <summary>
        /// arrival_time
        /// </summary>
        [Column("ARRIVAL_TIME")]
        public DateTime? arrival_time { get; set; }
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

