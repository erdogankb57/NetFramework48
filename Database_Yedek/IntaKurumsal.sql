USE [master]
GO
/****** Object:  Database [IntaKurumsal]    Script Date: 25/10/2024 15:25:31 ******/
CREATE DATABASE [IntaKurumsal]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IntaKurumsal', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\IntaKurumsal.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'IntaKurumsal_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\IntaKurumsal_log.ldf' , SIZE = 139264KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [IntaKurumsal] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IntaKurumsal].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [IntaKurumsal] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [IntaKurumsal] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [IntaKurumsal] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [IntaKurumsal] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [IntaKurumsal] SET ARITHABORT OFF 
GO
ALTER DATABASE [IntaKurumsal] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [IntaKurumsal] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [IntaKurumsal] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [IntaKurumsal] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [IntaKurumsal] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [IntaKurumsal] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [IntaKurumsal] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [IntaKurumsal] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [IntaKurumsal] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [IntaKurumsal] SET  DISABLE_BROKER 
GO
ALTER DATABASE [IntaKurumsal] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [IntaKurumsal] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [IntaKurumsal] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [IntaKurumsal] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [IntaKurumsal] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [IntaKurumsal] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [IntaKurumsal] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [IntaKurumsal] SET RECOVERY FULL 
GO
ALTER DATABASE [IntaKurumsal] SET  MULTI_USER 
GO
ALTER DATABASE [IntaKurumsal] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [IntaKurumsal] SET DB_CHAINING OFF 
GO
ALTER DATABASE [IntaKurumsal] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [IntaKurumsal] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [IntaKurumsal] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [IntaKurumsal] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'IntaKurumsal', N'ON'
GO
ALTER DATABASE [IntaKurumsal] SET QUERY_STORE = OFF
GO
USE [IntaKurumsal]
GO
/****** Object:  Table [dbo].[Banner]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NULL,
	[LanguageId] [int] NULL,
	[BannerTypeId] [int] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Link] [nvarchar](50) NULL,
	[ShortExplanation] [nvarchar](max) NULL,
	[OrderNumber] [int] NOT NULL,
	[TargetId] [int] NULL,
	[Image] [varchar](max) NULL,
	[RecordDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BannerType]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BannerType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NULL,
	[LanguageId] [int] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[SmallImageWidth] [int] NULL,
	[BigImageWidth] [int] NULL,
	[SmallImageHeight] [int] NULL,
	[BigImageHeight] [int] NULL,
	[RecordDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_BannerType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NULL,
	[LanguageId] [int] NULL,
	[PageTypeId] [int] NULL,
	[CategoryId] [int] NULL,
	[FormGroupId] [int] NULL,
	[Code] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[CategoryUrl] [nvarchar](255) NULL,
	[Title] [nvarchar](max) NULL,
	[MetaDecription] [nvarchar](max) NULL,
	[MetaKeywords] [nvarchar](max) NULL,
	[ShortExplanation] [nvarchar](max) NULL,
	[CanBeDeleted] [bit] NULL,
	[CanSubCategoryBeAdded] [bit] NULL,
	[Image] [nvarchar](max) NULL,
	[ImageTag] [nvarchar](max) NULL,
	[ImageTitle] [nvarchar](max) NULL,
	[Explanation] [nvarchar](max) NULL,
	[OrderNumber] [int] NULL,
	[RecordDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[CategoryLink] [nvarchar](500) NULL,
	[CanContentBeAdded] [bit] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategorySpecialty]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategorySpecialty](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NULL,
	[LanguageId] [int] NULL,
	[CategoryId] [int] NULL,
	[SpecialtyTypeId] [int] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ShortExplanation] [nvarchar](50) NULL,
	[Explanation] [nvarchar](max) NULL,
	[FileName] [nvarchar](max) NULL,
	[FileTagName] [nvarchar](max) NULL,
	[FileTitleName] [nvarchar](max) NULL,
	[TargetType] [nvarchar](50) NULL,
	[HomePageStatus] [bit] NULL,
	[OrderNumber] [int] NULL,
	[RecordDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_CategorySpecialty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactInformation]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NULL,
	[LanguageId] [int] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Gsm] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Adress] [nvarchar](50) NULL,
	[Explanation] [nvarchar](max) NULL,
	[GoogleMapFrame] [nvarchar](max) NULL,
	[GoogleMapLink] [nvarchar](max) NULL,
	[GoogleMapX] [nvarchar](50) NULL,
	[GoogleMapY] [nvarchar](50) NULL,
	[Image] [nvarchar](100) NULL,
	[QrCode] [nvarchar](50) NULL,
	[FormSendEmail] [nvarchar](50) NULL,
	[OrderNumber] [int] NOT NULL,
	[RecordDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ContactInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EditorImages]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EditorImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](3000) NULL,
 CONSTRAINT [PK_EditorImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EditorTemplate]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EditorTemplate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NULL,
	[LanguageId] [int] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[HtmlContent] [nvarchar](max) NULL,
	[Image] [nvarchar](50) NULL,
	[OrderNumber] [int] NOT NULL,
	[RecordDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_EditorTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FirmVariables]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FirmVariables](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
	[OrderNumber] [int] NOT NULL,
	[RecordDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_FirmVariables] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormElement]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormElement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NULL,
	[SystemUserId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[FormGroupId] [int] NULL,
	[FormTypeId] [int] NULL,
	[ElementTypeId] [int] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsRequired] [bit] NULL,
	[ValidationMessage] [nvarchar](max) NULL,
	[AllowNulls] [bit] NULL,
	[OrderNumber] [int] NOT NULL,
	[RecordDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_FormElement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormElementOptions]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormElementOptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NULL,
	[LanguageId] [int] NULL,
	[FormElementId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Value] [nvarchar](50) NULL,
	[IsSelected] [bit] NULL,
	[OrderNumber] [int] NOT NULL,
	[RecordDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_FormOptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormGroup]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NULL,
	[LanguageId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Explanation] [nvarchar](500) NULL,
	[OrderNumber] [int] NULL,
	[RecordDate] [date] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_FormGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormType]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[CategoryId] [int] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[OrderNumber] [int] NOT NULL,
	[RecordDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_FormType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GeneralSettings]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneralSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LanguageId] [int] NULL,
	[SystemUserId] [int] NULL,
	[EmailIpAdress] [nvarchar](100) NULL,
	[EmailAdress] [nvarchar](100) NULL,
	[EmailPort] [int] NULL,
	[EmailPassword] [nvarchar](50) NULL,
	[DomainName] [nvarchar](50) NULL,
	[ImageCdnUrl] [nvarchar](3000) NULL,
	[FileCdnUrl] [nvarchar](3000) NULL,
	[ImageUploadPath] [nvarchar](3000) NULL,
	[FileUploadPath] [nvarchar](3000) NULL,
	[DeveloperName] [nvarchar](100) NULL,
	[DeveloperEmail] [nvarchar](100) NULL,
	[CategoryImageSmallWidth] [int] NULL,
	[CategoryImageSmallHeight] [int] NULL,
	[CategoryImageBigWidth] [int] NULL,
	[CategoryImageBigHeight] [int] NULL,
	[ContentImageSmallWidth] [int] NULL,
	[ContentImageSmallHeight] [int] NULL,
	[ContentImageBigWidth] [int] NULL,
	[ContentImageBigHeight] [int] NULL,
	[GalleryImageSmallWidth] [int] NULL,
	[GalleryImageSmallHeight] [int] NULL,
	[GalleryImageBigWidth] [int] NULL,
	[GalleryImageBigHeight] [int] NULL,
	[EditorImageUploadCdn] [nvarchar](3000) NULL,
	[EditorImageUploadPath] [nvarchar](3000) NULL,
	[Logo] [nvarchar](3000) NULL,
 CONSTRAINT [PK_GeneralSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Language]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[OrderNumber] [int] NOT NULL,
	[RecordDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogMessage]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogMessage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](50) NULL,
	[Source] [nvarchar](max) NULL,
	[ErrorMessage] [nvarchar](max) NULL,
	[InnerException] [nvarchar](max) NULL,
	[StackTrace] [nvarchar](max) NULL,
	[ObjectSource] [nvarchar](max) NULL,
	[RecordDate] [datetime] NULL,
 CONSTRAINT [PK_LogMessage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageHistory]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LanguageId] [int] NULL,
	[MessageTypeId] [int] NULL,
	[ClientName] [nvarchar](50) NULL,
	[ClientSurname] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Subject] [nvarchar](500) NULL,
	[Explanation] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[IsRead] [bit] NULL,
	[ArchiveDate] [datetime] NULL,
	[IpNumber] [nvarchar](50) NULL,
	[RecordDate] [datetime] NULL,
 CONSTRAINT [PK_MessageHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageType]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NULL,
	[LanguageCode] [nvarchar](10) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[RecordDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_MessageType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PageType]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NULL,
	[Code] [nvarchar](10) NULL,
	[Name] [nvarchar](500) NULL,
	[ControllerName] [nvarchar](500) NULL,
	[ActionName] [nvarchar](500) NULL,
	[ViewName] [nvarchar](500) NULL,
	[IsExplanationEnabled] [bit] NULL,
	[IsShortExplanationEnabled] [bit] NULL,
	[IsMenuFirstRecord] [bit] NULL,
	[IsMenuFirstCategory] [bit] NULL,
	[IsPagingEnabled] [bit] NULL,
	[RecordDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_PageType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Record]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Record](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NULL,
	[LanguageId] [int] NULL,
	[BannerTypeId] [int] NULL,
	[TargetId] [int] NULL,
	[CategoryId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[RecordUrl] [nvarchar](255) NULL,
	[Title] [nvarchar](50) NULL,
	[MetaDescription] [nvarchar](max) NULL,
	[MetaKeywords] [nvarchar](max) NULL,
	[Url] [nvarchar](max) NULL,
	[ShortContent] [nvarchar](max) NULL,
	[Link] [nvarchar](max) NULL,
	[TargetType] [int] NULL,
	[ShortExplanation] [nvarchar](max) NULL,
	[Explanation] [nvarchar](max) NULL,
	[Image] [varchar](255) NULL,
	[OrderNumber] [int] NULL,
	[RecordDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecordFile]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecordFile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NULL,
	[LanguageId] [int] NULL,
	[RecordId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[ShortExplanation] [nvarchar](max) NULL,
	[Explanation] [nvarchar](max) NULL,
	[FileName] [varchar](255) NULL,
	[FileTagName] [nvarchar](max) NULL,
	[FileTitleName] [nvarchar](max) NULL,
	[TargetId] [int] NULL,
	[HomePageStatus] [bit] NULL,
	[OrderNumber] [int] NULL,
	[RecordDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_ContentFile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecordImage]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecordImage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NULL,
	[LanguageId] [int] NULL,
	[RecordId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[ShortExplanation] [nvarchar](max) NULL,
	[Explanation] [nvarchar](max) NULL,
	[ImageName] [nvarchar](max) NULL,
	[ImageTagName] [nvarchar](max) NULL,
	[ImageTitleName] [nvarchar](max) NULL,
	[TargetId] [int] NULL,
	[HomePageStatus] [bit] NULL,
	[OrderNumber] [int] NULL,
	[RecordDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_ContentImage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecordSpecialty]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecordSpecialty](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NULL,
	[LanguageId] [int] NULL,
	[CategoryId] [int] NULL,
	[SpecialtyTypeId] [int] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ShortExplanation] [nvarchar](max) NULL,
	[Explanation] [nvarchar](max) NULL,
	[FileName] [nvarchar](max) NULL,
	[FileTagName] [nvarchar](max) NULL,
	[FileTitleName] [nvarchar](max) NULL,
	[TargetType] [nvarchar](50) NULL,
	[HomePageStatus] [bit] NULL,
	[OrderNumber] [int] NOT NULL,
	[RecordDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ContentSpecialty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SEOIndex]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SEOIndex](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NULL,
	[LanguageId] [int] NULL,
	[Name] [nvarchar](4000) NULL,
	[Url] [nvarchar](max) NULL,
	[RedirectUrl] [nvarchar](max) NULL,
	[RecordDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SEOIndex] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpecialtyType]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpecialtyType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SpecialtyType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StaticText]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StaticText](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Explanation] [nvarchar](max) NULL,
	[OrderNumber] [int] NULL,
	[RecordDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_StaticText] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemAction]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemAction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemMenuId] [int] NULL,
	[ControllerName] [nvarchar](255) NULL,
	[ActionName] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [PK_SystemAction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemActionRole]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemActionRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemActionId] [int] NULL,
	[SystemRoleId] [int] NULL,
 CONSTRAINT [PK_SystemActionRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemMenu]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemMenu](
	[Id] [int] NOT NULL,
	[SystemMenuId] [int] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Explanation] [nvarchar](max) NULL,
	[BreadCrumpName] [nvarchar](50) NULL,
	[BreadCrumpUrl] [nvarchar](50) NULL,
	[ControllerName] [nvarchar](50) NULL,
	[ActionName] [nvarchar](50) NULL,
	[RecordDate] [datetime] NULL,
	[MenuIcon] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[ShowInMenu] [bit] NULL,
	[OrderNumber] [int] NULL,
 CONSTRAINT [PK_SystemMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemMenuRole]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemMenuRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemRoleId] [int] NULL,
	[SystemMenuId] [int] NULL,
	[RecordDate] [datetime] NULL,
 CONSTRAINT [PK_SystemMenuRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemRole]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Explanation] [nvarchar](max) NULL,
	[RecordDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_SystemRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemUser]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NULL,
	[SystemRoleId] [int] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[SurName] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Image] [nvarchar](500) NULL,
	[RecordDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsAdmin] [bit] NULL,
 CONSTRAINT [PK_SystemUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemUserRole]    Script Date: 25/10/2024 15:25:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemUserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemRoleId] [int] NULL,
	[SystemUserId] [int] NULL,
	[RecordDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_SystemUserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Banner] ON 

INSERT [dbo].[Banner] ([Id], [SystemUserId], [LanguageId], [BannerTypeId], [Name], [Link], [ShortExplanation], [OrderNumber], [TargetId], [Image], [RecordDate], [IsActive]) VALUES (75900, 5, 5, 76864, N'dfdfdf', NULL, NULL, 0, 0, N'1-6a6609d1-0a87-4be1-9d1a-10a4ac47a5f9.jpg', CAST(N'2024-07-20T20:57:23.460' AS DateTime), 1)
INSERT [dbo].[Banner] ([Id], [SystemUserId], [LanguageId], [BannerTypeId], [Name], [Link], [ShortExplanation], [OrderNumber], [TargetId], [Image], [RecordDate], [IsActive]) VALUES (75902, 5, 4, 76864, N'banner - 1', NULL, NULL, 0, 0, N'channels4-profile-6949669c-3a22-4b80-8eb1-670a2a465203.jpg', CAST(N'2024-10-25T14:46:56.270' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Banner] OFF
GO
SET IDENTITY_INSERT [dbo].[BannerType] ON 

INSERT [dbo].[BannerType] ([Id], [SystemUserId], [LanguageId], [Name], [Description], [SmallImageWidth], [BigImageWidth], [SmallImageHeight], [BigImageHeight], [RecordDate], [IsActive]) VALUES (76864, 5, 4, N'Ana sayfa banner', NULL, 200, 800, 200, 800, NULL, 1)
INSERT [dbo].[BannerType] ([Id], [SystemUserId], [LanguageId], [Name], [Description], [SmallImageWidth], [BigImageWidth], [SmallImageHeight], [BigImageHeight], [RecordDate], [IsActive]) VALUES (76865, 5, 4, N'Ana sayfa sol banner', NULL, 200, 800, 200, 800, NULL, 1)
SET IDENTITY_INSERT [dbo].[BannerType] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [SystemUserId], [LanguageId], [PageTypeId], [CategoryId], [FormGroupId], [Code], [Name], [CategoryUrl], [Title], [MetaDecription], [MetaKeywords], [ShortExplanation], [CanBeDeleted], [CanSubCategoryBeAdded], [Image], [ImageTag], [ImageTitle], [Explanation], [OrderNumber], [RecordDate], [IsActive], [CategoryLink], [CanContentBeAdded]) VALUES (71683, 5, 4, 2, 71682, NULL, NULL, N'fgfgf', N'fgfgf', N'fgfgf', N'fgfgf', N'fgfgf', NULL, 1, 1, NULL, NULL, NULL, NULL, 0, CAST(N'2024-07-23T14:00:42.450' AS DateTime), 1, NULL, 1)
INSERT [dbo].[Category] ([Id], [SystemUserId], [LanguageId], [PageTypeId], [CategoryId], [FormGroupId], [Code], [Name], [CategoryUrl], [Title], [MetaDecription], [MetaKeywords], [ShortExplanation], [CanBeDeleted], [CanSubCategoryBeAdded], [Image], [ImageTag], [ImageTitle], [Explanation], [OrderNumber], [RecordDate], [IsActive], [CategoryLink], [CanContentBeAdded]) VALUES (71684, 5, 4, 2, 71683, NULL, NULL, N'hhkhk', N'hhkhk', N'hhkhk', N'hhkhk', N'hhkhk', NULL, 1, 1, NULL, NULL, NULL, NULL, 0, CAST(N'2024-07-23T14:00:58.750' AS DateTime), 1, NULL, 1)
INSERT [dbo].[Category] ([Id], [SystemUserId], [LanguageId], [PageTypeId], [CategoryId], [FormGroupId], [Code], [Name], [CategoryUrl], [Title], [MetaDecription], [MetaKeywords], [ShortExplanation], [CanBeDeleted], [CanSubCategoryBeAdded], [Image], [ImageTag], [ImageTitle], [Explanation], [OrderNumber], [RecordDate], [IsActive], [CategoryLink], [CanContentBeAdded]) VALUES (71685, 5, 4, 2, 71684, NULL, NULL, N'fgfg', N'fgfg', N'fgfg', N'fgfg', N'fgfg', NULL, 1, 1, NULL, NULL, NULL, NULL, 0, CAST(N'2024-07-23T14:07:06.330' AS DateTime), 1, NULL, 1)
INSERT [dbo].[Category] ([Id], [SystemUserId], [LanguageId], [PageTypeId], [CategoryId], [FormGroupId], [Code], [Name], [CategoryUrl], [Title], [MetaDecription], [MetaKeywords], [ShortExplanation], [CanBeDeleted], [CanSubCategoryBeAdded], [Image], [ImageTag], [ImageTitle], [Explanation], [OrderNumber], [RecordDate], [IsActive], [CategoryLink], [CanContentBeAdded]) VALUES (71686, 5, 4, 2, 0, NULL, NULL, N'Ana sayfa', N'ana-sayfa', N'Ana sayfa', N'Ana sayfa', N'Ana sayfa', NULL, 1, 1, NULL, NULL, NULL, NULL, 0, CAST(N'2024-07-23T14:19:23.607' AS DateTime), 1, NULL, 1)
INSERT [dbo].[Category] ([Id], [SystemUserId], [LanguageId], [PageTypeId], [CategoryId], [FormGroupId], [Code], [Name], [CategoryUrl], [Title], [MetaDecription], [MetaKeywords], [ShortExplanation], [CanBeDeleted], [CanSubCategoryBeAdded], [Image], [ImageTag], [ImageTitle], [Explanation], [OrderNumber], [RecordDate], [IsActive], [CategoryLink], [CanContentBeAdded]) VALUES (71687, 5, 4, 2, 0, NULL, NULL, N'Hakkımızda', N'hakkimizda', N'Hakkımızda', N'Hakkımızda', N'Hakkımızda', NULL, 1, 1, N'1-563ece7a-b3e1-4819-bdee-e84910eeeb0c.jpg', NULL, NULL, NULL, 0, CAST(N'2024-07-23T14:19:46.387' AS DateTime), 1, NULL, 1)
INSERT [dbo].[Category] ([Id], [SystemUserId], [LanguageId], [PageTypeId], [CategoryId], [FormGroupId], [Code], [Name], [CategoryUrl], [Title], [MetaDecription], [MetaKeywords], [ShortExplanation], [CanBeDeleted], [CanSubCategoryBeAdded], [Image], [ImageTag], [ImageTitle], [Explanation], [OrderNumber], [RecordDate], [IsActive], [CategoryLink], [CanContentBeAdded]) VALUES (71688, 5, 4, 2, 0, NULL, NULL, N'Ürünler', N'urunler', N'Ürünler', N'Ürünler', N'Ürünler', NULL, 1, 1, NULL, NULL, NULL, NULL, 0, CAST(N'2024-07-23T14:19:59.763' AS DateTime), 1, NULL, 1)
INSERT [dbo].[Category] ([Id], [SystemUserId], [LanguageId], [PageTypeId], [CategoryId], [FormGroupId], [Code], [Name], [CategoryUrl], [Title], [MetaDecription], [MetaKeywords], [ShortExplanation], [CanBeDeleted], [CanSubCategoryBeAdded], [Image], [ImageTag], [ImageTitle], [Explanation], [OrderNumber], [RecordDate], [IsActive], [CategoryLink], [CanContentBeAdded]) VALUES (71689, 5, 4, 2, 0, NULL, NULL, N'Haberler', N'haberler', N'Haberler', N'Haberler', N'Haberler', NULL, 1, 1, NULL, NULL, NULL, NULL, 0, CAST(N'2024-07-23T14:20:09.110' AS DateTime), 1, NULL, 1)
INSERT [dbo].[Category] ([Id], [SystemUserId], [LanguageId], [PageTypeId], [CategoryId], [FormGroupId], [Code], [Name], [CategoryUrl], [Title], [MetaDecription], [MetaKeywords], [ShortExplanation], [CanBeDeleted], [CanSubCategoryBeAdded], [Image], [ImageTag], [ImageTitle], [Explanation], [OrderNumber], [RecordDate], [IsActive], [CategoryLink], [CanContentBeAdded]) VALUES (71690, 5, 4, 2, 71687, NULL, NULL, N'Misyon', N'misyon', N'Misyon', N'Misyon', N'Misyon', NULL, 1, 1, NULL, NULL, NULL, NULL, 0, CAST(N'2024-07-23T14:20:25.900' AS DateTime), 1, NULL, 1)
INSERT [dbo].[Category] ([Id], [SystemUserId], [LanguageId], [PageTypeId], [CategoryId], [FormGroupId], [Code], [Name], [CategoryUrl], [Title], [MetaDecription], [MetaKeywords], [ShortExplanation], [CanBeDeleted], [CanSubCategoryBeAdded], [Image], [ImageTag], [ImageTitle], [Explanation], [OrderNumber], [RecordDate], [IsActive], [CategoryLink], [CanContentBeAdded]) VALUES (71691, 5, 4, 2, 71687, NULL, NULL, N'Vizyon', N'vizyon', N'Vizyon', N'Vizyon', N'Vizyon', NULL, 1, 1, NULL, NULL, NULL, NULL, 0, CAST(N'2024-07-23T14:20:41.003' AS DateTime), 1, NULL, 1)
INSERT [dbo].[Category] ([Id], [SystemUserId], [LanguageId], [PageTypeId], [CategoryId], [FormGroupId], [Code], [Name], [CategoryUrl], [Title], [MetaDecription], [MetaKeywords], [ShortExplanation], [CanBeDeleted], [CanSubCategoryBeAdded], [Image], [ImageTag], [ImageTitle], [Explanation], [OrderNumber], [RecordDate], [IsActive], [CategoryLink], [CanContentBeAdded]) VALUES (71692, 5, 4, 2, 71687, NULL, NULL, N'Sertifikalar', N'sertifikalar', N'Sertifikalar', N'Sertifikalar', N'Sertifikalar', NULL, 1, 1, NULL, NULL, NULL, NULL, 0, CAST(N'2024-07-23T14:21:03.230' AS DateTime), 1, NULL, 1)
INSERT [dbo].[Category] ([Id], [SystemUserId], [LanguageId], [PageTypeId], [CategoryId], [FormGroupId], [Code], [Name], [CategoryUrl], [Title], [MetaDecription], [MetaKeywords], [ShortExplanation], [CanBeDeleted], [CanSubCategoryBeAdded], [Image], [ImageTag], [ImageTitle], [Explanation], [OrderNumber], [RecordDate], [IsActive], [CategoryLink], [CanContentBeAdded]) VALUES (71693, 5, 4, 2, 71692, NULL, NULL, N'Sertifika 1', N'sertifika-1', N'Sertifika 1', N'Sertifika 1', N'Sertifika 1', NULL, 1, 1, N'channels4-profile-eae21496-51db-40cf-8441-65540b19f4e5.jpg', NULL, NULL, NULL, 0, CAST(N'2024-07-23T14:24:48.953' AS DateTime), 1, NULL, 1)
INSERT [dbo].[Category] ([Id], [SystemUserId], [LanguageId], [PageTypeId], [CategoryId], [FormGroupId], [Code], [Name], [CategoryUrl], [Title], [MetaDecription], [MetaKeywords], [ShortExplanation], [CanBeDeleted], [CanSubCategoryBeAdded], [Image], [ImageTag], [ImageTitle], [Explanation], [OrderNumber], [RecordDate], [IsActive], [CategoryLink], [CanContentBeAdded]) VALUES (71694, 5, 4, 2, 0, NULL, NULL, N'İletişim', N'iletisim', N'İletişim', N'İletişim', N'İletişim', NULL, 1, 1, NULL, NULL, NULL, NULL, 0, CAST(N'2024-07-23T14:31:16.270' AS DateTime), 1, NULL, 1)
INSERT [dbo].[Category] ([Id], [SystemUserId], [LanguageId], [PageTypeId], [CategoryId], [FormGroupId], [Code], [Name], [CategoryUrl], [Title], [MetaDecription], [MetaKeywords], [ShortExplanation], [CanBeDeleted], [CanSubCategoryBeAdded], [Image], [ImageTag], [ImageTitle], [Explanation], [OrderNumber], [RecordDate], [IsActive], [CategoryLink], [CanContentBeAdded]) VALUES (71695, 5, 4, 2, 0, NULL, NULL, N'deneme', N'deneme', N'deneme', N'deneme', N'deneme', NULL, 1, 1, N'channels4-profile-aa1a4bb3-a553-492a-b061-f704147b1660.jpg', NULL, NULL, NULL, 0, CAST(N'2024-10-25T14:47:52.437' AS DateTime), 1, NULL, 1)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[EditorImages] ON 

INSERT [dbo].[EditorImages] ([Id], [Name]) VALUES (1, N'1-1449ce21-c3b8-4cc7-9b7c-bb8a8cb51fcb.jpg')
SET IDENTITY_INSERT [dbo].[EditorImages] OFF
GO
SET IDENTITY_INSERT [dbo].[EditorTemplate] ON 

INSERT [dbo].[EditorTemplate] ([Id], [SystemUserId], [LanguageId], [Name], [HtmlContent], [Image], [OrderNumber], [RecordDate], [IsActive]) VALUES (1, 1, 4, N'Test', NULL, NULL, 0, CAST(N'2021-10-12T18:32:31.457' AS DateTime), 0)
INSERT [dbo].[EditorTemplate] ([Id], [SystemUserId], [LanguageId], [Name], [HtmlContent], [Image], [OrderNumber], [RecordDate], [IsActive]) VALUES (2, NULL, 4, N'Test', N'adsdsddsdsds', NULL, 0, CAST(N'2021-10-12T18:32:48.000' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[EditorTemplate] OFF
GO
SET IDENTITY_INSERT [dbo].[FirmVariables] ON 

INSERT [dbo].[FirmVariables] ([Id], [SystemUserId], [LanguageId], [Name], [Value], [Description], [OrderNumber], [RecordDate], [IsActive]) VALUES (3, NULL, 4, N'CategoryBigImageWidth', NULL, N'500', 0, CAST(N'2021-10-17T21:46:09.043' AS DateTime), 0)
INSERT [dbo].[FirmVariables] ([Id], [SystemUserId], [LanguageId], [Name], [Value], [Description], [OrderNumber], [RecordDate], [IsActive]) VALUES (4, NULL, 4, N'CategorySmallImageWidth', NULL, N'500', 0, CAST(N'2021-10-17T21:46:25.670' AS DateTime), 0)
INSERT [dbo].[FirmVariables] ([Id], [SystemUserId], [LanguageId], [Name], [Value], [Description], [OrderNumber], [RecordDate], [IsActive]) VALUES (5, NULL, 4, N'CategorySmallImageHeight', NULL, N'500', 0, CAST(N'2021-10-17T21:46:58.127' AS DateTime), 0)
INSERT [dbo].[FirmVariables] ([Id], [SystemUserId], [LanguageId], [Name], [Value], [Description], [OrderNumber], [RecordDate], [IsActive]) VALUES (12, NULL, 5, N'6', NULL, N'6', 0, CAST(N'2022-05-09T14:49:38.910' AS DateTime), 1)
INSERT [dbo].[FirmVariables] ([Id], [SystemUserId], [LanguageId], [Name], [Value], [Description], [OrderNumber], [RecordDate], [IsActive]) VALUES (13, NULL, 4, N'5', N'5', N'5', 5, CAST(N'2024-05-11T13:59:33.153' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[FirmVariables] OFF
GO
SET IDENTITY_INSERT [dbo].[FormElement] ON 

INSERT [dbo].[FormElement] ([Id], [CategoryId], [SystemUserId], [LanguageId], [FormGroupId], [FormTypeId], [ElementTypeId], [Name], [IsRequired], [ValidationMessage], [AllowNulls], [OrderNumber], [RecordDate], [IsActive]) VALUES (1, 0, 1, 1, 1, NULL, 1, N'1', NULL, NULL, 1, 0, CAST(N'2021-10-13T19:40:38.120' AS DateTime), 1)
INSERT [dbo].[FormElement] ([Id], [CategoryId], [SystemUserId], [LanguageId], [FormGroupId], [FormTypeId], [ElementTypeId], [Name], [IsRequired], [ValidationMessage], [AllowNulls], [OrderNumber], [RecordDate], [IsActive]) VALUES (2, 0, 1, 1, 3, NULL, 1, N'asasasa', NULL, NULL, 0, 0, CAST(N'2021-10-14T22:00:28.427' AS DateTime), 0)
INSERT [dbo].[FormElement] ([Id], [CategoryId], [SystemUserId], [LanguageId], [FormGroupId], [FormTypeId], [ElementTypeId], [Name], [IsRequired], [ValidationMessage], [AllowNulls], [OrderNumber], [RecordDate], [IsActive]) VALUES (3, 0, 1, 1, 3, NULL, 1, N'asasasa', NULL, NULL, 0, 0, CAST(N'2021-10-14T22:00:28.427' AS DateTime), 0)
INSERT [dbo].[FormElement] ([Id], [CategoryId], [SystemUserId], [LanguageId], [FormGroupId], [FormTypeId], [ElementTypeId], [Name], [IsRequired], [ValidationMessage], [AllowNulls], [OrderNumber], [RecordDate], [IsActive]) VALUES (4, 0, 1, 1, 3, NULL, 1, N'asasasa', NULL, NULL, 0, 0, CAST(N'2021-10-14T22:00:28.427' AS DateTime), 0)
INSERT [dbo].[FormElement] ([Id], [CategoryId], [SystemUserId], [LanguageId], [FormGroupId], [FormTypeId], [ElementTypeId], [Name], [IsRequired], [ValidationMessage], [AllowNulls], [OrderNumber], [RecordDate], [IsActive]) VALUES (5, 0, 1, 1, 1, NULL, 1, N'asas', NULL, NULL, 0, 0, CAST(N'2021-10-14T22:00:51.883' AS DateTime), 0)
INSERT [dbo].[FormElement] ([Id], [CategoryId], [SystemUserId], [LanguageId], [FormGroupId], [FormTypeId], [ElementTypeId], [Name], [IsRequired], [ValidationMessage], [AllowNulls], [OrderNumber], [RecordDate], [IsActive]) VALUES (1002, 0, 1, 1, 1, NULL, 5, N'test drop', NULL, NULL, 0, 1, CAST(N'2021-10-19T16:00:27.360' AS DateTime), 0)
INSERT [dbo].[FormElement] ([Id], [CategoryId], [SystemUserId], [LanguageId], [FormGroupId], [FormTypeId], [ElementTypeId], [Name], [IsRequired], [ValidationMessage], [AllowNulls], [OrderNumber], [RecordDate], [IsActive]) VALUES (1007, NULL, 1, 1, 1, NULL, 1, N'5', NULL, NULL, 0, 5, CAST(N'2022-04-18T14:19:14.000' AS DateTime), 0)
INSERT [dbo].[FormElement] ([Id], [CategoryId], [SystemUserId], [LanguageId], [FormGroupId], [FormTypeId], [ElementTypeId], [Name], [IsRequired], [ValidationMessage], [AllowNulls], [OrderNumber], [RecordDate], [IsActive]) VALUES (1008, NULL, 5, 4, 5, NULL, 5, N'f', NULL, NULL, 0, 0, CAST(N'2024-05-18T21:56:59.000' AS DateTime), 0)
INSERT [dbo].[FormElement] ([Id], [CategoryId], [SystemUserId], [LanguageId], [FormGroupId], [FormTypeId], [ElementTypeId], [Name], [IsRequired], [ValidationMessage], [AllowNulls], [OrderNumber], [RecordDate], [IsActive]) VALUES (1014, 0, 5, 4, 1, NULL, 1, N'ad', NULL, NULL, NULL, 0, CAST(N'2024-05-18T23:08:13.533' AS DateTime), 1)
INSERT [dbo].[FormElement] ([Id], [CategoryId], [SystemUserId], [LanguageId], [FormGroupId], [FormTypeId], [ElementTypeId], [Name], [IsRequired], [ValidationMessage], [AllowNulls], [OrderNumber], [RecordDate], [IsActive]) VALUES (1015, 0, 5, 4, 1, NULL, 1, N'Adınız', NULL, NULL, NULL, 0, CAST(N'2024-05-18T23:08:49.767' AS DateTime), 1)
INSERT [dbo].[FormElement] ([Id], [CategoryId], [SystemUserId], [LanguageId], [FormGroupId], [FormTypeId], [ElementTypeId], [Name], [IsRequired], [ValidationMessage], [AllowNulls], [OrderNumber], [RecordDate], [IsActive]) VALUES (1016, 0, 5, 4, 1, NULL, 1, N'Adınız', NULL, NULL, NULL, 0, CAST(N'2024-05-18T23:09:30.540' AS DateTime), 1)
INSERT [dbo].[FormElement] ([Id], [CategoryId], [SystemUserId], [LanguageId], [FormGroupId], [FormTypeId], [ElementTypeId], [Name], [IsRequired], [ValidationMessage], [AllowNulls], [OrderNumber], [RecordDate], [IsActive]) VALUES (1018, 0, 5, 4, 1, NULL, 1, N'Deneme', NULL, NULL, NULL, 0, CAST(N'2024-06-22T19:22:39.323' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[FormElement] OFF
GO
SET IDENTITY_INSERT [dbo].[FormElementOptions] ON 

INSERT [dbo].[FormElementOptions] ([Id], [SystemUserId], [LanguageId], [FormElementId], [Name], [Value], [IsSelected], [OrderNumber], [RecordDate], [IsActive]) VALUES (1, 1, 1, 0, N'test', N'test', 1, 0, CAST(N'2021-10-19T15:57:45.987' AS DateTime), 0)
INSERT [dbo].[FormElementOptions] ([Id], [SystemUserId], [LanguageId], [FormElementId], [Name], [Value], [IsSelected], [OrderNumber], [RecordDate], [IsActive]) VALUES (2, 1, 1, 1002, N'test', N'test', 0, 0, CAST(N'2021-10-19T16:00:40.450' AS DateTime), 0)
INSERT [dbo].[FormElementOptions] ([Id], [SystemUserId], [LanguageId], [FormElementId], [Name], [Value], [IsSelected], [OrderNumber], [RecordDate], [IsActive]) VALUES (3, 1, 1, 1002, N'g', N'k', 0, 0, CAST(N'2021-10-24T22:16:00.990' AS DateTime), 0)
INSERT [dbo].[FormElementOptions] ([Id], [SystemUserId], [LanguageId], [FormElementId], [Name], [Value], [IsSelected], [OrderNumber], [RecordDate], [IsActive]) VALUES (4, 5, 4, 1, N'sd', N'sd', 0, 0, CAST(N'2024-05-20T21:53:53.187' AS DateTime), 0)
INSERT [dbo].[FormElementOptions] ([Id], [SystemUserId], [LanguageId], [FormElementId], [Name], [Value], [IsSelected], [OrderNumber], [RecordDate], [IsActive]) VALUES (5, 5, 4, 0, N'sdsdds', N'22', 0, 0, CAST(N'2024-05-20T21:56:55.990' AS DateTime), 0)
INSERT [dbo].[FormElementOptions] ([Id], [SystemUserId], [LanguageId], [FormElementId], [Name], [Value], [IsSelected], [OrderNumber], [RecordDate], [IsActive]) VALUES (11, 5, 4, 0, N'Erdoğan', N'0', 0, 0, CAST(N'2024-05-21T11:17:17.243' AS DateTime), 1)
INSERT [dbo].[FormElementOptions] ([Id], [SystemUserId], [LanguageId], [FormElementId], [Name], [Value], [IsSelected], [OrderNumber], [RecordDate], [IsActive]) VALUES (12, 5, 4, 0, N'Ahmet', N'0', 0, 0, CAST(N'2024-05-21T11:18:49.217' AS DateTime), 1)
INSERT [dbo].[FormElementOptions] ([Id], [SystemUserId], [LanguageId], [FormElementId], [Name], [Value], [IsSelected], [OrderNumber], [RecordDate], [IsActive]) VALUES (13, 5, 4, 0, N'df', N'2', 0, 0, CAST(N'2024-05-21T11:22:10.550' AS DateTime), 1)
INSERT [dbo].[FormElementOptions] ([Id], [SystemUserId], [LanguageId], [FormElementId], [Name], [Value], [IsSelected], [OrderNumber], [RecordDate], [IsActive]) VALUES (14, 5, 4, 1016, N'1', N'1', 1, 0, CAST(N'2024-05-21T11:23:28.353' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[FormElementOptions] OFF
GO
SET IDENTITY_INSERT [dbo].[FormGroup] ON 

INSERT [dbo].[FormGroup] ([Id], [SystemUserId], [LanguageId], [Name], [Explanation], [OrderNumber], [RecordDate], [IsActive]) VALUES (1, 1, 1, N'test', N'test', 0, CAST(N'2021-10-14' AS Date), 0)
INSERT [dbo].[FormGroup] ([Id], [SystemUserId], [LanguageId], [Name], [Explanation], [OrderNumber], [RecordDate], [IsActive]) VALUES (2, 1, 1, N'test1', N'test1', 1, CAST(N'2021-10-14' AS Date), 1)
INSERT [dbo].[FormGroup] ([Id], [SystemUserId], [LanguageId], [Name], [Explanation], [OrderNumber], [RecordDate], [IsActive]) VALUES (4, 1, 1, N'2', N'6', 6, CAST(N'2022-04-18' AS Date), 0)
INSERT [dbo].[FormGroup] ([Id], [SystemUserId], [LanguageId], [Name], [Explanation], [OrderNumber], [RecordDate], [IsActive]) VALUES (5, 5, 4, N'test', N'3', 0, CAST(N'2024-05-18' AS Date), 0)
SET IDENTITY_INSERT [dbo].[FormGroup] OFF
GO
SET IDENTITY_INSERT [dbo].[GeneralSettings] ON 

INSERT [dbo].[GeneralSettings] ([Id], [LanguageId], [SystemUserId], [EmailIpAdress], [EmailAdress], [EmailPort], [EmailPassword], [DomainName], [ImageCdnUrl], [FileCdnUrl], [ImageUploadPath], [FileUploadPath], [DeveloperName], [DeveloperEmail], [CategoryImageSmallWidth], [CategoryImageSmallHeight], [CategoryImageBigWidth], [CategoryImageBigHeight], [ContentImageSmallWidth], [ContentImageSmallHeight], [ContentImageBigWidth], [ContentImageBigHeight], [GalleryImageSmallWidth], [GalleryImageSmallHeight], [GalleryImageBigWidth], [GalleryImageBigHeight], [EditorImageUploadCdn], [EditorImageUploadPath], [Logo]) VALUES (7, NULL, NULL, N'100', N'1', 1, N'1', N'https://localhost:44338/', N'http://localhost:81/Content/Uploads/Image/', N'http://localhost:81/Content/Uploads/File/', N'C:\inetpub\wwwroot\admin\Content\Uploads\Image\', N'C:\inetpub\wwwroot\admin\Content\Uploads\File\', N'Erdoğan KABA', N'erdogankb57@gmail.com', 200, 200, 1000, 1000, 200, 200, 1000, 1000, 200, 200, 500, 500, N'https://localhost:44306/Uploads/EditorImages/', N'C:\inetpub\wwwroot\admin\Content\Uploads\EditorImages\', N'1-3878af54-6860-4834-8d53-448a2a1c6eb6.jpg')
INSERT [dbo].[GeneralSettings] ([Id], [LanguageId], [SystemUserId], [EmailIpAdress], [EmailAdress], [EmailPort], [EmailPassword], [DomainName], [ImageCdnUrl], [FileCdnUrl], [ImageUploadPath], [FileUploadPath], [DeveloperName], [DeveloperEmail], [CategoryImageSmallWidth], [CategoryImageSmallHeight], [CategoryImageBigWidth], [CategoryImageBigHeight], [ContentImageSmallWidth], [ContentImageSmallHeight], [ContentImageBigWidth], [ContentImageBigHeight], [GalleryImageSmallWidth], [GalleryImageSmallHeight], [GalleryImageBigWidth], [GalleryImageBigHeight], [EditorImageUploadCdn], [EditorImageUploadPath], [Logo]) VALUES (8, 5, 1, N'1', N'1', 1, N'1', N'1', N'1', NULL, N'1', N'1', N'1', N'11', 1, 1, 11, 1, 1, 1, 11, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[GeneralSettings] OFF
GO
SET IDENTITY_INSERT [dbo].[Language] ON 

INSERT [dbo].[Language] ([Id], [Code], [Name], [Description], [OrderNumber], [RecordDate], [IsActive]) VALUES (4, N'tr', N'Türkçe', NULL, 1, CAST(N'2022-04-21T09:20:46.517' AS DateTime), 1)
INSERT [dbo].[Language] ([Id], [Code], [Name], [Description], [OrderNumber], [RecordDate], [IsActive]) VALUES (5, N'en', N'İngilizce', NULL, 1, CAST(N'2022-04-21T09:20:54.467' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Language] OFF
GO
SET IDENTITY_INSERT [dbo].[LogMessage] ON 

INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (482, N'Kayıt işlemi sırasında hata oluştu', N'Microsoft.EntityFrameworkCore.Relational', N'An error occurred while saving the entity changes. See the inner exception for details.', N'Invalid object name ''dbo.Banner''.', N'   at Microsoft.EntityFrameworkCore.Update.ReaderModificationCommandBatch.Execute(IRelationalConnection connection)
   at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.Execute(IEnumerable`1 commandBatches, IRelationalConnection connection)
   at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.SaveChanges(IList`1 entriesToSave)
   at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.SaveChanges(StateManager stateManager, Boolean acceptAllChangesOnSuccess)
   at Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal.SqlServerExecutionStrategy.Execute[TState,TResult](TState state, Func`3 operation, Func`3 verifySucceeded)
   at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.SaveChanges(Boolean acceptAllChangesOnSuccess)
   at Microsoft.EntityFrameworkCore.DbContext.SaveChanges(Boolean acceptAllChangesOnSuccess)
   at Inta.EntityFramework.Core.Base.UnitOfWork`1.SaveChanges() in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\UnitOfWork.cs:line 35', N'', CAST(N'2023-09-14T20:18:21.877' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (483, N'RepositoryBase`2 base repository find', N'Core Microsoft SqlClient Data Provider', N'Invalid object name ''dbo.Banner''.', N'', N'   at Microsoft.Data.SqlClient.SqlConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at Microsoft.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj, Boolean callerHasConnectionLock, Boolean asyncClose)
   at Microsoft.Data.SqlClient.TdsParser.TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, Boolean& dataReady)
   at Microsoft.Data.SqlClient.SqlDataReader.TryConsumeMetaData()
   at Microsoft.Data.SqlClient.SqlDataReader.get_MetaData()
   at Microsoft.Data.SqlClient.SqlCommand.FinishExecuteReader(SqlDataReader ds, RunBehavior runBehavior, String resetOptionsString, Boolean isInternal, Boolean forDescribeParameterEncryption, Boolean shouldCacheForAlwaysEncrypted)
   at Microsoft.Data.SqlClient.SqlCommand.RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, Boolean isAsync, Int32 timeout, Task& task, Boolean asyncWrite, Boolean inRetry, SqlDataReader ds, Boolean describeParameterEncryptionRequest)
   at Microsoft.Data.SqlClient.SqlCommand.RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, TaskCompletionSource`1 completion, Int32 timeout, Task& task, Boolean& usedCache, Boolean asyncWrite, Boolean inRetry, String method)
   at Microsoft.Data.SqlClient.SqlCommand.ExecuteReader(CommandBehavior behavior)
   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReader(RelationalCommandParameterObject parameterObject)
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.InitializeReader(Enumerator enumerator)
   at Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal.SqlServerExecutionStrategy.Execute[TState,TResult](TState state, Func`3 operation, Func`3 verifySucceeded)
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2023-09-14T20:36:06.433' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (484, N'RepositoryBase`2 base repository find', N'Core Microsoft SqlClient Data Provider', N'Invalid object name ''dbo.Banner''.', N'', N'   at Microsoft.Data.SqlClient.SqlConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at Microsoft.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj, Boolean callerHasConnectionLock, Boolean asyncClose)
   at Microsoft.Data.SqlClient.TdsParser.TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, Boolean& dataReady)
   at Microsoft.Data.SqlClient.SqlDataReader.TryConsumeMetaData()
   at Microsoft.Data.SqlClient.SqlDataReader.get_MetaData()
   at Microsoft.Data.SqlClient.SqlCommand.FinishExecuteReader(SqlDataReader ds, RunBehavior runBehavior, String resetOptionsString, Boolean isInternal, Boolean forDescribeParameterEncryption, Boolean shouldCacheForAlwaysEncrypted)
   at Microsoft.Data.SqlClient.SqlCommand.RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, Boolean isAsync, Int32 timeout, Task& task, Boolean asyncWrite, Boolean inRetry, SqlDataReader ds, Boolean describeParameterEncryptionRequest)
   at Microsoft.Data.SqlClient.SqlCommand.RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, TaskCompletionSource`1 completion, Int32 timeout, Task& task, Boolean& usedCache, Boolean asyncWrite, Boolean inRetry, String method)
   at Microsoft.Data.SqlClient.SqlCommand.ExecuteReader(CommandBehavior behavior)
   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReader(RelationalCommandParameterObject parameterObject)
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.InitializeReader(Enumerator enumerator)
   at Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal.SqlServerExecutionStrategy.Execute[TState,TResult](TState state, Func`3 operation, Func`3 verifySucceeded)
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2023-09-14T20:43:24.667' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (485, N'RepositoryBase`2 base repository find', N'Core Microsoft SqlClient Data Provider', N'Invalid object name ''dbo.Banner''.', N'', N'   at Microsoft.Data.SqlClient.SqlConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at Microsoft.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj, Boolean callerHasConnectionLock, Boolean asyncClose)
   at Microsoft.Data.SqlClient.TdsParser.TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, Boolean& dataReady)
   at Microsoft.Data.SqlClient.SqlDataReader.TryConsumeMetaData()
   at Microsoft.Data.SqlClient.SqlDataReader.get_MetaData()
   at Microsoft.Data.SqlClient.SqlCommand.FinishExecuteReader(SqlDataReader ds, RunBehavior runBehavior, String resetOptionsString, Boolean isInternal, Boolean forDescribeParameterEncryption, Boolean shouldCacheForAlwaysEncrypted)
   at Microsoft.Data.SqlClient.SqlCommand.RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, Boolean isAsync, Int32 timeout, Task& task, Boolean asyncWrite, Boolean inRetry, SqlDataReader ds, Boolean describeParameterEncryptionRequest)
   at Microsoft.Data.SqlClient.SqlCommand.RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, TaskCompletionSource`1 completion, Int32 timeout, Task& task, Boolean& usedCache, Boolean asyncWrite, Boolean inRetry, String method)
   at Microsoft.Data.SqlClient.SqlCommand.ExecuteReader(CommandBehavior behavior)
   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReader(RelationalCommandParameterObject parameterObject)
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.InitializeReader(Enumerator enumerator)
   at Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal.SqlServerExecutionStrategy.Execute[TState,TResult](TState state, Func`3 operation, Func`3 verifySucceeded)
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2023-09-14T20:43:31.023' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (486, N'', N'', N'Exception of type ''System.Exception'' was thrown.', N'', N'', N'', CAST(N'2023-11-20T20:38:31.397' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (487, N'', N'', N'Exception of type ''System.Exception'' was thrown.', N'', N'', N'', CAST(N'2023-11-20T20:38:53.367' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (488, N'', N'', N'Exception of type ''System.Exception'' was thrown.', N'', N'', N'', CAST(N'2023-11-20T20:39:22.043' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (489, N'', N'', N'Exception of type ''System.Exception'' was thrown.', N'', N'', N'', CAST(N'2023-11-20T20:39:25.080' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (490, N'', N'', N'Exception of type ''System.Exception'' was thrown.', N'', N'', N'', CAST(N'2023-11-20T22:04:33.633' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (491, N'', N'', N'Exception of type ''System.Exception'' was thrown.', N'', N'', N'', CAST(N'2023-11-20T22:05:20.197' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (492, N'', N'', N'Exception of type ''System.Exception'' was thrown.', N'', N'', N'', CAST(N'2023-11-20T22:06:44.490' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (493, N'', N'', N'Exception of type ''System.Exception'' was thrown.', N'', N'', N'', CAST(N'2023-11-20T22:07:17.750' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (494, N'', N'', N'Exception of type ''System.Exception'' was thrown.', N'', N'', N'', CAST(N'2023-11-20T22:07:39.477' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (495, N'', N'', N'Exception of type ''System.Exception'' was thrown.', N'', N'', N'', CAST(N'2023-11-20T22:08:10.783' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (496, N'RepositoryBase`2 base repository find', N'Core Microsoft SqlClient Data Provider', N'Invalid column name ''LanguageCode''.
Invalid column name ''LanguageCode''.', N'', N'   at Microsoft.Data.SqlClient.SqlConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at Microsoft.Data.SqlClient.SqlInternalConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at Microsoft.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj, Boolean callerHasConnectionLock, Boolean asyncClose)
   at Microsoft.Data.SqlClient.TdsParser.TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, Boolean& dataReady)
   at Microsoft.Data.SqlClient.SqlDataReader.TryConsumeMetaData()
   at Microsoft.Data.SqlClient.SqlDataReader.get_MetaData()
   at Microsoft.Data.SqlClient.SqlCommand.FinishExecuteReader(SqlDataReader ds, RunBehavior runBehavior, String resetOptionsString, Boolean isInternal, Boolean forDescribeParameterEncryption, Boolean shouldCacheForAlwaysEncrypted)
   at Microsoft.Data.SqlClient.SqlCommand.RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, Boolean isAsync, Int32 timeout, Task& task, Boolean asyncWrite, Boolean inRetry, SqlDataReader ds, Boolean describeParameterEncryptionRequest)
   at Microsoft.Data.SqlClient.SqlCommand.RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, TaskCompletionSource`1 completion, Int32 timeout, Task& task, Boolean& usedCache, Boolean asyncWrite, Boolean inRetry, String method)
   at Microsoft.Data.SqlClient.SqlCommand.ExecuteReader(CommandBehavior behavior)
   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReader(RelationalCommandParameterObject parameterObject)
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.InitializeReader(Enumerator enumerator)
   at Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal.SqlServerExecutionStrategy.Execute[TState,TResult](TState state, Func`3 operation, Func`3 verifySucceeded)
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2023-11-27T00:05:05.300' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (497, N'RepositoryBase`2 base repository find', N'Core Microsoft SqlClient Data Provider', N'Invalid column name ''LanguageCode''.
Invalid column name ''LanguageCode''.', N'', N'   at Microsoft.Data.SqlClient.SqlConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at Microsoft.Data.SqlClient.SqlInternalConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at Microsoft.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj, Boolean callerHasConnectionLock, Boolean asyncClose)
   at Microsoft.Data.SqlClient.TdsParser.TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, Boolean& dataReady)
   at Microsoft.Data.SqlClient.SqlDataReader.TryConsumeMetaData()
   at Microsoft.Data.SqlClient.SqlDataReader.get_MetaData()
   at Microsoft.Data.SqlClient.SqlCommand.FinishExecuteReader(SqlDataReader ds, RunBehavior runBehavior, String resetOptionsString, Boolean isInternal, Boolean forDescribeParameterEncryption, Boolean shouldCacheForAlwaysEncrypted)
   at Microsoft.Data.SqlClient.SqlCommand.RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, Boolean isAsync, Int32 timeout, Task& task, Boolean asyncWrite, Boolean inRetry, SqlDataReader ds, Boolean describeParameterEncryptionRequest)
   at Microsoft.Data.SqlClient.SqlCommand.RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, TaskCompletionSource`1 completion, Int32 timeout, Task& task, Boolean& usedCache, Boolean asyncWrite, Boolean inRetry, String method)
   at Microsoft.Data.SqlClient.SqlCommand.ExecuteReader(CommandBehavior behavior)
   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReader(RelationalCommandParameterObject parameterObject)
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.InitializeReader(Enumerator enumerator)
   at Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal.SqlServerExecutionStrategy.Execute[TState,TResult](TState state, Func`3 operation, Func`3 verifySucceeded)
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2023-11-27T00:05:09.663' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (498, N'', N'', N'Exception of type ''System.Exception'' was thrown.', N'', N'', N'', CAST(N'2023-11-30T22:44:46.677' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (499, N'RepositoryBase`2 base repository find', N'Core Microsoft SqlClient Data Provider', N'Invalid column name ''LanguageCode''.
Invalid column name ''LanguageCode''.', N'', N'   at Microsoft.Data.SqlClient.SqlConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at Microsoft.Data.SqlClient.SqlInternalConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at Microsoft.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj, Boolean callerHasConnectionLock, Boolean asyncClose)
   at Microsoft.Data.SqlClient.TdsParser.TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, Boolean& dataReady)
   at Microsoft.Data.SqlClient.SqlDataReader.TryConsumeMetaData()
   at Microsoft.Data.SqlClient.SqlDataReader.get_MetaData()
   at Microsoft.Data.SqlClient.SqlCommand.FinishExecuteReader(SqlDataReader ds, RunBehavior runBehavior, String resetOptionsString, Boolean isInternal, Boolean forDescribeParameterEncryption, Boolean shouldCacheForAlwaysEncrypted)
   at Microsoft.Data.SqlClient.SqlCommand.RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, Boolean isAsync, Int32 timeout, Task& task, Boolean asyncWrite, Boolean inRetry, SqlDataReader ds, Boolean describeParameterEncryptionRequest)
   at Microsoft.Data.SqlClient.SqlCommand.RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, TaskCompletionSource`1 completion, Int32 timeout, Task& task, Boolean& usedCache, Boolean asyncWrite, Boolean inRetry, String method)
   at Microsoft.Data.SqlClient.SqlCommand.ExecuteReader(CommandBehavior behavior)
   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReader(RelationalCommandParameterObject parameterObject)
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.InitializeReader(Enumerator enumerator)
   at Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal.SqlServerExecutionStrategy.Execute[TState,TResult](TState state, Func`3 operation, Func`3 verifySucceeded)
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2023-12-15T00:08:21.087' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (500, N'RepositoryBase`2 base repository find', N'Microsoft.Data.SqlClient', N'Data is Null. This method or property cannot be called on Null values.', N'', N'   at Microsoft.Data.SqlClient.SqlBuffer.get_String()
   at lambda_method258(Closure , QueryContext , DbDataReader , ResultContext , SingleQueryResultCoordinator )
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 106', N'', CAST(N'2024-05-11T14:49:20.073' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (501, N'RepositoryBase`2 base repository find', N'Microsoft.Data.SqlClient', N'Data is Null. This method or property cannot be called on Null values.', N'', N'   at Microsoft.Data.SqlClient.SqlBuffer.ThrowIfNull()
   at Microsoft.Data.SqlClient.SqlBuffer.get_DateTime()
   at Microsoft.Data.SqlClient.SqlDataReader.GetDateTime(Int32 i)
   at lambda_method118(Closure , QueryContext , DbDataReader , ResultContext , SingleQueryResultCoordinator )
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2024-05-13T14:47:07.860' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (502, N'RepositoryBase`2 base repository find', N'Microsoft.Data.SqlClient', N'Data is Null. This method or property cannot be called on Null values.', N'', N'   at Microsoft.Data.SqlClient.SqlBuffer.ThrowIfNull()
   at Microsoft.Data.SqlClient.SqlBuffer.get_DateTime()
   at Microsoft.Data.SqlClient.SqlDataReader.GetDateTime(Int32 i)
   at lambda_method118(Closure , QueryContext , DbDataReader , ResultContext , SingleQueryResultCoordinator )
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2024-05-13T14:47:08.113' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (503, N'RepositoryBase`2 base repository find', N'Microsoft.Data.SqlClient', N'Data is Null. This method or property cannot be called on Null values.', N'', N'   at Microsoft.Data.SqlClient.SqlBuffer.get_DateTime()
   at Microsoft.Data.SqlClient.SqlDataReader.GetDateTime(Int32 i)
   at lambda_method118(Closure , QueryContext , DbDataReader , ResultContext , SingleQueryResultCoordinator )
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2024-05-13T14:47:12.450' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (504, N'RepositoryBase`2 base repository find', N'Microsoft.Data.SqlClient', N'Data is Null. This method or property cannot be called on Null values.', N'', N'   at Microsoft.Data.SqlClient.SqlBuffer.get_Int32()
   at lambda_method208(Closure , QueryContext , DbDataReader , ResultContext , SingleQueryResultCoordinator )
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2024-05-13T14:47:12.960' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (505, N'RepositoryBase`2 base repository find', N'Microsoft.Data.SqlClient', N'Data is Null. This method or property cannot be called on Null values.', N'', N'   at Microsoft.Data.SqlClient.SqlBuffer.get_DateTime()
   at Microsoft.Data.SqlClient.SqlDataReader.GetDateTime(Int32 i)
   at lambda_method118(Closure , QueryContext , DbDataReader , ResultContext , SingleQueryResultCoordinator )
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2024-05-13T14:47:25.933' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (506, N'RepositoryBase`2 base repository find', N'Microsoft.Data.SqlClient', N'Data is Null. This method or property cannot be called on Null values.', N'', N'   at Microsoft.Data.SqlClient.SqlBuffer.get_DateTime()
   at Microsoft.Data.SqlClient.SqlDataReader.GetDateTime(Int32 i)
   at lambda_method118(Closure , QueryContext , DbDataReader , ResultContext , SingleQueryResultCoordinator )
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2024-05-13T14:47:40.770' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (507, N'RepositoryBase`2 base repository find', N'Microsoft.Data.SqlClient', N'Data is Null. This method or property cannot be called on Null values.', N'', N'   at Microsoft.Data.SqlClient.SqlBuffer.get_DateTime()
   at Microsoft.Data.SqlClient.SqlDataReader.GetDateTime(Int32 i)
   at lambda_method164(Closure , QueryContext , DbDataReader , ResultContext , SingleQueryResultCoordinator )
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2024-05-18T21:56:08.033' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (508, N'RepositoryBase`2 base repository find', N'Microsoft.Data.SqlClient', N'Data is Null. This method or property cannot be called on Null values.', N'', N'   at Microsoft.Data.SqlClient.SqlBuffer.get_DateTime()
   at Microsoft.Data.SqlClient.SqlDataReader.GetDateTime(Int32 i)
   at lambda_method164(Closure , QueryContext , DbDataReader , ResultContext , SingleQueryResultCoordinator )
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2024-05-18T21:56:08.133' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (509, N'RepositoryBase`2 base repository find', N'Microsoft.Data.SqlClient', N'Data is Null. This method or property cannot be called on Null values.', N'', N'   at Microsoft.Data.SqlClient.SqlBuffer.get_DateTime()
   at Microsoft.Data.SqlClient.SqlDataReader.GetDateTime(Int32 i)
   at lambda_method164(Closure , QueryContext , DbDataReader , ResultContext , SingleQueryResultCoordinator )
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2024-05-18T21:56:31.657' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (510, N'RepositoryBase`2 base repository find', N'Microsoft.Data.SqlClient', N'Data is Null. This method or property cannot be called on Null values.', N'', N'   at Microsoft.Data.SqlClient.SqlBuffer.get_DateTime()
   at Microsoft.Data.SqlClient.SqlDataReader.GetDateTime(Int32 i)
   at lambda_method164(Closure , QueryContext , DbDataReader , ResultContext , SingleQueryResultCoordinator )
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2024-05-18T21:56:48.970' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (511, N'RepositoryBase`2 base repository find', N'Microsoft.Data.SqlClient', N'Data is Null. This method or property cannot be called on Null values.', N'', N'   at Microsoft.Data.SqlClient.SqlBuffer.get_DateTime()
   at Microsoft.Data.SqlClient.SqlDataReader.GetDateTime(Int32 i)
   at lambda_method164(Closure , QueryContext , DbDataReader , ResultContext , SingleQueryResultCoordinator )
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2024-05-18T21:57:10.720' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (512, N'RepositoryBase`2 base repository find', N'Microsoft.Data.SqlClient', N'Data is Null. This method or property cannot be called on Null values.', N'', N'   at Microsoft.Data.SqlClient.SqlBuffer.get_DateTime()
   at Microsoft.Data.SqlClient.SqlDataReader.GetDateTime(Int32 i)
   at lambda_method164(Closure , QueryContext , DbDataReader , ResultContext , SingleQueryResultCoordinator )
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2024-05-18T21:57:18.550' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (513, N'RepositoryBase`2 base repository find', N'Microsoft.Data.SqlClient', N'Data is Null. This method or property cannot be called on Null values.', N'', N'   at Microsoft.Data.SqlClient.SqlBuffer.get_DateTime()
   at Microsoft.Data.SqlClient.SqlDataReader.GetDateTime(Int32 i)
   at lambda_method164(Closure , QueryContext , DbDataReader , ResultContext , SingleQueryResultCoordinator )
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2024-05-18T21:57:29.213' AS DateTime))
INSERT [dbo].[LogMessage] ([Id], [Message], [Source], [ErrorMessage], [InnerException], [StackTrace], [ObjectSource], [RecordDate]) VALUES (514, N'RepositoryBase`2 base repository find', N'Microsoft.Data.SqlClient', N'Data is Null. This method or property cannot be called on Null values.', N'', N'   at Microsoft.Data.SqlClient.SqlBuffer.get_DateTime()
   at Microsoft.Data.SqlClient.SqlDataReader.GetDateTime(Int32 i)
   at lambda_method164(Closure , QueryContext , DbDataReader , ResultContext , SingleQueryResultCoordinator )
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at Inta.EntityFramework.Core.Base.RepositoryBase`2.Find(Expression`1 filter) in D:\Repos\AspNetCoreRepository\Inta.Framework\Inta.EntityFramework.Core\Base\RepositoryBase.cs:line 108', N'', CAST(N'2024-05-18T21:57:33.387' AS DateTime))
SET IDENTITY_INSERT [dbo].[LogMessage] OFF
GO
SET IDENTITY_INSERT [dbo].[MessageHistory] ON 

INSERT [dbo].[MessageHistory] ([Id], [LanguageId], [MessageTypeId], [ClientName], [ClientSurname], [Email], [Phone], [Subject], [Explanation], [IsActive], [IsRead], [ArchiveDate], [IpNumber], [RecordDate]) VALUES (11, NULL, NULL, N'sd', N'ds', N'erdogankb57@gmail.com', NULL, N'deneme mesajıdır.', NULL, 1, 0, NULL, NULL, CAST(N'2024-05-28T21:30:35.793' AS DateTime))
INSERT [dbo].[MessageHistory] ([Id], [LanguageId], [MessageTypeId], [ClientName], [ClientSurname], [Email], [Phone], [Subject], [Explanation], [IsActive], [IsRead], [ArchiveDate], [IpNumber], [RecordDate]) VALUES (12, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 0, NULL, NULL, CAST(N'2024-06-04T23:49:18.253' AS DateTime))
INSERT [dbo].[MessageHistory] ([Id], [LanguageId], [MessageTypeId], [ClientName], [ClientSurname], [Email], [Phone], [Subject], [Explanation], [IsActive], [IsRead], [ArchiveDate], [IpNumber], [RecordDate]) VALUES (13, NULL, NULL, N'Erdoğan', N'KABA', N'erdogankb57@gmail.com', NULL, N'Deneme mesajıdır.', NULL, 1, 0, NULL, NULL, CAST(N'2024-06-24T01:41:42.797' AS DateTime))
SET IDENTITY_INSERT [dbo].[MessageHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[PageType] ON 

INSERT [dbo].[PageType] ([Id], [SystemUserId], [Code], [Name], [ControllerName], [ActionName], [ViewName], [IsExplanationEnabled], [IsShortExplanationEnabled], [IsMenuFirstRecord], [IsMenuFirstCategory], [IsPagingEnabled], [RecordDate], [IsActive]) VALUES (2, NULL, N'1', N'İçerik listesi sayfası', N'1', N'1', N'1', 0, 0, 0, 0, 0, NULL, 1)
INSERT [dbo].[PageType] ([Id], [SystemUserId], [Code], [Name], [ControllerName], [ActionName], [ViewName], [IsExplanationEnabled], [IsShortExplanationEnabled], [IsMenuFirstRecord], [IsMenuFirstCategory], [IsPagingEnabled], [RecordDate], [IsActive]) VALUES (3, NULL, N'CATLIST', N'Kategori Listeleme sayfası', N'Category', N'Index', N'Index', 1, 1, 0, 0, 0, NULL, 1)
INSERT [dbo].[PageType] ([Id], [SystemUserId], [Code], [Name], [ControllerName], [ActionName], [ViewName], [IsExplanationEnabled], [IsShortExplanationEnabled], [IsMenuFirstRecord], [IsMenuFirstCategory], [IsPagingEnabled], [RecordDate], [IsActive]) VALUES (4, NULL, N'test', N'1', N'1', N'1', N'1', 1, 1, 1, 1, 1, NULL, 1)
INSERT [dbo].[PageType] ([Id], [SystemUserId], [Code], [Name], [ControllerName], [ActionName], [ViewName], [IsExplanationEnabled], [IsShortExplanationEnabled], [IsMenuFirstRecord], [IsMenuFirstCategory], [IsPagingEnabled], [RecordDate], [IsActive]) VALUES (5, NULL, N'test', N'test', N'test', N'test', N'test', 1, 1, 1, 1, 1, NULL, 1)
INSERT [dbo].[PageType] ([Id], [SystemUserId], [Code], [Name], [ControllerName], [ActionName], [ViewName], [IsExplanationEnabled], [IsShortExplanationEnabled], [IsMenuFirstRecord], [IsMenuFirstCategory], [IsPagingEnabled], [RecordDate], [IsActive]) VALUES (6, 5, N'deneme', N'deneme', N'deneme', N'deneme', N'deneme', 0, 0, 0, 0, 0, NULL, 0)
SET IDENTITY_INSERT [dbo].[PageType] OFF
GO
SET IDENTITY_INSERT [dbo].[Record] ON 

INSERT [dbo].[Record] ([Id], [SystemUserId], [LanguageId], [BannerTypeId], [TargetId], [CategoryId], [Name], [RecordUrl], [Title], [MetaDescription], [MetaKeywords], [Url], [ShortContent], [Link], [TargetType], [ShortExplanation], [Explanation], [Image], [OrderNumber], [RecordDate], [IsActive]) VALUES (8099, 5, 4, NULL, 1, 71687, N'a', N'a', N'a', N'a', N'a', NULL, NULL, NULL, NULL, NULL, NULL, N'1-d76e024d-4c6c-4d17-99dd-e74415b9fefc.jpg', 0, CAST(N'2024-08-15T15:16:08.513' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Record] OFF
GO
SET IDENTITY_INSERT [dbo].[RecordFile] ON 

INSERT [dbo].[RecordFile] ([Id], [SystemUserId], [LanguageId], [RecordId], [Name], [ShortExplanation], [Explanation], [FileName], [FileTagName], [FileTitleName], [TargetId], [HomePageStatus], [OrderNumber], [RecordDate], [IsActive]) VALUES (8070, 5, NULL, NULL, N'asasa', NULL, NULL, NULL, NULL, NULL, 0, 0, 0, CAST(N'2024-06-25T14:30:05.250' AS DateTime), 1)
INSERT [dbo].[RecordFile] ([Id], [SystemUserId], [LanguageId], [RecordId], [Name], [ShortExplanation], [Explanation], [FileName], [FileTagName], [FileTitleName], [TargetId], [HomePageStatus], [OrderNumber], [RecordDate], [IsActive]) VALUES (8071, 5, NULL, 8093, N'aasas', NULL, NULL, NULL, NULL, NULL, 0, 0, 0, CAST(N'2024-06-25T14:45:29.737' AS DateTime), 1)
INSERT [dbo].[RecordFile] ([Id], [SystemUserId], [LanguageId], [RecordId], [Name], [ShortExplanation], [Explanation], [FileName], [FileTagName], [FileTitleName], [TargetId], [HomePageStatus], [OrderNumber], [RecordDate], [IsActive]) VALUES (8073, 5, NULL, 8094, N'sil dosya', NULL, NULL, N'download_-1-31de0df6-11e6-4348-90ea-d05a26a849e3.pdf', NULL, NULL, 0, 0, 0, CAST(N'2024-06-27T13:46:00.763' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[RecordFile] OFF
GO
SET IDENTITY_INSERT [dbo].[SEOIndex] ON 

INSERT [dbo].[SEOIndex] ([Id], [SystemUserId], [LanguageId], [Name], [Url], [RedirectUrl], [RecordDate], [IsActive]) VALUES (5, NULL, NULL, N'Anasayfa', N'Default.aspx', NULL, CAST(N'2024-05-28T21:29:53.543' AS DateTime), 1)
INSERT [dbo].[SEOIndex] ([Id], [SystemUserId], [LanguageId], [Name], [Url], [RedirectUrl], [RecordDate], [IsActive]) VALUES (6, NULL, 4, N'test', N'test', NULL, CAST(N'2024-06-04T22:03:21.033' AS DateTime), 1)
INSERT [dbo].[SEOIndex] ([Id], [SystemUserId], [LanguageId], [Name], [Url], [RedirectUrl], [RecordDate], [IsActive]) VALUES (8, 5, 4, N'test', N'test', N'test', CAST(N'2024-06-04T23:45:02.807' AS DateTime), 1)
INSERT [dbo].[SEOIndex] ([Id], [SystemUserId], [LanguageId], [Name], [Url], [RedirectUrl], [RecordDate], [IsActive]) VALUES (9, 5, 4, N'1', N'2', N'3', CAST(N'2024-06-24T01:42:06.463' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[SEOIndex] OFF
GO
SET IDENTITY_INSERT [dbo].[StaticText] ON 

INSERT [dbo].[StaticText] ([Id], [SystemUserId], [Name], [Explanation], [OrderNumber], [RecordDate], [IsActive]) VALUES (1, 1, N'1', N'1', 0, CAST(N'2021-10-12T18:31:48.407' AS DateTime), 1)
INSERT [dbo].[StaticText] ([Id], [SystemUserId], [Name], [Explanation], [OrderNumber], [RecordDate], [IsActive]) VALUES (2, 1, N'3333', N'2', 0, CAST(N'2022-05-09T14:24:10.673' AS DateTime), 1)
INSERT [dbo].[StaticText] ([Id], [SystemUserId], [Name], [Explanation], [OrderNumber], [RecordDate], [IsActive]) VALUES (6, 0, N'6', N'6', 6, CAST(N'2024-05-11T13:28:40.530' AS DateTime), 1)
INSERT [dbo].[StaticText] ([Id], [SystemUserId], [Name], [Explanation], [OrderNumber], [RecordDate], [IsActive]) VALUES (7, 0, N'7', N'7', 7, CAST(N'2024-05-11T13:29:01.393' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[StaticText] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemAction] ON 

INSERT [dbo].[SystemAction] ([Id], [SystemMenuId], [ControllerName], [ActionName], [Description]) VALUES (1, 1, N'Banner', N'Index', N'Banner Listeleme Ekranı')
INSERT [dbo].[SystemAction] ([Id], [SystemMenuId], [ControllerName], [ActionName], [Description]) VALUES (2, 9, N'SystemUser', N'Index', N'Kullanıcı tanımlaırı listeleme')
INSERT [dbo].[SystemAction] ([Id], [SystemMenuId], [ControllerName], [ActionName], [Description]) VALUES (3, 6, N'Category', N'Index', N'Kategori Listeleme')
INSERT [dbo].[SystemAction] ([Id], [SystemMenuId], [ControllerName], [ActionName], [Description]) VALUES (4, 2, N'Home', N'Index', N'Ana sayfa')
INSERT [dbo].[SystemAction] ([Id], [SystemMenuId], [ControllerName], [ActionName], [Description]) VALUES (5, 3, N'Home', N'Index', N'Listeleme sayfası')
INSERT [dbo].[SystemAction] ([Id], [SystemMenuId], [ControllerName], [ActionName], [Description]) VALUES (9, 2, N'BannerType', N'Index', N'Banner tip listeleeme')
INSERT [dbo].[SystemAction] ([Id], [SystemMenuId], [ControllerName], [ActionName], [Description]) VALUES (10, 2, N'BannerType', N'Add', N'Banner tip listeleeme')
INSERT [dbo].[SystemAction] ([Id], [SystemMenuId], [ControllerName], [ActionName], [Description]) VALUES (11, 6, N'Category', N'Add', N'Kategori ekleme sayfası')
SET IDENTITY_INSERT [dbo].[SystemAction] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemActionRole] ON 

INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1005, 1, -1)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1006, 1, 1)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1007, 1, 1)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1008, 1, 1)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1009, 1, 8)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1020, 5, 2)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1023, 5, 2)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1026, 5, 2)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1028, 1, 2)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1029, 2, 2)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1030, 3, 2)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1032, 9, 10)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1033, 10, 10)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1034, 3, 10)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1035, 11, 10)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1036, 1, 11)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1037, 2, 11)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1038, 3, 11)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1039, 4, 11)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1040, 5, 11)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1041, 9, 11)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1042, 10, 11)
INSERT [dbo].[SystemActionRole] ([Id], [SystemActionId], [SystemRoleId]) VALUES (1043, 11, 11)
SET IDENTITY_INSERT [dbo].[SystemActionRole] OFF
GO
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (1, 0, N'Ana sayfa', NULL, N'Ana sayfa', N'Home/Index', N'Home', N'Index', CAST(N'2021-10-12T17:02:05.533' AS DateTime), N'fas fa-bars', 1, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (2, 3, N'Banner Tipleri', NULL, N'Banner Tipleri', N'BannerType/Index', N'BannerType', N'Index', CAST(N'2021-10-12T17:03:05.573' AS DateTime), N'fa fa-adn', 1, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (3, 0, N'Banner Yönetimi', NULL, N'Ana sayfa', N'Home/Index', N'#', N'', CAST(N'2021-10-12T17:03:19.663' AS DateTime), N'fab fa-adversal', 1, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (4, 5, N'İletişim Bilgileri', NULL, N'İletişim Bilgileri', N'ContactInformation/Index', N'ContactInformation', N'Index', CAST(N'2021-10-12T17:22:39.807' AS DateTime), N'fa fa-map-marker', 1, 1, 5)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (5, 0, N'Menü ve İçerik Yönetimi', NULL, N'Ana sayfa', N'Home/Index', N'#', N'', CAST(N'2021-10-12T17:30:15.267' AS DateTime), N'fa fa-window-maximize', 1, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (6, 5, N'Kategori Yönetimi', NULL, N'Kategori Yönetimi', N'Category/Index', N'Category', N'Index', CAST(N'2021-10-12T17:30:42.737' AS DateTime), N'fa fa-list', 1, 1, 1)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (7, 5, N'İçerik Yönetimi', NULL, N'İçerik Yönetimi', N'Record/Index', N'Record', N'Index', CAST(N'2021-10-12T17:31:45.037' AS DateTime), N'fa fa-file-text-o', 1, 1, 3)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (8, 0, N'Kullanıcı Yönetimi', NULL, N'Ana sayfa', N'Home/Index', N'#', N'', CAST(N'2021-10-12T18:27:14.597' AS DateTime), N'fa fa-user-o', 1, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (9, 8, N'Kullanıcı Tanımları', NULL, N'Kullanıcı Tanımları', N'SystemUser/Index', N'SystemUser', N'Index', CAST(N'2021-10-12T18:27:44.707' AS DateTime), N'fa fa-user-circle-o', 1, 1, 1)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (10, 8, N'Kullanıcı Rolleri', NULL, N'Kullanıcı Rolleri', N'SystemRole/Index', N'SystemRole', N'Index', CAST(N'2021-10-12T18:28:32.493' AS DateTime), N'fa fa-user-circle', 1, 1, 0)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (11, 0, N'Form Yönetimi', NULL, N'Ana sayfa', N'Home/Index', N'#', N'', CAST(N'2021-10-12T18:29:10.437' AS DateTime), N'fa fa-check-square', 1, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (12, 11, N'Form Elemanları', NULL, N'Form Elemanları', N'FormElement/Index', N'FormElement', N'Index', CAST(N'2021-10-12T18:29:28.780' AS DateTime), N'far fa-bookmark', 1, 1, 1)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (13, 17, N'Sistem Sayfaları', NULL, N'Sistem Sayfaları', N'SystemAction/Index', N'SystemAction', N'Index', CAST(N'2021-10-12T18:30:26.260' AS DateTime), N'fa fa-file-text', 1, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (14, 17, N'Sayfa Türü Ayarları', NULL, N'Sayfa Türü Ayarları', N'PageType/Index', N'PageType', N'Index', CAST(N'2021-10-12T18:30:57.580' AS DateTime), N'fa fa-file-o', 1, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (15, 17, N'Statik İçerikler', NULL, N'Statik İçerikler', N'StaticText/Index', N'StaticText', N'Index', CAST(N'2021-10-12T18:31:20.073' AS DateTime), N'fa fa-file', 1, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (16, 17, N'Html Editör Tanımları', NULL, N'Html Editör Tanımları', N'EditorTemplate/Index', N'EditorTemplate', N'Index', CAST(N'2021-10-12T18:32:18.930' AS DateTime), N'fa fa-html5', 0, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (17, 0, N'Sistem Ayarları', NULL, N'Ana sayfa', N'Home/Index', N'#', N'', CAST(N'2021-10-14T14:59:13.170' AS DateTime), N'fa fa-dedent', 1, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (18, 11, N'Form Grupları', NULL, N'Form Grupları', N'FormGroup/Index', N'FormGroup', N'Index', CAST(N'2021-10-14T21:25:03.823' AS DateTime), N'fas fa-align-left', 1, 1, 0)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (19, 17, N'Genel Ayarlar', NULL, N'Genel Ayarlar', N'GeneralSettings/Index', N'GeneralSettings', N'Index', CAST(N'2021-10-17T14:47:51.007' AS DateTime), N'fas fa-clipboard-list', 1, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (20, 17, N'Firma Değişkenleri', NULL, N'Firma Değişkenleri', N'FirmVariables/Index', N'FirmVariables', N'Index', CAST(N'2021-10-17T19:13:09.463' AS DateTime), N'fa fa-feed', 1, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (21, 5, N'Seo Index', NULL, N'Seo Index', N'SeoIndex/Index', N'SeoIndex', N'Index', CAST(N'2021-10-18T22:20:11.367' AS DateTime), N'fab fa-google-plus', 1, 1, 7)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (22, 12, N'Form Seçenekleri', NULL, N'Form Seçenekleri', N'FormElementOptions/Index', N'FormElementOptions', N'Index', CAST(N'2021-10-19T16:01:53.077' AS DateTime), N'fas fa-align-justify', 1, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (23, 3, N'Banner Listesi', NULL, N'Banner Listesi', N'Banner/Index', N'Banner', N'Index', CAST(N'2021-10-19T16:04:53.883' AS DateTime), N'fa fa-list-alt', 1, 1, 1)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (24, 7, N'Dosya Ekle', NULL, N'Dosya Ekle', N'RecordFile/Index', N'RecordFile', N'Index', CAST(N'2021-10-21T21:20:10.160' AS DateTime), NULL, 0, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (25, 7, N'Resim Ekle', NULL, N'Resim Ekle', N'RecordImage/Index', N'RecordImage', N'Index', CAST(N'2021-10-21T21:21:40.470' AS DateTime), NULL, 0, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (26, 5, N'Mesajlarım', NULL, N'Mesajlarım', N'MessageHistory/Index', N'MessageHistory', N'Index', CAST(N'2021-10-21T21:22:38.947' AS DateTime), N'far fa-comment-alt', 1, 1, 6)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (27, 5, N'Kategori Ekleme', NULL, N'Kategori Ekleme', N'Category/Add', N'Category', N'Add', CAST(N'2022-04-04T00:00:00.000' AS DateTime), N'far fa-comment-alt', 1, 1, 0)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (28, 5, N'İçerik Ekleme', NULL, N'İçerik Ekleme', N'Record/Add', N'Record', N'Add', CAST(N'2022-04-04T00:00:00.000' AS DateTime), N'fas fa-align-left', 1, 1, 2)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (29, 16, N'Editor Tanımı Ekle', NULL, N'Editor Tanımı Ekle', N'EditorTemplate/Add', N'EditorTemplate', N'Add', CAST(N'2022-05-09T00:00:00.000' AS DateTime), NULL, 0, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (30, 8, N'Hesabım', NULL, N'Hesabım', N'Account/Index', N'Account', N'Index', CAST(N'2021-10-12T18:28:32.493' AS DateTime), N'fa fa-user-circle', 1, 1, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (31, 3, N'Banner Ekle', NULL, N'Banner Ekle', N'Banner/Add', N'Banner', N'Add', CAST(N'2021-10-19T16:04:53.883' AS DateTime), N'fa fa-list-alt', 1, 1, 0)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (32, 0, N'Resim düzenle', NULL, N'Resim düzenle', N'ImageCrop/Index', N'ImageCrop', N'Index', CAST(N'2021-10-12T17:02:05.533' AS DateTime), N'fas fa-bars', 1, 0, NULL)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (33, 5, N'İletişim Bilgisi Ekle', NULL, N'İletişim Bilgisi Ekle', N'ContactInformation/Add', N'ContactInformation', N'Add', CAST(N'2021-10-12T17:22:39.807' AS DateTime), N'fa fa-map-marker', 1, 1, 4)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (34, 0, N'İşlem Başarılı', NULL, N'İşlem Başarılı', N'Message/Success', N'Message', N'Success', CAST(N'2021-10-12T17:02:05.533' AS DateTime), N'fas fa-bars', 1, 0, 0)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (35, 0, N'İşlem Başarısız', NULL, N'İşlem Başarısız', N'Message/Error', N'Message', N'Error', CAST(N'2021-10-12T17:02:05.533' AS DateTime), N'fas fa-bars', 1, 0, 0)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (36, 3, N'Banner Kaydet', NULL, N'Banner Ekle', N'Banner/Add', N'Banner', N'Save', CAST(N'2021-10-19T16:04:53.883' AS DateTime), N'fa fa-list-alt', 1, 0, 0)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (37, 5, N'Kategori Kaydet', NULL, N'Kategori Ekle', N'Category/Add', N'Category', N'Save', CAST(N'2022-04-04T00:00:00.000' AS DateTime), N'far fa-comment-alt', 1, 0, 0)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (38, 5, N'İçerik Kaydet', NULL, N'İçerik Ekle', N'Record/Add', N'Record', N'Save', CAST(N'2022-04-04T00:00:00.000' AS DateTime), N'fas fa-align-left', 1, 0, 2)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (39, 5, N'İletişim Bilgisi Kaydet', NULL, N'İletişim Bilgisi Ekle', N'ContactInformation/Add', N'ContactInformation', N'Save', CAST(N'2021-10-12T17:22:39.807' AS DateTime), N'fa fa-map-marker', 1, 0, 4)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (40, 17, N'Genel Ayarlar Kaydet', NULL, N'Genel Ayarlar Ekle', N'GeneralSettings/Add', N'GeneralSettings', N'Save', CAST(N'2021-10-17T14:47:51.007' AS DateTime), N'fas fa-clipboard-list', 1, 0, 0)
INSERT [dbo].[SystemMenu] ([Id], [SystemMenuId], [Name], [Explanation], [BreadCrumpName], [BreadCrumpUrl], [ControllerName], [ActionName], [RecordDate], [MenuIcon], [IsActive], [ShowInMenu], [OrderNumber]) VALUES (41, 8, N'Kullanıcı Kaydet', NULL, N'Kullanıcı Tanımları', N'SystemUser/Add', N'SystemUser', N'Add', CAST(N'2021-10-12T18:27:44.707' AS DateTime), N'fa fa-user-circle-o', 1, 0, 1)
GO
SET IDENTITY_INSERT [dbo].[SystemMenuRole] ON 

INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (27, -1, 1, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (28, 1, 1, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (29, 1, 1, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (30, 1, 1, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (31, 8, 1, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (33, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (37, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (40, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (43, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (46, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (49, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (52, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (55, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (57, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (59, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (61, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (63, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (65, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (67, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (69, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (71, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (73, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (75, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (77, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (79, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (81, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (83, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (85, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (87, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (89, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (91, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (93, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (95, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (96, 2, 2, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (97, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (98, 2, 4, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (99, 2, 2, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (100, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (101, 2, 4, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (102, 2, 2, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (103, 2, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (104, 2, 4, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (105, 2, 5, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (106, 2, 6, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (107, 2, 7, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (109, 10, 2, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (110, 10, 2, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (111, 10, 2, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (112, 10, 2, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (113, 11, 1, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (114, 11, 2, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (115, 11, 3, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (116, 11, 4, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (117, 11, 5, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (118, 11, 6, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (119, 11, 7, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (120, 11, 8, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (121, 11, 9, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (122, 11, 10, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (123, 11, 11, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (124, 11, 12, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (125, 11, 13, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (126, 11, 14, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (127, 11, 15, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (128, 11, 16, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (129, 11, 17, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (130, 11, 18, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (131, 11, 19, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (132, 11, 20, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (133, 11, 21, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (134, 11, 22, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (135, 11, 23, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (136, 11, 24, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (137, 11, 25, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (138, 11, 26, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (139, 11, 27, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (140, 11, 28, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (141, 11, 29, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (142, 11, 30, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (143, 11, 31, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (144, 11, 32, NULL)
INSERT [dbo].[SystemMenuRole] ([Id], [SystemRoleId], [SystemMenuId], [RecordDate]) VALUES (145, 11, 33, NULL)
SET IDENTITY_INSERT [dbo].[SystemMenuRole] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemRole] ON 

INSERT [dbo].[SystemRole] ([Id], [Name], [Explanation], [RecordDate], [IsActive]) VALUES (2, N'Test', N's', CAST(N'2024-05-18T20:29:35.573' AS DateTime), 1)
INSERT [dbo].[SystemRole] ([Id], [Name], [Explanation], [RecordDate], [IsActive]) VALUES (10, N'Banner Tip', N'Banner Tip', CAST(N'2024-06-23T22:48:20.290' AS DateTime), 1)
INSERT [dbo].[SystemRole] ([Id], [Name], [Explanation], [RecordDate], [IsActive]) VALUES (11, N'Test rolü', N'Test rolü', CAST(N'2024-06-24T01:42:59.573' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[SystemRole] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemUser] ON 

INSERT [dbo].[SystemUser] ([Id], [SystemUserId], [SystemRoleId], [Name], [SurName], [UserName], [Password], [Email], [Phone], [Address], [Image], [RecordDate], [IsActive], [IsAdmin]) VALUES (5, 1, 2, N'Erdoğan', N'Kaba', N'erdogankb57@gmail.com', N'123456', N'erdogankb57@gmail.com', N'2', N'2', NULL, CAST(N'2024-05-16T20:00:01.827' AS DateTime), 1, 1)
INSERT [dbo].[SystemUser] ([Id], [SystemUserId], [SystemRoleId], [Name], [SurName], [UserName], [Password], [Email], [Phone], [Address], [Image], [RecordDate], [IsActive], [IsAdmin]) VALUES (6, 1, 10, N'Admin', N'Admin', N'admin', N'admin', N'gh', N'gh', N'gh', NULL, CAST(N'2024-05-18T20:28:26.127' AS DateTime), 1, 0)
INSERT [dbo].[SystemUser] ([Id], [SystemUserId], [SystemRoleId], [Name], [SurName], [UserName], [Password], [Email], [Phone], [Address], [Image], [RecordDate], [IsActive], [IsAdmin]) VALUES (7, NULL, 11, N'Erdoğan', N'KABA', N'KABA', N'123456', N'erdogankb57@gmail.com', N'05318479796', N'Bağcan sok', N'1-d1990989-19c8-4a15-9334-9285ae4f05cd.jpg', CAST(N'2024-06-24T01:44:06.720' AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[SystemUser] OFF
GO
ALTER TABLE [dbo].[Banner] ADD  CONSTRAINT [DF_Banner_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_CanBeDeleted]  DEFAULT ((1)) FOR [CanBeDeleted]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_CanSubCategoryBeAdded]  DEFAULT ((1)) FOR [CanSubCategoryBeAdded]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_OrderNumber]  DEFAULT ((0)) FOR [OrderNumber]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[CategorySpecialty] ADD  CONSTRAINT [DF_CategorySpecialty_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[EditorTemplate] ADD  CONSTRAINT [DF_EditorTemplate_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[FirmVariables] ADD  CONSTRAINT [DF_FirmVariables_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[FormElement] ADD  CONSTRAINT [DF_FormElement_CategoryId]  DEFAULT ((0)) FOR [CategoryId]
GO
ALTER TABLE [dbo].[FormElement] ADD  CONSTRAINT [DF_FormElement_FormGroupId]  DEFAULT ((0)) FOR [FormGroupId]
GO
ALTER TABLE [dbo].[FormElement] ADD  CONSTRAINT [DF_FormElement_OrderNumber]  DEFAULT ((0)) FOR [OrderNumber]
GO
ALTER TABLE [dbo].[FormElement] ADD  CONSTRAINT [DF_FormElement_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[FormElement] ADD  CONSTRAINT [DF_FormElement_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[FormElementOptions] ADD  CONSTRAINT [DF_FormOptions_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[FormGroup] ADD  CONSTRAINT [DF_FormGroup_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[FormType] ADD  CONSTRAINT [DF_FormType_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[Language] ADD  CONSTRAINT [DF_Language_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[LogMessage] ADD  CONSTRAINT [DF_LogMessage_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[MessageHistory] ADD  CONSTRAINT [DF_MessageHistory_IsArchive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[MessageHistory] ADD  CONSTRAINT [DF_MessageHistory_IsRead]  DEFAULT ((0)) FOR [IsRead]
GO
ALTER TABLE [dbo].[MessageHistory] ADD  CONSTRAINT [DF_MessageHistory_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[MessageType] ADD  CONSTRAINT [DF_MessageType_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[Record] ADD  CONSTRAINT [DF_Content_OrderNumber]  DEFAULT ((0)) FOR [OrderNumber]
GO
ALTER TABLE [dbo].[Record] ADD  CONSTRAINT [DF_Content_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[RecordFile] ADD  CONSTRAINT [DF_ContentFile_OrderNumber]  DEFAULT ((0)) FOR [OrderNumber]
GO
ALTER TABLE [dbo].[RecordFile] ADD  CONSTRAINT [DF_ContentFile_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[RecordFile] ADD  CONSTRAINT [DF_ContentFile_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[RecordImage] ADD  CONSTRAINT [DF_ContentImage_OrderNumber]  DEFAULT ((0)) FOR [OrderNumber]
GO
ALTER TABLE [dbo].[RecordImage] ADD  CONSTRAINT [DF_ContentImage_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[RecordImage] ADD  CONSTRAINT [DF_ContentImage_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[RecordSpecialty] ADD  CONSTRAINT [DF_ContentSpecialty_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[SEOIndex] ADD  CONSTRAINT [DF_SEOIndex_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[StaticText] ADD  CONSTRAINT [DF_StaticText_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[SystemMenu] ADD  CONSTRAINT [DF_SystemMenu_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[SystemMenu] ADD  CONSTRAINT [DF_SystemMenu_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SystemMenu] ADD  CONSTRAINT [DF_SystemMenu_ShowInMenu]  DEFAULT ((1)) FOR [ShowInMenu]
GO
ALTER TABLE [dbo].[SystemMenu] ADD  CONSTRAINT [DF_SystemMenu_OrderNumber]  DEFAULT ((0)) FOR [OrderNumber]
GO
ALTER TABLE [dbo].[SystemRole] ADD  CONSTRAINT [DF_SystemRole_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[SystemRole] ADD  CONSTRAINT [DF_SystemRole_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SystemUser] ADD  CONSTRAINT [DF_SystemUser_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[SystemUser] ADD  CONSTRAINT [DF_SystemUser_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SystemUserRole] ADD  CONSTRAINT [DF_SystemUserRole_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
USE [master]
GO
ALTER DATABASE [IntaKurumsal] SET  READ_WRITE 
GO
