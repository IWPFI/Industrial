USE [master]
GO
/****** Object:  Database [scada_data]    Script Date: 2022/4/15 13:57:46 ******/
CREATE DATABASE [scada_data]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'scada_data', FILENAME = N'D:\-SQL2019-\[0]\MSSQL14.MSSQLSERVER\MSSQL\DATA\scada_data.mdf' , SIZE = 8192KB , MAXSIZE = 102400KB , FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'scada_data_log', FILENAME = N'D:\-SQL2019-\[0]\MSSQL14.MSSQLSERVER\MSSQL\DATA\scada_data_log.ldf' , SIZE = 8192KB , MAXSIZE = 102400KB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [scada_data] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [scada_data].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [scada_data] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [scada_data] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [scada_data] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [scada_data] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [scada_data] SET ARITHABORT OFF 
GO
ALTER DATABASE [scada_data] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [scada_data] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [scada_data] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [scada_data] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [scada_data] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [scada_data] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [scada_data] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [scada_data] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [scada_data] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [scada_data] SET  DISABLE_BROKER 
GO
ALTER DATABASE [scada_data] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [scada_data] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [scada_data] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [scada_data] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [scada_data] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [scada_data] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [scada_data] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [scada_data] SET RECOVERY FULL 
GO
ALTER DATABASE [scada_data] SET  MULTI_USER 
GO
ALTER DATABASE [scada_data] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [scada_data] SET DB_CHAINING OFF 
GO
ALTER DATABASE [scada_data] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [scada_data] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [scada_data] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [scada_data] SET QUERY_STORE = OFF
GO
USE [scada_data]
GO
/****** Object:  Table [dbo].[devices]    Script Date: 2022/4/15 13:57:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[devices](
	[d_id] [nvarchar](50) NOT NULL,
	[d_name] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[monitor_values]    Script Date: 2022/4/15 13:57:46 ******/
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
/****** Object:  Table [dbo].[storage_area]    Script Date: 2022/4/15 13:57:46 ******/
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
INSERT [dbo].[devices] ([d_id], [d_name]) VALUES (N'CT001', N'冷却塔 1#')
INSERT [dbo].[devices] ([d_id], [d_name]) VALUES (N'CT002', N'冷却塔 2#')
INSERT [dbo].[devices] ([d_id], [d_name]) VALUES (N'CT003', N'冷却塔 3#')
GO
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT001', N'CT001001', N'液位', N'1030', 0, N'40000', N'Float', 1, N'冷却塔1#液位', N'm', N'20', N'30', N'70', N'80')
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT002', N'CT002004', N'出口压力', N'1030', 18, N'40018', N'Float', 0, N'冷却塔2#出口压力', N'Mpa', NULL, NULL, NULL, NULL)
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT002', N'CT002005', N'出口温度', N'1030', 20, N'40020', N'Float', 0, N'冷却塔2#出口温度', N'℃', NULL, NULL, NULL, NULL)
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT002', N'CT002006', N'补水压力', N'1030', 22, N'40022', N'Float', 0, N'冷却塔2#补水压力', N'Mpa', NULL, NULL, NULL, NULL)
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT003', N'CT003001', N'液位', N'1030', 24, N'40024', N'Float', 1, N'冷却塔3#液位', N'm', N'20', N'30', N'70', N'80')
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT001', N'CT001002', N'入口压力', N'1030', 2, N'40002', N'Float', 1, N'冷却塔1#入口压力', N'Mpa', N'23', N'30', N'70', N'80')
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT001', N'CT001003', N'入口温度', N'1030', 4, N'40004', N'Float', 1, N'冷却塔1#入口温度', N'℃', N'12', N'15', N'90', N'100')
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT001', N'CT001004', N'出口压力', N'1030', 6, N'40006', N'Float', 0, N'冷却塔1#出口压力', N'Mpa', NULL, NULL, NULL, NULL)
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT001', N'CT001005', N'出口温度', N'1030', 8, N'40008', N'Float', 0, N'冷却塔1#出口温度', N'℃', NULL, NULL, NULL, NULL)
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT001', N'CT001006', N'补水压力', N'1030', 10, N'40010', N'Float', 0, N'冷却塔1#补水压力', N'Mpa', NULL, NULL, NULL, NULL)
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT002', N'CT002001', N'液位', N'1030', 12, N'40012', N'Float', 1, N'冷却塔2#液位', N'm', N'20', N'30', N'70', N'80')
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT002', N'CT002002', N'入口压力', N'1030', 14, N'40014', N'Float', 1, N'冷却塔2#入口压力', N'Mpa', N'23', N'30', N'60', N'70')
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT002', N'CT002003', N'入口温度', N'1030', 16, N'40016', N'Float', 1, N'冷却塔2#入口温度', N'℃', N'12', N'15', N'90', N'100')
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT003', N'CT003002', N'入口压力', N'1030', 26, N'40026', N'Float', 1, N'冷却塔3#入口压力', N'Mpa', N'23', N'30', N'60', N'70')
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT003', N'CT003003', N'入口温度', N'1030', 28, N'40028', N'Float', 1, N'冷却塔3#入口温度', N'℃', N'12', N'15', N'90', N'100')
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT003', N'CT003004', N'出口压力', N'1030', 30, N'40030', N'Float', 0, N'冷却塔3#出口压力', N'Mpa', NULL, NULL, NULL, NULL)
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT003', N'CT003005', N'出口温度', N'1030', 32, N'40032', N'Float', 0, N'冷却塔3#出口温度', N'℃', NULL, NULL, NULL, NULL)
INSERT [dbo].[monitor_values] ([d_id], [value_id], [value_name], [s_area_id], [address], [absolute_add], [data_type], [is_alarm], [description], [unit], [alarm_lolo], [alarm_low], [alarm_high], [alarm_hihi]) VALUES (N'CT003', N'CT003006', N'补水压力', N'1030', 34, N'40034', N'Float', 0, N'冷却塔3#补水压力', N'Mpa', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[storage_area] ([id], [slave_add], [func_code], [s_area_name], [start_reg], [length]) VALUES (N'1030', 1, N'03', N'03 Holding Register(4x)', N'0', N'36')
GO
USE [master]
GO
ALTER DATABASE [scada_data] SET  READ_WRITE 
GO
