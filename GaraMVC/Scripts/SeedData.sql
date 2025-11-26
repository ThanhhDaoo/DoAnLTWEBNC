-- =============================================
-- SEED DATA CHO GARA MVC - TEST DASHBOARD
-- =============================================

-- Xóa dữ liệu cũ (theo thứ tự FK)
DELETE FROM ChiTietHDSP;
DELETE FROM ChiTietHDDV;
DELETE FROM HoaDon;
DELETE FROM YeucauDichVu;
DELETE FROM Xe;
DELETE FROM KhachHang;
DELETE FROM NhanVien;
DELETE FROM DichVu;
DELETE FROM SanPham;

-- Reset Identity
DBCC CHECKIDENT ('KhachHang', RESEED, 0);
DBCC CHECKIDENT ('Xe', RESEED, 0);
DBCC CHECKIDENT ('NhanVien', RESEED, 0);
DBCC CHECKIDENT ('DichVu', RESEED, 0);
DBCC CHECKIDENT ('SanPham', RESEED, 0);
DBCC CHECKIDENT ('HoaDon', RESEED, 0);
DBCC CHECKIDENT ('ChiTietHDDV', RESEED, 0);
DBCC CHECKIDENT ('ChiTietHDSP', RESEED, 0);

-- =============================================
-- 1. NHÂN VIÊN (5 người)
-- =============================================
INSERT INTO NhanVien (TenNV, ChucVu, SDT, Email, VaiTro) VALUES
(N'Nguyễn Văn Admin', N'Quản lý', '0901234567', 'admin@mtproauto.com', 'Admin'),
(N'Trần Thị Hoa', N'Kế toán', '0912345678', 'hoa.tran@mtproauto.com', 'NhanVien'),
(N'Lê Văn Minh', N'Kỹ thuật viên', '0923456789', 'minh.le@mtproauto.com', 'NhanVien'),
(N'Phạm Đức Anh', N'Kỹ thuật viên', '0934567890', 'anh.pham@mtproauto.com', 'NhanVien'),
(N'Hoàng Thu Hằng', N'Lễ tân', '0945678901', 'hang.hoang@mtproauto.com', 'NhanVien');

-- =============================================
-- 2. DỊCH VỤ (10 dịch vụ)
-- =============================================
INSERT INTO DichVu (TenDichVu, Gia, MoTa, TrangThai) VALUES
(N'Thay dầu máy', 350000, N'Thay dầu nhớt động cơ định kỳ', 1),
(N'Rửa xe cao cấp', 150000, N'Rửa xe bằng máy, hút bụi nội thất', 1),
(N'Bảo dưỡng định kỳ', 1500000, N'Kiểm tra tổng quát và bảo dưỡng xe', 1),
(N'Thay lọc gió', 200000, N'Thay lọc gió động cơ', 1),
(N'Thay lọc điều hòa', 250000, N'Thay lọc gió điều hòa', 1),
(N'Kiểm tra ắc quy', 100000, N'Kiểm tra và bảo dưỡng ắc quy', 1),
(N'Cân chỉnh góc lái', 300000, N'Cân chỉnh góc đặt bánh xe', 1),
(N'Thay má phanh', 800000, N'Thay má phanh trước/sau', 1),
(N'Vệ sinh kim phun', 500000, N'Vệ sinh hệ thống phun xăng', 1),
(N'Thay dầu hộp số', 450000, N'Thay dầu hộp số tự động/số sàn', 1);

-- =============================================
-- 3. SẢN PHẨM (15 sản phẩm)
-- =============================================
INSERT 
INTO SanPham (TenSanPham, Gia, SoLuongTon, DonVi, MoTa) VALUES
(N'Dầu nhớt Castrol 5W-40', 450000, 50, N'Lít', N'Dầu nhớt tổng hợp cao cấp'),
(N'Dầu nhớt Shell Helix 10W-40', 380000, 45, N'Lít', N'Dầu nhớt bán tổng hợp'),
(N'Lọc gió động cơ Toyota', 350000, 30, N'Cái', N'Lọc gió chính hãng Toyota'),
(N'Lọc gió động cơ Honda', 320000, 25, N'Cái', N'Lọc gió chính hãng Honda'),
(N'Lọc điều hòa Denso', 280000, 40, N'Cái', N'Lọc điều hòa cao cấp Denso'),
(N'Ắc quy GS 12V 45Ah', 1850000, 15, N'Cái', N'Ắc quy khô GS chính hãng'),
(N'Ắc quy Varta 12V 60Ah', 2200000, 10, N'Cái', N'Ắc quy Varta nhập khẩu'),
(N'Bóng đèn LED Philips H4', 650000, 35, N'Bộ', N'Bóng đèn LED siêu sáng'),
(N'Bóng đèn Halogen H7', 250000, 50, N'Cái', N'Bóng đèn halogen tiêu chuẩn'),
(N'Má phanh Brembo', 1200000, 20, N'Bộ', N'Má phanh cao cấp Brembo'),
(N'Má phanh TRW', 800000, 25, N'Bộ', N'Má phanh TRW chính hãng'),
(N'Nước làm mát Prestone', 180000, 60, N'Lít', N'Nước làm mát chống đông'),
(N'Dầu phanh DOT4', 120000, 40, N'Chai', N'Dầu phanh tiêu chuẩn DOT4'),
(N'Bugi NGK Iridium', 350000, 8, N'Cái', N'Bugi cao cấp NGK Iridium'),
(N'Dây curoa Gates', 450000, 5, N'Cái', N'Dây curoa Gates chính hãng');

