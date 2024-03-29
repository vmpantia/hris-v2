﻿CREATE TABLE [dbo].[Employee_TRN]
(
	[RequestID] VARCHAR(13) NOT NULL PRIMARY KEY,
	[InternalID] UNIQUEIDENTIFIER NOT NULL,
	[EmployeeID] VARCHAR(15) NOT NULL,
	[FirstName] VARCHAR(20) NOT NULL,
	[LastName] VARCHAR(20) NOT NULL,
	[MiddleName] VARCHAR(20) NULL,
	[Suffix] VARCHAR(5) NULL,
	[Gender] VARCHAR(6) NOT NULL,
	[Birthdate] DATETIME NOT NULL,
	[CivilStatus] VARCHAR(9) NOT NULL,
	[Status] INT NOT NULL,
	[CreatedDate] DATETIME NOT NULL,
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[ModifiedDate] DATETIME NULL,
	[ModifiedBy] UNIQUEIDENTIFIER NULL
)
