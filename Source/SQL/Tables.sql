

-- DROP Table [dbo].[Customer]
CREATE Table [dbo].[Customer]

(
	SysNO					INT IDENTITY(1,1)	NOT NULL,
	CustomerID				NVARCHAR(50)		NOT NULL,
	NAME					NVARCHAR(50)		NULL,
	[Password]				VARCHAR(50)			NOT NULL,
	[Status]				CHAR(1)				NOT NULL,
	InDate					DATETIME			NOT NULL,
	InUser					VARCHAR(20)			NULL,
	EditDate				DATETIME			NOT NULL,
	EditUser				VARCHAR(20)			NULL,
	CONSTRAINT [PK_Customer_SysNO] PRIMARY KEY CLUSTERED
	(
		SysNO ASC
	)
) ON [PRIMARY]
GO

-- DROP Table [dbo].[Account]
CREATE Table [dbo].[Account]

(
	SysNO					INT					NOT NULL,
	Amount					DECIMAL(19,6)		NOT NULL,
	LockedAmount			DECIMAL(19,6)		NOT NULL,
	InDate					DATETIME			NOT NULL,
	InUser					VARCHAR(20)			NULL,
	EditDate				DATETIME			NOT NULL,
	EditUser				VARCHAR(20)			NULL,
	CONSTRAINT [PK_Account_SysNO] PRIMARY KEY CLUSTERED
	(
		SysNO ASC
	)
) ON [PRIMARY]
GO



-- DROP Table [dbo].[CustomerSetting]
CREATE Table [dbo].[CustomerSetting]

(
	SysNO				INT IDENTITY(1,1)	NOT NULL,
	CustomerSysNo		INT NOT NULL,
	Setting				XML NULL,
	[InDate]				[datetime]			NULL,
	[InUser]				[varchar](20)		NULL,
	[EditDate]				[datetime]			NULL,
	[EditUser]				[varchar](20)		NULL,
	CONSTRAINT [PK_CustomerSetting_SysNO] PRIMARY KEY CLUSTERED
	(
		SysNO ASC
	)
) ON [PRIMARY]
GO

-- DROP Table [dbo].[Task]
CREATE Table [dbo].[Task]

(
	SysNo					INT IDENTITY(1,1)	NOT NULL,
	CustomerSysNO			INT					NOT NULL,
	CustomerSettingSysNo	INT					NOT NULL,
	RunTimes				INT					NOT NULL,
	DownTimes				INT					NOT NULL,
	BeginDate				DATETIME			NOT NULL,
	[Status]				CHAR(1)				NOT NULL,
	[InDate]				[datetime]			NULL,
	[InUser]				[varchar](20)		NULL,
	[EditDate]				[datetime]			NULL,
	[EditUser]				[varchar](20)		NULL,
	CONSTRAINT [PK_Task_SysNO] PRIMARY KEY CLUSTERED
	(
		SysNo ASC
	)
) ON [PRIMARY]
GO

-- DROP Table [dbo].[RetrieveTask]
CREATE Table [dbo].[RetrieveTask]

(
	SysNo					INT IDENTITY(1,1)	NOT NULL,
	CustomerSysNO			INT					NOT NULL,
	RunTaskSysNo			INT					NOT NULL,
	[Status]				CHAR(1)				NOT NULL,
	[InDate]				[datetime]			NULL,
	[InUser]				[varchar](20)		NULL,
	[EditDate]				[datetime]			NULL,
	[EditUser]				[varchar](20)		NULL,
	CONSTRAINT [PK_RetrieveTask_SysNO] PRIMARY KEY CLUSTERED
	(
		SysNo ASC
	)
) ON [PRIMARY]
GO

