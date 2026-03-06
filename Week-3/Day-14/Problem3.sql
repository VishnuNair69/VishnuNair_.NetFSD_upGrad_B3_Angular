USE SESSION14;

CREATE TABLE stores1(
    store_id INT PRIMARY KEY,
    store_name VARCHAR(50)
);
CREATE TABLE products1(
    product_id INT PRIMARY KEY,
    product_name VARCHAR(50),
    list_price DECIMAL(10,2)
);

CREATE TABLE stocks (
    store_id INT,
    product_id INT,
    quantity INT,
    PRIMARY KEY (store_id, product_id)
);

CREATE TABLE orders1(
    order_id INT PRIMARY KEY,
    store_id INT
);

CREATE TABLE order_items (
    order_id INT,
    product_id INT,
    quantity INT,
    discount DECIMAL(10,2)
);

INSERT INTO stores1 VALUES
(1,'Hyderabad Store'),
(2,'Bangalore Store');

INSERT INTO products1 VALUES
(1,'Yamaha R15',180000),
(2,'KTM RC 200',210000),
(3,'Royal Enfield Classic',190000);

INSERT INTO stocks VALUES
(1,1,0),
(1,2,5),
(1,3,0),
(2,1,10),
(2,2,0),
(2,3,2);

INSERT INTO orders1 VALUES
(101,1),
(102,1),
(103,2);

INSERT INTO order_items VALUES
(101,1,2,5000),
(101,2,1,3000),
(102,3,1,2000),
(103,2,2,4000);

select * from stores1;
SELECT * from products1;
SELECT * from stocks;
select * from orders1;
SELECT * from order_items;
--Identify products sold in each store using nested queries.
SELECT 
    s.store_name,
    p.product_name,
    SUM(oi.quantity) AS total_quantity_sold
FROM order_items oi
JOIN orders1 o 
    ON oi.order_id = o.order_id
JOIN stores1 s 
    ON o.store_id = s.store_id
JOIN products1 p 
    ON oi.product_id = p.product_id
GROUP BY 
    s.store_name,
    p.product_name;

--Compare sold products with current stock using INTERSECT and EXCEPT operators.

SELECT product_id
FROM order_items
INTERSECT
SELECT product_id
FROM stocks
WHERE quantity > 0;

SELECT product_id
FROM order_items
EXCEPT
SELECT product_id
FROM stocks
WHERE quantity > 0;

--Display store_name, product_name, total quantity sold.

SELECT 
    s.store_name,
    p.product_name,
    SUM(oi.quantity) AS total_quantity_sold
FROM order_items oi
JOIN orders1 o 
    ON oi.order_id = o.order_id
JOIN stores1 s 
    ON o.store_id = s.store_id
JOIN products1 p 
    ON oi.product_id = p.product_id
GROUP BY 
    s.store_name,
    p.product_name;

-- Calculate total revenue per product (quantity × list_price – discount).

SELECT 
    p.product_name,
    SUM((oi.quantity * p.list_price) - oi.discount) AS total_revenue
FROM order_items oi
JOIN products1 p 
    ON oi.product_id = p.product_id
GROUP BY 
    p.product_name;
    
--Update stock quantity to 0 for discontinued products (simulation).

UPDATE stocks
SET quantity = 0
WHERE product_id = 3;