--
-- File generated with SQLiteStudio v3.2.1 on Wed Aug 5 21:33:42 2020
--
-- NVARCHAR(MAX) encoding used: UTF-8
--
BEGIN TRANSACTION;

-- Table: ChuDes
CREATE TABLE ChuDes (
    Id       INTEGER IDENTITY(1, 1),
    Ten      NVARCHAR(MAX),
    NoiDung  NVARCHAR(MAX),
    ThoiGian DATETIME,
    PRIMARY KEY (
        Id
    )
);

SET IDENTITY_INSERT ChuDes ON
INSERT INTO ChuDes (Id, Ten, NoiDung, ThoiGian) VALUES (1, N'Ngợi Khen Đức Chúa Trời', NULL, NULL);
SET IDENTITY_INSERT ChuDes OFF

-- Table: BoDes
CREATE TABLE BoDes (
    Id      INTEGER IDENTITY(1, 1),
    Ten     NVARCHAR(MAX),
    ChuDeId INTEGER,
    FOREIGN KEY (
        ChuDeId
    )
    REFERENCES ChuDes (Id),
    PRIMARY KEY (
        Id
    )
);

-- Table: GopYPhanMems
CREATE TABLE GopYPhanMems (
    Id          INTEGER IDENTITY(1, 1),
    TieuDe    NVARCHAR(MAX),
    NoiDung   NVARCHAR(MAX),
    CreatedOn DATETIME,
    PRIMARY KEY (
        Id
    )
);


-- Table: LoaiThanhCas
CREATE TABLE LoaiThanhCas (
    Id  INTEGER IDENTITY(1, 1),
    Ten NVARCHAR(MAX),
    PRIMARY KEY (
        Id
    )
);

SET IDENTITY_INSERT LoaiThanhCas ON
INSERT INTO LoaiThanhCas (Id, Ten) VALUES (1, N'Thánh Ca');
INSERT INTO LoaiThanhCas (Id, Ten) VALUES (2, N'Tôn Vinh Chúa Hằng Hữu');
INSERT INTO LoaiThanhCas (Id, Ten) VALUES (3, N'Biệt Thánh Ca');
INSERT INTO LoaiThanhCas (Id, Ten) VALUES (4, N'Kinh Thánh Đối Đáp');
SET IDENTITY_INSERT LoaiThanhCas OFF

-- Table: MediaTypes
CREATE TABLE MediaTypes (
    Id                     INTEGER IDENTITY(1, 1),
    Ten                    NVARCHAR(MAX),
    MoTa                   NVARCHAR(MAX),
    DunLuongUploadToiDa    INTEGER,
    DungLuongDownloadToiDa INTEGER,
    DinhDang               NVARCHAR(MAX),
    PRIMARY KEY (
        Id
    )
);

SET IDENTITY_INSERT MediaTypes ON
INSERT INTO MediaTypes (Id, Ten, MoTa, DunLuongUploadToiDa, DungLuongDownloadToiDa, DinhDang) VALUES (1, 'Doc', N'Tệp văn bản', 10, 10, '[''txt'', ''doc'', ''docx'', ''ppt'', ''pptx'']');
INSERT INTO MediaTypes (Id, Ten, MoTa, DunLuongUploadToiDa, DungLuongDownloadToiDa, DinhDang) VALUES (2, 'Image', N'Tệp hình ảnh', 10, 10, '[''jpeg'', ''png'']');
INSERT INTO MediaTypes (Id, Ten, MoTa, DunLuongUploadToiDa, DungLuongDownloadToiDa, DinhDang) VALUES (3, 'Video', N'Tệp video', 10, 10, '[''mp4'', ''wmv'', ''avi'']');
SET IDENTITY_INSERT MediaTypes OFF

-- Table: Medias
CREATE TABLE Medias (
    Id      INTEGER IDENTITY(1, 1),
    Ten     NVARCHAR(MAX),
    MoTa    NVARCHAR(MAX),
    Link    NVARCHAR(MAX),
    ChuDeId INTEGER,
    Loai    INTEGER,
	LuotXem    INTEGER,
	LuotTai    INTEGER,
	TrangThai INTEGER,
    FOREIGN KEY (
        Loai
    )
    REFERENCES MediaTypes (Id),
    FOREIGN KEY (
        ChuDeId
    )
    REFERENCES ChuDes (Id),
    PRIMARY KEY (
        Id
    )
);

