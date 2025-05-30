USE [IT_test]
GO
/****** Object:  Table [dbo].[mas_member]    Script Date: 28/4/2568 0:48:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mas_member](
	[member_id] [int] IDENTITY(1,1) NOT NULL,
	[firstname] [varchar](255) NOT NULL,
	[address] [text] NOT NULL,
	[birthday] [datetime] NOT NULL,
	[status] [bit] NOT NULL,
	[lastname] [varchar](225) NULL,
PRIMARY KEY CLUSTERED 
(
	[member_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[mas_member] ADD  DEFAULT ((1)) FOR [status]
GO
