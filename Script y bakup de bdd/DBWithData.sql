USE [BluedditDB]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 20/6/2021 13:12:38 ******/
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
/****** Object:  Table [dbo].[Files]    Script Date: 20/6/2021 13:12:38 ******/
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
/****** Object:  Table [dbo].[FileThemeDto]    Script Date: 20/6/2021 13:12:38 ******/
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
/****** Object:  Table [dbo].[Logs]    Script Date: 20/6/2021 13:12:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[CreationDate] [nvarchar](max) NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 20/6/2021 13:12:38 ******/
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
/****** Object:  Table [dbo].[PostThemeDto]    Script Date: 20/6/2021 13:12:38 ******/
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
/****** Object:  Table [dbo].[Themes]    Script Date: 20/6/2021 13:12:38 ******/
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

INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (4, N'The theme Music was added', N'20/6/2021 1:18:20')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (5, N'The theme Movies was added', N'20/6/2021 1:18:59')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (6, N'The theme Games was added', N'20/6/2021 1:19:08')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (7, N'The theme Music was modified', N'20/6/2021 1:19:47')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (8, N'The theme Musics was deleted', N'20/6/2021 1:20:09')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (9, N'The theme Musics was added', N'20/6/2021 1:20:18')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (10, N'The post Post about music was created', N'20/6/2021 1:20:50')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (11, N'The theme Musics was associated', N'20/6/2021 1:20:53')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (12, N'Modify post: Post about music new name: Post about musics new creation date: 12/12/2020', N'20/6/2021 1:21:58')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (13, N'The post Post about movies and games was created', N'20/6/2021 1:22:33')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (14, N'The theme Games was associated', N'20/6/2021 1:22:37')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (15, N'Not associated, the theme Games already associated', N'20/6/2021 1:22:44')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (16, N'The theme Movies was associated', N'20/6/2021 1:23:09')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (17, N'The theme Movies was disassociate and Musics was associate', N'20/6/2021 1:24:06')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (18, N'The file Batman.jpg was uploaded in the post Post about musics', N'20/6/2021 1:25:02')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (19, N'Search post: Post about musics', N'20/6/2021 1:26:58')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (20, N'The file Batman.jpg was uploaded in the post Post about movies and games', N'20/6/2021 1:29:37')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (21, N'The file Batman.jpg was uploaded in the post Post about musics', N'20/6/2021 1:41:49')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (22, N'The file Batman.jpg was uploaded in the post Post about musics', N'20/6/2021 1:45:39')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (23, N'The file Batman.jpg was uploaded in the post Post about musics', N'20/6/2021 2:16:54')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (24, N'The file Batman.jpg was uploaded in the post Post about musics', N'20/6/2021 2:16:59')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (25, N'The file Batman.jpg was uploaded in the post Post about movies and games', N'20/6/2021 2:16:59')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (26, N'The file Batman.jpg was uploaded in the post Post about movies and games', N'20/6/2021 2:16:59')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (27, N'The file Batman.jpg was uploaded in the post Post about movies and games', N'20/6/2021 2:18:58')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (28, N'The file Batman.jpg was uploaded in the post Post about musics', N'20/6/2021 2:22:07')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (29, N'The file Batman.jpg was uploaded in the post Post about musics', N'20/6/2021 2:28:02')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (30, N'The file Batman.jpg was uploaded in the post Post about musics', N'20/6/2021 2:30:32')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (31, N'The file Batman.jpg was uploaded in the post Post about movies and games', N'20/6/2021 2:30:51')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (32, N'Post  wasn''t added, invalid empty name', N'20/6/2021 2:33:58')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (33, N'Post Post about movies not exist', N'20/6/2021 2:34:20')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (34, N'Post Post about movies not exist', N'20/6/2021 2:37:36')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (35, N'Post Post about movies was added', N'20/6/2021 2:44:26')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (36, N'Post Post about movies wasn''t added, already exist', N'20/6/2021 2:46:37')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (37, N'Post Post about games was added', N'20/6/2021 2:47:32')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (38, N'Post Post about games wasn''t added, already exist', N'20/6/2021 2:50:41')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (39, N'Theme Theme was added', N'20/6/2021 9:36:52')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (40, N'Theme Post was added', N'20/6/2021 9:38:00')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (41, N'Post Post was added', N'20/6/2021 9:38:19')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (42, N'Theme them2 was added', N'20/6/2021 9:40:20')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (43, N'Theme post prueba was added', N'20/6/2021 9:40:50')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (44, N'Post post prueba was added', N'20/6/2021 9:41:13')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (45, N'Post post prueba2 was added', N'20/6/2021 9:43:46')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (46, N'Post post prueba2 wasn''t added, already exist', N'20/6/2021 9:43:51')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (47, N'Post post prueba3 was added', N'20/6/2021 9:44:31')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (48, N'Post post prueba5 was added', N'20/6/2021 10:18:41')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (49, N'Theme post prueba5 not exist', N'20/6/2021 10:18:45')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (50, N'Post  wasn''t added, invalid empty name', N'20/6/2021 10:18:45')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (51, N'Post  wasn''t added, invalid empty name', N'20/6/2021 10:18:45')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (52, N'Post post wasn''t added, invalid creation date', N'20/6/2021 10:18:45')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (53, N'Post  wasn''t added, invalid empty name', N'20/6/2021 10:18:45')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (54, N'Post post wasn''t added, invalid creation date', N'20/6/2021 10:18:45')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (55, N'Theme post not exist', N'20/6/2021 10:18:45')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (56, N'Theme post not exist', N'20/6/2021 10:18:45')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (57, N'Post Post wasn''t added, already exist', N'20/6/2021 10:18:45')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (58, N'Post Post new was added', N'20/6/2021 10:18:45')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (59, N'Theme Theme new was added', N'20/6/2021 10:18:45')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (60, N'Theme Theme new wasn''t added, already exist', N'20/6/2021 10:18:45')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (61, N'Theme Theme prubea des was added', N'20/6/2021 10:18:45')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (62, N'Theme Theme prubea descrition was added', N'20/6/2021 10:19:24')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (63, N'Theme Theme prubea descrition wasn''t associated to post post', N'20/6/2021 10:27:39')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (64, N'Theme Theme was associated to post Post', N'20/6/2021 10:34:35')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (65, N'Theme Theme wasn''t associated to post Postnnn', N'20/6/2021 10:35:10')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (66, N'Theme Theme wasn''t associated to post ', N'20/6/2021 10:35:18')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (67, N'Theme Theme was associated to post Post about musics', N'20/6/2021 10:36:00')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (68, N'Theme Theme was associated to post Post about musics', N'20/6/2021 10:43:56')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (69, N'Theme Theme was associated to post Post about musics', N'20/6/2021 10:48:41')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (70, N'Theme Theme was dissasociated to post Post about musics', N'20/6/2021 10:49:02')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (71, N'Theme Theme was associated to post Post about musics', N'20/6/2021 10:49:43')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (72, N'Theme Theme was dissasociated to post Post about musics', N'20/6/2021 10:49:57')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (73, N'Post Post new was deleted', N'20/6/2021 10:51:27')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (74, N'Post Post new wasnt deleted', N'20/6/2021 10:54:02')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (75, N'Post post prueba5 was deleted', N'20/6/2021 10:54:51')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (76, N'Post post prueba5 wasnt deleted', N'20/6/2021 10:55:17')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (77, N'Post post prueba4 was deleted', N'20/6/2021 10:57:03')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (78, N'Post post prueba4 wasnt deleted', N'20/6/2021 10:59:18')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (79, N'Post post prueba3 was deleted', N'20/6/2021 10:59:38')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (80, N'Post Post about musics was modified', N'20/6/2021 11:00:24')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (81, N'Post Post about musics wasn''t modified', N'20/6/2021 11:03:52')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (82, N'Post Post about musics wasn''t modified', N'20/6/2021 11:04:12')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (83, N'Post Post about musics wasn''t modified', N'20/6/2021 11:04:48')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (84, N'Post Post about movies and games was modified', N'20/6/2021 11:05:01')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (85, N'Post Post about movies and games was modified', N'20/6/2021 11:07:58')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (86, N'Post Post about movies and games was modified', N'20/6/2021 11:08:54')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (87, N'Post Post about movies and games was modified', N'20/6/2021 11:12:17')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (88, N'Post Post prubea 12 was added', N'20/6/2021 11:12:45')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (89, N'Theme Post prubea 13 not exist', N'20/6/2021 11:12:53')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (90, N'Post Post prubea 12 was deleted', N'20/6/2021 11:13:35')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (91, N'Theme Theme prubea descrition was deleted', N'20/6/2021 11:15:49')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (92, N'Theme Theme prubea descrition wasn''t deleted, not exist', N'20/6/2021 11:15:52')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (93, N'Theme Theme prubea des was deleted', N'20/6/2021 11:16:06')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (94, N'Theme Theme prubea des wasn''t deleted, not exist', N'20/6/2021 11:16:14')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (95, N'Theme Theme new was deleted', N'20/6/2021 11:17:42')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (96, N'Theme Post prueba wasn''t deleted, not exist', N'20/6/2021 11:21:05')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (97, N'Theme post prueba was deleted', N'20/6/2021 11:21:27')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (98, N'Theme Theme was deleted', N'20/6/2021 11:23:08')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (99, N'Theme them2 was deleted', N'20/6/2021 11:23:13')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (100, N'Theme Post was deleted', N'20/6/2021 11:23:19')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (101, N'Post Post was deleted', N'20/6/2021 11:23:41')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (102, N'Post post prueba was deleted', N'20/6/2021 11:23:51')
GO
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (103, N'Post post prueba2 was deleted', N'20/6/2021 11:23:53')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (104, N'Post Post about music wasnt deleted', N'20/6/2021 11:24:27')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (105, N'Post Post about musics was deleted', N'20/6/2021 11:24:35')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (106, N'The file Batman.jpg was uploaded in the post Post about movies', N'20/6/2021 0:00:00')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (107, N'The file Batman.jpg was uploaded in the post Post about movies', N'20/6/2021 0:00:00')
INSERT [dbo].[Logs] ([Id], [Message], [CreationDate]) VALUES (108, N'The file Batman.jpg was uploaded in the post Post about movies', N'20/6/2021 0:00:00')
SET IDENTITY_INSERT [dbo].[Logs] OFF
GO
SET IDENTITY_INSERT [dbo].[Posts] ON 

