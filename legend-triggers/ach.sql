-- 1. Add powebuff on character create

USE [Server]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[AddPowerBuffToNewAccountCharacter] ON [dbo].[Character] AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @account_id		VARCHAR(10)
	DECLARE @lvlb			INT

	SELECT @account_id = ins.AccountID FROM INSERTED ins
	SELECT @lvlb = LVLB FROM Me_Server.dbo.MEMB_INFO WHERE memb___id = @account_id

	IF(@lvlb > 0)
	BEGIN
		UPDATE [dbo].[Character] SET [LVLB] = @lvlb FROM INSERTED ins WHERE ins.name = [dbo].[Character].Name
	END
END