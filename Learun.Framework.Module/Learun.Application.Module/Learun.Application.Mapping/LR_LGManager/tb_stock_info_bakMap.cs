using Learun.Application.TwoDevelopment.LR_LGManager;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-11-19 17:31
    /// 描 述：标准化BOM备份
    /// </summary>
    public class tb_stock_info_bakMap : EntityTypeConfiguration<tb_stock_info_bakEntity>
    {
        public tb_stock_info_bakMap()
        {
            #region  表、主键
            //表
            this.ToTable("TB_STOCK_INFO_BAK");
            //主键
            this.HasKey(t => t.id);
            #endregion

            #region  配置关系
            #endregion
        }
    }
}

