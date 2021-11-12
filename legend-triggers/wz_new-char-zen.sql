-- 1. Create Money Column

ALTER TABLE [DefaultClassType] ADD [Money] INT NOT NULL DEFAULT 0;

-- 2. Set default value

UPDATE [DefaultClassType] SET Money = XXX

-- 3. ServerX -> Programmability -> Stored Procedures -> WZ_CreateChracter [right-click -> modify]
-- Replace part of code:

BEGIN
  INSERT INTO dbo.Character(AccountID, Name, cLevel, LevelUpPoint, Class, Strength, Dexterity, Vitality, Energy, Inventory, MagicList,
      Life, MaxLife, Mana, MaxMana, MapNumber, MapPosX, MapPosY,  MDate, LDate, Quest, Leadership, Money)

  SELECT @AccountID AS AccountID, @Name AS Name, Level, LevelUpPoint, @Class AS Class, Strength, Dexterity, Vitality, Energy, Inventory,
    MagicList, Life, MaxLife, Mana, MaxMana, MapNumber, MapPosX, MapPosY, getdate() AS MDate, getdate() AS LDate, Quest, Leadership, Money
  FROM  DefaultClassType WHERE Class = @Class

  SET @Result = @@Error
END