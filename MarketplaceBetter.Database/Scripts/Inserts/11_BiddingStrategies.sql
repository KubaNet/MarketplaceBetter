IF NOT EXISTS (SELECT NULL FROM [dbo].[BiddingStrategy] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[BiddingStrategy] ([Name], [SystemName])
    VALUES ('Dynamic bids - down only', 1)
ELSE UPDATE [dbo].[BiddingStrategy] SET [Name] = 'Dynamic bids - down only' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[BiddingStrategy] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[BiddingStrategy] ([Name], [SystemName])
    VALUES ('Dynamic bids - up and down', 2)
ELSE UPDATE [dbo].[BiddingStrategy] SET [Name] = 'Dynamic bids - up and down' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[BiddingStrategy] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[BiddingStrategy] ([Name], [SystemName])
    VALUES ('Fixed bid', 3)
ELSE UPDATE [dbo].[BiddingStrategy] SET [Name] = 'Fixed bid' WHERE [SystemName] = 3
GO