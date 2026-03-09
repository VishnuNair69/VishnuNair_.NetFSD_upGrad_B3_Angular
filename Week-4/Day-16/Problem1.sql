	CREATE DATABASE DAY16;
	USE DAY16;

	CREATE TABLE stores
(
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100)
);

CREATE TABLE orders
(
    order_id INT PRIMARY KEY,
    store_id INT,
    order_date DATE
);

CREATE TABLE order_items
(
    order_id INT,
    product_id INT,
    quantity INT,
    list_price DECIMAL(10,2),
    discount DECIMAL(4,2)
);

CREATE TABLE products
(
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100)
);

INSERT INTO stores VALUES
(1,'New York Store'),
(2,'California Store');

INSERT INTO products VALUES
(1,'Car Model A'),
(2,'Car Model B'),
(3,'Car Model C');

INSERT INTO orders VALUES
(101,1,'2024-01-10'),
(102,1,'2024-02-05'),
(103,2,'2024-02-15');

INSERT INTO order_items VALUES
(101,1,2,5000,0.10),
(101,2,1,7000,0.05),
(102,1,1,5000,0.10),
(103,3,3,6000,0.08);



--Create a stored procedure to generate total sales amount per store.


CREATE PROCEDURE sp_TotalSalesPerStore
AS
BEGIN
    SELECT 
        s.store_name,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_sales
    FROM stores s
    INNER JOIN orders o
        ON s.store_id = o.store_id
    INNER JOIN order_items oi
        ON o.order_id = oi.order_id
    GROUP BY s.store_name
END

EXEC sp_TotalSalesPerStore;


--Create a stored procedure to retrieve orders by date range.


CREATE PROCEDURE sp_GetOrdersByDateRange
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT *
    FROM orders
    WHERE order_date BETWEEN @StartDate AND @EndDate
END

EXEC sp_GetOrdersByDateRange '2024-01-01','2024-02-01';


--Create a scalar function to calculate total price after discount.


CREATE FUNCTION fn_CalculateDiscountPrice
(
    @price DECIMAL(10,2),
    @discount DECIMAL(4,2)
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN @price * (1 - ISNULL(@discount,0))
END

SELECT 
product_id,
dbo.fn_CalculateDiscountPrice(list_price,discount) AS final_price
FROM order_items;


--- Create a table-valued function to return top 5 selling products.


CREATE FUNCTION fn_Top5SellingProducts()
RETURNS TABLE
AS
RETURN
(
    SELECT TOP 5
        p.product_name,
        SUM(oi.quantity) AS total_quantity
    FROM products p
    INNER JOIN order_items oi
        ON p.product_id = oi.product_id
    GROUP BY p.product_name
    ORDER BY total_quantity DESC
);

SELECT * 
FROM dbo.fn_Top5SellingProducts();