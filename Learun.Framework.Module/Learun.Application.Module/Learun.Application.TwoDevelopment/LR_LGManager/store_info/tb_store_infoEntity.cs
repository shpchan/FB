using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-11-26 17:40
    /// 描 述：仓库即时库存
    /// </summary>
    public class tb_store_infoEntity 
    {
        #region  实体成员
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// materiel_id
        /// </summary>
        [Column("MATERIEL_ID")]
        public string materiel_id { get; set; }
        /// <summary>
        /// supplier
        /// </summary>
        [Column("SUPPLIER")]
        public string supplier { get; set; }
        /// <summary>
        /// store_id
        /// </summary>
        [Column("STORE_ID")]
        public string store_id { get; set; }
        /// <summary>
        /// store_name
        /// </summary>
        [Column("STORE_NAME")]
        public string store_name { get; set; }
        /// <summary>
        /// storepos_id
        /// </summary>
        [Column("STOREPOS_ID")]
        public string storepos_id { get; set; }
        /// <summary>
        /// storepos_name
        /// </summary>
        [Column("STOREPOS_NAME")]
        public string storepos_name { get; set; }
        /// <summary>
        /// buy_time
        /// </summary>
        [Column("BUY_TIME")]
        public DateTime? buy_time { get; set; }
        /// <summary>
        /// quality_period
        /// </summary>
        [Column("QUALITY_PERIOD")]
        public int? quality_period { get; set; }
        /// <summary>
        /// due_date
        /// </summary>
        [Column("DUE_DATE")]
        public DateTime? due_date { get; set; }
        /// <summary>
        /// baseunit
        /// </summary>
        [Column("BASEUNIT")]
        public string baseunit { get; set; }
        /// <summary>
        /// baseunit_num
        /// </summary>
        [Column("BASEUNIT_NUM")]
        public int? baseunit_num { get; set; }
        /// <summary>
        /// comunit
        /// </summary>
        [Column("COMUNIT")]
        public string comunit { get; set; }
        /// <summary>
        /// comunit_num
        /// </summary>
        [Column("COMUNIT_NUM")]
        public int? comunit_num { get; set; }
        /// <summary>
        /// assistunit
        /// </summary>
        [Column("ASSISTUNIT")]
        public string assistunit { get; set; }
        /// <summary>
        /// changenum
        /// </summary>
        [Column("CHANGENUM")]
        public int? changenum { get; set; }
        /// <summary>
        /// assistunit_num
        /// </summary>
        [Column("ASSISTUNIT_NUM")]
        public int? assistunit_num { get; set; }
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

