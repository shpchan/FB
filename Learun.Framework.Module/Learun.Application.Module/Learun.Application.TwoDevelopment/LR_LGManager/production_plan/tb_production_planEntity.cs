using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_LGManager
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-09-18 10:52
    /// 描 述：Production_plan
    /// </summary>
    public class tb_production_planEntity
    {
        #region  实体成员
        /// <summary>
        /// plan_name
        /// </summary>
        [Column("PLAN_NAME")]
        public string plan_name { get; set; }
        /// <summary>
        /// product_name
        /// </summary>
        [Column("PRODUCT_NAME")]
        public string product_name { get; set; }
        /// <summary>
        /// plan_amount
        /// </summary>
        [Column("PLAN_AMOUNT")]
        public string plan_amount { get; set; }
        /// <summary>
        /// state
        /// </summary>
        [Column("STATE")]
        public string state { get; set; }
        /// <summary>
        /// machine_id
        /// </summary>
        [Column("MACHINE_ID")]
        public int? machine_id { get; set; }
        /// <summary>
        /// start_time
        /// </summary>
        [Column("START_TIME")]
        public DateTime? start_time { get; set; }
        /// <summary>
        /// end_time
        /// </summary>
        [Column("END_TIME")]
        public DateTime? end_time { get; set; }
        /// <summary>
        /// id
        /// </summary>
        [Column("ID")]
        public string id { get; set; }

        /// <summary>
        /// create_time
        /// </summary>
        [Column("CREATE_TIME")]
        public DateTime? create_time { get; set; }
        /// <summary>
        /// create_user
        /// </summary>
        [Column("CREATE_USER")]
        public string create_user { get; set; }

        [Column("OPERATION_USER")]
        public string operation_user { get; set; }

        [Column("PROD_NUM")]
        public int prod_num { get; set; }
        [Column("SORT")]
        public int sort { get; set; }
        [Column("AUTO")]
        public int auto { get; set; }
        #endregion

        #region  扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.id = Guid.NewGuid().ToString();
            this.create_time = DateTime.Now;
            this.start_time = DateTime.Now;
            this.prod_num = 0;
            this.auto = 0;
            this.create_user = LoginUserInfo.Get().realName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.id = keyValue;
            this.create_time = DateTime.Now;
            this.operation_user = LoginUserInfo.Get().realName;

        }

        #endregion
        #region  扩展字段
        #endregion
    }
}

