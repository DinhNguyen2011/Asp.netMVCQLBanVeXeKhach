-- Tạo database QlDatVe
Create DATABASE QlDatVe;
USE QlDatVe;

-- Bảng NguoiDung (Thông tin người dùng)
CREATE TABLE NguoiDung (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(255) UNIQUE NOT NULL,
    SDT NVARCHAR(20),
    HoTen NVARCHAR(100),
    MatKhau NVARCHAR(255) NOT NULL,
    LoaiNguoiDung NVARCHAR(50), -- VD: Admin, KhachHang
    NgaySinh DATE,
    DiaChi NVARCHAR(255)
);

-- Bảng TuyenXe (Quản lý tuyến xe)
CREATE TABLE TuyenXe (
    MaTuyen INT PRIMARY KEY IDENTITY(1,1),
    DiemDi NVARCHAR(100) NOT NULL,
    DiemDen NVARCHAR(100) NOT NULL,
    SoNgayChayTrongTuan INT,
    GiaHienHanh DECIMAL(18,2),
    QuangDuong INT
);






-- Bảng ChuyenXe (Quản lý chuyến xe)
CREATE TABLE ChuyenXe (
    MaChuyen INT PRIMARY KEY IDENTITY(1,1),
    MaTuyen INT FOREIGN KEY REFERENCES TuyenXe(MaTuyen),
    ThoiDiemKhoiHanh DATETIME,
    ThoiDiemDenDuKien DATETIME,
    GiaVe DECIMAL(18,2),
    BienSoXe NVARCHAR(50) -- Biển số của xe được chọn cho chuyến
);

-- Bảng loaixe (Loại xe)
CREATE TABLE loaixe (
    ID_LOAI INT PRIMARY KEY,
    TENLOAI NVARCHAR(50),
    SOGHE INT NOT NULL
);

-- Bảng Xe (Thông tin về các xe)
CREATE TABLE Xe (
    BIENSO NVARCHAR(15) PRIMARY KEY,
    ID_LOAI INT NOT NULL,
    TENXE NVARCHAR(50),
    FOREIGN KEY (ID_LOAI) REFERENCES loaixe(ID_LOAI)
);

-- Bảng vitrighe (Vị trí ghế trên xe)
CREATE TABLE Vitrighe (
    ID_VITRI INT PRIMARY KEY,
    BIENSO NVARCHAR(15) NOT NULL,
    TENVITRI NVARCHAR(50),
    TRANGTHAI BIT,
    FOREIGN KEY (BIENSO) REFERENCES Xe(BIENSO)
);


-- Bảng PhieuDatVe (Thông tin phiếu đặt vé)
CREATE TABLE PhieuDatVe (
    MaPhieu INT PRIMARY KEY IDENTITY(1,1),
  --  MaHoaDon INT,
    Email NVARCHAR(255),
    BienSoXe NVARCHAR(15),
    NgayDat DATETIME DEFAULT GETDATE(),
    TongTien DECIMAL(18,2),
    TrangThai NVARCHAR(50), -- VD: Đã xác nhận, Chưa xác nhận
  --  FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon),
    FOREIGN KEY (Email) REFERENCES NguoiDung(Email),
    FOREIGN KEY (BienSoXe) REFERENCES Xe(BIENSO)
);


--  Xóa ràng buộc khóa ngoại trên cột Email
ALTER TABLE PhieuDatVe
DROP CONSTRAINT FK__PhieuDatV__Email__5BE2A6F2;  

SELECT 
    fk.name AS FK_name,
    tp.name AS Table_name,
    ref.name AS Ref_table_name
FROM 
    sys.foreign_keys AS fk
INNER JOIN 
    sys.tables AS tp ON fk.parent_object_id = tp.object_id
INNER JOIN 
    sys.tables AS ref ON fk.referenced_object_id = ref.object_id
WHERE 
    tp.name = 'PhieuDatVe';




