USE [master]
GO
/****** Object:  Database [dbdemo]    Script Date: 2/21/2025 4:51:25 PM ******/
CREATE DATABASE [dbdemo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbdemo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\dbdemo.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbdemo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\dbdemo_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [dbdemo] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbdemo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbdemo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbdemo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbdemo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbdemo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbdemo] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbdemo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbdemo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbdemo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbdemo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbdemo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbdemo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbdemo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbdemo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbdemo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbdemo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbdemo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbdemo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbdemo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbdemo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbdemo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbdemo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbdemo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbdemo] SET RECOVERY FULL 
GO
ALTER DATABASE [dbdemo] SET  MULTI_USER 
GO
ALTER DATABASE [dbdemo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbdemo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbdemo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbdemo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbdemo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbdemo] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'dbdemo', N'ON'
GO
ALTER DATABASE [dbdemo] SET QUERY_STORE = ON
GO
ALTER DATABASE [dbdemo] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [dbdemo]
GO
/****** Object:  Table [dbo].[(old)Account]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[(old)Account](
	[Email] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](50) NULL,
	[PhoneNumbers] [int] NULL,
	[Gender] [nvarchar](10) NULL,
 CONSTRAINT [PK_Account_1] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[(old)ActivityLog2]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[(old)ActivityLog2](
	[No] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](20) NOT NULL,
	[Action] [nvarchar](50) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ActivityLog] PRIMARY KEY CLUSTERED 
(
	[No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[(old)CustomerInvoice]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[(old)CustomerInvoice](
	[No] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID]  AS ('C'+right('00000000'+CONVERT([varchar](5),[No]),(5))) PERSISTED NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Gender] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Company] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Region] [nvarchar](50) NOT NULL,
	[PostalCode] [nchar](20) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Phone] [nchar](12) NOT NULL,
	[Fax] [nchar](12) NULL,
	[PaymentMethod] [nvarchar](50) NOT NULL,
	[Bill] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime2](7) NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CustomerInvoice] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[(old)ProductList]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[(old)ProductList](
	[ID] [uniqueidentifier] NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[Catagory] [int] NOT NULL,
	[Quality] [int] NOT NULL,
	[ImportPrice] [float] NOT NULL,
	[ExportPrice] [float] NOT NULL,
	[DayCreated] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_ProductList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountLinked]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountLinked](
	[AccountID] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Apple] [nvarchar](50) NULL,
	[Facebook] [nvarchar](50) NULL,
	[Twitter] [nvarchar](50) NULL,
	[Github] [nvarchar](50) NULL,
	[NotifyCount] [decimal](18, 0) NULL,
 CONSTRAINT [PK_AccountLinked] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountNotify]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountNotify](
	[No] [int] IDENTITY(1,1) NOT NULL,
	[NotifyID]  AS ('Ntf'+right('00000000'+CONVERT([varchar](5),[No]),(5))) PERSISTED NOT NULL,
	[AccountID] [nvarchar](20) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](20) NOT NULL,
	[Display] [nvarchar](50) NOT NULL,
	[Details] [nvarchar](100) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
	[RequestType] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[TimeCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_AccountNotify] PRIMARY KEY CLUSTERED 
(
	[NotifyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[NotifyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountTest]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountTest](
	[No] [int] IDENTITY(1,1) NOT NULL,
	[AccountID]  AS ('Acc'+right('00000000'+CONVERT([varchar](5),[No]),(5))) PERSISTED NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](10) NULL,
	[PhoneNumbers] [nchar](12) NULL,
	[Gender] [nvarchar](10) NULL,
	[CreatedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_AccountTest] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityLog]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityLog](
	[No] [int] IDENTITY(1,1) NOT NULL,
	[ActivityID]  AS ('Act'+right('00000000'+CONVERT([varchar](5),[No]),(5))) PERSISTED NOT NULL,
	[AccountID] [nvarchar](20) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](20) NOT NULL,
	[Action] [nvarchar](50) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
(
	[ActivityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ActivityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[No] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID]  AS ('C'+right('00000000'+CONVERT([varchar](5),[No]),(5))) PERSISTED NOT NULL,
	[AccountID] [nvarchar](20) NOT NULL,
	[Username] [nvarchar](20) NOT NULL,
	[Role] [nvarchar](20) NOT NULL,
	[InputName] [nvarchar](50) NOT NULL,
	[Gender] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Company] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Region] [nvarchar](50) NOT NULL,
	[PostalCode] [nchar](20) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Phone] [nchar](12) NOT NULL,
	[Fax] [nchar](12) NULL,
	[PaymentMethod] [nvarchar](50) NOT NULL,
	[Bill] [float] NOT NULL,
	[CouponCode] [nvarchar](20) NOT NULL,
	[PaymentStatus] [nvarchar](20) NOT NULL,
	[CreatedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerList]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerList](
	[No] [int] IDENTITY(1,1) NOT NULL,
	[OrderID]  AS ('Order'+right('00000000'+CONVERT([varchar](5),[No]),(5))) PERSISTED NOT NULL,
	[AccountID] [nvarchar](20) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Product] [nvarchar](40) NOT NULL,
	[ProductCode] [nvarchar](40) NOT NULL,
	[Price] [float] NOT NULL,
	[Amount] [float] NOT NULL,
	[CreatedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_CustomerList] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerListFinal]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerListFinal](
	[OrderID] [nvarchar](20) NOT NULL,
	[AccountID] [nvarchar](20) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[InputName] [nvarchar](50) NULL,
	[Product] [nvarchar](40) NOT NULL,
	[ProductCode] [nvarchar](40) NOT NULL,
	[Price] [float] NOT NULL,
	[Amount] [float] NOT NULL,
	[CreatedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_CustomerListFinal] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerOrder]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerOrder](
	[No] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PaymentStatus] [nvarchar](50) NOT NULL,
	[OrderStatus] [nvarchar](50) NOT NULL,
	[DeliveryPartner] [nvarchar](50) NULL,
	[DeliveryMethod] [nvarchar](50) NULL,
	[Vehicle] [nvarchar](50) NULL,
 CONSTRAINT [PK_CustomerOrder] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerPreOrder]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerPreOrder](
	[No] [int] IDENTITY(1,1) NOT NULL,
	[PreOrderID]  AS ('Pre'+right('00000000'+CONVERT([varchar](5),[No]),(5))) PERSISTED NOT NULL,
	[AccountID] [nvarchar](20) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Product] [nvarchar](40) NOT NULL,
	[ProductCode] [nvarchar](40) NOT NULL,
	[Price] [float] NOT NULL,
	[Amount] [float] NOT NULL,
	[CreatedDate] [datetime2](7) NULL,
	[Condition] [nvarchar](20) NULL,
 CONSTRAINT [PK_CustomerPreOrder] PRIMARY KEY CLUSTERED 
(
	[PreOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[PreOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Issues]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Issues](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Priority] [int] NOT NULL,
	[IssueType] [int] NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[Completed] [datetime2](7) NULL,
 CONSTRAINT [PK_Issues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductLists]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductLists](
	[ProductCode] [nvarchar](10) NOT NULL,
	[Product] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Type] [nvarchar](20) NOT NULL,
	[Price] [float] NOT NULL,
	[Amount] [float] NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[TimeCreated] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](50) NOT NULL,
	[TimeModified] [datetime2](7) NOT NULL,
	[SalePercent] [float] NULL,
 CONSTRAINT [PK_ProductListU] PRIMARY KEY CLUSTERED 
(
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Product] [nvarchar](40) NOT NULL,
	[ProductCode] [nvarchar](40) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [float] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductTemp]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductTemp](
	[ProductCode] [nvarchar](10) NOT NULL,
	[Product] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Type] [nvarchar](20) NOT NULL,
	[Price] [float] NOT NULL,
	[Amount] [float] NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[AccountID] [nvarchar](50) NOT NULL,
	[TimeCreated] [datetime2](7) NOT NULL,
	[SalePercent] [float] NULL,
 CONSTRAINT [PK_ProductTemp] PRIMARY KEY CLUSTERED 
(
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockList]    Script Date: 2/21/2025 4:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockList](
	[Product] [nvarchar](40) NOT NULL,
	[ProductCode] [nvarchar](40) NOT NULL,
	[Price] [nvarchar](40) NULL,
 CONSTRAINT [PK_StockList] PRIMARY KEY CLUSTERED 
(
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [dbdemo] SET  READ_WRITE 
GO