-- =============================================
-- 4. KHÁCH HÀNG (20 khách hàng)
-- =============================================
INSERT INTO KhachHang (TenKH, SDT, DiaChi, Email, NgayDangKy) VALUES
(N'Nguyễn Văn An', '0901111111', N'123 Nguyễn Huệ, Q1, TP.HCM', 'an.nguyen@gmail.com', '2024-01-15'),
(N'Trần Thị Bình', '0902222222', N'456 Lê Lợi, Q3, TP.HCM', 'binh.tran@gmail.com', '2024-01-20'),
(N'Lê Văn Cường', '0903333333', N'789 Hai Bà Trưng, Q1, TP.HCM', 'cuong.le@gmail.com', '2024-02-05'),
(N'Phạm Thị Dung', '0904444444', N'321 Điện Biên Phủ, Q.Bình Thạnh', 'dung.pham@gmail.com', '2024-02-10'),
(N'Hoàng Văn Em', '0905555555', N'654 Cách Mạng Tháng 8, Q10', 'em.hoang@gmail.com', '2024-02-20'),
(N'Vũ Thị Phương', '0906666666', N'987 Nguyễn Thị Minh Khai, Q3', 'phuong.vu@gmail.com', '2024-03-01'),
(N'Đặng Văn Giang', '0907777777', N'147 Võ Văn Tần, Q3', 'giang.dang@gmail.com', '2024-03-15'),
(N'Bùi Thị Hạnh', '0908888888', N'258 Pasteur, Q1', 'hanh.bui@gmail.com', '2024-03-25'),
(N'Ngô Văn Inh', '0909999999', N'369 Nam Kỳ Khởi Nghĩa, Q3', 'inh.ngo@gmail.com', '2024-04-05'),
(N'Lý Thị Kim', '0910000000', N'741 Trần Hưng Đạo, Q5', 'kim.ly@gmail.com', '2024-04-15'),
(N'Trịnh Văn Long', '0911111111', N'852 Nguyễn Trãi, Q5', 'long.trinh@gmail.com', '2024-05-01'),
(N'Mai Thị Mỹ', '0912222222', N'963 Lý Thường Kiệt, Q10', 'my.mai@gmail.com', '2024-05-10'),
(N'Đinh Văn Nam', '0913333333', N'159 Nguyễn Đình Chiểu, Q3', 'nam.dinh@gmail.com', '2024-06-01'),
(N'Cao Thị Oanh', '0914444444', N'357 Võ Thị Sáu, Q3', 'oanh.cao@gmail.com', '2024-06-15'),
(N'Tạ Văn Phúc', '0915555555', N'468 Đinh Tiên Hoàng, Q.Bình Thạnh', 'phuc.ta@gmail.com', '2024-07-01'),
(N'Dương Thị Quỳnh', '0916666666', N'579 Xô Viết Nghệ Tĩnh, Q.Bình Thạnh', 'quynh.duong@gmail.com', '2024-07-20'),
(N'Hồ Văn Rồng', '0917777777', N'680 Phan Xích Long, Q.Phú Nhuận', 'rong.ho@gmail.com', '2024-08-05'),
(N'Phan Thị Sen', '0918888888', N'791 Hoàng Văn Thụ, Q.Tân Bình', 'sen.phan@gmail.com', '2024-08-20'),
(N'Châu Văn Tài', '0919999999', N'802 Cộng Hòa, Q.Tân Bình', 'tai.chau@gmail.com', '2024-09-01'),
(N'Lâm Thị Uyên', '0920000000', N'913 Trường Chinh, Q.Tân Bình', 'uyen.lam@gmail.com', '2024-09-15');


