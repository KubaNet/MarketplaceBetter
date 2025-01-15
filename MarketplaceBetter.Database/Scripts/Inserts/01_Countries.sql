IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 1)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('United States', 1, 'US')
ELSE UPDATE [dbo].[Country] SET [Name] = 'United States', [Code] = 'US' WHERE [SystemName] = 1
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 2)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Canada', 2, 'CA')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Canada', [Code] = 'CA' WHERE [SystemName] = 2
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 3)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Great Britain', 3, 'GB')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Great Britain', [Code] = 'GB' WHERE [SystemName] = 3
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 4)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Germany', 4, 'DE')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Germany', [Code] = 'DE' WHERE [SystemName] = 4
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 5)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('France', 5, 'FR')
ELSE UPDATE [dbo].[Country] SET [Name] = 'France', [Code] = 'FR' WHERE [SystemName] = 5
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 6)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Italy', 6, 'IT')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Italy', [Code] = 'IT' WHERE [SystemName] = 6
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 7)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Spain', 7, 'ES')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Spain', [Code] = 'ES' WHERE [SystemName] = 7
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 8)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Netherlands', 8, 'NL')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Netherlands', [Code] = 'NL' WHERE [SystemName] = 8
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 9)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Sweden', 9, 'SE')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Sweden', [Code] = 'SE' WHERE [SystemName] = 9
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 10)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Poland', 10, 'PL')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Poland', [Code] = 'PL' WHERE [SystemName] = 10
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 11)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Austria', 11, 'AT')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Austria', [Code] = 'AT' WHERE [SystemName] = 11
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 12)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Ireland', 12, 'IE')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Ireland', [Code] = 'IE' WHERE [SystemName] = 12
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 13)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('CzechRepublic', 13, 'CZ')
ELSE UPDATE [dbo].[Country] SET [Name] = 'CzechRepublic', [Code] = 'CZ' WHERE [SystemName] = 13
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 14)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Belgium', 14, 'BE')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Belgium', [Code] = 'BE' WHERE [SystemName] = 14
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 15)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Bulgaria', 15, 'BG')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Bulgaria', [Code] = 'BG' WHERE [SystemName] = 15
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 16)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Croatia', 16, 'HR')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Croatia', [Code] = 'HR' WHERE [SystemName] = 16
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 17)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Cyprus', 17, 'CY')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Cyprus', [Code] = 'CY' WHERE [SystemName] = 17
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 18)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Denmark', 18, 'DK')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Denmark', [Code] = 'DK' WHERE [SystemName] = 18
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 19)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Estonia', 19, 'EE')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Estonia', [Code] = 'EE' WHERE [SystemName] = 19
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 20)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Finland', 20, 'FI')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Finland', [Code] = 'FI' WHERE [SystemName] = 20
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 21)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Greece', 21, 'GR')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Greece', [Code] = 'GR' WHERE [SystemName] = 21
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 22)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Lithuania', 22, 'LT')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Lithuania', [Code] = 'LT' WHERE [SystemName] = 22
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 23)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Luxembourg', 23, 'LU')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Luxembourg', [Code] = 'LU' WHERE [SystemName] = 23
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 24)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Latvia', 24, 'LV')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Latvia', [Code] = 'LV' WHERE [SystemName] = 24
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 25)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Malta', 25, 'MT')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Malta', [Code] = 'MT' WHERE [SystemName] = 25
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 26)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Portugal', 26, 'PT')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Portugal', [Code] = 'PT' WHERE [SystemName] = 26
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 27)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Romania', 27, 'RO')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Romania', [Code] = 'RO' WHERE [SystemName] = 27
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 28)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Slovakia', 28, 'SK')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Slovakia', [Code] = 'SK' WHERE [SystemName] = 28
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 29)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Slovenia', 29, 'SI')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Slovenia', [Code] = 'SI' WHERE [SystemName] = 29
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 30)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Hungary', 30, 'HU')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Hungary', [Code] = 'HU' WHERE [SystemName] = 30
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 31)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Monaco', 31, 'MC')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Monaco', [Code] = 'MC' WHERE [SystemName] = 31
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 32)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Jersey', 32, 'JE')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Jersey', [Code] = 'JE' WHERE [SystemName] = 32
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 33)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Guernsey', 33, 'GG')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Guernsey', [Code] = 'GG' WHERE [SystemName] = 33
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 34)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Norway', 34, 'NO')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Norway', [Code] = 'NO' WHERE [SystemName] = 34
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 35)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Israel', 35, 'IL')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Israel', [Code] = 'IL' WHERE [SystemName] = 35
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 36)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Qatar', 36, 'QA')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Qatar', [Code] = 'QA' WHERE [SystemName] = 36
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 37)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Switzerland', 37, 'CH')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Switzerland', [Code] = 'CH' WHERE [SystemName] = 37
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 38)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Kuwait', 38, 'KW')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Kuwait', [Code] = 'KW' WHERE [SystemName] = 38
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 39)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Japan', 39, 'JP')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Japan', [Code] = 'JP' WHERE [SystemName] = 39
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 40)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Mexico', 40, 'MX')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Mexico', [Code] = 'MX' WHERE [SystemName] = 40
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 41)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('China', 41, 'CN')
ELSE UPDATE [dbo].[Country] SET [Name] = 'China', [Code] = 'CN' WHERE [SystemName] = 41
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 42)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Chile', 42, 'CL')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Chile', [Code] = 'CL' WHERE [SystemName] = 42
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 43)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Taiwan', 43, 'TW')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Taiwan', [Code] = 'TW' WHERE [SystemName] = 43
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 44)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Egypt', 44, 'EG')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Egypt', [Code] = 'EG' WHERE [SystemName] = 44
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 45)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('HongKong', 45, 'HK')
ELSE UPDATE [dbo].[Country] SET [Name] = 'HongKong', [Code] = 'HK' WHERE [SystemName] = 45
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 46)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Iceland', 46, 'IS')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Iceland', [Code] = 'IS' WHERE [SystemName] = 46
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 47)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Bahrain', 47, 'BH')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Bahrain', [Code] = 'BH' WHERE [SystemName] = 47
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 48)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('India', 48, 'IN')
ELSE UPDATE [dbo].[Country] SET [Name] = 'India', [Code] = 'IN' WHERE [SystemName] = 48
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 49)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('United Arab Emirates', 49, 'AE')
ELSE UPDATE [dbo].[Country] SET [Name] = 'United Arab Emirates', [Code] = 'AE' WHERE [SystemName] = 49
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 50)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Australia', 50, 'AU')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Australia', [Code] = 'AU' WHERE [SystemName] = 50
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 51)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('South Africa', 51, 'ZA')
ELSE UPDATE [dbo].[Country] SET [Name] = 'South Africa', [Code] = 'ZA' WHERE [SystemName] = 51
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 52)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Peru', 52, 'PE')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Peru', [Code] = 'PE' WHERE [SystemName] = 52
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 53)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Colombia', 53, 'CO')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Colombia', [Code] = 'CO' WHERE [SystemName] = 53
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 54)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('South Korea', 54, 'KR')
ELSE UPDATE [dbo].[Country] SET [Name] = 'South Korea', [Code] = 'KR' WHERE [SystemName] = 54
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 55)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Philippines', 55, 'PH')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Philippines', [Code] = 'PH' WHERE [SystemName] = 55
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 56)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Singapore', 56, 'SG')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Singapore', [Code] = 'SG' WHERE [SystemName] = 56
GO

IF NOT EXISTS (SELECT NULL FROM [dbo].[Country] WHERE [SystemName] = 57)
    INSERT INTO [dbo].[Country] ([Name], [SystemName], [Code])
    VALUES ('Brazil', 57, 'BR')
ELSE UPDATE [dbo].[Country] SET [Name] = 'Brazil', [Code] = 'BR' WHERE [SystemName] = 57
GO