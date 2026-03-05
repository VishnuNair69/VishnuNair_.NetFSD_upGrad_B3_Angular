CREATE DATABASE SESSION14;

USE SESSION14;

CREATE TABLE products
(
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    category_id INT,
    model_year INT,
    list_price DECIMAL(10,2)
);

INSERT INTO products VALUES
(1,'Trek X1',1,2017,1200),
(2,'Trek X2',1,2018,1500),
(3,'Trek X3',1,2019,1000),

(4,'Giant A1',2,2017,900),
(5,'Giant A2',2,2018,1100),
(6,'Giant A3',2,2019,1300),

(7,'Scott S1',3,2018,2000),
(8,'Scott S2',3,2019,1800);

SELECT * FROM products;



--. Retrieve product details (product_name, model_year, list_price).
SELECT 
    product_name,
    model_year,
    list_price
FROM products;
    
--Compare each product’s price with the average price of products in the same category using a nested query.
SELECT category_id, AVG(list_price) AS avg_price
FROM products
GROUP BY category_id;

SELECT 
    product_name + ' (' + CAST(model_year AS VARCHAR) + ')' AS product_info,
    product_name,
    model_year,
    list_price,
    
    list_price - (
        SELECT AVG(list_price)
        FROM products
        WHERE category_id = p.category_id
    ) AS price_difference

FROM products p

WHERE list_price >
(
    SELECT AVG(list_price)
    FROM products
    WHERE category_id = p.category_id
);