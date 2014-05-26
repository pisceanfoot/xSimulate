

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
	DROP TABLE [dbo].[Customer]
GO
CREATE Table [dbo].[Customer]

(
	SysNo					INT IDENTITY(1,1)	NOT NULL,
	CustomerID				NVARCHAR(50)		NOT NULL,
	NAME					NVARCHAR(50)		NULL,
	[Password]				VARCHAR(50)			NOT NULL,
	[Status]				CHAR(1)				NOT NULL,
	[QQ]					VARCHAR(50)			NULL,
	InDate					DATETIME			NULL,
	InUser					VARCHAR(20)			NULL,
	EditDate				DATETIME			NULL,
	EditUser				VARCHAR(20)			NULL,
	CONSTRAINT [PK_Customer_SysNo] PRIMARY KEY CLUSTERED
	(
		SysNo ASC
	)
) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Account]') AND type in (N'U'))
	DROP TABLE [dbo].[Account]
GO
CREATE Table [dbo].[Account]

(
	SysNo					INT					NOT NULL,
	Amount					DECIMAL(19,6)		NOT NULL,
	LockedAmount			DECIMAL(19,6)		NOT NULL,
	InDate					DATETIME			NOT NULL,
	InUser					VARCHAR(20)			NULL,
	EditDate				DATETIME			NOT NULL,
	EditUser				VARCHAR(20)			NULL,
	CONSTRAINT [PK_Account_SysNo] PRIMARY KEY CLUSTERED
	(
		SysNo ASC
	)
) ON [PRIMARY]
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerSetting]') AND type in (N'U'))
	DROP TABLE [dbo].[CustomerSetting]
GO
CREATE Table [dbo].[CustomerSetting]

(
	SysNo				INT IDENTITY(1,1)	NOT NULL,
	CustomerSysNo		INT NOT NULL,
	Setting				XML NULL,
	[InDate]				[datetime]			NULL,
	[InUser]				[varchar](20)		NULL,
	[EditDate]				[datetime]			NULL,
	[EditUser]				[varchar](20)		NULL,
	CONSTRAINT [PK_CustomerSetting_SysNo] PRIMARY KEY CLUSTERED
	(
		SysNo ASC
	)
) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Task]') AND type in (N'U'))
	DROP TABLE [dbo].[Task]
GO
CREATE Table [dbo].[Task]

(
	SysNo					INT IDENTITY(1,1)	NOT NULL,
	CustomerSysNo			INT					NOT NULL,
	CustomerSettingSysNo	INT					NOT NULL,
	RunTimes				INT					NOT NULL,
	DownTimes				INT					NOT NULL,
	BeginDate				DATETIME			NOT NULL,
	EndDate					DATETIME			NULL,
	Costs					DECIMAL(19, 6)		NULL,
	[Status]				CHAR(1)				NOT NULL,	-- A:发布
	[InDate]				[datetime]			NULL,
	[InUser]				[varchar](20)		NULL,
	[EditDate]				[datetime]			NULL,
	[EditUser]				[varchar](20)		NULL,
	CONSTRAINT [PK_Task_SysNo] PRIMARY KEY CLUSTERED
	(
		SysNo ASC
	)
) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RetrieveTask]') AND type in (N'U'))
	DROP TABLE [dbo].[RetrieveTask]
GO
CREATE Table [dbo].[RetrieveTask]

(
	SysNo					INT IDENTITY(1,1)	NOT NULL,
	CustomerSysNo			INT					NOT NULL,  -- 发布者编号
	RetrieveCustomerSysNo	INT					NOT NULL,  -- 任务接受者编号
	RunTaskSysNo			INT					NOT NULL,
	[Status]				CHAR(1)				NOT NULL,  -- A: 接受 D:完成 F:失败
	[Description]			NVARCHAR(500)		NULL,
	[InDate]				[datetime]			NULL,
	[InUser]				[varchar](20)		NULL,
	[EditDate]				[datetime]			NULL,
	[EditUser]				[varchar](20)		NULL,
	CONSTRAINT [PK_RetrieveTask_SysNo] PRIMARY KEY CLUSTERED
	(
		SysNo ASC
	)
) ON [PRIMARY]
GO

