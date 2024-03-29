USE [Jornada.Milhas]
GO

/****** Object:  Table [dbo].[Destino]    Script Date: 1/21/2024 9:00:06 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Destino](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](300) NOT NULL,
	[Preco] [decimal](18, 2) NOT NULL,
	[Deleted] [bit] NOT NULL,
	[DeletedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


