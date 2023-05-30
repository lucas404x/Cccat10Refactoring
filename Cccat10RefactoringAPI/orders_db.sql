-- SQLite
CREATE TABLE IF NOT EXISTS "Order" (
    "Id" TEXT PRIMARY KEY COLLATE NOCASE,
    "CPF" VARCHAR(11) NOT NULL,
    "From" TEXT,
    "To" TEXT
);

CREATE TABLE IF NOT EXISTS "Product" (
    "Id" TEXT PRIMARY KEY COLLATE NOCASE,
    "Description" TEXT,
    "Price" REAL NOT NULL,
    "Width" REAL NOT NULL,
    "Height" REAL NOT NULL,
    "Length" REAL NOT NULL,
    "Weight" REAL NOT NULL
)

CREATE TABLE IF NOT EXISTS "OrderItem" (
    "Id" TEXT PRIMARY KEY COLLATE NOCASE,
    "Quantity" INTEGER NOT NULL,
    "ProductId" TEXT NOT NULL,
    "OrderId" TEXT NOT NULL,
    FOREIGN KEY("OrderId") REFERENCES "Order" ("Id"),
    FOREIGN KEY("ProductId") REFERENCES "Product" ("Id")
);


CREATE TABLE IF NOT EXISTS "Coupon" (
    "Id" TEXT PRIMARY KEY COLLATE NOCASE,
    "Description" TEXT,
    "PercentDiscount" REAL,
    "ExpiredDate" TEXT
);
--DROP TABLE "OrderItem"
--DROP TABLE "Order"
--DROP TABLE "Coupon"
SELECT *
FROM "Order";
SELECT *
FROM "Coupon";