﻿using Learun.Application.TwoDevelopment.LR_Desktop;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 版 本 Learun-ADMS-Ultimate V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员
    /// 日 期：2018-09-25 11:32
    /// 描 述：图标配置
    /// </summary>
    public class DTChartMap1 : EntityTypeConfiguration<DTChartEntity1>
    {
        public DTChartMap1()
        {
            #region  表、主键
            //表
            this.ToTable("LR_DT_CHART1");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region  配置关系
            #endregion
        }
    }
}

