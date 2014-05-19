
CREATE TABLE [dbo].[ApplicationLog](
	[SysNo] [int] IDENTITY(1,1) NOT NULL,
	[Source] [nvarchar](100) NOT NULL,
	[Category] [nvarchar](50) NULL,
	[SubCategory] [nvarchar](50) NULL,
	[ReferenceIP] [varchar](20) NULL,
	[EventType] [int] NOT NULL,
	[EventTypeName] [varchar](50) NULL,
	[HostName] [varchar](50) NOT NULL,
	[EventTitle] [nvarchar](100) NULL,
	[EventMessage] [nvarchar](max) NOT NULL,
	[EventStackTrace] [nvarchar](max) NULL,
	[EventDetail] [nvarchar](max) NULL,
	[InDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ApplicationLog_SysNo] PRIMARY KEY CLUSTERED 
(
	[SysNo] ASC
)
) ON [PRIMARY]

GO
