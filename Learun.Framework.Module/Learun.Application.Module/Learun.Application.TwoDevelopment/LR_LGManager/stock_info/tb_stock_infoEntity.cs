using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-11-12 11:21
    /// 描 述：stock_info
    /// </summary>
    public class tb_stock_infoEntity 
    {
        #region  实体成员
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// open_id
        /// </summary>
        [Column("OPEN_ID")]
        public string open_id { get; set; }
        /// <summary>
        /// start_time
        /// </summary>
        [Column("START_TIME")]
        public DateTime? start_time { get; set; }
        /// <summary>
        /// open_time
        /// </summary>
        [Column("OPEN_TIME")]
        public DateTime? open_time { get; set; }
        /// <summary>
        /// platform_name
        /// </summary>
        [Column("PLATFORM_NAME")]
        public string platform_name { get; set; }
        /// <summary>
        /// designer
        /// </summary>
        [Column("DESIGNER")]
        public string designer { get; set; }
        /// <summary>
        /// platform_id
        /// </summary>
        [Column("PLATFORM_ID")]
        public string platform_id { get; set; }
        /// <summary>
        /// unit_name
        /// </summary>
        [Column("UNIT_NAME")]
        public string unit_name { get; set; }
        /// <summary>
        /// unit_id
        /// </summary>
        [Column("UNIT_ID")]
        public string unit_id { get; set; }
        /// <summary>
        /// order_number
        /// </summary>
        [Column("ORDER_NUMBER")]
        public int? order_number { get; set; }
        /// <summary>
        /// type
        /// </summary>
        [Column("TYPE")]
        public string type { get; set; }
        /// <summary>
        /// element_id
        /// </summary>
        [Column("ELEMENT_ID")]
        public string element_id { get; set; }
        /// <summary>
        /// element_name
        /// </summary>
        [Column("ELEMENT_NAME")]
        public string element_name { get; set; }
        /// <summary>
        /// element_cz
        /// </summary>
        [Column("ELEMENT_CZ")]
        public string element_cz { get; set; }
        /// <summary>
        /// element_gg
        /// </summary>
        [Column("ELEMENT_GG")]
        public string element_gg { get; set; }
        /// <summary>
        /// element_zl
        /// </summary>
        [Column("ELEMENT_ZL")]
        public string element_zl { get; set; }
        /// <summary>
        /// single_num
        /// </summary>
        [Column("SINGLE_NUM")]
        public int? single_num { get; set; }
        /// <summary>
        /// element_unit
        /// </summary>
        [Column("ELEMENT_UNIT")]
        public string element_unit { get; set; }
        /// <summary>
        /// skin_stress
        /// </summary>
        [Column("SKIN_STRESS")]
        public string skin_stress { get; set; }
        /// <summary>
        /// hot_stress
        /// </summary>
        [Column("HOT_STRESS")]
        public string hot_stress { get; set; }
        /// <summary>
        /// element_color
        /// </summary>
        [Column("ELEMENT_COLOR")]
        public string element_color { get; set; }
        /// <summary>
        /// element_supplier
        /// </summary>
        [Column("ELEMENT_SUPPLIER")]
        public string element_supplier { get; set; }
        /// <summary>
        /// note
        /// </summary>
        [Column("NOTE")]
        public string note { get; set; }
        /// <summary>
        /// element_type
        /// </summary>
        [Column("ELEMENT_TYPE")]
        public string element_type { get; set; }
        /// <summary>
        /// element_nature
        /// </summary>
        [Column("ELEMENT_NATURE")]
        public string element_nature { get; set; }
        /// <summary>
        /// safe_number
        /// </summary>
        [Column("SAFE_NUMBER")]
        public string safe_number { get; set; }
        /// <summary>
        /// storehouse_id
        /// </summary>
        [Column("STOREHOUSE_ID")]
        public string storehouse_id { get; set; }
        /// <summary>
        /// drawing_id
        /// </summary>
        [Column("DRAWING_ID")]
        public string drawing_id { get; set; }
        /// <summary>
        /// craft_id
        /// </summary>
        [Column("CRAFT_ID")]
        public string craft_id { get; set; }
        /// <summary>
        /// material_size
        /// </summary>
        [Column("MATERIAL_SIZE")]
        public string material_size { get; set; }
        /// <summary>
        /// material_weight
        /// </summary>
        [Column("MATERIAL_WEIGHT")]
        public string material_weight { get; set; }
        /// <summary>
        /// procedures1
        /// </summary>
        [Column("PROCEDURES1")]
        public string procedures1 { get; set; }
        /// <summary>
        /// procedures2
        /// </summary>
        [Column("PROCEDURES2")]
        public string procedures2 { get; set; }
        /// <summary>
        /// procedures3
        /// </summary>
        [Column("PROCEDURES3")]
        public string procedures3 { get; set; }
        /// <summary>
        /// procedures4
        /// </summary>
        [Column("PROCEDURES4")]
        public string procedures4 { get; set; }
        /// <summary>
        /// procedures5
        /// </summary>
        [Column("PROCEDURES5")]
        public string procedures5 { get; set; }
        /// <summary>
        /// procedures6
        /// </summary>
        [Column("PROCEDURES6")]
        public string procedures6 { get; set; }
        /// <summary>
        /// procedures7
        /// </summary>
        [Column("PROCEDURES7")]
        public string procedures7 { get; set; }
        /// <summary>
        /// procedures8
        /// </summary>
        [Column("PROCEDURES8")]
        public string procedures8 { get; set; }
        /// <summary>
        /// procedures9
        /// </summary>
        [Column("PROCEDURES9")]
        public string procedures9 { get; set; }
        /// <summary>
        /// procedures10
        /// </summary>
        [Column("PROCEDURES10")]
        public string procedures10 { get; set; }
        /// <summary>
        /// quota1
        /// </summary>
        [Column("QUOTA1")]
        public string quota1 { get; set; }
        /// <summary>
        /// quota2
        /// </summary>
        [Column("QUOTA2")]
        public string quota2 { get; set; }
        /// <summary>
        /// quota3
        /// </summary>
        [Column("QUOTA3")]
        public string quota3 { get; set; }
        /// <summary>
        /// quota4
        /// </summary>
        [Column("QUOTA4")]
        public string quota4 { get; set; }
        /// <summary>
        /// quota5
        /// </summary>
        [Column("QUOTA5")]
        public string quota5 { get; set; }
        /// <summary>
        /// quota6
        /// </summary>
        [Column("QUOTA6")]
        public string quota6 { get; set; }
        /// <summary>
        /// quota7
        /// </summary>
        [Column("QUOTA7")]
        public string quota7 { get; set; }
        /// <summary>
        /// quota8
        /// </summary>
        [Column("QUOTA8")]
        public string quota8 { get; set; }
        /// <summary>
        /// quota9
        /// </summary>
        [Column("QUOTA9")]
        public string quota9 { get; set; }
        /// <summary>
        /// quota10
        /// </summary>
        [Column("QUOTA10")]
        public string quota10 { get; set; }
        /// <summary>
        /// out_cost1
        /// </summary>
        [Column("OUT_COST1")]
        public string out_cost1 { get; set; }
        /// <summary>
        /// out_cost2
        /// </summary>
        [Column("OUT_COST2")]
        public string out_cost2 { get; set; }
        /// <summary>
        /// out_cost3
        /// </summary>
        [Column("OUT_COST3")]
        public string out_cost3 { get; set; }
        /// <summary>
        /// out_cost4
        /// </summary>
        [Column("OUT_COST4")]
        public string out_cost4 { get; set; }
        /// <summary>
        /// out_cost5
        /// </summary>
        [Column("OUT_COST5")]
        public string out_cost5 { get; set; }
        /// <summary>
        /// out_cost6
        /// </summary>
        [Column("OUT_COST6")]
        public string out_cost6 { get; set; }
        /// <summary>
        /// out_cost7
        /// </summary>
        [Column("OUT_COST7")]
        public string out_cost7 { get; set; }
        /// <summary>
        /// out_cost8
        /// </summary>
        [Column("OUT_COST8")]
        public string out_cost8 { get; set; }
        /// <summary>
        /// out_cost9
        /// </summary>
        [Column("OUT_COST9")]
        public string out_cost9 { get; set; }
        /// <summary>
        /// out_cost10
        /// </summary>
        [Column("OUT_COST10")]
        public string out_cost10 { get; set; }
        /// <summary>
        /// outbuy_cost
        /// </summary>
        [Column("OUTBUY_COST")]
        public string outbuy_cost { get; set; }
        /// <summary>
        /// state
        /// </summary>
        [Column("STATE")]
        public string state { get; set; }
        /// <summary>
        /// change_before
        /// </summary>
        [Column("CHANGE_BEFORE")]
        public string change_before { get; set; }
        /// <summary>
        /// change_after
        /// </summary>
        [Column("CHANGE_AFTER")]
        public string change_after { get; set; }
        /// <summary>
        /// oprate
        /// </summary>
        [Column("OPRATE")]
        public string oprate { get; set; }
        /// <summary>
        /// store_number
        /// </summary>
        [Column("STORE_NUMBER")]
        public string store_number { get; set; }
        /// <summary>
        /// diff_number
        /// </summary>
        [Column("DIFF_NUMBER")]
        public string diff_number { get; set; }
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