-- =============================================
-- 5. XE (25 xe)
-- =============================================
INSERT INTO Xe (BienSo, HangXe, DoiXe, MauXe, MaKH) VALUES
('51A-12345', 'Toyota Camry', 2022, N'Đen', 1),
('51A-23456', 'Honda Civic', 2021, N'Trắng', 1),
('51B-34567', 'Mazda CX-5', 2023, N'Đỏ', 2),
('51C-45678', 'Hyundai Tucson', 2022, N'Xanh', 3),
('51D-56789', 'Kia Seltos', 2023, N'Bạc', 4),
('51E-67890', 'Ford Ranger', 2021, N'Đen', 5),
('51F-78901', 'Toyota Vios', 2020, N'Trắng', 6),
('51G-89012', 'Honda CR-V', 2022, N'Xám', 7),
('51H-90123', 'Mazda 3', 2023, N'Đỏ', 8),
('51K-01234', 'VinFast VF8', 2023, N'Xanh', 9),
('51L-11111', 'Mercedes C200', 2022, N'Đen', 10),
('51M-22222', 'BMW 320i', 2021, N'Trắng', 11),
('51N-33333', 'Audi A4', 2022, N'Xám', 12),
('51P-44444', 'Toyota Fortuner', 2023, N'Đen', 13),
('51Q-55555', 'Mitsubishi Xpander', 2022, N'Bạc', 14),
('51R-66666', 'Suzuki XL7', 2021, N'Trắng', 15),
('51S-77777', 'Nissan X-Trail', 2022, N'Xanh', 16),
('51T-88888', 'Peugeot 3008', 2023, N'Đen', 17),
('51U-99999', 'Subaru Forester', 2022, N'Xám', 18),
('51V-00000', 'Lexus RX350', 2023, N'Trắng', 19),
('51X-11122', 'Toyota Corolla Cross', 2023, N'Đỏ', 20),
('51Y-22233', 'Honda HR-V', 2022, N'Bạc', 1),
('51Z-33344', 'Mazda CX-8', 2023, N'Đen', 2),
('30A-44455', 'Hyundai Santa Fe', 2022, N'Trắng', 3),
('30B-55566', 'Kia Carnival', 2023, N'Xám', 4);

-- =============================================
-- 6. HÓA ĐƠN (50 hóa đơn từ tháng 1-11/2024)
-- =============================================
-- Tháng 1
INSERT INTO HoaDon (MaKH, MaXe, MaNV, NgayLap, TongTien, HinhThucTT, TrangThai) VALUES
(1, 1, 1, '2024-01-15', 850000, N'Tiền mặt', N'Đã thanh toán'),
(2, 3, 2, '2024-01-20', 1650000, N'Chuyển khoản', N'Đã thanh toán'),
(3, 4, 3, '2024-01-25', 500000, N'Tiền mặt', N'Đã thanh toán');

-- Tháng 2
INSERT INTO HoaDon (MaKH, MaXe, MaNV, NgayLap, TongTien, HinhThucTT, TrangThai) VALUES
(4, 5, 1, '2024-02-05', 2350000, N'Thẻ', N'Đã thanh toán'),
(5, 6, 2, '2024-02-10', 750000, N'Tiền mặt', N'Đã thanh toán'),
(6, 7, 3, '2024-02-15', 1200000, N'Chuyển khoản', N'Đã thanh toán'),
(7, 8, 4, '2024-02-20', 450000, N'Tiền mặt', N'Đã thanh toán');

-- Tháng 3
INSERT INTO HoaDon (MaKH, MaXe, MaNV, NgayLap, TongTien, HinhThucTT, TrangThai) VALUES
(8, 9, 1, '2024-03-01', 3500000, N'Chuyển khoản', N'Đã thanh toán'),
(9, 10, 2, '2024-03-10', 650000, N'Tiền mặt', N'Đã thanh toán'),
(10, 11, 3, '2024-03-15', 1850000, N'Thẻ', N'Đã thanh toán'),
(1, 1, 4, '2024-03-20', 500000, N'Tiền mặt', N'Đã thanh toán'),
(2, 3, 1, '2024-03-25', 2200000, N'Chuyển khoản', N'Đã thanh toán');

-- Tháng 4
INSERT INTO HoaDon (MaKH, MaXe, MaNV, NgayLap, TongTien, HinhThucTT, TrangThai) VALUES
(11, 12, 2, '2024-04-05', 4500000, N'Chuyển khoản', N'Đã thanh toán'),
(12, 13, 3, '2024-04-10', 850000, N'Tiền mặt', N'Đã thanh toán'),
(13, 14, 4, '2024-04-15', 1500000, N'Thẻ', N'Đã thanh toán'),
(14, 15, 1, '2024-04-20', 750000, N'Tiền mặt', N'Đã thanh toán'),
(15, 16, 2, '2024-04-25', 2800000, N'Chuyển khoản', N'Đã thanh toán');

