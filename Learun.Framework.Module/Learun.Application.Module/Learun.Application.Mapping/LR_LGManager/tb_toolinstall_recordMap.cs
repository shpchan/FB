﻿using Learun.Application.TwoDevelopment.LR_LGManager;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2019-09-17 14:56
    /// 描 述：装/卸刀记录
    /// </summary>
    public class tb_toolinstall_recordMap : EntityTypeConfiguration<tb_toolinstall_recordEntity>
    {
        public tb_toolinstall_recordMap()
        {
            #region  表、主键
            //表
            this.ToTable("TB_TOOLINSTALL_RECORD");
            //主键
            this.HasKey(t => t.id);
            #endregion

            #region  配置关系
            #endregion
        }
    }
}

