Drop table [dbo].[GYM_MEMEBER]

CREATE TABLE [dbo].[GYM_MEMEBER]
(
	[member_Id] INT IDENTITY(1000,1) NOT NULL PRIMARY KEY, 
    [f_name] NVARCHAR(50) NOT NULL, 
    [l_name] NVARCHAR(50) NOT NULL, 
    [dob] DATETIME NOT NULL, 
    [address] NVARCHAR(50) NOT NULL
)
