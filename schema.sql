CREATE TABLE IF NOT EXISTS Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Username TEXT NOT NULL UNIQUE,
    Password TEXT NOT NULL,
    FullName TEXT
);
CREATE TABLE IF NOT EXISTS thongtintepbai (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    tendaubai TEXT,
    sochuy TEXT,
    bandotapbai TEXT,
    manh1 TEXT,
    manh2 TEXT,
    manh3 TEXT,
    manh4 TEXT,
    chihuyduan TEXT,
    chihuyhaucan TEXT,
    chihuyduan_tt TEXT,
    chihuyhaucan_tt TEXT,
    captren TEXT,
    capminh TEXT,
    User TEXT
);
CREATE TABLE IF NOT EXISTS quansochiendau (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    phienhieudonvi TEXT,
    phdv1 TEXT,
    phdv2 TEXT,
    phdv3 TEXT,
    phdv4 TEXT,
    phdv5 TEXT,
    quansochiendau TEXT,
    qscd1 TEXT,
    qscd2 TEXT,
    qscd3 TEXT,
    qscd4 TEXT,
    qscd5 TEXT,
    User TEXT
);
CREATE TABLE VatChatNguoiDung (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    vcId INTEGER NOT NULL,
    SoLuong TEXT, 
    GhiChu TEXT         
);
CREATE TABLE QuyDinhDuTruTieuThuVoSung (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    vcId INTEGER NOT NULL,
    QuyDinhDuTru REAL,
    PhaiCo0400N REAL,
    PhaiCSCD REAL,
    TieuThuGDCB REAL,
    TieuThuGDCD REAL
);