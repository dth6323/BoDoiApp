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