-- SQLite
CREATE TABLE IF NOT EXISTS "Order" (
    "Id" TEXT PRIMARY KEY COLLATE NOCASE,
    "CPF" VARCHAR(11),
    "From" TEXT,
    "To" TEXT
);

CREATE TABLE IF NOT EXISTS "Coupon" (
    "Id" TEXT PRIMARY KEY COLLATE NOCASE,
    "Description" TEXT,
    "PercentDiscount" REAL,
    "ExpiredDate" TEXT
);

--DROP TABLE "Order"
--DROP TABLE "Coupon"

SELECT * FROM "Order";

SELECT * FROM "Coupon";