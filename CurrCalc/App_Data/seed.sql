USE [CurrCalcDb]

INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'23067f05-aa32-4b08-b890-a07a87bab776', N'User', N'USER', N'23d1b5b1-d7d4-41dc-adab-e8057e3f983b')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'623ffe90-d30b-4b9f-8643-eb322f7f86b5', N'Trader', N'TRADER', N'b73d6168-8913-498e-b239-3c5c7eb4608f')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'bd36379e-9399-4b20-ad5e-b4ada7408579', N'Admin', N'ADMIN', N'1da218a5-9d56-4a61-9cac-3ef951b3877e')

INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5dd913ef-54e0-4e2b-9c63-bd7394a3b3eb', N'trader@trader.com', N'TRADER@TRADER.COM', N'trader@trader.com', N'TRADER@TRADER.COM', 0, N'AQAAAAEAACcQAAAAEBeVBpERB81EP7f0scebRJLtEKTA3OSZk+UMpzSHNVemvnZyBAJDHJqZKcF2SMw2+Q==', N'F4XBCKQO7DNZEYD7VGQZ75ESKUU7BOPH', N'67ea53b6-0afc-4900-82f7-895367a07687', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'aafb003f-9835-4d64-bebf-202238a4e5fb', N'test@test.com', N'TEST@TEST.COM', N'test@test.com', N'TEST@TEST.COM', 0, N'AQAAAAEAACcQAAAAEGaZrKO8V/xtgk7MYc9P9Hp3lhIvWsHs12XwJMcpfJGyIpU7fQsHjfmnlLMheRMn/g==', N'THT2D3VMKQM7YEZTIXWE57NHBHXIZTVK', N'736f37bd-708d-4be9-a272-eb90bb30c1d6', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'ab3cefb3-8248-41c3-880e-22f030a59a47', N'admin@admin.com', N'ADMIN@ADMIN.COM', N'admin@admin.com', N'ADMIN@ADMIN.COM', 0, N'AQAAAAEAACcQAAAAECfIrppxYkSiGL/jKv9bxCMROyF9ZPtkE3Gz7UAN3dEsXQtrS0H2CaHzyAZUwx+NiA==', N'IVOZYHR43GFLYN5VJDGSMPMORX5DJ5VL', N'fc506505-2e1b-4988-b5a4-9b1a33304fa1', NULL, 0, 0, NULL, 1, 0)

INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'aafb003f-9835-4d64-bebf-202238a4e5fb', N'23067f05-aa32-4b08-b890-a07a87bab776')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5dd913ef-54e0-4e2b-9c63-bd7394a3b3eb', N'623ffe90-d30b-4b9f-8643-eb322f7f86b5')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ab3cefb3-8248-41c3-880e-22f030a59a47', N'bd36379e-9399-4b20-ad5e-b4ada7408579')

SET IDENTITY_INSERT [dbo].[Currency] ON 

INSERT [dbo].[Currency] ([Id], [Country], [Name], [IsoCode], [IsoNumber]) VALUES (1, N'GRECE', N'Euro', N'EUR', 987)
INSERT [dbo].[Currency] ([Id], [Country], [Name], [IsoCode], [IsoNumber]) VALUES (2, N'UNITED STATES OF AMERICA', N'US Dollar', N'USD', 840)
INSERT [dbo].[Currency] ([Id], [Country], [Name], [IsoCode], [IsoNumber]) VALUES (3, N'SWITZERLAND', N'Swiss Franc', N'CHF', 756)
INSERT [dbo].[Currency] ([Id], [Country], [Name], [IsoCode], [IsoNumber]) VALUES (4, N'UNITED KINGDOM OF GREAT BRITAIN AND NORTHERN IRELAND', N'British Pound', N'GBP', 826)
INSERT [dbo].[Currency] ([Id], [Country], [Name], [IsoCode], [IsoNumber]) VALUES (7, N'JAPAN', N'Yen', N'JPY', 392)
INSERT [dbo].[Currency] ([Id], [Country], [Name], [IsoCode], [IsoNumber]) VALUES (9, N'CANADA', N'Canadian Dollar', N'CAD', 124)

SET IDENTITY_INSERT [dbo].[Currency] OFF

SET IDENTITY_INSERT [dbo].[CurrencyExchangeRate] ON 

