
--Level-1 Problem 1: Basic Setup and Data Retrieval in EcommDb
create database eccomDb;
use eccomDb;
CREATE table categories(
 category_id int PRIMARY KEY,
 category_name VARCHAR(50)
);
CREATE table brands(
    brand_id INT PRIMARY KEY,
    brand_name VARCHAR(50)
);
create TABLE products(
    product_id int PRIMARY key,
    product_name VARCHAR(50),
    brand_id INT,
    category_id INT,
    model_year int,
    list_price DECIMAL(10,2),
    FOREIGN KEY(category_id) REFERENCES categories(category_id),
    FOREIGN KEY(brand_id) REFERENCES brands(brand_id)
);
CREATE TABLE customers(
    customer_id int PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    city VARCHAR(50)
);
CREATE TABLE stores(
    store_id INT PRIMARY KEY,
    store_name VARCHAR(50),
    city VARCHAR(50)
);
INSERT INTO categories VALUES
(1,'SUV'),
(2,'Sedan'),
(3,'Hatchback'),
(4,'Electric'),
(5,'Sports');
INSERT INTO brands VALUES
(1,'Toyota'),
(2,'Honda'),
(3,'BMW'),
(4,'Tesla'),
(5,'Hyundai');

INSERT INTO products VALUES
(1,'Toyota Fortuner',1,1,2023,4500000),
(2,'Honda City',2,2,2022,1500000),
(3,'BMW M4',3,5,2024,9000000),
(4,'Tesla Model 3',4,4,2023,5500000),
(5,'Hyundai i20',5,3,2022,900000);

INSERT INTO customers VALUES
(1,'Rahul','Sharma','Delhi'),
(2,'Amit','Verma','Mumbai'),
(3,'Sneha','Reddy','Hyderabad'),
(4,'Priya','Singh','Delhi'),
(5,'Kiran','Patel','Ahmedabad');

INSERT INTO stores VALUES
(1,'AutoHub Delhi','Delhi'),
(2,'Speed Motors','Mumbai'),
(3,'Car Zone','Hyderabad'),
(4,'Drive India','Chennai'),
(5,'Auto Point','Bangalore');

--Write SELECT queries to retrieve all products with their brand and category names.
SELECT p.product_name,b.brand_name,c.category_name,p.model_year,p.list_price 
from products p join brands b on p.brand_id=b.brand_id 
JOIN categories c on p.category_id=c.category_id;

--Retrieve all customers from a specific city.

SELECT city, COUNT(*) AS total_customers FROM customers GROUP BY city ORDER BY total_customers desc;

--Display total number of products available in each category.

SELECT c.category_name,
COUNT(p.product_id) AS total_products FROM categories c
LEFT JOIN products p ON c.category_id = p.category_id
GROUP BY c.category_name;

--Problem 2: Creating Views and Indexes for Performance

--Create a view that shows product name, brand name, category name, model year and list price.

CREATE VIEW vm_productdetils AS
SELECT p.product_name,b.brand_name,c.category_name,p.model_year,p.list_price FROM products p INNER JOIN brands b on p.brand_id = b.brand_id
INNER join categories c ON p.category_id=c.category_id;

select * from vm_productdetils;

CREATE TABLE staffs(
    staff_id INT PRIMARY KEY,
    staff_name VARCHAR(50),
    store_id INT,
    FOREIGN KEY(store_id) REFERENCES stores(store_id)
);
CREATE TABLE orders(
    order_id INT PRIMARY KEY,
    customer_id INT,
    store_id INT,
    staff_id INT,
    order_date DATE,
    FOREIGN KEY(customer_id) REFERENCES customers(customer_id),
    FOREIGN KEY(store_id) REFERENCES stores(store_id),
    FOREIGN KEY(staff_id) REFERENCES staffs(staff_id)
);
INSERT INTO staffs VALUES
(1,'Ramesh',1),
(2,'Suresh',2),
(3,'Anil',3),
(4,'David',4),
(5,'John',5);
INSERT INTO orders VALUES
(101,1,1,1,'2025-01-10'),
(102,2,2,2,'2025-01-12'),
(103,3,3,3,'2025-01-14'),
(104,4,1,1,'2025-01-16'),
(105,5,5,5,'2025-01-18');
--Create a view that shows order details with customer name, store name and staff name.
CREATE VIEW vm_orderdetailes AS
select CONCAT(c.first_name,' ',c.last_name)as customer_name,s.store_name,sf.staff_name,o.order_date from orders o
join customers c on o.customer_id=c.customer_id 
JOIN stores s on o.store_id=s.store_id 
join staffs sf on o.staff_id=sf.staff_id;

select * from vm_orderdetailes;
--Create appropriate indexes on foreign key columns.

CREATE INDEX idx_products_brand
ON products(brand_id);

CREATE INDEX idx_products_category
ON products(category_id);

CREATE INDEX idx_orders_customerx
ON orders(customer_id);

CREATE INDEX idx_orders_store
ON orders(store_id);
