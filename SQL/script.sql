USE [scada_data]
GO
/****** Object:  Table [dbo].[devices]    Script Date: 2020/12/12 9:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[devices](
	[d_id] [nvarchar](50) NOT NULL,
	[d_name] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[monitor_values]    Script Date: 2020/12/12 9:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[monitor_values](
	[d_id] [nvarchar](50) NOT NULL,
	[value_id] [nvarchar](50) NULL,
	[value_name] [nvarchar](50) NULL,
	[s_area_id] [nvarchar](10) NULL,
	[address] [int] NULL,
	[absolute_add] [nvarchar](50) NULL,
	[data_type] [nvarchar](50) NULL,
	[is_alarm] [int] NULL,
	[description] [nvarchar](200) NULL,
	[unit] [nvarchar](50) NULL,
	[alarm_lolo] [nvarchar](50) NULL,
	[alarm_low] [nvarchar](50) NULL,
	[alarm_high] [nvarchar](50) NULL,
	[alarm_hihi] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[storage_area]    Script Date: 2020/12/12 9:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[storage_area](
	[id] [nvarchar](10) NULL,
	[slave_add] [int] NOT NULL,
	[func_code] [nvarchar](10) NOT NULL,
	[s_area_name] [nvarchar](50) NULL,
	[start_reg] [nvarchar](50) NULL,
	[length] [nvarchar](50) NULL
) ON [PRIMARY]
GO
