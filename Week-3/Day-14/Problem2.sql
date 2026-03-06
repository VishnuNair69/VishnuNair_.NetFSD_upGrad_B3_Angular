USE SESSION14;

CREATE TABLE customers (
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50)
);

CREATE TABLE orders (
    order_id INT PRIMARY KEY,
    customer_id INT,
    order_value DECIMAL(10,2),
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id)
);

INSERT INTO customers VALUES
(1, 'Rahul', 'Sharma'),
(2, 'Anita', 'Reddy'),
(3, 'Vikram', 'Singh'),
(4, 'Priya', 'Nair'),
(5, 'Arjun', 'Kumar');

INSERT INTO orders VALUES
(101, 1, 4000),
(102, 1, 3000),
(103, 2, 12000),
(104, 3, 2000),
(105, 3, 1000);

SELECT CONCAT(c.first_name,' ',c.last_name)AS fullname,
(SELECT SUM(o.order_value) FROM orders o WHERE o.customer_id = c.customer_id) AS total_order_value,
     case
      WHEN (SELECT SUM(o.order_value)
              FROM orders o
              WHERE o.customer_id = c.customer_id) > 10000
             THEN 'Premium'
      WHEN (SELECT SUM(o.order_value)
              FROM orders o
              WHERE o.customer_id = c.customer_id) BETWEEN 5000 AND 10000
             THEN 'Regular'
        ELSE 'Basic'
    END AS customer_type
FROM customers c
WHERE c.customer_id IN (SELECT customer_id FROM orders)
UNION
SELECT 
    CONCAT(first_name,' ',last_name) AS full_name,
    0 AS total_order_value,
    'No Orders' AS customer_type
FROM customers
WHERE customer_id NOT IN (SELECT customer_id FROM orders);