INSERT [dbo].[Posts] ([Id], [Name], [CreationDate], [FileId]) VALUES (2, N'Post about movies and games', N'12/12/2001', 0)
INSERT [dbo].[Posts] ([Id], [Name], [CreationDate], [FileId]) VALUES (5, N'Post about movies', N'12/12/2000', 5)
INSERT [dbo].[Posts] ([Id], [Name], [CreationDate], [FileId]) VALUES (6, N'Post about games', N'12/12/2000', 0)
SET IDENTITY_INSERT [dbo].[Posts] OFF
GO
INSERT [dbo].[PostThemeDto] ([PostId], [ThemeId], [Id]) VALUES (2, 8, 0)
INSERT [dbo].[PostThemeDto] ([PostId], [ThemeId], [Id]) VALUES (5, 10, 0)
INSERT [dbo].[PostThemeDto] ([PostId], [ThemeId], [Id]) VALUES (6, 8, 0)
GO
SET IDENTITY_INSERT [dbo].[Themes] ON 

INSERT [dbo].[Themes] ([Id], [Name], [Description]) VALUES (8, N'Games', N'Theme games')
INSERT [dbo].[Themes] ([Id], [Name], [Description]) VALUES (9, N'Musics', N'Theme musics')
INSERT [dbo].[Themes] ([Id], [Name], [Description]) VALUES (10, N'Movies', N'Theme movies')
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
