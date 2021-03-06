IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'trainop')
CREATE USER [trainop] FOR LOGIN [trainop] WITH DEFAULT_SCHEMA=[dbo]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_Settings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_Settings](
	[SettingName] [nvarchar](50) NOT NULL,
	[SettingValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_be_Settings] PRIMARY KEY CLUSTERED 
(
	[SettingName] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_Excellent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_Excellent](
	[ExcellentID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Author] [nvarchar](25) NOT NULL,
	[CityTown] [nvarchar](50) NOT NULL,
	[Teacher] [nvarchar](100) NOT NULL,
	[TrainingDate] [datetime] NOT NULL,
	[MastPic] [uniqueidentifier] NULL,
	[IsPublished] [bit] NULL,
 CONSTRAINT [PK_Excellent] PRIMARY KEY CLUSTERED 
(
	[ExcellentID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_Profiles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_Profiles](
	[ProfileID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NULL,
	[SettingName] [nvarchar](200) NULL,
	[SettingValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_be_Profiles] PRIMARY KEY CLUSTERED 
(
	[ProfileID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[be_Profiles]') AND name = N'I_UserName')
CREATE NONCLUSTERED INDEX [I_UserName] ON [dbo].[be_Profiles] 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_StopWords]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_StopWords](
	[StopWord] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_be_StopWords] PRIMARY KEY CLUSTERED 
(
	[StopWord] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_Comments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_Comments](
	[CommentID] [uniqueidentifier] NOT NULL,
	[ParentID] [uniqueidentifier] NOT NULL,
	[ParentType] [int] NOT NULL,
	[CommentDate] [datetime] NOT NULL,
	[Author] [nvarchar](25) NOT NULL,
	[Count] [int] NOT NULL,
	[Sex] [bit] NULL,
	[Phone] [nvarchar](20) NULL,
	[Mobile] [nvarchar](11) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Company] [nvarchar](100) NULL,
	[QQ_msn] [nvarchar](100) NULL,
	[Comment] [nvarchar](max) NULL,
	[IsDispose] [bit] NOT NULL,
	[DisposeBy] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[IP] [nvarchar](50) NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'be_Comments', N'COLUMN',N'ParentType'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:公开课1:内训:' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'be_Comments', @level2type=N'COLUMN',@level2name=N'ParentType'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'be_Comments', N'COLUMN',N'Count'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'人数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'be_Comments', @level2type=N'COLUMN',@level2name=N'Count'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_DownFile]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_DownFile](
	[DownFileID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[Author] [nvarchar](25) NOT NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[Points] [int] NOT NULL,
 CONSTRAINT [PK_be_DownFile] PRIMARY KEY CLUSTERED 
(
	[DownFileID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_Res]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_Res](
	[ResID] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](150) NOT NULL,
	[ResType] [nvarchar](50) NULL,
	[Description] [nvarchar](250) NOT NULL,
	[Blob] [image] NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[ResID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_Fields]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_Fields](
	[FieldID] [uniqueidentifier] NOT NULL,
	[FieldName] [nvarchar](50) NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_be_Fields] PRIMARY KEY CLUSTERED 
(
	[FieldID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'be_Fields', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'领域' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'be_Fields'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_CurriculaCategorie]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_CurriculaCategorie](
	[CurriculaID] [uniqueidentifier] NOT NULL,
	[CategorieID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_be_Categorie] PRIMARY KEY CLUSTERED 
(
	[CurriculaID] ASC,
	[CategorieID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_CurriculaField]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_CurriculaField](
	[CurriculaID] [uniqueidentifier] NOT NULL,
	[FieldID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_be_CurriculaField] PRIMARY KEY CLUSTERED 
(
	[CurriculaID] ASC,
	[FieldID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_TrainingCategorie]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_TrainingCategorie](
	[TrainingID] [uniqueidentifier] NOT NULL,
	[CategorieID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_be_TrainingCategorie] PRIMARY KEY CLUSTERED 
(
	[TrainingID] ASC,
	[CategorieID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_TrainingField]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_TrainingField](
	[TrainingID] [uniqueidentifier] NOT NULL,
	[FieldID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_be_TrainingField] PRIMARY KEY CLUSTERED 
(
	[TrainingID] ASC,
	[FieldID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_UserRes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_UserRes](
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_be_UserRes] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_Tags]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_Tags](
	[TagID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [uniqueidentifier] NULL,
	[ParentType] [int] NULL,
	[Tag] [nvarchar](50) NULL,
 CONSTRAINT [PK_be_Tags] PRIMARY KEY CLUSTERED 
(
	[TagID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_CurriculaInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_CurriculaInfo](
	[InfoID] [uniqueidentifier] NOT NULL,
	[CurriculaID] [uniqueidentifier] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[Cast] [int] NOT NULL,
	[CityTown] [nvarchar](50) NOT NULL,
	[IsPublished] [bit] NULL,
 CONSTRAINT [PK_be_CurriculaInfo] PRIMARY KEY CLUSTERED 
(
	[InfoID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'be_CurriculaInfo', N'COLUMN',N'Cast'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'be_CurriculaInfo', @level2type=N'COLUMN',@level2name=N'Cast'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_ExcellentRes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_ExcellentRes](
	[ExcellentID] [uniqueidentifier] NOT NULL,
	[ResID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_be_ExcellentRes] PRIMARY KEY CLUSTERED 
(
	[ExcellentID] ASC,
	[ResID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_Categories]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_Categories](
	[CategoryID] [uniqueidentifier] ROWGUIDCOL  NOT NULL CONSTRAINT [DF_be_Categories_CategoryID]  DEFAULT (newid()),
	[CategoryName] [nvarchar](50) NULL,
	[Description] [nvarchar](200) NULL,
	[ParentID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_be_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'be_Categories', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'be_Categories'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_DownFileRes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_DownFileRes](
	[DownFileID] [uniqueidentifier] NOT NULL,
	[ResID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_be_DownFileRes] PRIMARY KEY CLUSTERED 
(
	[DownFileID] ASC,
	[ResID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_DataStoreSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_DataStoreSettings](
	[ExtensionType] [nvarchar](50) NOT NULL,
	[ExtensionId] [nvarchar](100) NOT NULL,
	[Settings] [nvarchar](max) NOT NULL
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[be_DataStoreSettings]') AND name = N'I_TypeID')
CREATE NONCLUSTERED INDEX [I_TypeID] ON [dbo].[be_DataStoreSettings] 
(
	[ExtensionType] ASC,
	[ExtensionId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_Curriculas]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_Curriculas](
	[CurriculaID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[ObjectDes] [nvarchar](max) NOT NULL,
	[CurriculaContent] [nvarchar](max) NOT NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[Author] [nvarchar](25) NULL,
	[IsPublished] [bit] NOT NULL,
	[Points] [int] NOT NULL,
	[Scores] [int] NOT NULL,
	[ViewCount] [int] NOT NULL,
	[IsGold] [bit] NOT NULL,
 CONSTRAINT [PK_be_Curriculas] PRIMARY KEY CLUSTERED 
(
	[CurriculaID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'be_Curriculas', N'COLUMN',N'ObjectDes'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授课对象' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'be_Curriculas', @level2type=N'COLUMN',@level2name=N'ObjectDes'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'be_Curriculas', N'COLUMN',N'Author'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'讲师' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'be_Curriculas', @level2type=N'COLUMN',@level2name=N'Author'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'be_Curriculas', N'COLUMN',N'IsPublished'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否发布' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'be_Curriculas', @level2type=N'COLUMN',@level2name=N'IsPublished'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'be_Curriculas', N'COLUMN',N'Points'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'积分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'be_Curriculas', @level2type=N'COLUMN',@level2name=N'Points'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'be_Curriculas', N'COLUMN',N'Scores'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'培训币' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'be_Curriculas', @level2type=N'COLUMN',@level2name=N'Scores'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'be_Curriculas', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公开课' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'be_Curriculas'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_Trainings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_Trainings](
	[TrainingID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[TrainingContent] [nvarchar](max) NOT NULL,
	[Days] [int] NOT NULL,
	[Teacher] [nvarchar](100) NOT NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[Author] [nvarchar](250) NOT NULL,
	[IsPublished] [bit] NOT NULL,
	[ViewCount] [int] NULL,
 CONSTRAINT [PK_Training] PRIMARY KEY CLUSTERED 
(
	[TrainingID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[LastLoginTime] [datetime] NULL,
	[EmailAddress] [nvarchar](100) NULL,
 CONSTRAINT [PK_be_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[Role] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_be_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_UserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[be_UserRoles](
	[UserRoleID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_be_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserRoleID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[be_UploadFilePrd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[be_UploadFilePrd] 
@ResId uniqueidentifier,
@Blob image,
@ResType nvarchar(50)
AS 
update be_Res set Blob = @Blob,ResType = @ResType where ResId = @ResId


' 
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_be_UserRoles_be_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[be_UserRoles]'))
ALTER TABLE [dbo].[be_UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_be_UserRoles_be_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[be_Roles] ([RoleID])
GO
ALTER TABLE [dbo].[be_UserRoles] CHECK CONSTRAINT [FK_be_UserRoles_be_Roles]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_be_UserRoles_be_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[be_UserRoles]'))
ALTER TABLE [dbo].[be_UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_be_UserRoles_be_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[be_Users] ([UserID])
GO
ALTER TABLE [dbo].[be_UserRoles] CHECK CONSTRAINT [FK_be_UserRoles_be_Users]