ALTER TABLE PhieuDatVe
DROP COLUMN MaHoaDon;



	CREATE TABLE VeXe (
    MaVe INT PRIMARY KEY IDENTITY(1,1),
    MaPhieu INT FOREIGN KEY REFERENCES PhieuDatVe(MaPhieu),
    MaChuyen INT FOREIGN KEY REFERENCES ChuyenXe(MaChuyen),
	TenVe NVARCHAR (50),
    ViTriGhe NVARCHAR(10), -- Vị trí ghế
    TrangThai NVARCHAR(50), -- VD: Đã đặt, Trống
	GhiChu NVARCHAR (200),
	TenKH NVARCHAR (50),
	Email NVARCHAR(50)

);

--(Quản lý phân quyền người dùng)
CREATE TABLE PhanQuyen (
    MaQuyen INT PRIMARY KEY IDENTITY(1,1),
    TenQuyen NVARCHAR(50) -- Ví dụ: Admin, KhachHang
);

CREATE TABLE BenXe (
    MaBenXe INT PRIMARY KEY IDENTITY(1,1),
    TenBenXe NVARCHAR(100),
    DiaChi NVARCHAR(255),
    SDT NVARCHAR(20)      -- Số điện thoại của bến xe (nếu có)
);
 

 -- Tạo bảng BenXeDen (Bến xe đến)
--DROP
CREATE TABLE BenXeDen (
    MaBenXeDen INT PRIMARY KEY IDENTITY(1,1),
    TenBenXeDen NVARCHAR(100),
    DiaChi NVARCHAR(255),
    SDT NVARCHAR(20) -- Số điện thoại của bến xe đến (nếu có)
);

use QLDatVe
ALTER TABLE NguoiDung
ADD HinhAnh nvarchar(50);



ALTER TABLE Xe
ADD HinhAnh nvarchar(50);


select *from NguoiDung
select *from Xe

ALTER TABLE VeXe
ADD CONSTRAINT FK_VeXe_Vitrighe
FOREIGN KEY (ID_VITRI) REFERENCES Vitrighe(ID_VITRI);



ALTER TABLE VeXe
DROP COLUMN ViTriGhe;


-- Thêm dữ liệu vào bảng PhanQuyen
INSERT INTO PhanQuyen (TenQuyen) VALUES ('Admin');
INSERT INTO PhanQuyen (TenQuyen) VALUES ('KhachHang');

-- Thêm dữ liệu vào bảng BenXe
INSERT INTO BenXe (TenBenXe, DiaChi, SDT) VALUES (N'Bến xe Đà Nẵng', N'Đà Nẵng', '0234567890');

INSERT INTO BenXe (TenBenXe, DiaChi, SDT) VALUES (N'Bến xe Miền Đông Mới ', N'292 Đinh Bộ Lĩnh, Phường 26 Bình Thạnh', '0258967421');

