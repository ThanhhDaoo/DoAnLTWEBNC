-- Bổ sung câu lệnh if exists
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'GaraOToManagement')
BEGIN
    USE master;
    ALTER DATABASE GaraOToManagement SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE GaraOToManagement;
END
GO

CREATE DATABASE GaraOToManagement;
GO

USE GaraOToManagement;
GO

-- Bảng Users để quản lý đăng nhập và phân quyền
CREATE TABLE USERS (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(200) NOT NULL,
    Email NVARCHAR(100),
    Role NVARCHAR(20) NOT NULL DEFAULT 'Customer', -- Admin/Customer
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Bảng Khách hàng
CREATE TABLE KHACHHANG (
    MaKH INT PRIMARY KEY IDENTITY(1,1),
    TenKH NVARCHAR(100) NOT NULL,
    SDT VARCHAR(15) NOT NULL,
    DiaChi NVARCHAR(200),
    Email VARCHAR(100),
    NgayDangKy DATETIME DEFAULT GETDATE(),
    UserId INT,
    FOREIGN KEY (UserId) REFERENCES USERS(UserId)
);

-- Bảng Xe
CREATE TABLE XE (
    MaXe INT PRIMARY KEY IDENTITY(1,1),
    BienSo VARCHAR(20) NOT NULL UNIQUE,
    HangXe NVARCHAR(50),
    DoiXe INT,
    MauXe NVARCHAR(30),
    MaKH INT NOT NULL,
    FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH)
);

-- Bảng Dịch vụ
CREATE TABLE DICHVU (
    MaDV INT PRIMARY KEY IDENTITY(1,1),
    TenDV NVARCHAR(100) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,
    MoTa NVARCHAR(500),
    TrangThai BIT DEFAULT 1 -- 1: Còn cung cấp, 0: Ngừng
);
-- Bổ sung trường hình ảnh cho dịch vụ 
ALTER TABLE DICHVU ADD HinhAnh NVARCHAR(255) NULL;


-- Bảng Yêu cầu dịch vụ
CREATE TABLE YEUCAU_DICHVU (
    MaYeuCau INT PRIMARY KEY IDENTITY(1,1),
    TenKhachHang NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(20) NOT NULL,
    DiaChi NVARCHAR(500) NOT NULL,
    MaDV INT NOT NULL,
    GhiChu NVARCHAR(1000),
    NgayYeuCau DATETIME DEFAULT GETDATE(),
    TrangThai NVARCHAR(20) DEFAULT 'Mới',
    FOREIGN KEY (MaDV) REFERENCES DICHVU(MaDV)
);

-- Bổ sung cột ngày giờ hẹn cho yêu cầu dịch vụ (fix đồng bộ API)
ALTER TABLE YEUCAU_DICHVU
ADD NgayHen DATETIME NULL, GioHen NVARCHAR(10) NULL;

-- Bảng Sản phẩm
CREATE TABLE SANPHAM (
    MaSP INT PRIMARY KEY IDENTITY(1,1),
    TenSP NVARCHAR(100) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,
    SoLuongTon INT DEFAULT 0,
    DonVi NVARCHAR(20),
    MoTa NVARCHAR(500)
);

-- Bổ sung trường hình ảnh cho  sản phẩm
ALTER TABLE SANPHAM ADD HinhAnh NVARCHAR(255) NULL;


-- Bảng Liên hệ
CREATE TABLE LIENHE (
    MaLienHe INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(20) NOT NULL,
    ChuDe NVARCHAR(100),
    NoiDung NVARCHAR(MAX),
    NgayGui DATETIME DEFAULT GETDATE(),
    DaXuLy BIT DEFAULT 0         -- 0: chưa xử lý, 1: đã xử lý
);



-- Bảng Hóa đơn
CREATE TABLE HOADON (
    MaHD INT PRIMARY KEY IDENTITY(1,1),
    MaKH INT NOT NULL,
    MaXe INT NOT NULL,
    UserId INT NOT NULL, -- Người tạo hóa đơn (thay cho MaNV)
    NgayLap DATETIME DEFAULT GETDATE(),
    TongTien DECIMAL(18,2) DEFAULT 0,
    HinhThucTT NVARCHAR(50), -- Tiền mặt, Thẻ, Chuyển khoản
    TrangThai NVARCHAR(30) DEFAULT N'Chờ xác nhận', -- Chờ xác nhận, Đang xử lý, Hoàn thành, Đã hủy
    FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH),
    FOREIGN KEY (MaXe) REFERENCES XE(MaXe),
    FOREIGN KEY (UserId) REFERENCES USERS(UserId)
);

