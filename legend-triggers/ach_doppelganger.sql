USE [ServerX]
GO
/****** Object:  Trigger [dbo].[EventMapEnterLimitAch]    Script Date: 08.12.2021 18:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[EventMapEnterLimitAch] ON [dbo].[IGC_EventMapEnterLimit]
   AFTER UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	DECLARE @doppel_ins	tinyint
	DECLARE @doppel_ach	tinyint
	DECLARE @doppel_del	tinyint

	IF UPDATE (DoppelGanger)
	BEGIN
		SELECT @doppel_ins = ins.DoppelGanger, @doppel_ach = ins.DoppelGangerAch FROM INSERTED ins
		SELECT @doppel_del = del.DoppelGanger FROM DELETED del

		IF(@doppel_ins > @doppel_del)
		BEGIN
			SET @doppel_ach = @doppel_ach + 1
		END

		UPDATE [dbo].[IGC_EventMapEnterLimit] SET DoppelGangerAch = @doppel_ach FROM INSERTED ins WHERE ins.CharacterName = [dbo].[IGC_EventMapEnterLimit].CharacterName
	END

END