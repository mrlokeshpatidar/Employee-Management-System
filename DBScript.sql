USE [EmpDB]
GO
/****** Object:  Table [dbo].[EmpDetails]    Script Date: 7/30/2022 3:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmpDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Designation] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_EmpDetails_CreatedDate]  DEFAULT (getdate()),
	[UpdatedDate] [datetime] NOT NULL CONSTRAINT [DF_Table_1_]  DEFAULT (getdate()),
 CONSTRAINT [PK_EmpDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmpSalarys]    Script Date: 7/30/2022 3:15:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmpSalarys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[SalaryPaid] [decimal](18, 2) NOT NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_EmpSalarys_CreatedDate]  DEFAULT (getdate()),
	[UpdatedDate] [datetime] NOT NULL CONSTRAINT [DF_EmpSalarys_UpdatedDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_EmpSalarys] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 7/30/2022 3:15:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[empdetails_getall_paging]    Script Date: 7/30/2022 3:15:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[empdetails_getall_paging]
@PageNum int,
@PageSize int

AS
BEGIN

	WITH TempResult AS
	(
	          SELECT * FROM [dbo].[EmpDetails]
	), 
	TempCount AS
	(
		SELECT COUNT(*) AS  TotalRows FROM TempResult
	)
	SELECT *
	FROM TempResult, TempCount
	ORDER BY TempResult.[Id] desc
	OFFSET (@PageNum - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY; 
END

GO
/****** Object:  StoredProcedure [dbo].[empsalary_getall_paging]    Script Date: 7/30/2022 3:15:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[empsalary_getall_paging]
@PageNum int,
@PageSize int

AS
BEGIN

	WITH TempResult AS
	(
	          SELECT * FROM [dbo].[EmpSalarys]
	), 
	TempCount AS
	(
		SELECT COUNT(*) AS  TotalRows FROM TempResult
	)
	SELECT *
	FROM TempResult, TempCount
	ORDER BY TempResult.[Id] desc
	OFFSET (@PageNum - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY; 
END

GO
