-- Run this script ONCE after docker-compose up
-- Creates tables in each microservice database

-- ── Product Service DB ────────────────────────────────────────────
USE ShopEZ_Products;
GO
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Products' AND xtype='U')
CREATE TABLE Products (
    ProductId   INT IDENTITY(1,1) PRIMARY KEY,
    Name        NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Price       DECIMAL(18,2) NOT NULL,
    ImageUrl    NVARCHAR(500),
    Stock       INT NOT NULL DEFAULT 0
);

-- Seed data
INSERT INTO Products (Name, Description, Price, ImageUrl, Stock)
VALUES
  ('Laptop Pro X',       'High-performance laptop',     75000.00, '/images/laptop.jpg',   15),
  ('Wireless Headphones','Noise-cancelling headphones',   3500.00, '/images/headphones.jpg',30),
  ('USB-C Hub',          '7-in-1 multiport adapter',      1800.00, '/images/hub.jpg',      50);
GO
