

DROP TABLE [dbo].[ReadModel-CustomerState]

CREATE TABLE [dbo].[ReadModel-CustomerState]
(
	[Id] [uniqueidentifier] NOT NULL,
	[AggregateId] NVARCHAR(255),
	[EmailAddress] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255),
	[InvitationCode] [nvarchar](255),
	[RegisterState] [nvarchar](255),
	[Token] [NVARCHAR](255),
	[FailedMessage] [nvarchar](255),
	[Version] int,
	PRIMARY KEY(Id)
)
