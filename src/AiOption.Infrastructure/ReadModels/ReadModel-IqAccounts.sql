

DROP TABLE [dbo].[ReadModel-IqAccounts]

CREATE TABLE [dbo].[ReadModel-IqAccounts]
(
	[EmailAddress] [nvarchar](255) NOT NULL,
	[Message] [nvarchar](255) NOT NULL,
	[IqIdentity] [uniqueidentifier] NOT NULL,
	[IsSuccess] bit NOT NULL,
	[Version] [int] NULL,
	PRIMARY KEY(EmailAddress)
)