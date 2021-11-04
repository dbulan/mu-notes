-- 1. Add Columns

-- 2. Create Trigger AFTER UPDATE

USE [Server]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[C_Monster_KillCountAch_UPDATE] ON [dbo].[C_Monster_KillCount]
   AFTER UPDATE
AS 
BEGIN
  -- SET NOCOUNT ON added to prevent extra result sets from
  -- interfering with SELECT statements.
  SET NOCOUNT ON;

    -- Insert statements for trigger here
  DECLARE @counter_del  int
  DECLARE @counter_ins  int

  IF UPDATE (count)
  BEGIN
    SELECT @counter_ins = ins.count FROM INSERTED ins
    SELECT @counter_del = del.count FROM DELETED del
    
    UPDATE [dbo].[C_Monster_KillCount] SET [countAch] = [dbo].[C_Monster_KillCount].countAch + (@counter_ins - @counter_del) FROM INSERTED ins WHERE ins.name = [dbo].[C_Monster_KillCount].name AND ins.MonsterId = [dbo].[C_Monster_KillCount].MonsterId
  END
END

-- 3. Create Trigger AFTER INSERT

USE [Server]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[C_Monster_KillCountAch_INSERT] ON [dbo].[C_Monster_KillCount] AFTER INSERT
AS 
BEGIN
  -- SET NOCOUNT ON added to prevent extra result sets from
  -- interfering with SELECT statements.
  SET NOCOUNT ON;
  
  DECLARE @counter  INT

  SELECT @counter = ins.count FROM INSERTED ins
  
  UPDATE [dbo].[C_Monster_KillCount] SET [countAch] = @counter FROM INSERTED ins WHERE ins.name = [dbo].[C_Monster_KillCount].Name AND ins.MonsterId = [dbo].[C_Monster_KillCount].MonsterId
END
