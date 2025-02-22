USE [master]
GO
/****** Object:  Database [IntaKurumsal]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[Banner]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[BannerType]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[CategorySpecialty]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[ContactInformation]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[EditorImages]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[EditorTemplate]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[FirmVariables]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[FormElement]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[FormElementOptions]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[FormGroup]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[FormType]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[GeneralSettings]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[Language]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[LogMessage]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[MessageHistory]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[MessageType]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[PageType]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[Record]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[RecordFile]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[RecordImage]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[RecordSpecialty]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[SEOIndex]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[SpecialtyType]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[StaticText]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[SystemAction]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[SystemActionRole]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[SystemMenu]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[SystemMenuRole]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[SystemRole]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[SystemUser]    Script Date: 25/10/2024 15:18:37 ******/
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
/****** Object:  Table [dbo].[SystemUserRole]    Script Date: 25/10/2024 15:18:37 ******/
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
