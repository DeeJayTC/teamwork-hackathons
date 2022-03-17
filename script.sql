IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'twhackathon_db')
BEGIN
CREATE DATABASE [twhackathon_db]
END
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [twhackathon_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [twhackathon_db] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [twhackathon_db] SET ANSI_NULLS OFF
GO
ALTER DATABASE [twhackathon_db] SET ANSI_PADDING OFF
GO
ALTER DATABASE [twhackathon_db] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [twhackathon_db] SET ARITHABORT OFF
GO
ALTER DATABASE [twhackathon_db] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [twhackathon_db] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [twhackathon_db] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [twhackathon_db] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [twhackathon_db] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [twhackathon_db] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [twhackathon_db] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [twhackathon_db] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [twhackathon_db] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [twhackathon_db] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [twhackathon_db] SET  ENABLE_BROKER
GO
ALTER DATABASE [twhackathon_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [twhackathon_db] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [twhackathon_db] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [twhackathon_db] SET ALLOW_SNAPSHOT_ISOLATION ON
GO
ALTER DATABASE [twhackathon_db] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [twhackathon_db] SET READ_COMMITTED_SNAPSHOT ON
GO
ALTER DATABASE [twhackathon_db] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [twhackathon_db] SET  READ_WRITE
GO
ALTER DATABASE [twhackathon_db] SET RECOVERY FULL
GO
ALTER DATABASE [twhackathon_db] SET  MULTI_USER
GO
ALTER DATABASE [twhackathon_db] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [twhackathon_db] SET DB_CHAINING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[ProfileImage] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND name = N'EmailIndex')
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers] 
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND name = N'UserNameIndex')
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers] 
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND name = N'RoleNameIndex')
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles] 
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hackathon]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Hackathon](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Year] [int] NULL,
	[Date] [date] NULL,
 CONSTRAINT [PK_tHackathon] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserTokens]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoleClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoleClaims]') AND name = N'IX_AspNetRoleClaims_RoleId')
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims] 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND name = N'IX_AspNetUserRoles_RoleId')
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles] 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND name = N'IX_AspNetUserLogins_UserId')
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND name = N'IX_AspNetUserClaims_UserId')
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HackathonTeam]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[HackathonTeam](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HackathonID] [int] NULL,
	[TeamName] [nvarchar](255) NULL,
	[TeamDescription] [ntext] NULL,
	[URL] [nvarchar](500) NULL,
	[Logo] [nvarchar](500) NULL,
	[CreatedByID] [nvarchar](450) NULL,
 CONSTRAINT [PK_tHackathonTeam] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HackathonSearchMembers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[HackathonSearchMembers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](450) NULL,
	[Title] [nvarchar](255) NULL,
	[Text] [ntext] NULL,
 CONSTRAINT [PK_HackathonSearchMembers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HackathonVotingCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[HackathonVotingCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HackathonID] [int] NULL,
	[Name] [nvarchar](250) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_HackathonVotingCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HackathonTeamOfferings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[HackathonTeamOfferings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TeamID] [int] NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Text] [ntext] NULL,
 CONSTRAINT [PK_HackathonTeamOfferings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HackathonMembers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[HackathonMembers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TeamID] [int] NULL,
	[UserID] [nvarchar](450) NULL,
	[DisplayName] [nvarchar](450) NULL,
	[ProfileImage] [nvarchar](800) NULL,
 CONSTRAINT [PK_tHackathonMembers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_SomeName2]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonTeamOfferings]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_SomeName2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[HackathonTeamOfferings] ADD  CONSTRAINT [DF_SomeName2]  DEFAULT (N'') FOR [Title]
END


End
GO
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_SomeName]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonTeamOfferings]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_SomeName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[HackathonTeamOfferings] ADD  CONSTRAINT [DF_SomeName]  DEFAULT (N'') FOR [Text]
END


