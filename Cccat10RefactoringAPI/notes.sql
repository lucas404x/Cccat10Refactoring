-- CREATE TABLES

CREATE TABLE IF NOT EXISTS "Order" (
    "Id" TEXT PRIMARY KEY COLLATE NOCASE,
    "CPF" VARCHAR(11) NOT NULL,
    "From" TEXT,
    "To" TEXT,
  	"CouponId" TEXT,
  	FOREIGN KEY("CouponId") REFERENCES "Coupon" ("Id")
);

CREATE TABLE IF NOT EXISTS "Product" (
    "Id" TEXT PRIMARY KEY COLLATE NOCASE,
    "Description" TEXT,
    "Price" REAL NOT NULL,
    "Width" REAL NOT NULL,
    "Height" REAL NOT NULL,
    "Length" REAL NOT NULL,
    "Weight" REAL NOT NULL
);

CREATE TABLE IF NOT EXISTS "OrderItem" (
    "Id" TEXT PRIMARY KEY COLLATE NOCASE,
    "Quantity" INTEGER NOT NULL,
    "ProductId" TEXT NOT NULL,
    "OrderId" TEXT NOT NULL,
    FOREIGN KEY("OrderId") REFERENCES "Order" ("Id"),
    FOREIGN KEY("ProductId") REFERENCES "Product" ("Id"),
  	UNIQUE ("OrderId", "ProductId")
);


CREATE TABLE IF NOT EXISTS "Coupon" (
    "Id" TEXT PRIMARY KEY COLLATE NOCASE,
    "Description" TEXT,
    "PercentDiscount" REAL,
    "ExpiredDate" TEXT
);

-- POPULATE TABLES

INSERT INTO "Order"("Id","CPF","From","To","CouponId")
VALUES ('41538c75-02a1-4453-af8d-5e4efbfb6773','54571663765',NULL,NULL,'3fa85f64-5717-4562-b3fc-2c963f66afa6');

INSERT INTO "Order"("Id","CPF","From","To","CouponId")
VALUES ('41538c75-02a1-4453-af8d-5e4efbfb6773','54571663765',NULL,NULL,'3fa85f64-5717-4562-b3fc-2c963f66afa6');

INSERT INTO "OrderItem"("Id","Quantity","ProductId","OrderId")
VALUES ('5008260c-a87f-4e1b-b276-206aa2537de1',3,'956855c7-66ad-4905-9634-8a8d417feda1','41538c75-02a1-4453-af8d-5e4efbfb6773');

INSERT INTO "OrderItem"("Id","Quantity","ProductId","OrderId")
VALUES ('29371188-1a56-44e1-8bd8-2e3c1b522539',10,'dc2588d4-77dc-4c77-aa0b-3f0e3af18fdb','41538c75-02a1-4453-af8d-5e4efbfb6773');

-- QUERIES

SELECT * FROM "Order"
LEFT JOIN "Coupon" ON "Order".CouponId = "Coupon".Id
LEFT JOIN "OrderItem" ON "Order".Id = "OrderItem".OrderId
LEFT JOIN "Product" ON "OrderItem".ProductId = "Product".Id
WHERE "Order".Id = '41538c75-02a1-4453-af8d-5e4efbfb6773';

SELECT o.*,c.*,oi.*,p.* FROM "Order" AS o
LEFT JOIN "Coupon" AS c ON o.CouponId = c.Id
LEFT JOIN "OrderItem" AS oi ON o.Id = oi.OrderId
LEFT JOIN "Product" AS p ON oi.ProductId = p.Id
WHERE o.Id = '41538c75-02a1-4453-af8d-5e4efbfb6773';

-- DROPS
DROP TABLE "OrderItem";