-- Thá
ng 5
INSERT INTO HoaDon (MaKH, MaXe, MaNV, NgayLap, TongTien, HinhThucTT, TrangThai) VALUES
(16, 17, 3, '2024-05-01', 1200000, N'Tiền mặt', N'Đã thanh toán'),
(17, 18, 4, '2024-05-08', 3200000, N'Chuyển khoản', N'Đã thanh toán'),
(18, 19, 1, '2024-05-15', 650000, N'Tiền mặt', N'Đã thanh toán'),
(19, 20, 2, '2024-05-20', 5500000, N'Thẻ', N'Đã thanh toán'),
(20, 21, 3, '2024-05-25', 950000, N'Tiền mặt', N'Đã thanh toán'),
(1, 22, 4, '2024-05-30', 1800000, N'Chuyển khoản', N'Đã thanh toán');

-- Tháng 6
INSERT INTO HoaDon (MaKH, MaXe, MaNV, NgayLap, TongTien, HinhThucTT, TrangThai) VALUES
(2, 23, 1, '2024-06-05', 2500000, N'Chuyển khoản', N'Đã thanh toán'),
(3, 24, 2, '2024-06-10', 750000, N'Tiền mặt', N'Đã thanh toán'),
(4, 25, 3, '2024-06-15', 4200000, N'Thẻ', N'Đã thanh toán'),
(5, 6, 4, '2024-06-20', 550000, N'Tiền mặt', N'Đã thanh toán'),
(6, 7, 1, '2024-06-25', 1650000, N'Chuyển khoản', N'Đã thanh toán'),
(7, 8, 2, '2024-06-30', 3800000, N'Thẻ', N'Đã thanh toán');

-- Tháng 7
INSERT INTO HoaDon (MaKH, MaXe, MaNV, NgayLap, TongTien, HinhThucTT, TrangThai) VALUES
(8, 9, 3, '2024-07-05', 1250000, N'Tiền mặt', N'Đã thanh toán'),
(9, 10, 4, '2024-07-10', 2850000, N'Chuyển khoản', N'Đã thanh toán'),
(10, 11, 1, '2024-07-15', 650000, N'Tiền mặt', N'Đã thanh toán'),
(11, 12, 2, '2024-07-20', 4500000, N'Thẻ', N'Đã thanh toán'),
(12, 13, 3, '2024-07-25', 950000, N'Tiền mặt', N'Đã thanh toán'),
(13, 14, 4, '2024-07-30', 1750000, N'Chuyển khoản', N'Đã thanh toán');

-- Tháng 8
INSERT INTO HoaDon (MaKH, MaXe, MaNV, NgayLap, TongTien, HinhThucTT, TrangThai) VALUES
(14, 15, 1, '2024-08-05', 3200000, N'Chuyển khoản', N'Đã thanh toán'),
(15, 16, 2, '2024-08-10', 850000, N'Tiền mặt', N'Đã thanh toán'),
(16, 17, 3, '2024-08-15', 5200000, N'Thẻ', N'Đã thanh toán'),
(17, 18, 4, '2024-08-20', 450000, N'Tiền mặt', N'Đã thanh toán'),
(18, 19, 1, '2024-08-25', 2100000, N'Chuyển khoản', N'Đã thanh toán'),
(19, 20, 2, '2024-08-30', 1550000, N'Tiền mặt', N'Đã thanh toán');

-- Tháng 9
INSERT INTO HoaDon (MaKH, MaXe, MaNV, NgayLap, TongTien, HinhThucTT, TrangThai) VALUES
(20, 21, 3, '2024-09-05', 2750000, N'Chuyển khoản', N'Đã thanh toán'),
(1, 1, 4, '2024-09-10', 650000, N'Tiền mặt', N'Đã thanh toán'),
(2, 3, 1, '2024-09-15', 3850000, N'Thẻ', N'Đã thanh toán'),
(3, 4, 2, '2024-09-20', 1200000, N'Tiền mặt', N'Đã thanh toán'),
(4, 5, 3, '2024-09-25', 4500000, N'Chuyển khoản', N'Đã thanh toán'),
(5, 6, 4, '2024-09-30', 750000, N'Tiền mặt', N'Đã thanh toán');

-- Tháng 10
INSERT INTO HoaDon (MaKH, MaXe, MaNV, NgayLap, TongTien, HinhThucTT, TrangThai) VALUES
(6, 7, 1, '2024-10-05', 2350000, N'Chuyển khoản', N'Đã thanh toán'),
(7, 8, 2, '2024-10-10', 950000, N'Tiền mặt', N'Đã thanh toán'),
(8, 9, 3, '2024-10-15', 5800000, N'Thẻ', N'Đã thanh toán'),
(9, 10, 4, '2024-10-20', 1450000, N'Tiền mặt', N'Đã thanh toán'),
(10, 11, 1, '2024-10-25', 3200000, N'Chuyển khoản', N'Đã thanh toán'),
(11, 12, 2, '2024-10-30', 850000, N'Tiền mặt', N'Đã thanh toán');