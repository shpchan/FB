using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-11-26 17:54
    /// 描 述：采购申请单
    /// </summary>
    public class tb_apply_infoEntity 
    {
        #region  实体成员
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// apply_time
        /// </summary>
        [Column("APPLY_TIME")]
        public DateTime? apply_time { get; set; }
        /// <summary>
        /// bill_id
        /// </summary>
        [Column("BILL_ID")]
        public string bill_id { get; set; }
        /// <summary>
        /// business_type
        /// </summary>
        [Column("BUSINESS_TYPE")]
        public string business_type { get; set; }
        /// <summary>
        /// check_flag
        /// </summary>
        [Column("CHECK_FLAG")]
        public string check_flag { get; set; }
        /// <summary>
        /// close_flag
        /// </summary>
        [Column("CLOSE_FLAG")]
        public string close_flag { get; set; }
        /// <summary>
        /// use_department
        /// </summary>
        [Column("USE_DEPARTMENT")]
        public string use_department { get; set; }
        /// <summary>
        /// applyer
        /// </summary>
        [Column("APPLYER")]
        public string applyer { get; set; }
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
        /// materiel_num
        /// </summary>
        [Column("MATERIEL_NUM")]
        public int? materiel_num { get; set; }
        /// <summary>
        /// arrival_time
        /// </summary>
        [Column("ARRIVAL_TIME")]
        public DateTime? arrival_time { get; set; }
        /// <summary>
        /// supplier
        /// </summary>
        [Column("SUPPLIER")]
        public string supplier { get; set; }
        /// <summary>
        /// rowclose_flag
        /// </summary>
        [Column("ROWCLOSE_FLAG")]
        public string rowclose_flag { get; set; }
        /// <summary>
        /// materiel_id
        /// </summary>
        [Column("MATERIEL_ID")]
        public string materiel_id { get; set; }
        /// <summary>
        /// adviseapply_time
        /// </summary>
        [Column("ADVISEAPPLY_TIME")]
        public DateTime? adviseapply_time { get; set; }
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