INSERT [dbo].[CurrencyExchangeRate] ([Id], [Rate], [From], [To], [SourceId], [TargetId]) VALUES (1, CAST(1.3764 AS Decimal(18, 4)), CAST(N'2020-02-10T00:00:00.0000000' AS DateTime2), CAST(N'2020-02-10T23:59:59.9999999' AS DateTime2), 1, 2)
INSERT [dbo].[CurrencyExchangeRate] ([Id], [Rate], [From], [To], [SourceId], [TargetId]) VALUES (2, CAST(1.2079 AS Decimal(18, 4)), CAST(N'2020-02-10T00:00:00.0000000' AS DateTime2), CAST(N'2020-02-10T23:59:59.9999999' AS DateTime2), 1, 3)
INSERT [dbo].[CurrencyExchangeRate] ([Id], [Rate], [From], [To], [SourceId], [TargetId]) VALUES (3, CAST(0.8731 AS Decimal(18, 4)), CAST(N'2020-02-10T00:00:00.0000000' AS DateTime2), CAST(N'2020-02-10T23:59:59.9999999' AS DateTime2), 1, 4)
INSERT [dbo].[CurrencyExchangeRate] ([Id], [Rate], [From], [To], [SourceId], [TargetId]) VALUES (6, CAST(76.7200 AS Decimal(18, 4)), CAST(N'2020-02-10T00:00:00.0000000' AS DateTime2), CAST(N'2020-02-10T23:59:59.9999999' AS DateTime2), 2, 7)
INSERT [dbo].[CurrencyExchangeRate] ([Id], [Rate], [From], [To], [SourceId], [TargetId]) VALUES (7, CAST(1.1379 AS Decimal(18, 4)), CAST(N'2020-02-10T00:00:00.0000000' AS DateTime2), CAST(N'2020-02-10T23:59:59.9999999' AS DateTime2), 3, 2)
INSERT [dbo].[CurrencyExchangeRate] ([Id], [Rate], [From], [To], [SourceId], [TargetId]) VALUES (8, CAST(1.5648 AS Decimal(18, 4)), CAST(N'2020-02-10T00:00:00.0000000' AS DateTime2), CAST(N'2020-02-10T23:59:59.9999999' AS DateTime2), 4, 9)

SET IDENTITY_INSERT [dbo].[CurrencyExchangeRate] OFF

SET IDENTITY_INSERT [dbo].[Language] ON

INSERT [dbo].[Language] ([Id], [LanguageCode], [LanguageName]) VALUES (1, N'EN', N'English')
INSERT [dbo].[Language] ([Id], [LanguageCode], [LanguageName]) VALUES (2, N'EL', N'Greek')

SET IDENTITY_INSERT [dbo].[Language] OFF

SET IDENTITY_INSERT [dbo].[LocalizedText] ON 

INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (1, 1, N'Localize_Key_Get_Exchange_Rate', N'Get Exchanges Rates')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (2, 1, N'Localize_Key_Put_Exchange_Rate', N'Update Exchanges Rates')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (3, 1, N'Localize_Key_Post_Exchange_Rate', N'Create Exchanges Rates')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (4, 2, N'Localize_Key_Get_Exchange_Rate', N'Φέρνει τις Τιμές Ανταλλαγής')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (5, 2, N'Localize_Key_Put_Exchange_Rate', N'Ανανεώνει Τιμές Ανταλλαγής')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (6, 2, N'Localize_Key_Post_Exchange_Rate', N'Δημιουργεί Τιμές Ανταλλαγής')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (7, 1, N'Localize_Key_Get_Currencies', N'Get the Currency')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (8, 1, N'Localize_Key_Put_Currencies', N'Update the Currency')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (9, 1, N'Localize_Key_Post_Currencies', N'Create the Currency')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (10, 2, N'Localize_Key_Get_Currencies', N'Φέρνει το Νόμισμα')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (11, 2, N'Localize_Key_Put_Currencies', N'Ανανεώνει το Νόμισμα')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (12, 2, N'Localize_Key_Post_Currencies', N'Δημιουργεί το Νόμισμα')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (13, 1, N'Localize_Key_Create_Token', N'Creates an access token for use of authorized actions')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (14, 1, N'Localize_Key_Create_User', N'Create User')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (15, 2, N'Localize_Key_Create_Token', N'Δημιουργεί token για χρήση εξουσιοδοτημένων ενεργειών')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (16, 2, N'Localize_Key_Create_User', N'Δημιουργεί χρήστη')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (17, 1, N'Localize_Key_Get_Exchange_Rate_Value', N'The value will be calculated with the rate for specific currencies')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (18, 1, N'Localize_Key_Get_Exchange_Rate_Day', N'Get the rate for this day')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (19, 1, N'Localize_Key_Sample_Request', N'Sample request for this day and with a specific day')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (20, 2, N'Localize_Key_Get_Exchange_Rate_Value', N'Η τιμή θα υπολογιστεί με την ισοτιμία για συγκεκριμένα νομίσματα')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (21, 2, N'Localize_Key_Get_Exchange_Rate_Day', N'Πάρτε την ισοτιμία για αυτήν την ημέρα')
INSERT [dbo].[LocalizedText] ([Id], [LanguageId], [Key], [Value]) VALUES (22, 2, N'Localize_Key_Sample_Request', N'Δείγμα αίτησης για σήμερα ή για συγκεκριμένη ημερομηνία')

SET IDENTITY_INSERT [dbo].[LocalizedText] OFF