INSERT INTO BenXe (TenBenXe, DiaChi, SDT) VALUES (N'Bến xe Miền Nam', N'
Số 97A An Dương Vương - TP Huế - Thừa Thiên Huế ', '0234567890');

INSERT INTO BenXe (TenBenXe, DiaChi, SDT) VALUES (N'Bến xe CamRanh', 'Số 1 Lê Duẩn, Cam Lộc, Thành phố Cam Ranh, Tỉnh Khánh Hòa', '0258967421');

INSERT INTO BenXe (TenBenXe, DiaChi, SDT) VALUES (N'Bến xe Liên Chiểu', N'56 Tôn Đức Thắng, Quận Liên Chiểu, TP. Đà Nẵng', '0234567890');
INSERT INTO BenXe (TenBenXe, DiaChi, SDT) VALUES (N'Bến xe Biên Hòa', N'1A QL1A, Tân Biên, Biên Hòa, Đồng Nai.', '0213165454');
INSERT INTO BenXe (TenBenXe, DiaChi, SDT) VALUES (N'Bến xe Vũng Tàu', N'200 Phạm Hồng Thái, Phường 8, Thành phố Vũng Tàu, Bà Rịa – Vũng Tàu.', '02223265454');
INSERT INTO BenXe (TenBenXe, DiaChi, SDT) VALUES (N'Bến xe Gia Lai', N' 23 Phan Đình Phùng, Thành phố Pleiku, Gia Lai.', '02231265454');
INSERT INTO BenXe (TenBenXe, DiaChi, SDT) VALUES (N'Bến xe Đông Hà', N' 163 Trần Hưng Đạo, TP. Đông Hà, Quảng Trị', '02231265454');

select *from BenXe
select *from BenXeDen
select *from TuyenXe


-- Thêm dữ liệu vào bảng BenXeDen
INSERT INTO BenXeDen (TenBenXeDen, DiaChi, SDT) VALUES (N'Bến xe Đà Nẵng', N'Đà Nẵng', '0234567890');

INSERT INTO BenXeDen (TenBenXeDen, DiaChi, SDT) VALUES (N'Bến xe Miền Đông Mới ', N'292 Đinh Bộ Lĩnh, Phường 26 Bình Thạnh', '0258967421');

INSERT INTO BenXeDen (TenBenXeDen, DiaChi, SDT) VALUES (N'Bến xe Miền Nam', N'
Số 97A An Dương Vương - TP Huế - Thừa Thiên Huế ', '0234567890');

INSERT INTO BenXeDen (TenBenXeDen, DiaChi, SDT) VALUES (N'Bến xe CamRanh', 'Số 1 Lê Duẩn, Cam Lộc, Thành phố Cam Ranh, Tỉnh Khánh Hòa', '0258967421');

INSERT INTO BenXeDen (TenBenXeDen, DiaChi, SDT) VALUES (N'Bến xe Liên Chiểu', N'56 Tôn Đức Thắng, Quận Liên Chiểu, TP. Đà Nẵng', '0234567890');
INSERT INTO BenXeDen (TenBenXeDen, DiaChi, SDT) VALUES (N'Bến xe Biên Hòa', N'1A QL1A, Tân Biên, Biên Hòa, Đồng Nai.', '0213165454');
INSERT INTO BenXeDen (TenBenXeDen, DiaChi, SDT) VALUES (N'Bến xe Vũng Tàu', N'200 Phạm Hồng Thái, Phường 8, Thành phố Vũng Tàu, Bà Rịa – Vũng Tàu.', '02223265454');
INSERT INTO BenXeDen (TenBenXeDen, DiaChi, SDT) VALUES (N'Bến xe Gia Lai', N' 23 Phan Đình Phùng, Thành phố Pleiku, Gia Lai.', '02231265454');

INSERT INTO BenXeDen (TenBenXeDen, DiaChi, SDT) VALUES (N'Bến xe Đông Hà', N' 163 Trần Hưng Đạo, TP. Đông Hà, Quảng Trị', '02231265454');


select *from BenXeDen



-- Thêm dữ liệu vào bảng NguoiDung
INSERT INTO NguoiDung (Email, SDT, HoTen, MatKhau, MaQuyen, NgaySinh, DiaChi)
VALUES ('admin@admin.com', '0123456789', 'Admin', 'admin123', 1002, '1980-01-01', 'Hà Nội');

INSERT INTO NguoiDung (Email, SDT, HoTen, MatKhau, MaQuyen, NgaySinh, DiaChi)
VALUES ('customer1@customer.com', '0123456789', 'Khách Hàng 1', 'khachhang123', 1003, '1990-05-15', 'Hồ Chí Minh');
select *from NguoiDung
select *from PhanQuyen


-- Thêm dữ liệu vào bảng Xe
INSERT INTO Xe (BIENSO, ID_LOAI, TENXE) VALUES ('29B-12345', 1, 'Xe Limousine Hà Nội - Hồ Chí Minh');
INSERT INTO Xe (BIENSO, ID_LOAI, TENXE) VALUES ('30B-67890', 2, 'Xe Giường Nằm Hà Nội - Đà Nẵng');


-- Thêm dữ liệu vào bảng loaixe
INSERT INTO loaixe (TENLOAI, SOGHE) VALUES ('Xe Limousine', 20);
INSERT INTO loaixe (TENLOAI, SOGHE) VALUES ('Xe Giường Nằm', 30);

-- Thêm dữ liệu vào bảng Vitrighe
INSERT INTO Vitrighe (BIENSO, TENVITRI, TRANGTHAI) VALUES ('29B-12345', '1A', 0);
INSERT INTO Vitrighe (BIENSO, TENVITRI, TRANGTHAI) VALUES ('29B-12345', '1B', 0);


-- Thêm dữ liệu vào bảng TuyenXe
INSERT INTO TuyenXe (DiemDi, DiemDen, SoNgayChayTrongTuan, GiaHienHanh, QuangDuong, MaBenXe, MaBenXeDen)
VALUES 
--(N'Hà Nội', N'Hồ Chí Minh', 7, 1500000, 1700, 1002, 2),
--(N'Hồ Chí Minh', N'Hà Nội', 5, 1400000, 760, 1005, 1),

--(N'Đà Nẵng', N'Hồ Chí Minh', 6, 450000, 900, 2010, 2),
--('Hồ Chí Minh', 'Đà Nẵng', 6, 450000, 900, 1004, 1005),

--(N'Thừa Thiên Huế', N'Đà Nẵng', 7, 100000, 120, 1004, 2005),

(N'Thừa Thiên Huế', N'Hồ Chí Minh', 7, 1000000, 120, 1004, 2),
(N'Hồ Chí Minh', N'Thừa Thiên Huế', 5, 1400000, 760, 1005, 1004),

(N'Quảng Bình', N'Hồ Chí Minh', 4, 350000, 350, 2005, 2),
(N'Hồ Chí Minh', N'Quảng Bình', 4, 1350000, 350, 1005, 2010),

(N'Khánh Hòa', N'Hồ Chí Minh', 4, 350000, 350, 2009, 2),
(N'Hồ Chí Minh', N'Khánh Hòa', 4, 1350000, 350, 1005, 1005);
select *from BenXe
select *from BenXeDen
SELECT *FROM TuyenXe

INSERT INTO TuyenXe (DiemDi, DiemDen, SoNgayChayTrongTuan, GiaHienHanh, QuangDuong, MaBenXe, MaBenXeDen)
VALUES ('Hà Nội', 'Đà Nẵng', 5, 300000, 800, 1003, 2);


-- Thêm dữ liệu vào bảng ChuyenXe
INSERT INTO ChuyenXe (MaTuyen, ThoiDiemKhoiHanh, ThoiDiemDenDuKien, GiaVe, BienSoXe, TenChuyenXe)
VALUES (17, '2024-11-10 08:00', '2024-11-10 18:00', 500000, '29B-12345', 'Chuyến Hà Nội - Hồ Chí Minh');

INSERT INTO ChuyenXe (MaTuyen, ThoiDiemKhoiHanh, ThoiDiemDenDuKien, GiaVe, BienSoXe, TenChuyenXe)
VALUES --(18, '2024-11-11 07:00', '2024-11-11 15:00', 300000, '30B-67890', 'Chuyến Hà Nội - Đà Nẵng'),
(17, '2024-11-11 07:00', '2024-11-11 15:00', 1300000, '29B-12345', N'Chuyến Hà Nội - Hồ Chí Minh'),
(17, '2024-11-11 07:00', '2024-11-11 15:00', 1300000, '29B-54321',  N'Chuyến Hà Nội - Hồ Chí Minh'),
(17, '2024-11-11 07:00', '2024-11-11 15:00', 1300000, '30C-67890', N'Chuyến Hà Nội - Hồ Chí Minh');



(18, '2024-11-11 07:00', '2024-11-11 15:00', 300000, '30B-67890', 'Chuyến Hà Nội - Đà Nẵng'),
(18, '2024-11-11 07:00', '2024-11-11 15:00', 300000, '30B-67890', 'Chuyến Hà Nội - Đà Nẵng'),

Select *from TuyenXe
select *from BenXe
select *from BenXeDen
select *from ChuyenXe
select *from Xe



-- Thêm dữ liệu vào bảng PhieuDatVe
INSERT INTO PhieuDatVe (Email, TongTien, TrangThai)
VALUES ('customer1@customer.com', 500000, 'Đã xác nhận');

-- Thêm dữ liệu vào bảng VeXe
INSERT INTO VeXe (MaPhieu, MaChuyen, TenVe, ViTriGhe, TrangThai, GhiChu, TenKH, Email)
--VALUES (1008, 1011, 'Vé 1A', '1A', 'Đã đặt', 'Không có ghi chú', 'Khách Hàng 1', 'customer1@customer.com');
VALUES (1008, 1011, 'Vé 2A', '2A', 'Đã đặt', 'Không có ghi chú', 'Khách Hàng 2', 'customer1@customer.com');


Select *from TuyenXe
select *from BenXe
select *from BenXeDen
select *from ChuyenXe
select *from PhieuDatVe

select *from Xe
select *from vitrighe


select *from xe
select *from loaixe

select *from vitrighe

DELETE FROM VeXe; -- Xóa dữ liệu ở bảng VeXe
DELETE FROM ChuyenXe; -- Xóa dữ liệu ở bảng ChuyenXe
DELETE FROM TuyenXe; -- Xóa dữ liệu ở bảng TuyenXe
DELETE FROM PhieuDatVe;
DELETE FROM NguoiDung; -- Xóa dữ liệu ở bảng NguoiDung
DELETE FROM BenXe; -- Xóa dữ liệu ở bảng BenXe
DELETE FROM PhanQuyen; -- Xóa dữ liệu ở bảng PhanQuyen



 ALTER TABLE NguoiDung
ADD MaQuyen INT FOREIGN KEY REFERENCES PhanQuyen(MaQuyen);
ALTER TABLE NguoiDung
DROP COLUMN LoaiNguoiDung;
-- Thêm cột MaBenXe vào bảng TuyenXe
ALTER TABLE TuyenXe
ADD MaBenXe INT;

-- : Thiết lập khóa ngoại cho cột MaBenXe để tham chiếu đến MaBenXe trong bảng BenXe
ALTER TABLE TuyenXe
ADD CONSTRAINT FK_TuyenXe_BenXe
FOREIGN KEY (MaBenXe) REFERENCES BenXe(MaBenXe);

-- Xóa dữ liệu trong bảng NguoiDung
DELETE FROM NguoiDung;

-- Xóa dữ liệu trong bảng PhanQuyen
DELETE FROM PhanQuyen;

-- Xóa dữ liệu trong bảng loaixe
DELETE FROM loaixe;

ALTER TABLE ChuyenXe
ADD TenChuyenXe NVARCHAR(100);

-- Thêm cột MaBenXeDen vào bảng TuyenXe để tham chiếu đến BenXeDen
ALTER TABLE TuyenXe
ADD MaBenXeDen INT;

-- Thiết lập khóa ngoại cho cột MaBenXeDen để tham chiếu đến MaBenXeDen trong bảng BenXeDen
ALTER TABLE TuyenXe
ADD CONSTRAINT FK_TuyenXe_BenXeDen
FOREIGN KEY (MaBenXeDen) REFERENCES BenXeDen(MaBenXeDen);



-- Cập nhật độ dài cột BienSoXe trong bảng ChuyenXe
ALTER TABLE ChuyenXe
ALTER COLUMN BienSoXe NVARCHAR(15);


-- Kiểm tra các giá trị BienSoXe trong bảng ChuyenXe nhưng không tồn tại trong bảng Xe
SELECT DISTINCT BienSoXe
FROM ChuyenXe
WHERE BienSoXe NOT IN (SELECT BIENSO FROM Xe);


-- Thêm lại khóa ngoại cho cột BienSoXe trong bảng ChuyenXe
ALTER TABLE ChuyenXe
ADD CONSTRAINT FK_ChuyenXe_BienSoXe FOREIGN KEY (BienSoXe) REFERENCES Xe(BIENSO);



ALTER TABLE Vitrighe
ADD CONSTRAINT FK_Vitrighe_BienSoXe FOREIGN KEY (BIENSO) REFERENCES Xe(BIENSO);



select* from PhieuDatVe

select* from VeXe
select* from vitrighe

ALTER TABLE PhieuDatVe
DROP CONSTRAINT FK__PhieuDatV__BienS__5CD6CB2B;

ALTER TABLE PhieuDatVe
drop column BienSoXe

-- Thêm biển số thiếu vào bảng Xe



-- Cập nhật biển số trong bảng ChuyenXe để khớp với biển số trong bảng Xe
UPDATE ChuyenXe
SET BienSoXe = Xe.BIENSO
FROM ChuyenXe
INNER JOIN Xe ON ChuyenXe.BienSoXe = Xe.BIENSO;



SELECT COLUMN_NAME, DATA_TYPE 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'ChuyenXe';

-- Kiểm tra các biển số trong bảng Xe
SELECT BIENSO FROM Xe;

-- Kiểm tra các biển số trong bảng ChuyenXe
SELECT DISTINCT BienSoXe FROM ChuyenXe;

-- Cập nhật biển số trong bảng ChuyenXe


select *from ChuyenXe
select *from Xe

--Xem khóa ngoại
SELECT 
    fk.name AS FK_name,
    tp.name AS Table_name,
    ref.name AS Referenced_table
FROM 
    sys.foreign_keys fk
    INNER JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
    INNER JOIN sys.tables ref ON fk.referenced_object_id = ref.object_id
WHERE 
    tp.name = 'PhieuDatVe';




-- Kiểm tra lại bảng ChuyenXe
SELECT * FROM ChuyenXe;

-- Kiểm tra lại bảng Xe
SELECT * FROM Xe;

-- Kiểm tra lại bảng Vitrighe
SELECT * FROM Vitrighe;

-- Kiểm tra lại bảng PhieuDatVe
SELECT * FROM PhieuDatVe;





 

	
 select *from TuyenXe
 select *from BenXe
 select *from BenXeDen





--home index tuyenxe-chuyenxe-xe-loaixe-vitrighe-phieudatve-thanhtoan
use QLDatVe
select *from TuyenXe
select *from ChuyenXe
select *from VeXe
select *from PhieuDatVe
select *from xe
select *from vitrighe
select *from loaixe


use QLDatVe
--Stored Procedure: Lấy danh sách các tuyến xe
Create PROCEDURE sp_GetAllTuyenXe
AS
BEGIN
    SELECT MaTuyen, DiemDi, DiemDen, SoNgayChayTrongTuan, GiaHienHanh, QuangDuong, MaBenXe, MaBenXeDen
    FROM TuyenXe;
END;

-- Stored Procedure: Lấy danh sách các chuyến xe theo tuyến
CREATE PROCEDURE sp_GetChuyenXeByTuyenXe
    @MaTuyen INT
AS
BEGIN
    SELECT MaChuyen, MaTuyen, ThoiDiemKhoiHanh, ThoiDiemDenDuKien, GiaVe, BienSoXe, TenChuyenXe
    FROM ChuyenXe
    WHERE MaTuyen = @MaTuyen;
END;

 --Stored Procedure: Thêm mới tuyến xe
CREATE PROCEDURE sp_AddTuyenXe
    @DiemDi NVARCHAR(100),
    @DiemDen NVARCHAR(100),
    @SoNgayChayTrongTuan INT,
    @GiaHienHanh DECIMAL(18,2),
    @QuangDuong INT,
    @MaBenXe INT,
    @MaBenXeDen INT
AS
BEGIN
    INSERT INTO TuyenXe (DiemDi, DiemDen, SoNgayChayTrongTuan, GiaHienHanh, QuangDuong, MaBenXe, MaBenXeDen)
    VALUES (@DiemDi, @DiemDen, @SoNgayChayTrongTuan, @GiaHienHanh, @QuangDuong, @MaBenXe, @MaBenXeDen);
END;



--Stored Procedure: Cập nhật thông tin tuyến xe
CREATE PROCEDURE sp_UpdateTuyenXe
    @MaTuyen INT,
    @DiemDi NVARCHAR(100),
    @DiemDen NVARCHAR(100),
    @SoNgayChayTrongTuan INT,
    @GiaHienHanh DECIMAL(18,2),
    @QuangDuong INT,
    @MaBenXe INT,
    @MaBenXeDen INT
AS
BEGIN
    UPDATE TuyenXe
    SET DiemDi = @DiemDi,
        DiemDen = @DiemDen,
        SoNgayChayTrongTuan = @SoNgayChayTrongTuan,
        GiaHienHanh = @GiaHienHanh,
        QuangDuong = @QuangDuong,
        MaBenXe = @MaBenXe,
        MaBenXeDen = @MaBenXeDen
    WHERE MaTuyen = @MaTuyen;
END;

--Stored Procedure: Xoá tuyến xe
CREATE PROCEDURE sp_DeleteTuyenXe
    @MaTuyen INT
AS
BEGIN
    DELETE FROM TuyenXe
    WHERE MaTuyen = @MaTuyen;
END;


--Stored Procedure: Lấy thông tin ghế ngồi theo chuyến
CREATE PROCEDURE sp_GetSeatsByChuyenXe
    @MaChuyen INT
AS
BEGIN
    SELECT V.ID_VITRI, V.BIENSO, V.TENVITRI, V.TRANGTHAI
    FROM Vitrighe V
    JOIN ChuyenXe C ON V.BIENSO = C.BienSoXe
    WHERE C.MaChuyen = @MaChuyen;
END;


--Stored Procedure: Đặt vé (Thêm phiếu đặt vé)
CREATE PROCEDURE sp_AddPhieuDatVe
    @Email NVARCHAR(255),
    @TongTien DECIMAL(18,2),
    @TrangThai NVARCHAR(50)
AS
BEGIN
    INSERT INTO PhieuDatVe (Email, NgayDat, TongTien, TrangThai)
    VALUES (@Email, GETDATE(), @TongTien, @TrangThai);
    
    SELECT SCOPE_IDENTITY() AS NewPhieuDatVeID; -- Trả về ID phiếu đặt vé mới
END;

--Stored Procedure: Thêm vé xe cho phiếu đặt vé

CREATE PROCEDURE sp_AddVeXe
    @MaPhieu INT,
    @MaChuyen INT,
    @TenVe NVARCHAR(50),
    @ViTriGhe NVARCHAR(10),
    @TrangThai NVARCHAR(50),
    @GhiChu NVARCHAR(200),
    @TenKH NVARCHAR(50),
    @Email NVARCHAR(50)
AS
BEGIN
    INSERT INTO VeXe (MaPhieu, MaChuyen, TenVe, ViTriGhe, TrangThai, GhiChu, TenKH, Email)
    VALUES (@MaPhieu, @MaChuyen, @TenVe, @ViTriGhe, @TrangThai, @GhiChu, @TenKH, @Email);
END;

--Stored Procedure: Lấy thông tin vé theo khách hàng
CREATE PROCEDURE sp_GetVeXeByKhachHang
    @Email NVARCHAR(255)
AS
BEGIN
    SELECT Ve.MaVe, Ve.TenVe, Ve.ViTriGhe, Ve.TrangThai, Ve.GhiChu, Ve.TenKH, Ch.TenChuyenXe, Ch.ThoiDiemKhoiHanh, Ch.ThoiDiemDenDuKien
    FROM VeXe Ve
    JOIN ChuyenXe Ch ON Ve.MaChuyen = Ch.MaChuyen
    WHERE Ve.Email = @Email;
END;

--Stored Procedure: Cập nhật trạng thái vé
CREATE PROCEDURE sp_UpdateVeXeStatus
    @MaVe INT,
    @TrangThai NVARCHAR(50)
AS
BEGIN
    UPDATE VeXe
    SET TrangThai = @TrangThai
    WHERE MaVe = @MaVe;
END;


--EXEC sp_GetAllTuyenXe;


--Trigger cập nhật trạng thái ghế khi có phiếu đặt vé
Drop TRIGGER trg_UpdateSeatStatusOnBooking
ON VeXe
AFTER INSERT
AS
BEGIN
    DECLARE @ViTriGhe NVARCHAR(10), @BienSoXe NVARCHAR(15);
    
    -- Lấy vị trí ghế và biển số từ phiếu đặt vé vừa thêm
    SELECT @ViTriGhe = ViTriGhe, @BienSoXe = BienSoXe
    FROM inserted;

    -- Cập nhật trạng thái ghế thành "Đã đặt"
    UPDATE Vitrighe
    SET TRANGTHAI = 1
    WHERE TENVITRI = @ViTriGhe AND BIENSO = @BienSoXe;
END;



--Trigger tự động xóa phiếu đặt vé khi chuyến xe bị hủy

CREATE TRIGGER trg_DeleteTicketOnTripCancel
ON ChuyenXe
AFTER DELETE
AS
BEGIN
    DECLARE @MaChuyen INT;

    -- Lấy mã chuyến xe bị xóa
    SELECT @MaChuyen = MaChuyen
    FROM deleted;

    -- Xóa các phiếu đặt vé liên quan
    DELETE FROM PhieuDatVe
    WHERE MaChuyen = @MaChuyen;

    -- Xóa các vé xe liên quan
    DELETE FROM VeXe
    WHERE MaChuyen = @MaChuyen;
END;

-- Kiểm tra cấu trúc bảng VeXe
sp_help 'VeXe';



-- Trigger cập nhật tổng tiền phiếu đặt vé


CREATE TRIGGER trg_UpdateTotalAmountOnTicketChange
ON VeXe
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @MaPhieu INT;

    -- Lấy mã phiếu đặt vé từ vé vừa thêm hoặc cập nhật
    SELECT @MaPhieu = MaPhieu
    FROM inserted;

    -- Tính tổng tiền
    UPDATE PhieuDatVe
    SET TongTien = (
        SELECT SUM(GiaVe)
        FROM ChuyenXe
        INNER JOIN VeXe ON ChuyenXe.MaChuyen = VeXe.MaChuyen
        WHERE VeXe.MaPhieu = @MaPhieu
    )
    WHERE PhieuDatVe.MaPhieu = @MaPhieu;
END;

--Trigger tự động cập nhật giá vé khi giá tuyến xe thay đổi
CREATE TRIGGER trg_UpdateTicketPriceOnRouteChange
ON TuyenXe
AFTER UPDATE
AS
BEGIN
    DECLARE @MaTuyen INT, @GiaHienHanh DECIMAL(18,2);

    -- Lấy thông tin tuyến xe vừa được cập nhật
    SELECT @MaTuyen = MaTuyen, @GiaHienHanh = GiaHienHanh
    FROM inserted;

    -- Cập nhật giá vé của các chuyến xe thuộc tuyến đó
    UPDATE ChuyenXe
    SET GiaVe = @GiaHienHanh
    WHERE MaTuyen = @MaTuyen;
END;

---trigger ghế trống khi xóa vé 
CREATE TRIGGER TRG_AfterDelete_VeXe
ON VeXe
AFTER DELETE
AS
BEGIN
    -- Cập nhật trạng thái ghế về false (0) cho những ghế liên quan đến vé bị xóa
    UPDATE Vitrighe
    SET TrangThai = 0
    FROM Vitrighe
    INNER JOIN DELETED ON Vitrighe.ID_VITRI = DELETED.ID_VITRI;
END;
GO


select *from vitrighe
select *from VeXe