-- Chi tiết hóa đơn - Dịch vụ
CREATE TABLE CHITIET_HDDV (
    MaCTDV INT PRIMARY KEY IDENTITY(1,1),
    MaHD INT NOT NULL,
    MaDV INT NOT NULL,
    SoLuong INT DEFAULT 1,
    DonGia DECIMAL(18,2),
    ThanhTien AS (SoLuong * DonGia),
    FOREIGN KEY (MaHD) REFERENCES HOADON(MaHD),
    FOREIGN KEY (MaDV) REFERENCES DICHVU(MaDV)
);

-- Chi tiết hóa đơn - Sản phẩm
CREATE TABLE CHITIET_HDSP (
    MaCTSP INT PRIMARY KEY IDENTITY(1,1),
    MaHD INT NOT NULL,
    MaSP INT NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18,2),
    ThanhTien AS (SoLuong * DonGia),
    FOREIGN KEY (MaHD) REFERENCES HOADON(MaHD),
    FOREIGN KEY (MaSP) REFERENCES SANPHAM(MaSP)
);

-- Tạo các indexes
CREATE INDEX IX_USERS_Username ON USERS(Username);
CREATE INDEX IX_KHACHHANG_SDT ON KHACHHANG(SDT);
CREATE INDEX IX_XE_BIENSO ON XE(BienSo);
CREATE INDEX IX_HOADON_NGAYLAP ON HOADON(NgayLap);

-- Tạo triggers
GO
CREATE TRIGGER TRG_CapNhatTongTien_SP
ON CHITIET_HDSP
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @MaHD INT;
    
    SELECT @MaHD = MaHD FROM inserted;
    IF @MaHD IS NULL
        SELECT @MaHD = MaHD FROM deleted;
        
    UPDATE HOADON
    SET TongTien = (
        SELECT ISNULL(SUM(ThanhTien), 0)
        FROM CHITIET_HDSP
        WHERE MaHD = @MaHD
    ) + (
        SELECT ISNULL(SUM(ThanhTien), 0)
        FROM CHITIET_HDDV
        WHERE MaHD = @MaHD
    )
    WHERE MaHD = @MaHD;
END;
GO

