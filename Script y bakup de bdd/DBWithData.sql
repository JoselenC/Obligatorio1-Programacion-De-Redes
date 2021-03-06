USE [BluedditDB]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 20/6/2021 17:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TimeOfConnection] [nvarchar](max) NULL,
	[Ip] [nvarchar](max) NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Files]    Script Date: 20/6/2021 17:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Size] [float] NOT NULL,
	[UploadDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FileThemeDto]    Script Date: 20/6/2021 17:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileThemeDto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NOT NULL,
	[ThemeId] [int] NOT NULL,
 CONSTRAINT [PK_FileThemeDto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 20/6/2021 17:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[CreationDate] [nvarchar](max) NULL,
	[ObjectName] [nvarchar](max) NULL,
	[ObjectType] [nvarchar](max) NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 20/6/2021 17:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[CreationDate] [nvarchar](max) NULL,
	[FileId] [int] NOT NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostThemeDto]    Script Date: 20/6/2021 17:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostThemeDto](
	[PostId] [int] NOT NULL,
	[ThemeId] [int] NOT NULL,
	[Id] [int] NOT NULL,
 CONSTRAINT [PK_PostThemeDto] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC,
	[ThemeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Themes]    Script Date: 20/6/2021 17:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Themes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Themes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([Id], [TimeOfConnection], [Ip]) VALUES (3, N'20/6/2021 1:41:27', N'127.0.0.1:6000')
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
INSERT [dbo].[Files] ([Id], [Name], [Size], [UploadDate]) VALUES (5, N'Batman.jpg', 65969, CAST(N'2021-06-20T13:07:09.8670383' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[FileThemeDto] ON 

INSERT [dbo].[FileThemeDto] ([Id], [FileId], [ThemeId]) VALUES (12, 5, 10)
SET IDENTITY_INSERT [dbo].[FileThemeDto] OFF
GO
SET IDENTITY_INSERT [dbo].[Logs] ON 

INSERT [dbo].[Logs] ([Id], [Message], [CreationDate], [ObjectName], [ObjectType]) VALUES (112, N'Theme Redes Sociales was added', N'20/6/2021', N'Redes Sociales', N'theme')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate], [ObjectName], [ObjectType]) VALUES (113, N'Theme post about Redes Sociales was added', N'20/6/2021', N'post about Redes Sociales', N'theme')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate], [ObjectName], [ObjectType]) VALUES (114, N'Post post about Redes Sociales was added', N'20/6/2021', N'post about Redes Sociales', N'post')
SET IDENTITY_INSERT [dbo].[Logs] OFF
GO
SET IDENTITY_INSERT [dbo].[Posts] ON 

INSERT [dbo].[Posts] ([Id], [Name], [CreationDate], [FileId]) VALUES (2, N'Post about movies and games', N'12/12/2001', 0)
INSERT [dbo].[Posts] ([Id], [Name], [CreationDate], [FileId]) VALUES (5, N'Post about movies', N'12/12/2000', 5)
INSERT [dbo].[Posts] ([Id], [Name], [CreationDate], [FileId]) VALUES (6, N'Post about games', N'12/12/2000', 0)
INSERT [dbo].[Posts] ([Id], [Name], [CreationDate], [FileId]) VALUES (15, N'post prubea', N'12/12/2000', 0)
INSERT [dbo].[Posts] ([Id], [Name], [CreationDate], [FileId]) VALUES (16, N'post prubea2', N'12/12/2000', 0)
INSERT [dbo].[Posts] ([Id], [Name], [CreationDate], [FileId]) VALUES (17, N'post about Redes Sociales', N'11/06/2020', 0)
SET IDENTITY_INSERT [dbo].[Posts] OFF
GO
INSERT [dbo].[PostThemeDto] ([PostId], [ThemeId], [Id]) VALUES (2, 8, 0)
INSERT [dbo].[PostThemeDto] ([PostId], [ThemeId], [Id]) VALUES (5, 10, 0)
INSERT [dbo].[PostThemeDto] ([PostId], [ThemeId], [Id]) VALUES (6, 8, 0)
INSERT [dbo].[PostThemeDto] ([PostId], [ThemeId], [Id]) VALUES (17, 22, 0)
GO
SET IDENTITY_INSERT [dbo].[Themes] ON 

INSERT [dbo].[Themes] ([Id], [Name], [Description]) VALUES (8, N'Games', N'Theme games')
INSERT [dbo].[Themes] ([Id], [Name], [Description]) VALUES (9, N'Musics', N'Theme musics')
INSERT [dbo].[Themes] ([Id], [Name], [Description]) VALUES (10, N'Movies', N'Theme movies')
INSERT [dbo].[Themes] ([Id], [Name], [Description]) VALUES (22, N'Redes Sociales', N'citicas a redes sociales')
INSERT [dbo].[Themes] ([Id], [Name], [Description]) VALUES (23, N'post about Redes Sociales', N'')
SET IDENTITY_INSERT [dbo].[Themes] OFF
GO
ALTER TABLE [dbo].[Files]  WITH CHECK ADD  CONSTRAINT [FK_Files_Posts_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[Posts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Files] CHECK CONSTRAINT [FK_Files_Posts_Id]
GO
ALTER TABLE [dbo].[FileThemeDto]  WITH CHECK ADD  CONSTRAINT [FK_FileThemeDto_Files_FileId] FOREIGN KEY([FileId])
REFERENCES [dbo].[Files] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FileThemeDto] CHECK CONSTRAINT [FK_FileThemeDto_Files_FileId]
GO
ALTER TABLE [dbo].[FileThemeDto]  WITH CHECK ADD  CONSTRAINT [FK_FileThemeDto_Themes_ThemeId] FOREIGN KEY([ThemeId])
REFERENCES [dbo].[Themes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FileThemeDto] CHECK CONSTRAINT [FK_FileThemeDto_Themes_ThemeId]
GO
ALTER TABLE [dbo].[PostThemeDto]  WITH CHECK ADD  CONSTRAINT [FK_PostThemeDto_Posts_PostId] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PostThemeDto] CHECK CONSTRAINT [FK_PostThemeDto_Posts_PostId]
GO
ALTER TABLE [dbo].[PostThemeDto]  WITH CHECK ADD  CONSTRAINT [FK_PostThemeDto_Themes_ThemeId] FOREIGN KEY([ThemeId])
REFERENCES [dbo].[Themes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PostThemeDto] CHECK CONSTRAINT [FK_PostThemeDto_Themes_ThemeId]
GO
