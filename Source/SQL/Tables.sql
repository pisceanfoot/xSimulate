

DROP Table [dbo].[Customer]
CREATE Table [dbo].[Customer]
(
	SysNO					INT IDENTITY(1,1)	NOT NULL,
	CustomerID				NVARCHAR(50)		NOT NULL,
	NAME					NVARCHAR(50)		NULL,
	[Password]				VARCHAR(50)			NOT NULL,
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

DROP Table [dbo].[Account]
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