CREATE TRIGGER TRG_CapNhatTonKho
ON CHITIET_HDSP
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        JOIN SANPHAM s ON i.MaSP = s.MaSP
        WHERE s.SoLuongTon < i.SoLuong
    )
    BEGIN
        RAISERROR (N'Số lượng tồn không đủ!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
    
    UPDATE s
    SET SoLuongTon = s.SoLuongTon - i.SoLuong
    FROM SANPHAM s
    JOIN inserted i ON s.MaSP = i.MaSP;
END;
GO

-- Stored Procedures
CREATE PROCEDURE sp_DangKyKhachHang
    @Username NVARCHAR(50),
    @Password NVARCHAR(200),
    @Email NVARCHAR(100),
    @HoTen NVARCHAR(100),
    @SDT VARCHAR(15),
    @DiaChi NVARCHAR(200)
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Thêm user mới
        INSERT INTO USERS (Username, Password, Email, Role)
        VALUES (@Username, @Password, @Email, 'Customer');
        
        DECLARE @UserId INT = SCOPE_IDENTITY();
        
        -- Thêm thông tin khách hàng
        INSERT INTO KHACHHANG (TenKH, SDT, DiaChi, Email, UserId)
        VALUES (@HoTen, @SDT, @DiaChi, @Email, @UserId);
        
        COMMIT;
        SELECT 'Success' AS Result;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        SELECT ERROR_MESSAGE() AS Result;
    END CATCH;
END;
GO

CREATE PROCEDURE sp_DangNhap
    @Username NVARCHAR(50),
    @Password NVARCHAR(200)
AS
BEGIN
    SELECT 
        u.UserId,
        u.Username,
        u.Email,
        u.Role,
        k.MaKH,
        k.TenKH
    FROM USERS u
    LEFT JOIN KHACHHANG k ON u.UserId = k.UserId
    WHERE u.Username = @Username 
    AND u.Password = @Password
    AND u.IsActive = 1;
END;
GO

-- Thêm dữ liệu mẫu
-- Admin user (password: 123456 hashed with SHA256)
INSERT INTO USERS (Username, Password, Email, Role) VALUES
('admin', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'admin@gara.com', 'Admin');

-- Customer users (password: 123456 hashed with SHA256)
INSERT INTO USERS (Username, Password, Email, Role) VALUES
('customer1', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'customer1@gmail.com', 'Customer'),
('customer2', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'customer2@gmail.com', 'Customer');

-- Khách hàng
INSERT INTO KHACHHANG (TenKH, SDT, DiaChi, Email, UserId) VALUES
(N'Nguyễn Văn A', '0901234567', N'Hà Nội', 'customer1@gmail.com', 2),
(N'Trần Thị B', '0912345678', N'TP.HCM', 'customer2@gmail.com', 3);

-- Xe
INSERT INTO XE (BienSo, HangXe, DoiXe, MauXe, MaKH) VALUES
('30A-12345', N'Toyota', 2020, N'Trắng', 1),
('51B-67890', N'Honda', 2019, N'Đen', 2);

-- Insert dữ liệu mẫu cho DICHVU với hình ảnh
INSERT INTO DICHVU (TenDV, DonGia, MoTa, HinhAnh) VALUES
(N'Thay dầu động cơ', 1500000, N'Thay dầu nhớt động cơ và lọc dầu cho ô tô', 'https://images.pexels.com/photos/13065690/pexels-photo-13065690.jpeg'),
(N'Rửa xe', 150000, N'Rửa xe và hút bụi nội thất cơ bản cho ô tô', 'https://images.pexels.com/photos/6872156/pexels-photo-6872156.jpeg'),
(N'Bảo dưỡng định kỳ', 1500000, N'Kiểm tra tổng quát và thay thế phụ tùng hao mòn theo km', 'https://images.pexels.com/photos/8986070/pexels-photo-8986070.jpeg'),
(N'Vá lốp/Cân mâm', 300000, N'Vá lốp, cân bằng động bánh xe ô tô', 'https://images.pexels.com/photos/3807386/pexels-photo-3807386.jpeg'),
(N'Vệ sinh nội thất', 1200000, N'Làm sạch nội thất xe ô tô chuyên sâu', 'https://images.pexels.com/photos/6873185/pexels-photo-6873185.jpeg'),
(N'Đánh bóng/Hiệu chỉnh sơn', 2500000, N'Xử lý các vết xước và hiệu chỉnh sơn mới', 'https://images.pexels.com/photos/6026083/pexels-photo-6026083.jpeg'),
(N'Thay ắc quy', 800000, N'Kiểm tra và thay thế ắc quy cho xe', 'https://images.pexels.com/photos/5572265/pexels-photo-5572265.jpeg'),
(N'Sửa chữa phanh', 750000, N'Kiểm tra, thay má phanh, tiện đĩa phanh', 'https://images.pexels.com/photos/3642618/pexels-photo-3642618.jpeg'),
(N'Vệ sinh khoang máy', 500000, N'Làm sạch dầu mỡ và bụi bẩn trong khoang động cơ', 'https://images.pexels.com/photos/8478224/pexels-photo-8478224.jpeg');
GO
-- Insert dữ liệu mẫu cho SANPHAM với hình ảnh
INSERT INTO SANPHAM (TenSP, DonGia, SoLuongTon, DonVi, MoTa, HinhAnh) VALUES
(N'Dầu nhớt Shell 5W30', 250000, 50, N'Chai', N'Dầu tổng hợp, dùng cho động cơ xăng.', 'https://cdn2.fptshop.com.vn/unsafe/800x0/Castrol_5_W30_3_2bc030e832.jpg'),
(N'Lọc gió động cơ', 80000, 100, N'Cái', N'Lọc bụi bẩn, kéo dài tuổi thọ động cơ.', 'https://otohoangkim-storage.sgp1.cdn.digitaloceanspaces.com/loc-gio-dong-co.webp'),
(N'Bóng đèn LED', 150000, 30, N'Bộ', N'Đèn pha LED độ sáng cao.', 'https://images.pexels.com/photos/2127020/pexels-photo-2127020.jpeg'),
(N'Nước làm mát 4L', 180000, 40, N'Can', N'Dung dịch tản nhiệt, chống đông.', 'https://danchoioto.vn/wp-content/uploads/2021/05/buoc-3-thay-nuoc-lam-mat-moi.jpeg.webp'),
(N'Má phanh trước', 650000, 25, N'Bộ', N'Má phanh gốm, hiệu suất phanh tốt.', 'https://images.pexels.com/photos/248370/pexels-photo-248370.jpeg'),
(N'Bugi động cơ', 120000, 60, N'Cái', N'Bugi đánh lửa Iridium.', 'https://ranone.vn/wp-content/uploads/2024/07/bugi-4-cay.jpg'),
(N'Lọc dầu', 100000, 80, N'Cái', N'Lọc cặn bẩn trong dầu.', 'https://danchoioto.vn/wp-content/uploads/2021/03/loc-dau-o-to-3.jpg.webp'),
(N'Dung dịch rửa kính', 50000, 120, N'Chai', N'Làm sạch kính chắn gió.', 'https://images.pexels.com/photos/6872603/pexels-photo-6872603.jpeg'),
(N'Lốp xe ô tô R16', 1800000, 15, N'Cái', N'Lốp xe du lịch, 205/55 R16.', 'https://images.pexels.com/photos/6870331/pexels-photo-6870331.jpeg');
GO
