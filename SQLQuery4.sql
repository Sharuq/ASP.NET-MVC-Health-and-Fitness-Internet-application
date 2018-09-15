
CREATE TABLE [dbo].[GYM_MEMEBER] (
    [member_Id] NVARCHAR (128) NOT NULL,
    [f_name]    NVARCHAR (50)  NOT NULL,
    [l_name]    NVARCHAR (50)  NOT NULL,
    [dob]       DATETIME       NOT NULL,
    [address]   NVARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([member_Id] ASC),
	foreign key (member_id)  references Dbo.AspNetUsers(Id)
);
