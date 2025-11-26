-- Thêm cột Username vào bảng YEUCAU_DICHVU và LIENHE
USE GaraOToManagement;
GO

-- Thêm cột Username vào YEUCAU_DICHVU
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'YEUCAU_DICHVU') AND name = 'Username')
BEGIN
    ALTER TABLE YEUCAU_DICHVU ADD Username NVARCHAR(50) NULL;
    PRINT 'Đã thêm cột Username vào YEUCAU_DICHVU';
END
ELSE
BEGIN
    PRINT 'Cột Username đã tồn tại trong YEUCAU_DICHVU';
END
GO

-- Thêm cột Username vào LIENHE
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'LIENHE') AND name = 'Username')
BEGIN
    ALTER TABLE LIENHE ADD Username NVARCHAR(50) NULL;
    PRINT 'Đã thêm cột Username vào LIENHE';
END
ELSE
BEGIN
    PRINT 'Cột Username đã tồn tại trong LIENHE';
END
GO

-- Xem kết quả
SELECT TOP 5 * FROM YEUCAU_DICHVU;
SELECT TOP 5 * FROM LIENHE;
GO
