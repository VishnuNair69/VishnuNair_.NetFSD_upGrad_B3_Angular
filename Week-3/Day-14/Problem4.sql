USE SESSION14;

CREATE TABLE customers1(
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50)
);

CREATE TABLE orders2(
    order_id INT PRIMARY KEY,
    customer_id INT,
    order_date DATE,
    required_date DATE,
    shipped_date DATE,
    order_status INT,
    FOREIGN KEY (customer_id) REFERENCES customers1(customer_id)
);
CREATE TABLE archived_orders (
    order_id INT,
    customer_id INT,
    order_date DATE,
    required_date DATE,
    shipped_date DATE,
    order_status INT
);

INSERT INTO customers1 VALUES
(1,'Ravi','Kumar'),
(2,'Anita','Sharma'),
(3,'Rahul','Verma');

INSERT INTO orders2 VALUES
(101,1,'2023-01-10','2023-01-15','2023-01-14',4),
(102,1,'2023-02-10','2023-02-15','2023-02-18',4), 
(103,2,'2022-01-05','2022-01-10','2022-01-09',3),
(104,3,'2024-03-01','2024-03-05','2024-03-04',4), 
(105,2,'2024-01-10','2024-01-15','2024-01-14',4);

-- Archive rejected orders older than 1 year
INSERT INTO archived_orders
SELECT *
FROM orders2
WHERE order_status = 3
AND order_date < DATEADD(YEAR,-1,GETDATE());


-- Delete archived orders from main table
DELETE FROM orders2
WHERE order_id IN (
    SELECT order_id
    FROM archived_orders
);

-- Customers whose all orders are completed
SELECT customer_id
FROM orders2
GROUP BY customer_id
HAVING COUNT(*) =
(
    SELECT COUNT(*)
    FROM orders2 o2
    WHERE o2.customer_id = orders2.customer_id
    AND o2.order_status = 4
);

-- Order processing delay
SELECT 
order_id,
DATEDIFF(DAY, order_date, shipped_date) AS processing_delay
FROM orders2;

-- Delayed or On Time
SELECT 
order_id,
order_date,
required_date,
shipped_date,
CASE
    WHEN shipped_date > required_date THEN 'Delayed'
    ELSE 'On Time'
END AS delivery_status
FROM orders2;