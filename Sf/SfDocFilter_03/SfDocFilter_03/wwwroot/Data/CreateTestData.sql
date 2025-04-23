-- Script Date: 22.04.2025 14:41  - ErikEJ.SqlCeScripting version 3.5.2.95
CREATE TABLE [TestData] (
  [TestDataId] INTEGER PRIMARY KEY   AUTOINCREMENT
, [first_name] nvarchar(50) NULL COLLATE NOCASE
, [last_name] nvarchar(50) NULL COLLATE NOCASE
, [full_name] nvarchar(100) NULL COLLATE NOCASE
, [birth_date] datetime NULL
, [gender] nvarchar(10) NULL COLLATE NOCASE
, [nationality] nvarchar(100) NULL COLLATE NOCASE
, [occupation] nvarchar(100) NULL COLLATE NOCASE
, [marital_status] nvarchar(20) NULL COLLATE NOCASE
, [street_address] nvarchar(100) NULL COLLATE NOCASE
, [city] nvarchar(50) NULL COLLATE NOCASE
, [state] nvarchar(50) NULL COLLATE NOCASE
, [country] nvarchar(100) NULL COLLATE NOCASE
, [postal_code] nvarchar(20) NULL COLLATE NOCASE
, [phone_number] nvarchar(50) NULL COLLATE NOCASE
, [email] nvarchar(100) NULL COLLATE NOCASE
, [first_name1] nvarchar(50) NULL COLLATE NOCASE
, [last_name1] nvarchar(50) NULL COLLATE NOCASE
, [document_number] uniqueidentifier NULL
, [document_type] nvarchar(50) NULL COLLATE NOCASE
, [issue_date] datetime NULL
, [expiry_date] datetime NULL
);
