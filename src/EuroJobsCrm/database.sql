USE [bwg20171_crm_bwg]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 10.04.2017 08:24:20 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[adr_id] [int] IDENTITY(1,1) NOT NULL,
	[ard_clt_id] [int] NULL,
	[ard_emp_id] [int] NULL,
	[adr_cgt_id] [int] NULL,
	[adr_type] [varchar](50) NULL,
	[adr_post_code] [varchar](10) NULL,
	[adr_city] [varchar](100) NULL,
	[adr_country] [varchar](100) NULL,
	[adr_audit_cd] [datetime] NULL,
	[adr_audit_cu] [nvarchar](100) NULL,
	[adr_audit_md] [datetime] NULL,
	[adr_aduit_mu] [nvarchar](100) NULL,
	[adr_audit_rd] [datetime] NULL,
	[adr_aduit_ru] [nvarchar](100) NULL,
	[adr_address] [nvarchar](500) NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[adr_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
	[Blocked] [bit] NULL,
	[Deleted] [bit] NULL,
	[FullName] [nvarchar](500) NULL,
	[LanguageCode] [nvarchar](2) NULL,
	[ContragentId] [int] NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Clients]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[clt_id] [int] IDENTITY(1,1) NOT NULL,
	[clt_name] [nvarchar](100) NOT NULL,
	[clt_nip] [nvarchar](20) NOT NULL,
	[clt_krs] [nvarchar](20) NOT NULL,
	[clt_regon] [nvarchar](20) NOT NULL,
	[clt_audit_cd] [datetime] NULL,
	[clt_audit_cu] [nvarchar](100) NULL,
	[clt_audit_md] [datetime] NULL,
	[clt_audit_mu] [nvarchar](100) NULL,
	[clt_audit_rd] [datetime] NULL,
	[clt_audit_ru] [nvarchar](100) NULL,
	[clt_status] [int] NULL,
	[clt_type] [int] NULL,
	[clt_branch] [nvarchar](30) NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[clt_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ContactPersons]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactPersons](
	[ctp_id] [int] IDENTITY(1,1) NOT NULL,
	[ctp_clt_id] [int] NULL,
	[ctp_cgt_id] [int] NULL,
	[ctp_name] [nvarchar](150) NULL,
	[ctp_surname] [nvarchar](150) NULL,
	[ctp_position] [nvarchar](250) NULL,
	[ctp_email] [nvarchar](250) NULL,
	[ctp_skype] [nvarchar](150) NULL,
	[ctp_phone_number] [nvarchar](150) NULL,
	[ctp_messanger] [nvarchar](150) NULL,
	[ctp_audit_cd] [datetime] NULL,
	[ctp_audit_cu] [nvarchar](100) NULL,
	[ctp_audit_md] [datetime] NULL,
	[ctp_audit_mu] [nvarchar](100) NULL,
	[ctp_audit_rd] [datetime] NULL,
	[ctp_audit_ru] [nvarchar](100) NULL,
	[ctp_messanger_type] [int] NULL,
 CONSTRAINT [PK_ContactPersons] PRIMARY KEY CLUSTERED 
(
	[ctp_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contracts]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contracts](
	[cnt_id] [int] IDENTITY(1,1) NOT NULL,
	[cnt_cgt_id] [int] NOT NULL,
	[cnt_emp_id] [int] NOT NULL,
	[cnt_start_date] [datetime] NOT NULL,
	[cnt_end_date] [datetime] NULL,
	[cnt_issue_date] [datetime] NOT NULL,
	[cnt_audit_cd] [datetime] NULL,
	[cnt_audit_cu] [nvarchar](100) NULL,
	[cnt_audit_md] [datetime] NULL,
	[cnt_audit_mu] [nvarchar](100) NULL,
	[cnt_audit_rd] [datetime] NULL,
	[cnt_audit_ru] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[cnt_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contragents]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contragents](
	[cgt_id] [int] IDENTITY(1,1) NOT NULL,
	[cgt_name] [nvarchar](100) NOT NULL,
	[cgt_license_number] [nvarchar](100) NOT NULL,
	[cgt_status] [nvarchar](1) NOT NULL,
	[cgt_audit_cd] [datetime] NULL,
	[cgt_audit_cu] [nvarchar](100) NULL,
	[cgt_audit_md] [datetime] NULL,
	[cgt_audit_mu] [nvarchar](100) NULL,
	[cgt_audit_rd] [datetime] NULL,
	[cgt_audit_ru] [nvarchar](100) NULL,
	[cgt_responsible_user] [nvarchar](100) NULL,
 CONSTRAINT [PK_Contragents] PRIMARY KEY CLUSTERED 
(
	[cgt_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ContragentsUsersMappings]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContragentsUsersMappings](
	[cum_id] [int] IDENTITY(1,1) NOT NULL,
	[cum_ctg_id] [int] NOT NULL,
	[cum_user_id] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_ContragentsUsersMappings] PRIMARY KEY CLUSTERED 
(
	[cum_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DocumentFiles]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentFiles](
	[dcf_id] [int] IDENTITY(1,1) NOT NULL,
	[dcf_inv_id] [int] NULL,
	[dcf_cnt_id] [int] NULL,
	[dcf_idc_id] [int] NULL,
	[dcf_google_file_id] [nvarchar](50) NULL,
	[dcf_name] [nvarchar](300) NOT NULL,
	[dcf_description] [nvarchar](1000) NULL,
	[dcf_url] [nvarchar](500) NOT NULL,
	[dcf_audit_cd] [datetime] NULL,
	[dcf_audit_cu] [nvarchar](100) NULL,
	[dcf_audit_md] [datetime] NULL,
	[dcf_audit_mu] [nvarchar](100) NULL,
	[dcf_audit_rd] [datetime] NULL,
	[dcf_audit_ru] [nvarchar](100) NULL,
	[dcf_cli_id] [int] NULL,
	[dcf_ofr_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[dcf_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employees]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[emp_id] [int] IDENTITY(1,1) NOT NULL,
	[emp_ctg_id] [int] NOT NULL,
	[emp_first_name] [nvarchar](100) NOT NULL,
	[emp_middle_name] [nvarchar](150) NULL,
	[emp_last_name] [nvarchar](100) NOT NULL,
	[emp_birth_date] [date] NOT NULL,
	[emp_description] [nvarchar](1000) NULL,
	[emp_responsible_user] [nvarchar](100) NULL,
	[emp_status] [int] NULL,
	[emp_audit_cd] [datetime] NULL,
	[emp_audit_cu] [nvarchar](100) NULL,
	[emp_audit_md] [datetime] NULL,
	[emp_audit_mu] [nvarchar](100) NULL,
	[emp_audit_rd] [datetime] NULL,
	[emp_audit_ru] [nvarchar](100) NULL,
	[emp_clt_id] [int] NULL,
	[emp_off_id] [int] NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[emp_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmploymentRequests]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmploymentRequests](
	[etr_id] [int] IDENTITY(1,1) NOT NULL,
	[etr_ofr_id] [int] NOT NULL,
	[etr_emp_id] [int] NOT NULL,
	[etr_status] [int] NOT NULL,
	[etr_cnt_id] [int] NULL,
	[etr_audit_cd] [datetime] NULL,
	[etr_audit_cu] [nvarchar](100) NULL,
	[etr_audit_md] [datetime] NULL,
	[etr_audit_mu] [nvarchar](100) NULL,
	[etr_audit_rd] [datetime] NULL,
	[etr_audit_ru] [nvarchar](100) NULL,
	[etr_clt_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[etr_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityDocuments]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityDocuments](
	[idc_id] [int] IDENTITY(1,1) NOT NULL,
	[idc_parent_idc_id] [int] NULL,
	[idc_emp_id] [int] NOT NULL,
	[idc_seria] [nvarchar](16) NULL,
	[idc_number] [nvarchar](32) NULL,
	[idc_issue_date] [datetime] NULL,
	[idc_valid_from] [datetime] NULL,
	[idc_valid_to] [datetime] NULL,
	[idc_type] [int] NOT NULL,
	[idc_visa_type] [nvarchar](10) NULL,
	[idc_remarks] [nvarchar](500) NULL,
	[idc_audit_cd] [datetime] NULL,
	[idc_audit_cu] [nvarchar](100) NULL,
	[idc_audit_md] [datetime] NULL,
	[idc_audit_mu] [nvarchar](100) NULL,
	[idc_audit_rd] [datetime] NULL,
	[idc_audit_ru] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idc_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Notes]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notes](
	[not_id] [nvarchar](100) NOT NULL,
	[not_subject] [nvarchar](200) NULL,
	[not_text] [nvarchar](1000) NULL,
	[not_end_date] [datetime] NULL,
	[not_remind_date] [datetime] NULL,
	[not_target_user] [nvarchar](100) NULL,
	[not_ctg_id] [int] NULL,
	[not_clt_id] [int] NULL,
	[not_emp] [int] NULL,
	[not_audit_md] [datetime] NULL,
	[not_audit_mu] [nvarchar](100) NULL,
	[not_audit_rd] [datetime] NULL,
	[not_audit_ru] [nvarchar](100) NULL,
	[not_start_date] [datetime] NULL,
	[not_audit_cd] [datetime] NULL,
	[not_audit_cu] [nvarchar](150) NULL,
	[not_status] [nvarchar](150) NULL,
	[not_reminded] [bit] NULL,
 CONSTRAINT [PK_Notes] PRIMARY KEY CLUSTERED 
(
	[not_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Offers]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offers](
	[ofr_id] [int] IDENTITY(1,1) NOT NULL,
	[ofr_clt_id] [int] NOT NULL,
	[ofr_branch] [int] NULL,
	[ofr_vacancies_number] [int] NULL,
	[ofr_starting_date] [datetime] NULL,
	[ofr_ending_date] [datetime] NULL,
	[ofr_work_place] [nvarchar](500) NULL,
	[ofr_responsibilities] [nvarchar](1500) NULL,
	[ofr_education] [int] NULL,
	[ofr_experience] [nvarchar](100) NULL,
	[ofr_age_from] [int] NULL,
	[ofr_age_to] [int] NULL,
	[ofr_languages] [nvarchar](300) NULL,
	[ofr_comments] [nvarchar](1000) NULL,
	[ofr_contract_type] [int] NULL,
	[ofr_work_start] [datetime] NULL,
	[ofr_work_end] [datetime] NULL,
	[ofr_overtime_rate] [nvarchar](20) NULL,
	[ofr_hours_per_month] [int] NULL,
	[ofr_work_days] [int] NOT NULL,
	[ofr_rate_per_hour] [float] NULL,
	[ofr_rate_per_month] [float] NULL,
	[ofr_payment_method] [int] NULL,
	[ofr_advance_amount] [float] NULL,
	[ofr_accomodation_type] [int] NULL,
	[ofr_accomodation_price] [float] NULL,
	[ofr_room_people_number] [int] NULL,
	[ofr_distance_to_work] [float] NULL,
	[ofr_transport_to_work] [int] NULL,
	[ofr_transport_price] [float] NULL,
	[ofr_facilities] [nvarchar](1000) NULL,
	[ofr_additional_info] [nvarchar](1000) NULL,
	[ofr_documents] [nvarchar](500) NULL,
	[ofr_audit_cd] [datetime] NULL,
	[ofr_audit_cu] [nvarchar](100) NULL,
	[ofr_audit_md] [datetime] NULL,
	[ofr_audit_mu] [nvarchar](100) NULL,
	[ofr_audit_rd] [datetime] NULL,
	[ofr_audit_ru] [nvarchar](100) NULL,
	[ofr_gender] [int] NULL,
	[ofr_position] [nvarchar](150) NULL,
 CONSTRAINT [PK_Offers] PRIMARY KEY CLUSTERED 
(
	[ofr_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UsersToContragents]    Script Date: 10.04.2017 08:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersToContragents](
	[utc_id] [int] IDENTITY(1,1) NOT NULL,
	[utc_usr_id] [varchar](100) NULL,
	[utc_usr_name] [nvarchar](50) NULL,
	[utc_ctg_id] [int] NULL,
	[utc_lng] [varchar](2) NULL,
PRIMARY KEY CLUSTERED 
(
	[utc_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT ((0)) FOR [Blocked]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Notes] ADD  DEFAULT ((0)) FOR [not_reminded]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
