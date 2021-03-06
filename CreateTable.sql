USE [StudentManagement]
GO
/****** Object:  Table [dbo].[User]    Script Date: 05/02/2022 10:39:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[FullName] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[isAdmin] [nchar](10) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SinhVien]    Script Date: 05/02/2022 10:39:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SinhVien](
	[MaSinhVien] [int] IDENTITY(1,1) NOT NULL,
	[TenSinhVien] [nvarchar](50) NULL,
	[GioiTinh] [nvarchar](10) NULL,
	[NgaySinh] [nvarchar](50) NULL,
	[QueQuan] [nvarchar](50) NULL,
	[SDT] [nvarchar](50) NULL,
	[MaLop] [nvarchar](10) NULL,
	[HinhAnh] [nvarchar](500) NULL,
	[TinhTrang] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MonHoc]    Script Date: 05/02/2022 10:39:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonHoc](
	[MaMonHoc] [nvarchar](10) NOT NULL,
	[TenMonHoc] [nvarchar](50) NOT NULL,
	[SoTinChi] [nvarchar](10) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lop]    Script Date: 05/02/2022 10:39:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lop](
	[MaLop] [nvarchar](10) NOT NULL,
	[TenLop] [nvarchar](50) NOT NULL,
	[MaKhoa] [nvarchar](10) NOT NULL,
	[MaHeDT] [nvarchar](10) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhoaHoc]    Script Date: 05/02/2022 10:39:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhoaHoc](
	[MaKhoaHoc] [nvarchar](10) NOT NULL,
	[TenKhoaHoc] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Khoa]    Script Date: 05/02/2022 10:39:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Khoa](
	[MaKhoa] [nvarchar](10) NOT NULL,
	[TenKhoa] [nvarchar](50) NOT NULL,
	[DiaChi] [nvarchar](250) NOT NULL,
	[DienThoai] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HeDaoTao]    Script Date: 05/02/2022 10:39:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HeDaoTao](
	[MaHeDT] [nvarchar](10) NOT NULL,
	[TenHeDT] [nvarchar](50) NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diem](
	[MaSinhVien] [int] NOT NULL,
	[MaMonHoc] [nvarchar](10) NOT NULL,
	[DiemLan1] [float] NULL,
	[DiemLan2] [float] NULL,
	[DiemTB] [nvarchar](10) NULL
) ON [PRIMARY]
GO