SET IDENTITY_INSERT Medias ON
INSERT INTO Medias (Id, Ten, MoTa, Link, ChuDeId, Loai, LuotXem, LuotTai, TrangThai) VALUES (1, 'Cui_Xin_Vua_Thanh_Ngu_Lai', N'TC 001 - Cúi Xin Vua Thánh Ngự Lai', 'TC_PP/001-Cui_Xin_Vua_Thanh_Ngu_Lai.PPTX', 1, 1, 0, 0, 0);
INSERT INTO Medias (Id, Ten, MoTa, Link, ChuDeId, Loai, LuotXem, LuotTai, TrangThai) VALUES (2, 'Nguyen_Tung_My_Chua_Linh_Nang', N'TC 002 - Nguyện Tụng Mỹ Chúa Linh Năng', 'TC_PP/002-Nguyen_Tung_My_Chua_Linh_Nang.PPTX', 1, 1, 0, 0, 0);
SET IDENTITY_INSERT Medias OFF

-- Table: ThanhCas
CREATE TABLE ThanhCas (
    Id        INTEGER IDENTITY(1, 1),
    STT       INTEGER,
    Ten       NVARCHAR(MAX),
    SoCau     INTEGER,
    Loai      INTEGER,
    DiepKhuc  NVARCHAR(MAX),
    PRIMARY KEY (
        Id
    ),
    FOREIGN KEY (
        Loai
    )
    REFERENCES LoaiThanhCas (Id) 
);

