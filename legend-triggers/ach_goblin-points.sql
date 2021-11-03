-- 1. Create Table T_Achpoints 

USE [Server]

CREATE TABLE [dbo].[T_Achpoints] (
	[AccountID] [varchar](10) NOT NULL,
	[GoblinPoint] [float] NOT NULL DEFAULT ((0.00)),
)

-- 2. Create Trigger AFTER UPDATE

USE [Server]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[GoblinPointAch] ON [dbo].[T_InGameShop_Point]
   AFTER UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	DECLARE @goblin_ins	float
	DECLARE @goblin_del	float
	DECLARE @account_id varchar(10)

	IF UPDATE (GoblinPoint)
	BEGIN
		SELECT @goblin_ins = ins.GoblinPoint, @account_id = ins.AccountID FROM INSERTED ins
		SELECT @goblin_del = del.GoblinPoint FROM DELETED del

		IF(@goblin_ins > @goblin_del)
		BEGIN
			INSERT [T_Achpoints] (AccountID, GoblinPoint) VALUES (@account_id, (@goblin_ins - @goblin_del));
		END
	END

END