End
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserTokens_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserTokens]'))
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserTokens_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserTokens]'))
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetRoleClaims_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetRoleClaims]'))
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetRoleClaims_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetRoleClaims]'))
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserLogins_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserLogins_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserClaims_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserClaims_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HackathonTeam_AspNetUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonTeam]'))
ALTER TABLE [dbo].[HackathonTeam]  WITH CHECK ADD  CONSTRAINT [FK_HackathonTeam_AspNetUsers] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HackathonTeam_AspNetUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonTeam]'))
ALTER TABLE [dbo].[HackathonTeam] CHECK CONSTRAINT [FK_HackathonTeam_AspNetUsers]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tHackathonTeam_tHackathon]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonTeam]'))
ALTER TABLE [dbo].[HackathonTeam]  WITH CHECK ADD  CONSTRAINT [FK_tHackathonTeam_tHackathon] FOREIGN KEY([HackathonID])
REFERENCES [dbo].[Hackathon] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tHackathonTeam_tHackathon]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonTeam]'))
ALTER TABLE [dbo].[HackathonTeam] CHECK CONSTRAINT [FK_tHackathonTeam_tHackathon]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HackathonSearchMembers_AspNetUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonSearchMembers]'))
ALTER TABLE [dbo].[HackathonSearchMembers]  WITH CHECK ADD  CONSTRAINT [FK_HackathonSearchMembers_AspNetUsers] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HackathonSearchMembers_AspNetUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonSearchMembers]'))
ALTER TABLE [dbo].[HackathonSearchMembers] CHECK CONSTRAINT [FK_HackathonSearchMembers_AspNetUsers]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HackathonSearchMembers_HackathonSearchMembers]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonSearchMembers]'))
ALTER TABLE [dbo].[HackathonSearchMembers]  WITH CHECK ADD  CONSTRAINT [FK_HackathonSearchMembers_HackathonSearchMembers] FOREIGN KEY([ID])
REFERENCES [dbo].[HackathonSearchMembers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HackathonSearchMembers_HackathonSearchMembers]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonSearchMembers]'))
ALTER TABLE [dbo].[HackathonSearchMembers] CHECK CONSTRAINT [FK_HackathonSearchMembers_HackathonSearchMembers]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HackathonVotingCategory_Hackathon]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonVotingCategory]'))
ALTER TABLE [dbo].[HackathonVotingCategory]  WITH CHECK ADD  CONSTRAINT [FK_HackathonVotingCategory_Hackathon] FOREIGN KEY([HackathonID])
REFERENCES [dbo].[Hackathon] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HackathonVotingCategory_Hackathon]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonVotingCategory]'))
ALTER TABLE [dbo].[HackathonVotingCategory] CHECK CONSTRAINT [FK_HackathonVotingCategory_Hackathon]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HackathonTeamOfferings_HackathonTeam]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonTeamOfferings]'))
ALTER TABLE [dbo].[HackathonTeamOfferings]  WITH CHECK ADD  CONSTRAINT [FK_HackathonTeamOfferings_HackathonTeam] FOREIGN KEY([TeamID])
REFERENCES [dbo].[HackathonTeam] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HackathonTeamOfferings_HackathonTeam]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonTeamOfferings]'))
ALTER TABLE [dbo].[HackathonTeamOfferings] CHECK CONSTRAINT [FK_HackathonTeamOfferings_HackathonTeam]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tHackathonMembers_AspNetUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonMembers]'))
ALTER TABLE [dbo].[HackathonMembers]  WITH CHECK ADD  CONSTRAINT [FK_tHackathonMembers_AspNetUsers] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tHackathonMembers_AspNetUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonMembers]'))
ALTER TABLE [dbo].[HackathonMembers] CHECK CONSTRAINT [FK_tHackathonMembers_AspNetUsers]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tHackathonMembers_tHackathonTeam]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonMembers]'))
ALTER TABLE [dbo].[HackathonMembers]  WITH CHECK ADD  CONSTRAINT [FK_tHackathonMembers_tHackathonTeam] FOREIGN KEY([TeamID])
REFERENCES [dbo].[HackathonTeam] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tHackathonMembers_tHackathonTeam]') AND parent_object_id = OBJECT_ID(N'[dbo].[HackathonMembers]'))
ALTER TABLE [dbo].[HackathonMembers] CHECK CONSTRAINT [FK_tHackathonMembers_tHackathonTeam]
GO