SET IDENTITY_INSERT ThanhCas ON
INSERT INTO ThanhCas (Id, STT, Ten, SoCau, Loai, DiepKhuc) VALUES (1, 1, N'Cúi Xin Vua Thánh Ngự Lai', 4, 1, NULL);
INSERT INTO ThanhCas (Id, STT, Ten, SoCau, Loai, DiepKhuc) VALUES (2, 2, N'Nguyện Tụng Mỹ Chúa Linh Năng', 5, 1, NULL);
INSERT INTO ThanhCas (Id, STT, Ten, SoCau, Loai, DiepKhuc) VALUES (3, 3, N'Ngợi Giê-hô-va Thánh Đế', 4, 1, '');
INSERT INTO ThanhCas (Id, STT, Ten, SoCau, Loai, DiepKhuc) VALUES (4, 4, N'Ha-lê-lu-gia! Vinh Danh Ngài', 6, 1, N'Ha-lê-lu-gia! Vinh danh Ngài! 

A-men chúng tôi tôn thờ đây; 

Ha-lê-lu-gia! Vinh danh Ngài!

Nguyện được yêu Chúa thêm lên hoài.');
INSERT INTO ThanhCas (Id, STT, Ten, SoCau, Loai, DiepKhuc) VALUES (5, 5, N'Muôn Dân Trên Hoàn Cầu Nên Ca Xướng', 4, 1, NULL);
INSERT INTO ThanhCas (Id, STT, Ten, SoCau, Loai, DiepKhuc) VALUES (6, 6, N'Thành Tâm Tôn Vua Thánh', 6, 1, '');
INSERT INTO ThanhCas (Id, STT, Ten, SoCau, Loai, DiepKhuc) VALUES (7, 7, N'Ca Cảm Tạ', 3, 1, '');
INSERT INTO ThanhCas (Id, STT, Ten, SoCau, Loai, DiepKhuc) VALUES (8, 8, N'Ngợi Danh Jêsus Rất Oai Quyền', 5, 1, NULL);
INSERT INTO ThanhCas (Id, STT, Ten, SoCau, Loai, DiepKhuc) VALUES (9, 9, N'Ước Thuật Chuyện Tuyệt Đối', 4, 1, NULL);
INSERT INTO ThanhCas (Id, STT, Ten, SoCau, Loai, DiepKhuc) VALUES (10, 10, N'Nguyện Tụng Ngợi Chiên Con Thánh', 4, 1, NULL);
SET IDENTITY_INSERT ThanhCas OFF

-- Table: MediaThanhCas
CREATE TABLE MediaThanhCas (
    Id        INTEGER IDENTITY(1, 1),
    MediaId   INTEGER,
    ThanhCaId INTEGER,
    FOREIGN KEY (
        ThanhCaId
    )
    REFERENCES ThanhCas (Id),
    FOREIGN KEY (
        MediaId
    )
    REFERENCES Medias (Id),
    PRIMARY KEY (
        Id
    )
);

SET IDENTITY_INSERT MediaThanhCas ON
INSERT INTO MediaThanhCas (Id, MediaId, ThanhCaId) VALUES (1, 1, 1);
INSERT INTO MediaThanhCas (Id, MediaId, ThanhCaId) VALUES (2, 2, 2);
SET IDENTITY_INSERT MediaThanhCas OFF

-- Table: NgonNgus
CREATE TABLE NgonNgus (
    Id   INTEGER NOT NULL IDENTITY(1, 1),
    Ten  NVARCHAR(MAX),
    MoTa NVARCHAR(MAX),
    PRIMARY KEY (
        Id
    )
);

SET IDENTITY_INSERT NgonNgus ON
INSERT INTO NgonNgus (Id, Ten, MoTa) VALUES (1, 'Vietnamese', N'Tiếng Việt');
INSERT INTO NgonNgus (Id, Ten, MoTa) VALUES (2, 'English', N'Tiếng Anh');
SET IDENTITY_INSERT NgonNgus OFF

-- Table: PhienBans
CREATE TABLE PhienBans (
    Id  INTEGER IDENTITY(1, 1),
    Ten NVARCHAR(MAX),
    Nam INTEGER,
    PRIMARY KEY (
        Id
    )
);

-- Table: Sachs
CREATE TABLE Sachs (
    Id           INTEGER IDENTITY(1, 1),
    STT          INTEGER,
    Ten          NVARCHAR(MAX),
    TongSoChuong INTEGER,
    PhienBanId   INTEGER,
    PRIMARY KEY (
        Id
    ),
    FOREIGN KEY (
        PhienBanId
    )
    REFERENCES PhienBans (Id) 
);

-- Table: Templates
CREATE TABLE Templates (
    Id      INTEGER IDENTITY(1, 1),
    MediaId INTEGER,
    FOREIGN KEY (
        MediaId
    )
    REFERENCES Medias (Id),
    PRIMARY KEY (
        Id
    )
);

-- Table: CauKinhThanhs
CREATE TABLE CauKinhThanhs (
    Id         INTEGER IDENTITY(1, 1),
    SachId     INTEGER,
    Chuong     INTEGER,
    STT        INTEGER,
    NoiDung    NVARCHAR(MAX),
    TemplateId INTEGER,
	PRIMARY KEY (
        Id
    ),
    FOREIGN KEY (
        TemplateId
    )
    REFERENCES Templates (Id),
    FOREIGN KEY (
        SachId
    )
    REFERENCES Sachs (Id)
);

-- Table: BanDichCaus
CREATE TABLE BanDichCaus (
    Id          INTEGER IDENTITY(1, 1),
    CauId       INTEGER,
    NoiDungDich NVARCHAR(MAX),
    NgonNguId   INTEGER,
    PRIMARY KEY (
        Id
    ),
    FOREIGN KEY (
        CauId
    )
    REFERENCES CauKinhThanhs (Id),
    FOREIGN KEY (
        NgonNguId
    )
    REFERENCES NgonNgus (Id) 
);


-- Table: BanDichPhienBans
CREATE TABLE BanDichPhienBans (
    Id          INTEGER IDENTITY(1, 1),
    PhienBanId          INTEGER,
    NoiDungDich NVARCHAR(MAX),
    NgonNguId   INTEGER,
    FOREIGN KEY (
        PhienBanId
    )
    REFERENCES PhienBans (Id),
    FOREIGN KEY (
        NgonNguId
    )
    REFERENCES NgonNgus (Id),
    PRIMARY KEY (
        Id
    )
);


-- Table: BanDichSachs
CREATE TABLE BanDichSachs (
    Id          INTEGER IDENTITY(1, 1),
    SachId      INTEGER,
    NoiDungDich NVARCHAR(MAX),
    NgonNguId   INTEGER,
    FOREIGN KEY (
        NgonNguId
    )
    REFERENCES NgonNgus (Id),
    FOREIGN KEY (
        SachId
    )
    REFERENCES Sachs (Id),
    PRIMARY KEY (
        Id
    )
);

-- Table: CauHois
CREATE TABLE CauHois (
    Id          INTEGER IDENTITY(1, 1),
    NoiDung NVARCHAR(MAX),
    MediaId INTEGER,
    FOREIGN KEY (
        MediaId
    )
    REFERENCES Medias (Id),
    PRIMARY KEY (
        Id
    )
);

-- Table: DapAns
CREATE TABLE DapAns (
    Id          INTEGER IDENTITY(1, 1),
    NoiDung        NVARCHAR(MAX),
    CauKinhThanhId INTEGER,
    PRIMARY KEY (
        Id
    ),
    FOREIGN KEY (
        CauKinhThanhId
    )
    REFERENCES CauKinhThanhs (Id) 
);

-- Table: CauDos
CREATE TABLE CauDos (
    Id       INTEGER UNIQUE,
    BoDeId   INTEGER,
    CauHoiId INTEGER,
    DapAnId  INTEGER,
    GoiY     NVARCHAR(MAX),
    FOREIGN KEY (
        BoDeId
    )
    REFERENCES BoDes (Id),
    FOREIGN KEY (
        CauHoiId
    )
    REFERENCES CauHois (Id),
    FOREIGN KEY (
        DapAnId
    )
    REFERENCES DapAns (Id),
    PRIMARY KEY (
        BoDeId,
        CauHoiId,
        DapAnId
    )
);

-- Table: LoiBaiHats
CREATE TABLE LoiBaiHats (
    Id        INTEGER IDENTITY(1, 1),
    ThanhCaId INTEGER,
    STT       INTEGER,
    NoiDung   NVARCHAR(MAX),
    FOREIGN KEY (
        ThanhCaId
    )
    REFERENCES ThanhCas (Id),
    PRIMARY KEY (
        Id
    )
);

SET IDENTITY_INSERT LoiBaiHats ON
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (1, 1, 1, N'Hỡi Thánh Vương kíp ngự lai.

Hộ tôi cung chúc danh Ngài.

Hát lên khen ngợi;

ngợi cha sáng láng trên trời,

là danh chiến thắng muôn đời.

Nguyền quang lâm trên chính tôi, Chúa thánh muôn đời.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (2, 1, 2, N'Cúi xin đạo thể ngự lai.

Nịt gươm chói sáng hiển oai.

Đoái nghe tôi nài;

Nguyện ban tứ thánh dân Ngài,

dùng chân lý nuôi tâm hoài.

Thần Nhân ôi xin giáng đây, ở chính tâm nầy.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (3, 1, 3, N'Đấng ủy lạo hỡi ngự lai.

Nguyện đem thánh chứng theo Ngài.

Chính phút vui nầy:

Nguyện xin Thánh Linh năng tài,

quyền thiêng sức thiêng ai tày,

ngự vô tâm tôi Chúa ơi, quản cai muôn đời.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (4, 1, 4, N'Hát khen muôn thuở nào thôi,

hiệp tôn Chân Chúa Ba Ngôi,

Chúa trên muôn loài!

Nguyền trong hiển vinh nơi Ngài,

được xem đáng tôn nghiêm hoài.

Thờ tôn, yêu thương Chúa tôi trải đến đời đời.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (5, 2, 1, N'Nguyện tụng mỹ Chúa Linh Năng,

Vua Vinh Diệu tạo vạn vật chúng dân!

Này hồn hỡi, hãy tôn ca Ngài,

là nguồn lực lượng, sông cứu ân!

Tôn vinh danh Chúa, đồng tâm mãi trổi khúc kim cầm,

cùng ngợi Ba Ngôi nhân ái chí thâm.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (6, 2, 2, N'Nguyện tụng mỹ Chúa Đấng cai trị vạn vật,

khôn ngoan quảng thông;

Ngài dùng cánh chim ưng nâng ngươi,

ban đều cao hơn ngươi ước mong;

Bao nhiêu nhu yếu Ngài hằng ban cho không diên trì,

lòng Ngài thật ngươi không thể am tri.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (7, 2, 3, N'Nguyện tụng mỹ Chúa,

Đấng đem muôn sự hiệp lại làm lợi ích ngươi.

Từ từ dẫn dắt ngươi ban sinh lực dồi dào đời ngươi thắm tươi!

Trong cơn nguy biến Ngài giơ tay che ngươi mọi chiều,

dường gà mẹ che con khỏi chim diều.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (8, 2, 4, N'Nguyện tụng mỹ Chúa ban ngươi vô luận địa vị nào may mắn luôn.

Hằng ngày cứ điểm trang ngươi do ơn lành từ trời cao đổ tuôn.

Khi ngươi suy nghĩ quyền năng Thánh Chúa cao lạ lùng,

thì lòng ngươi nên vui vẻ vô cùng.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (9, 2, 5, N'Nguyện tụng mỹ Chúa,

hỡi linh hồn cùng mọi điều chi trong chính ta.

Này toàn thể chúng sinh mau tung hô thờ phượng Danh Giê-hô-va!

Muôn muôn dân thánh đồng thanh hô “A-men” rập ràng,

thờ lạy Vua muôn Vua cách hân hoan.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (10, 3, 1, N'Ngợi Giê-hô-va Thánh Ðế,

Là Chân Chúa Áp-ra-ham,

Từ xưa cho đến vĩnh viễn độc tôn, bác ái siêu phàm,

Trời đất hết thảy chứng minh;

Từ hữu vĩnh hữu duy Ngài,

Quì đây tôi tung hô Ðấng Chí Thánh hiển vinh lâu dài.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (11, 3, 2, N'Ngợi Giê-hô-va Thánh Ðế,

Nhờ linh cao minh Chúa ban,

Dìu tôi xa nhân thế đến thượng thiên,

Thỏa vui nhẹ nhàng, Kể phú quí thảy ảo không,

Chọn phần sở hửu duy Ngài,

Là khiên che thân, cây tháp phủ bóng,

Ðở che tôi hoài.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (12, 3, 3, N'Nghìn muôn tinh binh đắc thắng.

Tạ ơn Ba Ngôi Thánh thay,

Ngợi Cha, Con, Linh, ca khúc trầm thăng,

Chúc tôn đêm ngày, Chúa Áp-ra-ham Chúa tôi,

Tôi cùng con Áp-ra-ham Tụng ca Ba Ngôi Chân Chúa vô đối,

Dũng uy siêu phàm.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (13, 4, 1, N'Lạy Thần đời đời ban ơn phước, 

Ai có thể ca khen cho được?');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (14, 4, 2, N'Đạo từ lòng Ngài xưa ban phát,

Tâm chúng tôi nay nghe thông đạt.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (15, 4, 3, N'Vì tội người Ngài dâng sinh tế, 

Công giá trả xong xuôi mọi bề.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (16, 4, 4, N'Lòng này mờ mịt như đêm tối,

Cha lấy linh quang soi rạng ngời.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (17, 4, 5, N'Ngài hiện diện lời xưa tuyên ấn,

Đau khổ lui xa khi Ngài gần.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (18, 4, 6, N'Ngài định ngày đại ban ân thưởng, 

Phu phỉ chúng tôi trong mọi đường.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (19, 5, 1, N'Muôn dân trên hoàn cầu nên ca xướng, chúc tán Chúa Cha, thật thỏa thích thay!

Lòng vui hát, siêng năng hầu việc luôn, Khá đến ngợi khen Chúa cách vui vầy.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (20, 5, 2, N'Giê-hô-va thật là Ngôi Chân Chúa, chính Chúa chẳng ai tạo hóa thế gian;

Hằng nuôi chúng ta trong bầy Ngài mua, Dẫn dắt mọi chiên cách rất vẹn toàn.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (21, 5, 3, N'Hân hoan ta vào đền Vua minh chánh, cất tiếng hát trong Hội Thánh Chúa Cha.

Đồng tâm cám ơn, khen ngợi hòa thanh, chúc danh Ngài luôn hiển vinh rạng lòa.');
INSERT INTO LoiBaiHats (Id, ThanhCaId, STT, NoiDung) VALUES (22, 5, 4, N'Ba Ngôi Chân Thần từ bi chan chứa, ái đức Chúa tôi đằm thắm thủy chung.

Nguồn chân lý đây nguyên từ ngàn xưa, suốt bao thời gian đứng vững vô cùng.');
SET IDENTITY_INSERT LoiBaiHats OFF

COMMIT TRANSACTION;
